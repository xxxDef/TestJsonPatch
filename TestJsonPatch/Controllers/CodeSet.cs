namespace TestJsonPatch.Controllers
{
    public class CodeSet
    {
        public string? Name { get; set; }
        public string? Id { get; set; }

        public Dictionary<string, Code> Codes { get; set; } = [];
    }
}
