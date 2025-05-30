namespace AdminCreator
{
    partial class Form1
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
            groupBox1 = new GroupBox();
            CreatBtn = new Button();
            label3 = new Label();
            label2 = new Label();
            mailLabel = new Label();
            confirmTxt = new TextBox();
            pswTxt = new TextBox();
            emailTxt = new TextBox();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(CreatBtn);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(mailLabel);
            groupBox1.Controls.Add(confirmTxt);
            groupBox1.Controls.Add(pswTxt);
            groupBox1.Controls.Add(emailTxt);
            groupBox1.Location = new Point(12, 35);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(460, 264);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Panel";
            // 
            // CreatBtn
            // 
            CreatBtn.Location = new Point(104, 213);
            CreatBtn.Name = "CreatBtn";
            CreatBtn.Size = new Size(166, 23);
            CreatBtn.TabIndex = 6;
            CreatBtn.Text = "Create Account";
            CreatBtn.UseVisualStyleBackColor = true;
            CreatBtn.Click += CreatBtn_ClickAsync;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 172);
            label3.Name = "label3";
            label3.Size = new Size(78, 15);
            label3.TabIndex = 5;
            label3.Text = "Confirm Psw:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(59, 128);
            label2.Name = "label2";
            label2.Size = new Size(31, 15);
            label2.TabIndex = 4;
            label2.Text = "Psw:";
            // 
            // mailLabel
            // 
            mailLabel.AutoSize = true;
            mailLabel.Location = new Point(59, 72);
            mailLabel.Name = "mailLabel";
            mailLabel.Size = new Size(39, 15);
            mailLabel.TabIndex = 3;
            mailLabel.Text = "Email:";
            // 
            // confirmTxt
            // 
            confirmTxt.Location = new Point(103, 169);
            confirmTxt.Name = "confirmTxt";
            confirmTxt.PasswordChar = '*';
            confirmTxt.Size = new Size(167, 23);
            confirmTxt.TabIndex = 2;
            // 
            // pswTxt
            // 
            pswTxt.Location = new Point(103, 120);
            pswTxt.Name = "pswTxt";
            pswTxt.PasswordChar = '*';
            pswTxt.Size = new Size(167, 23);
            pswTxt.TabIndex = 1;
            // 
            // emailTxt
            // 
            emailTxt.Location = new Point(103, 69);
            emailTxt.Name = "emailTxt";
            emailTxt.Size = new Size(167, 23);
            emailTxt.TabIndex = 0;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(484, 311);
            Controls.Add(groupBox1);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form1";
            Text = "Admin Creator Tool";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private Button CreatBtn;
        private Label label3;
        private Label label2;
        private Label mailLabel;
        private TextBox confirmTxt;
        private TextBox pswTxt;
        private TextBox emailTxt;
    }
}
