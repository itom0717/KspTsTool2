using System;
using System.Windows.Forms;

namespace KspTsTool2.Forms
{
    /// <summary>
    /// 設定フォーム
    /// </summary>
    public partial class SettingsForm : Form
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SettingsForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Loadイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SettingsForm_Load( object sender , EventArgs e )
        {
            //現在の値をコントロールに設定
            this.IsMachineTranslationCheckBox.Checked = Properties.Settings.Default.IsMachineTranslation;
            this.ClientIdTextBox.Text = Properties.Settings.Default.MicrosoftTranslatorAPIClientId;
            this.ClientSecretTextBox.Text = Properties.Settings.Default.MicrosoftTranslatorAPIClientSecret;

            //テキストボックスの有効無効切り替え
            this.ChangeEnabledControl( this.IsMachineTranslationCheckBox.Checked );
        }

        /// <summary>
        /// キャンセルボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseButton_Click( object sender , EventArgs e )
        {
            this.Close();
        }

        /// <summary>
        /// OKボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveButton_Click( object sender , EventArgs e )
        {

            //入力チェック
            if ( this.IsMachineTranslationCheckBox.Checked )
            {
                if ( this.ClientIdTextBox.Text.Trim().Equals( "" )
                    || this.ClientSecretTextBox.Text.Trim().Equals( "" ) )
                {
                    MessageBox.Show(
                        "Microsoft Translator API を使用して日本語へ翻訳を行う場合は、\n「クライアントID」と「顧客の秘密」を入力してください。",
                        "",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Asterisk );
                    return;
                }
            }

            //設定を記憶
            Properties.Settings.Default.IsMachineTranslation = this.IsMachineTranslationCheckBox.Checked;
            Properties.Settings.Default.MicrosoftTranslatorAPIClientId = this.ClientIdTextBox.Text.Trim();
            Properties.Settings.Default.MicrosoftTranslatorAPIClientSecret = this.ClientSecretTextBox.Text.Trim();

            //設定値保存
            Properties.Settings.Default.Save();

            //フォーム閉じる
            this.Close();
        }

        /// <summary>
        /// 自動翻訳のチェックボックスのチェックが変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IsMachineTranslationCheckBox_CheckedChanged( object sender , EventArgs e )
        {
            var checkBox = (CheckBox)sender;

            //テキストボックスの有効無効切り替え
            this.ChangeEnabledControl( checkBox.Checked );
        }


        /// <summary>
        /// コントロールの有効/無効切り替え
        /// </summary>
        private void ChangeEnabledControl( bool isEnable )
        {
            this.ClientIdTextBox.Enabled = isEnable;
            this.ClientSecretTextBox.Enabled = isEnable;
        }

    }
}
