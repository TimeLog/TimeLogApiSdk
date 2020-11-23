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
    public partial class UserControl_CustomerImport : UserControl
    {
        #region Variable declarations

        private DataTable _customerTable;
        private DataTable _fileContent;
        private Button _senderButton;
        private bool _isRowValid = true;
        private int _errorRowCount;

        private static readonly Dictionary<int, string> MandatoryFields = new Dictionary<int, string>
        {
            {0, "Customer Name"},
            {1, "Currency ID"},
            {2, "Customer Status ID"},
            {3, "Country ID"}
        };

        //all column header variables
        private readonly string _customerName = "Customer Name";
        private readonly string _currencyID = "Currency ID";
        private readonly string _customerStatusID = "Customer Status ID";
        private readonly string _countryID = "Country ID";
        private readonly string _customerNo = "Customer No";
        private readonly string _nickname = "Nickname";
        private readonly string _primaryKAMID = "Primary KAM ID";
        private readonly string _secondaryKAMID = "Secondary KAM ID";
        private readonly string _customerSince = "Customer Since";
        private readonly string _industryID = "Industry ID";
        private readonly string _phoneNo = "Phone No";
        private readonly string _faxNo = "Fax No";
        private readonly string _email = "Email";
        private readonly string _website = "Website";
        private readonly string _address = "Address";
        private readonly string _address2 = "Address 2";
        private readonly string _address3 = "Address 3";
        private readonly string _zipCode = "Zip Code";
        private readonly string _city = "City";
        private readonly string _state = "State";
        private readonly string _useEanNo = "Use Ean No";
        private readonly string _eanNo = "Ean No";
        private readonly string _organizationNo = "Organization No";
        private readonly string _VATNo = "VAT No";
        private readonly string _useInvoicingAddress = "Use Invoicing Address";
        private readonly string _invoicingAddress = "Invoicing Address";
        private readonly string _invoicingAddress2 = "Invoicing Address 2";
        private readonly string _invoicingAddress3 = "Invoicing Address 3";
        private readonly string _invoicingAddressZipCode = "Invoicing Address Zip Code";
        private readonly string _invoicingAddressCity = "Invoicing Address City";
        private readonly string _invoicingAddressState = "Invoicing Address State";
        private readonly string _invoicingAddressCountryID = "Invoicing Address Country ID";
        private readonly string _defaultMileageDistance = "Default Mileage Distance";
        private readonly string _expenseIsBillable = "Expense Is Billable";
        private readonly string _mileageIsBillable = "Mileage Is Billable";
        private readonly string _defaultDistIsMaxBillable = "Default Dist Is Max Billable";
        private readonly string _contactID = "Contact ID";
        private readonly string _invoiceAddressToUse = "Invoice Address To Use";
        private readonly string _internalReferenceID = "Internal Reference ID";
        private readonly string _customerReferenceID = "Customer Reference ID";
        private readonly string _paymentTermID = "Payment Term ID";
        private readonly string _discountPercentage = "Discount Percentage";
        private readonly string _calculateVAT = "Calculate VAT";
        private readonly string _VATPercentage = "VAT Percentage";

        //default value lists
        private static readonly List<string> ExpenseIsBillableList = new List<string> {"true", "false"};
        private static readonly List<string> MileageIsBillableList = new List<string> {"true", "false"};
        private List<string> _VATPercentageList;

        //default value lists from API 
        private List<KeyValuePair<int, string>> _currencyIDList;
        private List<KeyValuePair<int, string>> _countryIDList;
        private List<KeyValuePair<int, string>> _customerStatusIDList;
        private List<KeyValuePair<int, string>> _primaryKAMIDList;
        private List<KeyValuePair<int, string>> _secondaryKAMIDList;
        private List<KeyValuePair<int, string>> _industryIDList;
        private List<KeyValuePair<int, string>> _paymentTermIDList; // not yet added, to be implemented

        //expanding panels' current states, expand panels, expand buttons
        private BaseHandler.ExpandState[] _expandStates;
        private Panel[] _expandPanels;
        private Button[] _expandButtons;

        //set the number of pixels expanded per timer Tick
        private const int ExpansionPerTick = 7;

        #endregion

        public UserControl_CustomerImport()
        {
            InitializeComponent();
            InitializeDelimiterComboBox();
            InitializeExpandCollapsePanels();
            AddRowNumberToDataTable();
            InitializeCustomerDataTable();
            dataGridView_customer.DataSource = _customerTable;
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
                BaseHandler.ExpandState.Expanded,
                BaseHandler.ExpandState.Expanded,
                BaseHandler.ExpandState.Expanded,
                BaseHandler.ExpandState.Expanded,
                BaseHandler.ExpandState.Expanded,
                BaseHandler.ExpandState.Expanded,
            };
            _expandPanels = new[]
            {
                panel_customerDetails,
                panel_contactDetails,
                panel_invoiceAddress,
                panel_financeCompanyInfo,
                panel_defaultInvoiceSettings,
                panel_incoiceExternalCosts
            };
            _expandButtons = new[]
            {
                button_customerDetails,
                button_contactDetails,
                button_invoiceAddress,
                button_financeCompanyInfo,
                button_defaultInvoiceSettings,
                button_invoiceExternalCosts
            };

            for (int i = 0; i < _expandButtons.Length; i++)
            {
                _expandButtons[i].Tag = i;
                _expandButtons[i].BackgroundImage = Properties.Resources.upload;
            }
        }

        private void InitializeDelimiterComboBox()
        {
            comboBox_delimiter.Items.AddRange(CustomerHandler.Instance.GetDelimiterList().Cast<object>().ToArray());
            comboBox_delimiter.SelectedIndex = 0;
        }

        private void InitializeCustomerDataTable()
        {
            _customerTable = new DataTable();

            foreach (var _mandatoryField in MandatoryFields)
            {
                _customerTable.Columns.Add(_mandatoryField.Value);
            }
        }

        #endregion

        #region Functionalities implementations

        private void button_select_customer_file_Click(object sender, EventArgs e)
        {
            CustomerHandler.Instance.FileColumnHeaders = new List<string>();
            _fileContent = new DataTable();
            _fileContent = CustomerHandler.Instance.GetFileContent(comboBox_delimiter.SelectedItem.ToString());

            if (_fileContent != null)
            {
                textBox_customerImportMessages.Text = string.Empty;
                ClearAndResetAllCheckBoxes();
                ClearAndResetAllComboBoxes();
                Invoke((MethodInvoker) (() => button_import.Enabled = false));

                if (dataGridView_customer.RowCount > 1)
                {
                    dataGridView_customer.DataSource = null;
                    InitializeCustomerDataTable();
                    dataGridView_customer.DataSource = _customerTable;
                }

                foreach (DataRow _fileContentRow in _fileContent.Rows)
                {
                    _customerTable.Rows.Add();
                }

                AddFileColumnHeaderToComboBox(CustomerHandler.Instance.FileColumnHeaders.Cast<object>().ToArray());
            }
            else
            {
                button_clear_Click(sender, e);
            }
        }

        private void button_validate_Click(object sender, EventArgs e)
        {
            textBox_customerImportMessages.Text = string.Empty;
            _senderButton = (Button) sender;
            WorkerFetcher.RunWorkerAsync();
        }

        private void button_stop_Click(object sender, EventArgs e)
        {
            WorkerFetcher.CancelAsync();
        }

        private void button_import_Click(object sender, EventArgs e)
        {
            textBox_customerImportMessages.Text = string.Empty;
            _senderButton = (Button) sender;
            WorkerFetcher.RunWorkerAsync();
        }

        private void button_clear_Click(object sender, EventArgs e)
        {
            CustomerHandler.Instance.FileColumnHeaders = new List<string>();
            textBox_customerImportMessages.Text = string.Empty;
            ClearAndResetAllCheckBoxes();
            ClearAndResetAllComboBoxes();
            Invoke((MethodInvoker) (() => button_import.Enabled = false));

            dataGridView_customer.DataSource = null;
            InitializeCustomerDataTable();
            dataGridView_customer.DataSource = _customerTable;
        }

        private void textBox_customerImportMessages_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var _position = textBox_customerImportMessages.GetCharIndexFromPosition(e.Location);
                var _lineNo = textBox_customerImportMessages.GetLineFromCharIndex(_position) - 1;

                for (int i = 0; i < dataGridView_customer.Rows.Count - 1; i++)
                {
                    if (i == _lineNo)
                    {
                        Invoke((MethodInvoker)(() => dataGridView_customer.Rows[i].Selected = true));
                        dataGridView_customer.FirstDisplayedScrollingRowIndex = i;
                        dataGridView_customer.Focus();
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
            if (dataGridView_customer != null && dataGridView_customer.RowCount > 1)
            {
                _isRowValid = true;
                _errorRowCount = 0;

                //while validating, deactivate other buttons
                Invoke((MethodInvoker)(() => button_validate.Enabled = false));
                Invoke((MethodInvoker)(() => button_import.Enabled = false));
                Invoke((MethodInvoker)(() => button_clear.Enabled = false));
                Invoke((MethodInvoker)(() => button_customerSelectFile.Enabled = false));

                try
                {
                    foreach (DataGridViewRow _row in dataGridView_customer.Rows)
                    {
                        if (WorkerFetcher.CancellationPending)
                        {
                            break;
                        }

                        if (_row.DataBoundItem != null)
                        {
                            CustomerCreateModel _newCustomer = new CustomerCreateModel
                            {
                                Name = (dataGridView_customer.Columns[_customerName] != null)
                                    ? CustomerHandler.Instance.CheckAndGetString(_row.Cells[dataGridView_customer.Columns[_customerName].Index].Value) : string.Empty,
                                CurrencyID = (dataGridView_customer.Columns[_currencyID] != null)
                                    ? CustomerHandler.Instance.CheckAndGetInteger(_currencyID, _row.Cells[dataGridView_customer.Columns[_currencyID].Index].Value) : 0,
                                CustomerStatusID = (dataGridView_customer.Columns[_customerStatusID] != null)
                                    ? CustomerHandler.Instance.CheckAndGetInteger(_customerStatusID, _row.Cells[dataGridView_customer.Columns[_customerStatusID].Index].Value) : 0,
                                CountryID = (dataGridView_customer.Columns[_countryID] != null)
                                    ? CustomerHandler.Instance.CheckAndGetInteger(_countryID, _row.Cells[dataGridView_customer.Columns[_countryID].Index].Value) : 0,
                                CustomerNo = (dataGridView_customer.Columns[_customerNo] != null)
                                    ? CustomerHandler.Instance.CheckAndGetString(_row.Cells[dataGridView_customer.Columns[_customerNo].Index].Value) : string.Empty,
                                NickName = (dataGridView_customer.Columns[_nickname] != null)
                                    ? CustomerHandler.Instance.CheckAndGetString(_row.Cells[dataGridView_customer.Columns[_nickname].Index].Value) : string.Empty,
                                PrimaryKAMID = (dataGridView_customer.Columns[_primaryKAMID] != null)
                                    ? CustomerHandler.Instance.CheckAndGetNullableInteger(_primaryKAMID, _row.Cells[dataGridView_customer.Columns[_primaryKAMID].Index].Value) : null,
                                SecondaryKAMID = (dataGridView_customer.Columns[_secondaryKAMID] != null)
                                    ? CustomerHandler.Instance.CheckAndGetNullableInteger(_secondaryKAMID, _row.Cells[dataGridView_customer.Columns[_secondaryKAMID].Index].Value) : null,
                                CustomerSince = (dataGridView_customer.Columns[_customerSince] != null)
                                    ? CustomerHandler.Instance.CheckAndGetDate(_customerSince, _row.Cells[dataGridView_customer.Columns[_customerSince].Index].Value) : DateTime.Now,
                                IndustryID = (dataGridView_customer.Columns[_industryID] != null)
                                    ? CustomerHandler.Instance.CheckAndGetNullableInteger(_industryID, _row.Cells[dataGridView_customer.Columns[_industryID].Index].Value) : null,
                                Phone = (dataGridView_customer.Columns[_phoneNo] != null)
                                    ? CustomerHandler.Instance.CheckAndGetString(_row.Cells[dataGridView_customer.Columns[_phoneNo].Index].Value) : string.Empty,
                                Fax = (dataGridView_customer.Columns[_faxNo] != null)
                                    ? CustomerHandler.Instance.CheckAndGetString(_row.Cells[dataGridView_customer.Columns[_faxNo].Index].Value) : string.Empty,
                                Email = (dataGridView_customer.Columns[_email] != null)
                                    ? CustomerHandler.Instance.CheckAndGetString(_row.Cells[dataGridView_customer.Columns[_email].Index].Value) : string.Empty,
                                Website = (dataGridView_customer.Columns[_website] != null)
                                    ? CustomerHandler.Instance.CheckAndGetString(_row.Cells[dataGridView_customer.Columns[_website].Index].Value) : string.Empty,
                                Address = (dataGridView_customer.Columns[_address] != null)
                                    ? CustomerHandler.Instance.CheckAndGetString(_row.Cells[dataGridView_customer.Columns[_address].Index].Value) : string.Empty,
                                Address2 = (dataGridView_customer.Columns[_address2] != null)
                                    ? CustomerHandler.Instance.CheckAndGetString(_row.Cells[dataGridView_customer.Columns[_address2].Index].Value) : string.Empty,
                                Address3 = (dataGridView_customer.Columns[_address3] != null)
                                    ? CustomerHandler.Instance.CheckAndGetString(_row.Cells[dataGridView_customer.Columns[_address3].Index].Value) : string.Empty,
                                ZipCode = (dataGridView_customer.Columns[_zipCode] != null)
                                    ? CustomerHandler.Instance.CheckAndGetString(_row.Cells[dataGridView_customer.Columns[_zipCode].Index].Value) : string.Empty,
                                City = (dataGridView_customer.Columns[_city] != null)
                                    ? CustomerHandler.Instance.CheckAndGetString(_row.Cells[dataGridView_customer.Columns[_city].Index].Value) : string.Empty,
                                State = (dataGridView_customer.Columns[_state] != null)
                                    ? CustomerHandler.Instance.CheckAndGetString(_row.Cells[dataGridView_customer.Columns[_state].Index].Value) : string.Empty,
                                UseEanNo = (dataGridView_customer.Columns[_useEanNo] != null) &&
                                           CustomerHandler.Instance.CheckAndGetBoolean(_useEanNo, _row.Cells[dataGridView_customer.Columns[_useEanNo].Index].Value),
                                EanNo = (dataGridView_customer.Columns[_eanNo] != null)
                                    ? CustomerHandler.Instance.CheckAndGetString(_row.Cells[dataGridView_customer.Columns[_eanNo].Index].Value) : string.Empty,
                                OrganizationNo = (dataGridView_customer.Columns[_organizationNo] != null)
                                    ? CustomerHandler.Instance.CheckAndGetString(_row.Cells[dataGridView_customer.Columns[_organizationNo].Index].Value) : string.Empty,
                                VatNo = (dataGridView_customer.Columns[_VATNo] != null)
                                    ? CustomerHandler.Instance.CheckAndGetString(_row.Cells[dataGridView_customer.Columns[_VATNo].Index].Value) : string.Empty,
                                UseInvoicingAddress = (dataGridView_customer.Columns[_useInvoicingAddress] != null) &&
                                            CustomerHandler.Instance.CheckAndGetBoolean(_useInvoicingAddress, _row.Cells[dataGridView_customer.Columns[_useInvoicingAddress].Index].Value),
                                InvoicingAddress = (dataGridView_customer.Columns[_invoicingAddress] != null)
                                    ? CustomerHandler.Instance.CheckAndGetString(_row.Cells[dataGridView_customer.Columns[_invoicingAddress].Index].Value) : string.Empty,
                                InvoicingAddress2 = (dataGridView_customer.Columns[_invoicingAddress2] != null)
                                    ? CustomerHandler.Instance.CheckAndGetString(_row.Cells[dataGridView_customer.Columns[_invoicingAddress2].Index].Value) : string.Empty,
                                InvoicingAddress3 = (dataGridView_customer.Columns[_invoicingAddress3] != null)
                                    ? CustomerHandler.Instance.CheckAndGetString(_row.Cells[dataGridView_customer.Columns[_invoicingAddress3].Index].Value) : string.Empty,
                                InvoicingAddressZipCode = (dataGridView_customer.Columns[_invoicingAddressZipCode] != null)
                                    ? CustomerHandler.Instance.CheckAndGetString(_row.Cells[dataGridView_customer.Columns[_invoicingAddressZipCode].Index].Value) : string.Empty,
                                InvoicingAddressCity = (dataGridView_customer.Columns[_invoicingAddressCity] != null)
                                    ? CustomerHandler.Instance.CheckAndGetString(_row.Cells[dataGridView_customer.Columns[_invoicingAddressCity].Index].Value) : string.Empty,
                                InvoicingAddressState = (dataGridView_customer.Columns[_invoicingAddressState] != null)
                                    ? CustomerHandler.Instance.CheckAndGetString(_row.Cells[dataGridView_customer.Columns[_invoicingAddressState].Index].Value) : string.Empty,
                                InvoicingAddressCountryID = (dataGridView_customer.Columns[_invoicingAddressCountryID] != null)
                                    ? CustomerHandler.Instance.CheckAndGetInteger(_invoicingAddressCountryID, _row.Cells[dataGridView_customer.Columns[_invoicingAddressCountryID].Index].Value) : 0,
                                DefaultMileageDistance = (dataGridView_customer.Columns[_defaultMileageDistance] != null)
                                    ? CustomerHandler.Instance.CheckAndGetInteger(_defaultMileageDistance, _row.Cells[dataGridView_customer.Columns[_defaultMileageDistance].Index].Value) : 0,
                                ExpenseIsBillable = (dataGridView_customer.Columns[_expenseIsBillable] != null) &&
                                            CustomerHandler.Instance.CheckAndGetBoolean(_expenseIsBillable, _row.Cells[dataGridView_customer.Columns[_expenseIsBillable].Index].Value),
                                MileageIsBillable = (dataGridView_customer.Columns[_mileageIsBillable] != null) &&
                                            CustomerHandler.Instance.CheckAndGetBoolean(_mileageIsBillable, _row.Cells[dataGridView_customer.Columns[_mileageIsBillable].Index].Value),
                                DefaultDistIsMaxBillable = (dataGridView_customer.Columns[_defaultDistIsMaxBillable] != null) &&
                                            CustomerHandler.Instance.CheckAndGetBoolean(_defaultDistIsMaxBillable, _row.Cells[dataGridView_customer.Columns[_defaultDistIsMaxBillable].Index].Value),
                                ContactID = (dataGridView_customer.Columns[_contactID] != null)
                                    ? CustomerHandler.Instance.CheckAndGetInteger(_contactID, _row.Cells[dataGridView_customer.Columns[_contactID].Index].Value) : 0,
                                InvoiceAddressToUse = (dataGridView_customer.Columns[_invoiceAddressToUse] != null)
                                    ? CustomerHandler.Instance.CheckAndGetInteger(_invoiceAddressToUse, _row.Cells[dataGridView_customer.Columns[_invoiceAddressToUse].Index].Value) : 0,
                                InternalReferenceID = (dataGridView_customer.Columns[_internalReferenceID] != null)
                                    ? CustomerHandler.Instance.CheckAndGetInteger(_internalReferenceID, _row.Cells[dataGridView_customer.Columns[_internalReferenceID].Index].Value) : 0,
                                CustomerReferenceID = (dataGridView_customer.Columns[_customerReferenceID] != null)
                                    ? CustomerHandler.Instance.CheckAndGetInteger(_customerReferenceID, _row.Cells[dataGridView_customer.Columns[_customerReferenceID].Index].Value) : 0,
                                PaymentTermID = (dataGridView_customer.Columns[_paymentTermID] != null)
                                    ? CustomerHandler.Instance.CheckAndGetInteger(_paymentTermID, _row.Cells[dataGridView_customer.Columns[_paymentTermID].Index].Value) : 0,
                                DiscountPercentage = (dataGridView_customer.Columns[_discountPercentage] != null)
                                    ? CustomerHandler.Instance.CheckAndGetDouble(_discountPercentage, _row.Cells[dataGridView_customer.Columns[_discountPercentage].Index].Value) : 0,
                                CalculateVat = (dataGridView_customer.Columns[_calculateVAT] != null) &&
                                            CustomerHandler.Instance.CheckAndGetBoolean(_calculateVAT, _row.Cells[dataGridView_customer.Columns[_calculateVAT].Index].Value),
                                VatPercentage = (dataGridView_customer.Columns[_VATPercentage] != null)
                                    ? CustomerHandler.Instance.CheckAndGetDouble(_VATPercentage, _row.Cells[dataGridView_customer.Columns[_VATPercentage].Index].Value) : 0
                            };

                            DefaultApiResponse _defaultApiResponse;

                            if (_senderButton.Name == button_validate.Name)
                            {
                                _defaultApiResponse = CustomerHandler.Instance.ValidateCustomer(_newCustomer,
                                    AuthenticationHandler.Instance.Token, out var _businessRulesApiResponse);

                                HandleApiResponse(_defaultApiResponse, _row, _businessRulesApiResponse);
                            }
                            else
                            {
                                _defaultApiResponse = CustomerHandler.Instance.ImportCustomer(_newCustomer,
                                    AuthenticationHandler.Instance.Token, out var _businessRulesApiResponse);

                                HandleApiResponse(_defaultApiResponse, _row, _businessRulesApiResponse);

                                _isRowValid = false;
                            }
                        }
                    }

                    //show error row count at the end
                    Invoke((MethodInvoker)(() => textBox_customerImportMessages.AppendText(Environment.NewLine + Environment.NewLine)));
                    Invoke((MethodInvoker)(() => textBox_customerImportMessages.AppendText("Invalid data input row count: " + _errorRowCount)));
                    Invoke((MethodInvoker)(() => textBox_customerImportMessages.AppendText(Environment.NewLine + Environment.NewLine)));

                    //display success message after import / validation is done
                    if (_errorRowCount == 0)
                    {
                        if (_senderButton.Name == button_validate.Name)
                        {
                            Invoke((MethodInvoker)(() => textBox_customerImportMessages.AppendText("Validation completed successfully with no error. You may press the Import button to start importing data right away.")));
                        }
                        else
                        {
                            Invoke((MethodInvoker)(() => textBox_customerImportMessages.AppendText("Data import completed successfully with no error. Excellent!")));
                        }
                    }
                    else
                    {
                        Invoke((MethodInvoker)(() => textBox_customerImportMessages.AppendText("Validation completed successfully with " + _errorRowCount + " error(s). You may modify the invalid input data and then press Validate button again.")));
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
                Invoke((MethodInvoker)(() => button_customerSelectFile.Enabled = true));
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
                    Invoke((MethodInvoker)(() => textBox_customerImportMessages.AppendText(Environment.NewLine)));
                    Invoke((MethodInvoker)(() => textBox_customerImportMessages.AppendText("Row " + (row.Index + 1) + " - " + defaultResponse.Message)));
                }
                else if (defaultResponse.Code == 401)
                {
                    Invoke((MethodInvoker)(() => row.DefaultCellStyle.BackColor = Color.Red));
                    Invoke((MethodInvoker)(() => textBox_customerImportMessages.AppendText(Environment.NewLine)));
                    Invoke((MethodInvoker)(() => textBox_customerImportMessages.AppendText("Row " + (row.Index + 1) + " - " + defaultResponse.Message)));
                    _errorRowCount++;
                    _isRowValid = false;
                    //return to login page if token has expired
                    RedirectToLoginPage();
                }
                else if (defaultResponse.Code == 201)
                {
                    Invoke((MethodInvoker)(() => row.DefaultCellStyle.BackColor = Color.Red));
                    Invoke((MethodInvoker)(() => textBox_customerImportMessages.AppendText(Environment.NewLine)));
                    Invoke((MethodInvoker)(() => textBox_customerImportMessages.AppendText("Row " + (row.Index + 1)
                       + " - " + defaultResponse.Message + " Details: " + string.Join("  ", defaultResponse.Details))));
                    _errorRowCount++;
                    _isRowValid = false;
                }
                else if (defaultResponse.Code == 500)
                {
                    Invoke((MethodInvoker)(() => row.DefaultCellStyle.BackColor = Color.Red));
                    Invoke((MethodInvoker)(() => textBox_customerImportMessages.AppendText(Environment.NewLine)));
                    Invoke((MethodInvoker)(() => textBox_customerImportMessages.AppendText("Row " + (row.Index + 1) + " - " + defaultResponse.Message)));
                    _errorRowCount++;
                    _isRowValid = false;
                }
            }
            else
            {
                if (businessRulesResponse.Code == 102)
                {
                    Invoke((MethodInvoker)(() => row.DefaultCellStyle.BackColor = Color.Red));
                    Invoke((MethodInvoker)(() => textBox_customerImportMessages.AppendText(Environment.NewLine)));
                    Invoke((MethodInvoker)(() => textBox_customerImportMessages.AppendText("Row " + (row.Index + 1)
                       + " - " + businessRulesResponse.Message + " Details: "
                       + string.Join("  ", businessRulesResponse.Details.Select(x => x.Message)))));
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
            comboBox_customerName.Items.AddRange(fileColumnHeaderArray);
            comboBox_currencyID.Items.AddRange(fileColumnHeaderArray);
            comboBox_customerStatusID.Items.AddRange(fileColumnHeaderArray);
            comboBox_countryID.Items.AddRange(fileColumnHeaderArray);
            comboBox_customerNo.Items.AddRange(fileColumnHeaderArray);
            comboBox_nickName.Items.AddRange(fileColumnHeaderArray);
            comboBox_primaryKAMID.Items.AddRange(fileColumnHeaderArray);
            comboBox_secondaryKAMID.Items.AddRange(fileColumnHeaderArray);
            comboBox_customerSince.Items.AddRange(fileColumnHeaderArray);
            comboBox_industryID.Items.AddRange(fileColumnHeaderArray);
            comboBox_phoneNo.Items.AddRange(fileColumnHeaderArray);
            comboBox_faxNo.Items.AddRange(fileColumnHeaderArray);
            comboBox_email.Items.AddRange(fileColumnHeaderArray);
            comboBox_website.Items.AddRange(fileColumnHeaderArray);
            comboBox_address.Items.AddRange(fileColumnHeaderArray);
            comboBox_address2.Items.AddRange(fileColumnHeaderArray);
            comboBox_address3.Items.AddRange(fileColumnHeaderArray);
            comboBox_zipCode.Items.AddRange(fileColumnHeaderArray);
            comboBox_city.Items.AddRange(fileColumnHeaderArray);
            comboBox_state.Items.AddRange(fileColumnHeaderArray);
            comboBox_useEanNo.Items.AddRange(fileColumnHeaderArray);
            comboBox_eanNo.Items.AddRange(fileColumnHeaderArray);
            comboBox_organizationNo.Items.AddRange(fileColumnHeaderArray);
            comboBox_VATNo.Items.AddRange(fileColumnHeaderArray);
            comboBox_useInvoicingAddress.Items.AddRange(fileColumnHeaderArray);
            comboBox_invoicingAddress.Items.AddRange(fileColumnHeaderArray);
            comboBox_invoicingAddress2.Items.AddRange(fileColumnHeaderArray);
            comboBox_invoicingAddress3.Items.AddRange(fileColumnHeaderArray);
            comboBox_invoicingAddressZipCode.Items.AddRange(fileColumnHeaderArray);
            comboBox_invoicingAddressCity.Items.AddRange(fileColumnHeaderArray);
            comboBox_invoicingAddressState.Items.AddRange(fileColumnHeaderArray);
            comboBox_invoicingAddressCountryID.Items.AddRange(fileColumnHeaderArray);
            comboBox_defaultMileageDistance.Items.AddRange(fileColumnHeaderArray);
            comboBox_expenseIsBillable.Items.AddRange(fileColumnHeaderArray);
            comboBox_mileageIsBillable.Items.AddRange(fileColumnHeaderArray);
            comboBox_defaultDistIsMaxBillable.Items.AddRange(fileColumnHeaderArray);
            comboBox_contactID.Items.AddRange(fileColumnHeaderArray);
            comboBox_invoiceAddressToUse.Items.AddRange(fileColumnHeaderArray);
            comboBox_internalReferenceID.Items.AddRange(fileColumnHeaderArray);
            comboBox_customerReferenceID.Items.AddRange(fileColumnHeaderArray);
            comboBox_paymentTermID.Items.AddRange(fileColumnHeaderArray);
            comboBox_discountPercentage.Items.AddRange(fileColumnHeaderArray);
            comboBox_calculateVAT.Items.AddRange(fileColumnHeaderArray);
            comboBox_VATPercentage.Items.AddRange(fileColumnHeaderArray);
        }

        private void MapFileContentToTable(int tableColumnIndex, int fileColumnIndex)
        {
            for (int i = 0; i < _fileContent.Rows.Count; i++)
            {
                Invoke((MethodInvoker) (() => _customerTable.Rows[i][tableColumnIndex] = _fileContent.Rows[i][fileColumnIndex]));
            }

            dataGridView_customer.Rows[0].Cells[tableColumnIndex].Selected = true;
            dataGridView_customer.FirstDisplayedScrollingColumnIndex = tableColumnIndex;
            dataGridView_customer.Focus();
        }

        private void MapDefaultValueToTable(int tableColumnIndex, string defaultValue)
        {
            for (int i = 0; i < _customerTable.Rows.Count; i++)
            {
                Invoke((MethodInvoker) (() => _customerTable.Rows[i][tableColumnIndex] = defaultValue));
            }

            dataGridView_customer.Rows[0].Cells[tableColumnIndex].Selected = true;
            dataGridView_customer.FirstDisplayedScrollingColumnIndex = tableColumnIndex;
            dataGridView_customer.Focus();
        }

        private void CheckAndAddColumn(string columnName)
        {
            if (!_customerTable.Columns.Contains(columnName))
            {
                _customerTable.Columns.Add(columnName);
            }
        }

        private void CheckCellsForNullOrEmpty(int columnIndex)
        {
            foreach (DataGridViewRow _row in dataGridView_customer.Rows)
            {
                if (_row.Cells[columnIndex].Value == null ||
                    string.IsNullOrEmpty(_row.Cells[columnIndex].Value.ToString()))
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
            if (dataGridView_customer != null && dataGridView_customer.Columns.Count - 1 >= columnIndex)
            {
                var _tmpCol = dataGridView_customer.Columns[columnIndex];
                dataGridView_customer.Columns.Remove(dataGridView_customer.Columns[columnIndex]);
                dataGridView_customer.Columns.Insert(columnIndex, _tmpCol);
            }
        }

        private void ClearRow(int tableColumnIndex)
        {
            for (int i = 0; i < _customerTable.Rows.Count; i++)
            {
                Invoke((MethodInvoker) (() => _customerTable.Rows[i][tableColumnIndex] = ""));
            }
        }

        private void ClearAndResetAllComboBoxes()
        {
            comboBox_customerName.ResetText();
            comboBox_customerName.Items.Clear();
            comboBox_currencyID.ResetText();
            comboBox_currencyID.Items.Clear();
            comboBox_customerStatusID.ResetText();
            comboBox_customerStatusID.Items.Clear();
            comboBox_countryID.ResetText();
            comboBox_countryID.Items.Clear();
            comboBox_customerNo.ResetText();
            comboBox_customerNo.Items.Clear();
            comboBox_nickName.ResetText();
            comboBox_nickName.Items.Clear();
            comboBox_primaryKAMID.ResetText();
            comboBox_primaryKAMID.Items.Clear();
            comboBox_secondaryKAMID.ResetText();
            comboBox_secondaryKAMID.Items.Clear();
            comboBox_customerSince.ResetText();
            comboBox_customerSince.Items.Clear();
            comboBox_industryID.ResetText();
            comboBox_industryID.Items.Clear();
            comboBox_phoneNo.ResetText();
            comboBox_phoneNo.Items.Clear();
            comboBox_faxNo.ResetText();
            comboBox_faxNo.Items.Clear();
            comboBox_email.ResetText();
            comboBox_email.Items.Clear();
            comboBox_website.ResetText();
            comboBox_website.Items.Clear();
            comboBox_address.ResetText();
            comboBox_address.Items.Clear();
            comboBox_address2.ResetText();
            comboBox_address2.Items.Clear();
            comboBox_address3.ResetText();
            comboBox_address3.Items.Clear();
            comboBox_zipCode.ResetText();
            comboBox_zipCode.Items.Clear();
            comboBox_city.ResetText();
            comboBox_city.Items.Clear();
            comboBox_state.ResetText();
            comboBox_state.Items.Clear();
            comboBox_useEanNo.ResetText();
            comboBox_useEanNo.Items.Clear();
            comboBox_eanNo.ResetText();
            comboBox_eanNo.Items.Clear();
            comboBox_organizationNo.ResetText();
            comboBox_organizationNo.Items.Clear();
            comboBox_VATNo.ResetText();
            comboBox_VATNo.Items.Clear();
            comboBox_useInvoicingAddress.ResetText();
            comboBox_useInvoicingAddress.Items.Clear();
            comboBox_invoicingAddress.ResetText();
            comboBox_invoicingAddress.Items.Clear();
            comboBox_invoicingAddress2.ResetText();
            comboBox_invoicingAddress2.Items.Clear();
            comboBox_invoicingAddress3.ResetText();
            comboBox_invoicingAddress3.Items.Clear();
            comboBox_invoicingAddressZipCode.ResetText();
            comboBox_invoicingAddressZipCode.Items.Clear();
            comboBox_invoicingAddressCity.ResetText();
            comboBox_invoicingAddressCity.Items.Clear();
            comboBox_invoicingAddressState.ResetText();
            comboBox_invoicingAddressState.Items.Clear();
            comboBox_invoicingAddressCountryID.ResetText();
            comboBox_invoicingAddressCountryID.Items.Clear();
            comboBox_defaultMileageDistance.ResetText();
            comboBox_defaultMileageDistance.Items.Clear();
            comboBox_expenseIsBillable.ResetText();
            comboBox_expenseIsBillable.Items.Clear();
            comboBox_mileageIsBillable.ResetText();
            comboBox_mileageIsBillable.Items.Clear();
            comboBox_defaultDistIsMaxBillable.ResetText();
            comboBox_defaultDistIsMaxBillable.Items.Clear();
            comboBox_contactID.ResetText();
            comboBox_contactID.Items.Clear();
            comboBox_invoiceAddressToUse.ResetText();
            comboBox_invoiceAddressToUse.Items.Clear();
            comboBox_internalReferenceID.ResetText();
            comboBox_internalReferenceID.Items.Clear();
            comboBox_customerReferenceID.ResetText();
            comboBox_customerReferenceID.Items.Clear();
            comboBox_paymentTermID.ResetText();
            comboBox_paymentTermID.Items.Clear();
            comboBox_invoicingAddress.ResetText();
            comboBox_invoicingAddress.Items.Clear();
            comboBox_discountPercentage.ResetText();
            comboBox_discountPercentage.Items.Clear();
            comboBox_calculateVAT.ResetText();
            comboBox_calculateVAT.Items.Clear();
            comboBox_VATPercentage.ResetText();
            comboBox_VATPercentage.Items.Clear();
        }

        private void ClearAndResetAllCheckBoxes()
        {
            checkBox_defaultCurrencyID.Checked = false;
            checkBox_defaultCustomerStatusID.Checked = false;
            checkBox_defaultCountryID.Checked = false;
            checkBox_defaultPrimaryKAMID.Checked = false;
            checkBox_defaultSecondaryKAMID.Checked = false;
            checkBox_defaultIndustryID.Checked = false;
            checkBox_defaultExpenseIsBillable.Checked = false;
            checkBox_defaultMileageIsBillable.Checked = false;
            checkBox_defaultPaymentTermID.Checked = false;
            checkBox_defaultVATPercentage.Checked = false;
        }

        #endregion

        #region Get default values from API

        private void GetAllCurrencyFromApi()
        {
            var _apiResponse = CustomerHandler.Instance.GetAllCurrency(AuthenticationHandler.Instance.Token);

            if (_apiResponse != null)
            {
                _currencyIDList = new List<KeyValuePair<int, string>>();

                foreach (var _currency in _apiResponse)
                {
                    _currencyIDList.Add(new KeyValuePair<int, string>(_currency.CurrencyID, _currency.DescriptiveName));
                }
            }
        }

        private void GetAllCountryFromApi()
        {
            var _apiResponse = CustomerHandler.Instance.GetAllCountry(AuthenticationHandler.Instance.Token);

            if (_apiResponse != null)
            {
                _countryIDList = new List<KeyValuePair<int, string>>();

                foreach (var _country in _apiResponse)
                {
                    _countryIDList.Add(new KeyValuePair<int, string>(_country.CountryID, _country.CountryName));
                }
            }
        }

        private void GetAllCustomerStatusFromApi()
        {
            var _apiResponse = CustomerHandler.Instance.GetAllCustomerStatus(AuthenticationHandler.Instance.Token);

            if (_apiResponse != null)
            {
                _customerStatusIDList = new List<KeyValuePair<int, string>>();

                foreach (var _customerStatus in _apiResponse)
                {
                    _customerStatusIDList.Add(new KeyValuePair<int, string>(_customerStatus.CustomerStatusID,
                        _customerStatus.Name));
                }
            }
        }

        private void GetAllPrimaryKAMFromApi()
        {
            var _apiResponse = CustomerHandler.Instance.GetAllEmployee(AuthenticationHandler.Instance.Token);

            if (_apiResponse != null)
            {
                _primaryKAMIDList = new List<KeyValuePair<int, string>>();

                foreach (var _primaryKAM in _apiResponse)
                {
                    _primaryKAMIDList.Add(new KeyValuePair<int, string>(_primaryKAM.UserID, _primaryKAM.Initials));
                }
            }
        }

        private void GetAllSecondaryKAMFromApi()
        {
            var _apiResponse = CustomerHandler.Instance.GetAllEmployee(AuthenticationHandler.Instance.Token);

            if (_apiResponse != null)
            {
                _secondaryKAMIDList = new List<KeyValuePair<int, string>>();

                foreach (var _secondaryKAM in _apiResponse)
                {
                    _secondaryKAMIDList.Add(new KeyValuePair<int, string>(_secondaryKAM.UserID,
                        _secondaryKAM.Initials));
                }
            }
        }

        private void GetAllIndustryFromApi()
        {
            var _apiResponse = CustomerHandler.Instance.GetAllIndustry(AuthenticationHandler.Instance.Token);

            if (_apiResponse != null)
            {
                _industryIDList = new List<KeyValuePair<int, string>>();

                foreach (var _industry in _apiResponse)
                {
                    _industryIDList.Add(new KeyValuePair<int, string>(_industry.IndustryID, _industry.IndustryName));
                }
            }
        }

        private void GetAllPaymentTermFromApi()
        {
            var _apiResponse = CustomerHandler.Instance.GetAllPaymentTerm(AuthenticationHandler.Instance.Token);

            if (_apiResponse != null)
            {
                _paymentTermIDList = new List<KeyValuePair<int, string>>();

                foreach (var _paymentTerm in _apiResponse)
                {
                    _paymentTermIDList.Add(new KeyValuePair<int, string>(_paymentTerm.PaymentMethodID,
                        _paymentTerm.Name));
                }
            }
        }

        #endregion

        #region Add default key value pair list to Combobox

        private void AddKeyValuePairListToCurrencyIDComboBox()
        {
            comboBox_currencyID.DisplayMember = "Value";
            comboBox_currencyID.ValueMember = "Key";

            if (_currencyIDList != null)
            {
                foreach (var _currency in _currencyIDList)
                {
                    comboBox_currencyID.Items.Add(new {_currency.Key, _currency.Value});
                }
            }
        }

        private void AddKeyValuePairListToCountryIDComboBox()
        {
            comboBox_countryID.DisplayMember = "Value";
            comboBox_countryID.ValueMember = "Key";

            if (_countryIDList != null)
            {
                foreach (var _country in _countryIDList)
                {
                    comboBox_countryID.Items.Add(new {_country.Key, _country.Value});
                }
            }
        }

        private void AddKeyValuePairListToCustomerStatusIDComboBox()
        {
            comboBox_customerStatusID.DisplayMember = "Value";
            comboBox_customerStatusID.ValueMember = "Key";

            if (_customerStatusIDList != null)
            {
                foreach (var _customerStatus in _customerStatusIDList)
                {
                    comboBox_customerStatusID.Items.Add(new {_customerStatus.Key, _customerStatus.Value});
                }
            }
        }

        private void AddKeyValuePairListToPrimaryKAMIDComboBox()
        {
            comboBox_primaryKAMID.DisplayMember = "Value";
            comboBox_primaryKAMID.ValueMember = "Key";

            if (_primaryKAMIDList != null)
            {
                foreach (var _primaryKAM in _primaryKAMIDList)
                {
                    comboBox_primaryKAMID.Items.Add(new {_primaryKAM.Key, _primaryKAM.Value});
                }
            }
        }

        private void AddKeyValuePairListToSecondaryKAMIDComboBox()
        {
            comboBox_secondaryKAMID.DisplayMember = "Value";
            comboBox_secondaryKAMID.ValueMember = "Key";

            if (_secondaryKAMIDList != null)
            {
                foreach (var _secondaryKAM in _secondaryKAMIDList)
                {
                    comboBox_secondaryKAMID.Items.Add(new {_secondaryKAM.Key, _secondaryKAM.Value});
                }
            }
        }

        private void AddKeyValuePairListToIndustryIDComboBox()
        {
            comboBox_industryID.DisplayMember = "Value";
            comboBox_industryID.ValueMember = "Key";

            if (_industryIDList != null)
            {
                foreach (var _industry in _industryIDList)
                {
                    comboBox_industryID.Items.Add(new {_industry.Key, _industry.Value});
                }
            }
        }

        private void AddKeyValuePairListToPaymentTermIDComboBox()
        {
            comboBox_paymentTermID.DisplayMember = "Value";
            comboBox_paymentTermID.ValueMember = "Key";

            if (_paymentTermIDList != null)
            {
                foreach (var _paymentTerm in _paymentTermIDList)
                {
                    comboBox_paymentTermID.Items.Add(new {_paymentTerm.Key, _paymentTerm.Value});
                }
            }
        }

        #endregion

        #region Combobox implementations

        private void comboBox_customerName_SelectedIndexChanged(object sender, EventArgs e)
        {
            var _columnIndex = _fileContent.Columns.IndexOf(comboBox_customerName.SelectedItem.ToString());

            var _tableColumnIndex = _customerTable.Columns.IndexOf(_customerName);

            ClearColumn(_tableColumnIndex);

            MapFileContentToTable(_tableColumnIndex, _columnIndex);

            CheckCellsForNullOrEmpty(_tableColumnIndex);
        }

        private void comboBox_currencyID_SelectedIndexChanged(object sender, EventArgs e)
        {
            var _tableColumnIndex = _customerTable.Columns.IndexOf(_currencyID);

            ClearColumn(_tableColumnIndex);

            if (!checkBox_defaultCurrencyID.Checked)
            {
                var _columnIndex = _fileContent.Columns.IndexOf(comboBox_currencyID.SelectedItem.ToString());

                MapFileContentToTable(_tableColumnIndex, _columnIndex);
            }
            else
            {
                var _defaultValue = (comboBox_currencyID.SelectedItem as dynamic).Key.ToString();

                MapDefaultValueToTable(_tableColumnIndex, _defaultValue);
            }

            CheckCellsForNullOrEmpty(_tableColumnIndex);
        }

        private void comboBox_customerStatusID_SelectedIndexChanged(object sender, EventArgs e)
        {
            var _tableColumnIndex = _customerTable.Columns.IndexOf(_customerStatusID);

            ClearColumn(_tableColumnIndex);

            if (!checkBox_defaultCustomerStatusID.Checked)
            {
                var _columnIndex = _fileContent.Columns.IndexOf(comboBox_customerStatusID.SelectedItem.ToString());

                MapFileContentToTable(_tableColumnIndex, _columnIndex);
            }
            else
            {
                var _defaultValue = (comboBox_customerStatusID.SelectedItem as dynamic).Key.ToString();

                MapDefaultValueToTable(_tableColumnIndex, _defaultValue);
            }

            CheckCellsForNullOrEmpty(_tableColumnIndex);
        }

        private void comboBox_countryID_SelectedIndexChanged(object sender, EventArgs e)
        {
            var _tableColumnIndex = _customerTable.Columns.IndexOf(_countryID);

            ClearColumn(_tableColumnIndex);

            if (!checkBox_defaultCountryID.Checked)
            {
                var _columnIndex = _fileContent.Columns.IndexOf(comboBox_countryID.SelectedItem.ToString());

                MapFileContentToTable(_tableColumnIndex, _columnIndex);
            }
            else
            {
                var _defaultValue = (comboBox_countryID.SelectedItem as dynamic).Key.ToString();

                MapDefaultValueToTable(_tableColumnIndex, _defaultValue);
            }

            CheckCellsForNullOrEmpty(_tableColumnIndex);
        }

        private void comboBox_customerNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            var _columnIndex = _fileContent.Columns.IndexOf(comboBox_customerNo.SelectedItem.ToString());

            CheckAndAddColumn(_customerNo);

            var _tableColumnIndex = _customerTable.Columns.IndexOf(_customerNo);

            ClearColumn(_tableColumnIndex);

            MapFileContentToTable(_tableColumnIndex, _columnIndex);

            CheckCellsForNullOrEmpty(_tableColumnIndex);
        }

        private void comboBox_nickName_SelectedIndexChanged(object sender, EventArgs e)
        {
            var _columnIndex = _fileContent.Columns.IndexOf(comboBox_nickName.SelectedItem.ToString());

            CheckAndAddColumn(_nickname);

            var _tableColumnIndex = _customerTable.Columns.IndexOf(_nickname);

            ClearColumn(_tableColumnIndex);

            MapFileContentToTable(_tableColumnIndex, _columnIndex);

            CheckCellsForNullOrEmpty(_tableColumnIndex);
        }

        private void comboBox_primaryKAMID_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckAndAddColumn(_primaryKAMID);

            var _tableColumnIndex = _customerTable.Columns.IndexOf(_primaryKAMID);

            ClearColumn(_tableColumnIndex);

            if (!checkBox_defaultPrimaryKAMID.Checked)
            {
                var _columnIndex = _fileContent.Columns.IndexOf(comboBox_primaryKAMID.SelectedItem.ToString());

                MapFileContentToTable(_tableColumnIndex, _columnIndex);
            }
            else
            {
                var _defaultValue = (comboBox_primaryKAMID.SelectedItem as dynamic).Key.ToString();

                MapDefaultValueToTable(_tableColumnIndex, _defaultValue);
            }

            CheckCellsForNullOrEmpty(_tableColumnIndex);
        }

        private void comboBox_secondaryKAMID_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckAndAddColumn(_secondaryKAMID);

            var _tableColumnIndex = _customerTable.Columns.IndexOf(_secondaryKAMID);

            ClearColumn(_tableColumnIndex);

            if (!checkBox_defaultSecondaryKAMID.Checked)
            {
                var _columnIndex = _fileContent.Columns.IndexOf(comboBox_secondaryKAMID.SelectedItem.ToString());

                MapFileContentToTable(_tableColumnIndex, _columnIndex);
            }
            else
            {
                var _defaultValue = (comboBox_secondaryKAMID.SelectedItem as dynamic).Key.ToString();

                MapDefaultValueToTable(_tableColumnIndex, _defaultValue);
            }

            CheckCellsForNullOrEmpty(_tableColumnIndex);
        }

        private void comboBox_customerSince_SelectedIndexChanged(object sender, EventArgs e)
        {
            var _columnIndex = _fileContent.Columns.IndexOf(comboBox_customerSince.SelectedItem.ToString());

            CheckAndAddColumn(_customerSince);

            var _tableColumnIndex = _customerTable.Columns.IndexOf(_customerSince);

            ClearColumn(_tableColumnIndex);

            MapFileContentToTable(_tableColumnIndex, _columnIndex);

            CheckCellsForNullOrEmpty(_tableColumnIndex);
        }

        private void comboBox_industryID_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckAndAddColumn(_industryID);

            var _tableColumnIndex = _customerTable.Columns.IndexOf(_industryID);

            ClearColumn(_tableColumnIndex);

            if (!checkBox_defaultIndustryID.Checked)
            {
                var _columnIndex = _fileContent.Columns.IndexOf(comboBox_industryID.SelectedItem.ToString());

                MapFileContentToTable(_tableColumnIndex, _columnIndex);
            }
            else
            {
                var _defaultValue = (comboBox_industryID.SelectedItem as dynamic).Key.ToString();

                MapDefaultValueToTable(_tableColumnIndex, _defaultValue);
            }

            CheckCellsForNullOrEmpty(_tableColumnIndex);
        }

        private void comboBox_phoneNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            var _columnIndex = _fileContent.Columns.IndexOf(comboBox_phoneNo.SelectedItem.ToString());

            CheckAndAddColumn(_phoneNo);

            var _tableColumnIndex = _customerTable.Columns.IndexOf(_phoneNo);

            ClearColumn(_tableColumnIndex);

            MapFileContentToTable(_tableColumnIndex, _columnIndex);

            CheckCellsForNullOrEmpty(_tableColumnIndex);
        }

        private void comboBox_faxNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            var _columnIndex = _fileContent.Columns.IndexOf(comboBox_faxNo.SelectedItem.ToString());

            CheckAndAddColumn(_faxNo);

            var _tableColumnIndex = _customerTable.Columns.IndexOf(_faxNo);

            ClearColumn(_tableColumnIndex);

            MapFileContentToTable(_tableColumnIndex, _columnIndex);

            CheckCellsForNullOrEmpty(_tableColumnIndex);
        }

        private void comboBox_email_SelectedIndexChanged(object sender, EventArgs e)
        {
            var _columnIndex = _fileContent.Columns.IndexOf(comboBox_email.SelectedItem.ToString());

            CheckAndAddColumn(_email);

            var _tableColumnIndex = _customerTable.Columns.IndexOf(_email);

            ClearColumn(_tableColumnIndex);

            MapFileContentToTable(_tableColumnIndex, _columnIndex);

            CheckCellsForNullOrEmpty(_tableColumnIndex);
        }

        private void comboBox_website_SelectedIndexChanged(object sender, EventArgs e)
        {
            var _columnIndex = _fileContent.Columns.IndexOf(comboBox_website.SelectedItem.ToString());

            CheckAndAddColumn(_website);

            var _tableColumnIndex = _customerTable.Columns.IndexOf(_website);

            ClearColumn(_tableColumnIndex);

            MapFileContentToTable(_tableColumnIndex, _columnIndex);

            CheckCellsForNullOrEmpty(_tableColumnIndex);
        }

        private void comboBox_address_SelectedIndexChanged(object sender, EventArgs e)
        {
            var _columnIndex = _fileContent.Columns.IndexOf(comboBox_address.SelectedItem.ToString());

            CheckAndAddColumn(_address);

            var _tableColumnIndex = _customerTable.Columns.IndexOf(_address);

            ClearColumn(_tableColumnIndex);

            MapFileContentToTable(_tableColumnIndex, _columnIndex);

            CheckCellsForNullOrEmpty(_tableColumnIndex);
        }

        private void comboBox_address2_SelectedIndexChanged(object sender, EventArgs e)
        {
            var _columnIndex = _fileContent.Columns.IndexOf(comboBox_address2.SelectedItem.ToString());

            CheckAndAddColumn(_address2);

            var _tableColumnIndex = _customerTable.Columns.IndexOf(_address2);

            ClearColumn(_tableColumnIndex);

            MapFileContentToTable(_tableColumnIndex, _columnIndex);

            CheckCellsForNullOrEmpty(_tableColumnIndex);
        }

        private void comboBox_address3_SelectedIndexChanged(object sender, EventArgs e)
        {
            var _columnIndex = _fileContent.Columns.IndexOf(comboBox_address3.SelectedItem.ToString());

            CheckAndAddColumn(_address3);

            var _tableColumnIndex = _customerTable.Columns.IndexOf(_address3);

            ClearColumn(_tableColumnIndex);

            MapFileContentToTable(_tableColumnIndex, _columnIndex);

            CheckCellsForNullOrEmpty(_tableColumnIndex);
        }

        private void comboBox_zipCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            var _columnIndex = _fileContent.Columns.IndexOf(comboBox_zipCode.SelectedItem.ToString());

            CheckAndAddColumn(_zipCode);

            var _tableColumnIndex = _customerTable.Columns.IndexOf(_zipCode);

            ClearColumn(_tableColumnIndex);

            MapFileContentToTable(_tableColumnIndex, _columnIndex);

            CheckCellsForNullOrEmpty(_tableColumnIndex);
        }

        private void comboBox_city_SelectedIndexChanged(object sender, EventArgs e)
        {
            var _columnIndex = _fileContent.Columns.IndexOf(comboBox_city.SelectedItem.ToString());

            CheckAndAddColumn(_city);

            var _tableColumnIndex = _customerTable.Columns.IndexOf(_city);

            ClearColumn(_tableColumnIndex);

            MapFileContentToTable(_tableColumnIndex, _columnIndex);

            CheckCellsForNullOrEmpty(_tableColumnIndex);
        }

        private void comboBox_state_SelectedIndexChanged(object sender, EventArgs e)
        {
            var _columnIndex = _fileContent.Columns.IndexOf(comboBox_state.SelectedItem.ToString());

            CheckAndAddColumn(_state);

            var _tableColumnIndex = _customerTable.Columns.IndexOf(_state);

            ClearColumn(_tableColumnIndex);

            MapFileContentToTable(_tableColumnIndex, _columnIndex);

            CheckCellsForNullOrEmpty(_tableColumnIndex);
        }

        private void comboBox_useEanNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            var _columnIndex = _fileContent.Columns.IndexOf(comboBox_useEanNo.SelectedItem.ToString());

            CheckAndAddColumn(_useEanNo);

            var _tableColumnIndex = _customerTable.Columns.IndexOf(_useEanNo);

            ClearColumn(_tableColumnIndex);

            MapFileContentToTable(_tableColumnIndex, _columnIndex);

            CheckCellsForNullOrEmpty(_tableColumnIndex);
        }

        private void comboBox_eanNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            var _columnIndex = _fileContent.Columns.IndexOf(comboBox_eanNo.SelectedItem.ToString());

            CheckAndAddColumn(_eanNo);

            var _tableColumnIndex = _customerTable.Columns.IndexOf(_eanNo);

            ClearColumn(_tableColumnIndex);

            MapFileContentToTable(_tableColumnIndex, _columnIndex);

            CheckCellsForNullOrEmpty(_tableColumnIndex);
        }

        private void comboBox_organizationNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            var _columnIndex = _fileContent.Columns.IndexOf(comboBox_organizationNo.SelectedItem.ToString());

            CheckAndAddColumn(_organizationNo);

            var _tableColumnIndex = _customerTable.Columns.IndexOf(_organizationNo);

            ClearColumn(_tableColumnIndex);

            MapFileContentToTable(_tableColumnIndex, _columnIndex);

            CheckCellsForNullOrEmpty(_tableColumnIndex);
        }

        private void comboBox_VATNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            var _columnIndex = _fileContent.Columns.IndexOf(comboBox_VATNo.SelectedItem.ToString());

            CheckAndAddColumn(_VATNo);

            var _tableColumnIndex = _customerTable.Columns.IndexOf(_VATNo);

            ClearColumn(_tableColumnIndex);

            MapFileContentToTable(_tableColumnIndex, _columnIndex);

            CheckCellsForNullOrEmpty(_tableColumnIndex);
        }

        private void comboBox_useInvoicingAddress_SelectedIndexChanged(object sender, EventArgs e)
        {
            var _columnIndex = _fileContent.Columns.IndexOf(comboBox_useInvoicingAddress.SelectedItem.ToString());

            CheckAndAddColumn(_useInvoicingAddress);

            var _tableColumnIndex = _customerTable.Columns.IndexOf(_useInvoicingAddress);

            ClearColumn(_tableColumnIndex);

            MapFileContentToTable(_tableColumnIndex, _columnIndex);

            CheckCellsForNullOrEmpty(_tableColumnIndex);
        }

        private void comboBox_invoicingAddress_SelectedIndexChanged(object sender, EventArgs e)
        {
            var _columnIndex = _fileContent.Columns.IndexOf(comboBox_invoicingAddress.SelectedItem.ToString());

            CheckAndAddColumn(_invoicingAddress);

            var _tableColumnIndex = _customerTable.Columns.IndexOf(_invoicingAddress);

            ClearColumn(_tableColumnIndex);

            MapFileContentToTable(_tableColumnIndex, _columnIndex);

            CheckCellsForNullOrEmpty(_tableColumnIndex);
        }

        private void comboBox_invoicingAddress2_SelectedIndexChanged(object sender, EventArgs e)
        {
            var _columnIndex = _fileContent.Columns.IndexOf(comboBox_invoicingAddress2.SelectedItem.ToString());

            CheckAndAddColumn(_invoicingAddress2);

            var _tableColumnIndex = _customerTable.Columns.IndexOf(_invoicingAddress2);

            ClearColumn(_tableColumnIndex);

            MapFileContentToTable(_tableColumnIndex, _columnIndex);

            CheckCellsForNullOrEmpty(_tableColumnIndex);
        }

        private void comboBox_invoicingAddress3_SelectedIndexChanged(object sender, EventArgs e)
        {
            var _columnIndex = _fileContent.Columns.IndexOf(comboBox_invoicingAddress3.SelectedItem.ToString());

            CheckAndAddColumn(_invoicingAddress3);

            var _tableColumnIndex = _customerTable.Columns.IndexOf(_invoicingAddress3);

            ClearColumn(_tableColumnIndex);

            MapFileContentToTable(_tableColumnIndex, _columnIndex);

            CheckCellsForNullOrEmpty(_tableColumnIndex);
        }

        private void comboBox_invoicingAddressZipCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            var _columnIndex = _fileContent.Columns.IndexOf(comboBox_invoicingAddressZipCode.SelectedItem.ToString());

            CheckAndAddColumn(_invoicingAddressZipCode);

            var _tableColumnIndex = _customerTable.Columns.IndexOf(_invoicingAddressZipCode);

            ClearColumn(_tableColumnIndex);

            MapFileContentToTable(_tableColumnIndex, _columnIndex);

            CheckCellsForNullOrEmpty(_tableColumnIndex);
        }

        private void comboBox_invoicingAddressCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            var _columnIndex = _fileContent.Columns.IndexOf(comboBox_invoicingAddressCity.SelectedItem.ToString());

            CheckAndAddColumn(_invoicingAddressCity);

            var _tableColumnIndex = _customerTable.Columns.IndexOf(_invoicingAddressCity);

            ClearColumn(_tableColumnIndex);

            MapFileContentToTable(_tableColumnIndex, _columnIndex);

            CheckCellsForNullOrEmpty(_tableColumnIndex);
        }

        private void comboBox_invoicingAddressState_SelectedIndexChanged(object sender, EventArgs e)
        {
            var _columnIndex = _fileContent.Columns.IndexOf(comboBox_invoicingAddressState.SelectedItem.ToString());

            CheckAndAddColumn(_invoicingAddressState);

            var _tableColumnIndex = _customerTable.Columns.IndexOf(_invoicingAddressState);

            ClearColumn(_tableColumnIndex);

            MapFileContentToTable(_tableColumnIndex, _columnIndex);

            CheckCellsForNullOrEmpty(_tableColumnIndex);
        }

        private void comboBox_invoicingAddressCountryID_SelectedIndexChanged(object sender, EventArgs e)
        {
            var _columnIndex = _fileContent.Columns.IndexOf(comboBox_invoicingAddressCountryID.SelectedItem.ToString());

            CheckAndAddColumn(_invoicingAddressCountryID);

            var _tableColumnIndex = _customerTable.Columns.IndexOf(_invoicingAddressCountryID);

            ClearColumn(_tableColumnIndex);

            MapFileContentToTable(_tableColumnIndex, _columnIndex);

            CheckCellsForNullOrEmpty(_tableColumnIndex);
        }

        private void comboBox_defaultMileageDistance_SelectedIndexChanged(object sender, EventArgs e)
        {
            var _columnIndex = _fileContent.Columns.IndexOf(comboBox_defaultMileageDistance.SelectedItem.ToString());

            CheckAndAddColumn(_defaultMileageDistance);

            var _tableColumnIndex = _customerTable.Columns.IndexOf(_defaultMileageDistance);

            ClearColumn(_tableColumnIndex);

            MapFileContentToTable(_tableColumnIndex, _columnIndex);

            CheckCellsForNullOrEmpty(_tableColumnIndex);
        }

        private void comboBox_expenseIsBillable_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckAndAddColumn(_expenseIsBillable);

            var _tableColumnIndex = _customerTable.Columns.IndexOf(_expenseIsBillable);

            ClearColumn(_tableColumnIndex);

            if (!checkBox_defaultExpenseIsBillable.Checked)
            {
                var _columnIndex = _fileContent.Columns.IndexOf(comboBox_expenseIsBillable.SelectedItem.ToString());

                MapFileContentToTable(_tableColumnIndex, _columnIndex);
            }
            else
            {
                var _defaultValue = comboBox_expenseIsBillable.SelectedItem.ToString();

                MapDefaultValueToTable(_tableColumnIndex, _defaultValue);
            }

            CheckCellsForNullOrEmpty(_tableColumnIndex);
        }

        private void comboBox_mileageIsBillable_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckAndAddColumn(_mileageIsBillable);

            var _tableColumnIndex = _customerTable.Columns.IndexOf(_mileageIsBillable);

            ClearColumn(_tableColumnIndex);

            if (!checkBox_defaultMileageIsBillable.Checked)
            {
                var _columnIndex = _fileContent.Columns.IndexOf(comboBox_mileageIsBillable.SelectedItem.ToString());

                MapFileContentToTable(_tableColumnIndex, _columnIndex);
            }
            else
            {
                var _defaultValue = comboBox_mileageIsBillable.SelectedItem.ToString();

                MapDefaultValueToTable(_tableColumnIndex, _defaultValue);
            }

            CheckCellsForNullOrEmpty(_tableColumnIndex);
        }

        private void comboBox_defaultDistIsMaxBillable_SelectedIndexChanged(object sender, EventArgs e)
        {
            var _columnIndex = _fileContent.Columns.IndexOf(comboBox_defaultDistIsMaxBillable.SelectedItem.ToString());

            CheckAndAddColumn(_defaultDistIsMaxBillable);

            var _tableColumnIndex = _customerTable.Columns.IndexOf(_defaultDistIsMaxBillable);

            ClearColumn(_tableColumnIndex);

            MapFileContentToTable(_tableColumnIndex, _columnIndex);

            CheckCellsForNullOrEmpty(_tableColumnIndex);
        }

        private void comboBox_contactID_SelectedIndexChanged(object sender, EventArgs e)
        {
            var _columnIndex = _fileContent.Columns.IndexOf(comboBox_contactID.SelectedItem.ToString());

            CheckAndAddColumn(_contactID);

            var _tableColumnIndex = _customerTable.Columns.IndexOf(_contactID);

            ClearColumn(_tableColumnIndex);

            MapFileContentToTable(_tableColumnIndex, _columnIndex);

            CheckCellsForNullOrEmpty(_tableColumnIndex);
        }

        private void comboBox_invoiceAddressToUse_SelectedIndexChanged(object sender, EventArgs e)
        {
            var _columnIndex = _fileContent.Columns.IndexOf(comboBox_invoiceAddressToUse.SelectedItem.ToString());

            CheckAndAddColumn(_invoiceAddressToUse);

            var _tableColumnIndex = _customerTable.Columns.IndexOf(_invoiceAddressToUse);

            ClearColumn(_tableColumnIndex);

            MapFileContentToTable(_tableColumnIndex, _columnIndex);

            CheckCellsForNullOrEmpty(_tableColumnIndex);
        }

        private void comboBox_internalReferenceID_SelectedIndexChanged(object sender, EventArgs e)
        {
            var _columnIndex = _fileContent.Columns.IndexOf(comboBox_internalReferenceID.SelectedItem.ToString());

            CheckAndAddColumn(_internalReferenceID);

            var _tableColumnIndex = _customerTable.Columns.IndexOf(_internalReferenceID);

            ClearColumn(_tableColumnIndex);

            MapFileContentToTable(_tableColumnIndex, _columnIndex);

            CheckCellsForNullOrEmpty(_tableColumnIndex);
        }

        private void comboBox_customerReferenceID_SelectedIndexChanged(object sender, EventArgs e)
        {
            var _columnIndex = _fileContent.Columns.IndexOf(comboBox_customerReferenceID.SelectedItem.ToString());

            CheckAndAddColumn(_customerReferenceID);

            var _tableColumnIndex = _customerTable.Columns.IndexOf(_customerReferenceID);

            ClearColumn(_tableColumnIndex);

            MapFileContentToTable(_tableColumnIndex, _columnIndex);

            CheckCellsForNullOrEmpty(_tableColumnIndex);
        }

        private void comboBox_paymentTermID_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckAndAddColumn(_paymentTermID);

            var _tableColumnIndex = _customerTable.Columns.IndexOf(_paymentTermID);

            ClearColumn(_tableColumnIndex);

            if (!checkBox_defaultPaymentTermID.Checked)
            {
                var _columnIndex = _fileContent.Columns.IndexOf(comboBox_paymentTermID.SelectedItem.ToString());

                MapFileContentToTable(_tableColumnIndex, _columnIndex);
            }
            else
            {
                var _defaultValue = (comboBox_paymentTermID.SelectedItem as dynamic).Key.ToString();

                MapDefaultValueToTable(_tableColumnIndex, _defaultValue);
            }

            CheckCellsForNullOrEmpty(_tableColumnIndex);
        }

        private void comboBox_discountPercentage_SelectedIndexChanged(object sender, EventArgs e)
        {
            var _columnIndex = _fileContent.Columns.IndexOf(comboBox_discountPercentage.SelectedItem.ToString());

            CheckAndAddColumn(_discountPercentage);

            var _tableColumnIndex = _customerTable.Columns.IndexOf(_discountPercentage);

            ClearColumn(_tableColumnIndex);

            MapFileContentToTable(_tableColumnIndex, _columnIndex);

            CheckCellsForNullOrEmpty(_tableColumnIndex);
        }

        private void comboBox_calculateVAT_SelectedIndexChanged(object sender, EventArgs e)
        {
            var _columnIndex = _fileContent.Columns.IndexOf(comboBox_calculateVAT.SelectedItem.ToString());

            CheckAndAddColumn(_calculateVAT);

            var _tableColumnIndex = _customerTable.Columns.IndexOf(_calculateVAT);

            ClearColumn(_tableColumnIndex);

            MapFileContentToTable(_tableColumnIndex, _columnIndex);

            CheckCellsForNullOrEmpty(_tableColumnIndex);
        }

        private void comboBox_VATPercentage_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckAndAddColumn(_VATPercentage);

            var _tableColumnIndex = _customerTable.Columns.IndexOf(_VATPercentage);

            ClearColumn(_tableColumnIndex);

            if (!checkBox_defaultVATPercentage.Checked)
            {
                var _columnIndex = _fileContent.Columns.IndexOf(comboBox_VATPercentage.SelectedItem.ToString());

                MapFileContentToTable(_tableColumnIndex, _columnIndex);
            }
            else
            {
                var _defaultValue = comboBox_VATPercentage.SelectedItem.ToString();

                MapDefaultValueToTable(_tableColumnIndex, _defaultValue);
            }

            CheckCellsForNullOrEmpty(_tableColumnIndex);
        }

        #endregion

        #region Checkbox implementations

        private void checkBox_defaultCurrencyID_CheckedChanged(object sender, EventArgs e)
        {
            comboBox_currencyID.ResetText();
            comboBox_currencyID.Items.Clear();

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
                comboBox_currencyID.Items.AddRange(CustomerHandler.Instance.FileColumnHeaders.Cast<object>().ToArray());
            }

            var _tableColumnIndex = _customerTable.Columns.IndexOf(_currencyID);

            if (_tableColumnIndex != -1)
            {
                ClearColumn(_tableColumnIndex);

                ClearRow(_tableColumnIndex);

                CheckCellsForNullOrEmpty(_tableColumnIndex);
            }
        }

        private void checkBox_defaultCustomerStatusID_CheckedChanged(object sender, EventArgs e)
        {
            comboBox_customerStatusID.ResetText();
            comboBox_customerStatusID.Items.Clear();

            if (checkBox_defaultCustomerStatusID.Checked)
            {
                if (_customerStatusIDList == null)
                {
                    GetAllCustomerStatusFromApi();
                }

                AddKeyValuePairListToCustomerStatusIDComboBox();
            }
            else
            {
                comboBox_customerStatusID.Items.AddRange(CustomerHandler.Instance.FileColumnHeaders.Cast<object>()
                    .ToArray());
            }

            var _tableColumnIndex = _customerTable.Columns.IndexOf(_customerStatusID);

            if (_tableColumnIndex != -1)
            {
                ClearColumn(_tableColumnIndex);

                ClearRow(_tableColumnIndex);

                CheckCellsForNullOrEmpty(_tableColumnIndex);
            }
        }

        private void checkBox_defaultCountryID_CheckedChanged(object sender, EventArgs e)
        {
            comboBox_countryID.ResetText();
            comboBox_countryID.Items.Clear();

            if (checkBox_defaultCountryID.Checked)
            {
                if (_countryIDList == null)
                {
                    GetAllCountryFromApi();
                }

                AddKeyValuePairListToCountryIDComboBox();
            }
            else
            {
                comboBox_countryID.Items.AddRange(CustomerHandler.Instance.FileColumnHeaders.Cast<object>().ToArray());
            }

            var _tableColumnIndex = _customerTable.Columns.IndexOf(_countryID);

            if (_tableColumnIndex != -1)
            {
                ClearColumn(_tableColumnIndex);

                ClearRow(_tableColumnIndex);

                CheckCellsForNullOrEmpty(_tableColumnIndex);
            }
        }

        private void checkBox_defaultPrimaryKAMID_CheckedChanged(object sender, EventArgs e)
        {
            comboBox_primaryKAMID.ResetText();
            comboBox_primaryKAMID.Items.Clear();

            if (checkBox_defaultPrimaryKAMID.Checked)
            {
                if (_primaryKAMIDList == null)
                {
                    GetAllPrimaryKAMFromApi();
                }

                AddKeyValuePairListToPrimaryKAMIDComboBox();
            }
            else
            {
                comboBox_primaryKAMID.Items.AddRange(
                    CustomerHandler.Instance.FileColumnHeaders.Cast<object>().ToArray());
            }

            var _tableColumnIndex = _customerTable.Columns.IndexOf(_primaryKAMID);

            if (_tableColumnIndex != -1)
            {
                ClearColumn(_tableColumnIndex);

                ClearRow(_tableColumnIndex);

                CheckCellsForNullOrEmpty(_tableColumnIndex);
            }
        }

        private void checkBox_defaultSecondaryKAMID_CheckedChanged(object sender, EventArgs e)
        {
            comboBox_secondaryKAMID.ResetText();
            comboBox_secondaryKAMID.Items.Clear();

            if (checkBox_defaultSecondaryKAMID.Checked)
            {
                if (_secondaryKAMIDList == null)
                {
                    GetAllSecondaryKAMFromApi();
                }

                AddKeyValuePairListToSecondaryKAMIDComboBox();
            }
            else
            {
                comboBox_secondaryKAMID.Items.AddRange(CustomerHandler.Instance.FileColumnHeaders.Cast<object>()
                    .ToArray());
            }

            var _tableColumnIndex = _customerTable.Columns.IndexOf(_secondaryKAMID);

            if (_tableColumnIndex != -1)
            {
                ClearColumn(_tableColumnIndex);

                ClearRow(_tableColumnIndex);

                CheckCellsForNullOrEmpty(_tableColumnIndex);
            }
        }

        private void checkBox_defaultIndustryID_CheckedChanged(object sender, EventArgs e)
        {
            comboBox_industryID.ResetText();
            comboBox_industryID.Items.Clear();

            if (checkBox_defaultIndustryID.Checked)
            {
                if (_industryIDList == null)
                {
                    GetAllIndustryFromApi();
                }

                AddKeyValuePairListToIndustryIDComboBox();
            }
            else
            {
                comboBox_industryID.Items.AddRange(CustomerHandler.Instance.FileColumnHeaders.Cast<object>().ToArray());
            }

            var _tableColumnIndex = _customerTable.Columns.IndexOf(_industryID);

            if (_tableColumnIndex != -1)
            {
                ClearColumn(_tableColumnIndex);

                ClearRow(_tableColumnIndex);

                CheckCellsForNullOrEmpty(_tableColumnIndex);
            }
        }

        private void checkBox_defaultExpenseIsBillable_CheckedChanged(object sender, EventArgs e)
        {
            comboBox_expenseIsBillable.ResetText();
            comboBox_expenseIsBillable.Items.Clear();

            comboBox_expenseIsBillable.Items.AddRange(checkBox_defaultExpenseIsBillable.Checked
                ? ExpenseIsBillableList.Cast<object>().ToArray()
                : CustomerHandler.Instance.FileColumnHeaders.Cast<object>().ToArray());

            var _tableColumnIndex = _customerTable.Columns.IndexOf(_expenseIsBillable);

            if (_tableColumnIndex != -1)
            {
                ClearColumn(_tableColumnIndex);

                ClearRow(_tableColumnIndex);

                CheckCellsForNullOrEmpty(_tableColumnIndex);
            }
        }

        private void checkBox_defaultMileageIsBillable_CheckedChanged(object sender, EventArgs e)
        {
            comboBox_mileageIsBillable.ResetText();
            comboBox_mileageIsBillable.Items.Clear();

            comboBox_mileageIsBillable.Items.AddRange(checkBox_defaultMileageIsBillable.Checked
                ? MileageIsBillableList.Cast<object>().ToArray()
                : CustomerHandler.Instance.FileColumnHeaders.Cast<object>().ToArray());

            var _tableColumnIndex = _customerTable.Columns.IndexOf(_mileageIsBillable);

            if (_tableColumnIndex != -1)
            {
                ClearColumn(_tableColumnIndex);

                ClearRow(_tableColumnIndex);

                CheckCellsForNullOrEmpty(_tableColumnIndex);
            }
        }

        private void checkBox_defaultPaymentTermID_CheckedChanged(object sender, EventArgs e)
        {
            comboBox_paymentTermID.ResetText();
            comboBox_paymentTermID.Items.Clear();

            if (checkBox_defaultPaymentTermID.Checked)
            {
                //default values for payment term ID to be implemented later due to lack of API
                //if (_paymentTermIDList == null)
                //{
                //    GetAllPaymentTermFromApi();
                //}

                //AddKeyValuePairListToPaymentTermIDComboBox();
            }
            else
            {
                comboBox_paymentTermID.Items.AddRange(CustomerHandler.Instance.FileColumnHeaders.Cast<object>()
                    .ToArray());
            }

            var _tableColumnIndex = _customerTable.Columns.IndexOf(_paymentTermID);

            if (_tableColumnIndex != -1)
            {
                ClearColumn(_tableColumnIndex);

                ClearRow(_tableColumnIndex);

                CheckCellsForNullOrEmpty(_tableColumnIndex);
            }
        }

        private void checkBox_defaultVATPercentage_CheckedChanged(object sender, EventArgs e)
        {
            comboBox_VATPercentage.ResetText();
            comboBox_VATPercentage.Items.Clear();

            _VATPercentageList = new List<string>();

            for (int i = 0; i <= 100; i++)
            {
                _VATPercentageList.Add(i.ToString());
            }

            comboBox_VATPercentage.Items.AddRange(checkBox_defaultVATPercentage.Checked
                ? _VATPercentageList.Cast<object>().ToArray()
                : CustomerHandler.Instance.FileColumnHeaders.Cast<object>().ToArray());

            var _tableColumnIndex = _customerTable.Columns.IndexOf(_VATPercentage);

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