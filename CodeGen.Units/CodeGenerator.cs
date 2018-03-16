using System.Collections.Generic;
using CodeGen.Ast;
using CodeGen.Ast.Parsers;
using CodeGen.Intermediate;
using CodeGen.Intermediate.Codes;
using Superpower;

namespace CodeGen.Compiler
{
    public class CodeGenerator
    {
        public IEnumerable<IntermediateCode> Generate(string source)
        {
            var tokens = TokenizerFactory.Create().Tokenize(source);
            var parsed = Statements.IfStatement.Parse(tokens);

            var generator = new IntermediateCodeGenerator();
            var codes = generator.Generate(parsed);
            return codes;
        }
    }
}