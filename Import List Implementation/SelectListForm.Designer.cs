namespace Import_List_Implementation
{
    partial class SelectList
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
            if(disposing && (components != null))
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
            this.label1 = new System.Windows.Forms.Label();
            this.siteURL = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Lists = new System.Windows.Forms.ComboBox();
            this.ImportBtn = new System.Windows.Forms.Button();
            this.CancelBtn = new System.Windows.Forms.Button();
            this.chkContent = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(170, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Site URL that contains existing list:";
            // 
            // siteURL
            // 
            this.siteURL.Location = new System.Drawing.Point(15, 26);
            this.siteURL.Name = "siteURL";
            this.siteURL.Size = new System.Drawing.Size(237, 20);
            this.siteURL.TabIndex = 1;
            this.siteURL.TextChanged += new System.EventHandler(this.OnTextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Select list:";
            // 
            // Lists
            // 
            this.Lists.Enabled = false;
            this.Lists.FormattingEnabled = true;
            this.Lists.Location = new System.Drawing.Point(15, 79);
            this.Lists.Name = "Lists";
            this.Lists.Size = new System.Drawing.Size(237, 21);
            this.Lists.TabIndex = 3;
            this.Lists.SelectedIndexChanged += new System.EventHandler(this.OnSelectList);
            // 
            // ImportBtn
            // 
            this.ImportBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ImportBtn.Enabled = false;
            this.ImportBtn.Location = new System.Drawing.Point(96, 135);
            this.ImportBtn.Name = "ImportBtn";
            this.ImportBtn.Size = new System.Drawing.Size(75, 23);
            this.ImportBtn.TabIndex = 4;
            this.ImportBtn.Text = "&Import";
            this.ImportBtn.UseVisualStyleBackColor = true;
            // 
            // CancelBtn
            // 
            this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelBtn.Location = new System.Drawing.Point(177, 135);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(75, 23);
            this.CancelBtn.TabIndex = 5;
            this.CancelBtn.Text = "&Cancel";
            this.CancelBtn.UseVisualStyleBackColor = true;
            // 
            // chkContent
            // 
            this.chkContent.AutoSize = true;
            this.chkContent.Location = new System.Drawing.Point(15, 107);
            this.chkContent.Name = "chkContent";
            this.chkContent.Size = new System.Drawing.Size(100, 17);
            this.chkContent.TabIndex = 6;
            this.chkContent.Text = "Include content";
            this.chkContent.UseVisualStyleBackColor = true;
            // 
            // SelectList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CancelBtn;
            this.ClientSize = new System.Drawing.Size(266, 168);
            this.ControlBox = false;
            this.Controls.Add(this.chkContent);
            this.Controls.Add(this.CancelBtn);
            this.Controls.Add(this.ImportBtn);
            this.Controls.Add(this.Lists);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.siteURL);
            this.Controls.Add(this.label1);
            this.Name = "SelectList";
            this.Text = "Select List";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox siteURL;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox Lists;
        private System.Windows.Forms.Button ImportBtn;
        private System.Windows.Forms.Button CancelBtn;
        private System.Windows.Forms.CheckBox chkContent;
    }
}