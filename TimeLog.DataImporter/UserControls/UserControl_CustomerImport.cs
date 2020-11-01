using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using TimeLog.DataImporter.Handlers;
using TimeLog.DataImporter.TimeLogApi;

namespace TimeLog.DataImporter
{
    public partial class UserControl_CustomerImport : UserControl
    {
        public UserControl_CustomerImport()
        {
            InitializeComponent();
            AddRowNumberToDataTable();

        }

        private void UserControl1_Load(object sender, EventArgs e)
        {

        }

        private void button_select_customer_file_Click(object sender, EventArgs e)
        {
            dataGridView_customer.DataSource = CustomerHandler.Instance.GetFileContent();
            comboBox_customerName.Items.AddRange(CustomerHandler.Instance._fileColumnHeaders.ToArray());

        }

        private void comboBox_customer_name_SelectedIndexChanged(object sender, EventArgs e)
        {
            var _columnIndex = this.dataGridView_customer.Columns[this.comboBox_customerName.SelectedItem.ToString()].Index;
            foreach (DataGridViewRow _row in this.dataGridView_customer.Rows)
            {
                if (_row.Cells[_columnIndex].Value == null || string.IsNullOrEmpty(_row.Cells[_columnIndex].Value.ToString()) || _row.Cells[_columnIndex].Value.ToString().Equals("Søren Parrot"))
                {
                    _row.Cells[_columnIndex].Style.BackColor = Color.Red;
                }
                
            }
        }

        private void button_validate_Click(object sender, EventArgs e)
        {
            this.WorkerFetcher.RunWorkerAsync();

        }

        private void WorkerFetcherDoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            foreach (DataGridViewRow _row in this.dataGridView_customer.Rows)
            {
                if (this.WorkerFetcher.CancellationPending)
                {
                    break;
                }

                CustomerModel _newCustomer = new CustomerModel();

                //_newCustomer.Name = _row.Cells[this.dataGridView1.Columns[this.comboBox_customer_name.SelectedItem.ToString()].Index].Value.ToString();
                _newCustomer.Name = _row.Cells[2].Value.ToString();

                var _response =  CustomerHandler.Instance.ValidateCustomer(_newCustomer, AuthenticationHandler.Instance._token);

                HandleApiResponse(_response, _row);
               
            }
        }


        private void HandleApiResponse(ApiResponse _response, DataGridViewRow _row)
        {
            if (_response.Code == 200)
            {
                this.Invoke((MethodInvoker)(() => _row.DefaultCellStyle.BackColor = Color.LimeGreen));
                this.Invoke((MethodInvoker)(() => this.textBox_customerImportMessages.AppendText(Environment.NewLine)));
                this.Invoke((MethodInvoker)(() => this.textBox_customerImportMessages.AppendText("Row " + _row.Index + _response.Message)));
            }
            else
            {
                this.Invoke((MethodInvoker)(() => _row.DefaultCellStyle.BackColor = Color.Red));

                this.Invoke((MethodInvoker)(() => this.textBox_customerImportMessages.AppendText(Environment.NewLine)));
                this.Invoke((MethodInvoker)(() => this.textBox_customerImportMessages.AppendText("Row " + _row.Index + ": " + _response.Message + " Details: " + string.Join(";", _response.Details))));


            }
        }

        private void button_stop_Click(object sender, EventArgs e)
        {
            this.WorkerFetcher.CancelAsync();
        }

        private void button_clear_Click(object sender, EventArgs e)
        {
            this.textBox_customerImportMessages.Text = String.Empty;
            this.dataGridView_customer.DataSource = null;
            this.dataGridView_customer.Rows.Clear();

            this.comboBox_customerName.Items.Clear();
            this.comboBox_customerName.ResetText();

        }

    }
}
