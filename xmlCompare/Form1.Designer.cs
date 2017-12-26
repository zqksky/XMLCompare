namespace xmlCompare
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
            this.btnOpenPath2 = new System.Windows.Forms.Button();
            this.txtPath2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnOpenPath1 = new System.Windows.Forms.Button();
            this.txtPath1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnTest = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnOpenPath2
            // 
            this.btnOpenPath2.Location = new System.Drawing.Point(364, 59);
            this.btnOpenPath2.Name = "btnOpenPath2";
            this.btnOpenPath2.Size = new System.Drawing.Size(75, 23);
            this.btnOpenPath2.TabIndex = 15;
            this.btnOpenPath2.Text = "打开";
            this.btnOpenPath2.UseVisualStyleBackColor = true;
            this.btnOpenPath2.Click += new System.EventHandler(this.btnOpenPath2_Click);
            // 
            // txtPath2
            // 
            this.txtPath2.Location = new System.Drawing.Point(93, 61);
            this.txtPath2.Name = "txtPath2";
            this.txtPath2.Size = new System.Drawing.Size(256, 20);
            this.txtPath2.TabIndex = 14;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "XML路径2：";
            // 
            // btnOpenPath1
            // 
            this.btnOpenPath1.Location = new System.Drawing.Point(364, 23);
            this.btnOpenPath1.Name = "btnOpenPath1";
            this.btnOpenPath1.Size = new System.Drawing.Size(75, 23);
            this.btnOpenPath1.TabIndex = 12;
            this.btnOpenPath1.Text = "打开";
            this.btnOpenPath1.UseVisualStyleBackColor = true;
            this.btnOpenPath1.Click += new System.EventHandler(this.btnOpenPath1_Click);
            // 
            // txtPath1
            // 
            this.txtPath1.Location = new System.Drawing.Point(93, 25);
            this.txtPath1.Name = "txtPath1";
            this.txtPath1.Size = new System.Drawing.Size(256, 20);
            this.txtPath1.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "XML路径1：";
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(135, 109);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(75, 23);
            this.btnTest.TabIndex = 9;
            this.btnTest.Text = "运行";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(254, 109);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 16;
            this.btnExit.Text = "退出";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(460, 163);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnOpenPath2);
            this.Controls.Add(this.txtPath2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnOpenPath1);
            this.Controls.Add(this.txtPath1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnTest);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "xmlCompare";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOpenPath2;
        private System.Windows.Forms.TextBox txtPath2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnOpenPath1;
        private System.Windows.Forms.TextBox txtPath1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.Button btnExit;
    }
}

