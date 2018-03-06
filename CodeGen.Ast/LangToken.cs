using Superpower.Display;

namespace CodeGen.Ast
{
    public enum LangToken
    {
        [Token(Category = "keyword", Example = "123")]
        Number,
        [Token(Category = "separator", Example = ",")]
        Comma,
        [Token(Category = "text", Example = "abc")]
        Identifier,
        [Token(Category = "text", Example = ":")]
        Colon,
        [Token(Category = "operator", Example = "+")]
        Plus,
        Asterisk,
        Semicolon,
        LeftBrace,
        RightBrace,
        LeftParenthesis,
        RightParenthesis,
        If,
        Equal,
        While,
        For,
        Do,
        Slash,
        Minus,
        DoubleEqual,
        True,
        False
    }
}