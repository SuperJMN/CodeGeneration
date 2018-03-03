using Superpower;

namespace CodeGen.Ast
{
    public static class ExtraCombinators 
    {
        /// <summary>
        /// Construct a parser that matches <paramref name="left"/>, discards the resulting value,
        /// then matches <paramref name="parser"/>, keeps the value, then matches <paramref name="right"/>
        /// and returns the value matched by <paramref name="parser"/>.
        /// </summary>
        /// <typeparam name="TKind">The kind of the tokens being parsed.</typeparam>
        /// <typeparam name="T">The type of value being parsed.</typeparam>
        /// <typeparam name="U">The type of the resulting value.</typeparam>
        /// <param name="parser">The parser.</param>
        /// <param name="left">First parser to match, value is ignored.</param>
        /// <param name="right">Last parser to match, value is ignored.</param>
        /// <returns>The resulting parser.</returns>
        public static TokenListParser<TKind, T> Between<TKind, T, U>(this TokenListParser<TKind, T> parser, TokenListParser<TKind, U> left, TokenListParser<TKind, U> right)
        {
            return left.IgnoreThen(parser.Then(right.Value));
        }

        /// <summary>
        /// Construct a parser that matches <paramref name="left"/>, discards the resulting value,
        /// then matches <paramref name="parser"/>, keeps the value, then matches <paramref name="right"/>
        /// and returns the value matched by <paramref name="parser"/>.
        /// </summary>
        /// <typeparam name="T">The type of value being parsed.</typeparam>
        /// <typeparam name="U">The type of the resulting value.</typeparam>
        /// <param name="parser">The parser.</param>
        /// <param name="left">First parser to match, value is ignored.</param>
        /// <param name="right">Last parser to match, value is ignored.</param>
        /// <returns>The resulting parser.</returns>
        public static TextParser<T> Between<T, U>(this TextParser<T> parser, TextParser<U> left, TextParser<U> right)
        {
            return left.IgnoreThen(parser.Then(right.Value));
        }

    }
}