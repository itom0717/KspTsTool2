namespace KspTsTool2.Forms
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
        protected override void Dispose( bool disposing )
        {
            if ( disposing && ( components != null ) )
            {
                components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ClientIdTextBox = new System.Windows.Forms.TextBox();
            this.IsMachineTranslationCheckBox = new System.Windows.Forms.CheckBox();
            this.ClientSecretTextBox = new System.Windows.Forms.TextBox();
            this.Label3 = new System.Windows.Forms.Label();
            this.IDLabel = new System.Windows.Forms.Label();
            this.SaveButton = new System.Windows.Forms.Button();
            this.CloseButton = new System.Windows.Forms.Button();
            this.TranslationGroupBox = new System.Windows.Forms.GroupBox();
            this.TranslationGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // ClientIdTextBox
            // 
            this.ClientIdTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ClientIdTextBox.Location = new System.Drawing.Point(90, 40);
            this.ClientIdTextBox.Name = "ClientIdTextBox";
            this.ClientIdTextBox.Size = new System.Drawing.Size(310, 19);
            this.ClientIdTextBox.TabIndex = 2;
            // 
            // IsMachineTranslationCheckBox
            // 
            this.IsMachineTranslationCheckBox.AutoSize = true;
            this.IsMachineTranslationCheckBox.Location = new System.Drawing.Point(19, 18);
            this.IsMachineTranslationCheckBox.Name = "IsMachineTranslationCheckBox";
            this.IsMachineTranslationCheckBox.Size = new System.Drawing.Size(311, 16);
            this.IsMachineTranslationCheckBox.TabIndex = 0;
            this.IsMachineTranslationCheckBox.Text = "Microsoft Translator API を使用して日本語へ翻訳を行う。";
            this.IsMachineTranslationCheckBox.UseVisualStyleBackColor = true;
            this.IsMachineTranslationCheckBox.CheckedChanged += new System.EventHandler(this.IsMachineTranslationCheckBox_CheckedChanged);
            // 
            // ClientSecretTextBox
            // 
            this.ClientSecretTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ClientSecretTextBox.Location = new System.Drawing.Point(90, 65);
            this.ClientSecretTextBox.Name = "ClientSecretTextBox";
            this.ClientSecretTextBox.Size = new System.Drawing.Size(310, 19);
            this.ClientSecretTextBox.TabIndex = 4;
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(17, 43);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(67, 12);
            this.Label3.TabIndex = 1;
            this.Label3.Text = "クライアントID";
            // 
            // IDLabel
            // 
            this.IDLabel.AutoSize = true;
            this.IDLabel.Location = new System.Drawing.Point(17, 68);
            this.IDLabel.Name = "IDLabel";
            this.IDLabel.Size = new System.Drawing.Size(63, 12);
            this.IDLabel.TabIndex = 3;
            this.IDLabel.Text = "顧客の秘密";
            // 
            // SaveButton
            // 
            this.SaveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveButton.Location = new System.Drawing.Point(262, 121);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(75, 23);
            this.SaveButton.TabIndex = 1;
            this.SaveButton.Text = "保存";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // CloseButton
            // 
            this.CloseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CloseButton.Location = new System.Drawing.Point(343, 121);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(75, 23);
            this.CloseButton.TabIndex = 2;
            this.CloseButton.Text = "キャンセル";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // TranslationGroupBox
            // 
            this.TranslationGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TranslationGroupBox.Controls.Add(this.IsMachineTranslationCheckBox);
            this.TranslationGroupBox.Controls.Add(this.ClientIdTextBox);
            this.TranslationGroupBox.Controls.Add(this.ClientSecretTextBox);
            this.TranslationGroupBox.Controls.Add(this.IDLabel);
            this.TranslationGroupBox.Controls.Add(this.Label3);
            this.TranslationGroupBox.Location = new System.Drawing.Point(12, 12);
            this.TranslationGroupBox.Name = "TranslationGroupBox";
            this.TranslationGroupBox.Size = new System.Drawing.Size(406, 100);
            this.TranslationGroupBox.TabIndex = 0;
            this.TranslationGroupBox.TabStop = false;
            this.TranslationGroupBox.Text = "自動翻訳設定";
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(430, 156);
            this.Controls.Add(this.TranslationGroupBox);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.CloseButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "設定";
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.TranslationGroupBox.ResumeLayout(false);
            this.TranslationGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.TextBox ClientIdTextBox;
        internal System.Windows.Forms.CheckBox IsMachineTranslationCheckBox;
        internal System.Windows.Forms.TextBox ClientSecretTextBox;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.Label IDLabel;
        internal System.Windows.Forms.Button SaveButton;
        internal System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.GroupBox TranslationGroupBox;
    }
}