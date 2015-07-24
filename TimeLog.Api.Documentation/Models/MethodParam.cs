namespace TimeLog.Api.Documentation.Models
{
    public class MethodParam
    {
        public MethodParam(string name, string value)
        {
            this.Name = name;
            this.Value = value;
        }

        public string Name { get; set; }

        public string Value { get; set; }
    }
}