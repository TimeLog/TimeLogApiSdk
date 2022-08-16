using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace TimeLog.Library.Data
{
    /// <summary>
    ///     Reads from a CSV file
    /// </summary>
    /// <remarks>
    ///     Usage:
    ///     var csv = new CsvReader(@"C:\test.csv");
    ///     while (csv.Read())
    ///     {
    ///     Logger.Debug(csv.LineIndex + " - " + csv.GetString("projectName"));
    ///     }
    /// </remarks>
    public class CsvReader
    {
        /// <summary>
        ///     Initializes a new instance of the CsvReader class with all settings for default.
        /// </summary>
        /// <remarks>Default: Headers, European file encoding (1252) and comma as splitter</remarks>
        /// <param name="csvFilePath">The full path of the CSV file to read</param>
        public CsvReader(string csvFilePath)
            : this(csvFilePath, ',')
        {
        }

        /// <summary>
        ///     Initializes a new instance of the CsvReader class with optional headers
        /// </summary>
        /// <param name="csvFilePath">The full path of the CSV file to read</param>
        /// <param name="splitter">The splitter character</param>
        public CsvReader(string csvFilePath, char splitter)
            : this(csvFilePath, splitter, true, Encoding.GetEncoding(1252), new CultureInfo("en-US"))
        {
        }

        /// <summary>
        ///     Initializes a new instance of the CsvReader class with optional headers
        /// </summary>
        /// <param name="csvFilePath">The full path of the CSV file to read</param>
        /// <param name="splitter">The splitter character</param>
        /// <param name="hasHeaders">Indication if the file has headers</param>
        public CsvReader(string csvFilePath, char splitter, bool hasHeaders)
            : this(csvFilePath, splitter, hasHeaders, Encoding.GetEncoding(1252), new CultureInfo("en-US"))
        {
        }

        /// <summary>
        ///     Initializes a new instance of the CsvReader class with optional headers
        /// </summary>
        /// <param name="csvFilePath">The full path of the CSV file to read</param>
        /// <param name="splitter">The splitter character</param>
        /// <param name="hasHeaders">Indication if the file has headers</param>
        /// <param name="encoding">Specific file encoding format</param>
        /// <param name="culture">Culture for detecting integers, double, dates etc.</param>
        public CsvReader(string csvFilePath, char splitter, bool hasHeaders, Encoding encoding, CultureInfo culture)
        {
            Culture = culture;
            Splitter = splitter;
            HasHeaders = hasHeaders;
            Encoding = encoding;
            CsvFile = new FileInfo(csvFilePath);
            Reset();

            if (!CsvFile.Exists)
            {
                throw new ArgumentException("CSV file path not found (" + csvFilePath + ")");
            }

            if (CsvFile.Length == 0)
            {
                throw new ArgumentException("CSV file empty (" + csvFilePath + ")");
            }

            Lines = File.ReadAllLines(CsvFile.FullName, Encoding);

            if (hasHeaders)
            {
                Columns = Split(Lines[0]);
                ColumnsLength = Columns.Length;
            }
            else
            {
                ColumnsLength = Lines[0].Length;
            }
        }

        /// <summary>
        ///     Gets the culture for detecting values
        /// </summary>
        public CultureInfo Culture { get; }

        /// <summary>
        ///     Gets the encoding for the file contents
        /// </summary>
        public Encoding Encoding { get; }

        /// <summary>
        ///     Gets a value indicating whether the file has headers in the first line
        /// </summary>
        public bool HasHeaders { get; }

        /// <summary>
        ///     Gets the current line index
        /// </summary>
        public int LineIndex { get; private set; }

        /// <summary>
        ///     Gets the FileInfo instance of the CSV file to read
        /// </summary>
        public FileInfo CsvFile { get; }

        /// <summary>
        ///     Gets the list of lines available in the CSV file
        /// </summary>
        public string[] Lines { get; }

        /// <summary>
        ///     Gets the columns of the current line
        /// </summary>
        public string[] CurrentLine { get; private set; }

        /// <summary>
        ///     Gets a list of the column names, if HasHeaders is true
        /// </summary>
        public string[] Columns { get; }

        /// <summary>
        ///     Gets the number of columns (whether the file has header or not
        /// </summary>
        public int ColumnsLength { get; }

        /// <summary>
        ///     Gets or sets the splitter character, default is comma (',')
        /// </summary>
        public char Splitter { get; set; }

        /// <summary>
        ///     Splits a string into an array based on the splitter char
        /// </summary>
        /// <param name="input">A full line with quotes and splitter chars</param>
        /// <returns>An array of string for each column</returns>
        public string[] Split(string input)
        {
            var hasQuote = false;
            var cells = new List<string>();
            var currentCell = string.Empty;
            var counter = 1;

            foreach (var character in input)
            {
                if (character == '"')
                {
                    hasQuote = !hasQuote;
                }
                else if (character == Splitter)
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
        ///     Moves the line pointer and returns a value indicating whether no more lines exists
        /// </summary>
        /// <returns>A boolean object</returns>
        public bool Read()
        {
            if (LineIndex >= Lines.Length)
            {
                return false;
            }

            CurrentLine = Split(Lines[LineIndex]);
            LineIndex++;
            return true;
        }

        /// <summary>
        ///     Returns a string object from the value of a given column index
        /// </summary>
        /// <param name="columnIndex">Index of the column to get data from</param>
        /// <returns>An string object</returns>
        public string GetString(int columnIndex)
        {
            if (columnIndex > ColumnsLength)
            {
                throw new ArgumentException("Index out of bounds (tried: " + columnIndex + " length: " + ColumnsLength);
            }

            return CurrentLine[columnIndex] ?? string.Empty;
        }

        /// <summary>
        ///     Returns a string object from the value of a given column name
        /// </summary>
        /// <param name="columnName">Name of the column to get data from</param>
        /// <returns>An string object</returns>
        public string GetString(string columnName)
        {
            return GetString(GetColumnIndex(columnName));
        }

        /// <summary>
        ///     Returns a int object from the value of a given column index
        /// </summary>
        /// <param name="columnIndex">Index of the column to get data from</param>
        /// <returns>An integer object</returns>
        public int GetInteger(int columnIndex)
        {
            if (columnIndex > ColumnsLength)
            {
                throw new ArgumentException("Index out of bounds (tried: " + columnIndex + " length: " + ColumnsLength);
            }

            return int.Parse(CurrentLine[columnIndex], Culture);
        }

        /// <summary>
        ///     Returns a TimeSpan object from the value of a given column index
        /// </summary>
        /// <param name="columnIndex">Index of the column to get data from</param>
        /// <returns>A TimeSpan object</returns>
        public TimeSpan GetTimeSpan(int columnIndex)
        {
            if (columnIndex > ColumnsLength)
            {
                throw new ArgumentException("Index out of bounds (tried: " + columnIndex + " length: " + ColumnsLength);
            }

            if (TimeSpan.TryParse(CurrentLine[columnIndex], out var _span))
            {
                return _span;
            }

            return new TimeSpan();
        }

        /// <summary>
        ///     Returns a integer object from the value of a given column name
        /// </summary>
        /// <param name="columnName">Name of the column to get data from</param>
        /// <returns>An integer object</returns>
        public int GetInteger(string columnName)
        {
            return GetInteger(GetColumnIndex(columnName));
        }

        /// <summary>
        ///     Returns a double object from the value of a given column index
        /// </summary>
        /// <param name="columnIndex">Index of the column to get data from</param>
        /// <returns>An double object</returns>
        public double GetDouble(int columnIndex)
        {
            if (columnIndex > ColumnsLength)
            {
                throw new ArgumentException("Index out of bounds (tried: " + columnIndex + " length: " + ColumnsLength);
            }

            return double.Parse(CurrentLine[columnIndex], Culture);
        }

        /// <summary>
        ///     Returns a doubleeger object from the value of a given column name
        /// </summary>
        /// <param name="columnName">Name of the column to get data from</param>
        /// <returns>An double object</returns>
        public double GetDouble(string columnName)
        {
            return GetDouble(GetColumnIndex(columnName));
        }

        /// <summary>
        ///     Returns a boolean object from the value of a given column index
        /// </summary>
        /// <param name="columnIndex">Index of the column to get data from</param>
        /// <returns>A boolean object</returns>
        public bool GetBoolean(int columnIndex)
        {
            if (columnIndex > ColumnsLength)
            {
                throw new ArgumentException("Index out of bounds (tried: " + columnIndex + " length: " + ColumnsLength);
            }

            if (bool.TryParse(CurrentLine[columnIndex], out var result))
            {
                return result;
            }

            if (CurrentLine[columnIndex] == "1")
            {
                return true;
            }

            if (CurrentLine[columnIndex] == "0")
            {
                return false;
            }

            throw new Exception("String format cannot be converted to boolean (" + CurrentLine[columnIndex] + ")");
        }

        /// <summary>
        ///     Returns a boolean object from the value of a given column name
        /// </summary>
        /// <param name="columnName">Name of the column to get data from</param>
        /// <returns>A boolean object</returns>
        public bool GetBoolean(string columnName)
        {
            return GetBoolean(GetColumnIndex(columnName));
        }

        /// <summary>
        ///     Returns a guid object from the value of a given column index
        /// </summary>
        /// <param name="columnIndex">Index of the column to get data from</param>
        /// <returns>A guid object</returns>
        public Guid GetGuid(int columnIndex)
        {
            if (columnIndex > ColumnsLength)
            {
                throw new ArgumentException("Index out of bounds (tried: " + columnIndex + " length: " + ColumnsLength);
            }

            return Guid.Parse(CurrentLine[columnIndex]);
        }

        /// <summary>
        ///     Returns a guid object from the value of a given column name
        /// </summary>
        /// <param name="columnName">Name of the column to get data from</param>
        /// <returns>A guid object</returns>
        public Guid GetGuid(string columnName)
        {
            return GetGuid(GetColumnIndex(columnName));
        }

        /// <summary>
        ///     Returns a DateTime object from the value of a given column index
        /// </summary>
        /// <param name="columnIndex">Index of the column to get data from</param>
        /// <returns>A DateTime object</returns>
        public DateTime GetDateTime(int columnIndex)
        {
            if (columnIndex > ColumnsLength)
            {
                throw new ArgumentException("Index out of bounds (tried: " + columnIndex + " length: " + ColumnsLength);
            }

            return DateTime.Parse(CurrentLine[columnIndex], Culture);
        }

        /// <summary>
        ///     Returns a DateTime object from the value of a given column name
        /// </summary>
        /// <param name="columnName">Name of the column to get data from</param>
        /// <returns>A DateTime object</returns>
        public DateTime GetDateTime(string columnName)
        {
            return GetDateTime(GetColumnIndex(columnName));
        }

        /// <summary>
        ///     Gets the column index for a given column name (requires HasHeaders = true)
        /// </summary>
        /// <param name="columnName">Name of a column</param>
        /// <returns>The column index</returns>
        public int GetColumnIndex(string columnName)
        {
            if (!HasHeaders)
            {
                throw new FormatException("HasHeaders needs to be true to use this method");
            }

            for (var i = 0; i < Columns.Length; i++)
            {
                if (Columns[i].ToUpper() == columnName.ToUpper())
                {
                    return i;
                }
            }

            throw new ArgumentException("Column name not found (" + columnName + ")");
        }

        /// <summary>
        ///     Moves the line pointer to the first line
        /// </summary>
        public void Reset()
        {
            LineIndex = HasHeaders ? 1 : 0;
        }
    }
}