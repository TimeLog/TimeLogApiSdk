using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using TimeLog.DataImporter.Handlers;

namespace TimeLog.DataImporter.UserControls
{
    public partial class UserControl_Login : UserControl
    {
        public UserControl_Login()
        {
            InitializeComponent();
        }

        private async void LoginButton_Click(object sender, EventArgs e)
        {
            AuthenticationHandler.Instance.Authenticate();
        }
    }
}
