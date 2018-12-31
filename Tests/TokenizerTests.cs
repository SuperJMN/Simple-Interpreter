using System;
using System.Linq;
using Core;
using FluentAssertions;
using Xunit;

namespace Tests
{
    public class TokenizerTests
    {


        [Theory]
        [InlineData(@"""Hola""", new []{ LangToken.DoubleQuote, LangToken.Word, LangToken.DoubleQuote})]
        [InlineData("Hola", new []{ LangToken.Word})]
        [InlineData("Hola\n\nPatchCode", new []{ LangToken.Word, LangToken.NewLine, LangToken.CodeStart})]       
        [InlineData(@"Command ""Parameter""", new[]
            {LangToken.Word, LangToken.Space, LangToken.DoubleQuote, LangToken.Word, LangToken.DoubleQuote})]
        public void Test(string input, LangToken[] expected)
        {
            var result = Tokenizer.Create().Tokenize(input);
            result.Select(x => x.Kind)
                .Should()
                .BeEquivalentTo(expected);
        }
    }
}
