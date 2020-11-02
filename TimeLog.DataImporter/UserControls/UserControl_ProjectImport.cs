using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TimeLog.DataImporter.Handlers;
using TimeLog.DataImporter.TimeLogApi;

namespace TimeLog.DataImporter.UserControls
{
    public partial class UserControl_ProjectImport : UserControl
    {
        private DataTable _projectTable;
        private DataTable _fileContent = new DataTable();

        private static readonly Dictionary<int, string> MandatoryFields = new Dictionary<int, string>
        {
            {0, "Project Name"},
            {1, "Customer"},
            {2, "Project Manager"},
            {3, "Project Template"},
            {4, "Project Currency"},
            {5, "Project Legal Entity"}
        };

        private static readonly int NumberOfMadatoryFields = 6;
        public UserControl_ProjectImport()
        {
            InitializeComponent();
            AddRowNumberToDataTable();
            InitializeProjectDataTable();
            dataGridView_project.DataSource = _projectTable;

        }

        private void InitializeProjectDataTable()
        {
            _projectTable = new DataTable();
            foreach (var _mandatoryField in MandatoryFields)
            {
                _projectTable.Columns.Add(_mandatoryField.Value);
            }

        }

        private void UserControl1_Load(object sender, EventArgs e)
        {

        }

        private void button_select_project_file_Click(object sender, EventArgs e)
        {
            _fileContent = ProjectHandler.Instance.GetFileContent();
            dataGridView_project.DataSource = _projectTable;

            foreach (DataRow _fileContentRow in _fileContent.Rows)
            {
                //this.Invoke((MethodInvoker)(() => _projectTable.Rows.Add()));
                _projectTable.Rows.Add();
            }


            comboBox_projectName.Items.AddRange(ProjectHandler.Instance._fileColumnHeaders.ToArray());
            comboBox_projectCustomer.Items.AddRange(ProjectHandler.Instance._fileColumnHeaders.ToArray());
            comboBox_projectManager.Items.AddRange(ProjectHandler.Instance._fileColumnHeaders.ToArray());
            comboBox_projectTemplate.Items.AddRange(ProjectHandler.Instance._fileColumnHeaders.ToArray());
            comboBox_projectCurrency.Items.AddRange(ProjectHandler.Instance._fileColumnHeaders.ToArray());
            comboBox_projectLegalEntity.Items.AddRange(ProjectHandler.Instance._fileColumnHeaders.ToArray());

        }

      

        private void button_validate_Click(object sender, EventArgs e)
        {
            this.WorkerFetcher.RunWorkerAsync();

        }

        private void WorkerFetcherDoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            foreach (DataGridViewRow _row in this.dataGridView_project.Rows)
            {
                if (this.WorkerFetcher.CancellationPending)
                {
                    break;
                }

                ProjectModel _newProject = new ProjectModel();

                _newProject.Name = _row.Cells[0].Value.ToString();
                //_newProject.Name = _row.Cells[this.dataGridView_project.Columns[this.comboBox_projectName.SelectedItem.ToString()].Index].Value.ToString();
                

                var _response =  ProjectHandler.Instance.ValidateProject(_newProject, AuthenticationHandler.Instance._token);

                HandleApiResponse(_response, _row);
               
            }
        }


        private void HandleApiResponse(ApiResponse _response, DataGridViewRow _row)
        {
            if (_response.Code == 200)
            {
                this.Invoke((MethodInvoker)(() => _row.DefaultCellStyle.BackColor = Color.LimeGreen));
                this.Invoke((MethodInvoker)(() => this.textBox_projectImportMessages.AppendText(Environment.NewLine)));
                this.Invoke((MethodInvoker)(() => this.textBox_projectImportMessages.AppendText("Row " + _row.Index + _response.Message)));
            }
            else
            {
                this.Invoke((MethodInvoker)(() => _row.DefaultCellStyle.BackColor = Color.Red));
                this.Invoke((MethodInvoker)(() => this.textBox_projectImportMessages.AppendText(Environment.NewLine)));
                this.Invoke((MethodInvoker)(() => this.textBox_projectImportMessages.AppendText("Row " + _row.Index + ": " + _response.Message + " Details: " + string.Join(";", _response.Details))));
            }
        }

        private void button_stop_Click(object sender, EventArgs e)
        {
            this.WorkerFetcher.CancelAsync();
        }

        private void button_clear_Click(object sender, EventArgs e)
        {
            this.textBox_projectImportMessages.Text = String.Empty;
            this.dataGridView_project.DataSource = null;
            this.dataGridView_project.Rows.Clear();

            this.comboBox_projectName.Items.Clear();
            this.comboBox_projectName.ResetText();

            _fileContent = new DataTable();
            InitializeProjectDataTable();

        }

        private void comboBox_projectName_SelectedIndexChanged(object sender, EventArgs e)
        {
            var _columnIndex = _fileContent.Columns.IndexOf(this.comboBox_projectName.SelectedItem.ToString());


            for(int i=0; i < _fileContent.Rows.Count;i++)
            {
                this.Invoke((MethodInvoker)(() => _projectTable.Rows[i][0] = _fileContent.Rows[i][_columnIndex]));
            }

            CheckCellsForNullOrEmpty(0);

        }

        private void comboBox_projectCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            var _columnIndex = _fileContent.Columns.IndexOf(this.comboBox_projectCustomer.SelectedItem.ToString());

            for(int i=0; i < _fileContent.Rows.Count;i++)
            {
                this.Invoke((MethodInvoker)(() => _projectTable.Rows[i][1] = _fileContent.Rows[i][_columnIndex]));
            }

            CheckCellsForNullOrEmpty(1);

        }

        private void comboBox_projectTemplate_SelectedIndexChanged(object sender, EventArgs e)
        {
            var _columnIndex = this.dataGridView_project.Columns[this.comboBox_projectTemplate.SelectedItem.ToString()].Index;
            MakeColumnVisible(_columnIndex);
            CheckCellsForNullOrEmpty(_columnIndex);

        }

        private void comboBox_projectManager_SelectedIndexChanged(object sender, EventArgs e)
        {
            var _columnIndex = this.dataGridView_project.Columns[this.comboBox_projectManager.SelectedItem.ToString()].Index;
            MakeColumnVisible(_columnIndex);
            CheckCellsForNullOrEmpty(_columnIndex);

        }

        private void comboBox_projectCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {
            var _columnIndex = this.dataGridView_project.Columns[this.comboBox_projectCurrency.SelectedItem.ToString()].Index;
            MakeColumnVisible(_columnIndex);
            CheckCellsForNullOrEmpty(_columnIndex);

        }

        private void comboBox_projectLegalEntity_SelectedIndexChanged(object sender, EventArgs e)
        {
            var _columnIndex = this.dataGridView_project.Columns[this.comboBox_projectLegalEntity.SelectedItem.ToString()].Index;
            MakeColumnVisible(_columnIndex);
            CheckCellsForNullOrEmpty(_columnIndex);
        }








        #region Helper methods

        private void MakeColumnVisible(int columnIndex)
        {

            dataGridView_project.Columns[columnIndex].Visible = true;
        }

        private void CheckCellsForNullOrEmpty(int columnIndex)
        {
            foreach (DataGridViewRow _row in this.dataGridView_project.Rows)
            {
                if (_row.Cells[columnIndex].Value == null || string.IsNullOrEmpty(_row.Cells[columnIndex].Value.ToString()) || _row.Cells[columnIndex].Value.ToString().Equals("Søren Parrot"))
                {
                    _row.Cells[columnIndex].Style.BackColor = Color.Red;
                }
                
            }
        }

       
        public void ClearColumn(int columnIndex)
        {
            if (dataGridView_project.Columns != null && dataGridView_project.Columns.Count - 1 >= columnIndex)
            {
                var tmpCol = dataGridView_project.Columns[columnIndex];
                dataGridView_project.Columns.Remove(dataGridView_project.Columns[columnIndex]);
                dataGridView_project.Columns.Insert(columnIndex, tmpCol);
            }
        }
         

        #endregion


    }
}
