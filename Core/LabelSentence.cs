namespace Core
{
    class LabelSentence : Sentence
    {
        public string Name { get; }

        public LabelSentence(string name)
        {
            Name = name;
        }
    }
}