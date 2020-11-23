using System.Windows.Forms;
using TimeLog.DataImporter.UserControls;

namespace TimeLog.DataImporter
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage_customer = new System.Windows.Forms.TabPage();
            this.userControl_CustomerImport1 = new UserControl_CustomerImport();
            this.tabPage_project = new System.Windows.Forms.TabPage();
            this.userControl_ProjectImport1 = new UserControl_ProjectImport();
            this.tabPage_help = new System.Windows.Forms.TabPage();
            this.userControl_Help1 = new UserControl_Help();
            this.tabPage_logout = new System.Windows.Forms.TabPage();
            this.userControl_Logout1 = new UserControl_Logout();
            this.tabControl1.SuspendLayout();
            this.tabPage_customer.SuspendLayout();
            this.tabPage_project.SuspendLayout();
            this.tabPage_help.SuspendLayout();
            this.tabPage_logout.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage_customer);
            this.tabControl1.Controls.Add(this.tabPage_project);
            this.tabControl1.Controls.Add(this.tabPage_help);
            this.tabControl1.Controls.Add(this.tabPage_logout);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1006, 942);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage_customer
            // 
            this.tabPage_customer.AutoScroll = true;
            this.tabPage_customer.Controls.Add(this.userControl_CustomerImport1);
            this.tabPage_customer.Location = new System.Drawing.Point(4, 24);
            this.tabPage_customer.Name = "tabPage_customer";
            this.tabPage_customer.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_customer.Size = new System.Drawing.Size(998, 914);
            this.tabPage_customer.TabIndex = 0;
            this.tabPage_customer.Text = "Customer";
            this.tabPage_customer.UseVisualStyleBackColor = true;
            // 
            // userControl_CustomerImport1
            // 
            this.userControl_CustomerImport1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.userControl_CustomerImport1.Location = new System.Drawing.Point(3, 3);
            this.userControl_CustomerImport1.Name = "userControl_CustomerImport1";
            this.userControl_CustomerImport1.Size = new System.Drawing.Size(992, 908);
            this.userControl_CustomerImport1.TabIndex = 0;
            // 
            // tabPage_project
            // 
            this.tabPage_project.AutoScroll = true;
            this.tabPage_project.Controls.Add(this.userControl_ProjectImport1);
            this.tabPage_project.Location = new System.Drawing.Point(4, 24);
            this.tabPage_project.Name = "tabPage_project";
            this.tabPage_project.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_project.Size = new System.Drawing.Size(998, 914);
            this.tabPage_project.TabIndex = 1;
            this.tabPage_project.Text = "Project";
            this.tabPage_project.UseVisualStyleBackColor = true;
            // 
            // userControl_ProjectImport1
            // 
            this.userControl_ProjectImport1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.userControl_ProjectImport1.Location = new System.Drawing.Point(3, 3);
            this.userControl_ProjectImport1.Name = "userControl_ProjectImport1";
            this.userControl_ProjectImport1.Size = new System.Drawing.Size(992, 908);
            this.userControl_ProjectImport1.TabIndex = 0;
            // 
            // tabPage_help
            // 
            this.tabPage_help.AutoScroll = true;
            this.tabPage_help.Controls.Add(this.userControl_Help1);
            this.tabPage_help.Location = new System.Drawing.Point(4, 24);
            this.tabPage_help.Name = "tabPage_help";
            this.tabPage_help.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_help.Size = new System.Drawing.Size(998, 914);
            this.tabPage_help.TabIndex = 2;
            this.tabPage_help.Text = "Help";
            this.tabPage_help.UseVisualStyleBackColor = true;
            // 
            // userControl_Help1
            // 
            this.userControl_Help1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.userControl_Help1.Location = new System.Drawing.Point(3, 3);
            this.userControl_Help1.Name = "userControl_Help1";
            this.userControl_Help1.Size = new System.Drawing.Size(992, 908);
            this.userControl_Help1.TabIndex = 0;
            // 
            // tabPage_logout
            // 
            this.tabPage_logout.AutoScroll = true;
            this.tabPage_logout.Controls.Add(this.userControl_Logout1);
            this.tabPage_logout.Location = new System.Drawing.Point(4, 24);
            this.tabPage_logout.Name = "tabPage_logout";
            this.tabPage_logout.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_logout.Size = new System.Drawing.Size(998, 914);
            this.tabPage_logout.TabIndex = 3;
            this.tabPage_logout.Text = "Logout";
            this.tabPage_help.UseVisualStyleBackColor = true;
            // 
            // userControl_Logout1
            // 
            this.userControl_Logout1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.userControl_Logout1.Location = new System.Drawing.Point(3, 3);
            this.userControl_Logout1.Name = "userControl_Logout1";
            this.userControl_Logout1.Size = new System.Drawing.Size(992, 908);
            this.userControl_Logout1.TabIndex = 0;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1006, 942);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Main";
            this.Text = "TimeLog Data Importer";
            this.Load += new System.EventHandler(this.Main_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage_customer.ResumeLayout(false);
            this.tabPage_project.ResumeLayout(false);
            this.tabPage_help.ResumeLayout(false);
            this.tabPage_logout.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public TabControl tabControl1;
        public TabPage tabPage_customer;
        public TabPage tabPage_project;
        public TabPage tabPage_help;
        public TabPage tabPage_logout;
        public UserControl_CustomerImport userControl_CustomerImport1;
        public UserControl_ProjectImport userControl_ProjectImport1;
        public UserControl_Help userControl_Help1;
        public UserControl_Logout userControl_Logout1;
    }
}