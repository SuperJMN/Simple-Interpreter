namespace Core
{
    public class NamedArgument : Argument
    {
        public string Name { get; }

        public NamedArgument(string name, string value) : base(value)
        {
            Name = name;
        }

        public override string ToString()
        {
            return $"{Name}={Value}";
        }
    }
}