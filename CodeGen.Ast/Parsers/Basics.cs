using Superpower;
using Superpower.Parsers;

namespace CodeGen.Ast.Parsers
{
    public static class Basics
    {
        public static readonly TokenListParser<LangToken, string> Identifier = Token.EqualTo(LangToken.Identifier).Select(token => token.ToStringValue());
    }
}