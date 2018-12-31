using System.Linq;
using System.Net.Sockets;
using System.Runtime.InteropServices.ComTypes;
using Superpower;
using Superpower.Parsers;

namespace Core
{
    public static class Parsers
    {
        public static TokenListParser<LangToken, string> Text => Token.EqualTo(LangToken.Word).Select(x => x.ToStringValue());
        public static TokenListParser<LangToken, string> QuotedString => Token.EqualTo(LangToken.String).Select(x => x.ToStringValue());

        public static TokenListParser<LangToken, string> Whitespace => Token.EqualTo(LangToken.Space)
            .Or(Token.EqualTo(LangToken.NewLine)).Select(x => x.ToStringValue());

        public static TokenListParser<LangToken, string> Padding => Whitespace.Many().Select(string.Concat);

        public static TokenListParser<LangToken, string> Value => QuotedString.Or(Text);

        public static TokenListParser<LangToken, Argument> NamedArgument =>
            from name in Text
            from eq in Token.EqualTo(LangToken.Equal)
            from value in Value
            select (Argument)new NamedArgument(name, value);

        public static TokenListParser<LangToken, Argument> PositionalArgument =>
            from t in Value
            select (Argument)new PositionalArgument(t);

        public static TokenListParser<LangToken, Argument> Argument =>
            NamedArgument.Try().Or(PositionalArgument);

        public static TokenListParser<LangToken, Argument[]> Arguments =>
            from _ in Token.EqualTo(LangToken.Space).Optional()
            from t in Argument.ManyDelimitedBy(Token.EqualTo(LangToken.Space))
            from __ in Token.EqualTo(LangToken.Space).Optional()
            select t;
        
        public static TokenListParser<LangToken, Command> PatchCommand =>
            from code in Token.EqualTo(LangToken.Code)
            select (Command) new PatchCommand(code.ToStringValue());
            
        public static TokenListParser<LangToken, Command> RegularCommand =>
            from name in Text
            from args in Arguments
            select (Command)new RegularCommand(name, args ?? new Argument[0]);

        public static TokenListParser<LangToken, Command> Command => RegularCommand.Try().Or(PatchCommand);

        public static TokenListParser<LangToken, string> Label => Text.Then(s => Token.EqualTo(LangToken.Colon).Select(x => s));

        public static TokenListParser<LangToken, Sentence> Instruction =>
            from cmd in LabelSentence.Try().Or(CommandSentence)
            select cmd;

        private static TokenListParser<LangToken, Sentence> LabelSentence => Label.Select(x => (Sentence)new LabelSentence(x));

        private static TokenListParser<LangToken, Sentence> CommandSentence => Command.Select(x => (Sentence)new CommandSentence(x));

        public static TokenListParser<LangToken, Script> Script =>
            from cmds in Instruction.ManyDelimitedBy(Token.EqualTo(LangToken.NewLine))
            select new Script(cmds);
    }
}
