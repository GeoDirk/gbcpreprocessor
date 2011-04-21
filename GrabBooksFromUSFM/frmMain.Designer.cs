namespace GBC_USFM_Preprocessor
{
    partial class frmMain
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.cmdClose = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cboEncoding = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmdConvertToUTF_WithoutBOM = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtOutFileExtension = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cmdCopyToClipboard = new System.Windows.Forms.Button();
            this.txtOutput = new System.Windows.Forms.RichTextBox();
            this.lvOutput = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cmdProcess = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.txtBookNameTag = new System.Windows.Forms.TextBox();
            this.radioDropComments = new System.Windows.Forms.RadioButton();
            this.radioKeepComments = new System.Windows.Forms.RadioButton();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.lblDisplayTag = new System.Windows.Forms.Label();
            this.txtDisplayTag = new System.Windows.Forms.TextBox();
            this.lblOutputExtension = new System.Windows.Forms.Label();
            this.txtOutputTagExtension = new System.Windows.Forms.TextBox();
            this.btnProcessExtraTags = new System.Windows.Forms.Button();
            this.gridExtraTags = new System.Windows.Forms.DataGridView();
            this.cmdScanTags = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.cmdVersificationFind = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.updownLongWords = new System.Windows.Forms.NumericUpDown();
            this.rbWordsAndHyphens = new System.Windows.Forms.RadioButton();
            this.cmdFindAllLongWords = new System.Windows.Forms.Button();
            this.rbWordsAndPunctuation = new System.Windows.Forms.RadioButton();
            this.rbWordsOnly = new System.Windows.Forms.RadioButton();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.chkCharacterReplacement = new System.Windows.Forms.CheckBox();
            this.panel7 = new System.Windows.Forms.Panel();
            this.lblExportTo = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.cmdGenerateBQExport = new System.Windows.Forms.Button();
            this.label19 = new System.Windows.Forms.Label();
            this.txtBQ_BooknameTag = new System.Windows.Forms.TextBox();
            this.txtBQVerseSign = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.txtBQChapterSign = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.cboBQFontCharSet = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.txtBQAlphabet = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.rbBQGreek_N = new System.Windows.Forms.RadioButton();
            this.rbBQGreek_Y = new System.Windows.Forms.RadioButton();
            this.label13 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.rbBQApocrypha_N = new System.Windows.Forms.RadioButton();
            this.rbBQApocrypha_Y = new System.Windows.Forms.RadioButton();
            this.panel4 = new System.Windows.Forms.Panel();
            this.rbBQ_NT_N = new System.Windows.Forms.RadioButton();
            this.rbBQ_NT_Y = new System.Windows.Forms.RadioButton();
            this.panel5 = new System.Windows.Forms.Panel();
            this.rbBQ_OT_N = new System.Windows.Forms.RadioButton();
            this.rbBQ_OT_Y = new System.Windows.Forms.RadioButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.rbBQIsBible_N = new System.Windows.Forms.RadioButton();
            this.rbBQIsBible_Y = new System.Windows.Forms.RadioButton();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbR2L = new System.Windows.Forms.RadioButton();
            this.rbL2R = new System.Windows.Forms.RadioButton();
            this.txtBQCopyright = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtBQBibleNameShort = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtBQBibleNameFull = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cboBQCodePage = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cboExt = new System.Windows.Forms.ComboBox();
            this.txtDir = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmdGetDir = new System.Windows.Forms.Button();
            this.ttGrid = new System.Windows.Forms.ToolTip(this.components);
            this.lblVersion = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.cmdAbout = new System.Windows.Forms.Button();
            this.ckAddBOM = new System.Windows.Forms.CheckBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridExtraTags)).BeginInit();
            this.tabPage3.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.updownLongWords)).BeginInit();
            this.tabPage4.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdClose
            // 
            this.cmdClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdClose.Location = new System.Drawing.Point(817, 663);
            this.cmdClose.Margin = new System.Windows.Forms.Padding(4);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(100, 28);
            this.cmdClose.TabIndex = 1;
            this.cmdClose.Text = "Close";
            this.cmdClose.UseVisualStyleBackColor = true;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Location = new System.Drawing.Point(0, 91);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(920, 573);
            this.tabControl1.TabIndex = 20;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage1.Size = new System.Drawing.Size(912, 544);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Standard Utilities";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.ckAddBOM);
            this.groupBox3.Controls.Add(this.cboEncoding);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.cmdConvertToUTF_WithoutBOM);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.txtOutFileExtension);
            this.groupBox3.Location = new System.Drawing.Point(4, 7);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox3.Size = new System.Drawing.Size(902, 76);
            this.groupBox3.TabIndex = 21;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Convert Files from ANSI Code Page to UTF-8";
            // 
            // cboEncoding
            // 
            this.cboEncoding.FormattingEnabled = true;
            this.cboEncoding.Location = new System.Drawing.Point(111, 43);
            this.cboEncoding.Margin = new System.Windows.Forms.Padding(4);
            this.cboEncoding.Name = "cboEncoding";
            this.cboEncoding.Size = new System.Drawing.Size(493, 24);
            this.cboEncoding.TabIndex = 8;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(24, 20);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(185, 17);
            this.label6.TabIndex = 17;
            this.label6.Text = "UTF8 Output File Extension:";
            // 
            // cmdConvertToUTF_WithoutBOM
            // 
            this.cmdConvertToUTF_WithoutBOM.Location = new System.Drawing.Point(621, 10);
            this.cmdConvertToUTF_WithoutBOM.Margin = new System.Windows.Forms.Padding(4);
            this.cmdConvertToUTF_WithoutBOM.Name = "cmdConvertToUTF_WithoutBOM";
            this.cmdConvertToUTF_WithoutBOM.Size = new System.Drawing.Size(247, 28);
            this.cmdConvertToUTF_WithoutBOM.TabIndex = 13;
            this.cmdConvertToUTF_WithoutBOM.Text = "Convert Files To UTF-8 (no BOM)";
            this.cmdConvertToUTF_WithoutBOM.UseVisualStyleBackColor = true;
            this.cmdConvertToUTF_WithoutBOM.Click += new System.EventHandler(this.cmdConvertToUTF_WithoutBOM_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(24, 48);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 17);
            this.label4.TabIndex = 9;
            this.label4.Text = "Codepage:";
            // 
            // txtOutFileExtension
            // 
            this.txtOutFileExtension.Location = new System.Drawing.Point(220, 16);
            this.txtOutFileExtension.Margin = new System.Windows.Forms.Padding(4);
            this.txtOutFileExtension.Name = "txtOutFileExtension";
            this.txtOutFileExtension.Size = new System.Drawing.Size(64, 22);
            this.txtOutFileExtension.TabIndex = 16;
            this.txtOutFileExtension.Text = "utf";
            this.txtOutFileExtension.TextChanged += new System.EventHandler(this.txtOutFileExtension_TextChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.cmdCopyToClipboard);
            this.groupBox2.Controls.Add(this.txtOutput);
            this.groupBox2.Controls.Add(this.lvOutput);
            this.groupBox2.Controls.Add(this.cmdProcess);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtBookNameTag);
            this.groupBox2.Controls.Add(this.radioDropComments);
            this.groupBox2.Controls.Add(this.radioKeepComments);
            this.groupBox2.Location = new System.Drawing.Point(4, 91);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(902, 447);
            this.groupBox2.TabIndex = 22;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Grab Booknames for Collections File (source files should be in UTF-8 format and m" +
                "ake sure that you have the correct extension above)";
            // 
            // cmdCopyToClipboard
            // 
            this.cmdCopyToClipboard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdCopyToClipboard.Location = new System.Drawing.Point(13, 410);
            this.cmdCopyToClipboard.Margin = new System.Windows.Forms.Padding(4);
            this.cmdCopyToClipboard.Name = "cmdCopyToClipboard";
            this.cmdCopyToClipboard.Size = new System.Drawing.Size(171, 28);
            this.cmdCopyToClipboard.TabIndex = 20;
            this.cmdCopyToClipboard.Text = "Copy to Clipboard";
            this.cmdCopyToClipboard.UseVisualStyleBackColor = true;
            this.cmdCopyToClipboard.Click += new System.EventHandler(this.cmdCopyToClipboard_Click);
            // 
            // txtOutput
            // 
            this.txtOutput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.txtOutput.Location = new System.Drawing.Point(8, 23);
            this.txtOutput.Margin = new System.Windows.Forms.Padding(4);
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.Size = new System.Drawing.Size(360, 380);
            this.txtOutput.TabIndex = 11;
            this.txtOutput.Text = "";
            // 
            // lvOutput
            // 
            this.lvOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lvOutput.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.lvOutput.Location = new System.Drawing.Point(377, 23);
            this.lvOutput.Margin = new System.Windows.Forms.Padding(4);
            this.lvOutput.Name = "lvOutput";
            this.lvOutput.Size = new System.Drawing.Size(508, 380);
            this.lvOutput.TabIndex = 12;
            this.lvOutput.UseCompatibleStateImageBehavior = false;
            this.lvOutput.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Title";
            this.columnHeader1.Width = 190;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Filename";
            this.columnHeader2.Width = 166;
            // 
            // cmdProcess
            // 
            this.cmdProcess.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdProcess.Location = new System.Drawing.Point(706, 411);
            this.cmdProcess.Margin = new System.Windows.Forms.Padding(4);
            this.cmdProcess.Name = "cmdProcess";
            this.cmdProcess.Size = new System.Drawing.Size(181, 28);
            this.cmdProcess.TabIndex = 0;
            this.cmdProcess.Text = "Grab Book Names";
            this.cmdProcess.UseVisualStyleBackColor = true;
            this.cmdProcess.Click += new System.EventHandler(this.cmdProcess_Click);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(526, 418);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(108, 17);
            this.label5.TabIndex = 15;
            this.label5.Text = "Bookname Tag:";
            // 
            // txtBookNameTag
            // 
            this.txtBookNameTag.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBookNameTag.Location = new System.Drawing.Point(644, 414);
            this.txtBookNameTag.Margin = new System.Windows.Forms.Padding(4);
            this.txtBookNameTag.Name = "txtBookNameTag";
            this.txtBookNameTag.Size = new System.Drawing.Size(52, 22);
            this.txtBookNameTag.TabIndex = 14;
            this.txtBookNameTag.Text = "\\h";
            // 
            // radioDropComments
            // 
            this.radioDropComments.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.radioDropComments.AutoSize = true;
            this.radioDropComments.Location = new System.Drawing.Point(192, 424);
            this.radioDropComments.Margin = new System.Windows.Forms.Padding(4);
            this.radioDropComments.Name = "radioDropComments";
            this.radioDropComments.Size = new System.Drawing.Size(117, 21);
            this.radioDropComments.TabIndex = 22;
            this.radioDropComments.Text = "No Comments";
            this.radioDropComments.UseVisualStyleBackColor = true;
            this.radioDropComments.CheckedChanged += new System.EventHandler(this.radioDropComments_CheckedChanged);
            // 
            // radioKeepComments
            // 
            this.radioKeepComments.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.radioKeepComments.AutoSize = true;
            this.radioKeepComments.Checked = true;
            this.radioKeepComments.Location = new System.Drawing.Point(192, 405);
            this.radioKeepComments.Margin = new System.Windows.Forms.Padding(4);
            this.radioKeepComments.Name = "radioKeepComments";
            this.radioKeepComments.Size = new System.Drawing.Size(121, 21);
            this.radioKeepComments.TabIndex = 21;
            this.radioKeepComments.TabStop = true;
            this.radioKeepComments.Text = "Full Comments";
            this.radioKeepComments.UseVisualStyleBackColor = true;
            this.radioKeepComments.CheckedChanged += new System.EventHandler(this.radioKeepComments_CheckedChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.lblDisplayTag);
            this.tabPage2.Controls.Add(this.txtDisplayTag);
            this.tabPage2.Controls.Add(this.lblOutputExtension);
            this.tabPage2.Controls.Add(this.txtOutputTagExtension);
            this.tabPage2.Controls.Add(this.btnProcessExtraTags);
            this.tabPage2.Controls.Add(this.gridExtraTags);
            this.tabPage2.Controls.Add(this.cmdScanTags);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage2.Size = new System.Drawing.Size(912, 544);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Extra Tags Processor";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // lblDisplayTag
            // 
            this.lblDisplayTag.AutoSize = true;
            this.lblDisplayTag.Location = new System.Drawing.Point(739, 43);
            this.lblDisplayTag.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDisplayTag.Name = "lblDisplayTag";
            this.lblDisplayTag.Size = new System.Drawing.Size(41, 17);
            this.lblDisplayTag.TabIndex = 21;
            this.lblDisplayTag.Text = "Tag: ";
            // 
            // txtDisplayTag
            // 
            this.txtDisplayTag.Location = new System.Drawing.Point(739, 71);
            this.txtDisplayTag.Margin = new System.Windows.Forms.Padding(4);
            this.txtDisplayTag.Multiline = true;
            this.txtDisplayTag.Name = "txtDisplayTag";
            this.txtDisplayTag.Size = new System.Drawing.Size(171, 421);
            this.txtDisplayTag.TabIndex = 20;
            // 
            // lblOutputExtension
            // 
            this.lblOutputExtension.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblOutputExtension.AutoSize = true;
            this.lblOutputExtension.Enabled = false;
            this.lblOutputExtension.Location = new System.Drawing.Point(135, 502);
            this.lblOutputExtension.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblOutputExtension.Name = "lblOutputExtension";
            this.lblOutputExtension.Size = new System.Drawing.Size(146, 17);
            this.lblOutputExtension.TabIndex = 19;
            this.lblOutputExtension.Text = "Output File Extension:";
            // 
            // txtOutputTagExtension
            // 
            this.txtOutputTagExtension.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtOutputTagExtension.Enabled = false;
            this.txtOutputTagExtension.Location = new System.Drawing.Point(283, 498);
            this.txtOutputTagExtension.Margin = new System.Windows.Forms.Padding(4);
            this.txtOutputTagExtension.Name = "txtOutputTagExtension";
            this.txtOutputTagExtension.Size = new System.Drawing.Size(64, 22);
            this.txtOutputTagExtension.TabIndex = 18;
            this.txtOutputTagExtension.Text = "utf";
            this.txtOutputTagExtension.TextChanged += new System.EventHandler(this.txtOutputTagExtension_TextChanged);
            // 
            // btnProcessExtraTags
            // 
            this.btnProcessExtraTags.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnProcessExtraTags.Enabled = false;
            this.btnProcessExtraTags.Location = new System.Drawing.Point(359, 496);
            this.btnProcessExtraTags.Margin = new System.Windows.Forms.Padding(4);
            this.btnProcessExtraTags.Name = "btnProcessExtraTags";
            this.btnProcessExtraTags.Size = new System.Drawing.Size(265, 28);
            this.btnProcessExtraTags.TabIndex = 2;
            this.btnProcessExtraTags.Text = "Process Tags and Create New Files";
            this.btnProcessExtraTags.UseVisualStyleBackColor = true;
            this.btnProcessExtraTags.Click += new System.EventHandler(this.btnProcessExtraTags_Click);
            // 
            // gridExtraTags
            // 
            this.gridExtraTags.AllowUserToAddRows = false;
            this.gridExtraTags.AllowUserToDeleteRows = false;
            this.gridExtraTags.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gridExtraTags.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridExtraTags.Location = new System.Drawing.Point(13, 43);
            this.gridExtraTags.Margin = new System.Windows.Forms.Padding(4);
            this.gridExtraTags.Name = "gridExtraTags";
            this.gridExtraTags.ShowCellToolTips = false;
            this.gridExtraTags.Size = new System.Drawing.Size(717, 450);
            this.gridExtraTags.TabIndex = 1;
            this.gridExtraTags.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridExtraTags_CellMouseEnter);
            // 
            // cmdScanTags
            // 
            this.cmdScanTags.Location = new System.Drawing.Point(8, 7);
            this.cmdScanTags.Margin = new System.Windows.Forms.Padding(4);
            this.cmdScanTags.Name = "cmdScanTags";
            this.cmdScanTags.Size = new System.Drawing.Size(468, 28);
            this.cmdScanTags.TabIndex = 0;
            this.cmdScanTags.Text = "Scan for Tags that will not be Processed by GBC (UTF-8 Files Only)";
            this.cmdScanTags.UseVisualStyleBackColor = true;
            this.cmdScanTags.Click += new System.EventHandler(this.cmdScanTags_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.groupBox5);
            this.tabPage3.Controls.Add(this.groupBox4);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage3.Size = new System.Drawing.Size(912, 544);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Check For Consistency";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.cmdVersificationFind);
            this.groupBox5.Location = new System.Drawing.Point(376, 11);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox5.Size = new System.Drawing.Size(372, 132);
            this.groupBox5.TabIndex = 6;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Look for Versification Issues";
            // 
            // cmdVersificationFind
            // 
            this.cmdVersificationFind.Location = new System.Drawing.Point(53, 23);
            this.cmdVersificationFind.Margin = new System.Windows.Forms.Padding(4);
            this.cmdVersificationFind.Name = "cmdVersificationFind";
            this.cmdVersificationFind.Size = new System.Drawing.Size(248, 28);
            this.cmdVersificationFind.TabIndex = 0;
            this.cmdVersificationFind.Text = "Search for Versification";
            this.cmdVersificationFind.UseVisualStyleBackColor = true;
            this.cmdVersificationFind.Click += new System.EventHandler(this.cmdVersificationFind_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.updownLongWords);
            this.groupBox4.Controls.Add(this.rbWordsAndHyphens);
            this.groupBox4.Controls.Add(this.cmdFindAllLongWords);
            this.groupBox4.Controls.Add(this.rbWordsAndPunctuation);
            this.groupBox4.Controls.Add(this.rbWordsOnly);
            this.groupBox4.Location = new System.Drawing.Point(11, 7);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox4.Size = new System.Drawing.Size(357, 135);
            this.groupBox4.TabIndex = 5;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Check for Long Words";
            // 
            // updownLongWords
            // 
            this.updownLongWords.Location = new System.Drawing.Point(8, 23);
            this.updownLongWords.Margin = new System.Windows.Forms.Padding(4);
            this.updownLongWords.Name = "updownLongWords";
            this.updownLongWords.Size = new System.Drawing.Size(63, 22);
            this.updownLongWords.TabIndex = 1;
            this.updownLongWords.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            // 
            // rbWordsAndHyphens
            // 
            this.rbWordsAndHyphens.AutoSize = true;
            this.rbWordsAndHyphens.Location = new System.Drawing.Point(8, 81);
            this.rbWordsAndHyphens.Margin = new System.Windows.Forms.Padding(4);
            this.rbWordsAndHyphens.Name = "rbWordsAndHyphens";
            this.rbWordsAndHyphens.Size = new System.Drawing.Size(186, 21);
            this.rbWordsAndHyphens.TabIndex = 4;
            this.rbWordsAndHyphens.Text = "Words only with hyphens";
            this.rbWordsAndHyphens.UseVisualStyleBackColor = true;
            // 
            // cmdFindAllLongWords
            // 
            this.cmdFindAllLongWords.Location = new System.Drawing.Point(79, 20);
            this.cmdFindAllLongWords.Margin = new System.Windows.Forms.Padding(4);
            this.cmdFindAllLongWords.Name = "cmdFindAllLongWords";
            this.cmdFindAllLongWords.Size = new System.Drawing.Size(211, 28);
            this.cmdFindAllLongWords.TabIndex = 0;
            this.cmdFindAllLongWords.Text = "Find All Long Words";
            this.cmdFindAllLongWords.UseVisualStyleBackColor = true;
            this.cmdFindAllLongWords.Click += new System.EventHandler(this.cmdFindAllLongWords_Click);
            // 
            // rbWordsAndPunctuation
            // 
            this.rbWordsAndPunctuation.AutoSize = true;
            this.rbWordsAndPunctuation.Location = new System.Drawing.Point(8, 106);
            this.rbWordsAndPunctuation.Margin = new System.Windows.Forms.Padding(4);
            this.rbWordsAndPunctuation.Name = "rbWordsAndPunctuation";
            this.rbWordsAndPunctuation.Size = new System.Drawing.Size(321, 21);
            this.rbWordsAndPunctuation.TabIndex = 3;
            this.rbWordsAndPunctuation.Text = "Words with hyphens and attached punctuation";
            this.rbWordsAndPunctuation.UseVisualStyleBackColor = true;
            // 
            // rbWordsOnly
            // 
            this.rbWordsOnly.AutoSize = true;
            this.rbWordsOnly.Checked = true;
            this.rbWordsOnly.Location = new System.Drawing.Point(8, 55);
            this.rbWordsOnly.Margin = new System.Windows.Forms.Padding(4);
            this.rbWordsOnly.Name = "rbWordsOnly";
            this.rbWordsOnly.Size = new System.Drawing.Size(282, 21);
            this.rbWordsOnly.TabIndex = 2;
            this.rbWordsOnly.TabStop = true;
            this.rbWordsOnly.Text = "Words only - no punctuation or hyphens";
            this.rbWordsOnly.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.chkCharacterReplacement);
            this.tabPage4.Controls.Add(this.panel7);
            this.tabPage4.Controls.Add(this.label19);
            this.tabPage4.Controls.Add(this.txtBQ_BooknameTag);
            this.tabPage4.Controls.Add(this.txtBQVerseSign);
            this.tabPage4.Controls.Add(this.label18);
            this.tabPage4.Controls.Add(this.txtBQChapterSign);
            this.tabPage4.Controls.Add(this.label17);
            this.tabPage4.Controls.Add(this.cboBQFontCharSet);
            this.tabPage4.Controls.Add(this.label16);
            this.tabPage4.Controls.Add(this.txtBQAlphabet);
            this.tabPage4.Controls.Add(this.label15);
            this.tabPage4.Controls.Add(this.label14);
            this.tabPage4.Controls.Add(this.panel6);
            this.tabPage4.Controls.Add(this.label13);
            this.tabPage4.Controls.Add(this.panel3);
            this.tabPage4.Controls.Add(this.panel4);
            this.tabPage4.Controls.Add(this.panel5);
            this.tabPage4.Controls.Add(this.panel2);
            this.tabPage4.Controls.Add(this.label10);
            this.tabPage4.Controls.Add(this.label11);
            this.tabPage4.Controls.Add(this.label12);
            this.tabPage4.Controls.Add(this.panel1);
            this.tabPage4.Controls.Add(this.txtBQCopyright);
            this.tabPage4.Controls.Add(this.label9);
            this.tabPage4.Controls.Add(this.txtBQBibleNameShort);
            this.tabPage4.Controls.Add(this.label8);
            this.tabPage4.Controls.Add(this.txtBQBibleNameFull);
            this.tabPage4.Controls.Add(this.label7);
            this.tabPage4.Controls.Add(this.label2);
            this.tabPage4.Controls.Add(this.cboBQCodePage);
            this.tabPage4.Location = new System.Drawing.Point(4, 25);
            this.tabPage4.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage4.Size = new System.Drawing.Size(912, 544);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Export to BQ/DigiStudy Format";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // chkCharacterReplacement
            // 
            this.chkCharacterReplacement.AutoSize = true;
            this.chkCharacterReplacement.Location = new System.Drawing.Point(17, 412);
            this.chkCharacterReplacement.Margin = new System.Windows.Forms.Padding(4);
            this.chkCharacterReplacement.Name = "chkCharacterReplacement";
            this.chkCharacterReplacement.Size = new System.Drawing.Size(292, 21);
            this.chkCharacterReplacement.TabIndex = 32;
            this.chkCharacterReplacement.Text = "Swap Characters for HTML friendly codes";
            this.chkCharacterReplacement.UseVisualStyleBackColor = true;
            this.chkCharacterReplacement.CheckedChanged += new System.EventHandler(this.chkCharacterReplacement_CheckedChanged);
            // 
            // panel7
            // 
            this.panel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel7.Controls.Add(this.lblExportTo);
            this.panel7.Controls.Add(this.progressBar1);
            this.panel7.Controls.Add(this.cmdGenerateBQExport);
            this.panel7.Location = new System.Drawing.Point(8, 441);
            this.panel7.Margin = new System.Windows.Forms.Padding(4);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(899, 86);
            this.panel7.TabIndex = 22;
            // 
            // lblExportTo
            // 
            this.lblExportTo.AutoSize = true;
            this.lblExportTo.Location = new System.Drawing.Point(8, 58);
            this.lblExportTo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblExportTo.Name = "lblExportTo";
            this.lblExportTo.Size = new System.Drawing.Size(92, 17);
            this.lblExportTo.TabIndex = 33;
            this.lblExportTo.Text = "Exporting To:";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(5, 36);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(4);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(884, 18);
            this.progressBar1.TabIndex = 32;
            this.progressBar1.Value = 50;
            // 
            // cmdGenerateBQExport
            // 
            this.cmdGenerateBQExport.Location = new System.Drawing.Point(327, 4);
            this.cmdGenerateBQExport.Margin = new System.Windows.Forms.Padding(4);
            this.cmdGenerateBQExport.Name = "cmdGenerateBQExport";
            this.cmdGenerateBQExport.Size = new System.Drawing.Size(232, 28);
            this.cmdGenerateBQExport.TabIndex = 29;
            this.cmdGenerateBQExport.Text = "Generate BQ Export";
            this.cmdGenerateBQExport.UseVisualStyleBackColor = true;
            this.cmdGenerateBQExport.Click += new System.EventHandler(this.cmdGenerateBQExport_Click);
            // 
            // label19
            // 
            this.label19.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(679, 395);
            this.label19.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(150, 17);
            this.label19.TabIndex = 31;
            this.label19.Text = "USFM Bookname Tag:";
            // 
            // txtBQ_BooknameTag
            // 
            this.txtBQ_BooknameTag.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBQ_BooknameTag.Location = new System.Drawing.Point(841, 391);
            this.txtBQ_BooknameTag.Margin = new System.Windows.Forms.Padding(4);
            this.txtBQ_BooknameTag.Name = "txtBQ_BooknameTag";
            this.txtBQ_BooknameTag.Size = new System.Drawing.Size(52, 22);
            this.txtBQ_BooknameTag.TabIndex = 30;
            this.txtBQ_BooknameTag.Text = "\\h";
            // 
            // txtBQVerseSign
            // 
            this.txtBQVerseSign.Location = new System.Drawing.Point(461, 362);
            this.txtBQVerseSign.Margin = new System.Windows.Forms.Padding(4);
            this.txtBQVerseSign.Name = "txtBQVerseSign";
            this.txtBQVerseSign.Size = new System.Drawing.Size(132, 22);
            this.txtBQVerseSign.TabIndex = 28;
            this.txtBQVerseSign.Text = "<vs>";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(332, 366);
            this.label18.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(81, 17);
            this.label18.TabIndex = 27;
            this.label18.Text = "Verse Sign:";
            // 
            // txtBQChapterSign
            // 
            this.txtBQChapterSign.Location = new System.Drawing.Point(165, 362);
            this.txtBQChapterSign.Margin = new System.Windows.Forms.Padding(4);
            this.txtBQChapterSign.Name = "txtBQChapterSign";
            this.txtBQChapterSign.Size = new System.Drawing.Size(132, 22);
            this.txtBQChapterSign.TabIndex = 26;
            this.txtBQChapterSign.Text = "<h4>";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(41, 366);
            this.label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(90, 17);
            this.label17.TabIndex = 25;
            this.label17.Text = "ChapterSign:";
            // 
            // cboBQFontCharSet
            // 
            this.cboBQFontCharSet.FormattingEnabled = true;
            this.cboBQFontCharSet.Location = new System.Drawing.Point(165, 329);
            this.cboBQFontCharSet.Margin = new System.Windows.Forms.Padding(4);
            this.cboBQFontCharSet.Name = "cboBQFontCharSet";
            this.cboBQFontCharSet.Size = new System.Drawing.Size(512, 24);
            this.cboBQFontCharSet.TabIndex = 24;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(17, 332);
            this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(148, 17);
            this.label16.TabIndex = 23;
            this.label16.Text = "Desired Font CharSet:";
            // 
            // txtBQAlphabet
            // 
            this.txtBQAlphabet.Location = new System.Drawing.Point(165, 263);
            this.txtBQAlphabet.Margin = new System.Windows.Forms.Padding(4);
            this.txtBQAlphabet.Name = "txtBQAlphabet";
            this.txtBQAlphabet.Size = new System.Drawing.Size(648, 22);
            this.txtBQAlphabet.TabIndex = 22;
            this.txtBQAlphabet.Text = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(17, 272);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(68, 17);
            this.label15.TabIndex = 21;
            this.label15.Text = "Alphabet:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(444, 197);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(59, 17);
            this.label14.TabIndex = 20;
            this.label14.Text = "Greek?:";
            // 
            // panel6
            // 
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel6.Controls.Add(this.rbBQGreek_N);
            this.panel6.Controls.Add(this.rbBQGreek_Y);
            this.panel6.Location = new System.Drawing.Point(592, 191);
            this.panel6.Margin = new System.Windows.Forms.Padding(4);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(158, 30);
            this.panel6.TabIndex = 19;
            // 
            // rbBQGreek_N
            // 
            this.rbBQGreek_N.AutoSize = true;
            this.rbBQGreek_N.Checked = true;
            this.rbBQGreek_N.Location = new System.Drawing.Point(93, 4);
            this.rbBQGreek_N.Margin = new System.Windows.Forms.Padding(4);
            this.rbBQGreek_N.Name = "rbBQGreek_N";
            this.rbBQGreek_N.Size = new System.Drawing.Size(47, 21);
            this.rbBQGreek_N.TabIndex = 1;
            this.rbBQGreek_N.TabStop = true;
            this.rbBQGreek_N.Text = "No";
            this.rbBQGreek_N.UseVisualStyleBackColor = true;
            // 
            // rbBQGreek_Y
            // 
            this.rbBQGreek_Y.AutoSize = true;
            this.rbBQGreek_Y.Location = new System.Drawing.Point(4, 4);
            this.rbBQGreek_Y.Margin = new System.Windows.Forms.Padding(4);
            this.rbBQGreek_Y.Name = "rbBQGreek_Y";
            this.rbBQGreek_Y.Size = new System.Drawing.Size(53, 21);
            this.rbBQGreek_Y.TabIndex = 0;
            this.rbBQGreek_Y.Text = "Yes";
            this.rbBQGreek_Y.UseVisualStyleBackColor = true;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(444, 159);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(88, 17);
            this.label13.TabIndex = 18;
            this.label13.Text = "Apocrypha?:";
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.rbBQApocrypha_N);
            this.panel3.Controls.Add(this.rbBQApocrypha_Y);
            this.panel3.Location = new System.Drawing.Point(592, 153);
            this.panel3.Margin = new System.Windows.Forms.Padding(4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(158, 30);
            this.panel3.TabIndex = 17;
            // 
            // rbBQApocrypha_N
            // 
            this.rbBQApocrypha_N.AutoSize = true;
            this.rbBQApocrypha_N.Checked = true;
            this.rbBQApocrypha_N.Location = new System.Drawing.Point(93, 4);
            this.rbBQApocrypha_N.Margin = new System.Windows.Forms.Padding(4);
            this.rbBQApocrypha_N.Name = "rbBQApocrypha_N";
            this.rbBQApocrypha_N.Size = new System.Drawing.Size(47, 21);
            this.rbBQApocrypha_N.TabIndex = 1;
            this.rbBQApocrypha_N.TabStop = true;
            this.rbBQApocrypha_N.Text = "No";
            this.rbBQApocrypha_N.UseVisualStyleBackColor = true;
            // 
            // rbBQApocrypha_Y
            // 
            this.rbBQApocrypha_Y.AutoSize = true;
            this.rbBQApocrypha_Y.Location = new System.Drawing.Point(4, 4);
            this.rbBQApocrypha_Y.Margin = new System.Windows.Forms.Padding(4);
            this.rbBQApocrypha_Y.Name = "rbBQApocrypha_Y";
            this.rbBQApocrypha_Y.Size = new System.Drawing.Size(53, 21);
            this.rbBQApocrypha_Y.TabIndex = 0;
            this.rbBQApocrypha_Y.Text = "Yes";
            this.rbBQApocrypha_Y.UseVisualStyleBackColor = true;
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.rbBQ_NT_N);
            this.panel4.Controls.Add(this.rbBQ_NT_Y);
            this.panel4.Location = new System.Drawing.Point(165, 225);
            this.panel4.Margin = new System.Windows.Forms.Padding(4);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(158, 30);
            this.panel4.TabIndex = 17;
            // 
            // rbBQ_NT_N
            // 
            this.rbBQ_NT_N.AutoSize = true;
            this.rbBQ_NT_N.Location = new System.Drawing.Point(93, 4);
            this.rbBQ_NT_N.Margin = new System.Windows.Forms.Padding(4);
            this.rbBQ_NT_N.Name = "rbBQ_NT_N";
            this.rbBQ_NT_N.Size = new System.Drawing.Size(47, 21);
            this.rbBQ_NT_N.TabIndex = 1;
            this.rbBQ_NT_N.Text = "No";
            this.rbBQ_NT_N.UseVisualStyleBackColor = true;
            // 
            // rbBQ_NT_Y
            // 
            this.rbBQ_NT_Y.AutoSize = true;
            this.rbBQ_NT_Y.Checked = true;
            this.rbBQ_NT_Y.Location = new System.Drawing.Point(4, 4);
            this.rbBQ_NT_Y.Margin = new System.Windows.Forms.Padding(4);
            this.rbBQ_NT_Y.Name = "rbBQ_NT_Y";
            this.rbBQ_NT_Y.Size = new System.Drawing.Size(53, 21);
            this.rbBQ_NT_Y.TabIndex = 0;
            this.rbBQ_NT_Y.TabStop = true;
            this.rbBQ_NT_Y.Text = "Yes";
            this.rbBQ_NT_Y.UseVisualStyleBackColor = true;
            // 
            // panel5
            // 
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.rbBQ_OT_N);
            this.panel5.Controls.Add(this.rbBQ_OT_Y);
            this.panel5.Location = new System.Drawing.Point(165, 187);
            this.panel5.Margin = new System.Windows.Forms.Padding(4);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(158, 30);
            this.panel5.TabIndex = 17;
            // 
            // rbBQ_OT_N
            // 
            this.rbBQ_OT_N.AutoSize = true;
            this.rbBQ_OT_N.Location = new System.Drawing.Point(93, 4);
            this.rbBQ_OT_N.Margin = new System.Windows.Forms.Padding(4);
            this.rbBQ_OT_N.Name = "rbBQ_OT_N";
            this.rbBQ_OT_N.Size = new System.Drawing.Size(47, 21);
            this.rbBQ_OT_N.TabIndex = 1;
            this.rbBQ_OT_N.Text = "No";
            this.rbBQ_OT_N.UseVisualStyleBackColor = true;
            // 
            // rbBQ_OT_Y
            // 
            this.rbBQ_OT_Y.AutoSize = true;
            this.rbBQ_OT_Y.Checked = true;
            this.rbBQ_OT_Y.Location = new System.Drawing.Point(4, 4);
            this.rbBQ_OT_Y.Margin = new System.Windows.Forms.Padding(4);
            this.rbBQ_OT_Y.Name = "rbBQ_OT_Y";
            this.rbBQ_OT_Y.Size = new System.Drawing.Size(53, 21);
            this.rbBQ_OT_Y.TabIndex = 0;
            this.rbBQ_OT_Y.TabStop = true;
            this.rbBQ_OT_Y.Text = "Yes";
            this.rbBQ_OT_Y.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.rbBQIsBible_N);
            this.panel2.Controls.Add(this.rbBQIsBible_Y);
            this.panel2.Location = new System.Drawing.Point(165, 149);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(158, 30);
            this.panel2.TabIndex = 16;
            // 
            // rbBQIsBible_N
            // 
            this.rbBQIsBible_N.AutoSize = true;
            this.rbBQIsBible_N.Location = new System.Drawing.Point(93, 4);
            this.rbBQIsBible_N.Margin = new System.Windows.Forms.Padding(4);
            this.rbBQIsBible_N.Name = "rbBQIsBible_N";
            this.rbBQIsBible_N.Size = new System.Drawing.Size(47, 21);
            this.rbBQIsBible_N.TabIndex = 1;
            this.rbBQIsBible_N.Text = "No";
            this.rbBQIsBible_N.UseVisualStyleBackColor = true;
            // 
            // rbBQIsBible_Y
            // 
            this.rbBQIsBible_Y.AutoSize = true;
            this.rbBQIsBible_Y.Checked = true;
            this.rbBQIsBible_Y.Location = new System.Drawing.Point(4, 4);
            this.rbBQIsBible_Y.Margin = new System.Windows.Forms.Padding(4);
            this.rbBQIsBible_Y.Name = "rbBQIsBible_Y";
            this.rbBQIsBible_Y.Size = new System.Drawing.Size(53, 21);
            this.rbBQIsBible_Y.TabIndex = 0;
            this.rbBQIsBible_Y.TabStop = true;
            this.rbBQIsBible_Y.Text = "Yes";
            this.rbBQIsBible_Y.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(17, 234);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(118, 17);
            this.label10.TabIndex = 15;
            this.label10.Text = "New Testament?:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(17, 193);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(113, 17);
            this.label11.TabIndex = 14;
            this.label11.Text = "Old Testament?:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(17, 155);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(65, 17);
            this.label12.TabIndex = 13;
            this.label12.Text = "Is Bible?:";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.rbR2L);
            this.panel1.Controls.Add(this.rbL2R);
            this.panel1.Location = new System.Drawing.Point(165, 102);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(398, 39);
            this.panel1.TabIndex = 12;
            // 
            // rbR2L
            // 
            this.rbR2L.AutoSize = true;
            this.rbR2L.Location = new System.Drawing.Point(223, 11);
            this.rbR2L.Margin = new System.Windows.Forms.Padding(4);
            this.rbR2L.Name = "rbR2L";
            this.rbR2L.Size = new System.Drawing.Size(144, 21);
            this.rbR2L.TabIndex = 3;
            this.rbR2L.Text = "Right-To-Left Text";
            this.rbR2L.UseVisualStyleBackColor = true;
            // 
            // rbL2R
            // 
            this.rbL2R.AutoSize = true;
            this.rbL2R.Checked = true;
            this.rbL2R.Location = new System.Drawing.Point(16, 11);
            this.rbL2R.Margin = new System.Windows.Forms.Padding(4);
            this.rbL2R.Name = "rbL2R";
            this.rbL2R.Size = new System.Drawing.Size(139, 21);
            this.rbL2R.TabIndex = 2;
            this.rbL2R.TabStop = true;
            this.rbL2R.Text = "Left-to-Right Text";
            this.rbL2R.UseVisualStyleBackColor = true;
            // 
            // txtBQCopyright
            // 
            this.txtBQCopyright.Location = new System.Drawing.Point(165, 70);
            this.txtBQCopyright.Margin = new System.Windows.Forms.Padding(4);
            this.txtBQCopyright.Name = "txtBQCopyright";
            this.txtBQCopyright.Size = new System.Drawing.Size(669, 22);
            this.txtBQCopyright.TabIndex = 11;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(17, 79);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(72, 17);
            this.label9.TabIndex = 10;
            this.label9.Text = "Copyright:";
            // 
            // txtBQBibleNameShort
            // 
            this.txtBQBibleNameShort.Location = new System.Drawing.Point(165, 38);
            this.txtBQBibleNameShort.Margin = new System.Windows.Forms.Padding(4);
            this.txtBQBibleNameShort.Name = "txtBQBibleNameShort";
            this.txtBQBibleNameShort.Size = new System.Drawing.Size(669, 22);
            this.txtBQBibleNameShort.TabIndex = 9;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(17, 47);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(122, 17);
            this.label8.TabIndex = 8;
            this.label8.Text = "Bible Name Short:";
            // 
            // txtBQBibleNameFull
            // 
            this.txtBQBibleNameFull.Location = new System.Drawing.Point(165, 6);
            this.txtBQBibleNameFull.Margin = new System.Windows.Forms.Padding(4);
            this.txtBQBibleNameFull.Name = "txtBQBibleNameFull";
            this.txtBQBibleNameFull.Size = new System.Drawing.Size(669, 22);
            this.txtBQBibleNameFull.TabIndex = 7;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(17, 15);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(110, 17);
            this.label7.TabIndex = 6;
            this.label7.Text = "Bible Name Full:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 299);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "CodePage:";
            // 
            // cboBQCodePage
            // 
            this.cboBQCodePage.FormattingEnabled = true;
            this.cboBQCodePage.Location = new System.Drawing.Point(165, 295);
            this.cboBQCodePage.Margin = new System.Windows.Forms.Padding(4);
            this.cboBQCodePage.Name = "cboBQCodePage";
            this.cboBQCodePage.Size = new System.Drawing.Size(512, 24);
            this.cboBQCodePage.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.cboExt);
            this.groupBox1.Controls.Add(this.txtDir);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cmdGetDir);
            this.groupBox1.Location = new System.Drawing.Point(9, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(911, 86);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Source USFM Files Directory:";
            // 
            // cboExt
            // 
            this.cboExt.FormattingEnabled = true;
            this.cboExt.Location = new System.Drawing.Point(161, 53);
            this.cboExt.Margin = new System.Windows.Forms.Padding(4);
            this.cboExt.Name = "cboExt";
            this.cboExt.Size = new System.Drawing.Size(93, 24);
            this.cboExt.TabIndex = 11;
            // 
            // txtDir
            // 
            this.txtDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDir.Location = new System.Drawing.Point(40, 22);
            this.txtDir.Margin = new System.Windows.Forms.Padding(4);
            this.txtDir.Name = "txtDir";
            this.txtDir.Size = new System.Drawing.Size(856, 22);
            this.txtDir.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 58);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(141, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "USFM File Extension:";
            // 
            // cmdGetDir
            // 
            this.cmdGetDir.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdGetDir.Location = new System.Drawing.Point(7, 20);
            this.cmdGetDir.Margin = new System.Windows.Forms.Padding(4);
            this.cmdGetDir.Name = "cmdGetDir";
            this.cmdGetDir.Size = new System.Drawing.Size(33, 28);
            this.cmdGetDir.TabIndex = 10;
            this.cmdGetDir.Text = "...";
            this.cmdGetDir.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdGetDir.UseVisualStyleBackColor = true;
            this.cmdGetDir.Click += new System.EventHandler(this.cmdGetDir_Click);
            // 
            // ttGrid
            // 
            this.ttGrid.AutomaticDelay = 100;
            this.ttGrid.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.ttGrid.UseAnimation = false;
            this.ttGrid.UseFading = false;
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Location = new System.Drawing.Point(5, 669);
            this.lblVersion.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(64, 17);
            this.lblVersion.TabIndex = 21;
            this.lblVersion.Text = "Version: ";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // cmdAbout
            // 
            this.cmdAbout.Location = new System.Drawing.Point(715, 663);
            this.cmdAbout.Margin = new System.Windows.Forms.Padding(4);
            this.cmdAbout.Name = "cmdAbout";
            this.cmdAbout.Size = new System.Drawing.Size(100, 28);
            this.cmdAbout.TabIndex = 22;
            this.cmdAbout.Text = "About";
            this.cmdAbout.UseVisualStyleBackColor = true;
            this.cmdAbout.Click += new System.EventHandler(this.cmdAbout_Click);
            // 
            // ckAddBOM
            // 
            this.ckAddBOM.AutoSize = true;
            this.ckAddBOM.Location = new System.Drawing.Point(658, 43);
            this.ckAddBOM.Name = "ckAddBOM";
            this.ckAddBOM.Size = new System.Drawing.Size(171, 21);
            this.ckAddBOM.TabIndex = 18;
            this.ckAddBOM.Text = "add BOM to UTF-8 file";
            this.ckAddBOM.UseVisualStyleBackColor = true;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(916, 690);
            this.Controls.Add(this.cmdAbout);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.cmdClose);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(934, 735);
            this.MinimumSize = new System.Drawing.Size(934, 735);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GoBibleCreator USFM Preprocessor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridExtraTags)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.updownLongWords)).EndInit();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdClose;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox cboEncoding;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button cmdConvertToUTF_WithoutBOM;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtOutFileExtension;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button cmdCopyToClipboard;
        private System.Windows.Forms.RichTextBox txtOutput;
        private System.Windows.Forms.ListView lvOutput;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Button cmdProcess;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtBookNameTag;
        private System.Windows.Forms.RadioButton radioDropComments;
        private System.Windows.Forms.RadioButton radioKeepComments;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtDir;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button cmdGetDir;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button cmdScanTags;
        private System.Windows.Forms.DataGridView gridExtraTags;
        private System.Windows.Forms.Label lblOutputExtension;
        private System.Windows.Forms.TextBox txtOutputTagExtension;
        private System.Windows.Forms.Button btnProcessExtraTags;
        private System.Windows.Forms.ComboBox cboExt;
        private System.Windows.Forms.ToolTip ttGrid;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.TextBox txtDisplayTag;
        private System.Windows.Forms.Label lblDisplayTag;
        private System.Windows.Forms.NumericUpDown updownLongWords;
        private System.Windows.Forms.Button cmdFindAllLongWords;
        private System.Windows.Forms.RadioButton rbWordsAndPunctuation;
        private System.Windows.Forms.RadioButton rbWordsOnly;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton rbWordsAndHyphens;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.RadioButton rbR2L;
        private System.Windows.Forms.RadioButton rbL2R;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboBQCodePage;
        private System.Windows.Forms.TextBox txtBQCopyright;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtBQBibleNameShort;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtBQBibleNameFull;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.RadioButton rbBQApocrypha_N;
        private System.Windows.Forms.RadioButton rbBQApocrypha_Y;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.RadioButton rbBQ_NT_N;
        private System.Windows.Forms.RadioButton rbBQ_NT_Y;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.RadioButton rbBQ_OT_N;
        private System.Windows.Forms.RadioButton rbBQ_OT_Y;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton rbBQIsBible_N;
        private System.Windows.Forms.RadioButton rbBQIsBible_Y;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtBQVerseSign;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txtBQChapterSign;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.ComboBox cboBQFontCharSet;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtBQAlphabet;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.RadioButton rbBQGreek_N;
        private System.Windows.Forms.RadioButton rbBQGreek_Y;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button cmdGenerateBQExport;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox txtBQ_BooknameTag;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button cmdVersificationFind;
        private System.Windows.Forms.Button cmdAbout;
        private System.Windows.Forms.Label lblExportTo;
        private System.Windows.Forms.CheckBox chkCharacterReplacement;
        private System.Windows.Forms.CheckBox ckAddBOM;
    }
}

