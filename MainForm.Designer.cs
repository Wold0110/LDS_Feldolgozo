namespace LDS_Feldolgozo
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.okBtn = new System.Windows.Forms.Button();
            this.readBtn = new System.Windows.Forms.Button();
            this.newfileBtn = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.sumMode = new System.Windows.Forms.RadioButton();
            this.dayByDayMode = new System.Windows.Forms.RadioButton();
            this.doGroups = new System.Windows.Forms.CheckBox();
            this.doABC = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // okBtn
            // 
            resources.ApplyResources(this.okBtn, "okBtn");
            this.okBtn.BackColor = System.Drawing.Color.ForestGreen;
            this.okBtn.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.okBtn.Name = "okBtn";
            this.okBtn.UseVisualStyleBackColor = false;
            this.okBtn.Click += new System.EventHandler(this.okBtn_Click);
            // 
            // readBtn
            // 
            resources.ApplyResources(this.readBtn, "readBtn");
            this.readBtn.BackColor = System.Drawing.Color.ForestGreen;
            this.readBtn.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.readBtn.Name = "readBtn";
            this.readBtn.UseVisualStyleBackColor = false;
            this.readBtn.Click += new System.EventHandler(this.readBtn_Click);
            // 
            // newfileBtn
            // 
            resources.ApplyResources(this.newfileBtn, "newfileBtn");
            this.newfileBtn.BackColor = System.Drawing.Color.ForestGreen;
            this.newfileBtn.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.newfileBtn.Name = "newfileBtn";
            this.newfileBtn.UseVisualStyleBackColor = false;
            this.newfileBtn.Click += new System.EventHandler(this.newfileBtn_Click);
            // 
            // textBox1
            // 
            resources.ApplyResources(this.textBox1, "textBox1");
            this.textBox1.BackColor = System.Drawing.Color.Green;
            this.textBox1.ForeColor = System.Drawing.Color.White;
            this.textBox1.Name = "textBox1";
            // 
            // sumMode
            // 
            resources.ApplyResources(this.sumMode, "sumMode");
            this.sumMode.Checked = true;
            this.sumMode.ForeColor = System.Drawing.Color.DarkGreen;
            this.sumMode.Name = "sumMode";
            this.sumMode.TabStop = true;
            this.sumMode.UseVisualStyleBackColor = true;

            // 
            // dayByDayMode
            // 
            resources.ApplyResources(this.dayByDayMode, "dayByDayMode");
            this.dayByDayMode.ForeColor = System.Drawing.Color.DarkGreen;
            this.dayByDayMode.Name = "dayByDayMode";
            this.dayByDayMode.TabStop = true;
            this.dayByDayMode.UseVisualStyleBackColor = true;

            // 
            // doGroups
            // 
            resources.ApplyResources(this.doGroups, "doGroups");
            this.doGroups.ForeColor = System.Drawing.Color.DarkGreen;
            this.doGroups.Name = "doGroups";
            this.doGroups.UseVisualStyleBackColor = true;

            // 
            // doABC
            // 
            resources.ApplyResources(this.doABC, "doABC");
            this.doABC.ForeColor = System.Drawing.Color.DarkGreen;
            this.doABC.Name = "doABC";
            this.doABC.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Controls.Add(this.doABC);
            this.Controls.Add(this.doGroups);
            this.Controls.Add(this.dayByDayMode);
            this.Controls.Add(this.sumMode);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.newfileBtn);
            this.Controls.Add(this.readBtn);
            this.Controls.Add(this.okBtn);
            this.Name = "MainForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button okBtn;
        private Button readBtn;
        private Button newfileBtn;
        private TextBox textBox1;
        private RadioButton sumMode;
        private RadioButton dayByDayMode;
        private CheckBox doGroups;
        private CheckBox doABC;
    }
}