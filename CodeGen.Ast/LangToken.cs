using Superpower.Display;

namespace CodeGen.Ast
{
    public enum LangToken
    {
        [Token(Category = "keyword", Example = "123")]
        Number,
        [Token(Category = "separator", Example = ",")]
        Comma,
        [Token(Category = "myIdentifier", Example = "abc")]
        Identifier,
        [Token(Category = "text", Example = "abc")]
        Colon,
        [Token(Category = "operator", Example = "+")]
        Plus,
        [Token(Category = "operator", Example = "*")]
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
        False,
        Or,
        And,
        Mod,
        LessThanOrEqual,
        LessThan,
        GreaterThan,
        GreaterThanOrEqual,
        NotEqual,
        Caret,
        Not,
        Null,
        Else,
        [Token(Category = "separator", Example = "\"")]
        Quote,
        Text,
        [Token(Category = "operator", Example = "++")]
        DoublePlus,
        [Token(Category = "operator", Example = "--")]
        DoubleMinus,
        Int,
        Char,
    }
}