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
            this.label_delimiter = new System.Windows.Forms.Label();
            this.comboBox_delimiter = new System.Windows.Forms.ComboBox();
            this.groupBox_customerNonMandatoryFields = new System.Windows.Forms.GroupBox();
            this.checkBox_defaultVATPercentage = new System.Windows.Forms.CheckBox();
            this.checkBox_defaultMileageIsBillable = new System.Windows.Forms.CheckBox();
            this.checkBox_defaultExpenseIsBillable = new System.Windows.Forms.CheckBox();
            this.checkBox_defaultPaymentTermID = new System.Windows.Forms.CheckBox();
            this.checkBox_defaultIndustryID = new System.Windows.Forms.CheckBox();
            this.checkBox_defaultSecondaryKAMID = new System.Windows.Forms.CheckBox();
            this.comboBox_VATPercentage = new System.Windows.Forms.ComboBox();
            this.comboBox_calculateVAT = new System.Windows.Forms.ComboBox();
            this.comboBox_discountPercentage = new System.Windows.Forms.ComboBox();
            this.comboBox_paymentTermID = new System.Windows.Forms.ComboBox();
            this.comboBox_customerReferenceID = new System.Windows.Forms.ComboBox();
            this.comboBox_internalReferenceID = new System.Windows.Forms.ComboBox();
            this.comboBox_invoiceAddressToUse = new System.Windows.Forms.ComboBox();
            this.comboBox_contactID = new System.Windows.Forms.ComboBox();
            this.comboBox_defaultDistIsMaxBillable = new System.Windows.Forms.ComboBox();
            this.comboBox_mileageIsBillable = new System.Windows.Forms.ComboBox();
            this.comboBox_expenseIsBillable = new System.Windows.Forms.ComboBox();
            this.comboBox_defaultMileageDistance = new System.Windows.Forms.ComboBox();
            this.comboBox_invoicingAddressCountryID = new System.Windows.Forms.ComboBox();
            this.comboBox_invoicingAddressState = new System.Windows.Forms.ComboBox();
            this.comboBox_invoicingAddressCity = new System.Windows.Forms.ComboBox();
            this.comboBox_invoicingAddressZipCode = new System.Windows.Forms.ComboBox();
            this.comboBox_invoicingAddress3 = new System.Windows.Forms.ComboBox();
            this.comboBox_invoicingAddress2 = new System.Windows.Forms.ComboBox();
            this.comboBox_invoicingAddress = new System.Windows.Forms.ComboBox();
            this.comboBox_VATNo = new System.Windows.Forms.ComboBox();
            this.comboBox_useInvoicingAddress = new System.Windows.Forms.ComboBox();
            this.comboBox_organizationNo = new System.Windows.Forms.ComboBox();
            this.comboBox_eanNo = new System.Windows.Forms.ComboBox();
            this.comboBox_useEanNo = new System.Windows.Forms.ComboBox();
            this.label_vatNo = new System.Windows.Forms.Label();
            this.comboBox_state = new System.Windows.Forms.ComboBox();
            this.comboBox_city = new System.Windows.Forms.ComboBox();
            this.comboBox_zipCode = new System.Windows.Forms.ComboBox();
            this.comboBox_address3 = new System.Windows.Forms.ComboBox();
            this.comboBox_address2 = new System.Windows.Forms.ComboBox();
            this.comboBox_address = new System.Windows.Forms.ComboBox();
            this.comboBox_website = new System.Windows.Forms.ComboBox();
            this.comboBox_email = new System.Windows.Forms.ComboBox();
            this.comboBox_faxNo = new System.Windows.Forms.ComboBox();
            this.comboBox_phoneNo = new System.Windows.Forms.ComboBox();
            this.comboBox_industryID = new System.Windows.Forms.ComboBox();
            this.comboBox_customerSince = new System.Windows.Forms.ComboBox();
            this.comboBox_primaryKAMID = new System.Windows.Forms.ComboBox();
            this.comboBox_nickName = new System.Windows.Forms.ComboBox();
            this.checkBox_defaultPrimaryKAMID = new System.Windows.Forms.CheckBox();
            this.label_invoiceAddressToUse = new System.Windows.Forms.Label();
            this.comboBox_customerNo = new System.Windows.Forms.ComboBox();
            this.comboBox_secondaryKAMID = new System.Windows.Forms.ComboBox();
            this.label_defaultMileageDistance = new System.Windows.Forms.Label();
            this.label_vatPercentage = new System.Windows.Forms.Label();
            this.label_useEanNo = new System.Windows.Forms.Label();
            this.label_calculateVAT = new System.Windows.Forms.Label();
            this.label_discountPercentage = new System.Windows.Forms.Label();
            this.label_invoicingAddressCountryID = new System.Windows.Forms.Label();
            this.label_paymentTermID = new System.Windows.Forms.Label();
            this.label_customerReferenceID = new System.Windows.Forms.Label();
            this.label_eanNo = new System.Windows.Forms.Label();
            this.label_internalReferenceID = new System.Windows.Forms.Label();
            this.label_contactID = new System.Windows.Forms.Label();
            this.label_defaultDistIsMaxBillable = new System.Windows.Forms.Label();
            this.label_organizationNo = new System.Windows.Forms.Label();
            this.label_email = new System.Windows.Forms.Label();
            this.label_industryID = new System.Windows.Forms.Label();
            this.label_secondaryKAMID = new System.Windows.Forms.Label();
            this.label_invoicingAddressState = new System.Windows.Forms.Label();
            this.label_expenseIsBillable = new System.Windows.Forms.Label();
            this.label_primaryKAMID = new System.Windows.Forms.Label();
            this.label_mileageIsBillable = new System.Windows.Forms.Label();
            this.label_invoicingAddress3 = new System.Windows.Forms.Label();
            this.label_invoicingAddress2 = new System.Windows.Forms.Label();
            this.label_invoicingAddressZipCode = new System.Windows.Forms.Label();
            this.label_invoicingAddressCity = new System.Windows.Forms.Label();
            this.label_invoicingAddress = new System.Windows.Forms.Label();
            this.label_useInvoicingAddress = new System.Windows.Forms.Label();
            this.label_state = new System.Windows.Forms.Label();
            this.label_city = new System.Windows.Forms.Label();
            this.label_zipCode = new System.Windows.Forms.Label();
            this.label_address3 = new System.Windows.Forms.Label();
            this.label_address2 = new System.Windows.Forms.Label();
            this.label_address = new System.Windows.Forms.Label();
            this.label_website = new System.Windows.Forms.Label();
            this.label_fax = new System.Windows.Forms.Label();
            this.label_phone = new System.Windows.Forms.Label();
            this.label_customerSince = new System.Windows.Forms.Label();
            this.label_nickname = new System.Windows.Forms.Label();
            this.label_customerNo = new System.Windows.Forms.Label();
            this.groupBox_customerMandatoryFields = new System.Windows.Forms.GroupBox();
            this.checkBox_defaultCountryID = new System.Windows.Forms.CheckBox();
            this.checkBox_defaultCustomerStatusID = new System.Windows.Forms.CheckBox();
            this.checkBox_defaultCurrencyID = new System.Windows.Forms.CheckBox();
            this.label_countryID = new System.Windows.Forms.Label();
            this.label_customerStatusID = new System.Windows.Forms.Label();
            this.comboBox_countryID = new System.Windows.Forms.ComboBox();
            this.comboBox_customerStatusID = new System.Windows.Forms.ComboBox();
            this.label_currencyID = new System.Windows.Forms.Label();
            this.comboBox_currencyID = new System.Windows.Forms.ComboBox();
            this.label_customerName = new System.Windows.Forms.Label();
            this.comboBox_customerName = new System.Windows.Forms.ComboBox();
            this.label_customerSetup = new System.Windows.Forms.Label();
            this.button_customerSelectFile = new System.Windows.Forms.Button();
            this.panel_customerDataTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_customer)).BeginInit();
            this.panel_customerMessage.SuspendLayout();
            this.panel_customerButtons.SuspendLayout();
            this.panel_customerFieldMapping.SuspendLayout();
            this.groupBox_customerNonMandatoryFields.SuspendLayout();
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
            this.panel_customerFieldMapping.AutoScroll = true;
            this.panel_customerFieldMapping.Controls.Add(this.label_delimiter);
            this.panel_customerFieldMapping.Controls.Add(this.comboBox_delimiter);
            this.panel_customerFieldMapping.Controls.Add(this.groupBox_customerNonMandatoryFields);
            this.panel_customerFieldMapping.Controls.Add(this.groupBox_customerMandatoryFields);
            this.panel_customerFieldMapping.Controls.Add(this.label_customerSetup);
            this.panel_customerFieldMapping.Controls.Add(this.button_customerSelectFile);
            this.panel_customerFieldMapping.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_customerFieldMapping.Location = new System.Drawing.Point(0, 0);
            this.panel_customerFieldMapping.Name = "panel_customerFieldMapping";
            this.panel_customerFieldMapping.Size = new System.Drawing.Size(1006, 345);
            this.panel_customerFieldMapping.TabIndex = 13;
            // 
            // label_delimiter
            // 
            this.label_delimiter.AutoSize = true;
            this.label_delimiter.Location = new System.Drawing.Point(14, 75);
            this.label_delimiter.Name = "label_delimiter";
            this.label_delimiter.Size = new System.Drawing.Size(55, 15);
            this.label_delimiter.TabIndex = 1;
            this.label_delimiter.Text = "Delimiter";
            // 
            // comboBox_delimiter
            // 
            this.comboBox_delimiter.FormattingEnabled = true;
            this.comboBox_delimiter.Location = new System.Drawing.Point(75, 72);
            this.comboBox_delimiter.Name = "comboBox_delimiter";
            this.comboBox_delimiter.Size = new System.Drawing.Size(56, 23);
            this.comboBox_delimiter.TabIndex = 7;
            // 
            // groupBox_customerNonMandatoryFields
            // 
            this.groupBox_customerNonMandatoryFields.Controls.Add(this.checkBox_defaultVATPercentage);
            this.groupBox_customerNonMandatoryFields.Controls.Add(this.checkBox_defaultMileageIsBillable);
            this.groupBox_customerNonMandatoryFields.Controls.Add(this.checkBox_defaultExpenseIsBillable);
            this.groupBox_customerNonMandatoryFields.Controls.Add(this.checkBox_defaultPaymentTermID);
            this.groupBox_customerNonMandatoryFields.Controls.Add(this.checkBox_defaultIndustryID);
            this.groupBox_customerNonMandatoryFields.Controls.Add(this.checkBox_defaultSecondaryKAMID);
            this.groupBox_customerNonMandatoryFields.Controls.Add(this.comboBox_VATPercentage);
            this.groupBox_customerNonMandatoryFields.Controls.Add(this.comboBox_calculateVAT);
            this.groupBox_customerNonMandatoryFields.Controls.Add(this.comboBox_discountPercentage);
            this.groupBox_customerNonMandatoryFields.Controls.Add(this.comboBox_paymentTermID);
            this.groupBox_customerNonMandatoryFields.Controls.Add(this.comboBox_customerReferenceID);
            this.groupBox_customerNonMandatoryFields.Controls.Add(this.comboBox_internalReferenceID);
            this.groupBox_customerNonMandatoryFields.Controls.Add(this.comboBox_invoiceAddressToUse);
            this.groupBox_customerNonMandatoryFields.Controls.Add(this.comboBox_contactID);
            this.groupBox_customerNonMandatoryFields.Controls.Add(this.comboBox_defaultDistIsMaxBillable);
            this.groupBox_customerNonMandatoryFields.Controls.Add(this.comboBox_mileageIsBillable);
            this.groupBox_customerNonMandatoryFields.Controls.Add(this.comboBox_expenseIsBillable);
            this.groupBox_customerNonMandatoryFields.Controls.Add(this.comboBox_defaultMileageDistance);
            this.groupBox_customerNonMandatoryFields.Controls.Add(this.comboBox_invoicingAddressCountryID);
            this.groupBox_customerNonMandatoryFields.Controls.Add(this.comboBox_invoicingAddressState);
            this.groupBox_customerNonMandatoryFields.Controls.Add(this.comboBox_invoicingAddressCity);
            this.groupBox_customerNonMandatoryFields.Controls.Add(this.comboBox_invoicingAddressZipCode);
            this.groupBox_customerNonMandatoryFields.Controls.Add(this.comboBox_invoicingAddress3);
            this.groupBox_customerNonMandatoryFields.Controls.Add(this.comboBox_invoicingAddress2);
            this.groupBox_customerNonMandatoryFields.Controls.Add(this.comboBox_invoicingAddress);
            this.groupBox_customerNonMandatoryFields.Controls.Add(this.comboBox_VATNo);
            this.groupBox_customerNonMandatoryFields.Controls.Add(this.comboBox_useInvoicingAddress);
            this.groupBox_customerNonMandatoryFields.Controls.Add(this.comboBox_organizationNo);
            this.groupBox_customerNonMandatoryFields.Controls.Add(this.comboBox_eanNo);
            this.groupBox_customerNonMandatoryFields.Controls.Add(this.comboBox_useEanNo);
            this.groupBox_customerNonMandatoryFields.Controls.Add(this.label_vatNo);
            this.groupBox_customerNonMandatoryFields.Controls.Add(this.comboBox_state);
            this.groupBox_customerNonMandatoryFields.Controls.Add(this.comboBox_city);
            this.groupBox_customerNonMandatoryFields.Controls.Add(this.comboBox_zipCode);
            this.groupBox_customerNonMandatoryFields.Controls.Add(this.comboBox_address3);
            this.groupBox_customerNonMandatoryFields.Controls.Add(this.comboBox_address2);
            this.groupBox_customerNonMandatoryFields.Controls.Add(this.comboBox_address);
            this.groupBox_customerNonMandatoryFields.Controls.Add(this.comboBox_website);
            this.groupBox_customerNonMandatoryFields.Controls.Add(this.comboBox_email);
            this.groupBox_customerNonMandatoryFields.Controls.Add(this.comboBox_faxNo);
            this.groupBox_customerNonMandatoryFields.Controls.Add(this.comboBox_phoneNo);
            this.groupBox_customerNonMandatoryFields.Controls.Add(this.comboBox_industryID);
            this.groupBox_customerNonMandatoryFields.Controls.Add(this.comboBox_customerSince);
            this.groupBox_customerNonMandatoryFields.Controls.Add(this.comboBox_primaryKAMID);
            this.groupBox_customerNonMandatoryFields.Controls.Add(this.comboBox_nickName);
            this.groupBox_customerNonMandatoryFields.Controls.Add(this.checkBox_defaultPrimaryKAMID);
            this.groupBox_customerNonMandatoryFields.Controls.Add(this.label_invoiceAddressToUse);
            this.groupBox_customerNonMandatoryFields.Controls.Add(this.comboBox_customerNo);
            this.groupBox_customerNonMandatoryFields.Controls.Add(this.comboBox_secondaryKAMID);
            this.groupBox_customerNonMandatoryFields.Controls.Add(this.label_defaultMileageDistance);
            this.groupBox_customerNonMandatoryFields.Controls.Add(this.label_vatPercentage);
            this.groupBox_customerNonMandatoryFields.Controls.Add(this.label_useEanNo);
            this.groupBox_customerNonMandatoryFields.Controls.Add(this.label_calculateVAT);
            this.groupBox_customerNonMandatoryFields.Controls.Add(this.label_discountPercentage);
            this.groupBox_customerNonMandatoryFields.Controls.Add(this.label_invoicingAddressCountryID);
            this.groupBox_customerNonMandatoryFields.Controls.Add(this.label_paymentTermID);
            this.groupBox_customerNonMandatoryFields.Controls.Add(this.label_customerReferenceID);
            this.groupBox_customerNonMandatoryFields.Controls.Add(this.label_eanNo);
            this.groupBox_customerNonMandatoryFields.Controls.Add(this.label_internalReferenceID);
            this.groupBox_customerNonMandatoryFields.Controls.Add(this.label_contactID);
            this.groupBox_customerNonMandatoryFields.Controls.Add(this.label_defaultDistIsMaxBillable);
            this.groupBox_customerNonMandatoryFields.Controls.Add(this.label_organizationNo);
            this.groupBox_customerNonMandatoryFields.Controls.Add(this.label_email);
            this.groupBox_customerNonMandatoryFields.Controls.Add(this.label_industryID);
            this.groupBox_customerNonMandatoryFields.Controls.Add(this.label_secondaryKAMID);
            this.groupBox_customerNonMandatoryFields.Controls.Add(this.label_invoicingAddressState);
            this.groupBox_customerNonMandatoryFields.Controls.Add(this.label_expenseIsBillable);
            this.groupBox_customerNonMandatoryFields.Controls.Add(this.label_primaryKAMID);
            this.groupBox_customerNonMandatoryFields.Controls.Add(this.label_mileageIsBillable);
            this.groupBox_customerNonMandatoryFields.Controls.Add(this.label_invoicingAddress3);
            this.groupBox_customerNonMandatoryFields.Controls.Add(this.label_invoicingAddress2);
            this.groupBox_customerNonMandatoryFields.Controls.Add(this.label_invoicingAddressZipCode);
            this.groupBox_customerNonMandatoryFields.Controls.Add(this.label_invoicingAddressCity);
            this.groupBox_customerNonMandatoryFields.Controls.Add(this.label_invoicingAddress);
            this.groupBox_customerNonMandatoryFields.Controls.Add(this.label_useInvoicingAddress);
            this.groupBox_customerNonMandatoryFields.Controls.Add(this.label_state);
            this.groupBox_customerNonMandatoryFields.Controls.Add(this.label_city);
            this.groupBox_customerNonMandatoryFields.Controls.Add(this.label_zipCode);
            this.groupBox_customerNonMandatoryFields.Controls.Add(this.label_address3);
            this.groupBox_customerNonMandatoryFields.Controls.Add(this.label_address2);
            this.groupBox_customerNonMandatoryFields.Controls.Add(this.label_address);
            this.groupBox_customerNonMandatoryFields.Controls.Add(this.label_website);
            this.groupBox_customerNonMandatoryFields.Controls.Add(this.label_fax);
            this.groupBox_customerNonMandatoryFields.Controls.Add(this.label_phone);
            this.groupBox_customerNonMandatoryFields.Controls.Add(this.label_customerSince);
            this.groupBox_customerNonMandatoryFields.Controls.Add(this.label_nickname);
            this.groupBox_customerNonMandatoryFields.Controls.Add(this.label_customerNo);
            this.groupBox_customerNonMandatoryFields.Location = new System.Drawing.Point(322, 16);
            this.groupBox_customerNonMandatoryFields.Name = "groupBox_customerNonMandatoryFields";
            this.groupBox_customerNonMandatoryFields.Size = new System.Drawing.Size(649, 612);
            this.groupBox_customerNonMandatoryFields.TabIndex = 6;
            this.groupBox_customerNonMandatoryFields.TabStop = false;
            this.groupBox_customerNonMandatoryFields.Text = "Non-Mandatory";
            // 
            // checkBox_defaultVATPercentage
            // 
            this.checkBox_defaultVATPercentage.AutoSize = true;
            this.checkBox_defaultVATPercentage.Location = new System.Drawing.Point(584, 576);
            this.checkBox_defaultVATPercentage.Name = "checkBox_defaultVATPercentage";
            this.checkBox_defaultVATPercentage.Size = new System.Drawing.Size(64, 19);
            this.checkBox_defaultVATPercentage.TabIndex = 8;
            this.checkBox_defaultVATPercentage.Text = "Default";
            this.checkBox_defaultVATPercentage.UseVisualStyleBackColor = true;
            this.checkBox_defaultVATPercentage.CheckedChanged += new System.EventHandler(this.checkBox_defaultVATPercentage_CheckedChanged);
            // 
            // checkBox_defaultMileageIsBillable
            // 
            this.checkBox_defaultMileageIsBillable.AutoSize = true;
            this.checkBox_defaultMileageIsBillable.Location = new System.Drawing.Point(584, 315);
            this.checkBox_defaultMileageIsBillable.Name = "checkBox_defaultMileageIsBillable";
            this.checkBox_defaultMileageIsBillable.Size = new System.Drawing.Size(64, 19);
            this.checkBox_defaultMileageIsBillable.TabIndex = 8;
            this.checkBox_defaultMileageIsBillable.Text = "Default";
            this.checkBox_defaultMileageIsBillable.UseVisualStyleBackColor = true;
            this.checkBox_defaultMileageIsBillable.CheckedChanged += new System.EventHandler(this.checkBox_defaultMileageIsBillable_CheckedChanged);
            // 
            // checkBox_defaultExpenseIsBillable
            // 
            this.checkBox_defaultExpenseIsBillable.AutoSize = true;
            this.checkBox_defaultExpenseIsBillable.Location = new System.Drawing.Point(584, 286);
            this.checkBox_defaultExpenseIsBillable.Name = "checkBox_defaultExpenseIsBillable";
            this.checkBox_defaultExpenseIsBillable.Size = new System.Drawing.Size(64, 19);
            this.checkBox_defaultExpenseIsBillable.TabIndex = 8;
            this.checkBox_defaultExpenseIsBillable.Text = "Default";
            this.checkBox_defaultExpenseIsBillable.UseVisualStyleBackColor = true;
            this.checkBox_defaultExpenseIsBillable.CheckedChanged += new System.EventHandler(this.checkBox_defaultExpenseIsBillable_CheckedChanged);
            // 
            // checkBox_defaultPaymentTermID
            // 
            this.checkBox_defaultPaymentTermID.AutoSize = true;
            this.checkBox_defaultPaymentTermID.Location = new System.Drawing.Point(584, 489);
            this.checkBox_defaultPaymentTermID.Name = "checkBox_defaultPaymentTermID";
            this.checkBox_defaultPaymentTermID.Size = new System.Drawing.Size(64, 19);
            this.checkBox_defaultPaymentTermID.TabIndex = 8;
            this.checkBox_defaultPaymentTermID.Text = "Default";
            this.checkBox_defaultPaymentTermID.UseVisualStyleBackColor = true;
            this.checkBox_defaultPaymentTermID.CheckedChanged += new System.EventHandler(this.checkBox_defaultPaymentTermID_CheckedChanged);
            // 
            // checkBox_defaultIndustryID
            // 
            this.checkBox_defaultIndustryID.AutoSize = true;
            this.checkBox_defaultIndustryID.Location = new System.Drawing.Point(232, 170);
            this.checkBox_defaultIndustryID.Name = "checkBox_defaultIndustryID";
            this.checkBox_defaultIndustryID.Size = new System.Drawing.Size(64, 19);
            this.checkBox_defaultIndustryID.TabIndex = 8;
            this.checkBox_defaultIndustryID.Text = "Default";
            this.checkBox_defaultIndustryID.UseVisualStyleBackColor = true;
            this.checkBox_defaultIndustryID.CheckedChanged += new System.EventHandler(this.checkBox_defaultIndustryID_CheckedChanged);
            // 
            // checkBox_defaultSecondaryKAMID
            // 
            this.checkBox_defaultSecondaryKAMID.AutoSize = true;
            this.checkBox_defaultSecondaryKAMID.Location = new System.Drawing.Point(232, 112);
            this.checkBox_defaultSecondaryKAMID.Name = "checkBox_defaultSecondaryKAMID";
            this.checkBox_defaultSecondaryKAMID.Size = new System.Drawing.Size(64, 19);
            this.checkBox_defaultSecondaryKAMID.TabIndex = 8;
            this.checkBox_defaultSecondaryKAMID.Text = "Default";
            this.checkBox_defaultSecondaryKAMID.UseVisualStyleBackColor = true;
            this.checkBox_defaultSecondaryKAMID.CheckedChanged += new System.EventHandler(this.checkBox_defaultSecondaryKAMID_CheckedChanged);
            // 
            // comboBox_VATPercentage
            // 
            this.comboBox_VATPercentage.FormattingEnabled = true;
            this.comboBox_VATPercentage.Location = new System.Drawing.Point(469, 573);
            this.comboBox_VATPercentage.Name = "comboBox_VATPercentage";
            this.comboBox_VATPercentage.Size = new System.Drawing.Size(109, 23);
            this.comboBox_VATPercentage.TabIndex = 3;
            this.comboBox_VATPercentage.SelectedIndexChanged += new System.EventHandler(this.comboBox_VATPercentage_SelectedIndexChanged);
            // 
            // comboBox_calculateVAT
            // 
            this.comboBox_calculateVAT.FormattingEnabled = true;
            this.comboBox_calculateVAT.Location = new System.Drawing.Point(469, 544);
            this.comboBox_calculateVAT.Name = "comboBox_calculateVAT";
            this.comboBox_calculateVAT.Size = new System.Drawing.Size(109, 23);
            this.comboBox_calculateVAT.TabIndex = 3;
            this.comboBox_calculateVAT.SelectedIndexChanged += new System.EventHandler(this.comboBox_calculateVAT_SelectedIndexChanged);
            // 
            // comboBox_discountPercentage
            // 
            this.comboBox_discountPercentage.FormattingEnabled = true;
            this.comboBox_discountPercentage.Location = new System.Drawing.Point(469, 515);
            this.comboBox_discountPercentage.Name = "comboBox_discountPercentage";
            this.comboBox_discountPercentage.Size = new System.Drawing.Size(109, 23);
            this.comboBox_discountPercentage.TabIndex = 3;
            this.comboBox_discountPercentage.SelectedIndexChanged += new System.EventHandler(this.comboBox_discountPercentage_SelectedIndexChanged);
            // 
            // comboBox_paymentTermID
            // 
            this.comboBox_paymentTermID.FormattingEnabled = true;
            this.comboBox_paymentTermID.Location = new System.Drawing.Point(469, 486);
            this.comboBox_paymentTermID.Name = "comboBox_paymentTermID";
            this.comboBox_paymentTermID.Size = new System.Drawing.Size(109, 23);
            this.comboBox_paymentTermID.TabIndex = 3;
            this.comboBox_paymentTermID.SelectedIndexChanged += new System.EventHandler(this.comboBox_paymentTermID_SelectedIndexChanged);
            // 
            // comboBox_customerReferenceID
            // 
            this.comboBox_customerReferenceID.FormattingEnabled = true;
            this.comboBox_customerReferenceID.Location = new System.Drawing.Point(480, 457);
            this.comboBox_customerReferenceID.Name = "comboBox_customerReferenceID";
            this.comboBox_customerReferenceID.Size = new System.Drawing.Size(98, 23);
            this.comboBox_customerReferenceID.TabIndex = 3;
            this.comboBox_customerReferenceID.SelectedIndexChanged += new System.EventHandler(this.comboBox_customerReferenceID_SelectedIndexChanged);
            // 
            // comboBox_internalReferenceID
            // 
            this.comboBox_internalReferenceID.FormattingEnabled = true;
            this.comboBox_internalReferenceID.Location = new System.Drawing.Point(469, 428);
            this.comboBox_internalReferenceID.Name = "comboBox_internalReferenceID";
            this.comboBox_internalReferenceID.Size = new System.Drawing.Size(109, 23);
            this.comboBox_internalReferenceID.TabIndex = 3;
            this.comboBox_internalReferenceID.SelectedIndexChanged += new System.EventHandler(this.comboBox_internalReferenceID_SelectedIndexChanged);
            // 
            // comboBox_invoiceAddressToUse
            // 
            this.comboBox_invoiceAddressToUse.FormattingEnabled = true;
            this.comboBox_invoiceAddressToUse.Location = new System.Drawing.Point(479, 399);
            this.comboBox_invoiceAddressToUse.Name = "comboBox_invoiceAddressToUse";
            this.comboBox_invoiceAddressToUse.Size = new System.Drawing.Size(99, 23);
            this.comboBox_invoiceAddressToUse.TabIndex = 3;
            this.comboBox_invoiceAddressToUse.SelectedIndexChanged += new System.EventHandler(this.comboBox_invoiceAddressToUse_SelectedIndexChanged);
            // 
            // comboBox_contactID
            // 
            this.comboBox_contactID.FormattingEnabled = true;
            this.comboBox_contactID.Location = new System.Drawing.Point(469, 370);
            this.comboBox_contactID.Name = "comboBox_contactID";
            this.comboBox_contactID.Size = new System.Drawing.Size(109, 23);
            this.comboBox_contactID.TabIndex = 3;
            this.comboBox_contactID.SelectedIndexChanged += new System.EventHandler(this.comboBox_contactID_SelectedIndexChanged);
            // 
            // comboBox_defaultDistIsMaxBillable
            // 
            this.comboBox_defaultDistIsMaxBillable.FormattingEnabled = true;
            this.comboBox_defaultDistIsMaxBillable.Location = new System.Drawing.Point(492, 341);
            this.comboBox_defaultDistIsMaxBillable.Name = "comboBox_defaultDistIsMaxBillable";
            this.comboBox_defaultDistIsMaxBillable.Size = new System.Drawing.Size(86, 23);
            this.comboBox_defaultDistIsMaxBillable.TabIndex = 3;
            this.comboBox_defaultDistIsMaxBillable.SelectedIndexChanged += new System.EventHandler(this.comboBox_defaultDistIsMaxBillable_SelectedIndexChanged);
            // 
            // comboBox_mileageIsBillable
            // 
            this.comboBox_mileageIsBillable.FormattingEnabled = true;
            this.comboBox_mileageIsBillable.Location = new System.Drawing.Point(469, 312);
            this.comboBox_mileageIsBillable.Name = "comboBox_mileageIsBillable";
            this.comboBox_mileageIsBillable.Size = new System.Drawing.Size(109, 23);
            this.comboBox_mileageIsBillable.TabIndex = 3;
            this.comboBox_mileageIsBillable.SelectedIndexChanged += new System.EventHandler(this.comboBox_mileageIsBillable_SelectedIndexChanged);
            // 
            // comboBox_expenseIsBillable
            // 
            this.comboBox_expenseIsBillable.FormattingEnabled = true;
            this.comboBox_expenseIsBillable.Location = new System.Drawing.Point(469, 283);
            this.comboBox_expenseIsBillable.Name = "comboBox_expenseIsBillable";
            this.comboBox_expenseIsBillable.Size = new System.Drawing.Size(109, 23);
            this.comboBox_expenseIsBillable.TabIndex = 3;
            this.comboBox_expenseIsBillable.SelectedIndexChanged += new System.EventHandler(this.comboBox_expenseIsBillable_SelectedIndexChanged);
            // 
            // comboBox_defaultMileageDistance
            // 
            this.comboBox_defaultMileageDistance.FormattingEnabled = true;
            this.comboBox_defaultMileageDistance.Location = new System.Drawing.Point(484, 254);
            this.comboBox_defaultMileageDistance.Name = "comboBox_defaultMileageDistance";
            this.comboBox_defaultMileageDistance.Size = new System.Drawing.Size(94, 23);
            this.comboBox_defaultMileageDistance.TabIndex = 3;
            this.comboBox_defaultMileageDistance.SelectedIndexChanged += new System.EventHandler(this.comboBox_defaultMileageDistance_SelectedIndexChanged);
            // 
            // comboBox_invoicingAddressCountryID
            // 
            this.comboBox_invoicingAddressCountryID.FormattingEnabled = true;
            this.comboBox_invoicingAddressCountryID.Location = new System.Drawing.Point(507, 225);
            this.comboBox_invoicingAddressCountryID.Name = "comboBox_invoicingAddressCountryID";
            this.comboBox_invoicingAddressCountryID.Size = new System.Drawing.Size(71, 23);
            this.comboBox_invoicingAddressCountryID.TabIndex = 3;
            this.comboBox_invoicingAddressCountryID.SelectedIndexChanged += new System.EventHandler(this.comboBox_invoicingAddressCountryID_SelectedIndexChanged);
            // 
            // comboBox_invoicingAddressState
            // 
            this.comboBox_invoicingAddressState.FormattingEnabled = true;
            this.comboBox_invoicingAddressState.Location = new System.Drawing.Point(476, 196);
            this.comboBox_invoicingAddressState.Name = "comboBox_invoicingAddressState";
            this.comboBox_invoicingAddressState.Size = new System.Drawing.Size(102, 23);
            this.comboBox_invoicingAddressState.TabIndex = 3;
            this.comboBox_invoicingAddressState.SelectedIndexChanged += new System.EventHandler(this.comboBox_invoicingAddressState_SelectedIndexChanged);
            // 
            // comboBox_invoicingAddressCity
            // 
            this.comboBox_invoicingAddressCity.FormattingEnabled = true;
            this.comboBox_invoicingAddressCity.Location = new System.Drawing.Point(476, 167);
            this.comboBox_invoicingAddressCity.Name = "comboBox_invoicingAddressCity";
            this.comboBox_invoicingAddressCity.Size = new System.Drawing.Size(102, 23);
            this.comboBox_invoicingAddressCity.TabIndex = 3;
            this.comboBox_invoicingAddressCity.SelectedIndexChanged += new System.EventHandler(this.comboBox_invoicingAddressCity_SelectedIndexChanged);
            // 
            // comboBox_invoicingAddressZipCode
            // 
            this.comboBox_invoicingAddressZipCode.FormattingEnabled = true;
            this.comboBox_invoicingAddressZipCode.Location = new System.Drawing.Point(498, 138);
            this.comboBox_invoicingAddressZipCode.Name = "comboBox_invoicingAddressZipCode";
            this.comboBox_invoicingAddressZipCode.Size = new System.Drawing.Size(80, 23);
            this.comboBox_invoicingAddressZipCode.TabIndex = 3;
            this.comboBox_invoicingAddressZipCode.SelectedIndexChanged += new System.EventHandler(this.comboBox_invoicingAddressZipCode_SelectedIndexChanged);
            // 
            // comboBox_invoicingAddress3
            // 
            this.comboBox_invoicingAddress3.FormattingEnabled = true;
            this.comboBox_invoicingAddress3.Location = new System.Drawing.Point(469, 109);
            this.comboBox_invoicingAddress3.Name = "comboBox_invoicingAddress3";
            this.comboBox_invoicingAddress3.Size = new System.Drawing.Size(109, 23);
            this.comboBox_invoicingAddress3.TabIndex = 3;
            this.comboBox_invoicingAddress3.SelectedIndexChanged += new System.EventHandler(this.comboBox_invoicingAddress3_SelectedIndexChanged);
            // 
            // comboBox_invoicingAddress2
            // 
            this.comboBox_invoicingAddress2.FormattingEnabled = true;
            this.comboBox_invoicingAddress2.Location = new System.Drawing.Point(469, 80);
            this.comboBox_invoicingAddress2.Name = "comboBox_invoicingAddress2";
            this.comboBox_invoicingAddress2.Size = new System.Drawing.Size(109, 23);
            this.comboBox_invoicingAddress2.TabIndex = 3;
            this.comboBox_invoicingAddress2.SelectedIndexChanged += new System.EventHandler(this.comboBox_invoicingAddress2_SelectedIndexChanged);
            // 
            // comboBox_invoicingAddress
            // 
            this.comboBox_invoicingAddress.FormattingEnabled = true;
            this.comboBox_invoicingAddress.Location = new System.Drawing.Point(469, 51);
            this.comboBox_invoicingAddress.Name = "comboBox_invoicingAddress";
            this.comboBox_invoicingAddress.Size = new System.Drawing.Size(109, 23);
            this.comboBox_invoicingAddress.TabIndex = 3;
            this.comboBox_invoicingAddress.SelectedIndexChanged += new System.EventHandler(this.comboBox_invoicingAddress_SelectedIndexChanged);
            // 
            // comboBox_VATNo
            // 
            this.comboBox_VATNo.FormattingEnabled = true;
            this.comboBox_VATNo.Location = new System.Drawing.Point(117, 573);
            this.comboBox_VATNo.Name = "comboBox_VATNo";
            this.comboBox_VATNo.Size = new System.Drawing.Size(109, 23);
            this.comboBox_VATNo.TabIndex = 3;
            this.comboBox_VATNo.SelectedIndexChanged += new System.EventHandler(this.comboBox_VATNo_SelectedIndexChanged);
            // 
            // comboBox_useInvoicingAddress
            // 
            this.comboBox_useInvoicingAddress.FormattingEnabled = true;
            this.comboBox_useInvoicingAddress.Location = new System.Drawing.Point(469, 21);
            this.comboBox_useInvoicingAddress.Name = "comboBox_useInvoicingAddress";
            this.comboBox_useInvoicingAddress.Size = new System.Drawing.Size(109, 23);
            this.comboBox_useInvoicingAddress.TabIndex = 3;
            this.comboBox_useInvoicingAddress.SelectedIndexChanged += new System.EventHandler(this.comboBox_useInvoicingAddress_SelectedIndexChanged);
            // 
            // comboBox_organizationNo
            // 
            this.comboBox_organizationNo.FormattingEnabled = true;
            this.comboBox_organizationNo.Location = new System.Drawing.Point(117, 544);
            this.comboBox_organizationNo.Name = "comboBox_organizationNo";
            this.comboBox_organizationNo.Size = new System.Drawing.Size(109, 23);
            this.comboBox_organizationNo.TabIndex = 3;
            this.comboBox_organizationNo.SelectedIndexChanged += new System.EventHandler(this.comboBox_organizationNo_SelectedIndexChanged);
            // 
            // comboBox_eanNo
            // 
            this.comboBox_eanNo.FormattingEnabled = true;
            this.comboBox_eanNo.Location = new System.Drawing.Point(117, 515);
            this.comboBox_eanNo.Name = "comboBox_eanNo";
            this.comboBox_eanNo.Size = new System.Drawing.Size(109, 23);
            this.comboBox_eanNo.TabIndex = 3;
            this.comboBox_eanNo.SelectedIndexChanged += new System.EventHandler(this.comboBox_eanNo_SelectedIndexChanged);
            // 
            // comboBox_useEanNo
            // 
            this.comboBox_useEanNo.FormattingEnabled = true;
            this.comboBox_useEanNo.Location = new System.Drawing.Point(117, 486);
            this.comboBox_useEanNo.Name = "comboBox_useEanNo";
            this.comboBox_useEanNo.Size = new System.Drawing.Size(109, 23);
            this.comboBox_useEanNo.TabIndex = 3;
            this.comboBox_useEanNo.SelectedIndexChanged += new System.EventHandler(this.comboBox_useEanNo_SelectedIndexChanged);
            // 
            // label_vatNo
            // 
            this.label_vatNo.AutoSize = true;
            this.label_vatNo.Location = new System.Drawing.Point(6, 576);
            this.label_vatNo.Name = "label_vatNo";
            this.label_vatNo.Size = new System.Drawing.Size(45, 15);
            this.label_vatNo.TabIndex = 1;
            this.label_vatNo.Text = "VAT No";
            // 
            // comboBox_state
            // 
            this.comboBox_state.FormattingEnabled = true;
            this.comboBox_state.Location = new System.Drawing.Point(117, 457);
            this.comboBox_state.Name = "comboBox_state";
            this.comboBox_state.Size = new System.Drawing.Size(109, 23);
            this.comboBox_state.TabIndex = 3;
            this.comboBox_state.SelectedIndexChanged += new System.EventHandler(this.comboBox_state_SelectedIndexChanged);
            // 
            // comboBox_city
            // 
            this.comboBox_city.FormattingEnabled = true;
            this.comboBox_city.Location = new System.Drawing.Point(117, 428);
            this.comboBox_city.Name = "comboBox_city";
            this.comboBox_city.Size = new System.Drawing.Size(109, 23);
            this.comboBox_city.TabIndex = 3;
            this.comboBox_city.SelectedIndexChanged += new System.EventHandler(this.comboBox_city_SelectedIndexChanged);
            // 
            // comboBox_zipCode
            // 
            this.comboBox_zipCode.FormattingEnabled = true;
            this.comboBox_zipCode.Location = new System.Drawing.Point(117, 399);
            this.comboBox_zipCode.Name = "comboBox_zipCode";
            this.comboBox_zipCode.Size = new System.Drawing.Size(109, 23);
            this.comboBox_zipCode.TabIndex = 3;
            this.comboBox_zipCode.SelectedIndexChanged += new System.EventHandler(this.comboBox_zipCode_SelectedIndexChanged);
            // 
            // comboBox_address3
            // 
            this.comboBox_address3.FormattingEnabled = true;
            this.comboBox_address3.Location = new System.Drawing.Point(117, 370);
            this.comboBox_address3.Name = "comboBox_address3";
            this.comboBox_address3.Size = new System.Drawing.Size(109, 23);
            this.comboBox_address3.TabIndex = 3;
            this.comboBox_address3.SelectedIndexChanged += new System.EventHandler(this.comboBox_address3_SelectedIndexChanged);
            // 
            // comboBox_address2
            // 
            this.comboBox_address2.FormattingEnabled = true;
            this.comboBox_address2.Location = new System.Drawing.Point(117, 341);
            this.comboBox_address2.Name = "comboBox_address2";
            this.comboBox_address2.Size = new System.Drawing.Size(109, 23);
            this.comboBox_address2.TabIndex = 3;
            this.comboBox_address2.SelectedIndexChanged += new System.EventHandler(this.comboBox_address2_SelectedIndexChanged);
            // 
            // comboBox_address
            // 
            this.comboBox_address.FormattingEnabled = true;
            this.comboBox_address.Location = new System.Drawing.Point(117, 312);
            this.comboBox_address.Name = "comboBox_address";
            this.comboBox_address.Size = new System.Drawing.Size(109, 23);
            this.comboBox_address.TabIndex = 3;
            this.comboBox_address.SelectedIndexChanged += new System.EventHandler(this.comboBox_address_SelectedIndexChanged);
            // 
            // comboBox_website
            // 
            this.comboBox_website.FormattingEnabled = true;
            this.comboBox_website.Location = new System.Drawing.Point(117, 283);
            this.comboBox_website.Name = "comboBox_website";
            this.comboBox_website.Size = new System.Drawing.Size(109, 23);
            this.comboBox_website.TabIndex = 3;
            this.comboBox_website.SelectedIndexChanged += new System.EventHandler(this.comboBox_website_SelectedIndexChanged);
            // 
            // comboBox_email
            // 
            this.comboBox_email.FormattingEnabled = true;
            this.comboBox_email.Location = new System.Drawing.Point(117, 254);
            this.comboBox_email.Name = "comboBox_email";
            this.comboBox_email.Size = new System.Drawing.Size(109, 23);
            this.comboBox_email.TabIndex = 3;
            this.comboBox_email.SelectedIndexChanged += new System.EventHandler(this.comboBox_email_SelectedIndexChanged);
            // 
            // comboBox_faxNo
            // 
            this.comboBox_faxNo.FormattingEnabled = true;
            this.comboBox_faxNo.Location = new System.Drawing.Point(117, 225);
            this.comboBox_faxNo.Name = "comboBox_faxNo";
            this.comboBox_faxNo.Size = new System.Drawing.Size(109, 23);
            this.comboBox_faxNo.TabIndex = 3;
            this.comboBox_faxNo.SelectedIndexChanged += new System.EventHandler(this.comboBox_faxNo_SelectedIndexChanged);
            // 
            // comboBox_phoneNo
            // 
            this.comboBox_phoneNo.FormattingEnabled = true;
            this.comboBox_phoneNo.Location = new System.Drawing.Point(117, 196);
            this.comboBox_phoneNo.Name = "comboBox_phoneNo";
            this.comboBox_phoneNo.Size = new System.Drawing.Size(109, 23);
            this.comboBox_phoneNo.TabIndex = 3;
            this.comboBox_phoneNo.SelectedIndexChanged += new System.EventHandler(this.comboBox_phoneNo_SelectedIndexChanged);
            // 
            // comboBox_industryID
            // 
            this.comboBox_industryID.FormattingEnabled = true;
            this.comboBox_industryID.Location = new System.Drawing.Point(117, 167);
            this.comboBox_industryID.Name = "comboBox_industryID";
            this.comboBox_industryID.Size = new System.Drawing.Size(109, 23);
            this.comboBox_industryID.TabIndex = 3;
            this.comboBox_industryID.SelectedIndexChanged += new System.EventHandler(this.comboBox_industryID_SelectedIndexChanged);
            // 
            // comboBox_customerSince
            // 
            this.comboBox_customerSince.FormattingEnabled = true;
            this.comboBox_customerSince.Location = new System.Drawing.Point(117, 138);
            this.comboBox_customerSince.Name = "comboBox_customerSince";
            this.comboBox_customerSince.Size = new System.Drawing.Size(109, 23);
            this.comboBox_customerSince.TabIndex = 3;
            this.comboBox_customerSince.SelectedIndexChanged += new System.EventHandler(this.comboBox_customerSince_SelectedIndexChanged);
            // 
            // comboBox_primaryKAMID
            // 
            this.comboBox_primaryKAMID.FormattingEnabled = true;
            this.comboBox_primaryKAMID.Location = new System.Drawing.Point(117, 80);
            this.comboBox_primaryKAMID.Name = "comboBox_primaryKAMID";
            this.comboBox_primaryKAMID.Size = new System.Drawing.Size(109, 23);
            this.comboBox_primaryKAMID.TabIndex = 3;
            this.comboBox_primaryKAMID.SelectedIndexChanged += new System.EventHandler(this.comboBox_primaryKAMID_SelectedIndexChanged);
            // 
            // comboBox_nickName
            // 
            this.comboBox_nickName.FormattingEnabled = true;
            this.comboBox_nickName.Location = new System.Drawing.Point(117, 51);
            this.comboBox_nickName.Name = "comboBox_nickName";
            this.comboBox_nickName.Size = new System.Drawing.Size(109, 23);
            this.comboBox_nickName.TabIndex = 3;
            this.comboBox_nickName.SelectedIndexChanged += new System.EventHandler(this.comboBox_nickName_SelectedIndexChanged);
            // 
            // checkBox_defaultPrimaryKAMID
            // 
            this.checkBox_defaultPrimaryKAMID.AutoSize = true;
            this.checkBox_defaultPrimaryKAMID.Location = new System.Drawing.Point(232, 84);
            this.checkBox_defaultPrimaryKAMID.Name = "checkBox_defaultPrimaryKAMID";
            this.checkBox_defaultPrimaryKAMID.Size = new System.Drawing.Size(64, 19);
            this.checkBox_defaultPrimaryKAMID.TabIndex = 8;
            this.checkBox_defaultPrimaryKAMID.Text = "Default";
            this.checkBox_defaultPrimaryKAMID.UseVisualStyleBackColor = true;
            this.checkBox_defaultPrimaryKAMID.CheckedChanged += new System.EventHandler(this.checkBox_defaultPrimaryKAMID_CheckedChanged);
            // 
            // label_invoiceAddressToUse
            // 
            this.label_invoiceAddressToUse.AutoSize = true;
            this.label_invoiceAddressToUse.Location = new System.Drawing.Point(340, 402);
            this.label_invoiceAddressToUse.Name = "label_invoiceAddressToUse";
            this.label_invoiceAddressToUse.Size = new System.Drawing.Size(133, 15);
            this.label_invoiceAddressToUse.TabIndex = 5;
            this.label_invoiceAddressToUse.Text = "Invoice Address To Use :";
            // 
            // comboBox_customerNo
            // 
            this.comboBox_customerNo.FormattingEnabled = true;
            this.comboBox_customerNo.Location = new System.Drawing.Point(117, 21);
            this.comboBox_customerNo.Name = "comboBox_customerNo";
            this.comboBox_customerNo.Size = new System.Drawing.Size(109, 23);
            this.comboBox_customerNo.TabIndex = 3;
            this.comboBox_customerNo.SelectedIndexChanged += new System.EventHandler(this.comboBox_customerNo_SelectedIndexChanged);
            // 
            // comboBox_secondaryKAMID
            // 
            this.comboBox_secondaryKAMID.FormattingEnabled = true;
            this.comboBox_secondaryKAMID.Location = new System.Drawing.Point(117, 109);
            this.comboBox_secondaryKAMID.Name = "comboBox_secondaryKAMID";
            this.comboBox_secondaryKAMID.Size = new System.Drawing.Size(109, 23);
            this.comboBox_secondaryKAMID.TabIndex = 3;
            this.comboBox_secondaryKAMID.SelectedIndexChanged += new System.EventHandler(this.comboBox_secondaryKAMID_SelectedIndexChanged);
            // 
            // label_defaultMileageDistance
            // 
            this.label_defaultMileageDistance.AutoSize = true;
            this.label_defaultMileageDistance.Location = new System.Drawing.Point(340, 257);
            this.label_defaultMileageDistance.Name = "label_defaultMileageDistance";
            this.label_defaultMileageDistance.Size = new System.Drawing.Size(138, 15);
            this.label_defaultMileageDistance.TabIndex = 1;
            this.label_defaultMileageDistance.Text = "Default Mileage Distance";
            // 
            // label_vatPercentage
            // 
            this.label_vatPercentage.AutoSize = true;
            this.label_vatPercentage.Location = new System.Drawing.Point(340, 576);
            this.label_vatPercentage.Name = "label_vatPercentage";
            this.label_vatPercentage.Size = new System.Drawing.Size(88, 15);
            this.label_vatPercentage.TabIndex = 1;
            this.label_vatPercentage.Text = "VAT Percentage";
            // 
            // label_useEanNo
            // 
            this.label_useEanNo.AutoSize = true;
            this.label_useEanNo.Location = new System.Drawing.Point(6, 489);
            this.label_useEanNo.Name = "label_useEanNo";
            this.label_useEanNo.Size = new System.Drawing.Size(67, 15);
            this.label_useEanNo.TabIndex = 1;
            this.label_useEanNo.Text = "Use Ean No";
            // 
            // label_calculateVAT
            // 
            this.label_calculateVAT.AutoSize = true;
            this.label_calculateVAT.Location = new System.Drawing.Point(340, 547);
            this.label_calculateVAT.Name = "label_calculateVAT";
            this.label_calculateVAT.Size = new System.Drawing.Size(78, 15);
            this.label_calculateVAT.TabIndex = 1;
            this.label_calculateVAT.Text = "Calculate VAT";
            // 
            // label_discountPercentage
            // 
            this.label_discountPercentage.AutoSize = true;
            this.label_discountPercentage.Location = new System.Drawing.Point(340, 518);
            this.label_discountPercentage.Name = "label_discountPercentage";
            this.label_discountPercentage.Size = new System.Drawing.Size(122, 15);
            this.label_discountPercentage.TabIndex = 1;
            this.label_discountPercentage.Text = "Discount Percentage :";
            // 
            // label_invoicingAddressCountryID
            // 
            this.label_invoicingAddressCountryID.AutoSize = true;
            this.label_invoicingAddressCountryID.Location = new System.Drawing.Point(340, 228);
            this.label_invoicingAddressCountryID.Name = "label_invoicingAddressCountryID";
            this.label_invoicingAddressCountryID.Size = new System.Drawing.Size(161, 15);
            this.label_invoicingAddressCountryID.TabIndex = 5;
            this.label_invoicingAddressCountryID.Text = "Invoicing Address Country ID";
            // 
            // label_paymentTermID
            // 
            this.label_paymentTermID.AutoSize = true;
            this.label_paymentTermID.Location = new System.Drawing.Point(340, 489);
            this.label_paymentTermID.Name = "label_paymentTermID";
            this.label_paymentTermID.Size = new System.Drawing.Size(103, 15);
            this.label_paymentTermID.TabIndex = 1;
            this.label_paymentTermID.Text = "Payment Term ID :";
            // 
            // label_customerReferenceID
            // 
            this.label_customerReferenceID.AutoSize = true;
            this.label_customerReferenceID.Location = new System.Drawing.Point(340, 460);
            this.label_customerReferenceID.Name = "label_customerReferenceID";
            this.label_customerReferenceID.Size = new System.Drawing.Size(134, 15);
            this.label_customerReferenceID.TabIndex = 1;
            this.label_customerReferenceID.Text = "Customer Reference ID :";
            // 
            // label_eanNo
            // 
            this.label_eanNo.AutoSize = true;
            this.label_eanNo.Location = new System.Drawing.Point(6, 518);
            this.label_eanNo.Name = "label_eanNo";
            this.label_eanNo.Size = new System.Drawing.Size(45, 15);
            this.label_eanNo.TabIndex = 1;
            this.label_eanNo.Text = "Ean No";
            // 
            // label_internalReferenceID
            // 
            this.label_internalReferenceID.AutoSize = true;
            this.label_internalReferenceID.Location = new System.Drawing.Point(340, 431);
            this.label_internalReferenceID.Name = "label_internalReferenceID";
            this.label_internalReferenceID.Size = new System.Drawing.Size(122, 15);
            this.label_internalReferenceID.TabIndex = 1;
            this.label_internalReferenceID.Text = "Internal Reference ID :";
            // 
            // label_contactID
            // 
            this.label_contactID.AutoSize = true;
            this.label_contactID.Location = new System.Drawing.Point(340, 373);
            this.label_contactID.Name = "label_contactID";
            this.label_contactID.Size = new System.Drawing.Size(63, 15);
            this.label_contactID.TabIndex = 1;
            this.label_contactID.Text = "Contact ID";
            // 
            // label_defaultDistIsMaxBillable
            // 
            this.label_defaultDistIsMaxBillable.AutoSize = true;
            this.label_defaultDistIsMaxBillable.Location = new System.Drawing.Point(340, 344);
            this.label_defaultDistIsMaxBillable.Name = "label_defaultDistIsMaxBillable";
            this.label_defaultDistIsMaxBillable.Size = new System.Drawing.Size(146, 15);
            this.label_defaultDistIsMaxBillable.TabIndex = 1;
            this.label_defaultDistIsMaxBillable.Text = "Default Dist Is Max Billable";
            // 
            // label_organizationNo
            // 
            this.label_organizationNo.AutoSize = true;
            this.label_organizationNo.Location = new System.Drawing.Point(6, 547);
            this.label_organizationNo.Name = "label_organizationNo";
            this.label_organizationNo.Size = new System.Drawing.Size(94, 15);
            this.label_organizationNo.TabIndex = 1;
            this.label_organizationNo.Text = "Organization No";
            // 
            // label_email
            // 
            this.label_email.AutoSize = true;
            this.label_email.Location = new System.Drawing.Point(6, 257);
            this.label_email.Name = "label_email";
            this.label_email.Size = new System.Drawing.Size(36, 15);
            this.label_email.TabIndex = 1;
            this.label_email.Text = "Email";
            // 
            // label_industryID
            // 
            this.label_industryID.AutoSize = true;
            this.label_industryID.Location = new System.Drawing.Point(6, 170);
            this.label_industryID.Name = "label_industryID";
            this.label_industryID.Size = new System.Drawing.Size(64, 15);
            this.label_industryID.TabIndex = 1;
            this.label_industryID.Text = "Industry ID";
            // 
            // label_secondaryKAMID
            // 
            this.label_secondaryKAMID.AutoSize = true;
            this.label_secondaryKAMID.Location = new System.Drawing.Point(6, 112);
            this.label_secondaryKAMID.Name = "label_secondaryKAMID";
            this.label_secondaryKAMID.Size = new System.Drawing.Size(105, 15);
            this.label_secondaryKAMID.TabIndex = 1;
            this.label_secondaryKAMID.Text = "Secondary KAM ID";
            // 
            // label_invoicingAddressState
            // 
            this.label_invoicingAddressState.AutoSize = true;
            this.label_invoicingAddressState.Location = new System.Drawing.Point(340, 199);
            this.label_invoicingAddressState.Name = "label_invoicingAddressState";
            this.label_invoicingAddressState.Size = new System.Drawing.Size(130, 15);
            this.label_invoicingAddressState.TabIndex = 1;
            this.label_invoicingAddressState.Text = "Invoicing Address State";
            // 
            // label_expenseIsBillable
            // 
            this.label_expenseIsBillable.AutoSize = true;
            this.label_expenseIsBillable.Location = new System.Drawing.Point(340, 286);
            this.label_expenseIsBillable.Name = "label_expenseIsBillable";
            this.label_expenseIsBillable.Size = new System.Drawing.Size(102, 15);
            this.label_expenseIsBillable.TabIndex = 1;
            this.label_expenseIsBillable.Text = "Expense Is Billable";
            // 
            // label_primaryKAMID
            // 
            this.label_primaryKAMID.AutoSize = true;
            this.label_primaryKAMID.Location = new System.Drawing.Point(6, 84);
            this.label_primaryKAMID.Name = "label_primaryKAMID";
            this.label_primaryKAMID.Size = new System.Drawing.Size(91, 15);
            this.label_primaryKAMID.TabIndex = 1;
            this.label_primaryKAMID.Text = "Primary KAM ID";
            // 
            // label_mileageIsBillable
            // 
            this.label_mileageIsBillable.AutoSize = true;
            this.label_mileageIsBillable.Location = new System.Drawing.Point(340, 315);
            this.label_mileageIsBillable.Name = "label_mileageIsBillable";
            this.label_mileageIsBillable.Size = new System.Drawing.Size(101, 15);
            this.label_mileageIsBillable.TabIndex = 1;
            this.label_mileageIsBillable.Text = "Mileage Is Billable";
            // 
            // label_invoicingAddress3
            // 
            this.label_invoicingAddress3.AutoSize = true;
            this.label_invoicingAddress3.Location = new System.Drawing.Point(340, 112);
            this.label_invoicingAddress3.Name = "label_invoicingAddress3";
            this.label_invoicingAddress3.Size = new System.Drawing.Size(110, 15);
            this.label_invoicingAddress3.TabIndex = 1;
            this.label_invoicingAddress3.Text = "Invoicing Address 3";
            // 
            // label_invoicingAddress2
            // 
            this.label_invoicingAddress2.AutoSize = true;
            this.label_invoicingAddress2.Location = new System.Drawing.Point(340, 84);
            this.label_invoicingAddress2.Name = "label_invoicingAddress2";
            this.label_invoicingAddress2.Size = new System.Drawing.Size(110, 15);
            this.label_invoicingAddress2.TabIndex = 1;
            this.label_invoicingAddress2.Text = "Invoicing Address 2";
            // 
            // label_invoicingAddressZipCode
            // 
            this.label_invoicingAddressZipCode.AutoSize = true;
            this.label_invoicingAddressZipCode.Location = new System.Drawing.Point(340, 141);
            this.label_invoicingAddressZipCode.Name = "label_invoicingAddressZipCode";
            this.label_invoicingAddressZipCode.Size = new System.Drawing.Size(152, 15);
            this.label_invoicingAddressZipCode.TabIndex = 1;
            this.label_invoicingAddressZipCode.Text = "Invoicing Address Zip Code";
            // 
            // label_invoicingAddressCity
            // 
            this.label_invoicingAddressCity.AutoSize = true;
            this.label_invoicingAddressCity.Location = new System.Drawing.Point(340, 170);
            this.label_invoicingAddressCity.Name = "label_invoicingAddressCity";
            this.label_invoicingAddressCity.Size = new System.Drawing.Size(125, 15);
            this.label_invoicingAddressCity.TabIndex = 1;
            this.label_invoicingAddressCity.Text = "Invoicing Address City";
            // 
            // label_invoicingAddress
            // 
            this.label_invoicingAddress.AutoSize = true;
            this.label_invoicingAddress.Location = new System.Drawing.Point(340, 54);
            this.label_invoicingAddress.Name = "label_invoicingAddress";
            this.label_invoicingAddress.Size = new System.Drawing.Size(101, 15);
            this.label_invoicingAddress.TabIndex = 1;
            this.label_invoicingAddress.Text = "Invoicing Address";
            // 
            // label_useInvoicingAddress
            // 
            this.label_useInvoicingAddress.AutoSize = true;
            this.label_useInvoicingAddress.Location = new System.Drawing.Point(340, 26);
            this.label_useInvoicingAddress.Name = "label_useInvoicingAddress";
            this.label_useInvoicingAddress.Size = new System.Drawing.Size(123, 15);
            this.label_useInvoicingAddress.TabIndex = 1;
            this.label_useInvoicingAddress.Text = "Use Invoicing Address";
            // 
            // label_state
            // 
            this.label_state.AutoSize = true;
            this.label_state.Location = new System.Drawing.Point(6, 460);
            this.label_state.Name = "label_state";
            this.label_state.Size = new System.Drawing.Size(33, 15);
            this.label_state.TabIndex = 1;
            this.label_state.Text = "State";
            // 
            // label_city
            // 
            this.label_city.AutoSize = true;
            this.label_city.Location = new System.Drawing.Point(6, 431);
            this.label_city.Name = "label_city";
            this.label_city.Size = new System.Drawing.Size(28, 15);
            this.label_city.TabIndex = 1;
            this.label_city.Text = "City";
            // 
            // label_zipCode
            // 
            this.label_zipCode.AutoSize = true;
            this.label_zipCode.Location = new System.Drawing.Point(6, 402);
            this.label_zipCode.Name = "label_zipCode";
            this.label_zipCode.Size = new System.Drawing.Size(55, 15);
            this.label_zipCode.TabIndex = 1;
            this.label_zipCode.Text = "Zip Code";
            // 
            // label_address3
            // 
            this.label_address3.AutoSize = true;
            this.label_address3.Location = new System.Drawing.Point(6, 373);
            this.label_address3.Name = "label_address3";
            this.label_address3.Size = new System.Drawing.Size(58, 15);
            this.label_address3.TabIndex = 1;
            this.label_address3.Text = "Address 3";
            // 
            // label_address2
            // 
            this.label_address2.AutoSize = true;
            this.label_address2.Location = new System.Drawing.Point(6, 344);
            this.label_address2.Name = "label_address2";
            this.label_address2.Size = new System.Drawing.Size(58, 15);
            this.label_address2.TabIndex = 1;
            this.label_address2.Text = "Address 2";
            // 
            // label_address
            // 
            this.label_address.AutoSize = true;
            this.label_address.Location = new System.Drawing.Point(6, 315);
            this.label_address.Name = "label_address";
            this.label_address.Size = new System.Drawing.Size(49, 15);
            this.label_address.TabIndex = 1;
            this.label_address.Text = "Address";
            // 
            // label_website
            // 
            this.label_website.AutoSize = true;
            this.label_website.Location = new System.Drawing.Point(6, 286);
            this.label_website.Name = "label_website";
            this.label_website.Size = new System.Drawing.Size(49, 15);
            this.label_website.TabIndex = 1;
            this.label_website.Text = "Website";
            // 
            // label_fax
            // 
            this.label_fax.AutoSize = true;
            this.label_fax.Location = new System.Drawing.Point(6, 228);
            this.label_fax.Name = "label_fax";
            this.label_fax.Size = new System.Drawing.Size(44, 15);
            this.label_fax.TabIndex = 1;
            this.label_fax.Text = "Fax No";
            // 
            // label_phone
            // 
            this.label_phone.AutoSize = true;
            this.label_phone.Location = new System.Drawing.Point(6, 199);
            this.label_phone.Name = "label_phone";
            this.label_phone.Size = new System.Drawing.Size(60, 15);
            this.label_phone.TabIndex = 1;
            this.label_phone.Text = "Phone No";
            // 
            // label_customerSince
            // 
            this.label_customerSince.AutoSize = true;
            this.label_customerSince.Location = new System.Drawing.Point(6, 141);
            this.label_customerSince.Name = "label_customerSince";
            this.label_customerSince.Size = new System.Drawing.Size(90, 15);
            this.label_customerSince.TabIndex = 1;
            this.label_customerSince.Text = "Customer Since";
            // 
            // label_nickname
            // 
            this.label_nickname.AutoSize = true;
            this.label_nickname.Location = new System.Drawing.Point(6, 54);
            this.label_nickname.Name = "label_nickname";
            this.label_nickname.Size = new System.Drawing.Size(61, 15);
            this.label_nickname.TabIndex = 1;
            this.label_nickname.Text = "Nickname";
            // 
            // label_customerNo
            // 
            this.label_customerNo.AutoSize = true;
            this.label_customerNo.Location = new System.Drawing.Point(6, 25);
            this.label_customerNo.Name = "label_customerNo";
            this.label_customerNo.Size = new System.Drawing.Size(78, 15);
            this.label_customerNo.TabIndex = 1;
            this.label_customerNo.Text = "Customer No";
            // 
            // groupBox_customerMandatoryFields
            // 
            this.groupBox_customerMandatoryFields.Controls.Add(this.checkBox_defaultCountryID);
            this.groupBox_customerMandatoryFields.Controls.Add(this.checkBox_defaultCustomerStatusID);
            this.groupBox_customerMandatoryFields.Controls.Add(this.checkBox_defaultCurrencyID);
            this.groupBox_customerMandatoryFields.Controls.Add(this.label_countryID);
            this.groupBox_customerMandatoryFields.Controls.Add(this.label_customerStatusID);
            this.groupBox_customerMandatoryFields.Controls.Add(this.comboBox_countryID);
            this.groupBox_customerMandatoryFields.Controls.Add(this.comboBox_customerStatusID);
            this.groupBox_customerMandatoryFields.Controls.Add(this.label_currencyID);
            this.groupBox_customerMandatoryFields.Controls.Add(this.comboBox_currencyID);
            this.groupBox_customerMandatoryFields.Controls.Add(this.label_customerName);
            this.groupBox_customerMandatoryFields.Controls.Add(this.comboBox_customerName);
            this.groupBox_customerMandatoryFields.Location = new System.Drawing.Point(8, 154);
            this.groupBox_customerMandatoryFields.Name = "groupBox_customerMandatoryFields";
            this.groupBox_customerMandatoryFields.Size = new System.Drawing.Size(304, 146);
            this.groupBox_customerMandatoryFields.TabIndex = 5;
            this.groupBox_customerMandatoryFields.TabStop = false;
            this.groupBox_customerMandatoryFields.Text = "Mandatory";
            // 
            // checkBox_defaultCountryID
            // 
            this.checkBox_defaultCountryID.AutoSize = true;
            this.checkBox_defaultCountryID.Location = new System.Drawing.Point(239, 112);
            this.checkBox_defaultCountryID.Name = "checkBox_defaultCountryID";
            this.checkBox_defaultCountryID.Size = new System.Drawing.Size(64, 19);
            this.checkBox_defaultCountryID.TabIndex = 8;
            this.checkBox_defaultCountryID.Text = "Default";
            this.checkBox_defaultCountryID.UseVisualStyleBackColor = true;
            this.checkBox_defaultCountryID.CheckedChanged += new System.EventHandler(this.checkBox_defaultCountryID_CheckedChanged);
            // 
            // checkBox_defaultCustomerStatusID
            // 
            this.checkBox_defaultCustomerStatusID.AutoSize = true;
            this.checkBox_defaultCustomerStatusID.Location = new System.Drawing.Point(239, 83);
            this.checkBox_defaultCustomerStatusID.Name = "checkBox_defaultCustomerStatusID";
            this.checkBox_defaultCustomerStatusID.Size = new System.Drawing.Size(64, 19);
            this.checkBox_defaultCustomerStatusID.TabIndex = 8;
            this.checkBox_defaultCustomerStatusID.Text = "Default";
            this.checkBox_defaultCustomerStatusID.UseVisualStyleBackColor = true;
            this.checkBox_defaultCustomerStatusID.CheckedChanged += new System.EventHandler(this.checkBox_defaultCustomerStatusID_CheckedChanged);
            // 
            // checkBox_defaultCurrencyID
            // 
            this.checkBox_defaultCurrencyID.AutoSize = true;
            this.checkBox_defaultCurrencyID.Location = new System.Drawing.Point(239, 54);
            this.checkBox_defaultCurrencyID.Name = "checkBox_defaultCurrencyID";
            this.checkBox_defaultCurrencyID.Size = new System.Drawing.Size(64, 19);
            this.checkBox_defaultCurrencyID.TabIndex = 8;
            this.checkBox_defaultCurrencyID.Text = "Default";
            this.checkBox_defaultCurrencyID.UseVisualStyleBackColor = true;
            this.checkBox_defaultCurrencyID.CheckedChanged += new System.EventHandler(this.checkBox_defaultCurrencyID_CheckedChanged);
            // 
            // label_countryID
            // 
            this.label_countryID.AutoSize = true;
            this.label_countryID.Location = new System.Drawing.Point(6, 112);
            this.label_countryID.Name = "label_countryID";
            this.label_countryID.Size = new System.Drawing.Size(64, 15);
            this.label_countryID.TabIndex = 5;
            this.label_countryID.Text = "Country ID";
            // 
            // label_customerStatusID
            // 
            this.label_customerStatusID.AutoSize = true;
            this.label_customerStatusID.Location = new System.Drawing.Point(6, 83);
            this.label_customerStatusID.Name = "label_customerStatusID";
            this.label_customerStatusID.Size = new System.Drawing.Size(108, 15);
            this.label_customerStatusID.TabIndex = 5;
            this.label_customerStatusID.Text = "Customer Status ID";
            // 
            // comboBox_countryID
            // 
            this.comboBox_countryID.FormattingEnabled = true;
            this.comboBox_countryID.Location = new System.Drawing.Point(120, 109);
            this.comboBox_countryID.Name = "comboBox_countryID";
            this.comboBox_countryID.Size = new System.Drawing.Size(113, 23);
            this.comboBox_countryID.TabIndex = 7;
            this.comboBox_countryID.SelectedIndexChanged += new System.EventHandler(this.comboBox_countryID_SelectedIndexChanged);
            // 
            // comboBox_customerStatusID
            // 
            this.comboBox_customerStatusID.FormattingEnabled = true;
            this.comboBox_customerStatusID.Location = new System.Drawing.Point(120, 80);
            this.comboBox_customerStatusID.Name = "comboBox_customerStatusID";
            this.comboBox_customerStatusID.Size = new System.Drawing.Size(113, 23);
            this.comboBox_customerStatusID.TabIndex = 6;
            this.comboBox_customerStatusID.SelectedIndexChanged += new System.EventHandler(this.comboBox_customerStatusID_SelectedIndexChanged);
            // 
            // label_currencyID
            // 
            this.label_currencyID.AutoSize = true;
            this.label_currencyID.Location = new System.Drawing.Point(6, 54);
            this.label_currencyID.Name = "label_currencyID";
            this.label_currencyID.Size = new System.Drawing.Size(69, 15);
            this.label_currencyID.TabIndex = 5;
            this.label_currencyID.Text = "Currency ID";
            // 
            // comboBox_currencyID
            // 
            this.comboBox_currencyID.FormattingEnabled = true;
            this.comboBox_currencyID.Location = new System.Drawing.Point(120, 51);
            this.comboBox_currencyID.Name = "comboBox_currencyID";
            this.comboBox_currencyID.Size = new System.Drawing.Size(113, 23);
            this.comboBox_currencyID.TabIndex = 4;
            this.comboBox_currencyID.SelectedIndexChanged += new System.EventHandler(this.comboBox_currencyID_SelectedIndexChanged);
            // 
            // label_customerName
            // 
            this.label_customerName.AutoSize = true;
            this.label_customerName.Location = new System.Drawing.Point(6, 25);
            this.label_customerName.Name = "label_customerName";
            this.label_customerName.Size = new System.Drawing.Size(94, 15);
            this.label_customerName.TabIndex = 1;
            this.label_customerName.Text = "Customer Name";
            // 
            // comboBox_customerName
            // 
            this.comboBox_customerName.FormattingEnabled = true;
            this.comboBox_customerName.Location = new System.Drawing.Point(120, 22);
            this.comboBox_customerName.Name = "comboBox_customerName";
            this.comboBox_customerName.Size = new System.Drawing.Size(113, 23);
            this.comboBox_customerName.TabIndex = 3;
            this.comboBox_customerName.SelectedIndexChanged += new System.EventHandler(this.comboBox_customerName_SelectedIndexChanged);
            // 
            // label_customerSetup
            // 
            this.label_customerSetup.AutoSize = true;
            this.label_customerSetup.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label_customerSetup.Location = new System.Drawing.Point(8, 16);
            this.label_customerSetup.Name = "label_customerSetup";
            this.label_customerSetup.Size = new System.Drawing.Size(251, 32);
            this.label_customerSetup.TabIndex = 0;
            this.label_customerSetup.Text = "Customer Data Import";
            // 
            // button_customerSelectFile
            // 
            this.button_customerSelectFile.Location = new System.Drawing.Point(13, 101);
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
            this.groupBox_customerNonMandatoryFields.ResumeLayout(false);
            this.groupBox_customerNonMandatoryFields.PerformLayout();
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
        private System.Windows.Forms.ComboBox comboBox_currencyID;
        private System.Windows.Forms.Label label_currencyID;
        private System.Windows.Forms.ComboBox comboBox_countryID;
        private System.Windows.Forms.ComboBox comboBox_customerStatusID;
        private System.Windows.Forms.Label label_invoiceAddressToUse;
        private System.Windows.Forms.Label label_invoicingAddressCountryID;
        private System.Windows.Forms.Label label_countryID;
        private System.Windows.Forms.Label label_customerStatusID;
        private System.Windows.Forms.Label label_eanNo;
        private System.Windows.Forms.Label label_secondaryKAMID;
        private System.Windows.Forms.Label label_primaryKAMID;
        private System.Windows.Forms.Label label_email;
        private System.Windows.Forms.Label label_industryID;
        private System.Windows.Forms.Label label_defaultMileageDistance;
        private System.Windows.Forms.GroupBox groupBox_customerNonMandatoryFields;
        private System.Windows.Forms.Label label_customerNo;
        private System.Windows.Forms.Label label_useEanNo;
        private System.Windows.Forms.Label label_vatPercentage;
        private System.Windows.Forms.Label label_calculateVAT;
        private System.Windows.Forms.Label label_discountPercentage;
        private System.Windows.Forms.Label label_paymentTermID;
        private System.Windows.Forms.Label label_customerReferenceID;
        private System.Windows.Forms.Label label_internalReferenceID;
        private System.Windows.Forms.Label label_contactID;
        private System.Windows.Forms.Label label_defaultDistIsMaxBillable;
        private System.Windows.Forms.Label label_organizationNo;
        private System.Windows.Forms.Label label_vatNo;
        private System.Windows.Forms.Label label_invoicingAddressState;
        private System.Windows.Forms.Label label_expenseIsBillable;
        private System.Windows.Forms.Label label_mileageIsBillable;
        private System.Windows.Forms.Label label_invoicingAddress3;
        private System.Windows.Forms.Label label_invoicingAddress2;
        private System.Windows.Forms.Label label_invoicingAddressZipCode;
        private System.Windows.Forms.Label label_invoicingAddressCity;
        private System.Windows.Forms.Label label_invoicingAddress;
        private System.Windows.Forms.Label label_useInvoicingAddress;
        private System.Windows.Forms.Label label_state;
        private System.Windows.Forms.Label label_city;
        private System.Windows.Forms.Label label_zipCode;
        private System.Windows.Forms.Label label_address3;
        private System.Windows.Forms.Label label_address2;
        private System.Windows.Forms.Label label_address;
        private System.Windows.Forms.Label label_website;
        private System.Windows.Forms.Label label_fax;
        private System.Windows.Forms.Label label_phone;
        private System.Windows.Forms.Label label_customerSince;
        private System.Windows.Forms.Label label_nickname;
        private System.Windows.Forms.ComboBox comboBox_customerNo;
        private System.Windows.Forms.ComboBox comboBox_secondaryKAMID;
        private System.Windows.Forms.CheckBox checkBox_defaultCurrencyID;
        private System.Windows.Forms.CheckBox checkBox_defaultPrimaryKAMID;
        private System.Windows.Forms.ComboBox comboBox_useInvoicingAddress;
        private System.Windows.Forms.ComboBox comboBox_organizationNo;
        private System.Windows.Forms.ComboBox comboBox_eanNo;
        private System.Windows.Forms.ComboBox comboBox_useEanNo;
        private System.Windows.Forms.ComboBox comboBox_state;
        private System.Windows.Forms.ComboBox comboBox_city;
        private System.Windows.Forms.ComboBox comboBox_zipCode;
        private System.Windows.Forms.ComboBox comboBox_address3;
        private System.Windows.Forms.ComboBox comboBox_address2;
        private System.Windows.Forms.ComboBox comboBox_address;
        private System.Windows.Forms.ComboBox comboBox_website;
        private System.Windows.Forms.ComboBox comboBox_email;
        private System.Windows.Forms.ComboBox comboBox_faxNo;
        private System.Windows.Forms.ComboBox comboBox_phoneNo;
        private System.Windows.Forms.ComboBox comboBox_industryID;
        private System.Windows.Forms.ComboBox comboBox_customerSince;
        private System.Windows.Forms.ComboBox comboBox_primaryKAMID;
        private System.Windows.Forms.ComboBox comboBox_nickName;
        private System.Windows.Forms.ComboBox comboBox_VATNo;
        private System.Windows.Forms.ComboBox comboBox_VATPercentage;
        private System.Windows.Forms.ComboBox comboBox_calculateVAT;
        private System.Windows.Forms.ComboBox comboBox_discountPercentage;
        private System.Windows.Forms.ComboBox comboBox_paymentTermID;
        private System.Windows.Forms.ComboBox comboBox_customerReferenceID;
        private System.Windows.Forms.ComboBox comboBox_internalReferenceID;
        private System.Windows.Forms.ComboBox comboBox_invoiceAddressToUse;
        private System.Windows.Forms.ComboBox comboBox_contactID;
        private System.Windows.Forms.ComboBox comboBox_defaultDistIsMaxBillable;
        private System.Windows.Forms.ComboBox comboBox_mileageIsBillable;
        private System.Windows.Forms.ComboBox comboBox_expenseIsBillable;
        private System.Windows.Forms.ComboBox comboBox_defaultMileageDistance;
        private System.Windows.Forms.ComboBox comboBox_invoicingAddressCountryID;
        private System.Windows.Forms.ComboBox comboBox_invoicingAddressState;
        private System.Windows.Forms.ComboBox comboBox_invoicingAddressCity;
        private System.Windows.Forms.ComboBox comboBox_invoicingAddressZipCode;
        private System.Windows.Forms.ComboBox comboBox_invoicingAddress3;
        private System.Windows.Forms.ComboBox comboBox_invoicingAddress2;
        private System.Windows.Forms.ComboBox comboBox_invoicingAddress;
        private System.Windows.Forms.CheckBox checkBox_defaultCountryID;
        private System.Windows.Forms.CheckBox checkBox_defaultCustomerStatusID;
        private System.Windows.Forms.CheckBox checkBox_defaultVATPercentage;
        private System.Windows.Forms.CheckBox checkBox_defaultMileageIsBillable;
        private System.Windows.Forms.CheckBox checkBox_defaultExpenseIsBillable;
        private System.Windows.Forms.CheckBox checkBox_defaultPaymentTermID;
        private System.Windows.Forms.CheckBox checkBox_defaultIndustryID;
        private System.Windows.Forms.CheckBox checkBox_defaultSecondaryKAMID;
        private System.Windows.Forms.Label label_delimiter;
        private System.Windows.Forms.ComboBox comboBox_delimiter;
    }
}
