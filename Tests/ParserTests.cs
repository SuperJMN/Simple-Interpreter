using System.IO;
using Core;
using FluentAssertions;
using Xunit;

namespace Tests
{
    public class ParserTests
    {
        [Theory]
        [InlineData(@"PatchDefinition")]
        [InlineData(@"PatchDefinition 1234")]
        [InlineData("PatchDefinition \"1234\"")]
        [InlineData(@"PatchDefinition Name=""Hola""")]
        [InlineData("PatchDefinition Name=\"RootAccess-MainOS\" VersionFrom=\"EFIESP\\Windows\\System32\\Boot\\mobilestartup.efi\"\r\nPatchFile Path=\"Windows\\System32\\sspisrv.dll\"")]        
        [InlineData("Command1 \"Pepito\"\nCommand2")]
        [InlineData("FindFunctionCall R0=\"ADD R0, SP, #0x7C\" R1=\"MOV R1, R?\"")]
        [InlineData("            CreateLabel \"SeAccessCheckWithHint\"\r\n           FindFunctionCall R0 = \"ADD R0, SP, #0x7C\" R1 = \"MOV R1, R?\"\r\n           JumpToTarget")]
        public void Test(string input)
        {
            var p = new ScriptParser(Tokenizer.Create());
            p.Parse(input);
        }

        [Fact]
        public void AcceptanceTest()
        {
            var scriptParser = new ScriptParser(Tokenizer.Create());
            var parsed = scriptParser.Parse(File.ReadAllText("FullCode.txt"));
            var expected = File.ReadAllText("Expected.txt");
            parsed.ToString().Should().Be(expected);
        }       
    }
}