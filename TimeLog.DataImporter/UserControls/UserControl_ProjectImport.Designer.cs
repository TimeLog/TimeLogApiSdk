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
            this.components = new System.ComponentModel.Container();
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
            this.flowLayoutPanel_nonMandatoryFields = new System.Windows.Forms.FlowLayoutPanel();
            this.panel_NonMandatoryButton = new System.Windows.Forms.Panel();
            this.label_nonMandatoryFields = new System.Windows.Forms.Label();
            this.button_expandNonMandatory = new System.Windows.Forms.Button();
            this.panel_NonMandatoryFields = new System.Windows.Forms.Panel();
            this.checkBox_defaultProjectCategoryID = new System.Windows.Forms.CheckBox();
            this.label_projectNo = new System.Windows.Forms.Label();
            this.comboBox_projectNo = new System.Windows.Forms.ComboBox();
            this.label_projectCategoryID = new System.Windows.Forms.Label();
            this.comboBox_description = new System.Windows.Forms.ComboBox();
            this.comboBox_projectCategoryID = new System.Windows.Forms.ComboBox();
            this.label_description = new System.Windows.Forms.Label();
            this.comboBox_projectStartDate = new System.Windows.Forms.ComboBox();
            this.label_projectStartDate = new System.Windows.Forms.Label();
            this.label_projectEndDate = new System.Windows.Forms.Label();
            this.comboBox_projectEndDate = new System.Windows.Forms.ComboBox();
            this.label_delimiter = new System.Windows.Forms.Label();
            this.comboBox_delimiter = new System.Windows.Forms.ComboBox();
            this.groupBox_projectMandatoryFields = new System.Windows.Forms.GroupBox();
            this.checkBox_defaultLegalEntityID = new System.Windows.Forms.CheckBox();
            this.checkBox_defaultProjectTemplateID = new System.Windows.Forms.CheckBox();
            this.checkBox_defaultProjectTypeID = new System.Windows.Forms.CheckBox();
            this.checkBox_defaultCurrencyID = new System.Windows.Forms.CheckBox();
            this.label_projectLegalEntityID = new System.Windows.Forms.Label();
            this.comboBox_projectLegalEntityID = new System.Windows.Forms.ComboBox();
            this.label_projectCurrencyID = new System.Windows.Forms.Label();
            this.label_projectTypeID = new System.Windows.Forms.Label();
            this.comboBox_projectCurrencyID = new System.Windows.Forms.ComboBox();
            this.label_projectManagerID = new System.Windows.Forms.Label();
            this.comboBox_projectManagerID = new System.Windows.Forms.ComboBox();
            this.label_projectTemplateID = new System.Windows.Forms.Label();
            this.comboBox_projectTypeID = new System.Windows.Forms.ComboBox();
            this.comboBox_projectTemplateID = new System.Windows.Forms.ComboBox();
            this.label_projectCustomerID = new System.Windows.Forms.Label();
            this.comboBox_projectCustomerID = new System.Windows.Forms.ComboBox();
            this.label_projectName = new System.Windows.Forms.Label();
            this.comboBox_projectName = new System.Windows.Forms.ComboBox();
            this.label_projectSetup = new System.Windows.Forms.Label();
            this.button_projectSelectFile = new System.Windows.Forms.Button();
            this.tmrExpand = new System.Windows.Forms.Timer(this.components);
            this.defaultToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.panel_projectDataTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_project)).BeginInit();
            this.panel_projectMessage.SuspendLayout();
            this.panel_projectButtons.SuspendLayout();
            this.panel_projectFieldMapping.SuspendLayout();
            this.flowLayoutPanel_nonMandatoryFields.SuspendLayout();
            this.panel_NonMandatoryButton.SuspendLayout();
            this.panel_NonMandatoryFields.SuspendLayout();
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
            this.panel_projectDataTable.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel_projectDataTable.Location = new System.Drawing.Point(0, 581);
            this.panel_projectDataTable.Name = "panel_projectDataTable";
            this.panel_projectDataTable.Size = new System.Drawing.Size(1006, 361);
            this.panel_projectDataTable.TabIndex = 6;
            // 
            // dataGridView_project
            // 
            this.dataGridView_project.BackgroundColor = System.Drawing.Color.DarkGray;
            this.dataGridView_project.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView_project.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_project.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dataGridView_project.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.dataGridView_project.Location = new System.Drawing.Point(0, 10);
            this.dataGridView_project.Name = "dataGridView_project";
            this.dataGridView_project.Size = new System.Drawing.Size(1006, 351);
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
            this.textBox_projectImportMessages.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.textBox_projectImportMessages.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox_projectImportMessages.Cursor = System.Windows.Forms.Cursors.Default;
            this.textBox_projectImportMessages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox_projectImportMessages.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.textBox_projectImportMessages.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.textBox_projectImportMessages.Location = new System.Drawing.Point(0, 0);
            this.textBox_projectImportMessages.Multiline = true;
            this.textBox_projectImportMessages.Name = "textBox_projectImportMessages";
            this.textBox_projectImportMessages.ReadOnly = true;
            this.textBox_projectImportMessages.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox_projectImportMessages.Size = new System.Drawing.Size(1006, 184);
            this.textBox_projectImportMessages.TabIndex = 0;
            this.defaultToolTip.SetToolTip(this.textBox_projectImportMessages, "Validation or import status");
            this.textBox_projectImportMessages.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBox_projectImportMessages_MouseClick);
            // 
            // panel_projectButtons
            // 
            this.panel_projectButtons.Controls.Add(this.button_clear);
            this.panel_projectButtons.Controls.Add(this.button_import);
            this.panel_projectButtons.Controls.Add(this.button_validate);
            this.panel_projectButtons.Controls.Add(this.button_stop);
            this.panel_projectButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel_projectButtons.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.panel_projectButtons.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel_projectButtons.Location = new System.Drawing.Point(0, 345);
            this.panel_projectButtons.Name = "panel_projectButtons";
            this.panel_projectButtons.Size = new System.Drawing.Size(1006, 52);
            this.panel_projectButtons.TabIndex = 12;
            // 
            // button_clear
            // 
            this.button_clear.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.button_clear.BackColor = System.Drawing.Color.DimGray;
            this.button_clear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_clear.FlatAppearance.BorderSize = 0;
            this.button_clear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_clear.ForeColor = System.Drawing.Color.White;
            this.button_clear.Location = new System.Drawing.Point(14, 12);
            this.button_clear.Name = "button_clear";
            this.button_clear.Size = new System.Drawing.Size(80, 29);
            this.button_clear.TabIndex = 12;
            this.button_clear.Text = "Reset All";
            this.defaultToolTip.SetToolTip(this.button_clear, "Reset all file input above and data table below");
            this.button_clear.UseVisualStyleBackColor = false;
            this.button_clear.Click += new System.EventHandler(this.button_clear_Click);
            // 
            // button_import
            // 
            this.button_import.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.button_import.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(43)))), ((int)(((byte)(141)))));
            this.button_import.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_import.FlatAppearance.BorderSize = 0;
            this.button_import.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_import.ForeColor = System.Drawing.Color.White;
            this.button_import.Location = new System.Drawing.Point(917, 12);
            this.button_import.Name = "button_import";
            this.button_import.Size = new System.Drawing.Size(80, 29);
            this.button_import.TabIndex = 7;
            this.button_import.Text = "Import";
            this.defaultToolTip.SetToolTip(this.button_import, "Import all data");
            this.button_import.UseVisualStyleBackColor = false;
            this.button_import.Click += new System.EventHandler(this.button_import_Click);
            // 
            // button_validate
            // 
            this.button_validate.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.button_validate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(43)))), ((int)(((byte)(141)))));
            this.button_validate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_validate.FlatAppearance.BorderSize = 0;
            this.button_validate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_validate.ForeColor = System.Drawing.Color.White;
            this.button_validate.Location = new System.Drawing.Point(745, 12);
            this.button_validate.Name = "button_validate";
            this.button_validate.Size = new System.Drawing.Size(80, 29);
            this.button_validate.TabIndex = 8;
            this.button_validate.Text = "Validate";
            this.defaultToolTip.SetToolTip(this.button_validate, "Validate data input before importing data");
            this.button_validate.UseVisualStyleBackColor = false;
            this.button_validate.Click += new System.EventHandler(this.button_validate_Click);
            // 
            // button_stop
            // 
            this.button_stop.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.button_stop.BackColor = System.Drawing.Color.DimGray;
            this.button_stop.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_stop.FlatAppearance.BorderSize = 0;
            this.button_stop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_stop.ForeColor = System.Drawing.Color.White;
            this.button_stop.Location = new System.Drawing.Point(831, 12);
            this.button_stop.Name = "button_stop";
            this.button_stop.Size = new System.Drawing.Size(80, 29);
            this.button_stop.TabIndex = 11;
            this.button_stop.Text = "Stop";
            this.defaultToolTip.SetToolTip(this.button_stop, "Stop validation or import");
            this.button_stop.UseVisualStyleBackColor = false;
            this.button_stop.Click += new System.EventHandler(this.button_stop_Click);
            // 
            // panel_projectFieldMapping
            // 
            this.panel_projectFieldMapping.AutoScroll = true;
            this.panel_projectFieldMapping.Controls.Add(this.flowLayoutPanel_nonMandatoryFields);
            this.panel_projectFieldMapping.Controls.Add(this.label_delimiter);
            this.panel_projectFieldMapping.Controls.Add(this.comboBox_delimiter);
            this.panel_projectFieldMapping.Controls.Add(this.groupBox_projectMandatoryFields);
            this.panel_projectFieldMapping.Controls.Add(this.label_projectSetup);
            this.panel_projectFieldMapping.Controls.Add(this.button_projectSelectFile);
            this.panel_projectFieldMapping.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_projectFieldMapping.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.panel_projectFieldMapping.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel_projectFieldMapping.Location = new System.Drawing.Point(0, 0);
            this.panel_projectFieldMapping.Name = "panel_projectFieldMapping";
            this.panel_projectFieldMapping.Size = new System.Drawing.Size(1006, 345);
            this.panel_projectFieldMapping.TabIndex = 13;
            // 
            // flowLayoutPanel_nonMandatoryFields
            // 
            this.flowLayoutPanel_nonMandatoryFields.Controls.Add(this.panel_NonMandatoryButton);
            this.flowLayoutPanel_nonMandatoryFields.Controls.Add(this.panel_NonMandatoryFields);
            this.flowLayoutPanel_nonMandatoryFields.Location = new System.Drawing.Point(589, 60);
            this.flowLayoutPanel_nonMandatoryFields.Name = "flowLayoutPanel_nonMandatoryFields";
            this.flowLayoutPanel_nonMandatoryFields.Size = new System.Drawing.Size(372, 208);
            this.flowLayoutPanel_nonMandatoryFields.TabIndex = 7;
            // 
            // panel_NonMandatoryButton
            // 
            this.panel_NonMandatoryButton.Controls.Add(this.label_nonMandatoryFields);
            this.panel_NonMandatoryButton.Controls.Add(this.button_expandNonMandatory);
            this.panel_NonMandatoryButton.Location = new System.Drawing.Point(3, 3);
            this.panel_NonMandatoryButton.Name = "panel_NonMandatoryButton";
            this.panel_NonMandatoryButton.Size = new System.Drawing.Size(363, 32);
            this.panel_NonMandatoryButton.TabIndex = 0;
            // 
            // label_nonMandatoryFields
            // 
            this.label_nonMandatoryFields.AutoSize = true;
            this.label_nonMandatoryFields.ForeColor = System.Drawing.Color.Black;
            this.label_nonMandatoryFields.Location = new System.Drawing.Point(46, 8);
            this.label_nonMandatoryFields.Name = "label_nonMandatoryFields";
            this.label_nonMandatoryFields.Size = new System.Drawing.Size(107, 17);
            this.label_nonMandatoryFields.TabIndex = 1;
            this.label_nonMandatoryFields.Text = "Non-Mandatory";
            // 
            // button_expandNonMandatory
            // 
            this.button_expandNonMandatory.BackColor = System.Drawing.Color.White;
            this.button_expandNonMandatory.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button_expandNonMandatory.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_expandNonMandatory.FlatAppearance.BorderSize = 0;
            this.button_expandNonMandatory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_expandNonMandatory.ForeColor = System.Drawing.Color.White;
            this.button_expandNonMandatory.Location = new System.Drawing.Point(10, 1);
            this.button_expandNonMandatory.Name = "button_expandNonMandatory";
            this.button_expandNonMandatory.Size = new System.Drawing.Size(30, 30);
            this.button_expandNonMandatory.TabIndex = 0;
            this.button_expandNonMandatory.UseVisualStyleBackColor = false;
            this.button_expandNonMandatory.Click += new System.EventHandler(this.button_expand_Click);
            // 
            // panel_NonMandatoryFields
            // 
            this.panel_NonMandatoryFields.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel_NonMandatoryFields.Controls.Add(this.checkBox_defaultProjectCategoryID);
            this.panel_NonMandatoryFields.Controls.Add(this.label_projectNo);
            this.panel_NonMandatoryFields.Controls.Add(this.comboBox_projectNo);
            this.panel_NonMandatoryFields.Controls.Add(this.label_projectCategoryID);
            this.panel_NonMandatoryFields.Controls.Add(this.comboBox_description);
            this.panel_NonMandatoryFields.Controls.Add(this.comboBox_projectCategoryID);
            this.panel_NonMandatoryFields.Controls.Add(this.label_description);
            this.panel_NonMandatoryFields.Controls.Add(this.comboBox_projectStartDate);
            this.panel_NonMandatoryFields.Controls.Add(this.label_projectStartDate);
            this.panel_NonMandatoryFields.Controls.Add(this.label_projectEndDate);
            this.panel_NonMandatoryFields.Controls.Add(this.comboBox_projectEndDate);
            this.panel_NonMandatoryFields.Location = new System.Drawing.Point(3, 41);
            this.panel_NonMandatoryFields.MaximumSize = new System.Drawing.Size(363, 163);
            this.panel_NonMandatoryFields.MinimumSize = new System.Drawing.Size(363, 0);
            this.panel_NonMandatoryFields.Name = "panel_NonMandatoryFields";
            this.panel_NonMandatoryFields.Size = new System.Drawing.Size(363, 163);
            this.panel_NonMandatoryFields.TabIndex = 1;
            // 
            // checkBox_defaultProjectCategoryID
            // 
            this.checkBox_defaultProjectCategoryID.AutoSize = true;
            this.checkBox_defaultProjectCategoryID.Location = new System.Drawing.Point(288, 133);
            this.checkBox_defaultProjectCategoryID.Name = "checkBox_defaultProjectCategoryID";
            this.checkBox_defaultProjectCategoryID.Size = new System.Drawing.Size(70, 21);
            this.checkBox_defaultProjectCategoryID.TabIndex = 5;
            this.checkBox_defaultProjectCategoryID.Text = "Default";
            this.defaultToolTip.SetToolTip(this.checkBox_defaultProjectCategoryID, "Set default values for all rows of a particular column field");
            this.checkBox_defaultProjectCategoryID.UseVisualStyleBackColor = true;
            this.checkBox_defaultProjectCategoryID.CheckedChanged += new System.EventHandler(this.checkBox_defaultProjectCategoryID_CheckedChanged);
            // 
            // label_projectNo
            // 
            this.label_projectNo.AutoSize = true;
            this.label_projectNo.Location = new System.Drawing.Point(10, 10);
            this.label_projectNo.Name = "label_projectNo";
            this.label_projectNo.Size = new System.Drawing.Size(72, 17);
            this.label_projectNo.TabIndex = 1;
            this.label_projectNo.Text = "Project No";
            // 
            // comboBox_projectNo
            // 
            this.comboBox_projectNo.FormattingEnabled = true;
            this.comboBox_projectNo.Location = new System.Drawing.Point(143, 7);
            this.comboBox_projectNo.Name = "comboBox_projectNo";
            this.comboBox_projectNo.Size = new System.Drawing.Size(139, 25);
            this.comboBox_projectNo.TabIndex = 3;
            this.comboBox_projectNo.SelectedIndexChanged += new System.EventHandler(this.comboBox_projectNo_SelectedIndexChanged);
            // 
            // label_projectCategoryID
            // 
            this.label_projectCategoryID.AutoSize = true;
            this.label_projectCategoryID.Location = new System.Drawing.Point(10, 134);
            this.label_projectCategoryID.Name = "label_projectCategoryID";
            this.label_projectCategoryID.Size = new System.Drawing.Size(127, 17);
            this.label_projectCategoryID.TabIndex = 1;
            this.label_projectCategoryID.Text = "Project Category ID";
            // 
            // comboBox_description
            // 
            this.comboBox_description.FormattingEnabled = true;
            this.comboBox_description.Location = new System.Drawing.Point(143, 38);
            this.comboBox_description.Name = "comboBox_description";
            this.comboBox_description.Size = new System.Drawing.Size(139, 25);
            this.comboBox_description.TabIndex = 3;
            this.comboBox_description.SelectedIndexChanged += new System.EventHandler(this.comboBox_description_SelectedIndexChanged);
            // 
            // comboBox_projectCategoryID
            // 
            this.comboBox_projectCategoryID.FormattingEnabled = true;
            this.comboBox_projectCategoryID.Location = new System.Drawing.Point(143, 131);
            this.comboBox_projectCategoryID.Name = "comboBox_projectCategoryID";
            this.comboBox_projectCategoryID.Size = new System.Drawing.Size(139, 25);
            this.comboBox_projectCategoryID.TabIndex = 3;
            this.comboBox_projectCategoryID.SelectedIndexChanged += new System.EventHandler(this.comboBox_projectCategoryID_SelectedIndexChanged);
            // 
            // label_description
            // 
            this.label_description.AutoSize = true;
            this.label_description.Location = new System.Drawing.Point(10, 41);
            this.label_description.Name = "label_description";
            this.label_description.Size = new System.Drawing.Size(76, 17);
            this.label_description.TabIndex = 1;
            this.label_description.Text = "Description";
            // 
            // comboBox_projectStartDate
            // 
            this.comboBox_projectStartDate.FormattingEnabled = true;
            this.comboBox_projectStartDate.Location = new System.Drawing.Point(143, 69);
            this.comboBox_projectStartDate.Name = "comboBox_projectStartDate";
            this.comboBox_projectStartDate.Size = new System.Drawing.Size(139, 25);
            this.comboBox_projectStartDate.TabIndex = 3;
            this.comboBox_projectStartDate.SelectedIndexChanged += new System.EventHandler(this.comboBox_projectStartDate_SelectedIndexChanged);
            // 
            // label_projectStartDate
            // 
            this.label_projectStartDate.AutoSize = true;
            this.label_projectStartDate.Location = new System.Drawing.Point(10, 72);
            this.label_projectStartDate.Name = "label_projectStartDate";
            this.label_projectStartDate.Size = new System.Drawing.Size(115, 17);
            this.label_projectStartDate.TabIndex = 1;
            this.label_projectStartDate.Text = "Project Start Date";
            // 
            // label_projectEndDate
            // 
            this.label_projectEndDate.AutoSize = true;
            this.label_projectEndDate.Location = new System.Drawing.Point(10, 103);
            this.label_projectEndDate.Name = "label_projectEndDate";
            this.label_projectEndDate.Size = new System.Drawing.Size(109, 17);
            this.label_projectEndDate.TabIndex = 1;
            this.label_projectEndDate.Text = "Project End Date";
            // 
            // comboBox_projectEndDate
            // 
            this.comboBox_projectEndDate.FormattingEnabled = true;
            this.comboBox_projectEndDate.Location = new System.Drawing.Point(143, 100);
            this.comboBox_projectEndDate.Name = "comboBox_projectEndDate";
            this.comboBox_projectEndDate.Size = new System.Drawing.Size(139, 25);
            this.comboBox_projectEndDate.TabIndex = 3;
            this.comboBox_projectEndDate.SelectedIndexChanged += new System.EventHandler(this.comboBox_projectEndDate_SelectedIndexChanged);
            // 
            // label_delimiter
            // 
            this.label_delimiter.AutoSize = true;
            this.label_delimiter.Location = new System.Drawing.Point(14, 75);
            this.label_delimiter.Name = "label_delimiter";
            this.label_delimiter.Size = new System.Drawing.Size(62, 17);
            this.label_delimiter.TabIndex = 1;
            this.label_delimiter.Text = "Delimiter";
            // 
            // comboBox_delimiter
            // 
            this.comboBox_delimiter.FormattingEnabled = true;
            this.comboBox_delimiter.Location = new System.Drawing.Point(82, 72);
            this.comboBox_delimiter.Name = "comboBox_delimiter";
            this.comboBox_delimiter.Size = new System.Drawing.Size(56, 25);
            this.comboBox_delimiter.TabIndex = 6;
            // 
            // groupBox_projectMandatoryFields
            // 
            this.groupBox_projectMandatoryFields.Controls.Add(this.checkBox_defaultLegalEntityID);
            this.groupBox_projectMandatoryFields.Controls.Add(this.checkBox_defaultProjectTemplateID);
            this.groupBox_projectMandatoryFields.Controls.Add(this.checkBox_defaultProjectTypeID);
            this.groupBox_projectMandatoryFields.Controls.Add(this.checkBox_defaultCurrencyID);
            this.groupBox_projectMandatoryFields.Controls.Add(this.label_projectLegalEntityID);
            this.groupBox_projectMandatoryFields.Controls.Add(this.comboBox_projectLegalEntityID);
            this.groupBox_projectMandatoryFields.Controls.Add(this.label_projectCurrencyID);
            this.groupBox_projectMandatoryFields.Controls.Add(this.label_projectTypeID);
            this.groupBox_projectMandatoryFields.Controls.Add(this.comboBox_projectCurrencyID);
            this.groupBox_projectMandatoryFields.Controls.Add(this.label_projectManagerID);
            this.groupBox_projectMandatoryFields.Controls.Add(this.comboBox_projectManagerID);
            this.groupBox_projectMandatoryFields.Controls.Add(this.label_projectTemplateID);
            this.groupBox_projectMandatoryFields.Controls.Add(this.comboBox_projectTypeID);
            this.groupBox_projectMandatoryFields.Controls.Add(this.comboBox_projectTemplateID);
            this.groupBox_projectMandatoryFields.Controls.Add(this.label_projectCustomerID);
            this.groupBox_projectMandatoryFields.Controls.Add(this.comboBox_projectCustomerID);
            this.groupBox_projectMandatoryFields.Controls.Add(this.label_projectName);
            this.groupBox_projectMandatoryFields.Controls.Add(this.comboBox_projectName);
            this.groupBox_projectMandatoryFields.Location = new System.Drawing.Point(184, 62);
            this.groupBox_projectMandatoryFields.Name = "groupBox_projectMandatoryFields";
            this.groupBox_projectMandatoryFields.Size = new System.Drawing.Size(358, 245);
            this.groupBox_projectMandatoryFields.TabIndex = 5;
            this.groupBox_projectMandatoryFields.TabStop = false;
            this.groupBox_projectMandatoryFields.Text = "Mandatory";
            // 
            // checkBox_defaultLegalEntityID
            // 
            this.checkBox_defaultLegalEntityID.AutoSize = true;
            this.checkBox_defaultLegalEntityID.Location = new System.Drawing.Point(283, 181);
            this.checkBox_defaultLegalEntityID.Name = "checkBox_defaultLegalEntityID";
            this.checkBox_defaultLegalEntityID.Size = new System.Drawing.Size(70, 21);
            this.checkBox_defaultLegalEntityID.TabIndex = 6;
            this.checkBox_defaultLegalEntityID.Text = "Default";
            this.defaultToolTip.SetToolTip(this.checkBox_defaultLegalEntityID, "Set default values for all rows of a particular column field");
            this.checkBox_defaultLegalEntityID.UseVisualStyleBackColor = true;
            this.checkBox_defaultLegalEntityID.CheckedChanged += new System.EventHandler(this.checkBox_defaultLegalEntityID_CheckedChanged);
            // 
            // checkBox_defaultProjectTemplateID
            // 
            this.checkBox_defaultProjectTemplateID.AutoSize = true;
            this.checkBox_defaultProjectTemplateID.Location = new System.Drawing.Point(283, 88);
            this.checkBox_defaultProjectTemplateID.Name = "checkBox_defaultProjectTemplateID";
            this.checkBox_defaultProjectTemplateID.Size = new System.Drawing.Size(70, 21);
            this.checkBox_defaultProjectTemplateID.TabIndex = 5;
            this.checkBox_defaultProjectTemplateID.Text = "Default";
            this.defaultToolTip.SetToolTip(this.checkBox_defaultProjectTemplateID, "Set default values for all rows of a particular column field");
            this.checkBox_defaultProjectTemplateID.UseVisualStyleBackColor = true;
            this.checkBox_defaultProjectTemplateID.CheckedChanged += new System.EventHandler(this.checkBox_defaultProjectTemplateID_CheckedChanged);
            // 
            // checkBox_defaultProjectTypeID
            // 
            this.checkBox_defaultProjectTypeID.AutoSize = true;
            this.checkBox_defaultProjectTypeID.Location = new System.Drawing.Point(283, 212);
            this.checkBox_defaultProjectTypeID.Name = "checkBox_defaultProjectTypeID";
            this.checkBox_defaultProjectTypeID.Size = new System.Drawing.Size(70, 21);
            this.checkBox_defaultProjectTypeID.TabIndex = 4;
            this.checkBox_defaultProjectTypeID.Text = "Default";
            this.defaultToolTip.SetToolTip(this.checkBox_defaultProjectTypeID, "Set default values for all rows of a particular column field");
            this.checkBox_defaultProjectTypeID.UseVisualStyleBackColor = true;
            this.checkBox_defaultProjectTypeID.CheckedChanged += new System.EventHandler(this.checkBox_defaultProjectTypeID_CheckedChanged);
            // 
            // checkBox_defaultCurrencyID
            // 
            this.checkBox_defaultCurrencyID.AutoSize = true;
            this.checkBox_defaultCurrencyID.Location = new System.Drawing.Point(283, 150);
            this.checkBox_defaultCurrencyID.Name = "checkBox_defaultCurrencyID";
            this.checkBox_defaultCurrencyID.Size = new System.Drawing.Size(70, 21);
            this.checkBox_defaultCurrencyID.TabIndex = 4;
            this.checkBox_defaultCurrencyID.Text = "Default";
            this.defaultToolTip.SetToolTip(this.checkBox_defaultCurrencyID, "Set default values for all rows of a particular column field");
            this.checkBox_defaultCurrencyID.UseVisualStyleBackColor = true;
            this.checkBox_defaultCurrencyID.CheckedChanged += new System.EventHandler(this.checkBox_defaultCurrencyID_CheckedChanged);
            // 
            // label_projectLegalEntityID
            // 
            this.label_projectLegalEntityID.AutoSize = true;
            this.label_projectLegalEntityID.Location = new System.Drawing.Point(6, 182);
            this.label_projectLegalEntityID.Name = "label_projectLegalEntityID";
            this.label_projectLegalEntityID.Size = new System.Drawing.Size(95, 17);
            this.label_projectLegalEntityID.TabIndex = 1;
            this.label_projectLegalEntityID.Text = "Legal Entity ID";
            // 
            // comboBox_projectLegalEntityID
            // 
            this.comboBox_projectLegalEntityID.FormattingEnabled = true;
            this.comboBox_projectLegalEntityID.Location = new System.Drawing.Point(138, 179);
            this.comboBox_projectLegalEntityID.Name = "comboBox_projectLegalEntityID";
            this.comboBox_projectLegalEntityID.Size = new System.Drawing.Size(139, 25);
            this.comboBox_projectLegalEntityID.TabIndex = 3;
            this.comboBox_projectLegalEntityID.SelectedIndexChanged += new System.EventHandler(this.comboBox_projectLegalEntity_SelectedIndexChanged);
            // 
            // label_projectCurrencyID
            // 
            this.label_projectCurrencyID.AutoSize = true;
            this.label_projectCurrencyID.Location = new System.Drawing.Point(6, 151);
            this.label_projectCurrencyID.Name = "label_projectCurrencyID";
            this.label_projectCurrencyID.Size = new System.Drawing.Size(79, 17);
            this.label_projectCurrencyID.TabIndex = 1;
            this.label_projectCurrencyID.Text = "Currency ID";
            // 
            // label_projectTypeID
            // 
            this.label_projectTypeID.AutoSize = true;
            this.label_projectTypeID.Location = new System.Drawing.Point(6, 213);
            this.label_projectTypeID.Name = "label_projectTypeID";
            this.label_projectTypeID.Size = new System.Drawing.Size(99, 17);
            this.label_projectTypeID.TabIndex = 1;
            this.label_projectTypeID.Text = "Project Type ID";
            // 
            // comboBox_projectCurrencyID
            // 
            this.comboBox_projectCurrencyID.FormattingEnabled = true;
            this.comboBox_projectCurrencyID.Location = new System.Drawing.Point(138, 148);
            this.comboBox_projectCurrencyID.Name = "comboBox_projectCurrencyID";
            this.comboBox_projectCurrencyID.Size = new System.Drawing.Size(139, 25);
            this.comboBox_projectCurrencyID.TabIndex = 3;
            this.comboBox_projectCurrencyID.SelectedIndexChanged += new System.EventHandler(this.comboBox_projectCurrency_SelectedIndexChanged);
            // 
            // label_projectManagerID
            // 
            this.label_projectManagerID.AutoSize = true;
            this.label_projectManagerID.Location = new System.Drawing.Point(6, 120);
            this.label_projectManagerID.Name = "label_projectManagerID";
            this.label_projectManagerID.Size = new System.Drawing.Size(125, 17);
            this.label_projectManagerID.TabIndex = 1;
            this.label_projectManagerID.Text = "Project Manager ID";
            // 
            // comboBox_projectManagerID
            // 
            this.comboBox_projectManagerID.FormattingEnabled = true;
            this.comboBox_projectManagerID.Location = new System.Drawing.Point(138, 117);
            this.comboBox_projectManagerID.Name = "comboBox_projectManagerID";
            this.comboBox_projectManagerID.Size = new System.Drawing.Size(139, 25);
            this.comboBox_projectManagerID.TabIndex = 3;
            this.comboBox_projectManagerID.SelectedIndexChanged += new System.EventHandler(this.comboBox_projectManager_SelectedIndexChanged);
            // 
            // label_projectTemplateID
            // 
            this.label_projectTemplateID.AutoSize = true;
            this.label_projectTemplateID.Location = new System.Drawing.Point(6, 89);
            this.label_projectTemplateID.Name = "label_projectTemplateID";
            this.label_projectTemplateID.Size = new System.Drawing.Size(126, 17);
            this.label_projectTemplateID.TabIndex = 1;
            this.label_projectTemplateID.Text = "Project Template ID";
            // 
            // comboBox_projectTypeID
            // 
            this.comboBox_projectTypeID.FormattingEnabled = true;
            this.comboBox_projectTypeID.Location = new System.Drawing.Point(138, 210);
            this.comboBox_projectTypeID.Name = "comboBox_projectTypeID";
            this.comboBox_projectTypeID.Size = new System.Drawing.Size(139, 25);
            this.comboBox_projectTypeID.TabIndex = 3;
            this.comboBox_projectTypeID.SelectedIndexChanged += new System.EventHandler(this.comboBox_projectTypeID_SelectedIndexChanged);
            // 
            // comboBox_projectTemplateID
            // 
            this.comboBox_projectTemplateID.FormattingEnabled = true;
            this.comboBox_projectTemplateID.Location = new System.Drawing.Point(138, 86);
            this.comboBox_projectTemplateID.Name = "comboBox_projectTemplateID";
            this.comboBox_projectTemplateID.Size = new System.Drawing.Size(139, 25);
            this.comboBox_projectTemplateID.TabIndex = 3;
            this.comboBox_projectTemplateID.SelectedIndexChanged += new System.EventHandler(this.comboBox_projectTemplate_SelectedIndexChanged);
            // 
            // label_projectCustomerID
            // 
            this.label_projectCustomerID.AutoSize = true;
            this.label_projectCustomerID.Location = new System.Drawing.Point(6, 58);
            this.label_projectCustomerID.Name = "label_projectCustomerID";
            this.label_projectCustomerID.Size = new System.Drawing.Size(84, 17);
            this.label_projectCustomerID.TabIndex = 1;
            this.label_projectCustomerID.Text = "Customer ID";
            // 
            // comboBox_projectCustomerID
            // 
            this.comboBox_projectCustomerID.FormattingEnabled = true;
            this.comboBox_projectCustomerID.Location = new System.Drawing.Point(138, 55);
            this.comboBox_projectCustomerID.Name = "comboBox_projectCustomerID";
            this.comboBox_projectCustomerID.Size = new System.Drawing.Size(139, 25);
            this.comboBox_projectCustomerID.TabIndex = 3;
            this.comboBox_projectCustomerID.SelectedIndexChanged += new System.EventHandler(this.comboBox_projectCustomer_SelectedIndexChanged);
            // 
            // label_projectName
            // 
            this.label_projectName.AutoSize = true;
            this.label_projectName.Location = new System.Drawing.Point(6, 27);
            this.label_projectName.Name = "label_projectName";
            this.label_projectName.Size = new System.Drawing.Size(90, 17);
            this.label_projectName.TabIndex = 1;
            this.label_projectName.Text = "Project Name";
            // 
            // comboBox_projectName
            // 
            this.comboBox_projectName.FormattingEnabled = true;
            this.comboBox_projectName.Location = new System.Drawing.Point(138, 24);
            this.comboBox_projectName.Name = "comboBox_projectName";
            this.comboBox_projectName.Size = new System.Drawing.Size(139, 25);
            this.comboBox_projectName.TabIndex = 3;
            this.comboBox_projectName.SelectedIndexChanged += new System.EventHandler(this.comboBox_projectName_SelectedIndexChanged);
            // 
            // label_projectSetup
            // 
            this.label_projectSetup.AutoSize = true;
            this.label_projectSetup.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label_projectSetup.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label_projectSetup.Location = new System.Drawing.Point(13, 16);
            this.label_projectSetup.Name = "label_projectSetup";
            this.label_projectSetup.Size = new System.Drawing.Size(231, 32);
            this.label_projectSetup.TabIndex = 0;
            this.label_projectSetup.Text = "Project Data Import";
            // 
            // button_projectSelectFile
            // 
            this.button_projectSelectFile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(43)))), ((int)(((byte)(141)))));
            this.button_projectSelectFile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_projectSelectFile.FlatAppearance.BorderSize = 0;
            this.button_projectSelectFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_projectSelectFile.ForeColor = System.Drawing.Color.White;
            this.button_projectSelectFile.Location = new System.Drawing.Point(13, 101);
            this.button_projectSelectFile.Name = "button_projectSelectFile";
            this.button_projectSelectFile.Size = new System.Drawing.Size(80, 29);
            this.button_projectSelectFile.TabIndex = 4;
            this.button_projectSelectFile.Text = "Select File";
            this.defaultToolTip.SetToolTip(this.button_projectSelectFile, "Select input CSV file");
            this.button_projectSelectFile.UseVisualStyleBackColor = false;
            this.button_projectSelectFile.Click += new System.EventHandler(this.button_select_project_file_Click);
            // 
            // tmrExpand
            // 
            this.tmrExpand.Interval = 10;
            this.tmrExpand.Tick += new System.EventHandler(this.tmrExpand_Tick);
            // 
            // defaultToolTip
            // 
            this.defaultToolTip.ShowAlways = true;
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
            this.flowLayoutPanel_nonMandatoryFields.ResumeLayout(false);
            this.panel_NonMandatoryButton.ResumeLayout(false);
            this.panel_NonMandatoryButton.PerformLayout();
            this.panel_NonMandatoryFields.ResumeLayout(false);
            this.panel_NonMandatoryFields.PerformLayout();
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
        private System.Windows.Forms.Label label_projectTemplateID;
        private System.Windows.Forms.ComboBox comboBox_projectTemplateID;
        private System.Windows.Forms.Label label_projectCustomerID;
        private System.Windows.Forms.ComboBox comboBox_projectCustomerID;
        private System.Windows.Forms.Label label_projectLegalEntityID;
        private System.Windows.Forms.ComboBox comboBox_projectLegalEntityID;
        private System.Windows.Forms.Label label_projectCurrencyID;
        private System.Windows.Forms.ComboBox comboBox_projectCurrencyID;
        private System.Windows.Forms.Label label_projectManagerID;
        private System.Windows.Forms.ComboBox comboBox_projectManagerID;
        private System.Windows.Forms.Label label_delimiter;
        private System.Windows.Forms.ComboBox comboBox_delimiter;
        private System.Windows.Forms.Label label_projectCategoryID;
        private System.Windows.Forms.ComboBox comboBox_projectCategoryID;
        private System.Windows.Forms.Label label_projectTypeID;
        private System.Windows.Forms.ComboBox comboBox_projectTypeID;
        private System.Windows.Forms.Label label_projectEndDate;
        private System.Windows.Forms.ComboBox comboBox_projectEndDate;
        private System.Windows.Forms.Label label_projectStartDate;
        private System.Windows.Forms.ComboBox comboBox_projectStartDate;
        private System.Windows.Forms.Label label_description;
        private System.Windows.Forms.ComboBox comboBox_description;
        private System.Windows.Forms.Label label_projectNo;
        private System.Windows.Forms.ComboBox comboBox_projectNo;
        private System.Windows.Forms.CheckBox checkBox_defaultCurrencyID;
        private System.Windows.Forms.CheckBox checkBox_defaultProjectCategoryID;
        private System.Windows.Forms.CheckBox checkBox_defaultProjectTypeID;
        private System.Windows.Forms.CheckBox checkBox_defaultLegalEntityID;
        private System.Windows.Forms.CheckBox checkBox_defaultProjectTemplateID;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel_nonMandatoryFields;
        private System.Windows.Forms.Panel panel_NonMandatoryButton;
        private System.Windows.Forms.Panel panel_NonMandatoryFields;
        private System.Windows.Forms.Label label_nonMandatoryFields;
        private System.Windows.Forms.Button button_expandNonMandatory;
        private System.Windows.Forms.Timer tmrExpand;
        private System.Windows.Forms.ToolTip defaultToolTip;
    }
}
