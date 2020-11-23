using System;
using System.Windows.Forms;
using TimeLog.DataImporter.Handlers;

namespace TimeLog.DataImporter
{
    public partial class Login : Form
    {
        public static Main MainForm;

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

                if (MainForm == null)
                {
                    MainForm = new Main();
                    MainForm.Closed += (s, args) => Close();
                }

                MainForm.Show();
            }
        }
    }
}
