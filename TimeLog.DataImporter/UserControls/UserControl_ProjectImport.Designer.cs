namespace TimeLog.DataImporter.UserControls
{
    partial class UserControl_ProjectImport
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
            this.panel_projectDataTable = new System.Windows.Forms.Panel();
            this.dataGridView_project = new System.Windows.Forms.DataGridView();
            this.panel_projectMessage = new System.Windows.Forms.Panel();
            this.textBox_projectImportMessages = new System.Windows.Forms.TextBox();
            this.panel_projectButtons = new System.Windows.Forms.Panel();
            this.button_clear = new System.Windows.Forms.Button();
            this.button_import = new System.Windows.Forms.Button();
            this.button_validate = new System.Windows.Forms.Button();
            this.button_stop = new System.Windows.Forms.Button();
            this.panel_projectFieldMapping = new System.Windows.Forms.Panel();
            this.groupBox_projectMandatoryFields = new System.Windows.Forms.GroupBox();
            this.label_projectLegalEntity = new System.Windows.Forms.Label();
            this.comboBox_projectLegalEntity = new System.Windows.Forms.ComboBox();
            this.label_projectCurrency = new System.Windows.Forms.Label();
            this.comboBox_projectCurrency = new System.Windows.Forms.ComboBox();
            this.label_projectManager = new System.Windows.Forms.Label();
            this.comboBox_projectManager = new System.Windows.Forms.ComboBox();
            this.label_projectTemplate = new System.Windows.Forms.Label();
            this.comboBox_projectTemplate = new System.Windows.Forms.ComboBox();
            this.label_projectCustomer = new System.Windows.Forms.Label();
            this.comboBox_projectCustomer = new System.Windows.Forms.ComboBox();
            this.label_projectName = new System.Windows.Forms.Label();
            this.comboBox_projectName = new System.Windows.Forms.ComboBox();
            this.label_projectSetup = new System.Windows.Forms.Label();
            this.button_projectSelectFile = new System.Windows.Forms.Button();
            this.panel_projectDataTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_project)).BeginInit();
            this.panel_projectMessage.SuspendLayout();
            this.panel_projectButtons.SuspendLayout();
            this.panel_projectFieldMapping.SuspendLayout();
            this.groupBox_projectMandatoryFields.SuspendLayout();
            this.SuspendLayout();
            // 
            // WorkerFetcher
            // 
            this.WorkerFetcher.WorkerReportsProgress = true;
            this.WorkerFetcher.WorkerSupportsCancellation = true;
            this.WorkerFetcher.DoWork += new System.ComponentModel.DoWorkEventHandler(this.WorkerFetcherDoWork);
            // 
            // panel_projectDataTable
            // 
            this.panel_projectDataTable.Controls.Add(this.dataGridView_project);
            this.panel_projectDataTable.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel_projectDataTable.Location = new System.Drawing.Point(0, 581);
            this.panel_projectDataTable.Name = "panel_projectDataTable";
            this.panel_projectDataTable.Size = new System.Drawing.Size(1006, 361);
            this.panel_projectDataTable.TabIndex = 6;
            // 
            // dataGridView_project
            // 
            this.dataGridView_project.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_project.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dataGridView_project.Location = new System.Drawing.Point(0, -6);
            this.dataGridView_project.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.dataGridView_project.Name = "dataGridView_project";
            this.dataGridView_project.Size = new System.Drawing.Size(1006, 367);
            this.dataGridView_project.TabIndex = 0;
            // 
            // panel_projectMessage
            // 
            this.panel_projectMessage.Controls.Add(this.textBox_projectImportMessages);
            this.panel_projectMessage.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel_projectMessage.Location = new System.Drawing.Point(0, 397);
            this.panel_projectMessage.Name = "panel_projectMessage";
            this.panel_projectMessage.Size = new System.Drawing.Size(1006, 184);
            this.panel_projectMessage.TabIndex = 10;
            // 
            // textBox_projectImportMessages
            // 
            this.textBox_projectImportMessages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox_projectImportMessages.Location = new System.Drawing.Point(0, 0);
            this.textBox_projectImportMessages.Multiline = true;
            this.textBox_projectImportMessages.Name = "textBox_projectImportMessages";
            this.textBox_projectImportMessages.ReadOnly = true;
            this.textBox_projectImportMessages.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox_projectImportMessages.Size = new System.Drawing.Size(1006, 184);
            this.textBox_projectImportMessages.TabIndex = 0;
            // 
            // panel_projectButtons
            // 
            this.panel_projectButtons.Controls.Add(this.button_clear);
            this.panel_projectButtons.Controls.Add(this.button_import);
            this.panel_projectButtons.Controls.Add(this.button_validate);
            this.panel_projectButtons.Controls.Add(this.button_stop);
            this.panel_projectButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel_projectButtons.Location = new System.Drawing.Point(0, 345);
            this.panel_projectButtons.Name = "panel_projectButtons";
            this.panel_projectButtons.Size = new System.Drawing.Size(1006, 52);
            this.panel_projectButtons.TabIndex = 12;
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
            // panel_projectFieldMapping
            // 
            this.panel_projectFieldMapping.Controls.Add(this.groupBox_projectMandatoryFields);
            this.panel_projectFieldMapping.Controls.Add(this.label_projectSetup);
            this.panel_projectFieldMapping.Controls.Add(this.button_projectSelectFile);
            this.panel_projectFieldMapping.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_projectFieldMapping.Location = new System.Drawing.Point(0, 0);
            this.panel_projectFieldMapping.Name = "panel_projectFieldMapping";
            this.panel_projectFieldMapping.Size = new System.Drawing.Size(1006, 345);
            this.panel_projectFieldMapping.TabIndex = 13;
            // 
            // groupBox_projectMandatoryFields
            // 
            this.groupBox_projectMandatoryFields.Controls.Add(this.label_projectLegalEntity);
            this.groupBox_projectMandatoryFields.Controls.Add(this.comboBox_projectLegalEntity);
            this.groupBox_projectMandatoryFields.Controls.Add(this.label_projectCurrency);
            this.groupBox_projectMandatoryFields.Controls.Add(this.comboBox_projectCurrency);
            this.groupBox_projectMandatoryFields.Controls.Add(this.label_projectManager);
            this.groupBox_projectMandatoryFields.Controls.Add(this.comboBox_projectManager);
            this.groupBox_projectMandatoryFields.Controls.Add(this.label_projectTemplate);
            this.groupBox_projectMandatoryFields.Controls.Add(this.comboBox_projectTemplate);
            this.groupBox_projectMandatoryFields.Controls.Add(this.label_projectCustomer);
            this.groupBox_projectMandatoryFields.Controls.Add(this.comboBox_projectCustomer);
            this.groupBox_projectMandatoryFields.Controls.Add(this.label_projectName);
            this.groupBox_projectMandatoryFields.Controls.Add(this.comboBox_projectName);
            this.groupBox_projectMandatoryFields.Location = new System.Drawing.Point(15, 99);
            this.groupBox_projectMandatoryFields.Name = "groupBox_projectMandatoryFields";
            this.groupBox_projectMandatoryFields.Size = new System.Drawing.Size(266, 201);
            this.groupBox_projectMandatoryFields.TabIndex = 5;
            this.groupBox_projectMandatoryFields.TabStop = false;
            this.groupBox_projectMandatoryFields.Text = "Mandatory";
            // 
            // label_projectLegalEntity
            // 
            this.label_projectLegalEntity.AutoSize = true;
            this.label_projectLegalEntity.Location = new System.Drawing.Point(6, 163);
            this.label_projectLegalEntity.Name = "label_projectLegalEntity";
            this.label_projectLegalEntity.Size = new System.Drawing.Size(111, 15);
            this.label_projectLegalEntity.TabIndex = 1;
            this.label_projectLegalEntity.Text = "Project Legal Entity:";
            // 
            // comboBox_projectLegalEntity
            // 
            this.comboBox_projectLegalEntity.FormattingEnabled = true;
            this.comboBox_projectLegalEntity.Location = new System.Drawing.Point(123, 159);
            this.comboBox_projectLegalEntity.Name = "comboBox_projectLegalEntity";
            this.comboBox_projectLegalEntity.Size = new System.Drawing.Size(121, 23);
            this.comboBox_projectLegalEntity.TabIndex = 3;
            this.comboBox_projectLegalEntity.SelectedIndexChanged += new System.EventHandler(this.comboBox_projectLegalEntity_SelectedIndexChanged);
            // 
            // label_projectCurrency
            // 
            this.label_projectCurrency.AutoSize = true;
            this.label_projectCurrency.Location = new System.Drawing.Point(6, 135);
            this.label_projectCurrency.Name = "label_projectCurrency";
            this.label_projectCurrency.Size = new System.Drawing.Size(98, 15);
            this.label_projectCurrency.TabIndex = 1;
            this.label_projectCurrency.Text = "Project Currency:";
            // 
            // comboBox_projectCurrency
            // 
            this.comboBox_projectCurrency.FormattingEnabled = true;
            this.comboBox_projectCurrency.Location = new System.Drawing.Point(123, 131);
            this.comboBox_projectCurrency.Name = "comboBox_projectCurrency";
            this.comboBox_projectCurrency.Size = new System.Drawing.Size(121, 23);
            this.comboBox_projectCurrency.TabIndex = 3;
            this.comboBox_projectCurrency.SelectedIndexChanged += new System.EventHandler(this.comboBox_projectCurrency_SelectedIndexChanged);
            // 
            // label_projectManager
            // 
            this.label_projectManager.AutoSize = true;
            this.label_projectManager.Location = new System.Drawing.Point(6, 106);
            this.label_projectManager.Name = "label_projectManager";
            this.label_projectManager.Size = new System.Drawing.Size(97, 15);
            this.label_projectManager.TabIndex = 1;
            this.label_projectManager.Text = "Project Manager:";
            // 
            // comboBox_projectManager
            // 
            this.comboBox_projectManager.FormattingEnabled = true;
            this.comboBox_projectManager.Location = new System.Drawing.Point(123, 102);
            this.comboBox_projectManager.Name = "comboBox_projectManager";
            this.comboBox_projectManager.Size = new System.Drawing.Size(121, 23);
            this.comboBox_projectManager.TabIndex = 3;
            this.comboBox_projectManager.SelectedIndexChanged += new System.EventHandler(this.comboBox_projectManager_SelectedIndexChanged);
            // 
            // label_projectTemplate
            // 
            this.label_projectTemplate.AutoSize = true;
            this.label_projectTemplate.Location = new System.Drawing.Point(6, 77);
            this.label_projectTemplate.Name = "label_projectTemplate";
            this.label_projectTemplate.Size = new System.Drawing.Size(99, 15);
            this.label_projectTemplate.TabIndex = 1;
            this.label_projectTemplate.Text = "Project Template:";
            // 
            // comboBox_projectTemplate
            // 
            this.comboBox_projectTemplate.FormattingEnabled = true;
            this.comboBox_projectTemplate.Location = new System.Drawing.Point(123, 73);
            this.comboBox_projectTemplate.Name = "comboBox_projectTemplate";
            this.comboBox_projectTemplate.Size = new System.Drawing.Size(121, 23);
            this.comboBox_projectTemplate.TabIndex = 3;
            this.comboBox_projectTemplate.SelectedIndexChanged += new System.EventHandler(this.comboBox_projectTemplate_SelectedIndexChanged);
            // 
            // label_projectCustomer
            // 
            this.label_projectCustomer.AutoSize = true;
            this.label_projectCustomer.Location = new System.Drawing.Point(6, 48);
            this.label_projectCustomer.Name = "label_projectCustomer";
            this.label_projectCustomer.Size = new System.Drawing.Size(62, 15);
            this.label_projectCustomer.TabIndex = 1;
            this.label_projectCustomer.Text = "Customer:";
            // 
            // comboBox_projectCustomer
            // 
            this.comboBox_projectCustomer.FormattingEnabled = true;
            this.comboBox_projectCustomer.Location = new System.Drawing.Point(123, 44);
            this.comboBox_projectCustomer.Name = "comboBox_projectCustomer";
            this.comboBox_projectCustomer.Size = new System.Drawing.Size(121, 23);
            this.comboBox_projectCustomer.TabIndex = 3;
            this.comboBox_projectCustomer.SelectedIndexChanged += new System.EventHandler(this.comboBox_projectCustomer_SelectedIndexChanged);
            // 
            // label_projectName
            // 
            this.label_projectName.AutoSize = true;
            this.label_projectName.Location = new System.Drawing.Point(6, 19);
            this.label_projectName.Name = "label_projectName";
            this.label_projectName.Size = new System.Drawing.Size(82, 15);
            this.label_projectName.TabIndex = 1;
            this.label_projectName.Text = "Project Name:";
            // 
            // comboBox_projectName
            // 
            this.comboBox_projectName.FormattingEnabled = true;
            this.comboBox_projectName.Location = new System.Drawing.Point(123, 15);
            this.comboBox_projectName.Name = "comboBox_projectName";
            this.comboBox_projectName.Size = new System.Drawing.Size(121, 23);
            this.comboBox_projectName.TabIndex = 3;
            this.comboBox_projectName.SelectedIndexChanged += new System.EventHandler(this.comboBox_projectName_SelectedIndexChanged);
            // 
            // label_projectSetup
            // 
            this.label_projectSetup.AutoSize = true;
            this.label_projectSetup.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label_projectSetup.Location = new System.Drawing.Point(13, 22);
            this.label_projectSetup.Name = "label_projectSetup";
            this.label_projectSetup.Size = new System.Drawing.Size(76, 32);
            this.label_projectSetup.TabIndex = 0;
            this.label_projectSetup.Text = "Setup";
            // 
            // button_projectSelectFile
            // 
            this.button_projectSelectFile.Location = new System.Drawing.Point(11, 54);
            this.button_projectSelectFile.Name = "button_projectSelectFile";
            this.button_projectSelectFile.Size = new System.Drawing.Size(75, 23);
            this.button_projectSelectFile.TabIndex = 4;
            this.button_projectSelectFile.Text = "Select file";
            this.button_projectSelectFile.UseVisualStyleBackColor = true;
            this.button_projectSelectFile.Click += new System.EventHandler(this.button_select_project_file_Click);
            // 
            // UserControl_ProjectImport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel_projectFieldMapping);
            this.Controls.Add(this.panel_projectButtons);
            this.Controls.Add(this.panel_projectMessage);
            this.Controls.Add(this.panel_projectDataTable);
            this.Name = "UserControl_ProjectImport";
            this.Size = new System.Drawing.Size(1006, 942);
            this.Load += new System.EventHandler(this.UserControl1_Load);
            this.panel_projectDataTable.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_project)).EndInit();
            this.panel_projectMessage.ResumeLayout(false);
            this.panel_projectMessage.PerformLayout();
            this.panel_projectButtons.ResumeLayout(false);
            this.panel_projectFieldMapping.ResumeLayout(false);
            this.panel_projectFieldMapping.PerformLayout();
            this.groupBox_projectMandatoryFields.ResumeLayout(false);
            this.groupBox_projectMandatoryFields.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion


        private void AddRowNumberToDataTable()
        {
            this.dataGridView_project.DataBindingComplete += (o, e) =>
            {
                foreach (System.Windows.Forms.DataGridViewRow row in dataGridView_project.Rows)
                {
                    row.HeaderCell.Value = (row.Index + 1).ToString();

                }
            };
            this.dataGridView_project.RowHeadersWidth = 65;
        }


        private System.ComponentModel.BackgroundWorker WorkerFetcher;
        private System.Windows.Forms.Panel panel_projectDataTable;
        private System.Windows.Forms.DataGridView dataGridView_project;
        private System.Windows.Forms.Panel panel_projectMessage;
        private System.Windows.Forms.Panel panel_projectButtons;
        private System.Windows.Forms.Button button_clear;
        private System.Windows.Forms.Button button_import;
        private System.Windows.Forms.Button button_validate;
        private System.Windows.Forms.Button button_stop;
        private System.Windows.Forms.Panel panel_projectFieldMapping;
        private System.Windows.Forms.GroupBox groupBox_projectMandatoryFields;
        private System.Windows.Forms.ComboBox comboBox_projectName;
        private System.Windows.Forms.Label label_projectName;
        private System.Windows.Forms.Label label_projectSetup;
        private System.Windows.Forms.Button button_projectSelectFile;
        private System.Windows.Forms.TextBox textBox_projectImportMessages;
        private System.Windows.Forms.Label label_projectTemplate;
        private System.Windows.Forms.ComboBox comboBox_projectTemplate;
        private System.Windows.Forms.Label label_projectCustomer;
        private System.Windows.Forms.ComboBox comboBox_projectCustomer;
        private System.Windows.Forms.Label label_projectLegalEntity;
        private System.Windows.Forms.ComboBox comboBox_projectLegalEntity;
        private System.Windows.Forms.Label label_projectCurrency;
        private System.Windows.Forms.ComboBox comboBox_projectCurrency;
        private System.Windows.Forms.Label label_projectManager;
        private System.Windows.Forms.ComboBox comboBox_projectManager;
    }
}
