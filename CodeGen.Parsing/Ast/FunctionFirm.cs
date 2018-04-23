using System.Collections.Generic;

namespace CodeGen.Parsing.Ast
{
    public class FunctionFirm
    {
        public FunctionFirm(string name, VariableType returnType, ICollection<Argument> arguments)
        {
            Name = name;
            ReturnType = returnType;
            Arguments = arguments;
        }

        public string Name { get; }
        public VariableType ReturnType { get; }
        public ICollection<Argument> Arguments { get; }
    }
}