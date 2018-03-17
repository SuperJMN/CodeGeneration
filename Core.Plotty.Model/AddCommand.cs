﻿namespace CodeGen.Plotty.Model
{
    public class AddCommand : Command
    {
        public AddCommand(PlottyCore plottyCore) : base(plottyCore)
        {
        }

        public override void Execute()
        {
            var inst = (ArithmeticInstruction)PlottyCore.CurrentLine.Instruction;
            var origin = inst.Source.Id;
            var destination = inst.Destination.Id;

            int valueToAdd;
            if (inst.Addend is ImmediateSource im)
            {
                valueToAdd = im.Immediate;
            }
            else
            {
                valueToAdd = PlottyCore.Registers[((RegisterSource) inst.Addend).Register.Id];
            }
            
            PlottyCore.Registers[destination] = PlottyCore.Registers[origin] + valueToAdd;            

            PlottyCore.GoToNext();
        }
    }
}