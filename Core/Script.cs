using System.Collections.Generic;

namespace Core
{
    public class Script
    {
        public IList<Sentence> Sentences { get; }

        public Script(IList<Sentence> sentences)
        {
            Sentences = sentences;
        }

        public override string ToString()
        {
            return string.Join("\n", Sentences);
        }
    }
}