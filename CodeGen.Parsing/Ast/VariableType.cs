namespace CodeGen.Parsing.Ast
{
    public class VariableType
    {     
        public static readonly VariableType Int = new VariableType(PrimitiveType.Int);
        public static readonly VariableType Char = new VariableType(PrimitiveType.Char);
        public static readonly VariableType Void = new VariableType(PrimitiveType.Void);
        public static readonly VariableType IntPointer = GetPointer(PrimitiveType.Int);
        public static readonly VariableType CharPointer = GetPointer(PrimitiveType.Char);
        public static readonly VariableType VoidPointer = GetPointer(PrimitiveType.Void);

        public static VariableType GetPointer(PrimitiveType i)
        {
            return new VariableType(i)
            {
                IsPointer = true
            };
        }

        public static VariableType GetPrimitive(PrimitiveType i)
        {
            return new VariableType(i);
        }

        public bool IsPointer { get; private set; }

        private VariableType(PrimitiveType identifier)
        {
            Identifier = identifier;
        }

        public PrimitiveType Identifier { get; }

        public override string ToString()
        {
            return $"{Identifier}";
        }
    }
}