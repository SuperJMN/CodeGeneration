using System.Collections.Generic;
using CodeGen.Intermediate;
using Superpower;

namespace CodeGen.Ast
{
    public class CodeGenerator
    {
        public IReadOnlyCollection<IntermediateCode> Generate(string source)
        {
            var tokens = new Tokenizer().Tokenize(source);
            var parsed = Parsers.IfStatement.Parse(tokens);

            var generator = new IntermediateCodeGenerator();
            var codes = generator.Generate(parsed);
            return codes;
        }
    }
}