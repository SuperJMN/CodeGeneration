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
    }
}