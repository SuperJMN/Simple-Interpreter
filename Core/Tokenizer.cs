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
                .Match(StringParser, LangToken.String)
                .Match(CodeBlockParser, LangToken.Code)
                .Match(Character.EqualTo('"'), LangToken.DoubleQuote)
                .Match(Character.EqualTo('='), LangToken.Equal)
                .Match(Character.EqualTo(':'), LangToken.Colon)
                .Match(Span.Regex(@"\w+[\d\w_]*"), LangToken.Word)
                .Build();
        }

        public static readonly TextParser<TextSpan> StringParser =
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
