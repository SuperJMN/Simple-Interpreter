using System.IO;
using Core;
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
        public void Test(string input)
        {
            var p = new ScriptParser(Tokenizer.Create());
            var ast = p.Parse(input);
        }

        [Fact]
        public void AcceptanceTest()
        {
            var p = new ScriptParser(Tokenizer.Create());
            var ast = p.Parse(File.ReadAllText("TextFile1.txt"));
        }       
    }
}