namespace AutoQueuer
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
            this.Queue = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.Data = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // Queue
            // 
            this.Queue.Location = new System.Drawing.Point(12, 12);
            this.Queue.Name = "Queue";
            this.Queue.Size = new System.Drawing.Size(812, 58);
            this.Queue.TabIndex = 3;
            this.Queue.Text = "Start Bot";
            this.Queue.UseVisualStyleBackColor = true;
            this.Queue.Click += new System.EventHandler(this.Queue_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(349, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Live Stats";
            // 
            // Data
            // 
            this.Data.FormattingEnabled = true;
            this.Data.Location = new System.Drawing.Point(12, 89);
            this.Data.Name = "Data";
            this.Data.Size = new System.Drawing.Size(812, 238);
            this.Data.TabIndex = 6;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(836, 331);
            this.Controls.Add(this.Data);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Queue);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Queue;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox Data;
    }
}

