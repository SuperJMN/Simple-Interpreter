using Superpower;
using Superpower.Model;
using Superpower.Parsers;

namespace Core
{
    public static class Parsers
    {
        public static TokenListParser<LangToken, string> String => Token.EqualTo(LangToken.String).Select(x => x.ToStringValue());
        public static TokenListParser<LangToken, string> Identifier => Token.EqualTo(LangToken.Identifier).Select(x => x.ToStringValue());
        public static TokenListParser<LangToken, string> Number => Token.EqualTo(LangToken.Number).Select(x => x.ToStringValue());
        public static TokenListParser<LangToken, string> Hex => Token.EqualTo(LangToken.Hex).Select(x => x.ToStringValue());

        public static TokenListParser<LangToken, string> Value => String.Or(Number).Or(Hex).Or(Identifier);

        public static TokenListParser<LangToken, Argument> NamedArgument =>
            from name in Identifier
            from eq in Token.EqualTo(LangToken.Equal)
            from value in Value
            select (Argument)new NamedArgument(name, value);

        public static TokenListParser<LangToken, Argument> PositionalArgument =>
            from t in Value
            select (Argument)new PositionalArgument(t);

        public static TokenListParser<LangToken, Argument> Argument =>
            NamedArgument.Try().Or(PositionalArgument);

        public static TokenListParser<LangToken, Argument[]> Arguments =>
            from _ in Token.EqualTo(LangToken.Space)
            from t in Argument.ManyDelimitedBy(Token.EqualTo(LangToken.Space))
            select t;
        
        public static TokenListParser<LangToken, Command> PatchCommand =>
            from code in Token.EqualTo(LangToken.Code)
            select (Command) new PatchCommand(ExtractCode(code));

        private static string ExtractCode(Token<LangToken> code)
        {
            return code.ToStringValue()
                .Replace("PatchCode", "")
                .Replace("EndPatch", "")
                .Trim();
        }

        public static TokenListParser<LangToken, Command> RegularCommand =>
            from name in Identifier
            from args in Arguments.OptionalOrDefault()
            select (Command)new RegularCommand(name, args ?? new Argument[0]);

        public static TokenListParser<LangToken, Command> Command => RegularCommand.Try().Or(PatchCommand);

        public static TokenListParser<LangToken, string> Label => Identifier.Then(s => Token.EqualTo(LangToken.Colon).Select(x => s));

        public static TokenListParser<LangToken, Sentence> Sentence =>
            from cmd in LabelSentence.Try().Or(CommandSentence)
            select cmd;

        private static TokenListParser<LangToken, Sentence> LabelSentence => Label.Select(x => (Sentence)new LabelSentence(x));

        private static TokenListParser<LangToken, Sentence> CommandSentence => Command.Select(x => (Sentence)new CommandSentence(x));

        public static TokenListParser<LangToken, Script> Script =>
            from cmds in Sentence.ManyDelimitedBy(Token.EqualTo(LangToken.NewLine))
                .AtEnd()
            select new Script(cmds);
    }
}
