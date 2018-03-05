﻿using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Superpower;
using Xunit;

namespace CodeGen.Ast.Tests
{
    public class TokenizerSpecs
    {
        [Theory]
        [MemberData(nameof(TokenData))]
        public void TokenizationTest(string code, IEnumerable<LangToken> tokens)
        {
            var sut = CreateSut();
            var actual = sut.Tokenize(code).Select(t => t.Kind);
            var expected = tokens;

            actual.Should().BeEquivalentTo(expected);
        }

        public static IEnumerable<object[]> TokenData()
        {
            return new List<object[]>()
            {
                new object[] {"==", new List<LangToken>() {LangToken.DoubleEqual},},
                new object[] {"=", new List<LangToken>() {LangToken.Equal},},
                new object[] {"ifSomething", new List<LangToken>() {LangToken.Identifier},},
                new object[]
                {
                    "if whileRunning == true", 
                    new List<LangToken>()
                    {
                        LangToken.If,
                        LangToken.Whitespace,
                        LangToken.Identifier,
                        LangToken.Whitespace,
                        LangToken.DoubleEqual,
                        LangToken.Whitespace,
                        LangToken.True,
                    },
                },
            };
        }

        private Tokenizer<LangToken> CreateSut()
        {
            return TokenizerFactory.Create();
        }
    }
}