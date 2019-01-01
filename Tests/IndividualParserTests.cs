using Core;
using FluentAssertions;
using Superpower;
using Xunit;

namespace Tests
{
    public class IndividualParserTests
    {
        [Theory]
        [InlineData("FindFunctionCall Something=\"Otra cosa\"")]
        [InlineData("FindFunctionCall 0x3912ad")]
        public void PathCommand(string input)
        {
            var tokenList = Tokenizer.Create().Tokenize(input);
            var parsed = Parsers.Command.Parse(tokenList);
        }  
        
        [Theory]
        [InlineData("Something", "Something()")]
        [InlineData("FindFunctionCall 0x3912ad", "FindFunctionCall(0x3912ad)")]
        [InlineData("label:", "Label \"label\":")]
        [InlineData("PatchCode\npatchitoEndPatch", "Patch\n{\n\tpatchito\n}")]
        [InlineData("FindFunctionCall R0=\"ADD R0, SP, #0x7C\" R1=\"MOV R1, R?\"", @"FindFunctionCall(R0=""ADD R0, SP, #0x7C"",R1=""MOV R1, R?"")")]
        public void Sentence(string input, string expected)
        {
            var tokenList = Tokenizer.Create().Tokenize(input);
            var sentence = Parsers.Sentence.Parse(tokenList);
            sentence.ToString().Should().Be(expected);
        }
    }
}