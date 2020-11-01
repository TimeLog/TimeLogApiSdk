using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TimeLog.DataImporter.Handlers
{
    public class BaseHandler
    {
        public List<string> _fileColumnHeaders = new List<string>();


         public DataTable GetFileContent()
        {
            try
            {
                OpenFileDialog _dialog = new OpenFileDialog();
                _dialog.ShowDialog();
                
                string SourceURl = "";
                if (_dialog.FileName != "")
                {
                    if (_dialog.FileName.EndsWith(".csv"))
                    {
                        DataTable _fileData = new DataTable();
                        _fileData = GetDataTableFromCSVFile(_dialog.FileName);
                        //if (Convert.ToString(dtNew.Columns[0]).ToLower() != "lookupcode")
                        //{
                        //    MessageBox.Show("Invalid Items File");
                        //    //btnSave.Enabled = false;
                        //    return;
                        //}

                        //txtFile.Text = dialog.SafeFileName;
                        SourceURl = _dialog.FileName;
                        if (_fileData.Rows != null && _fileData.Rows.ToString() != String.Empty)
                        {
                            return _fileData;
                        }

                        //foreach (DataGridViewRow row in dataGridView1.Rows)
                        //{
                        //    if (Convert.ToString(row.Cells["LookupCode"].Value) == "" || row.Cells["LookupCode"].Value == null || Convert.ToString(row.Cells["ItemName"].Value) == "" || row.Cells["ItemName"].Value == null || Convert.ToString(row.Cells["DeptId"].Value) == "" || row.Cells["DeptId"].Value == null || Convert.ToString(row.Cells["Price"].Value) == "" || row.Cells["Price"].Value == null)
                        //    {
                        //        row.DefaultCellStyle.BackColor = Color.Red;
                        //        inValidItem += 1;
                        //    }
                        //    else
                        //    {
                        //        ImportedRecord += 1;
                        //    }
                        //}

                        if (_fileData.Rows.Count == 0)
                        {
                            //btnSave.Enabled = false;
                            MessageBox.Show("There is no data in this file", "GAUTAM POS", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        }

                    }
                    else
                    {
                        MessageBox.Show("Selected File is Invalid, Please Select valid csv file.", "GAUTAM POS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception " + ex);
            }
            return null;

        }

        


        private DataTable GetDataTableFromCSVFile(string csv_file_path)
        {
            DataTable csvData = new DataTable();
            try
            {
                if (csv_file_path.EndsWith(".csv"))
                {
                    using (Microsoft.VisualBasic.FileIO.TextFieldParser csvReader = new Microsoft.VisualBasic.FileIO.TextFieldParser(csv_file_path))
                    {
                        csvReader.SetDelimiters(new string[]
                        {
                            ";"
                        });
                        //csvReader.HasFieldsEnclosedInQuotes = true;
                        //read column  
                        string[] colFields = csvReader.ReadFields();
                        foreach (string column in colFields)
                        {
                            DataColumn datacolumn = new DataColumn(column);
                            datacolumn.AllowDBNull = true;
                            csvData.Columns.Add(datacolumn);
                            _fileColumnHeaders.Add(datacolumn.ColumnName.ToString());
                        }

                        while (!csvReader.EndOfData)
                        {
                            string[] fieldData = csvReader.ReadFields();
                            for (int i = 0; i < fieldData.Length; i++)
                            {
                                if (fieldData[i] == "")
                                {
                                    fieldData[i] = null;
                                }
                            }

                            csvData.Rows.Add(fieldData);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exce " + ex);
            }

            return csvData;
        }

    }
}
