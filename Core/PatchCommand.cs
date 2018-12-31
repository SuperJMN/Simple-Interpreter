namespace Core
{
    public class PatchCommand : Command
    {
        public string Code { get; }

        public PatchCommand(string code)
        {
            Code = code;
        }
    }
}