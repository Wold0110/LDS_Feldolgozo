namespace LDS_Feldolgozo
{
    partial class FilterForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FilterForm));
            this.noneBtn = new System.Windows.Forms.Button();
            this.flipBtn = new System.Windows.Forms.Button();
            this.allBtn = new System.Windows.Forms.Button();
            this.okBtn = new System.Windows.Forms.Button();
            this.linePanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // noneBtn
            // 
            this.noneBtn.Location = new System.Drawing.Point(321, 12);
            this.noneBtn.Name = "noneBtn";
            this.noneBtn.Size = new System.Drawing.Size(147, 23);
            this.noneBtn.TabIndex = 1;
            this.noneBtn.Text = "Egyik se";
            this.noneBtn.UseVisualStyleBackColor = true;
            this.noneBtn.Click += new System.EventHandler(this.noneBtn_Click);
            // 
            // flipBtn
            // 
            this.flipBtn.Location = new System.Drawing.Point(155, 12);
            this.flipBtn.Name = "flipBtn";
            this.flipBtn.Size = new System.Drawing.Size(160, 23);
            this.flipBtn.TabIndex = 2;
            this.flipBtn.Text = "Fordítás";
            this.flipBtn.UseVisualStyleBackColor = true;
            this.flipBtn.Click += new System.EventHandler(this.flipBtn_Click);
            // 
            // allBtn
            // 
            this.allBtn.Location = new System.Drawing.Point(12, 12);
            this.allBtn.Name = "allBtn";
            this.allBtn.Size = new System.Drawing.Size(137, 23);
            this.allBtn.TabIndex = 0;
            this.allBtn.Text = "Mindegyik";
            this.allBtn.UseVisualStyleBackColor = true;
            this.allBtn.Click += new System.EventHandler(this.allBtn_Click);
            // 
            // okBtn
            // 
            this.okBtn.Location = new System.Drawing.Point(12, 382);
            this.okBtn.Name = "okBtn";
            this.okBtn.Size = new System.Drawing.Size(456, 23);
            this.okBtn.TabIndex = 3;
            this.okBtn.Text = "Ok";
            this.okBtn.UseVisualStyleBackColor = true;
            this.okBtn.Click += new System.EventHandler(this.okBtn_Click);
            // 
            // linePanel
            // 
            this.linePanel.Location = new System.Drawing.Point(12, 41);
            this.linePanel.Name = "linePanel";
            this.linePanel.Size = new System.Drawing.Size(456, 335);
            this.linePanel.TabIndex = 4;
            // 
            // FilterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(480, 417);
            this.Controls.Add(this.linePanel);
            this.Controls.Add(this.okBtn);
            this.Controls.Add(this.flipBtn);
            this.Controls.Add(this.noneBtn);
            this.Controls.Add(this.allBtn);
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FilterForm";
            this.Text = "FilterForm";
            this.ResumeLayout(false);

        }

        #endregion
        private Button noneBtn;
        private Button flipBtn;
        private Button allBtn;
        private Button okBtn;
        private Panel linePanel;
    }
}