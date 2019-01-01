using System.Net.Sockets;
using System.Text.RegularExpressions;
using Superpower;
using Superpower.Model;
using Superpower.Parsers;
using Superpower.Tokenizers;

namespace Core
{
    public static class Tokenizer
    {
        public static Tokenizer<LangToken> Create()
        {
            return new TokenizerBuilder<LangToken>()
                .Match(Span.Regex(@"(?:\r\n|\n)+"), LangToken.NewLine)
                .Match(Span.Regex(@"\s+"), LangToken.Space)
                .Match(HexParser, LangToken.Hex)
                .Match(Numerics.Integer, LangToken.Number)
                .Match(QuotedTextParser, LangToken.String)
                .Match(CodeBlockParser, LangToken.Code)
                .Match(Character.EqualTo('='), LangToken.Equal)
                .Match(Character.EqualTo(':'), LangToken.Colon)
                .Match(Span.Regex(@"\w+[\d\w_]*"), LangToken.Identifier)
                .Build();
        }

        private static readonly TextParser<TextSpan> HexParser = 
            from hexHint in Span.EqualToIgnoreCase("0x")
            from hexValue in Numerics.HexDigits
            select hexValue;

        public static readonly TextParser<TextSpan> QuotedTextParser =
            from leading in Span.EqualToIgnoreCase("\"")
            from str in Span.Regex(@"(?:(?!\"").)*")
            from trailing in Span.EqualToIgnoreCase("\"")
            select str;

        public static readonly TextParser<TextSpan> CodeBlockParser = 
            from leading in Span.EqualToIgnoreCase("PatchCode")
            from code in Span.Regex(@"(?:(?!EndPatch)[\s\S])*")
            from trailing in Span.EqualToIgnoreCase("EndPatch")
            select code;
    }
}
