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
            this.AddAccount = new System.Windows.Forms.Button();
            this.usernameBox = new System.Windows.Forms.TextBox();
            this.passwordBox = new System.Windows.Forms.TextBox();
            this.Queue = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.Data = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // AddAccount
            // 
            this.AddAccount.Location = new System.Drawing.Point(13, 13);
            this.AddAccount.Name = "AddAccount";
            this.AddAccount.Size = new System.Drawing.Size(95, 24);
            this.AddAccount.TabIndex = 0;
            this.AddAccount.Text = "Add Account";
            this.AddAccount.UseVisualStyleBackColor = true;
            this.AddAccount.Click += new System.EventHandler(this.AddAccount_Click);
            // 
            // usernameBox
            // 
            this.usernameBox.Location = new System.Drawing.Point(114, 17);
            this.usernameBox.Name = "usernameBox";
            this.usernameBox.Size = new System.Drawing.Size(100, 20);
            this.usernameBox.TabIndex = 1;
            this.usernameBox.Text = "LoginName";
            // 
            // passwordBox
            // 
            this.passwordBox.Location = new System.Drawing.Point(233, 17);
            this.passwordBox.Name = "passwordBox";
            this.passwordBox.Size = new System.Drawing.Size(100, 20);
            this.passwordBox.TabIndex = 2;
            this.passwordBox.Text = "Password";
            // 
            // Queue
            // 
            this.Queue.Location = new System.Drawing.Point(13, 43);
            this.Queue.Name = "Queue";
            this.Queue.Size = new System.Drawing.Size(320, 58);
            this.Queue.TabIndex = 3;
            this.Queue.Text = "Start Bot";
            this.Queue.UseVisualStyleBackColor = true;
            this.Queue.Click += new System.EventHandler(this.Queue_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 118);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Live Stats";
            // 
            // Data
            // 
            this.Data.FormattingEnabled = true;
            this.Data.Location = new System.Drawing.Point(16, 149);
            this.Data.Name = "Data";
            this.Data.Size = new System.Drawing.Size(353, 21);
            this.Data.TabIndex = 5;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(381, 331);
            this.Controls.Add(this.Data);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Queue);
            this.Controls.Add(this.passwordBox);
            this.Controls.Add(this.usernameBox);
            this.Controls.Add(this.AddAccount);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button AddAccount;
        private System.Windows.Forms.TextBox usernameBox;
        private System.Windows.Forms.TextBox passwordBox;
        private System.Windows.Forms.Button Queue;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox Data;
    }
}

