namespace TimeLog.ApiConsoleApp
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Text;

    /// <summary>
    /// Reads from a CSV file
    /// </summary>
    /// <remarks>
    /// Usage:
    /// var csv = new CsvReader(@"C:\test.csv");
    /// while (csv.Read())
    /// {
    ///     Logger.Debug(csv.LineIndex + " - " + csv.GetString("projectName"));
    /// }
    /// </remarks>
    public class CsvReader
    {
        /// <summary>
        /// Initializes a new instance of the CsvReader class with all settings for default.
        /// </summary>
        /// <remarks>Default: Headers, European file encoding (1252) and comma as splitter</remarks>
        /// <param name="csvFilePath">The full path of the CSV file to read</param>
        public CsvReader(string csvFilePath)
            : this(csvFilePath, true)
        {
        }

        /// <summary>
        /// Initializes a new instance of the CsvReader class with optional headers
        /// </summary>
        /// <param name="csvFilePath">The full path of the CSV file to read</param>
        /// <param name="hasHeaders">Indication if the file has headers</param>
        public CsvReader(string csvFilePath, bool hasHeaders)
            : this(csvFilePath, hasHeaders, Encoding.GetEncoding(1252), new CultureInfo("en-US"))
        {
        }

        /// <summary>
        /// Initializes a new instance of the CsvReader class with optional headers
        /// </summary>
        /// <param name="csvFilePath">The full path of the CSV file to read</param>
        /// <param name="hasHeaders">
        /// Indication if the file has headers
        /// </param>
        /// <param name="encoding">
        /// Specific file encoding format
        /// </param>
        /// <param name="culture">
        /// Culture for detecting integers, double, dates etc.
        /// </param>
        public CsvReader(string csvFilePath, bool hasHeaders, Encoding encoding, CultureInfo culture)
        {
            this.Culture = culture;
            this.Splitter = ',';
            this.HasHeaders = hasHeaders;
            this.Encoding = encoding;
            this.CsvFile = new FileInfo(csvFilePath);
            this.Reset();

            if (!this.CsvFile.Exists)
            {
                throw new ArgumentException("CSV file path not found (" + csvFilePath + ")");
            }

            if (this.CsvFile.Length == 0)
            {
                throw new ArgumentException("CSV file empty (" + csvFilePath + ")");
            }

            this.Lines = File.ReadAllLines(this.CsvFile.FullName, this.Encoding);

            if (hasHeaders)
            {
                this.Columns = this.Split(this.Lines[0]);
            }
        }

        /// <summary>
        /// Gets the culture for detecting values
        /// </summary>
        public CultureInfo Culture { get; private set; }

        /// <summary>
        /// Gets the encoding for the file contents
        /// </summary>
        public Encoding Encoding { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the file has headers in the first line
        /// </summary>
        public bool HasHeaders { get; private set; }

        /// <summary>
        /// Gets the current line index
        /// </summary>
        public int LineIndex { get; private set; }

        /// <summary>
        /// Gets the FileInfo instance of the CSV file to read
        /// </summary>
        public FileInfo CsvFile { get; private set; }

        /// <summary>
        /// Gets the list of lines available in the CSV file
        /// </summary>
        public string[] Lines { get; private set; }

        /// <summary>
        /// Gets the columns of the current line
        /// </summary>
        public string[] CurrentLine { get; private set; }

        /// <summary>
        /// Gets a list of the column names, if HasHeaders is true
        /// </summary>
        public string[] Columns { get; private set; }

        /// <summary>
        /// Gets or sets the splitter character, default is comma (',')
        /// </summary>
        public char Splitter { get; set; }

        /// <summary>
        /// Splits a string into an array based on the splitter char
        /// </summary>
        /// <param name="input">A full line with quotes and splitter chars</param>
        /// <returns>An array of string for each column</returns>
        public string[] Split(string input)
        {
            bool hasQuote = false;
            var cells = new List<string>();
            var currentCell = string.Empty;
            int counter = 1;

            foreach (char character in input)
            {
                if (character == '"')
                {
                    hasQuote = !hasQuote;
                }
                else if (character == this.Splitter)
                {
                    if (hasQuote)
                    {
                        currentCell += character;
                        continue;
                    }

                    cells.Add(currentCell);
                    currentCell = string.Empty;

                    if (counter == input.Length)
                    {
                        cells.Add(currentCell);
                    }
                }
                else
                {
                    currentCell += character;
                }

                counter++;
            }

            if (!string.IsNullOrEmpty(currentCell))
            {
                cells.Add(currentCell);                
            }

            return cells.ToArray();
        }

        /// <summary>
        /// Moves the line pointer and returns a value indicating whether no more lines exists
        /// </summary>
        /// <returns>A boolean object</returns>
        public bool Read()
        {
            if (this.LineIndex >= this.Lines.Length)
            {
                return false;
            }

            this.CurrentLine = this.Split(this.Lines[this.LineIndex]);
            this.LineIndex++;
            return true;
        }

        /// <summary>
        /// Returns a string object from the value of a given column index
        /// </summary>
        /// <param name="columnIndex">Index of the column to get data from</param>
        /// <returns>An string object</returns>
        public string GetString(int columnIndex)
        {
            if (columnIndex > this.Columns.Length)
            {
                throw new ArgumentException("Index out of bounds (tried: " + columnIndex + " length: " + this.Columns.Length);
            }

            return this.CurrentLine[columnIndex] ?? string.Empty;
        }

        /// <summary>
        /// Returns a string object from the value of a given column name
        /// </summary>
        /// <param name="columnName">Name of the column to get data from</param>
        /// <returns>An string object</returns>
        public string GetString(string columnName)
        {
            return this.GetString(this.GetColumnIndex(columnName));
        }

        /// <summary>
        /// Returns a int object from the value of a given column index
        /// </summary>
        /// <param name="columnIndex">Index of the column to get data from</param>
        /// <returns>An integer object</returns>
        public int GetInteger(int columnIndex)
        {
            if (columnIndex > this.Columns.Length)
            {
                throw new ArgumentException("Index out of bounds (tried: " + columnIndex + " length: " + this.Columns.Length);
            }

            return int.Parse(this.CurrentLine[columnIndex], this.Culture);
        }

        /// <summary>
        /// Returns a integer object from the value of a given column name
        /// </summary>
        /// <param name="columnName">Name of the column to get data from</param>
        /// <returns>An integer object</returns>
        public int GetInteger(string columnName)
        {
            return this.GetInteger(this.GetColumnIndex(columnName));
        }

        /// <summary>
        /// Returns a double object from the value of a given column index
        /// </summary>
        /// <param name="columnIndex">Index of the column to get data from</param>
        /// <returns>An double object</returns>
        public double GetDouble(int columnIndex)
        {
            if (columnIndex > this.Columns.Length)
            {
                throw new ArgumentException("Index out of bounds (tried: " + columnIndex + " length: " + this.Columns.Length);
            }

            return double.Parse(this.CurrentLine[columnIndex], this.Culture);
        }

        /// <summary>
        /// Returns a doubleeger object from the value of a given column name
        /// </summary>
        /// <param name="columnName">Name of the column to get data from</param>
        /// <returns>An double object</returns>
        public double GetDouble(string columnName)
        {
            return this.GetDouble(this.GetColumnIndex(columnName));
        }

        /// <summary>
        /// Returns a boolean object from the value of a given column index
        /// </summary>
        /// <param name="columnIndex">Index of the column to get data from</param>
        /// <returns>A boolean object</returns>
        public bool GetBoolean(int columnIndex)
        {
            if (columnIndex > this.Columns.Length)
            {
                throw new ArgumentException("Index out of bounds (tried: " + columnIndex + " length: " + this.Columns.Length);
            }

            bool result;
            if (bool.TryParse(this.CurrentLine[columnIndex], out result))
            {
                return result;
            }

            if (this.CurrentLine[columnIndex] == "1")
            {
                return true;
            }
            
            if (this.CurrentLine[columnIndex] == "0")
            {
                return false;
            }

            throw new Exception("String format cannot be converted to boolean (" + this.CurrentLine[columnIndex] + ")");
        }

        /// <summary>
        /// Returns a boolean object from the value of a given column name
        /// </summary>
        /// <param name="columnName">Name of the column to get data from</param>
        /// <returns>A boolean object</returns>
        public bool GetBoolean(string columnName)
        {
            return this.GetBoolean(this.GetColumnIndex(columnName));
        }

        /// <summary>
        /// Returns a guid object from the value of a given column index
        /// </summary>
        /// <param name="columnIndex">Index of the column to get data from</param>
        /// <returns>A guid object</returns>
        public Guid GetGuid(int columnIndex)
        {
            if (columnIndex > this.Columns.Length)
            {
                throw new ArgumentException("Index out of bounds (tried: " + columnIndex + " length: " + this.Columns.Length);
            }

            return Guid.Parse(this.CurrentLine[columnIndex]);
        }

        /// <summary>
        /// Returns a guid object from the value of a given column name
        /// </summary>
        /// <param name="columnName">Name of the column to get data from</param>
        /// <returns>A guid object</returns>
        public Guid GetGuid(string columnName)
        {
            return this.GetGuid(this.GetColumnIndex(columnName));
        }

        /// <summary>
        /// Returns a DateTime object from the value of a given column index
        /// </summary>
        /// <param name="columnIndex">Index of the column to get data from</param>
        /// <returns>A DateTime object</returns>
        public DateTime GetDateTime(int columnIndex)
        {
            if (columnIndex > this.Columns.Length)
            {
                throw new ArgumentException("Index out of bounds (tried: " + columnIndex + " length: " + this.Columns.Length);
            }

            return DateTime.Parse(this.CurrentLine[columnIndex], this.Culture);
        }

        /// <summary>
        /// Returns a DateTime object from the value of a given column name
        /// </summary>
        /// <param name="columnName">Name of the column to get data from</param>
        /// <returns>A DateTime object</returns>
        public DateTime GetDateTime(string columnName)
        {
            return this.GetDateTime(this.GetColumnIndex(columnName));
        }

        /// <summary>
        /// Gets the column index for a given column name (requires HasHeaders = true)
        /// </summary>
        /// <param name="columnName">Name of a column</param>
        /// <returns>The column index</returns>
        public int GetColumnIndex(string columnName)
        {
            if (!this.HasHeaders)
            {
                throw new FormatException("HasHeaders needs to be true to use this method");
            }

            for (int i = 0; i < this.Columns.Length; i++)
            {
                if (this.Columns[i].ToUpper() == columnName.ToUpper())
                {
                    return i;
                }
            }

            throw new ArgumentException("Column name not found (" + columnName + ")");
        }

        /// <summary>
        /// Moves the line pointer to the first line
        /// </summary>
        public void Reset()
        {
            this.LineIndex = this.HasHeaders ? 1 : 0;
        }
    }
}