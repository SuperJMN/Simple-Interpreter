namespace Core
{
    class LabelSentence : Sentence
    {
        public string Name { get; }

        public LabelSentence(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return $"Label \"{Name}\":";
        }
    }
}