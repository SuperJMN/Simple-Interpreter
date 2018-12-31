namespace Core
{
    public abstract class Argument
    {
        public Argument(string value)
        {
            Value = value;
        }

        public string Value { get; }
    }
}