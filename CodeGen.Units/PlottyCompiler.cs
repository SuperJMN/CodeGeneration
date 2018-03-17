using System.Collections.Generic;
using System.Linq;
using CodeGen.Ast;
using CodeGen.Ast.Parsers;
using CodeGen.Intermediate;
using CodeGen.Plotty;
using Plotty.Model;
using Superpower;

namespace CodeGen.Compiler
{
    public class PlottyCompiler
    {
        public IEnumerable<Line> Compile(string source)
        {
            var tokens = TokenizerFactory.Create().Tokenize(source);
            var parsed = Statements.ProgramParser.Parse(tokens);

            var generator = new IntermediateCodeGenerator();
            var codes = generator.Generate(parsed);
            var plottyGenerator = new PlottyCodeGenerator();
            
            return plottyGenerator.Generate(codes.ToList());
        }      
    } 
}