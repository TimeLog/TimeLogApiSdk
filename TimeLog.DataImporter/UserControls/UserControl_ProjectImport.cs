using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TimeLog.DataImporter.Handlers;
using TimeLog.DataImporter.TimeLogApi;
using TimeLog.DataImporter.TimeLogApi.Model;

namespace TimeLog.DataImporter.UserControls
{
    public partial class UserControl_ProjectImport : UserControl
    {
        #region Variable declarations

        private DataTable _projectTable;
        private DataTable _fileContent;
        private Button _senderButton;
        private bool _isRowValid = true;
        private int _errorRowCount;

        private static readonly Dictionary<int, string> MandatoryFields = new Dictionary<int, string>
        {
            {0, "Project Name"},
            {1, "Customer ID"},
            {2, "Project Template ID"},
            {3, "Project Manager ID"},
            {4, "Currency ID"},
            {5, "Legal Entity ID"},
            {6, "Project Type ID"}
        };

        //all column header variables
        private readonly string _projectName = "Project Name";
        private readonly string _customerID = "Customer ID";
        private readonly string _projectTemplateID = "Project Template ID";
        private readonly string _projectManagerID = "Project Manager ID";
        private readonly string _currencyID = "Currency ID";
        private readonly string _legalEntityID = "Legal Entity ID";
        private readonly string _projectNo = "Project No";
        private readonly string _description = "Description";
        private readonly string _projectStartDate = "Project Start Date";
        private readonly string _projectEndDate = "Project End Date";
        private readonly string _projectTypeID = "Project Type ID";
        private readonly string _projectCategoryID = "Project Category ID";

        //default value lists from API 
        private List<KeyValuePair<int, string>> _projectTemplateIDList;
        private List<KeyValuePair<int, string>> _currencyIDList;
        private List<KeyValuePair<int, string>> _legalEntityIDList;
        private List<KeyValuePair<int, string>> _projectTypeIDList;
        private List<KeyValuePair<int, string>> _projectCategoryIDList;

        //expanding panels' current states, expand panels, expand buttons
        private BaseHandler.ExpandState[] _expandStates;
        private Panel[] _expandPanels;
        private Button[] _expandButtons;

        //set the number of pixels expanded per timer Tick
        private const int ExpansionPerTick = 7;

        #endregion

        public UserControl_ProjectImport()
        {
            InitializeComponent();
            InitializeDelimiterComboBox();
            InitializeExpandCollapsePanels();
            AddRowNumberToDataTable();
            InitializeProjectDataTable();
            dataGridView_project.DataSource = _projectTable;
            button_import.Enabled = false;
        }

        private void UserControl1_Load(object sender, EventArgs e)
        {
          
        }

        #region Initialization methods

        private void InitializeExpandCollapsePanels()
        {
            _expandStates = new[]
            {
                BaseHandler.ExpandState.Expanded
            };
            _expandPanels = new[]
            {
                panel_NonMandatoryFields
            };
            _expandButtons = new[]
            {
                button_expandNonMandatory
            };

            for (int i = 0; i < _expandButtons.Length; i++)
            {
                _expandButtons[i].Tag = i;
                _expandButtons[i].BackgroundImage = Properties.Resources.upload;
            }
        }

        private void InitializeDelimiterComboBox()
        {
            comboBox_delimiter.Items.AddRange(ProjectHandler.Instance.GetDelimiterList().Cast<object>().ToArray());
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

        #endregion

        #region Functionalities implementations

        private void button_select_project_file_Click(object sender, EventArgs e)
        {
            ProjectHandler.Instance.FileColumnHeaders = new List<string>();
            _fileContent = new DataTable();
            _fileContent = ProjectHandler.Instance.GetFileContent(comboBox_delimiter.SelectedItem.ToString());

            if (_fileContent != null)
            {
                textBox_projectImportMessages.Text = string.Empty;
                ClearAndResetAllCheckBoxes();
                ClearAndResetAllComboBoxes();
                Invoke((MethodInvoker)(() => button_import.Enabled = false));

                if (dataGridView_project.RowCount > 1)
                {
                    dataGridView_project.DataSource = null;
                    InitializeProjectDataTable();
                    dataGridView_project.DataSource = _projectTable;
                }

                foreach (DataRow _fileContentRow in _fileContent.Rows)
                {
                    _projectTable.Rows.Add();
                }

                AddFileColumnHeaderToComboBox(ProjectHandler.Instance.FileColumnHeaders.Cast<object>().ToArray());
            }
            else
            {
                button_clear_Click(sender, e);
            }
        }

        private void button_validate_Click(object sender, EventArgs e)
        {
            textBox_projectImportMessages.Text = string.Empty;
            _senderButton = (Button) sender;
            WorkerFetcher.RunWorkerAsync();
        }

        private void button_stop_Click(object sender, EventArgs e)
        {
            WorkerFetcher.CancelAsync();
        }

        private void button_import_Click(object sender, EventArgs e)
        {
            textBox_projectImportMessages.Text = string.Empty;
            _senderButton = (Button) sender;
            WorkerFetcher.RunWorkerAsync();
        }

        private void button_clear_Click(object sender, EventArgs e)
        {
            ProjectHandler.Instance.FileColumnHeaders = new List<string>();
            textBox_projectImportMessages.Text = string.Empty;
            ClearAndResetAllCheckBoxes();
            ClearAndResetAllComboBoxes();
            Invoke((MethodInvoker)(() => button_import.Enabled = false));

            dataGridView_project.DataSource = null;
            InitializeProjectDataTable();
            dataGridView_project.DataSource = _projectTable;
        }

        private void textBox_projectImportMessages_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var _position = textBox_projectImportMessages.GetCharIndexFromPosition(e.Location);
                var _lineNo = textBox_projectImportMessages.GetLineFromCharIndex(_position) - 1;

                for (int i = 0; i < dataGridView_project.Rows.Count - 1; i++)
                {
                    if (i == _lineNo)
                    {
                        Invoke((MethodInvoker)(() => dataGridView_project.Rows[i].Selected = true));
                        dataGridView_project.FirstDisplayedScrollingRowIndex = i;
                        dataGridView_project.Focus();
                        break;
                    }
                }
            }
        }

        private void button_expand_Click(object sender, EventArgs e)
        {
            Button _button = sender as Button;
            int _index = (int)_button.Tag;

            // Get this panel's current expand state and set its new state
            BaseHandler.ExpandState _oldState = _expandStates[_index];

            if (_oldState == BaseHandler.ExpandState.Collapsed || _oldState == BaseHandler.ExpandState.Collapsing)
            {
                _expandStates[_index] = BaseHandler.ExpandState.Expanding;
                _expandButtons[_index].BackgroundImage = Properties.Resources.upload;
            }
            else
            {
                _expandStates[_index] = BaseHandler.ExpandState.Collapsing;
                _expandButtons[_index].BackgroundImage = Properties.Resources.download;
            }

            tmrExpand.Enabled = true;
        }

        private void tmrExpand_Tick(object sender, EventArgs e)
        {
            bool _notDone = false;

            for (int i = 0; i < _expandPanels.Length; i++)
            {
                Panel _panel = _expandPanels[i];
                int _newHeight = _panel.Height;

                if (_expandStates[i] == BaseHandler.ExpandState.Expanding)
                {
                    _newHeight = _panel.Height + ExpansionPerTick;

                    if (_newHeight <= _panel.MaximumSize.Height)
                    {
                        _newHeight = _panel.MaximumSize.Height;
                    }
                    else
                    {
                        _notDone = true;
                    }
                }
                else if (_expandStates[i] == BaseHandler.ExpandState.Collapsing)
                {
                    _newHeight = _panel.Height - ExpansionPerTick;

                    if (_newHeight <= _panel.MinimumSize.Height)
                    {
                        _newHeight = _panel.MinimumSize.Height;
                    }
                    else
                    {
                        _notDone = true;
                    }
                }

                _panel.Height = _newHeight;
            }

            // If we are done, disable the timer
            tmrExpand.Enabled = _notDone;
        }

        private void WorkerFetcherDoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            if (dataGridView_project != null && dataGridView_project.RowCount > 1)
            {
                _isRowValid = true;
                _errorRowCount = 0;

                //while validating, deactivate other buttons
                Invoke((MethodInvoker)(() => button_validate.Enabled = false));
                Invoke((MethodInvoker)(() => button_import.Enabled = false));
                Invoke((MethodInvoker)(() => button_clear.Enabled = false));
                Invoke((MethodInvoker)(() => button_projectSelectFile.Enabled = false));

                try
                {
                    foreach (DataGridViewRow _row in dataGridView_project.Rows)
                    {
                        if (WorkerFetcher.CancellationPending)
                        {
                            break;
                        }

                        if (_row.DataBoundItem != null)
                        {
                            ProjectCreateModel _newProject = new ProjectCreateModel
                            {
                                Name = (dataGridView_project.Columns[_projectName] != null)
                                    ? ProjectHandler.Instance.CheckAndGetString(_row.Cells[dataGridView_project.Columns[_projectName].Index].Value) : string.Empty,
                                CustomerID = (dataGridView_project.Columns[_customerID] != null)
                                    ? ProjectHandler.Instance.CheckAndGetInteger(_customerID, _row.Cells[dataGridView_project.Columns[_customerID].Index].Value) : 0,
                                ProjectTemplateID = (dataGridView_project.Columns[_projectTemplateID] != null)
                                    ? ProjectHandler.Instance.CheckAndGetInteger(_projectTemplateID, _row.Cells[dataGridView_project.Columns[_projectTemplateID].Index].Value) : 0,
                                ProjectManagerID = (dataGridView_project.Columns[_projectManagerID] != null)
                                    ? ProjectHandler.Instance.CheckAndGetInteger(_projectManagerID, _row.Cells[dataGridView_project.Columns[_projectManagerID].Index].Value) : 0,
                                CurrencyID = (dataGridView_project.Columns[_currencyID] != null)
                                    ? ProjectHandler.Instance.CheckAndGetInteger(_currencyID, _row.Cells[dataGridView_project.Columns[_currencyID].Index].Value) : 0,
                                LegalEntityID = (dataGridView_project.Columns[_legalEntityID] != null)
                                    ? ProjectHandler.Instance.CheckAndGetInteger(_legalEntityID, _row.Cells[dataGridView_project.Columns[_legalEntityID].Index].Value) : 0,
                                ProjectNo = (dataGridView_project.Columns[_projectNo] != null)
                                    ? ProjectHandler.Instance.CheckAndGetString(_row.Cells[dataGridView_project.Columns[_projectNo].Index].Value) : string.Empty,
                                Description = (dataGridView_project.Columns[_description] != null)
                                    ? ProjectHandler.Instance.CheckAndGetString(_row.Cells[dataGridView_project.Columns[_description].Index].Value) : string.Empty,
                                ProjectStartDate = (dataGridView_project.Columns[_projectStartDate] != null)
                                    ? ProjectHandler.Instance.CheckAndGetDate(_projectStartDate, _row.Cells[dataGridView_project.Columns[_projectStartDate].Index].Value) : DateTime.Now,
                                ProjectEndDate = (dataGridView_project.Columns[_projectEndDate] != null)
                                    ? ProjectHandler.Instance.CheckAndGetDate(_projectEndDate, _row.Cells[dataGridView_project.Columns[_projectEndDate].Index].Value) : DateTime.Now,
                                ProjectTypeID = (dataGridView_project.Columns[_projectTypeID] != null)
                                    ? ProjectHandler.Instance.CheckAndGetInteger(_projectTypeID, _row.Cells[dataGridView_project.Columns[_projectTypeID].Index].Value) : 0,
                                ProjectCategoryID = (dataGridView_project.Columns[_projectCategoryID] != null)
                                    ? ProjectHandler.Instance.CheckAndGetInteger(_projectCategoryID, _row.Cells[dataGridView_project.Columns[_projectCategoryID].Index].Value) : 0,
                            };

                            if (_senderButton.Name == button_validate.Name)
                            {
                                var _defaultApiResponse = ProjectHandler.Instance.ValidateProject(_newProject,
                                    AuthenticationHandler.Instance.Token, out var _businessRulesApiResponse);

                                HandleApiResponse(_defaultApiResponse, _row, _businessRulesApiResponse);
                            }
                            else
                            {
                                var _defaultApiResponse = ProjectHandler.Instance.ImportProject(_newProject,
                                    AuthenticationHandler.Instance.Token, out var _businessRulesApiResponse);

                                HandleApiResponse(_defaultApiResponse, _row, _businessRulesApiResponse);

                                _isRowValid = false;
                            }
                        }
                    }

                    //show error row count at the end
                    Invoke((MethodInvoker)(() => textBox_projectImportMessages.AppendText(Environment.NewLine + Environment.NewLine)));
                    Invoke((MethodInvoker)(() => textBox_projectImportMessages.AppendText("Invalid data input row count: " + _errorRowCount)));
                    Invoke((MethodInvoker)(() => textBox_projectImportMessages.AppendText(Environment.NewLine + Environment.NewLine)));

                    //display success message after import / validation is done
                    if (_errorRowCount == 0)
                    {
                        if (_senderButton.Name == button_validate.Name)
                        {
                            Invoke((MethodInvoker)(() => textBox_projectImportMessages.AppendText("Validation completed successfully with no error. You may press the Import button to start importing data right away.")));
                        }
                        else
                        {
                            Invoke((MethodInvoker)(() => textBox_projectImportMessages.AppendText("Data import completed successfully with no error. Excellent!")));
                        }
                    }
                    else
                    {
                        Invoke((MethodInvoker)(() => textBox_projectImportMessages.AppendText("Validation completed successfully with " + _errorRowCount + " error(s). You may modify the invalid input data and then press Validate button again.")));
                    }

                    //enable import button when there is no error in validation
                    if (_isRowValid)
                    {
                        Invoke((MethodInvoker)(() => button_import.Enabled = true));
                    }
                    else
                    {
                        Invoke((MethodInvoker)(() => button_import.Enabled = false));
                    }
                }
                catch (FormatException _ex)
                {
                    MessageBox.Show(_ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch (Exception _ex)
                {
                    MessageBox.Show(_ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                //reactivate buttons after work is done
                Invoke((MethodInvoker)(() => button_validate.Enabled = true));
                Invoke((MethodInvoker)(() => button_clear.Enabled = true));
                Invoke((MethodInvoker)(() => button_projectSelectFile.Enabled = true));
            }
        }

        #endregion

        #region Helper methods

        private void HandleApiResponse(DefaultApiResponse defaultResponse, DataGridViewRow row, BusinessRulesApiResponse businessRulesResponse)
        {
            if (defaultResponse != null)
            {
                if (defaultResponse.Code == 200)
                {
                    Invoke((MethodInvoker)(() => row.DefaultCellStyle.BackColor = Color.LimeGreen));
                    Invoke((MethodInvoker)(() => textBox_projectImportMessages.AppendText(Environment.NewLine)));
                    Invoke((MethodInvoker)(() => textBox_projectImportMessages.AppendText("Row " + (row.Index + 1) + " - " + defaultResponse.Message)));
                }
                else if (defaultResponse.Code == 401)
                {
                    Invoke((MethodInvoker)(() => row.DefaultCellStyle.BackColor = Color.Red));
                    Invoke((MethodInvoker)(() => textBox_projectImportMessages.AppendText(Environment.NewLine)));
                    Invoke((MethodInvoker)(() => textBox_projectImportMessages.AppendText("Row " + (row.Index + 1) + " - " + defaultResponse.Message)));
                    _errorRowCount++;
                    _isRowValid = false;
                    //return to login page if token has expired
                    RedirectToLoginPage();
                }
                else if (defaultResponse.Code == 201)
                {
                    Invoke((MethodInvoker)(() => row.DefaultCellStyle.BackColor = Color.Red));
                    Invoke((MethodInvoker)(() => textBox_projectImportMessages.AppendText(Environment.NewLine)));
                    Invoke((MethodInvoker)(() => textBox_projectImportMessages.AppendText("Row " + (row.Index + 1)
                       + " - " + defaultResponse.Message + " Details: " + string.Join("  ", defaultResponse.Details))));
                    _errorRowCount++;
                    _isRowValid = false;
                }
                else if (defaultResponse.Code == 500)
                {
                    Invoke((MethodInvoker)(() => row.DefaultCellStyle.BackColor = Color.Red));
                    Invoke((MethodInvoker)(() => textBox_projectImportMessages.AppendText(Environment.NewLine)));
                    Invoke((MethodInvoker)(() => textBox_projectImportMessages.AppendText("Row " + (row.Index + 1) + " - " + defaultResponse.Message)));
                    _errorRowCount++;
                    _isRowValid = false;
                }
            }
            else
            {
                if (businessRulesResponse.Code == 102)
                {
                    Invoke((MethodInvoker)(() => row.DefaultCellStyle.BackColor = Color.Red));
                    Invoke((MethodInvoker)(() => textBox_projectImportMessages.AppendText(Environment.NewLine)));
                    Invoke((MethodInvoker)(() => textBox_projectImportMessages.AppendText("Row " + (row.Index + 1)
                       + " - " + businessRulesResponse.Message + " Details: "
                       + string.Join(".  ", businessRulesResponse.Details.Select(x => x.Message)))));
                    _errorRowCount++;
                    _isRowValid = false;
                }
            }
        }

        private void RedirectToLoginPage()
        {
            MessageBox.Show("Authentication token has expired. You will be redirected to the Login page to login again.",
                "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            WorkerFetcher.CancelAsync();
            Invoke((MethodInvoker)(() => Login.MainForm.Hide()));
            Invoke((MethodInvoker)(() => Program.LoginForm.Show()));
        }

        private void AddFileColumnHeaderToComboBox(object[] fileColumnHeaderArray)
        {
            comboBox_projectName.Items.AddRange(fileColumnHeaderArray);
            comboBox_projectCustomerID.Items.AddRange(fileColumnHeaderArray);
            comboBox_projectManagerID.Items.AddRange(fileColumnHeaderArray);
            comboBox_projectTemplateID.Items.AddRange(fileColumnHeaderArray);
            comboBox_projectCurrencyID.Items.AddRange(fileColumnHeaderArray);
            comboBox_projectLegalEntityID.Items.AddRange(fileColumnHeaderArray);
            comboBox_projectNo.Items.AddRange(fileColumnHeaderArray);
            comboBox_description.Items.AddRange(fileColumnHeaderArray);
            comboBox_projectStartDate.Items.AddRange(fileColumnHeaderArray);
            comboBox_projectEndDate.Items.AddRange(fileColumnHeaderArray);
            comboBox_projectTypeID.Items.AddRange(fileColumnHeaderArray);
            comboBox_projectCategoryID.Items.AddRange(fileColumnHeaderArray);
        }

        private void MapFileContentToTable(int tableColumnIndex, int fileColumnIndex)
        {
            for (int i = 0; i < _fileContent.Rows.Count; i++)
            {
                Invoke((MethodInvoker)(() => _projectTable.Rows[i][tableColumnIndex] = _fileContent.Rows[i][fileColumnIndex]));
            }

            dataGridView_project.Rows[0].Cells[tableColumnIndex].Selected = true;
            dataGridView_project.FirstDisplayedScrollingColumnIndex = tableColumnIndex;
            dataGridView_project.Focus();
        }

        private void MapDefaultValueToTable(int tableColumnIndex, string defaultValue)
        {
            for (int i = 0; i < _projectTable.Rows.Count; i++)
            {
                Invoke((MethodInvoker)(() => _projectTable.Rows[i][tableColumnIndex] = defaultValue));
            }

            dataGridView_project.Rows[0].Cells[tableColumnIndex].Selected = true;
            dataGridView_project.FirstDisplayedScrollingColumnIndex = tableColumnIndex;
            dataGridView_project.Focus();
        }

        private void CheckAndAddColumn(string columnName)
        {
            if (!_projectTable.Columns.Contains(columnName))
            {
                _projectTable.Columns.Add(columnName);
            }
        }

        private void CheckCellsForNullOrEmpty(int columnIndex)
        {
            foreach (DataGridViewRow _row in dataGridView_project.Rows)
            {
                if (_row.Cells[columnIndex].Value == null || string.IsNullOrEmpty(_row.Cells[columnIndex].Value.ToString()))
                {
                    if (_row.DataBoundItem != null)
                    {
                        _row.Cells[columnIndex].Style.BackColor = Color.Red;
                    }
                }
            }
        }

        private void ClearColumn(int columnIndex)
        {
            if (dataGridView_project != null && dataGridView_project.Columns.Count - 1 >= columnIndex)
            {
                var _tmpCol = dataGridView_project.Columns[columnIndex];
                dataGridView_project.Columns.Remove(dataGridView_project.Columns[columnIndex]);
                dataGridView_project.Columns.Insert(columnIndex, _tmpCol);
            }
        }

        private void ClearRow(int tableColumnIndex)
        {
            for (int i = 0; i < _projectTable.Rows.Count; i++)
            {
                Invoke((MethodInvoker)(() => _projectTable.Rows[i][tableColumnIndex] = ""));
            }
        }

        private void ClearAndResetAllComboBoxes()
        {
            comboBox_projectName.ResetText();
            comboBox_projectName.Items.Clear();
            comboBox_projectCustomerID.ResetText();
            comboBox_projectCustomerID.Items.Clear();
            comboBox_projectTemplateID.ResetText();
            comboBox_projectTemplateID.Items.Clear();
            comboBox_projectManagerID.ResetText();
            comboBox_projectManagerID.Items.Clear();
            comboBox_projectCurrencyID.ResetText();
            comboBox_projectCurrencyID.Items.Clear();
            comboBox_projectLegalEntityID.ResetText();
            comboBox_projectLegalEntityID.Items.Clear();
            comboBox_projectNo.ResetText();
            comboBox_projectNo.Items.Clear();
            comboBox_description.ResetText();
            comboBox_description.Items.Clear();
            comboBox_projectStartDate.ResetText();
            comboBox_projectStartDate.Items.Clear();
            comboBox_projectEndDate.ResetText();
            comboBox_projectEndDate.Items.Clear();
            comboBox_projectTypeID.ResetText();
            comboBox_projectTypeID.Items.Clear();
            comboBox_projectCategoryID.ResetText();
            comboBox_projectCategoryID.Items.Clear();
        }

        private void ClearAndResetAllCheckBoxes()
        {
            checkBox_defaultProjectTemplateID.Checked = false;
            checkBox_defaultCurrencyID.Checked = false;
            checkBox_defaultLegalEntityID.Checked = false;
            checkBox_defaultProjectTypeID.Checked = false;
            checkBox_defaultProjectCategoryID.Checked = false;
        }

        #endregion

        #region Get default values from API

        private void GetAllProjectTemplateFromApi()
        {
            var _apiResponse = ProjectHandler.Instance.GetAllProjectTemplate(AuthenticationHandler.Instance.Token);

            if (_apiResponse != null)
            {
                _projectTemplateIDList = new List<KeyValuePair<int, string>>();

                foreach (var _projectTemplate in _apiResponse)
                {
                    _projectTemplateIDList.Add(new KeyValuePair<int, string>(_projectTemplate.ProjectTemplateID, _projectTemplate.ProjectTemplateName));
                }
            }
        }

        private void GetAllCurrencyFromApi()
        {
            var _apiResponse = ProjectHandler.Instance.GetAllCurrency(AuthenticationHandler.Instance.Token);

            if (_apiResponse != null)
            {
                _currencyIDList = new List<KeyValuePair<int, string>>();

                foreach (var _currency in _apiResponse)
                {
                    _currencyIDList.Add(new KeyValuePair<int, string>(_currency.CurrencyID, _currency.DescriptiveName));
                }
            }
        }

        private void GetAllLegalEntityFromApi()
        {
            var _apiResponse = ProjectHandler.Instance.GetAllLegalEntity(AuthenticationHandler.Instance.Token);

            if (_apiResponse != null)
            {
                _legalEntityIDList = new List<KeyValuePair<int, string>>();

                foreach (var _legalEntity in _apiResponse)
                {
                    _legalEntityIDList.Add(new KeyValuePair<int, string>(_legalEntity.LegalEntityID, _legalEntity.Name));
                }
            }
        }

        private void GetAllProjectTypeFromApi()
        {
            var _apiResponse = ProjectHandler.Instance.GetAllProjectType(AuthenticationHandler.Instance.Token);

            if (_apiResponse != null)
            {
                _projectTypeIDList = new List<KeyValuePair<int, string>>();

                foreach (var _projectType in _apiResponse)
                {
                    _projectTypeIDList.Add(new KeyValuePair<int, string>(_projectType.ProjectTypeID, _projectType.Name));
                }
            }
        }

        private void GetAllProjectCategoryFromApi()
        {
            var _apiResponse = ProjectHandler.Instance.GetAllProjectCategory(AuthenticationHandler.Instance.Token);

            if (_apiResponse != null)
            {
                _projectCategoryIDList = new List<KeyValuePair<int, string>>();

                foreach (var _projectCategory in _apiResponse)
                {
                    _projectCategoryIDList.Add(new KeyValuePair<int, string>(_projectCategory.ProjectCategoryID, _projectCategory.Name));
                }
            }
        }

        #endregion

        #region Add default key value pair list to Combobox

        private void AddKeyValuePairListToProjectTemplateIDComboBox()
        {
            comboBox_projectTemplateID.DisplayMember = "Value";
            comboBox_projectTemplateID.ValueMember = "Key";

            if (_projectTemplateIDList != null)
            {
                foreach (var _projectTemplate in _projectTemplateIDList)
                {
                    comboBox_projectTemplateID.Items.Add(new { _projectTemplate.Key, _projectTemplate.Value });
                }
            }
        }

        private void AddKeyValuePairListToCurrencyIDComboBox()
        {
            comboBox_projectCurrencyID.DisplayMember = "Value";
            comboBox_projectCurrencyID.ValueMember = "Key";

            if (_currencyIDList != null)
            {
                foreach (var _currency in _currencyIDList)
                {
                    comboBox_projectCurrencyID.Items.Add(new { _currency.Key, _currency.Value });
                }
            }
        }

        private void AddKeyValuePairListToLegalEntityIDComboBox()
        {
            comboBox_projectLegalEntityID.DisplayMember = "Value";
            comboBox_projectLegalEntityID.ValueMember = "Key";

            if (_legalEntityIDList != null)
            {
                foreach (var _legalEntity in _legalEntityIDList)
                {
                    comboBox_projectLegalEntityID.Items.Add(new { _legalEntity.Key, _legalEntity.Value });
                }
            }
        }

        private void AddKeyValuePairListToProjectTypeIDComboBox()
        {
            comboBox_projectTypeID.DisplayMember = "Value";
            comboBox_projectTypeID.ValueMember = "Key";

            if (_projectTypeIDList != null)
            {
                foreach (var _projectType in _projectTypeIDList)
                {
                    comboBox_projectTypeID.Items.Add(new { _projectType.Key, _projectType.Value });
                }
            }
        }

        private void AddKeyValuePairListToProjectCategoryIDComboBox()
        {
            comboBox_projectCategoryID.DisplayMember = "Value";
            comboBox_projectCategoryID.ValueMember = "Key";

            if (_projectCategoryIDList != null)
            {
                foreach (var _projectCategory in _projectCategoryIDList)
                {
                    comboBox_projectCategoryID.Items.Add(new { _projectCategory.Key, _projectCategory.Value });
                }
            }
        }

        #endregion

        #region Combobox implementations

        private void comboBox_projectName_SelectedIndexChanged(object sender, EventArgs e)
        {
            var _columnIndex = _fileContent.Columns.IndexOf(comboBox_projectName.SelectedItem.ToString());

            var _tableColumnIndex = _projectTable.Columns.IndexOf(_projectName);

            ClearColumn(_tableColumnIndex);

            MapFileContentToTable(_tableColumnIndex, _columnIndex);

            CheckCellsForNullOrEmpty(_tableColumnIndex);
        }

        private void comboBox_projectCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            var _columnIndex = _fileContent.Columns.IndexOf(comboBox_projectCustomerID.SelectedItem.ToString());

            var _tableColumnIndex = _projectTable.Columns.IndexOf(_customerID);

            ClearColumn(_tableColumnIndex);

            MapFileContentToTable(_tableColumnIndex, _columnIndex);

            CheckCellsForNullOrEmpty(_tableColumnIndex);
        }

        private void comboBox_projectTemplate_SelectedIndexChanged(object sender, EventArgs e)
        {
            var _tableColumnIndex = _projectTable.Columns.IndexOf(_projectTemplateID);

            ClearColumn(_tableColumnIndex);

            if (!checkBox_defaultProjectTemplateID.Checked)
            {
                var _columnIndex = _fileContent.Columns.IndexOf(comboBox_projectTemplateID.SelectedItem.ToString());

                MapFileContentToTable(_tableColumnIndex, _columnIndex);
            }
            else
            {
                var _defaultValue = (comboBox_projectTemplateID.SelectedItem as dynamic).Key.ToString();

                MapDefaultValueToTable(_tableColumnIndex, _defaultValue);
            }

            CheckCellsForNullOrEmpty(_tableColumnIndex);
        }

        private void comboBox_projectManager_SelectedIndexChanged(object sender, EventArgs e)
        {
            var _columnIndex = _fileContent.Columns.IndexOf(comboBox_projectManagerID.SelectedItem.ToString());

            var _tableColumnIndex = _projectTable.Columns.IndexOf(_projectManagerID);

            ClearColumn(_tableColumnIndex);

            MapFileContentToTable(_tableColumnIndex, _columnIndex);

            CheckCellsForNullOrEmpty(_tableColumnIndex);
        }

        private void comboBox_projectCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {
            var _tableColumnIndex = _projectTable.Columns.IndexOf(_currencyID);

            ClearColumn(_tableColumnIndex);

            if (!checkBox_defaultCurrencyID.Checked)
            {
                var _columnIndex = _fileContent.Columns.IndexOf(comboBox_projectCurrencyID.SelectedItem.ToString());

                MapFileContentToTable(_tableColumnIndex, _columnIndex);
            }
            else
            {
                var _defaultValue = (comboBox_projectCurrencyID.SelectedItem as dynamic).Key.ToString();

                MapDefaultValueToTable(_tableColumnIndex, _defaultValue);
            }

            CheckCellsForNullOrEmpty(_tableColumnIndex);
        }

        private void comboBox_projectLegalEntity_SelectedIndexChanged(object sender, EventArgs e)
        {
            var _tableColumnIndex = _projectTable.Columns.IndexOf(_legalEntityID);

            ClearColumn(_tableColumnIndex);

            if (!checkBox_defaultLegalEntityID.Checked)
            {
                var _columnIndex = _fileContent.Columns.IndexOf(comboBox_projectLegalEntityID.SelectedItem.ToString());

                MapFileContentToTable(_tableColumnIndex, _columnIndex);
            }
            else
            {
                var _defaultValue = (comboBox_projectLegalEntityID.SelectedItem as dynamic).Key.ToString();

                MapDefaultValueToTable(_tableColumnIndex, _defaultValue);
            }

            CheckCellsForNullOrEmpty(_tableColumnIndex);
        }

        private void comboBox_projectNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            var _columnIndex = _fileContent.Columns.IndexOf(comboBox_projectNo.SelectedItem.ToString());

            CheckAndAddColumn(_projectNo);

            var _tableColumnIndex = _projectTable.Columns.IndexOf(_projectNo);

            ClearColumn(_tableColumnIndex);

            MapFileContentToTable(_tableColumnIndex, _columnIndex);

            CheckCellsForNullOrEmpty(_tableColumnIndex);
        }

        private void comboBox_description_SelectedIndexChanged(object sender, EventArgs e)
        {
            var _columnIndex = _fileContent.Columns.IndexOf(comboBox_description.SelectedItem.ToString());

            CheckAndAddColumn(_description);

            var _tableColumnIndex = _projectTable.Columns.IndexOf(_description);

            ClearColumn(_tableColumnIndex);

            MapFileContentToTable(_tableColumnIndex, _columnIndex);

            CheckCellsForNullOrEmpty(_tableColumnIndex);
        }

        private void comboBox_projectStartDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            var _columnIndex = _fileContent.Columns.IndexOf(comboBox_projectStartDate.SelectedItem.ToString());

            CheckAndAddColumn(_projectStartDate);

            var _tableColumnIndex = _projectTable.Columns.IndexOf(_projectStartDate);

            ClearColumn(_tableColumnIndex);

            MapFileContentToTable(_tableColumnIndex, _columnIndex);

            CheckCellsForNullOrEmpty(_tableColumnIndex);
        }

        private void comboBox_projectEndDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            var _columnIndex = _fileContent.Columns.IndexOf(comboBox_projectEndDate.SelectedItem.ToString());

            CheckAndAddColumn(_projectEndDate);

            var _tableColumnIndex = _projectTable.Columns.IndexOf(_projectEndDate);

            ClearColumn(_tableColumnIndex);

            MapFileContentToTable(_tableColumnIndex, _columnIndex);

            CheckCellsForNullOrEmpty(_tableColumnIndex);
        }

        private void comboBox_projectTypeID_SelectedIndexChanged(object sender, EventArgs e)
        {
            var _tableColumnIndex = _projectTable.Columns.IndexOf(_projectTypeID);

            ClearColumn(_tableColumnIndex);

            if (!checkBox_defaultProjectTypeID.Checked)
            {
                var _columnIndex = _fileContent.Columns.IndexOf(comboBox_projectTypeID.SelectedItem.ToString());

                MapFileContentToTable(_tableColumnIndex, _columnIndex);
            }
            else
            {
                var _defaultValue = (comboBox_projectTypeID.SelectedItem as dynamic).Key.ToString();

                MapDefaultValueToTable(_tableColumnIndex, _defaultValue);
            }

            CheckCellsForNullOrEmpty(_tableColumnIndex);
        }

        private void comboBox_projectCategoryID_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckAndAddColumn(_projectCategoryID);

            var _tableColumnIndex = _projectTable.Columns.IndexOf(_projectCategoryID);

            ClearColumn(_tableColumnIndex);

            if (!checkBox_defaultProjectCategoryID.Checked)
            {
                var _columnIndex = _fileContent.Columns.IndexOf(comboBox_projectCategoryID.SelectedItem.ToString());

                MapFileContentToTable(_tableColumnIndex, _columnIndex);
            }
            else
            {
                var _defaultValue = (comboBox_projectCategoryID.SelectedItem as dynamic).Key.ToString();

                MapDefaultValueToTable(_tableColumnIndex, _defaultValue);
            }

            CheckCellsForNullOrEmpty(_tableColumnIndex);
        }

        #endregion

        #region Checkbox implementations

        private void checkBox_defaultCurrencyID_CheckedChanged(object sender, EventArgs e)
        {
            comboBox_projectCurrencyID.ResetText();
            comboBox_projectCurrencyID.Items.Clear();

            if (checkBox_defaultCurrencyID.Checked)
            {
                if (_currencyIDList == null)
                {
                    GetAllCurrencyFromApi();
                }

                AddKeyValuePairListToCurrencyIDComboBox();
            }
            else
            {
                comboBox_projectCurrencyID.Items.AddRange(ProjectHandler.Instance.FileColumnHeaders.Cast<object>().ToArray());
            }

            var _tableColumnIndex = _projectTable.Columns.IndexOf(_currencyID);

            if (_tableColumnIndex != -1)
            {
                ClearColumn(_tableColumnIndex);

                ClearRow(_tableColumnIndex);

                CheckCellsForNullOrEmpty(_tableColumnIndex);
            }
        }

        private void checkBox_defaultProjectTemplateID_CheckedChanged(object sender, EventArgs e)
        {
            comboBox_projectTemplateID.ResetText();
            comboBox_projectTemplateID.Items.Clear();

            if (checkBox_defaultProjectTemplateID.Checked)
            {
                if (_projectTemplateIDList == null)
                {
                    GetAllProjectTemplateFromApi();
                }

                AddKeyValuePairListToProjectTemplateIDComboBox();
            }
            else
            {
                comboBox_projectTemplateID.Items.AddRange(ProjectHandler.Instance.FileColumnHeaders.Cast<object>().ToArray());
            }

            var _tableColumnIndex = _projectTable.Columns.IndexOf(_projectTemplateID);

            if (_tableColumnIndex != -1)
            {
                ClearColumn(_tableColumnIndex);

                ClearRow(_tableColumnIndex);

                CheckCellsForNullOrEmpty(_tableColumnIndex);
            }
        }

        private void checkBox_defaultLegalEntityID_CheckedChanged(object sender, EventArgs e)
        {
            comboBox_projectLegalEntityID.ResetText();
            comboBox_projectLegalEntityID.Items.Clear();

            if (checkBox_defaultLegalEntityID.Checked)
            {
                if (_legalEntityIDList == null)
                {
                    GetAllLegalEntityFromApi();
                }

                AddKeyValuePairListToLegalEntityIDComboBox();
            }
            else
            {
                comboBox_projectLegalEntityID.Items.AddRange(ProjectHandler.Instance.FileColumnHeaders.Cast<object>().ToArray());
            }

            var _tableColumnIndex = _projectTable.Columns.IndexOf(_legalEntityID);

            if (_tableColumnIndex != -1)
            {
                ClearColumn(_tableColumnIndex);

                ClearRow(_tableColumnIndex);

                CheckCellsForNullOrEmpty(_tableColumnIndex);
            }
        }

        private void checkBox_defaultProjectTypeID_CheckedChanged(object sender, EventArgs e)
        {
            comboBox_projectTypeID.ResetText();
            comboBox_projectTypeID.Items.Clear();

            if (checkBox_defaultProjectTypeID.Checked)
            {
                if (_projectTypeIDList == null)
                {
                    GetAllProjectTypeFromApi();
                }

                AddKeyValuePairListToProjectTypeIDComboBox();
            }
            else
            {
                comboBox_projectTypeID.Items.AddRange(ProjectHandler.Instance.FileColumnHeaders.Cast<object>().ToArray());
            }

            var _tableColumnIndex = _projectTable.Columns.IndexOf(_projectTypeID);

            if (_tableColumnIndex != -1)
            {
                ClearColumn(_tableColumnIndex);

                ClearRow(_tableColumnIndex);

                CheckCellsForNullOrEmpty(_tableColumnIndex);
            }
        }

        private void checkBox_defaultProjectCategoryID_CheckedChanged(object sender, EventArgs e)
        {
            comboBox_projectCategoryID.ResetText();
            comboBox_projectCategoryID.Items.Clear();

            if (checkBox_defaultProjectCategoryID.Checked)
            {
                if (_projectCategoryIDList == null)
                {
                    GetAllProjectCategoryFromApi();
                }

                AddKeyValuePairListToProjectCategoryIDComboBox();
            }
            else
            {
                comboBox_projectCategoryID.Items.AddRange(ProjectHandler.Instance.FileColumnHeaders.Cast<object>().ToArray());
            }

            var _tableColumnIndex = _projectTable.Columns.IndexOf(_projectCategoryID);

            if (_tableColumnIndex != -1)
            {
                ClearColumn(_tableColumnIndex);

                ClearRow(_tableColumnIndex);

                CheckCellsForNullOrEmpty(_tableColumnIndex);
            }
        }

        #endregion
    }
}