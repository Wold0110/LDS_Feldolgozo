namespace LDS_Feldolgozo
{
    partial class DatePickers
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DatePickers));
            this.fromDate = new System.Windows.Forms.DateTimePicker();
            this.toDate = new System.Windows.Forms.DateTimePicker();
            this.okBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // fromDate
            // 
            this.fromDate.Location = new System.Drawing.Point(12, 24);
            this.fromDate.Name = "fromDate";
            this.fromDate.Size = new System.Drawing.Size(200, 23);
            this.fromDate.TabIndex = 0;
            // 
            // toDate
            // 
            this.toDate.Location = new System.Drawing.Point(12, 67);
            this.toDate.Name = "toDate";
            this.toDate.Size = new System.Drawing.Size(200, 23);
            this.toDate.TabIndex = 1;
            // 
            // okBtn
            // 
            this.okBtn.BackColor = System.Drawing.Color.DarkGreen;
            this.okBtn.ForeColor = System.Drawing.Color.White;
            this.okBtn.Location = new System.Drawing.Point(12, 105);
            this.okBtn.Name = "okBtn";
            this.okBtn.Size = new System.Drawing.Size(200, 31);
            this.okBtn.TabIndex = 2;
            this.okBtn.Text = "Ok";
            this.okBtn.UseVisualStyleBackColor = false;
            this.okBtn.Click += new System.EventHandler(this.okBtn_Click);
            // 
            // DatePickers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(227, 160);
            this.Controls.Add(this.okBtn);
            this.Controls.Add(this.toDate);
            this.Controls.Add(this.fromDate);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DatePickers";
            this.Text = "Dátum";
            this.ResumeLayout(false);

        }

        #endregion

        private DateTimePicker fromDate;
        private DateTimePicker toDate;
        private Button okBtn;
    }
}