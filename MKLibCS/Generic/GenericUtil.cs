using System;
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
    /// 
    /// </summary>
    static public partial class GenericUtil
    {
        static GenericUtil()
        {
            InitCreate();
            InitParseFormat();
        }

        static private List<Type> initFinished = new List<Type>();
        static private List<Type> initOngoing = new List<Type>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        static public void InitType(Type type)
        {
            var attr = type.GetTypeInfo().GetCustomAttribute<GenericUsageAttribute>();
            if (attr != null && !initFinished.Contains(type) && !initOngoing.Contains(type))
            {
                foreach (var prereq in attr.prerequisites)
                    InitType(prereq);
                initOngoing.Add(type);
                ForceInit(type);
                LoadGenericMethods(type);
                initOngoing.Remove(type);
                initFinished.Add(type);
            }
        }

        static private void ForceInit(Type type)
        {
            RuntimeHelpers.RunClassConstructor(type.TypeHandle);
        }

        static private void LoadGenericMethods(Type type)
        {
            var typeInfo = type.GetTypeInfo();
            foreach (var member in typeInfo.GetMembers())
            {
                var gmattr = member.GetCustomAttribute<GenericMethodAttribute>();
                if (gmattr == null)
                    continue;
                var name = gmattr.name;
                var gm = name == null ? GenericMethod.GetBound(member.Name) : GenericMethod.Get(name);
                var method = member.GetMemberType() == MemberTypes.Property ?
                    (member as PropertyInfo).GetMethod :
                    (member as MethodInfo);
                var paramTypes = method.GetParameters().ConvertAll(p => p.ParameterType).ToList();
                if (!method.IsStatic)
                    paramTypes.Insert(0, type);
                var types = gmattr.types;
                if (types.IsEmpty())
                {
                    switch(gmattr.methodType)
                    {
                        case GenericMethodType.Method:
                            types = paramTypes.ToArray();
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
                if (!method.IsStatic && typeInfo.IsValueType)
                    paramTypes[0] = paramTypes[0].MakeByRefType();
                paramTypes.Add(method.ReturnType);
                var delegType = Expression.GetDelegateType(paramTypes.ToArray());
                var deleg = method.CreateDelegate(delegType);
                gm.Add(deleg, types);
            }
        }
    }
}
