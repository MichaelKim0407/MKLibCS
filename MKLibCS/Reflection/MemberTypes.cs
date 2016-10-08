namespace MKLibCS.Reflection
{
    /// <summary>
    /// Marks each type of member that is defined as a derived class of MemberInfo.
    /// </summary>
    public enum MemberTypes
    { 
        /// <summary>
        /// Specifies that the member is a constructor, representing a System.Reflection.ConstructorInfo member. Hexadecimal value of 0x01.
        /// </summary>
        Constructor = 1,
        /// <summary>
        /// Specifies that the member is an event, representing an System.Reflection.EventInfo member. Hexadecimal value of 0x02.
        /// </summary>
        Event = 2,   
        /// <summary>
        /// Specifies that the member is a field, representing a System.Reflection.FieldInfo member. Hexadecimal value of 0x04.
        /// </summary>
        Field = 4,
        /// <summary>
        /// Specifies that the member is a method, representing a System.Reflection.MethodInfo member. Hexadecimal value of 0x08.
        /// </summary>
        Method = 8,
        /// <summary>
        /// Specifies that the member is a property, representing a System.Reflection.PropertyInfo member. Hexadecimal value of 0x10.
        /// </summary>
        Property = 16,
        /// <summary>
        /// Specifies that the member is a type, representing a System.Reflection.MemberTypes.TypeInfo member. Hexadecimal value of 0x20.
        /// </summary>
        TypeInfo = 32,
        /// <summary>
        /// Specifies that the member is a custom member type. Hexadecimal value of 0x40.
        /// </summary>
        Custom = 64,
        /// <summary>
        /// Specifies that the member is a nested type, extending System.Reflection.MemberInfo.
        /// </summary>
        NestedType = 128,
        /// <summary>
        /// Specifies all member types.
        /// </summary>
        All = 191
    }
}
