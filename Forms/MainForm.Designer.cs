namespace KspTsTool2.Forms
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.CloseButton = new System.Windows.Forms.Button();
            this.TargetFolderLabel = new System.Windows.Forms.Label();
            this.TargetFolderComboBox = new System.Windows.Forms.ComboBox();
            this.SelectTargetFolderButton = new System.Windows.Forms.Button();
            this.OpenTargetFolderButton = new System.Windows.Forms.Button();
            this.SaveFolderNameLabel = new System.Windows.Forms.Label();
            this.SaveFolderNameTextBox = new System.Windows.Forms.TextBox();
            this.TranslationGroupBox = new System.Windows.Forms.GroupBox();
            this.IsAfterOpenFolderCheckBox = new System.Windows.Forms.CheckBox();
            this.CancelTranslationButton = new System.Windows.Forms.Button();
            this.TranslationButton = new System.Windows.Forms.Button();
            this.LogGroupBox = new System.Windows.Forms.GroupBox();
            this.IsSaveLogFileCheckBox = new System.Windows.Forms.CheckBox();
            this.LogMessageListBox = new System.Windows.Forms.ListBox();
            this.TranslationProgressBar = new System.Windows.Forms.ProgressBar();
            this.SettingButton = new System.Windows.Forms.Button();
            this.TranslationBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.ToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.ImportFileButton = new System.Windows.Forms.Button();
            this.TranslationGroupBox.SuspendLayout();
            this.LogGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // CloseButton
            // 
            this.CloseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CloseButton.Location = new System.Drawing.Point(799, 475);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(81, 23);
            this.CloseButton.TabIndex = 4;
            this.CloseButton.Text = "閉じる";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // TargetFolderLabel
            // 
            this.TargetFolderLabel.AutoSize = true;
            this.TargetFolderLabel.Location = new System.Drawing.Point(18, 24);
            this.TargetFolderLabel.Name = "TargetFolderLabel";
            this.TargetFolderLabel.Size = new System.Drawing.Size(188, 12);
            this.TargetFolderLabel.TabIndex = 0;
            this.TargetFolderLabel.Text = "処理対象フォルダ（GameDataフォルダ）";
            // 
            // TargetFolderComboBox
            // 
            this.TargetFolderComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TargetFolderComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TargetFolderComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.TargetFolderComboBox.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TargetFolderComboBox.FormattingEnabled = true;
            this.TargetFolderComboBox.Location = new System.Drawing.Point(35, 39);
            this.TargetFolderComboBox.Name = "TargetFolderComboBox";
            this.TargetFolderComboBox.Size = new System.Drawing.Size(658, 23);
            this.TargetFolderComboBox.Sorted = true;
            this.TargetFolderComboBox.TabIndex = 1;
            this.TargetFolderComboBox.Enter += new System.EventHandler(this.TargetFolderComboBox_Enter);
            this.TargetFolderComboBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TargetFolderComboBox_KeyDown);
            this.TargetFolderComboBox.Leave += new System.EventHandler(this.TargetFolderComboBox_Leave);
            // 
            // SelectTargetFolderButton
            // 
            this.SelectTargetFolderButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SelectTargetFolderButton.Location = new System.Drawing.Point(712, 40);
            this.SelectTargetFolderButton.Name = "SelectTargetFolderButton";
            this.SelectTargetFolderButton.Size = new System.Drawing.Size(82, 23);
            this.SelectTargetFolderButton.TabIndex = 2;
            this.SelectTargetFolderButton.Text = "フォルダ選択";
            this.SelectTargetFolderButton.UseVisualStyleBackColor = true;
            this.SelectTargetFolderButton.Click += new System.EventHandler(this.SelectTargetFolderButton_Click);
            // 
            // OpenTargetFolderButton
            // 
            this.OpenTargetFolderButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.OpenTargetFolderButton.Location = new System.Drawing.Point(800, 40);
            this.OpenTargetFolderButton.Name = "OpenTargetFolderButton";
            this.OpenTargetFolderButton.Size = new System.Drawing.Size(58, 23);
            this.OpenTargetFolderButton.TabIndex = 3;
            this.OpenTargetFolderButton.Text = "開く";
            this.OpenTargetFolderButton.UseVisualStyleBackColor = true;
            this.OpenTargetFolderButton.Click += new System.EventHandler(this.OpenTargetFolderButton_Click);
            // 
            // SaveFolderNameLabel
            // 
            this.SaveFolderNameLabel.AutoSize = true;
            this.SaveFolderNameLabel.Location = new System.Drawing.Point(18, 69);
            this.SaveFolderNameLabel.Name = "SaveFolderNameLabel";
            this.SaveFolderNameLabel.Size = new System.Drawing.Size(334, 12);
            this.SaveFolderNameLabel.TabIndex = 4;
            this.SaveFolderNameLabel.Text = "翻訳済みデータ保存フォルダ名（GameDataフォルダ内に作成されます）";
            // 
            // SaveFolderNameTextBox
            // 
            this.SaveFolderNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveFolderNameTextBox.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.SaveFolderNameTextBox.Location = new System.Drawing.Point(35, 84);
            this.SaveFolderNameTextBox.Name = "SaveFolderNameTextBox";
            this.SaveFolderNameTextBox.Size = new System.Drawing.Size(658, 22);
            this.SaveFolderNameTextBox.TabIndex = 5;
            this.SaveFolderNameTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.SaveFolderNameTextBox_Validating);
            // 
            // TranslationGroupBox
            // 
            this.TranslationGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TranslationGroupBox.Controls.Add(this.IsAfterOpenFolderCheckBox);
            this.TranslationGroupBox.Controls.Add(this.CancelTranslationButton);
            this.TranslationGroupBox.Controls.Add(this.TranslationButton);
            this.TranslationGroupBox.Controls.Add(this.TargetFolderLabel);
            this.TranslationGroupBox.Controls.Add(this.SaveFolderNameTextBox);
            this.TranslationGroupBox.Controls.Add(this.TargetFolderComboBox);
            this.TranslationGroupBox.Controls.Add(this.SaveFolderNameLabel);
            this.TranslationGroupBox.Controls.Add(this.SelectTargetFolderButton);
            this.TranslationGroupBox.Controls.Add(this.OpenTargetFolderButton);
            this.TranslationGroupBox.Location = new System.Drawing.Point(15, 3);
            this.TranslationGroupBox.Name = "TranslationGroupBox";
            this.TranslationGroupBox.Size = new System.Drawing.Size(865, 139);
            this.TranslationGroupBox.TabIndex = 0;
            this.TranslationGroupBox.TabStop = false;
            // 
            // IsAfterOpenFolderCheckBox
            // 
            this.IsAfterOpenFolderCheckBox.AutoSize = true;
            this.IsAfterOpenFolderCheckBox.Location = new System.Drawing.Point(50, 112);
            this.IsAfterOpenFolderCheckBox.Name = "IsAfterOpenFolderCheckBox";
            this.IsAfterOpenFolderCheckBox.Size = new System.Drawing.Size(179, 16);
            this.IsAfterOpenFolderCheckBox.TabIndex = 6;
            this.IsAfterOpenFolderCheckBox.Text = "処理終了後に保存フォルダを開く";
            this.IsAfterOpenFolderCheckBox.UseVisualStyleBackColor = true;
            // 
            // CancelTranslationButton
            // 
            this.CancelTranslationButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CancelTranslationButton.ForeColor = System.Drawing.Color.Red;
            this.CancelTranslationButton.Location = new System.Drawing.Point(712, 79);
            this.CancelTranslationButton.Name = "CancelTranslationButton";
            this.CancelTranslationButton.Size = new System.Drawing.Size(146, 23);
            this.CancelTranslationButton.TabIndex = 7;
            this.CancelTranslationButton.Text = "処理中止";
            this.CancelTranslationButton.UseVisualStyleBackColor = true;
            this.CancelTranslationButton.Click += new System.EventHandler(this.CancelTranslationButton_Click);
            // 
            // TranslationButton
            // 
            this.TranslationButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TranslationButton.ForeColor = System.Drawing.Color.Blue;
            this.TranslationButton.Location = new System.Drawing.Point(712, 108);
            this.TranslationButton.Name = "TranslationButton";
            this.TranslationButton.Size = new System.Drawing.Size(146, 23);
            this.TranslationButton.TabIndex = 8;
            this.TranslationButton.Text = "処理実行";
            this.TranslationButton.UseVisualStyleBackColor = true;
            this.TranslationButton.Click += new System.EventHandler(this.TranslationButton_Click);
            // 
            // LogGroupBox
            // 
            this.LogGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LogGroupBox.Controls.Add(this.IsSaveLogFileCheckBox);
            this.LogGroupBox.Controls.Add(this.LogMessageListBox);
            this.LogGroupBox.Controls.Add(this.TranslationProgressBar);
            this.LogGroupBox.Location = new System.Drawing.Point(15, 148);
            this.LogGroupBox.Name = "LogGroupBox";
            this.LogGroupBox.Size = new System.Drawing.Size(865, 321);
            this.LogGroupBox.TabIndex = 1;
            this.LogGroupBox.TabStop = false;
            this.LogGroupBox.Text = "処理ログ";
            // 
            // IsSaveLogFileCheckBox
            // 
            this.IsSaveLogFileCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.IsSaveLogFileCheckBox.AutoSize = true;
            this.IsSaveLogFileCheckBox.Location = new System.Drawing.Point(6, 299);
            this.IsSaveLogFileCheckBox.Name = "IsSaveLogFileCheckBox";
            this.IsSaveLogFileCheckBox.Size = new System.Drawing.Size(128, 16);
            this.IsSaveLogFileCheckBox.TabIndex = 2;
            this.IsSaveLogFileCheckBox.Text = "ログファイルを保存する";
            this.IsSaveLogFileCheckBox.UseVisualStyleBackColor = true;
            // 
            // LogMessageListBox
            // 
            this.LogMessageListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LogMessageListBox.FormattingEnabled = true;
            this.LogMessageListBox.ItemHeight = 12;
            this.LogMessageListBox.Location = new System.Drawing.Point(6, 47);
            this.LogMessageListBox.Name = "LogMessageListBox";
            this.LogMessageListBox.Size = new System.Drawing.Size(853, 244);
            this.LogMessageListBox.TabIndex = 1;
            // 
            // TranslationProgressBar
            // 
            this.TranslationProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TranslationProgressBar.Location = new System.Drawing.Point(5, 18);
            this.TranslationProgressBar.Name = "TranslationProgressBar";
            this.TranslationProgressBar.Size = new System.Drawing.Size(853, 23);
            this.TranslationProgressBar.TabIndex = 0;
            // 
            // SettingButton
            // 
            this.SettingButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SettingButton.Location = new System.Drawing.Point(712, 475);
            this.SettingButton.Name = "SettingButton";
            this.SettingButton.Size = new System.Drawing.Size(81, 23);
            this.SettingButton.TabIndex = 3;
            this.SettingButton.Text = "翻訳設定";
            this.SettingButton.UseVisualStyleBackColor = true;
            this.SettingButton.Click += new System.EventHandler(this.SettingButton_Click);
            // 
            // TranslationBackgroundWorker
            // 
            this.TranslationBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.TranslationBackgroundWorker_DoWork);
            this.TranslationBackgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.TranslationBackgroundWorker_ProgressChanged);
            this.TranslationBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.TranslationBackgroundWorker_RunWorkerCompleted);
            // 
            // ImportFileButton
            // 
            this.ImportFileButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ImportFileButton.Location = new System.Drawing.Point(21, 475);
            this.ImportFileButton.Name = "ImportFileButton";
            this.ImportFileButton.Size = new System.Drawing.Size(111, 23);
            this.ImportFileButton.TabIndex = 2;
            this.ImportFileButton.Text = "翻訳ファイル読込";
            this.ImportFileButton.UseVisualStyleBackColor = true;
            this.ImportFileButton.Click += new System.EventHandler(this.ImportFileButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(892, 510);
            this.Controls.Add(this.ImportFileButton);
            this.Controls.Add(this.SettingButton);
            this.Controls.Add(this.LogGroupBox);
            this.Controls.Add(this.TranslationGroupBox);
            this.Controls.Add(this.CloseButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(620, 380);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AppName";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.TranslationGroupBox.ResumeLayout(false);
            this.TranslationGroupBox.PerformLayout();
            this.LogGroupBox.ResumeLayout(false);
            this.LogGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.Label TargetFolderLabel;
        private System.Windows.Forms.ComboBox TargetFolderComboBox;
        private System.Windows.Forms.Button SelectTargetFolderButton;
        private System.Windows.Forms.Button OpenTargetFolderButton;
        private System.Windows.Forms.Label SaveFolderNameLabel;
        private System.Windows.Forms.TextBox SaveFolderNameTextBox;
        private System.Windows.Forms.GroupBox TranslationGroupBox;
        private System.Windows.Forms.Button TranslationButton;
        private System.Windows.Forms.GroupBox LogGroupBox;
        private System.Windows.Forms.Button CancelTranslationButton;
        internal System.Windows.Forms.Button SettingButton;
        private System.ComponentModel.BackgroundWorker TranslationBackgroundWorker;
        private System.Windows.Forms.CheckBox IsAfterOpenFolderCheckBox;
        private System.Windows.Forms.ProgressBar TranslationProgressBar;
        private System.Windows.Forms.ToolTip ToolTip;
        private System.Windows.Forms.ListBox LogMessageListBox;
        private System.Windows.Forms.CheckBox IsSaveLogFileCheckBox;
        internal System.Windows.Forms.Button ImportFileButton;
    }
}