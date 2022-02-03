namespace LDS_Feldolgozo
{
    partial class SettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.threadNumInput = new System.Windows.Forms.NumericUpDown();
            this.theadLabel = new System.Windows.Forms.Label();
            this.okBtn = new System.Windows.Forms.Button();
            this.tmpLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.threadNumInput)).BeginInit();
            this.SuspendLayout();
            // 
            // threadNumInput
            // 
            this.threadNumInput.Location = new System.Drawing.Point(60, 18);
            this.threadNumInput.Name = "threadNumInput";
            this.threadNumInput.Size = new System.Drawing.Size(190, 23);
            this.threadNumInput.TabIndex = 0;
            this.threadNumInput.ValueChanged += new System.EventHandler(this.threadNumInput_ValueChanged);
            // 
            // theadLabel
            // 
            this.theadLabel.AutoSize = true;
            this.theadLabel.Location = new System.Drawing.Point(12, 20);
            this.theadLabel.Name = "theadLabel";
            this.theadLabel.Size = new System.Drawing.Size(42, 15);
            this.theadLabel.TabIndex = 1;
            this.theadLabel.Text = "Szálak:";
            // 
            // okBtn
            // 
            this.okBtn.Location = new System.Drawing.Point(88, 95);
            this.okBtn.Name = "okBtn";
            this.okBtn.Size = new System.Drawing.Size(65, 25);
            this.okBtn.TabIndex = 2;
            this.okBtn.Text = "Ok";
            this.okBtn.UseVisualStyleBackColor = true;
            this.okBtn.Click += new System.EventHandler(this.okBtn_Click);
            // 
            // tmpLabel
            // 
            this.tmpLabel.AutoSize = true;
            this.tmpLabel.Location = new System.Drawing.Point(71, 61);
            this.tmpLabel.Name = "tmpLabel";
            this.tmpLabel.Size = new System.Drawing.Size(139, 15);
            this.tmpLabel.TabIndex = 3;
            this.tmpLabel.Text = "(igen, ennyi van jelenleg)";
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(262, 132);
            this.Controls.Add(this.tmpLabel);
            this.Controls.Add(this.okBtn);
            this.Controls.Add(this.theadLabel);
            this.Controls.Add(this.threadNumInput);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SettingsForm";
            this.Text = "Beállítások";
            ((System.ComponentModel.ISupportInitialize)(this.threadNumInput)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private NumericUpDown threadNumInput;
        private Label theadLabel;
        private Button okBtn;
        private Label tmpLabel;
    }
}