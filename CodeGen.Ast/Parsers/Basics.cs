using Superpower;
using Superpower.Parsers;

namespace CodeGen.Ast.NewParsers
{
    public static class Basics
    {
        public static readonly TokenListParser<LangToken, string> Identifier = Token.EqualTo(LangToken.Identifier).Select(token => token.ToStringValue());
    }
}