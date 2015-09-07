namespace TimeLog.MicrosoftBiSync.Models.PowerBi
{
    using System.Xml;

    public class ColumnData
    {
        public ColumnData(XmlNode node)
        {
            this.Name = node.Name.Replace("tlp:", string.Empty);
            this.Value = node.InnerText;
            this.Type = ColumnDataType.@string;
        }

        public ColumnData(string name, string value, ColumnDataType type)
        {
            this.Name = name;
            this.Value = value;
            this.Type = type;
        }

        public string Name { get; private set; }

        public string Value { get; private set; }

        public ColumnDataType Type { get; private set; }
    }
}