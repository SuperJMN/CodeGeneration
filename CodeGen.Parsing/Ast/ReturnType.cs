namespace CodeGen.Parsing.Ast
{
    public abstract class ReturnType
    {
        public static ReturnType Int => new Primitive(PrimitiveType.Int);
        public static ReturnType Char => new Primitive(PrimitiveType.Char);
        public static ReturnType Void => new Primitive(PrimitiveType.Void);
        public static ReturnType IntPointer => new Pointer(Int);
        public static ReturnType CharPointer => new Pointer(Char);
        public static ReturnType VoidPointer => new Pointer(Void);

        public static ReturnType Create(PrimitiveType type, int indirectionLevel, bool isArray)
        {
            if (indirectionLevel == 0)
            {
                return new Primitive(type, isArray);
            }
            else
            {
                return new Pointer(Create(type, indirectionLevel - 1, isArray));
            }
        }
    }
}