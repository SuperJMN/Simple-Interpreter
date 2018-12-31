using System.Collections.Generic;

namespace Core
{
    public class RegularCommand : Command
    {
        private readonly ICollection<Argument> arguments;

        public RegularCommand(string name, ICollection<Argument> arguments)
        {
            this.arguments = arguments;
        }
    }
}