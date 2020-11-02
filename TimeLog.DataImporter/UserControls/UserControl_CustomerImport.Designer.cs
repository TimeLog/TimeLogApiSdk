namespace TimeLog.DataImporter.UserControls
{
    partial class UserControl_CustomerImport
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
            this.WorkerFetcher = new System.ComponentModel.BackgroundWorker();
            this.panel_customerDataTable = new System.Windows.Forms.Panel();
            this.dataGridView_customer = new System.Windows.Forms.DataGridView();
            this.panel_customerMessage = new System.Windows.Forms.Panel();
            this.textBox_customerImportMessages = new System.Windows.Forms.TextBox();
            this.panel_customerButtons = new System.Windows.Forms.Panel();
            this.button_clear = new System.Windows.Forms.Button();
            this.button_import = new System.Windows.Forms.Button();
            this.button_validate = new System.Windows.Forms.Button();
            this.button_stop = new System.Windows.Forms.Button();
            this.panel_customerFieldMapping = new System.Windows.Forms.Panel();
            this.groupBox_customerMandatoryFields = new System.Windows.Forms.GroupBox();
            this.label_customerName = new System.Windows.Forms.Label();
            this.comboBox_customerName = new System.Windows.Forms.ComboBox();
            this.label_customerSetup = new System.Windows.Forms.Label();
            this.button_customerSelectFile = new System.Windows.Forms.Button();
            this.panel_customerDataTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_customer)).BeginInit();
            this.panel_customerMessage.SuspendLayout();
            this.panel_customerButtons.SuspendLayout();
            this.panel_customerFieldMapping.SuspendLayout();
            this.groupBox_customerMandatoryFields.SuspendLayout();
            this.SuspendLayout();
            // 
            // WorkerFetcher
            // 
            this.WorkerFetcher.WorkerReportsProgress = true;
            this.WorkerFetcher.WorkerSupportsCancellation = true;
            this.WorkerFetcher.DoWork += new System.ComponentModel.DoWorkEventHandler(this.WorkerFetcherDoWork);
            // 
            // panel_customerDataTable
            // 
            this.panel_customerDataTable.Controls.Add(this.dataGridView_customer);
            this.panel_customerDataTable.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel_customerDataTable.Location = new System.Drawing.Point(0, 581);
            this.panel_customerDataTable.Name = "panel_customerDataTable";
            this.panel_customerDataTable.Size = new System.Drawing.Size(1006, 361);
            this.panel_customerDataTable.TabIndex = 6;
            // 
            // dataGridView_customer
            // 
            this.dataGridView_customer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_customer.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dataGridView_customer.Location = new System.Drawing.Point(0, -6);
            this.dataGridView_customer.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.dataGridView_customer.Name = "dataGridView_customer";
            this.dataGridView_customer.Size = new System.Drawing.Size(1006, 367);
            this.dataGridView_customer.TabIndex = 0;
            // 
            // panel_customerMessage
            // 
            this.panel_customerMessage.Controls.Add(this.textBox_customerImportMessages);
            this.panel_customerMessage.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel_customerMessage.Location = new System.Drawing.Point(0, 397);
            this.panel_customerMessage.Name = "panel_customerMessage";
            this.panel_customerMessage.Size = new System.Drawing.Size(1006, 184);
            this.panel_customerMessage.TabIndex = 10;
            // 
            // textBox_customerImportMessages
            // 
            this.textBox_customerImportMessages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox_customerImportMessages.Location = new System.Drawing.Point(0, 0);
            this.textBox_customerImportMessages.Multiline = true;
            this.textBox_customerImportMessages.Name = "textBox_customerImportMessages";
            this.textBox_customerImportMessages.ReadOnly = true;
            this.textBox_customerImportMessages.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox_customerImportMessages.Size = new System.Drawing.Size(1006, 184);
            this.textBox_customerImportMessages.TabIndex = 0;
            // 
            // panel_customerButtons
            // 
            this.panel_customerButtons.Controls.Add(this.button_clear);
            this.panel_customerButtons.Controls.Add(this.button_import);
            this.panel_customerButtons.Controls.Add(this.button_validate);
            this.panel_customerButtons.Controls.Add(this.button_stop);
            this.panel_customerButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel_customerButtons.Location = new System.Drawing.Point(0, 345);
            this.panel_customerButtons.Name = "panel_customerButtons";
            this.panel_customerButtons.Size = new System.Drawing.Size(1006, 52);
            this.panel_customerButtons.TabIndex = 12;
            // 
            // button_clear
            // 
            this.button_clear.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.button_clear.Location = new System.Drawing.Point(13, 16);
            this.button_clear.Name = "button_clear";
            this.button_clear.Size = new System.Drawing.Size(75, 23);
            this.button_clear.TabIndex = 12;
            this.button_clear.Text = "Clear";
            this.button_clear.UseVisualStyleBackColor = true;
            this.button_clear.Click += new System.EventHandler(this.button_clear_Click);
            // 
            // button_import
            // 
            this.button_import.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.button_import.Location = new System.Drawing.Point(912, 16);
            this.button_import.Name = "button_import";
            this.button_import.Size = new System.Drawing.Size(75, 23);
            this.button_import.TabIndex = 7;
            this.button_import.Text = "Import";
            this.button_import.UseVisualStyleBackColor = true;
            // 
            // button_validate
            // 
            this.button_validate.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.button_validate.Location = new System.Drawing.Point(750, 16);
            this.button_validate.Name = "button_validate";
            this.button_validate.Size = new System.Drawing.Size(75, 23);
            this.button_validate.TabIndex = 8;
            this.button_validate.Text = "Validate";
            this.button_validate.UseVisualStyleBackColor = true;
            this.button_validate.Click += new System.EventHandler(this.button_validate_Click);
            // 
            // button_stop
            // 
            this.button_stop.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.button_stop.Location = new System.Drawing.Point(831, 16);
            this.button_stop.Name = "button_stop";
            this.button_stop.Size = new System.Drawing.Size(75, 23);
            this.button_stop.TabIndex = 11;
            this.button_stop.Text = "Stop";
            this.button_stop.UseVisualStyleBackColor = true;
            this.button_stop.Click += new System.EventHandler(this.button_stop_Click);
            // 
            // panel_customerFieldMapping
            // 
            this.panel_customerFieldMapping.Controls.Add(this.groupBox_customerMandatoryFields);
            this.panel_customerFieldMapping.Controls.Add(this.label_customerSetup);
            this.panel_customerFieldMapping.Controls.Add(this.button_customerSelectFile);
            this.panel_customerFieldMapping.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_customerFieldMapping.Location = new System.Drawing.Point(0, 0);
            this.panel_customerFieldMapping.Name = "panel_customerFieldMapping";
            this.panel_customerFieldMapping.Size = new System.Drawing.Size(1006, 345);
            this.panel_customerFieldMapping.TabIndex = 13;
            // 
            // groupBox_customerMandatoryFields
            // 
            this.groupBox_customerMandatoryFields.Controls.Add(this.label_customerName);
            this.groupBox_customerMandatoryFields.Controls.Add(this.comboBox_customerName);
            this.groupBox_customerMandatoryFields.Location = new System.Drawing.Point(15, 97);
            this.groupBox_customerMandatoryFields.Name = "groupBox_customerMandatoryFields";
            this.groupBox_customerMandatoryFields.Size = new System.Drawing.Size(269, 227);
            this.groupBox_customerMandatoryFields.TabIndex = 5;
            this.groupBox_customerMandatoryFields.TabStop = false;
            this.groupBox_customerMandatoryFields.Text = "Mandatory";
            // 
            // label_customerName
            // 
            this.label_customerName.AutoSize = true;
            this.label_customerName.Location = new System.Drawing.Point(16, 29);
            this.label_customerName.Name = "label_customerName";
            this.label_customerName.Size = new System.Drawing.Size(97, 15);
            this.label_customerName.TabIndex = 1;
            this.label_customerName.Text = "Customer Name:";
            // 
            // comboBox_customerName
            // 
            this.comboBox_customerName.FormattingEnabled = true;
            this.comboBox_customerName.Location = new System.Drawing.Point(129, 22);
            this.comboBox_customerName.Name = "comboBox_customerName";
            this.comboBox_customerName.Size = new System.Drawing.Size(121, 23);
            this.comboBox_customerName.TabIndex = 3;
            this.comboBox_customerName.SelectedIndexChanged += new System.EventHandler(this.comboBox_customerName_SelectedIndexChanged);
            // 
            // label_customerSetup
            // 
            this.label_customerSetup.AutoSize = true;
            this.label_customerSetup.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label_customerSetup.Location = new System.Drawing.Point(13, 19);
            this.label_customerSetup.Name = "label_customerSetup";
            this.label_customerSetup.Size = new System.Drawing.Size(76, 32);
            this.label_customerSetup.TabIndex = 0;
            this.label_customerSetup.Text = "Setup";
            // 
            // button_customerSelectFile
            // 
            this.button_customerSelectFile.Location = new System.Drawing.Point(11, 51);
            this.button_customerSelectFile.Name = "button_customerSelectFile";
            this.button_customerSelectFile.Size = new System.Drawing.Size(75, 23);
            this.button_customerSelectFile.TabIndex = 4;
            this.button_customerSelectFile.Text = "Select file";
            this.button_customerSelectFile.UseVisualStyleBackColor = true;
            this.button_customerSelectFile.Click += new System.EventHandler(this.button_select_customer_file_Click);
            // 
            // UserControl_CustomerImport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel_customerFieldMapping);
            this.Controls.Add(this.panel_customerButtons);
            this.Controls.Add(this.panel_customerMessage);
            this.Controls.Add(this.panel_customerDataTable);
            this.Name = "UserControl_CustomerImport";
            this.Size = new System.Drawing.Size(1006, 942);
            this.Load += new System.EventHandler(this.UserControl1_Load);
            this.panel_customerDataTable.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_customer)).EndInit();
            this.panel_customerMessage.ResumeLayout(false);
            this.panel_customerMessage.PerformLayout();
            this.panel_customerButtons.ResumeLayout(false);
            this.panel_customerFieldMapping.ResumeLayout(false);
            this.panel_customerFieldMapping.PerformLayout();
            this.groupBox_customerMandatoryFields.ResumeLayout(false);
            this.groupBox_customerMandatoryFields.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion


        private void AddRowNumberToDataTable()
        {
            this.dataGridView_customer.DataBindingComplete += (o, e) =>
            {
                foreach (System.Windows.Forms.DataGridViewRow row in dataGridView_customer.Rows)
                {
                    row.HeaderCell.Value = (row.Index + 1).ToString();

                }
            };
            this.dataGridView_customer.RowHeadersWidth = 65;
        }


        private System.ComponentModel.BackgroundWorker WorkerFetcher;
        private System.Windows.Forms.Panel panel_customerDataTable;
        private System.Windows.Forms.DataGridView dataGridView_customer;
        private System.Windows.Forms.Panel panel_customerMessage;
        private System.Windows.Forms.Panel panel_customerButtons;
        private System.Windows.Forms.Button button_clear;
        private System.Windows.Forms.Button button_import;
        private System.Windows.Forms.Button button_validate;
        private System.Windows.Forms.Button button_stop;
        private System.Windows.Forms.Panel panel_customerFieldMapping;
        private System.Windows.Forms.GroupBox groupBox_customerMandatoryFields;
        private System.Windows.Forms.ComboBox comboBox_customerName;
        private System.Windows.Forms.Label label_customerName;
        private System.Windows.Forms.Label label_customerSetup;
        private System.Windows.Forms.Button button_customerSelectFile;
        private System.Windows.Forms.TextBox textBox_customerImportMessages;
    }
}
