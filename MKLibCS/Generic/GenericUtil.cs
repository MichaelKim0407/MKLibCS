﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using MKLibCS.Collections;
using MKLibCS.Reflection;

namespace MKLibCS.Generic
{
    /// <summary>
    /// </summary>
    public static partial class GenericUtil
    {
        static GenericUtil()
        {
            InitCreate();
            InitParseFormat();
        }

        private static List<Type> initFinished = new List<Type>();
        private static List<Type> initOngoing = new List<Type>();

        /// <summary>
        /// </summary>
        /// <param name="type"></param>
        public static void InitType(Type type)
        {
            var attr = type.GetTypeInfo().GetCustomAttribute<GenericUsageAttribute>();
            if (attr == null || initFinished.Contains(type) || initOngoing.Contains(type))
                return;
            foreach (var prereq in attr.prerequisites)
                InitType(prereq);
            initOngoing.Add(type);
            ForceInit(type);
            LoadGenericMethods(type);
            initOngoing.Remove(type);
            initFinished.Add(type);
        }

        private static void ForceInit(Type type)
        {
            RuntimeHelpers.RunClassConstructor(type.TypeHandle);
        }

        private static void LoadGenericMethods(Type type)
        {
            var typeInfo = type.GetTypeInfo();
            foreach (var member in typeInfo.GetMembers())
            {
                var gmattr = member.GetCustomAttribute<GenericMethodAttribute>();
                if (gmattr == null)
                    continue;
                var name = gmattr.name;
                var gm = name == null ? GenericMethod.GetBound(member.Name) : GenericMethod.Get(name);
                var method = member.GetMemberType() == MemberTypes.Property
                    ? (member as PropertyInfo).GetGetMethod()
                    : (member as MethodInfo);
                var types = gmattr.types;
                Type[] paramTypes;
                var deleg = method.CreateDelegate(type, out paramTypes);
                if (types.IsEmpty())
                {
                    switch (gmattr.methodType)
                    {
                        case GenericMethodType.Method:
                            types = paramTypes;
                            break;
                        case GenericMethodType.Creator:
                            types = method.ReturnType.CreateArray(1);
                            break;
                        case GenericMethodType.Parser:
                            types = method.ReturnType.CreateArray(1);
                            break;
                        default:
                            break;
                    }
                }
                gm.Add(deleg, types);
            }
        }
    }
}