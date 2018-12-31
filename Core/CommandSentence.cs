namespace Core
{
    class CommandSentence : Sentence
    {
        public Command Command { get; }

        public CommandSentence(Command command)
        {
            Command = command;
        }
    }
}