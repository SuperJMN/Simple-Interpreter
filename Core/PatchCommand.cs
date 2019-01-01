namespace Core
{
    public class PatchCommand : Command
    {
        public string Code { get; }

        public PatchCommand(string code)
        {
            Code = code;
        }

        public override string ToString()
        {
            var formattedCode = "\t" + Code.Replace("\n", "\n\t");

            return $"Patch\n{{\n{formattedCode}\n}}";
        }
    }
}