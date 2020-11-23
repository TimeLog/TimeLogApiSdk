namespace TimeLog.DataImporter
{
    partial class Login
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            this.panel_login = new System.Windows.Forms.Panel();
            this.label_loginText = new System.Windows.Forms.Label();
            this.label_login = new System.Windows.Forms.Label();
            this.button_login = new System.Windows.Forms.Button();
            this.panel_login.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_login
            // 
            this.panel_login.Controls.Add(this.label_loginText);
            this.panel_login.Controls.Add(this.label_login);
            this.panel_login.Controls.Add(this.button_login);
            this.panel_login.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_login.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.panel_login.Location = new System.Drawing.Point(0, 0);
            this.panel_login.Name = "panel_login";
            this.panel_login.Size = new System.Drawing.Size(990, 903);
            this.panel_login.TabIndex = 0;
            // 
            // label_loginText
            // 
            this.label_loginText.AutoSize = true;
            this.label_loginText.BackColor = System.Drawing.Color.Transparent;
            this.label_loginText.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label_loginText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label_loginText.Location = new System.Drawing.Point(217, 329);
            this.label_loginText.Name = "label_loginText";
            this.label_loginText.Size = new System.Drawing.Size(573, 25);
            this.label_loginText.TabIndex = 2;
            this.label_loginText.Text = "Please press the Login button to be redirected to the Login Portal.";
            // 
            // label_login
            // 
            this.label_login.AutoSize = true;
            this.label_login.BackColor = System.Drawing.Color.Transparent;
            this.label_login.Font = new System.Drawing.Font("Segoe UI Semibold", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label_login.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label_login.Location = new System.Drawing.Point(183, 182);
            this.label_login.Name = "label_login";
            this.label_login.Size = new System.Drawing.Size(648, 50);
            this.label_login.TabIndex = 1;
            this.label_login.Text = "Welcome to TimeLog Data Importer !";
            // 
            // button_login
            // 
            this.button_login.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(43)))), ((int)(((byte)(141)))));
            this.button_login.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_login.FlatAppearance.BorderSize = 0;
            this.button_login.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_login.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.button_login.ForeColor = System.Drawing.Color.White;
            this.button_login.Location = new System.Drawing.Point(412, 364);
            this.button_login.Margin = new System.Windows.Forms.Padding(10);
            this.button_login.Name = "button_login";
            this.button_login.Size = new System.Drawing.Size(174, 39);
            this.button_login.TabIndex = 0;
            this.button_login.Text = "Login";
            this.button_login.UseVisualStyleBackColor = false;
            this.button_login.Click += new System.EventHandler(this.LoginButton_Click);
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(990, 903);
            this.Controls.Add(this.panel_login);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Login";
            this.Text = "TimeLog Data Importer";
            this.panel_login.ResumeLayout(false);
            this.panel_login.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_login;
        private System.Windows.Forms.Button button_login;
        private System.Windows.Forms.Label label_login;
        private System.Windows.Forms.Label label_loginText;
    }
}
