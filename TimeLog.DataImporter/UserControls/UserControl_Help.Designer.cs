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
            System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("Glossary", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(" 1. Select the delimiter type used in the desired CSV file from the delimiter lis" +
        "t.");
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("  ");
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem(" 2. Press \"Select File\" to choose the desired CSV file to be imported. File of ot" +
        "her extension types will not be accepted.");
            System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem("  ");
            System.Windows.Forms.ListViewItem listViewItem5 = new System.Windows.Forms.ListViewItem(" 3. Map earn neccessary column of the file content to the columns in the data tab" +
        "le below by choosing from the drop down list of each column.");
            System.Windows.Forms.ListViewItem listViewItem6 = new System.Windows.Forms.ListViewItem("  ");
            System.Windows.Forms.ListViewItem listViewItem7 = new System.Windows.Forms.ListViewItem(" 4. After mapping, press \"Validate\" to validate the mapped input data. The valida" +
        "tion results will be shown in the center container.");
            System.Windows.Forms.ListViewItem listViewItem8 = new System.Windows.Forms.ListViewItem("  ");
            System.Windows.Forms.ListViewItem listViewItem9 = new System.Windows.Forms.ListViewItem(" 5. If there is error in the mapped input data, the invalid data input row count " +
        "will not be zero.");
            System.Windows.Forms.ListViewItem listViewItem10 = new System.Windows.Forms.ListViewItem("  ");
            System.Windows.Forms.ListViewItem listViewItem11 = new System.Windows.Forms.ListViewItem(" 6. The user has to recheck and modify the input data by referring to the center " +
        "validation results container and then repeat Step 4.");
            System.Windows.Forms.ListViewItem listViewItem12 = new System.Windows.Forms.ListViewItem("  ");
            System.Windows.Forms.ListViewItem listViewItem13 = new System.Windows.Forms.ListViewItem(" 7. If the validation passes without any error, the user can press the \"Import\" b" +
        "utton to start the data import process.");
            System.Windows.Forms.ListViewGroup listViewGroup2 = new System.Windows.Forms.ListViewGroup("Glossary", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewItem listViewItem14 = new System.Windows.Forms.ListViewItem(" Customer > Import customer data from CSV file");
            System.Windows.Forms.ListViewItem listViewItem15 = new System.Windows.Forms.ListViewItem("  ");
            System.Windows.Forms.ListViewItem listViewItem16 = new System.Windows.Forms.ListViewItem(" Project > Import project data from CSV file");
            System.Windows.Forms.ListViewItem listViewItem17 = new System.Windows.Forms.ListViewItem("  ");
            System.Windows.Forms.ListViewItem listViewItem18 = new System.Windows.Forms.ListViewItem(" Delimiter > The separator used in the CSV file to separate each field");
            System.Windows.Forms.ListViewItem listViewItem19 = new System.Windows.Forms.ListViewItem("  ");
            System.Windows.Forms.ListViewItem listViewItem20 = new System.Windows.Forms.ListViewItem(" Select File > Select a CSV data source file ");
            System.Windows.Forms.ListViewItem listViewItem21 = new System.Windows.Forms.ListViewItem("  ");
            System.Windows.Forms.ListViewItem listViewItem22 = new System.Windows.Forms.ListViewItem(" Reset All > Reset all the data in the input area and data table below");
            System.Windows.Forms.ListViewItem listViewItem23 = new System.Windows.Forms.ListViewItem("  ");
            System.Windows.Forms.ListViewItem listViewItem24 = new System.Windows.Forms.ListViewItem(" Validate > Validate the input data mapped into the data table");
            System.Windows.Forms.ListViewItem listViewItem25 = new System.Windows.Forms.ListViewItem("  ");
            System.Windows.Forms.ListViewItem listViewItem26 = new System.Windows.Forms.ListViewItem(" Import > Import input data (will be disabled if validation is not done properly)" +
        "");
            this.panel_projectFieldMapping = new System.Windows.Forms.Panel();
            this.listView_instruction = new System.Windows.Forms.ListView();
            this.label_instruction = new System.Windows.Forms.Label();
            this.label_glossary = new System.Windows.Forms.Label();
            this.listView_glossary = new System.Windows.Forms.ListView();
            this.label_helpPage = new System.Windows.Forms.Label();
            this.panel_projectFieldMapping.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_projectFieldMapping
            // 
            this.panel_projectFieldMapping.AutoScroll = true;
            this.panel_projectFieldMapping.Controls.Add(this.listView_instruction);
            this.panel_projectFieldMapping.Controls.Add(this.label_instruction);
            this.panel_projectFieldMapping.Controls.Add(this.label_glossary);
            this.panel_projectFieldMapping.Controls.Add(this.listView_glossary);
            this.panel_projectFieldMapping.Controls.Add(this.label_helpPage);
            this.panel_projectFieldMapping.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_projectFieldMapping.Location = new System.Drawing.Point(0, 0);
            this.panel_projectFieldMapping.Name = "panel_projectFieldMapping";
            this.panel_projectFieldMapping.Size = new System.Drawing.Size(1006, 942);
            this.panel_projectFieldMapping.TabIndex = 13;
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
            listViewGroup1.Header = "Glossary";
            listViewGroup1.Name = "listViewGroup_glossary";
            this.listView_instruction.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1});
            this.listView_instruction.HideSelection = false;
            listViewItem2.IndentCount = 1;
            listViewItem3.IndentCount = 2;
            listViewItem4.IndentCount = 3;
            listViewItem5.IndentCount = 4;
            listViewItem6.IndentCount = 5;
            listViewItem7.IndentCount = 6;
            listViewItem8.IndentCount = 7;
            listViewItem9.IndentCount = 8;
            listViewItem10.IndentCount = 9;
            listViewItem11.IndentCount = 10;
            listViewItem12.IndentCount = 11;
            listViewItem13.IndentCount = 12;
            this.listView_instruction.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3,
            listViewItem4,
            listViewItem5,
            listViewItem6,
            listViewItem7,
            listViewItem8,
            listViewItem9,
            listViewItem10,
            listViewItem11,
            listViewItem12,
            listViewItem13});
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
            listViewGroup2.Header = "Glossary";
            listViewGroup2.Name = "listViewGroup_glossary";
            this.listView_glossary.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup2});
            this.listView_glossary.HideSelection = false;
            listViewItem15.IndentCount = 1;
            listViewItem16.IndentCount = 2;
            listViewItem17.IndentCount = 3;
            listViewItem18.IndentCount = 4;
            listViewItem19.IndentCount = 5;
            listViewItem20.IndentCount = 6;
            listViewItem21.IndentCount = 7;
            listViewItem22.IndentCount = 8;
            listViewItem23.IndentCount = 9;
            listViewItem24.IndentCount = 10;
            listViewItem25.IndentCount = 11;
            listViewItem26.IndentCount = 12;
            this.listView_glossary.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem14,
            listViewItem15,
            listViewItem16,
            listViewItem17,
            listViewItem18,
            listViewItem19,
            listViewItem20,
            listViewItem21,
            listViewItem22,
            listViewItem23,
            listViewItem24,
            listViewItem25,
            listViewItem26});
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
            this.Controls.Add(this.panel_projectFieldMapping);
            this.Name = "UserControl_Help";
            this.Size = new System.Drawing.Size(1006, 942);
            this.Load += new System.EventHandler(this.UserControl1_Load);
            this.panel_projectFieldMapping.ResumeLayout(false);
            this.panel_projectFieldMapping.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_projectFieldMapping;
        private System.Windows.Forms.Label label_helpPage;
        private System.Windows.Forms.Label label_glossary;
        private System.Windows.Forms.ListView listView_glossary;
        private System.Windows.Forms.ListView listView_instruction;
        private System.Windows.Forms.Label label_instruction;
    }
}
