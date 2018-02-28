namespace CodeGen.Generation
{
    public class Reference
    {
        private readonly string identifier;
        private static int anonymousReferecenCount;

        public Reference()
        {
            identifier = "T" + anonymousReferecenCount++;
        }

        public Reference(string identifier)
        {
            this.identifier = identifier;
        }


        public override string ToString()
        {
            return identifier;
        }
    }
}