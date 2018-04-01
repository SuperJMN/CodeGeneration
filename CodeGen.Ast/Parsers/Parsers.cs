using CodeGen.Ast.Units.Statements;
using CodeGen.Core;
using Superpower;
using Superpower.Parsers;

namespace CodeGen.Ast.Parsers
{
    public class Parsers
    {
        public static readonly TokenListParser<LangToken, Statement> RegularAssignment =
            from identifier in Basics.Identifier
            from eq in Token.EqualTo(LangToken.Equal)
            from expr in Expressions.Expr
            select (Statement)new AssignmentStatement(new Reference(identifier), expr);

        public static readonly TokenListParser<LangToken, Statement> OperatorAssignment =
            from identifier in Basics.Identifier
            from eq in Expressions.Increment.Or(Expressions.Decrement)
            select (Statement)new AssignmentOperatorStatement(eq, new Reference(identifier));

        public static readonly TokenListParser<LangToken, Statement> Assignment =
            from assignment in RegularAssignment.Try().Or(OperatorAssignment)
            from sc in Token.EqualTo(LangToken.Semicolon)
            select assignment;

        public static readonly TokenListParser<LangToken, Statement> Block =
            (from decls in Parse.Ref(() => Declaration).Many()
            from statements in Parse.Ref(() => Statement).Many()
            select (Statement)new Block(statements, decls)).Between(Token.EqualTo(LangToken.LeftBrace), Token.EqualTo(LangToken.RightBrace));

        private static readonly TokenListParser<LangToken, Statement>
            Else = from keyworkd in Token.EqualTo(LangToken.Else)
                   from statement in Statement
                   select statement;

        private static readonly TokenListParser<LangToken, Statement> IfStatement = from keywork in Token.EqualTo(LangToken.If)
                                                                                    from cond in Expressions.Condition
                                                                                    from statement in Statement
                                                                                    from elseStatement in Else.OptionalOrDefault()
                                                                                    select (Statement)new IfStatement(cond, statement, elseStatement);

        private static readonly TokenListParser<LangToken, Statement> DoStatement =
            from keywork in Token.EqualTo(LangToken.Do)
            from statement in Statement
            from keyword in Token.EqualTo(LangToken.While)
            from cond in Expressions.Condition
            from sc in Token.EqualTo(LangToken.Semicolon)
            select (Statement)new DoStatement(cond, statement);

        private static readonly TokenListParser<LangToken, Statement> WhileStatement =
            from keywork in Token.EqualTo(LangToken.While)
            from cond in Expressions.Condition
            from statement in Statement
            select (Statement)new WhileStatement(cond, statement);

        public static readonly TokenListParser<LangToken, Statement>
            ConditionalStatement =
                IfStatement.Or(WhileStatement).Or(DoStatement);

        public static readonly TokenListParser<LangToken, Statement>
            Loop =
                from keywork in Token.EqualTo(LangToken.For)
                from header in ForLoopHeader
                from statement in Statement
                select (Statement) new ForLoop(header, statement);

        private static readonly TokenListParser<LangToken, ForLoopHeader> ForLoopHeader = (
                from initialization in RegularAssignment
                from sc1 in Token.EqualTo(LangToken.Semicolon)
                from condition in Expressions.Expr
                from sc2 in Token.EqualTo(LangToken.Semicolon)
                from step in RegularAssignment
                select new ForLoopHeader(initialization, condition, step))
            .Between(Token.EqualTo(LangToken.LeftParenthesis), Token.EqualTo(LangToken.RightParenthesis));

        public static readonly TokenListParser<LangToken, Statement>
            SingleStatement = ConditionalStatement.Or(Assignment).Or(Loop);

        public static readonly TokenListParser<LangToken, Statement>
            Statement = Block.Or(SingleStatement);

        public static readonly TokenListParser<LangToken, VariableType> Int = Token.EqualTo(LangToken.Int).Value(VariableType.Int);
        public static readonly TokenListParser<LangToken, VariableType> Char = Token.EqualTo(LangToken.Char).Value(VariableType.Char);

        public static readonly TokenListParser<LangToken, VariableType> VarType = Int.Or(Char);

        public static readonly TokenListParser<LangToken, DeclarationStatement> Declaration =
            from type in VarType
            from r in Basics.Identifier
            from sm in Token.EqualTo(LangToken.Semicolon)
            select new DeclarationStatement(type, new Reference(r));

        public static readonly TokenListParser<LangToken, Statement[]>
            Statements = Statement.Many();

        public static readonly TokenListParser<LangToken, object>
            Params = from lp in Token.EqualTo(LangToken.LeftParenthesis)
                     from rp in Token.EqualTo(LangToken.RightParenthesis)
                     select new object();

        public static readonly TokenListParser<LangToken, Unit> Unit =
            from name in Basics.Identifier
            from p in Params
            from block in Block
            select new Unit(name, (Block)block);
    }
}