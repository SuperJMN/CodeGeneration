namespace CodeGen.Generation
{
    public class Reference
    {
        private readonly string identifier;
        private static int anonymousRefereceCount;

        public Reference()
        {
            identifier = "T" + ++anonymousRefereceCount;
        }

        public Reference(string identifier)
        {
            this.identifier = identifier;
        }

        public string Identifier
        {
            get { return identifier; }
        }


        public override string ToString()
        {
            return Identifier;
        }
    }
}