﻿namespace CodeGen.Plotty.Model
{
    public class MoveInstruction : Instruction
    {
        public Register Destination { get; set; }
        public Source Source { get; set; }

        public override string ToString()
        {
            return $"Move {Source} into {Destination}";
        }
    }
}