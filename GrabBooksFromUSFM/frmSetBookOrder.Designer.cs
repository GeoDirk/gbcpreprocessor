namespace GBC_USFM_Preprocessor
{
    partial class frmSetBookOrder
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
            this.label23 = new System.Windows.Forms.Label();
            this.lvOutput = new DragNDrop.DragAndDropListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdProcess = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.txtBookNameTag = new System.Windows.Forms.TextBox();
            this.cmdSave = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.cmdLoadBookOrder = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(7, 46);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(222, 13);
            this.label23.TabIndex = 44;
            this.label23.Text = "Reorder the books into the correct sequence:";
            // 
            // lvOutput
            // 
            this.lvOutput.AllowDrop = true;
            this.lvOutput.AllowReorder = true;
            this.lvOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvOutput.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader3,
            this.columnHeader4});
            this.lvOutput.FullRowSelect = true;
            this.lvOutput.GridLines = true;
            this.lvOutput.LineColor = System.Drawing.Color.Green;
            this.lvOutput.Location = new System.Drawing.Point(9, 62);
            this.lvOutput.Margin = new System.Windows.Forms.Padding(2);
            this.lvOutput.Name = "lvOutput";
            this.lvOutput.Size = new System.Drawing.Size(476, 316);
            this.lvOutput.TabIndex = 43;
            this.lvOutput.UseCompatibleStateImageBehavior = false;
            this.lvOutput.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Original Order";
            this.columnHeader1.Width = 100;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Title";
            this.columnHeader3.Width = 250;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Filename";
            this.columnHeader4.Width = 357;
            // 
            // cmdCancel
            // 
            this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCancel.Location = new System.Drawing.Point(391, 384);
            this.cmdCancel.Margin = new System.Windows.Forms.Padding(2);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(93, 23);
            this.cmdCancel.TabIndex = 46;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cmdProcess
            // 
            this.cmdProcess.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdProcess.Location = new System.Drawing.Point(144, 5);
            this.cmdProcess.Name = "cmdProcess";
            this.cmdProcess.Size = new System.Drawing.Size(136, 23);
            this.cmdProcess.TabIndex = 47;
            this.cmdProcess.Text = "Grab Book Names";
            this.cmdProcess.UseVisualStyleBackColor = true;
            this.cmdProcess.Click += new System.EventHandler(this.cmdProcess_Click);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 11);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 13);
            this.label5.TabIndex = 49;
            this.label5.Text = "Bookname Tag:";
            // 
            // txtBookNameTag
            // 
            this.txtBookNameTag.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBookNameTag.Location = new System.Drawing.Point(99, 7);
            this.txtBookNameTag.Name = "txtBookNameTag";
            this.txtBookNameTag.Size = new System.Drawing.Size(40, 20);
            this.txtBookNameTag.TabIndex = 48;
            this.txtBookNameTag.Text = "\\h";
            // 
            // cmdSave
            // 
            this.cmdSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSave.Location = new System.Drawing.Point(281, 384);
            this.cmdSave.Margin = new System.Windows.Forms.Padding(2);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(105, 23);
            this.cmdSave.TabIndex = 50;
            this.cmdSave.Text = "Save Order";
            this.cmdSave.UseVisualStyleBackColor = true;
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // cmdLoadBookOrder
            // 
            this.cmdLoadBookOrder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdLoadBookOrder.Location = new System.Drawing.Point(286, 6);
            this.cmdLoadBookOrder.Name = "cmdLoadBookOrder";
            this.cmdLoadBookOrder.Size = new System.Drawing.Size(136, 23);
            this.cmdLoadBookOrder.TabIndex = 51;
            this.cmdLoadBookOrder.Text = "Load Existing Book Order";
            this.cmdLoadBookOrder.UseVisualStyleBackColor = true;
            this.cmdLoadBookOrder.Click += new System.EventHandler(this.cmdLoadBookOrder_Click);
            // 
            // frmSetBookOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(493, 415);
            this.Controls.Add(this.cmdLoadBookOrder);
            this.Controls.Add(this.cmdSave);
            this.Controls.Add(this.cmdProcess);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtBookNameTag);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.lvOutput);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmSetBookOrder";
            this.Text = "frmSetBookOrder";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label23;
        private DragNDrop.DragAndDropListView lvOutput;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Button cmdProcess;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtBookNameTag;
        private System.Windows.Forms.Button cmdSave;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button cmdLoadBookOrder;
    }
}