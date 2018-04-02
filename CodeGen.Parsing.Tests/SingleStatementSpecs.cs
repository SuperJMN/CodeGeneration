using CodeGen.Parsing.Ast;
using CodeGen.Parsing.Ast.Expressions;
using CodeGen.Parsing.Ast.Statements;
using CodeGen.Parsing.Tokenizer;
using Superpower;
using Xunit;

namespace CodeGen.Parsing.Tests
{
    public class SingleStatementSpecs : ParserSpecsBase<Statement>
    {
        [Fact]
        public void MethodCall()
        {
            AssertCode("func(a, b);", new MethodCall("func", new ReferenceExpression("a"), new ReferenceExpression("b")));
        }        

        protected override TokenListParser<LangToken, Statement> Parser => Parsers.SingleStatement;
    }
}