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
        [InlineData(@"123", new []{ LangToken.Number})]
        [InlineData(@"0xA000000", new []{ LangToken.Hex})]
        [InlineData(@"""Hola""", new []{ LangToken.String })]
        [InlineData("Hola", new []{ LangToken.Identifier})]
        [InlineData("Hola\n\nPatchCodeHOLAEndPatch", new []{ LangToken.Identifier, LangToken.NewLine, LangToken.Code})]       
        [InlineData(@"Command ""Parameter""", new[]
            {LangToken.Identifier, LangToken.Space, LangToken.String, })]
        public void Test(string input, LangToken[] expected)
        {
            var result = Tokenizer.Create().Tokenize(input);
            result.Select(x => x.Kind)
                .Should()
                .BeEquivalentTo(expected);
        }
    }
}