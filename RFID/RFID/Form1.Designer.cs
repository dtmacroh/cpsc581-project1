namespace RFID_Example
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.destinationBox = new System.Windows.Forms.TextBox();
            this.writeButton2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.issue_date_box = new System.Windows.Forms.TextBox();
            this.issued_to_box = new System.Windows.Forms.TextBox();
            this.progress_label = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // destinationBox
            // 
            this.destinationBox.Enabled = false;
            this.destinationBox.Location = new System.Drawing.Point(385, 241);
            this.destinationBox.Name = "destinationBox";
            this.destinationBox.Size = new System.Drawing.Size(294, 20);
            this.destinationBox.TabIndex = 18;
            // 
            // writeButton2
            // 
            this.writeButton2.Location = new System.Drawing.Point(434, 309);
            this.writeButton2.Name = "writeButton2";
            this.writeButton2.Size = new System.Drawing.Size(75, 23);
            this.writeButton2.TabIndex = 19;
            this.writeButton2.Text = "Write";
            this.writeButton2.UseVisualStyleBackColor = true;
            this.writeButton2.Click += new System.EventHandler(this.writeButton2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(272, 248);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 13);
            this.label1.TabIndex = 20;
            this.label1.Text = "Team Lead Message";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(272, 182);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 21;
            this.label2.Text = "Issue Date";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(272, 137);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 13);
            this.label3.TabIndex = 22;
            this.label3.Text = "Issued To";
            // 
            // issue_date_box
            // 
            this.issue_date_box.Enabled = false;
            this.issue_date_box.Location = new System.Drawing.Point(385, 175);
            this.issue_date_box.Name = "issue_date_box";
            this.issue_date_box.Size = new System.Drawing.Size(294, 20);
            this.issue_date_box.TabIndex = 23;
            // 
            // issued_to_box
            // 
            this.issued_to_box.Enabled = false;
            this.issued_to_box.Location = new System.Drawing.Point(385, 134);
            this.issued_to_box.Name = "issued_to_box";
            this.issued_to_box.Size = new System.Drawing.Size(294, 20);
            this.issued_to_box.TabIndex = 24;
            // 
            // progress_label
            // 
            this.progress_label.AutoSize = true;
            this.progress_label.Location = new System.Drawing.Point(272, 213);
            this.progress_label.Name = "progress_label";
            this.progress_label.Size = new System.Drawing.Size(48, 13);
            this.progress_label.TabIndex = 25;
            this.progress_label.Text = "Progress";
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(385, 203);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(294, 23);
            this.progressBar.TabIndex = 26;
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1008, 361);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.progress_label);
            this.Controls.Add(this.issued_to_box);
            this.Controls.Add(this.issue_date_box);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.writeButton2);
            this.Controls.Add(this.destinationBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Girl Scout Badge Scanner";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button writeButton2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox issue_date_box;
        private System.Windows.Forms.TextBox issued_to_box;
        private System.Windows.Forms.TextBox destinationBox;
        private System.Windows.Forms.Label progress_label;
        private System.Windows.Forms.ProgressBar progressBar;
    }
}

