using System.Collections.Generic;

namespace Core
{
    public class RegularCommand : Command
    {
        public string Name { get; }
        public ICollection<Argument> Arguments { get; }

        public RegularCommand(string name, ICollection<Argument> arguments)
        {
            Name = name;
            Arguments = arguments;
        }

        public override string ToString()
        {
            return $"{Name}({string.Join(",", Arguments)})";
        }
    }
}