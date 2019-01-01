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
        [InlineData("Something=\"Otra cosa\"")]
        [InlineData("FindFunctionCall 0x3912ad")]
        [InlineData("label:")]
        [InlineData("PatchCode\npatchitoEndPatch")]
        [InlineData("FindFunctionCall R0=\"ADD R0, SP, #0x7C\" R1=\"MOV R1, R?\"")]
        public void Instruction(string input)
        {
            var tokenList = Tokenizer.Create().Tokenize(input);
            var parsed = Parsers.Sentence.Parse(tokenList);
        }
    }
}