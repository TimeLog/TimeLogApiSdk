namespace TimeLog.DataImporter.UserControls
{
    partial class UserControl_Help
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
            System.Windows.Forms.ListViewGroup listViewGroup3 = new System.Windows.Forms.ListViewGroup("Glossary", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewItem listViewItem27 = new System.Windows.Forms.ListViewItem(" 1. Select the delimiter type used in the desired CSV file from the delimiter lis" +
        "t.");
            System.Windows.Forms.ListViewItem listViewItem28 = new System.Windows.Forms.ListViewItem("  ");
            System.Windows.Forms.ListViewItem listViewItem29 = new System.Windows.Forms.ListViewItem(" 2. Press \"Select File\" to choose the desired CSV file to be imported. File of ot" +
        "her extension types will not be accepted.");
            System.Windows.Forms.ListViewItem listViewItem30 = new System.Windows.Forms.ListViewItem("  ");
            System.Windows.Forms.ListViewItem listViewItem31 = new System.Windows.Forms.ListViewItem(" 3. Map earn neccessary column of the file content to the columns in the data tab" +
        "le below by choosing from the drop down list of each column.");
            System.Windows.Forms.ListViewItem listViewItem32 = new System.Windows.Forms.ListViewItem("  ");
            System.Windows.Forms.ListViewItem listViewItem33 = new System.Windows.Forms.ListViewItem(" 4. After mapping, press \"Validate\" to validate the mapped input data. The valida" +
        "tion results will be shown in the center container.");
            System.Windows.Forms.ListViewItem listViewItem34 = new System.Windows.Forms.ListViewItem("  ");
            System.Windows.Forms.ListViewItem listViewItem35 = new System.Windows.Forms.ListViewItem(" 5. If there is error in the mapped input data, the invalid data input row count " +
        "will not be zero.");
            System.Windows.Forms.ListViewItem listViewItem36 = new System.Windows.Forms.ListViewItem("  ");
            System.Windows.Forms.ListViewItem listViewItem37 = new System.Windows.Forms.ListViewItem(" 6. The user has to recheck and modify the input data by referring to the center " +
        "validation results container and then repeat Step 4.");
            System.Windows.Forms.ListViewItem listViewItem38 = new System.Windows.Forms.ListViewItem("  ");
            System.Windows.Forms.ListViewItem listViewItem39 = new System.Windows.Forms.ListViewItem(" 7. If the validation passes without any error, the user can press the \"Import\" b" +
        "utton to start the data import process.");
            System.Windows.Forms.ListViewGroup listViewGroup4 = new System.Windows.Forms.ListViewGroup("Glossary", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewItem listViewItem40 = new System.Windows.Forms.ListViewItem(" Customer > Import customer data from CSV file");
            System.Windows.Forms.ListViewItem listViewItem41 = new System.Windows.Forms.ListViewItem("  ");
            System.Windows.Forms.ListViewItem listViewItem42 = new System.Windows.Forms.ListViewItem(" Project > Import project data from CSV file");
            System.Windows.Forms.ListViewItem listViewItem43 = new System.Windows.Forms.ListViewItem("  ");
            System.Windows.Forms.ListViewItem listViewItem44 = new System.Windows.Forms.ListViewItem(" Delimiter > The separator used in the CSV file to separate each field");
            System.Windows.Forms.ListViewItem listViewItem45 = new System.Windows.Forms.ListViewItem("  ");
            System.Windows.Forms.ListViewItem listViewItem46 = new System.Windows.Forms.ListViewItem(" Select File > Select a CSV data source file ");
            System.Windows.Forms.ListViewItem listViewItem47 = new System.Windows.Forms.ListViewItem("  ");
            System.Windows.Forms.ListViewItem listViewItem48 = new System.Windows.Forms.ListViewItem(" Reset All > Reset all the data in the input area and data table below");
            System.Windows.Forms.ListViewItem listViewItem49 = new System.Windows.Forms.ListViewItem("  ");
            System.Windows.Forms.ListViewItem listViewItem50 = new System.Windows.Forms.ListViewItem(" Validate > Validate the input data mapped into the data table");
            System.Windows.Forms.ListViewItem listViewItem51 = new System.Windows.Forms.ListViewItem("  ");
            System.Windows.Forms.ListViewItem listViewItem52 = new System.Windows.Forms.ListViewItem(" Import > Import input data (will be disabled if validation is not done properly)" +
        "");
            this.panel_helpPage = new System.Windows.Forms.Panel();
            this.listView_instruction = new System.Windows.Forms.ListView();
            this.label_instruction = new System.Windows.Forms.Label();
            this.label_glossary = new System.Windows.Forms.Label();
            this.listView_glossary = new System.Windows.Forms.ListView();
            this.label_helpPage = new System.Windows.Forms.Label();
            this.panel_helpPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_helpPage
            // 
            this.panel_helpPage.AutoScroll = true;
            this.panel_helpPage.Controls.Add(this.listView_instruction);
            this.panel_helpPage.Controls.Add(this.label_instruction);
            this.panel_helpPage.Controls.Add(this.label_glossary);
            this.panel_helpPage.Controls.Add(this.listView_glossary);
            this.panel_helpPage.Controls.Add(this.label_helpPage);
            this.panel_helpPage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_helpPage.Location = new System.Drawing.Point(0, 0);
            this.panel_helpPage.Name = "panel_helpPage";
            this.panel_helpPage.Size = new System.Drawing.Size(1006, 942);
            this.panel_helpPage.TabIndex = 13;
            // 
            // listView_instruction
            // 
            this.listView_instruction.AutoArrange = false;
            this.listView_instruction.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.listView_instruction.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listView_instruction.Cursor = System.Windows.Forms.Cursors.Default;
            this.listView_instruction.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.listView_instruction.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.listView_instruction.FullRowSelect = true;
            listViewGroup3.Header = "Glossary";
            listViewGroup3.Name = "listViewGroup_glossary";
            this.listView_instruction.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup3});
            this.listView_instruction.HideSelection = false;
            listViewItem28.IndentCount = 1;
            listViewItem29.IndentCount = 2;
            listViewItem30.IndentCount = 3;
            listViewItem31.IndentCount = 4;
            listViewItem32.IndentCount = 5;
            listViewItem33.IndentCount = 6;
            listViewItem34.IndentCount = 7;
            listViewItem35.IndentCount = 8;
            listViewItem36.IndentCount = 9;
            listViewItem37.IndentCount = 10;
            listViewItem38.IndentCount = 11;
            listViewItem39.IndentCount = 12;
            this.listView_instruction.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem27,
            listViewItem28,
            listViewItem29,
            listViewItem30,
            listViewItem31,
            listViewItem32,
            listViewItem33,
            listViewItem34,
            listViewItem35,
            listViewItem36,
            listViewItem37,
            listViewItem38,
            listViewItem39});
            this.listView_instruction.Location = new System.Drawing.Point(13, 475);
            this.listView_instruction.MultiSelect = false;
            this.listView_instruction.Name = "listView_instruction";
            this.listView_instruction.Size = new System.Drawing.Size(977, 302);
            this.listView_instruction.TabIndex = 1;
            this.listView_instruction.TileSize = new System.Drawing.Size(977, 40);
            this.listView_instruction.UseCompatibleStateImageBehavior = false;
            this.listView_instruction.View = System.Windows.Forms.View.List;
            // 
            // label_instruction
            // 
            this.label_instruction.AutoSize = true;
            this.label_instruction.Font = new System.Drawing.Font("Segoe UI", 14.25F, ((System.Drawing.FontStyle)(((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic) 
                | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point);
            this.label_instruction.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label_instruction.Location = new System.Drawing.Point(13, 437);
            this.label_instruction.Name = "label_instruction";
            this.label_instruction.Size = new System.Drawing.Size(324, 25);
            this.label_instruction.TabIndex = 2;
            this.label_instruction.Text = "Instructions to Using Data Importer";
            // 
            // label_glossary
            // 
            this.label_glossary.AutoSize = true;
            this.label_glossary.Font = new System.Drawing.Font("Segoe UI", 14.25F, ((System.Drawing.FontStyle)(((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic) 
                | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point);
            this.label_glossary.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label_glossary.Location = new System.Drawing.Point(13, 75);
            this.label_glossary.Name = "label_glossary";
            this.label_glossary.Size = new System.Drawing.Size(87, 25);
            this.label_glossary.TabIndex = 2;
            this.label_glossary.Text = "Glossary";
            // 
            // listView_glossary
            // 
            this.listView_glossary.AutoArrange = false;
            this.listView_glossary.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.listView_glossary.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listView_glossary.Cursor = System.Windows.Forms.Cursors.Default;
            this.listView_glossary.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.listView_glossary.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.listView_glossary.FullRowSelect = true;
            listViewGroup4.Header = "Glossary";
            listViewGroup4.Name = "listViewGroup_glossary";
            this.listView_glossary.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup4});
            this.listView_glossary.HideSelection = false;
            listViewItem41.IndentCount = 1;
            listViewItem42.IndentCount = 2;
            listViewItem43.IndentCount = 3;
            listViewItem44.IndentCount = 4;
            listViewItem45.IndentCount = 5;
            listViewItem46.IndentCount = 6;
            listViewItem47.IndentCount = 7;
            listViewItem48.IndentCount = 8;
            listViewItem49.IndentCount = 9;
            listViewItem50.IndentCount = 10;
            listViewItem51.IndentCount = 11;
            listViewItem52.IndentCount = 12;
            this.listView_glossary.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem40,
            listViewItem41,
            listViewItem42,
            listViewItem43,
            listViewItem44,
            listViewItem45,
            listViewItem46,
            listViewItem47,
            listViewItem48,
            listViewItem49,
            listViewItem50,
            listViewItem51,
            listViewItem52});
            this.listView_glossary.Location = new System.Drawing.Point(13, 113);
            this.listView_glossary.MultiSelect = false;
            this.listView_glossary.Name = "listView_glossary";
            this.listView_glossary.Size = new System.Drawing.Size(977, 292);
            this.listView_glossary.TabIndex = 1;
            this.listView_glossary.TileSize = new System.Drawing.Size(977, 40);
            this.listView_glossary.UseCompatibleStateImageBehavior = false;
            this.listView_glossary.View = System.Windows.Forms.View.List;
            // 
            // label_helpPage
            // 
            this.label_helpPage.AutoSize = true;
            this.label_helpPage.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label_helpPage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label_helpPage.Location = new System.Drawing.Point(13, 16);
            this.label_helpPage.Name = "label_helpPage";
            this.label_helpPage.Size = new System.Drawing.Size(65, 32);
            this.label_helpPage.TabIndex = 0;
            this.label_helpPage.Text = "Help";
            // 
            // UserControl_Help
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel_helpPage);
            this.Name = "UserControl_Help";
            this.Size = new System.Drawing.Size(1006, 942);
            this.Load += new System.EventHandler(this.UserControl1_Load);
            this.panel_helpPage.ResumeLayout(false);
            this.panel_helpPage.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_helpPage;
        private System.Windows.Forms.Label label_helpPage;
        private System.Windows.Forms.Label label_glossary;
        private System.Windows.Forms.ListView listView_glossary;
        private System.Windows.Forms.ListView listView_instruction;
        private System.Windows.Forms.Label label_instruction;
    }
}
