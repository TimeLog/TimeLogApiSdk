using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms;
using IdentityModel.Client;
using IdentityModel.OidcClient;
using TimeLog.DataImporter.Handlers;
using TimeLog.DataImporter.TimeLogApi;

namespace TimeLog.DataImporter
{
    public partial class Main : Form
    {

        public Main()
        {
            InitializeComponent();
        }


        private void Main_Load(object sender, EventArgs e)
        {
        }


        


      

        //private void button_selectProjectFile_Click(object sender, EventArgs e)
        //{
        //    dataGridView1.DataSource = CustomerHandler.Instance.GetFileContent();
        //    comboBox_projectName.Items.AddRange(ProjectHandler.Instance._fileColumnHeaders.ToArray());

        //}


       

        //private void comboBox_projectName_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    var _columnIndex = this.dataGridView1.Columns[this.comboBox_projectName.SelectedItem.ToString()].Index;
        //    foreach (DataGridViewRow _row in this.dataGridView1.Rows)
        //    {
        //        if (_row.Cells[_columnIndex].Value == null || string.IsNullOrEmpty(_row.Cells[_columnIndex].Value.ToString()) || _row.Cells[_columnIndex].Value.ToString().Equals("Søren Parrot"))
        //        {
        //            _row.Cells[_columnIndex].Style.BackColor = Color.Red;
        //        }
                
        //    }
        //}

       

       
        private void tabPage_login_Click(object sender, EventArgs e)
        {

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}