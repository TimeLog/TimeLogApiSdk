using System;
using System.Windows.Forms;
using TimeLog.DataImporter.Handlers;

namespace TimeLog.DataImporter
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            panel_login.BackgroundImage = Properties.Resources.baggrund_min;
        }

        private async void LoginButton_Click(object sender, EventArgs e)
        {
            var _token = await AuthenticationHandler.Instance.Authenticate();

            if (!string.IsNullOrEmpty(_token))
            {
                Hide();
                var _mainForm = new Main();
                _mainForm.Closed += (s, args) => this.Close();
                _mainForm.Show();
            }
        }
    }
}
