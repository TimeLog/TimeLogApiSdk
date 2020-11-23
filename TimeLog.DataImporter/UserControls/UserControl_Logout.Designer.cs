namespace TimeLog.DataImporter.UserControls
{
    partial class UserControl_Logout
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
            this.panel_logout = new System.Windows.Forms.Panel();
            this.label_logoutText = new System.Windows.Forms.Label();
            this.label_logout = new System.Windows.Forms.Label();
            this.button_logout = new System.Windows.Forms.Button();
            this.panel_logout.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_logout
            // 
            this.panel_logout.Controls.Add(this.label_logoutText);
            this.panel_logout.Controls.Add(this.label_logout);
            this.panel_logout.Controls.Add(this.button_logout);
            this.panel_logout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_logout.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.panel_logout.Location = new System.Drawing.Point(0, 0);
            this.panel_logout.Name = "panel_logout";
            this.panel_logout.Size = new System.Drawing.Size(1006, 942);
            this.panel_logout.TabIndex = 0;
            // 
            // label_logoutText
            // 
            this.label_logoutText.AutoSize = true;
            this.label_logoutText.BackColor = System.Drawing.Color.Transparent;
            this.label_logoutText.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label_logoutText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label_logoutText.Location = new System.Drawing.Point(257, 329);
            this.label_logoutText.Name = "label_logoutText";
            this.label_logoutText.Size = new System.Drawing.Size(491, 25);
            this.label_logoutText.TabIndex = 2;
            this.label_logoutText.Text = "Please press the Logout button to log out of the system.";
            // 
            // label_logout
            // 
            this.label_logout.AutoSize = true;
            this.label_logout.BackColor = System.Drawing.Color.Transparent;
            this.label_logout.Font = new System.Drawing.Font("Segoe UI Semibold", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label_logout.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label_logout.Location = new System.Drawing.Point(144, 182);
            this.label_logout.Name = "label_logout";
            this.label_logout.Size = new System.Drawing.Size(729, 50);
            this.label_logout.TabIndex = 1;
            this.label_logout.Text = "Thanks for Using TimeLog Data Importer !";
            // 
            // button_logout
            // 
            this.button_logout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(43)))), ((int)(((byte)(141)))));
            this.button_logout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_logout.FlatAppearance.BorderSize = 0;
            this.button_logout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_logout.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.button_logout.ForeColor = System.Drawing.Color.White;
            this.button_logout.Location = new System.Drawing.Point(409, 364);
            this.button_logout.Margin = new System.Windows.Forms.Padding(10);
            this.button_logout.Name = "button_logout";
            this.button_logout.Size = new System.Drawing.Size(174, 39);
            this.button_logout.TabIndex = 0;
            this.button_logout.Text = "Logout";
            this.button_logout.UseVisualStyleBackColor = false;
            this.button_logout.Click += new System.EventHandler(this.button_logout_Click);
            // 
            // UserControl_Logout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel_logout);
            this.Name = "UserControl_Logout";
            this.Size = new System.Drawing.Size(1006, 942);
            this.panel_logout.ResumeLayout(false);
            this.panel_logout.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_logout;
        private System.Windows.Forms.Button button_logout;
        private System.Windows.Forms.Label label_logoutText;
        private System.Windows.Forms.Label label_logout;
    }
}