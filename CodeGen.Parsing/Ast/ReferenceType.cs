namespace CodeGen.Parsing.Ast
{
    public abstract class ReferenceType
    {
        public static ReferenceType Int => new Primitive(PrimitiveType.Int);
        public static ReferenceType Char => new Primitive(PrimitiveType.Char);
        public static ReferenceType Void => new Primitive(PrimitiveType.Void);
        public static ReferenceType IntPointer => new Pointer(Int);
        public static ReferenceType CharPointer => new Pointer(Char);
        public static ReferenceType VoidPointer => new Pointer(Void);        
    }
}