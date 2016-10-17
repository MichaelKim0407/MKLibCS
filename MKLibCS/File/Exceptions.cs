﻿using System;

namespace MKLibCS.File
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class TypeNotSupportedException : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reading"></param>
        /// <param name="type"></param>
        public TypeNotSupportedException(bool reading, Type type)
        {
            this.reading = reading;
            this.type = type;
        }

        /// <summary>
        /// 
        /// </summary>
        public readonly bool reading;

        /// <summary>
        /// 
        /// </summary>
        public readonly Type type;

        /// <summary>
        /// 
        /// </summary>
        public override string Message
        {
            get
            {
                return (reading ? "Reading" : "Writing") + " of type " + type.FullName + " variable is not supported!";
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public sealed class NullObjectException : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writing"></param>
        /// <param name="name"></param>
        /// <param name="parent"></param>
        public NullObjectException(bool writing, string name, ParentSLMethod parent = ParentSLMethod.NONE)
        {
            this.writing = writing;
            this.name = name;
            this.parent = parent;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="writing"></param>
        /// <param name="name"></param>
        /// <param name="parent"></param>
        public NullObjectException(bool writing, string name, object parent)
        {
            this.writing = writing;
            this.name = name;
            if (parent.IsSLCustomSingleObj())
                this.parent = ParentSLMethod.SLCustomSingle;
            else if (parent.IsSLCustomStructObj())
                this.parent = ParentSLMethod.SLCustomStruct;
            else if (parent.IsSLCustomComplexObj())
                this.parent = ParentSLMethod.SLCustomClassComplex;
            else
                this.parent = ParentSLMethod.NONE;
        }

        /// <summary>
        /// 
        /// </summary>
        public readonly bool writing;

        /// <summary>
        /// 
        /// </summary>
        public readonly string name;

        /// <summary>
        /// 
        /// </summary>
        public enum ParentSLMethod
        {
            /// <summary>
            /// 
            /// </summary>
            NONE,

            /// <summary>
            /// 
            /// </summary>
            SLCustomSingle,

            /// <summary>
            /// 
            /// </summary>
            SLCustomStruct,

            /// <summary>
            /// 
            /// </summary>
            SLCustomClassComplex
        }

        /// <summary>
        /// 
        /// </summary>
        public readonly ParentSLMethod parent;

        /// <summary>
        /// 
        /// </summary>
        public override string Message
        {
            get
            {
                return "Failed when " + (writing ? "writing" : "reading")
                       + " field \"" + name + "\": reference object is null.";
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public sealed class CorruptFileException : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nLine"></param>
        public CorruptFileException(int nLine)
        {
            this.nLine = nLine;
        }

        /// <summary>
        /// 
        /// </summary>
        public readonly int nLine;

        /// <summary>
        /// 
        /// </summary>
        public override string Message
        {
            get { return "File is corrupt at line " + nLine + "."; }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public sealed class ParsingFailureException : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="memberInfo"></param>
        /// <param name="reason"></param>
        public ParsingFailureException(ExceptionInfo memberInfo, string reason)
        {
            this.memberInfo = memberInfo;
            this.reason = reason;
        }

        /// <summary>
        /// 
        /// </summary>
        public readonly ExceptionInfo memberInfo;

        /// <summary>
        /// 
        /// </summary>
        public readonly string reason;

        /// <summary>
        /// 
        /// </summary>
        public const string Reason_0 = "No member with attribute FileSLItem found in an SLCustomSingle object.";

        /// <summary>
        /// 
        /// </summary>
        public const string Reason_1 = "No item or node with corresponding name found.";

        /// <summary>
        /// 
        /// </summary>
        public override string Message
        {
            get
            {
                return "Failed to read the member \"" + memberInfo.Name
                       + "\" (MemberType: " + memberInfo.MemberType + ", Type: " + memberInfo.ValueType + ")"
                       + " in class \"" + memberInfo.DeclaringType + "\" due to reason: " + reason;
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public sealed class WritingFailureException : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="memberInfo"></param>
        /// <param name="reason"></param>
        public WritingFailureException(ExceptionInfo memberInfo, string reason)
        {
            this.memberInfo = memberInfo;
            this.reason = reason;
        }

        /// <summary>
        /// 
        /// </summary>
        public readonly ExceptionInfo memberInfo;

        /// <summary>
        /// 
        /// </summary>
        public readonly string reason;

        /// <summary>
        /// 
        /// </summary>
        public const string Reason_0 = "No member with attribute FileSLItem found in an SLCustomSingle object.";

        /// <summary>
        /// 
        /// </summary>
        public const string Reason_1 = "It is not possible to save a single value to a node.";

        /// <summary>
        /// 
        /// </summary>
        public override string Message
        {
            get
            {
                return "Failed to write the member \"" + memberInfo.Name
                       + "\" (MemberType: " + memberInfo.MemberType + ", Type: " + memberInfo.ValueType + ")"
                       + " in class \"" + memberInfo.DeclaringType + "\" due to reason: " + reason;
            }
        }
    }
}