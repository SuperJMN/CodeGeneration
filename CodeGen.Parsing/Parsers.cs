using System.Collections.Generic;
using System.Linq;
using CodeGen.Core;
using CodeGen.Parsing.Ast;
using CodeGen.Parsing.Ast.Expressions;
using CodeGen.Parsing.Ast.Statements;
using CodeGen.Parsing.Tokenizer;
using Superpower;
using Superpower.Parsers;

namespace CodeGen.Parsing
{
    public class Parsers
    {  
        private static readonly TokenListParser<LangToken, string> Identifier = Token.EqualTo(LangToken.Identifier).Select(token => token.ToStringValue());
        private static readonly TokenListParser<LangToken, string> Add = Token.EqualTo(LangToken.Plus).Value(Operator.Add);
        private static readonly TokenListParser<LangToken, string> Subtract = Token.EqualTo(LangToken.Minus).Value(Operator.Subtract);
        private static readonly TokenListParser<LangToken, string> Or = Token.EqualTo(LangToken.Or).Value(Operator.Or);
        private static readonly TokenListParser<LangToken, string> And = Token.EqualTo(LangToken.And).Value(Operator.And);
        private static readonly TokenListParser<LangToken, string> Multiply = Token.EqualTo(LangToken.Asterisk).Value(Operator.Multiply);
        private static readonly TokenListParser<LangToken, string> Divide = Token.EqualTo(LangToken.Slash).Value(Operator.Divide);
        private static readonly TokenListParser<LangToken, string> Modulo = Token.EqualTo(LangToken.Mod).Value(Operator.Module);
        private static readonly TokenListParser<LangToken, string> Lte = Token.EqualTo(LangToken.LessThanOrEqual).Value(Operator.Lte);
        private static readonly TokenListParser<LangToken, string> Lt = Token.EqualTo(LangToken.LessThan).Value(Operator.Lt);
        private static readonly TokenListParser<LangToken, string> Gt = Token.EqualTo(LangToken.GreaterThan).Value(Operator.Gt);
        private static readonly TokenListParser<LangToken, string> Gte = Token.EqualTo(LangToken.GreaterThanOrEqual).Value(Operator.Gte);
        private static readonly TokenListParser<LangToken, string> Eq = Token.EqualTo(LangToken.DoubleEqual).Value(Operator.Eq);
        private static readonly TokenListParser<LangToken, string> Neq = Token.EqualTo(LangToken.NotEqual).Value(Operator.Neq);
        private static readonly TokenListParser<LangToken, string> Power = Token.EqualTo(LangToken.Caret).Value(Operator.Power);
        private static readonly TokenListParser<LangToken, string> Not = Token.EqualTo(LangToken.Exclamation).Value(Operator.Not);
        private static readonly TokenListParser<LangToken, string> Negate = Token.EqualTo(LangToken.Minus).Value(Operator.Negate);
        private static readonly TokenListParser<LangToken, string> Increment = Token.EqualTo(LangToken.DoublePlus).Value(Operator.Increment);
        private static readonly TokenListParser<LangToken, string> Decrement = Token.EqualTo(LangToken.DoubleMinus).Value(Operator.Decrement);
        private static readonly TokenListParser<LangToken, string> PointerValue = Token.EqualTo(LangToken.Asterisk).Value(Operator.PointerValue);
        private static readonly TokenListParser<LangToken, string> PointerAddress = Token.EqualTo(LangToken.Ampersand).Value(Operator.PointerAddress);

        private static readonly TokenListParser<LangToken, Expression> Number = Token.EqualTo(LangToken.Number).Apply(Numerics.IntegerInt32)
            .Select(d => (Expression)new ConstantExpression(d));

        private static readonly TokenListParser<LangToken, Expression> Reference =
            Token.EqualTo(LangToken.Identifier)
                .Select(token => (Expression) new ReferenceExpression(token.ToStringValue()));

        private static readonly TokenListParser<LangToken, Expression> BooleanValue =
            Token.EqualTo(LangToken.True).Value((Expression) new ConstantExpression(true))
                .Or(Token.EqualTo(LangToken.False).Value((Expression) new ConstantExpression(false)))
            .Named("boolean");

        private static readonly TokenListParser<LangToken, Expression> Text = 
            Token.EqualTo(LangToken.Text)
                .Select(x => (Expression)new ConstantExpression(x));
        
        private static readonly TokenListParser<LangToken, Expression> Literal =
            Number
                .Or(Reference)
                .Or(Text)
                .Or(Token.EqualTo(LangToken.Null).Value((Expression) new ConstantExpression(null)))
                .Or(BooleanValue)
                .Named("literal");


        public static readonly TokenListParser<LangToken, Expression> FunctionCall =
            from name in Identifier
            from parameters in Parameters.BetweenParenthesis()
            select (Expression) new Call(name, parameters);

        private static readonly TokenListParser<LangToken, Expression> Item = FunctionCall.Try().Or(Literal);

        private static readonly TokenListParser<LangToken, Expression> Factor =
            Parse.Ref(() => Expression).BetweenParenthesis()
                .Or(Item);

        private static readonly TokenListParser<LangToken, Expression> Operand =
            (from op in Negate.Or(Not).Or(Increment).Or(Decrement).Or(PointerValue).Or(PointerAddress)
                from factor in Factor
                select MakeUnary(op, factor)).Or(Factor).Named("expression");

        private static readonly TokenListParser<LangToken, Expression> InnerTerm = Parse.Chain(Power, Operand, MakeBinary);

        private static readonly TokenListParser<LangToken, Expression> Term = Parse.Chain(Multiply.Or(Divide).Or(Modulo), InnerTerm, MakeBinary);

        private static readonly TokenListParser<LangToken, Expression> Comparand = Parse.Chain(Add.Or(Subtract), Term, MakeBinary);

        private static readonly TokenListParser<LangToken, Expression> Comparison = Parse.Chain(Lte.Or(Neq).Or(Lt).Or(Gte.Or(Gt)).Or(Eq), Comparand, MakeBinary);

        private static readonly TokenListParser<LangToken, Expression> Conjunction = Parse.Chain(And, Comparison, MakeBinary);

        private static readonly TokenListParser<LangToken, Expression> Disjunction = Parse.Chain(Or, Conjunction, MakeBinary);


        
        public static readonly TokenListParser<LangToken, Expression> Expression = Disjunction;

        private static readonly TokenListParser<LangToken, Expression> Condition = Expression.BetweenParenthesis();

        private static Expression MakeBinary(string operatorName, Expression leftOperand, Expression rightOperand)
        {
            return new ExpressionNode(operatorName, leftOperand, rightOperand);
        }

        private static Expression MakeUnary(string operatorName, Expression factor)
        {
            return new ExpressionNode(operatorName, factor);
        }
        
        private static readonly TokenListParser<LangToken, Statement> RegularAssignment =
            from r in Identifier
            from eq in Token.EqualTo(LangToken.Equal)
            from expr in Expression
            select (Statement)new AssignmentStatement(new Reference(r), expr);

        private static readonly TokenListParser<LangToken, Statement> OperatorAssignment =
            from identifier in Identifier
            from eq in Increment.Or(Decrement)
            select (Statement)new AssignmentOperatorStatement(eq, new Reference(identifier));

        public static readonly TokenListParser<LangToken, Statement> Assignment =
            from assignment in RegularAssignment.Try().Or(OperatorAssignment)
            select assignment;

        public static readonly TokenListParser<LangToken, Statement> AssignmentStatement =
            from assignment in Assignment
            from sc in Token.EqualTo(LangToken.Semicolon)
            select assignment;
        
        private static readonly TokenListParser<LangToken, Statement>
            Else = from keyworkd in Token.EqualTo(LangToken.Else)
                   from statement in Statement
                   select statement;

        private static readonly TokenListParser<LangToken, Statement> IfStatement = from keywork in Token.EqualTo(LangToken.If)
                                                                                    from cond in Condition
                                                                                    from statement in Statement
                                                                                    from elseStatement in Else.OptionalOrDefault()
                                                                                    select (Statement)new IfStatement(cond, statement, elseStatement);

        private static readonly TokenListParser<LangToken, Statement> DoStatement =
            from keywork in Token.EqualTo(LangToken.Do)
            from statement in Statement
            from keyword in Token.EqualTo(LangToken.While)
            from cond in Condition
            from sc in Token.EqualTo(LangToken.Semicolon)
            select (Statement)new DoStatement(cond, statement);

        private static readonly TokenListParser<LangToken, Statement> WhileStatement =
            from keywork in Token.EqualTo(LangToken.While)
            from cond in Condition
            from statement in Statement
            select (Statement)new WhileStatement(cond, statement);

        public static readonly TokenListParser<LangToken, Statement>
            ConditionalStatement =
                IfStatement.Or(WhileStatement).Or(DoStatement);

        private static readonly TokenListParser<LangToken, Statement>
            ForLoop =
                from keywork in Token.EqualTo(LangToken.For)
                from header in ForLoopHeader
                from statement in Statement
                select (Statement)new ForLoop(header, statement);

        private static readonly TokenListParser<LangToken, ForLoopHeader> ForLoopHeader = (
                from initialization in RegularAssignment
                from sc1 in Token.EqualTo(LangToken.Semicolon)
                from condition in Expression
                from sc2 in Token.EqualTo(LangToken.Semicolon)
                from step in RegularAssignment
                select new ForLoopHeader(initialization, condition, step))
            .BetweenParenthesis();

        private static readonly TokenListParser<LangToken, Expression[]> Parameters =
            Expression.CommaDelimited();

        public static readonly TokenListParser<LangToken, Statement> Return =
            from keyw in Token.EqualTo(LangToken.Return)
            from expr in Expression.OptionalOrDefault()
            from sm in Token.EqualTo(LangToken.Semicolon)
            select (Statement) new ReturnStatement(expr);
            

        public static readonly TokenListParser<LangToken, Statement> FunctionStatement =
            from call in FunctionCall
            from sc in Token.EqualTo(LangToken.Semicolon)
            select (Statement) call;

        public static readonly TokenListParser<LangToken, Statement>
            SingleStatement = FunctionStatement.Try().Or(ConditionalStatement).Or(AssignmentStatement).Or(ForLoop).Or(Return);

        private static readonly TokenListParser<LangToken, PrimitiveType> Int = Token.EqualTo(LangToken.Int).Value(PrimitiveType.Int);
        private static readonly TokenListParser<LangToken, PrimitiveType> Char = Token.EqualTo(LangToken.Char).Value(PrimitiveType.Char);
        private static readonly TokenListParser<LangToken, PrimitiveType> Void = Token.EqualTo(LangToken.Void).Value(PrimitiveType.Void);

        public static readonly TokenListParser<LangToken, PrimitiveType> BaseType = Int.Or(Char).Or(Void);

        public static readonly TokenListParser<LangToken, ArrayDeclarator> ArrayDeclarator =
            from n in Token.EqualTo(LangToken.Number).Apply(Numerics.IntegerInt32).Optional().BetweenBrackets()
            select new ArrayDeclarator(n);

        private static readonly TokenListParser<LangToken, int> Integer  =
            Token.EqualTo(LangToken.Number).Apply(Numerics.IntegerInt32);

        private static readonly TokenListParser<LangToken, int[]> ListOfInts  =
            Integer.CommaDelimited();

        private static readonly TokenListParser<LangToken, InitializationExpression> ListInit =
        from ints in ListOfInts.BetweenBraces()
        select (InitializationExpression)new ListInitialization(ints);

        private static readonly TokenListParser<LangToken, InitializationExpression> DirectInit =
            from expr in Expression
            select (InitializationExpression)new DirectInitialization(expr);

        public static readonly TokenListParser<LangToken, InitializationExpression> InitializationExpression = DirectInit.Or(ListInit);

        public static readonly TokenListParser<LangToken, InitializationExpression> Initializer =
            from eq in Token.EqualTo(LangToken.Equal)
            from initExpr in InitializationExpression
            select initExpr;
                       
        public static readonly TokenListParser<LangToken, Declarator> Declarator =
            from asterisks in Token.EqualTo(LangToken.Asterisk).Many()
            from identifier in Identifier
            from array in ArrayDeclarator.OptionalOrDefault()
            select new Declarator(identifier, array, asterisks.Length);

        public static readonly TokenListParser<LangToken, DeclaratorAndInitializer> DeclaratorAndInitializer =
            from dec in Declarator
            from init in Initializer.OptionalOrDefault()
            select new DeclaratorAndInitializer(dec, init);
        
        private static readonly TokenListParser<LangToken, DeclaratorAndInitializer[]> DeclaratorsAndInitializers =
            DeclaratorAndInitializer.CommaDelimited();
        
        public static readonly TokenListParser<LangToken, IEnumerable<DeclarationStatement>> DeclarationStatement =
            from type in BaseType
            from declsAndInits in DeclaratorsAndInitializers
            from sm in Token.EqualTo(LangToken.Semicolon)
            select declsAndInits.Select(x => new DeclarationStatement(GetRefType(type, x.Declarator.IndirectionLevel), x.Declarator.Identifier, x.Initialization, x.Declarator?.Array?.Lenght));

        public static readonly TokenListParser<LangToken, IEnumerable<DeclarationStatement>[]> Declarations =
            DeclarationStatement.Many();

        private static ReferenceType GetRefType(PrimitiveType type, int indirectionLevel)
        {
            if (indirectionLevel == 0)
            {
                return new Primitive(type);
            }

            return new Pointer(GetRefType(type, indirectionLevel - 1));
        }

        public static readonly TokenListParser<LangToken, Statement> Block =
            (from decls in Parse.Ref(() => Declarations)
             from statements in Parse.Ref(() => Statements)
             select (Statement)new Block(statements, decls.SelectMany(x => x).ToList())).BetweenBraces();
        
        private static readonly TokenListParser<LangToken, Statement>
            Statement = Block.Or(SingleStatement);

        public static readonly TokenListParser<LangToken, Statement[]>
            Statements = Statement.Many();

        public static readonly TokenListParser<LangToken, ReferenceType> ReferenceType = 
            from b in BaseType
            from asterisks in Token.EqualTo(LangToken.Asterisk).Many()
            select GetRefType(b, asterisks.Length);

        private static readonly TokenListParser<LangToken, Argument> Argument =
            from t in ReferenceType
            from name in Identifier
            select new Argument(t, name);

        private static readonly TokenListParser<LangToken, Argument[]>
            Arguments = Argument.CommaDelimited();
        
        private static readonly TokenListParser<LangToken, ReferenceType> ReturnType = ReferenceType;

        public static readonly TokenListParser<LangToken, Function> Function =
            from returnType in ReturnType
            from name in Identifier
            from args in Arguments.BetweenParenthesis()
            from block in Block
            select new Function(new FunctionFirm(name, returnType, args), (Block)block);

        public static readonly TokenListParser<LangToken, Program> Program =
            from funcs in Function.Many()
            select new Program(funcs);
    }
}