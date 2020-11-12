using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using TimeLog.DataImporter.Handlers;
using TimeLog.DataImporter.TimeLogApi;
using TimeLog.DataImporter.TimeLogApi.Model;

namespace TimeLog.DataImporter.UserControls
{
    public partial class UserControl_ProjectImport : UserControl
    {
        private DataTable _projectTable;
        private DataTable _fileContent;

        private static readonly Dictionary<int, string> MandatoryFields = new Dictionary<int, string>
        {
            {0, "Project Name"},
            {1, "Customer"},
            {2, "Project Template"},
            {3, "Project Manager"},
            {4, "Project Currency"},
            {5, "Project Legal Entity"}
        };

        public UserControl_ProjectImport()
        {
            InitializeComponent();
            InitializeDelimiterComboBox();
            AddRowNumberToDataTable();
            InitializeProjectDataTable();
            dataGridView_project.DataSource = _projectTable;
        }

        private void UserControl1_Load(object sender, EventArgs e)
        {

        }

        private void InitializeDelimiterComboBox()
        {
            comboBox_delimiter.Items.AddRange(ProjectHandler.Instance.GetDelimiterList().ToArray());
            comboBox_delimiter.SelectedIndex = 0;
        }

        private void InitializeProjectDataTable()
        {
            _projectTable = new DataTable();

            foreach (var _mandatoryField in MandatoryFields)
            {
                _projectTable.Columns.Add(_mandatoryField.Value);
            }
        }

        private void button_select_project_file_Click(object sender, EventArgs e)
        {
            _fileContent = new DataTable();
            _fileContent = ProjectHandler.Instance.GetFileContent(comboBox_delimiter.SelectedItem.ToString());

            if (_fileContent != null)
            {
                foreach (DataRow _fileContentRow in _fileContent.Rows)
                {
                    _projectTable.Rows.Add();
                }

                comboBox_projectName.Items.AddRange(ProjectHandler.Instance.FileColumnHeaders.ToArray());
                comboBox_projectCustomer.Items.AddRange(ProjectHandler.Instance.FileColumnHeaders.ToArray());
                comboBox_projectManager.Items.AddRange(ProjectHandler.Instance.FileColumnHeaders.ToArray());
                comboBox_projectTemplate.Items.AddRange(ProjectHandler.Instance.FileColumnHeaders.ToArray());
                comboBox_projectCurrency.Items.AddRange(ProjectHandler.Instance.FileColumnHeaders.ToArray());
                comboBox_projectLegalEntity.Items.AddRange(ProjectHandler.Instance.FileColumnHeaders.ToArray());
                comboBox_sampleNonMandatory.Items.AddRange(ProjectHandler.Instance.FileColumnHeaders.ToArray());
            }
        }

        private void button_validate_Click(object sender, EventArgs e)
        {
            WorkerFetcher.RunWorkerAsync();
        }

        private void button_stop_Click(object sender, EventArgs e)
        {
            WorkerFetcher.CancelAsync();
        }

        private void button_clear_Click(object sender, EventArgs e)
        {
            textBox_projectImportMessages.Text = String.Empty;
            dataGridView_project.DataSource = null;
            ProjectHandler.Instance.FileColumnHeaders = new List<string>();

            //clear and reset all combo boxes
            comboBox_projectName.ResetText();
            comboBox_projectName.Items.Clear();
            comboBox_projectCustomer.ResetText();
            comboBox_projectCustomer.Items.Clear();
            comboBox_projectTemplate.ResetText();
            comboBox_projectTemplate.Items.Clear();
            comboBox_projectManager.ResetText();
            comboBox_projectManager.Items.Clear();
            comboBox_projectCurrency.ResetText();
            comboBox_projectCurrency.Items.Clear();
            comboBox_projectLegalEntity.ResetText();
            comboBox_projectLegalEntity.Items.Clear();
            comboBox_sampleNonMandatory.ResetText();
            comboBox_sampleNonMandatory.Items.Clear();

            _fileContent = new DataTable();
            InitializeProjectDataTable();
            dataGridView_project.DataSource = _projectTable;
        }

        private void WorkerFetcherDoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            if (dataGridView_project != null && dataGridView_project.RowCount > 1)
            {
                foreach (DataGridViewRow _row in dataGridView_project.Rows)
                {
                    if (WorkerFetcher.CancellationPending)
                    {
                        break;
                    }

                    ProjectCreateModel _newProject = new ProjectCreateModel
                    {
                        Name = _row.Cells[0].Value.ToString()
                        //Name = _row.Cells[dataGridView_project.Columns[comboBox_projectName.SelectedItem.ToString()].Index].Value.ToString()
                    };

                    var _response = ProjectHandler.Instance.ValidateProject(_newProject, AuthenticationHandler.Instance._token);

                    HandleApiResponse(_response, _row);
                }
            }
        }

        private void HandleApiResponse(BusinessRulesApiResponse response, DataGridViewRow row)
        {
            if (response.Code == 200)
            {
                Invoke((MethodInvoker)(() => row.DefaultCellStyle.BackColor = Color.LimeGreen));
                Invoke((MethodInvoker)(() => this.textBox_projectImportMessages.AppendText(Environment.NewLine)));
                Invoke((MethodInvoker)(() => this.textBox_projectImportMessages.AppendText("Row " + row.Index + response.Message)));
            }
            else
            {
                Invoke((MethodInvoker)(() => row.DefaultCellStyle.BackColor = Color.Red));
                Invoke((MethodInvoker)(() => this.textBox_projectImportMessages.AppendText(Environment.NewLine)));
                Invoke((MethodInvoker)(() => this.textBox_projectImportMessages.AppendText("Row " + row.Index + ": " + response.Message + " Details: " + string.Join(";", response.Details))));
            }
        }

        #region Helper methods

        private void CheckCellsForNullOrEmpty(int columnIndex)
        {
            foreach (DataGridViewRow _row in dataGridView_project.Rows)
            {
                if (_row.Cells[columnIndex].Value == null || string.IsNullOrEmpty(_row.Cells[columnIndex].Value.ToString()))
                {
                    _row.Cells[columnIndex].Style.BackColor = Color.Red;
                }
            }
        }

        public void ClearColumn(int columnIndex)
        {
            if (dataGridView_project != null && dataGridView_project.Columns.Count - 1 >= columnIndex)
            {
                var _tmpCol = dataGridView_project.Columns[columnIndex];
                dataGridView_project.Columns.Remove(dataGridView_project.Columns[columnIndex]);
                dataGridView_project.Columns.Insert(columnIndex, _tmpCol);
            }
        }

        #endregion

        private void comboBox_projectName_SelectedIndexChanged(object sender, EventArgs e)
        {
            var _columnIndex = _fileContent.Columns.IndexOf(comboBox_projectName.SelectedItem.ToString());

            var _tableColumnIndex = _projectTable.Columns.IndexOf("Project Name"); ;

            ClearColumn(_tableColumnIndex);

            for(int i = 0; i < _fileContent.Rows.Count; i++)
            {
                this.Invoke((MethodInvoker)(() => _projectTable.Rows[i][_tableColumnIndex] = _fileContent.Rows[i][_columnIndex]));
            }

            CheckCellsForNullOrEmpty(_tableColumnIndex);
        }

        private void comboBox_projectCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            var _columnIndex = _fileContent.Columns.IndexOf(comboBox_projectCustomer.SelectedItem.ToString());

            var _tableColumnIndex = _projectTable.Columns.IndexOf("Customer");
            
            ClearColumn(_tableColumnIndex);

            for (int i = 0; i < _fileContent.Rows.Count; i++)
            {
                this.Invoke((MethodInvoker)(() => _projectTable.Rows[i][_tableColumnIndex] = _fileContent.Rows[i][_columnIndex]));
            }

            CheckCellsForNullOrEmpty(_tableColumnIndex);
        }

        private void comboBox_projectTemplate_SelectedIndexChanged(object sender, EventArgs e)
        {
            var _columnIndex = _fileContent.Columns.IndexOf(comboBox_projectTemplate.SelectedItem.ToString());

            var _tableColumnIndex = _projectTable.Columns.IndexOf("Project Template");

            ClearColumn(_tableColumnIndex);

            for (int i = 0; i < _fileContent.Rows.Count; i++)
            {
                this.Invoke((MethodInvoker)(() => _projectTable.Rows[i][_tableColumnIndex] = _fileContent.Rows[i][_columnIndex]));
            }

            CheckCellsForNullOrEmpty(_tableColumnIndex);
        }

        private void comboBox_projectManager_SelectedIndexChanged(object sender, EventArgs e)
        {
            var _columnIndex = _fileContent.Columns.IndexOf(comboBox_projectManager.SelectedItem.ToString());

            var _tableColumnIndex = _projectTable.Columns.IndexOf("Project Manager");

            ClearColumn(_tableColumnIndex);

            for (int i = 0; i < _fileContent.Rows.Count; i++)
            {
                this.Invoke((MethodInvoker)(() => _projectTable.Rows[i][_tableColumnIndex] = _fileContent.Rows[i][_columnIndex]));
            }

            CheckCellsForNullOrEmpty(_tableColumnIndex);
        }

        private void comboBox_projectCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {
            var _columnIndex = _fileContent.Columns.IndexOf(comboBox_projectCurrency.SelectedItem.ToString());

            var _tableColumnIndex = _projectTable.Columns.IndexOf("Project Currency");

            ClearColumn(_tableColumnIndex);

            for (int i = 0; i < _fileContent.Rows.Count; i++)
            {
                this.Invoke((MethodInvoker)(() => _projectTable.Rows[i][_tableColumnIndex] = _fileContent.Rows[i][_columnIndex]));
            }

            CheckCellsForNullOrEmpty(_tableColumnIndex);
        }

        private void comboBox_projectLegalEntity_SelectedIndexChanged(object sender, EventArgs e)
        {
            var _columnIndex = _fileContent.Columns.IndexOf(comboBox_projectLegalEntity.SelectedItem.ToString());

            var _tableColumnIndex = _projectTable.Columns.IndexOf("Project Legal Entity");

            ClearColumn(_tableColumnIndex);

            for (int i = 0; i < _fileContent.Rows.Count; i++)
            {
                this.Invoke((MethodInvoker)(() => _projectTable.Rows[i][_tableColumnIndex] = _fileContent.Rows[i][_columnIndex]));
            }

            CheckCellsForNullOrEmpty(_tableColumnIndex);
        }

        private void comboBox_sampleNonMandatory_SelectedIndexChanged(object sender, EventArgs e)
        {
            var _columnIndex = _fileContent.Columns.IndexOf(comboBox_sampleNonMandatory.SelectedItem.ToString());

            string _columnName = "Sample";

            if (!_projectTable.Columns.Contains(_columnName))
            {
                _projectTable.Columns.Add(_columnName);
            }

            var _tableColumnIndex = _projectTable.Columns.IndexOf(_columnName);

            ClearColumn(_tableColumnIndex);

            for (int i = 0; i < _fileContent.Rows.Count; i++)
            {
                this.Invoke((MethodInvoker)(() => _projectTable.Rows[i][_tableColumnIndex] = _fileContent.Rows[i][_columnIndex]));
            }

            CheckCellsForNullOrEmpty(_tableColumnIndex);
        }
    }
}