namespace CodeGen.Parsing.Ast
{
    public class ArrayDeclarator
    {
        public int? Lenght { get; }

        public ArrayDeclarator(int? lenght)
        {
            Lenght = lenght;
        }
    }
}