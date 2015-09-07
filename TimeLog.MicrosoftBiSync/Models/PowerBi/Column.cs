namespace TimeLog.MicrosoftBiSync.Models.PowerBi
{
    public enum ColumnDataType
    {
        @string,
        
        Int64,

        @bool,

        @DateTime
    }

    public class Column
    {
        public Column(string name, ColumnDataType dataType)
        {
            this.Name = name;
            this.DataType = dataType;
        }

        public string Name { get; private set; }

        public ColumnDataType DataType { get; private set; }
    }
}