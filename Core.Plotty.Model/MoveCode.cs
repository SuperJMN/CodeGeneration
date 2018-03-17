namespace CodeGen.Plotty.Model
{
    public class MoveCode : PlottyCodeBase
    {
        public MoveCode(Register register, int value)
        {
            Register = register;
            Value = value;
        }

        public Register Register { get; set; }
        public int Value { get; }
    }
}