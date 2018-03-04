namespace CodeGen.Units
{
    public class Reference
    {
        public Reference()
        {
        }

        public Reference(string identifier)
        {
            Identifier = identifier;
        }

        public string Identifier { get; set; }

        public override string ToString()
        {
            return Identifier;
        }
    }
}