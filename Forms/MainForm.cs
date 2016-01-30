using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;


namespace KspTsTool2.Forms
{
    /// <summary>
    /// メインフォーム
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// 翻訳済みデータ保存フォルダ名の初期値
        /// </summary>
        static string DefaultSaveFolderName = "@toJapanese";

        /// <summary>
        /// インポートフォルダ記憶用
        /// </summary>
        private string ImportTranslationFilePath { get; set; } = "";



        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Loadイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load( object sender , EventArgs e )
        {

            ///旧バージョンの設定値を引き継ぐ
            if ( Properties.Settings.Default.UpdateRequired )
            {
                Properties.Settings.Default.Upgrade();
                Properties.Settings.Default.UpdateRequired = false;
                Properties.Settings.Default.Save();
            }

            //バージョン取得
            System.Diagnostics.FileVersionInfo fvInfo = System.Diagnostics.FileVersionInfo.GetVersionInfo(
                                            System.Reflection.Assembly.GetExecutingAssembly().Location
                                            );
            //フォームタイトル設定
            this.Text = fvInfo.ProductName + " - Ver." + fvInfo.ProductVersion;

            //キャンセルボタン設定
            //処理実行と同じ場所に設定し、非表示にする
            this.CancelTranslationButton.Top = this.TranslationButton.Top;
            this.CancelTranslationButton.Left = this.TranslationButton.Left;
            this.CancelTranslationButton.Width = this.TranslationButton.Width;
            this.CancelTranslationButton.Height = this.TranslationButton.Height;
            this.CancelTranslationButton.Visible = false;

            this.LogMessageListBox.Items.Clear();

            //プログレスバー初期設定
            this.TranslationProgressBar.Value = 0;
            this.TranslationProgressBar.Minimum = 0;
            this.TranslationProgressBar.Maximum = 100;



            //処理後にフォルダを開く
            this.IsAfterOpenFolderCheckBox.Checked = Properties.Settings.Default.IsAfterOpenFolder;

            //初期値（前回の値)を設定
            //保存フォルダ名
            this.SaveFolderNameTextBox.Text = this.ConfirmationSaveFolderName( Properties.Settings.Default.SaveFolderName );

            //コンボボックスへセットする
            string[] paths = Properties.Settings.Default.GameDataPathList.Split('\t');
            foreach ( string path in paths )
            {
                if ( !path.Equals( "" ) )
                {
                    this.TargetFolderComboBox.Items.Add( path );
                }
            }
            if ( this.TargetFolderComboBox.Items.Count > Properties.Settings.Default.LastSelectedGameDataPathIndex
                && Properties.Settings.Default.LastSelectedGameDataPathIndex >= 0 )
            {
                this.TargetFolderComboBox.SelectedIndex = Properties.Settings.Default.LastSelectedGameDataPathIndex;
            }
            else if ( this.TargetFolderComboBox.Items.Count > 0 )
            {
                this.TargetFolderComboBox.SelectedIndex = 1;
            }
            else
            {
                this.TargetFolderComboBox.SelectedIndex = -1;
            }


            //ログファイル保存
            this.IsSaveLogFileCheckBox.Checked = Properties.Settings.Default.IsSaveLogFile;

            //ボタン類のコントロール有効/無効切り替え
            this.ChangeEnabledControl( true );

            //閉じるボタンを選択
            this.CloseButton.Select();
        }

        /// <summary>
        /// FormClosedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_FormClosed( object sender , FormClosedEventArgs e )
        {
            //設定値を記憶
            Properties.Settings.Default.SaveFolderName = this.SaveFolderNameTextBox.Text;


            string pathList = "";
            foreach ( string path in this.TargetFolderComboBox.Items )
            {
                if ( !path.Equals( "" ) )
                {
                    if ( !pathList.Equals( "" ) )
                    {
                        pathList += '\t';
                    }
                    pathList += path;
                }
            }
            Properties.Settings.Default.GameDataPathList = pathList;
            Properties.Settings.Default.LastSelectedGameDataPathIndex = this.TargetFolderComboBox.SelectedIndex;

            Properties.Settings.Default.IsAfterOpenFolder = this.IsAfterOpenFolderCheckBox.Checked;


            //ログファイル保存
            Properties.Settings.Default.IsSaveLogFile = this.IsSaveLogFileCheckBox.Checked;


            //設定値保存
            Properties.Settings.Default.Save();
        }

        /// <summary>
        /// FormClosingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_FormClosing( object sender , FormClosingEventArgs e )
        {
            //バックグラウンド処理中であれば中止確認する
            if ( this.TranslationBackgroundWorker.IsBusy )
            {
                e.Cancel = !this.ConfirmCancel();
            }
        }

        /// <summary>
        /// 閉じるボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseButton_Click( object sender , EventArgs e )
        {
            this.Close();
        }

        /// <summary>
        /// 設定ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SettingButton_Click( object sender , EventArgs e )
        {
            var form = new Forms.SettingsForm();
            form.ShowDialog();
        }

        /// <summary>
        ///  翻訳済みデータ保存フォルダ名テキスト入力チェック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveFolderNameTextBox_Validating( object sender , CancelEventArgs e )
        {
            var textBox = (TextBox)sender;
            string saveFolder =  textBox.Text;

            //入力チェック
            string newSaveFolder= this.ConfirmationSaveFolderName( saveFolder );

            if ( !newSaveFolder.Equals( saveFolder ) )
            {
                //値が変更されたらデータを変更しておく
                textBox.Text = newSaveFolder;
            }
        }

        /// <summary>
        /// 翻訳済みデータ保存フォルダ名テキストのチェック＆加工
        /// </summary>
        /// <param name="saveFolder"></param>
        /// <returns></returns>
        private string ConfirmationSaveFolderName( string saveFolder )
        {
            //禁止文字を削除
            saveFolder = Common.File.ChangeInvalidFileName( saveFolder , "" );

            //前後のスペースを消す
            saveFolder = saveFolder.Trim();

            if ( saveFolder.Equals( "" ) )
            {
                //空欄の場合は初期値
                saveFolder = DefaultSaveFolderName;
            }

            return saveFolder;
        }



        /// <summary>
        /// 処理対象フォルダコンボボックスにフォーカスが移った
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TargetFolderComboBox_Enter( object sender , EventArgs e )
        {
            this.ToolTip.SetToolTip( this.TargetFolderComboBox , "Deleteキーで現在選択されている処理対象フォルダを一覧から消去します。" );
        }

        /// <summary>
        /// 処理対象フォルダコンボボックスのフォーカスがなくなった
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TargetFolderComboBox_Leave( object sender , EventArgs e )
        {
            this.ToolTip.SetToolTip( this.TargetFolderComboBox , "" );
        }

        /// <summary>
        /// 処理対象フォルダを選択するボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectTargetFolderButton_Click( object sender , EventArgs e )
        {
            //フォルダ選択
            //FolderBrowserDialogクラスのインスタンスを作成
            var folderBrowserDialog =new FolderBrowserDialog();

            //上部に表示する説明テキストを指定する
            folderBrowserDialog.Description = "処理対象のGameDataフォルダを指定してください。";

            //ルートフォルダを指定する
            folderBrowserDialog.RootFolder = Environment.SpecialFolder.Desktop;

            //最初に選択するフォルダを指定する
            //RootFolder以下にあるフォルダである必要がある
            folderBrowserDialog.SelectedPath = this.TargetFolderComboBox.Text;

            //ユーザーが新しいフォルダを作成できるようにする
            folderBrowserDialog.ShowNewFolderButton = false;

            //ダイアログを表示する
            if ( folderBrowserDialog.ShowDialog( this ) == DialogResult.OK )
            {
                //選択された
                string tgtPath = Common.File.AddDirectorySeparator( folderBrowserDialog.SelectedPath );

                //コンボボックスへセットする
                this.AddTargetFolderComboBoxList( tgtPath );
            }

        }

        /// <summary>
        /// 処理対象フォルダを開くボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenTargetFolderButton_Click( object sender , EventArgs e )
        {
            //対象フォルダ
            string tgtPath = this.TargetFolderComboBox.Text;


            //未選択の場合
            if ( tgtPath.Equals( "" ) )
            {
                this.DispErrorMessage( "フォルダを選択してください。" );
                return;
            }

            //存在しないパスの場合
            tgtPath = Common.File.AddDirectorySeparator( tgtPath );
            if ( !Common.File.ExistsDirectory( tgtPath ) )
            {
                this.DispErrorMessage( "存在しないフォルダです。" );
                return;
            }

            try
            {
                //フォルダを開く
                System.Diagnostics.Process.Start( tgtPath );
                return;
            }
            catch
            {
                this.DispErrorMessage( "フォルダを開くことができません。" ,
                                       true );
                return;
            }
        }



        /// <summary>
        /// 翻訳ファイル読み込みボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImportFileButton_Click( object sender , EventArgs e )
        {
            string filename = "";

            //記憶されているフォルダのチェック
            if ( this.ImportTranslationFilePath.Equals( "" )
                || !Common.File.ExistsDirectory( this.ImportTranslationFilePath ) )
            {
                //未設定or存在しないので、保存先フォルダを初期値として設定
                this.ImportTranslationFilePath = Common.File.CombinePath( this.TargetFolderComboBox.Text.Trim() , this.SaveFolderNameTextBox.Text.Trim() );
                if ( !Common.File.ExistsDirectory( this.ImportTranslationFilePath ) )
                {
                    //存在しない場合はデスクトップ
                    this.ImportTranslationFilePath = Common.File.GetDesktopDirectory();
                }

            }

            //OpenFileDialogクラスのインスタンスを作成
            var openFileDialog = new OpenFileDialog();
            openFileDialog.FileName = "";

            //はじめに表示されるフォルダを指定する
            openFileDialog.InitialDirectory = this.ImportTranslationFilePath;

            //[ファイルの種類]に表示される選択肢を指定
            openFileDialog.Filter = "ModuleManager用cfgファイル|*.cfg";

            //[ファイルの種類]
            openFileDialog.FilterIndex = 1;

            //タイトルを設定する
            openFileDialog.Title = "翻訳用ModuleManagerのcfgファイルを選択してください。";

            //ダイアログボックスを閉じる前に現在のディレクトリを復元するようにする
            openFileDialog.RestoreDirectory = true;

            //存在しないファイルの名前が指定されたとき警告を表示する
            openFileDialog.CheckFileExists = true;

            //存在しないパスが指定されたとき警告を表示する
            openFileDialog.CheckPathExists = true;

            //ダイアログを表示する
            if ( openFileDialog.ShowDialog() == DialogResult.OK )
            {
                //OKボタンがクリックされた
                filename = openFileDialog.FileName;

                //フォルダを記憶
                this.ImportTranslationFilePath = Common.File.GetDirectoryName( filename );
            }
            if ( filename.Equals( "" ) )
            {
                //キャンセル
                return;
            }


            try
            {
                //翻訳データベース
                var translationDataBase = new Translation.Database.TranslationDataBase();

                //翻訳データベースへ取り込み
                int importCount = translationDataBase.ImportTranslationFile(filename);

                translationDataBase = null;

                if ( importCount == 0 )
                {
                    this.DispErrorMessage( string.Format( "取り込むデータはありません。" , importCount ) , false );
                }
                else
                {
                    this.DispErrorMessage( string.Format( "{0} 件取り込みました。" , importCount ) , false );
                }
            }
            catch ( Exception ex )
            {
                this.DispErrorMessage( ex.Message , true );
                return;
            }
        }


        /// <summary>
        /// 処理中止ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelTranslationButton_Click( object sender , EventArgs e )
        {
            //バックグラウンド処理中であれば中止確認する
            if ( this.TranslationBackgroundWorker.IsBusy )
            {
                this.ConfirmCancel();
            }
        }

        /// <summary>
        /// コントロールの有効/無効切り替え
        /// </summary>
        private void ChangeEnabledControl( bool isEnable )
        {
            this.TargetFolderComboBox.Enabled = isEnable;
            this.SelectTargetFolderButton.Enabled = isEnable;
            this.OpenTargetFolderButton.Enabled = isEnable;

            this.SaveFolderNameTextBox.Enabled = isEnable;

            this.TranslationButton.Enabled = isEnable;
            this.TranslationButton.Visible = isEnable;

            this.CancelTranslationButton.Enabled = !isEnable;
            this.CancelTranslationButton.Visible = !isEnable;

            this.SettingButton.Enabled = isEnable;
        }


        /// <summary>
        /// 中止処理確認＆中止設定
        /// </summary>
        /// <returns></returns>
        private bool ConfirmCancel()
        {
            if ( MessageBox.Show( "処理を中止しますか？" ,
                                 "" ,
                                 MessageBoxButtons.OKCancel ,
                                 MessageBoxIcon.Question ) != DialogResult.OK )
            {
                return false;
            }


            //中止設定
            if ( this.TranslationBackgroundWorker.IsBusy )
            {
                this.TranslationBackgroundWorker.CancelAsync();
                this.CancelTranslationButton.Enabled = false;
            }
            return true;
        }


        /// <summary>
        /// 処理対象コンボに選択パスを追加する
        /// </summary>
        /// <param name="tgtPath"></param>
        private void AddTargetFolderComboBoxList( string tgtPath )
        {

            //同じものがあれば選択させる
            for ( int i = 0; i < this.TargetFolderComboBox.Items.Count; i++ )
            {
                if ( this.TargetFolderComboBox.Items[i].Equals( tgtPath ) )
                {
                    //存在したため、この項目を選択させる
                    this.TargetFolderComboBox.SelectedIndex = i;
                    return;
                }
            }

            //存在しないので、追加
            this.TargetFolderComboBox.Items.Add( tgtPath );

            //選択状態にするため、再帰呼び出し
            this.AddTargetFolderComboBoxList( tgtPath );
        }


        /// <summary>
        /// 処理対象フォルダをリストから削除する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TargetFolderComboBox_KeyDown( object sender , KeyEventArgs e )
        {
            if ( e.KeyCode == Keys.Delete )//Deleteキーで削除
            {
                var combo = (ComboBox)sender;

                int index = combo.SelectedIndex;
                if ( index >= 0 )
                {
                    combo.Items.RemoveAt( index );
                    if ( combo.Items.Count > index )
                    {
                        combo.SelectedIndex = index;
                    }
                    else
                    {
                        combo.SelectedIndex = index - 1;
                    }
                }
            }
        }


        /// <summary>
        /// エラーメッセージなどを表示する
        /// </summary>
        private void DispErrorMessage( string errMsg ,
                                        bool isError = false )
        {
            MessageBox.Show( errMsg ,
                             "" ,
                             MessageBoxButtons.OK ,
                             ( isError ? MessageBoxIcon.Error : MessageBoxIcon.Information ) );
        }




        /// <summary>
        /// 設定データ受け渡し用
        /// </summary>
        private SettingsParameters SettingsParameters = null;


        /// <summary>
        /// 処理実行ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TranslationButton_Click( object sender , EventArgs e )
        {
            //設定値チェック

            //設定データ受け渡し用
            this.SettingsParameters = new SettingsParameters();

            //設定値取得
            try
            {
                {
                    //読み込みフォルダ
                    this.SettingsParameters.GameDataPath = this.TargetFolderComboBox.Text.Trim();
                    if ( this.SettingsParameters.GameDataPath.Equals( "" ) )
                    {
                        this.DispErrorMessage( "「処理対象フォルダ」を指定してください。" );
                        return;
                    }
                    else if ( !Common.File.ExistsDirectory( this.SettingsParameters.GameDataPath ) )
                    {
                        this.DispErrorMessage( "「処理対象フォルダ」で指定されたフォルダが見つかりません。" );
                        return;
                    }

                    //保存フォルダ
                    this.SettingsParameters.SaveFolderName = this.SaveFolderNameTextBox.Text.Trim();
                    if ( this.SettingsParameters.SaveFolderName.Equals( "" ) )
                    {
                        this.DispErrorMessage( "「保存フォルダ名」を指定してください。" );
                        return;
                    }
                    this.SettingsParameters.SavePath = Common.File.CombinePath( this.SettingsParameters.GameDataPath ,
                                                                                this.SettingsParameters.SaveFolderName );
                    if ( !Common.File.ExistsDirectory( this.SettingsParameters.SavePath ) )
                    {
                        //フォルダが存在しない時の確認
                        if ( MessageBox.Show( "保存先のフォルダが存在しません。\n「" + this.SettingsParameters.SaveFolderName + "」フォルダを作成しますか？" ,
                                             "" ,
                                             MessageBoxButtons.OKCancel ,
                                             MessageBoxIcon.Question ) != DialogResult.OK )
                        {
                            return;
                        }
                        Common.File.CreateDirectory( this.SettingsParameters.SavePath );

                    }
                    else if ( !Common.File.IsEmptyDirectory( this.SettingsParameters.SavePath ) )
                    {
                        //空欄でないための確認
                        if ( MessageBox.Show( "保存先のフォルダにファイルが存在します。\nファイルを上書きしますか？" ,
                                             "" ,
                                             MessageBoxButtons.OKCancel ,
                                             MessageBoxIcon.Question ) != DialogResult.OK )
                        {
                            return;
                        }

                    }

                    // 処理後に処理フォルダを開くか？
                    this.SettingsParameters.IsAfterOpenFolderCheckBox = this.IsAfterOpenFolderCheckBox.Checked;


                    //Microsoft Translator API クライアントID
                    this.SettingsParameters.MicrosoftTranslatorAPIClientId = Properties.Settings.Default.MicrosoftTranslatorAPIClientId;

                    //Microsoft Translator API 顧客の秘密
                    this.SettingsParameters.MicrosoftTranslatorAPIClientSecret = Properties.Settings.Default.MicrosoftTranslatorAPIClientSecret;

                    //Microsoft Translator API を使用して日本語へ翻訳を行か？
                    this.SettingsParameters.IsMachineTranslation = Properties.Settings.Default.IsMachineTranslation;
                    if ( this.SettingsParameters.MicrosoftTranslatorAPIClientId.Equals( "" )
                         || this.SettingsParameters.MicrosoftTranslatorAPIClientSecret.Equals( "" ) )
                    {
                        //Microsoft Translator API クライアントID または　顧客の秘密　が空欄の場合、自動翻訳は行わない
                        this.SettingsParameters.IsMachineTranslation = false;
                    }


                }



                //処理開始を確認
                if ( MessageBox.Show( "処理を実行しますか？" ,
                         "" ,
                         MessageBoxButtons.OKCancel ,
                         MessageBoxIcon.Question ) != DialogResult.OK )
                {
                    return;
                }



                //進行状況コントロールを初期化
                this.LogMessageListBox.Items.Clear();


                //プログレスバー初期設定
                this.TranslationProgressBar.Value = 0;
                this.TranslationProgressBar.Minimum = 0;
                this.TranslationProgressBar.Maximum = 100;

                //ボタン類を無効にして押せないようにする
                this.ChangeEnabledControl( false );

                //バックグラウンド処理を開始する
                this.TranslationBackgroundWorker.WorkerReportsProgress = true; //進行状況を表示する
                this.TranslationBackgroundWorker.WorkerSupportsCancellation = true; //キャンセル可能
                this.TranslationBackgroundWorker.RunWorkerAsync( this.SettingsParameters );//処理開始
            }
            catch ( Exception ex )
            {
                //エラー表示
                this.DispErrorMessage( ex.Message , true );
                this.Close();
            }
        }

        /// <summary>
        /// BackgroundWorker処理開始
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TranslationBackgroundWorker_DoWork( object sender , DoWorkEventArgs e )
        {
            //BackgroundWorker
            var bw = (BackgroundWorker)sender;

            //設定値
            var settingsParameters = (SettingsParameters)e.Argument;

            //翻訳データベースインスタンス
            Translation.Database.TranslationDataBase translationDataBase;
            if ( settingsParameters.IsMachineTranslation )
            {
                //自動翻訳あり
                translationDataBase
                    = new Translation.Database.TranslationDataBase( settingsParameters.MicrosoftTranslatorAPIClientId ,
                                                                    settingsParameters.MicrosoftTranslatorAPIClientSecret );
            }
            else
            {
                //自動翻訳なし
                translationDataBase
                    = new Translation.Database.TranslationDataBase();
            }

            //進行状況表示処理用処理定義
            var progressStatus = new ProgressStatus(bw);

            //処理開始
            try
            {

                //フォルダ数
                const string StatusTextFirstLevelDirectory = @"({0,3}/{1,3})-[ {2} ]";

                //ファイル数
                const string StatusTextCfgFile =   @" -- ({0,3}/{1,3})-[ {2} ]";

                //翻訳数
                const string StatusTextTranslateOne =   @" --- 翻訳処理";
                const string StatusTextTranslate =   @" --- 翻訳処理({0,3}/{1,3}) ";


                ConfigurationFile.ConfigurationFolder configurationFolder;
                ConfigurationFile.ConfigurationFile   configurationFile;


                //表示リセット
                progressStatus.Reset();
                progressStatus.DispStatus( @"処理開始" );

                //翻訳DB読込
                translationDataBase.Load();

                //第一階層のフォルダを列挙
                List<string> firstLevelDirectoryList
                    = Common.File.GetFolderList( settingsParameters.GameDataPath , "*.*" , false );

                //最大フォルダ件数設定
                progressStatus.FirstLevelDirectoryMaxCount = firstLevelDirectoryList.Count;

                //フォルダ分だけループ
                foreach ( string tgtDirectory in firstLevelDirectoryList )
                {

                    System.Threading.Thread.Sleep( 1 ); //wait
                    if ( bw.CancellationPending )//キャンセルされたか調べる
                    {
                        //キャンセルされた
                        e.Cancel = true;
                        return;
                    }


                    //フォルダ情報のインスタンス作成
                    configurationFolder = new ConfigurationFile.ConfigurationFolder();

                    //ディレクトリ名のみにする
                    configurationFolder.DirectoryName = Common.File.GetFileName( Common.File.DeleteDirectorySeparator( tgtDirectory ) );

                    //保存パス
                    configurationFolder.SavePath = settingsParameters.SavePath;


                    progressStatus.FirstLevelDirectoryNowCount++;
                    string msgFirstLevelDirectory =  String.Format( StatusTextFirstLevelDirectory ,
                                                             progressStatus.FirstLevelDirectoryNowCount ,
                                                             progressStatus.FirstLevelDirectoryMaxCount ,
                                                             configurationFolder.DirectoryName );


                    //対象外のフォルダ
                    if ( this.SettingsParameters.SaveFolderName.Equals( configurationFolder.DirectoryName ,
                                                                        StringComparison.CurrentCultureIgnoreCase ) )
                    {
                        //保存先フォルダは対象外なので、次のフォルダへ
                        progressStatus.DispStatus( msgFirstLevelDirectory + " --- 対象外フォルダ" );
                        progressStatus.DispStatus( "" );
                        continue;
                    }

                    //フォルダ内の*.cfgを列挙
                    List<string> cfgList = Common.File.GetFileList( tgtDirectory , "*.cfg" , true );
                    if ( cfgList.Count == 0 )
                    {
                        //ファイル無しは次のフォルダへ
                        progressStatus.DispStatus( msgFirstLevelDirectory + " --- cfgファイル無し" );
                        progressStatus.DispStatus( "" );
                        continue;
                    }


                    //ファイル数
                    progressStatus.CfgFileMaxCount = cfgList.Count;

                    //cfgファイルを１ファイルづつ処理する
                    int dataOrderParts = 1;
                    int dataOrderScienceDefs = 1;
                    foreach ( string cfgFile in cfgList )
                    {
                        System.Threading.Thread.Sleep( 1 ); //wait
                        if ( bw.CancellationPending ) //キャンセルされたか調べる
                        {
                            //キャンセルされた
                            e.Cancel = true;
                            return;
                        }


                        string cfgFilename =  cfgFile.Substring(  tgtDirectory.Length+1 ) ;

                        progressStatus.CfgFileNowCount++;
                        string msgCfgFile = msgFirstLevelDirectory + String.Format( StatusTextCfgFile  ,
                                                                                    progressStatus.CfgFileNowCount ,
                                                                                    progressStatus.CfgFileMaxCount ,
                                                                                    cfgFilename );


                        //cfgファイルを１ファイル
                        configurationFile = new ConfigurationFile.ConfigurationFile();

                        //cfgファイルを読み込んで解析
                        if ( !configurationFile.AnalysisCfgFile( cfgFile ) )
                        {
                            //データなしのため、次のファイルへ
                            progressStatus.DispStatus( msgCfgFile + " --- 翻訳テキスト無し" );
                            continue;
                        }

                        //データ追加
                        configurationFolder.Add( configurationFile );

                        //翻訳処理
                        progressStatus.TranslationMaxCount = configurationFile.TranslationMaxCount;
                        foreach ( ConfigurationFile.TextData.TextData textData in configurationFile.TextDataList )
                        {
                            foreach ( ConfigurationFile.TextData.TranslateText trText in textData.TranslateTextList )
                            {
                                System.Threading.Thread.Sleep( 1 ); //wait
                                //if ( bw.CancellationPending )//キャンセルされたか調べる
                                //{
                                //    //キャンセルされた
                                //    e.Cancel = true;
                                //    return;
                                //}


                                progressStatus.TranslationNowCount++;
                                if ( progressStatus.TranslationMaxCount <= 1 )
                                {
                                    string msgTranslate = String.Format( StatusTextTranslateOne );
                                    progressStatus.DispStatus( msgCfgFile + msgTranslate );
                                }
                                else
                                {
                                    string msgTranslate = String.Format( StatusTextTranslate ,
                                                               progressStatus.TranslationNowCount,
                                                               progressStatus.TranslationMaxCount);
                                    progressStatus.DispStatus( msgCfgFile + msgTranslate );
                                }


                                //翻訳処理
                                translationDataBase.Translate( configurationFolder.DirectoryName ,
                                                               cfgFilename ,
                                                               textData ,
                                                               trText ,
                                ( textData.DataType == ConfigurationFile.TextData.DataType.Part ? dataOrderParts++ : dataOrderScienceDefs++ ) );
                            }
                        }

                    }

                    //Module Manager用cfgファイルに保存する
                    //データがなければファイルは作成しない（すでに存在した場合はファイル削除）
                    configurationFolder.ExportModuleManagerCfgFile();

                    progressStatus.DispStatus( "" );
                }

            }
            finally
            {
                //データベース保存
                translationDataBase.Save();
            }
        }

        /// <summary>
        /// 進行状況表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TranslationBackgroundWorker_ProgressChanged( object sender , ProgressChangedEventArgs e )
        {
            var statusText = (ProgressStatus.StatusText)e.UserState;

            //プログレスバーの値を変更する
            if ( e.ProgressPercentage < this.TranslationProgressBar.Minimum )
            {
                this.TranslationProgressBar.Value = this.TranslationProgressBar.Minimum;
            }
            else if ( this.TranslationProgressBar.Maximum < e.ProgressPercentage )
            {
                this.TranslationProgressBar.Value = this.TranslationProgressBar.Maximum;
            }
            else
            {
                this.TranslationProgressBar.Value = e.ProgressPercentage;
            }


            //ログメッセージ
            if ( statusText.IsReset )
            {
                //リセット
                this.LogMessageListBox.Items.Clear();
            }
            else
            {
                //ログを追加
                if ( statusText.AddLogText != null )
                {
                    this.AppendLogText( statusText.AddLogText );
                }
            }

        }

        /// <summary>
        /// ログ追加
        /// </summary>
        /// <param name="addMessageText"></param>
        private void AppendLogText( string addMessageText )
        {

            if ( !addMessageText.Equals( "" ) )
            {
                string nowText = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                this.LogMessageListBox.Items.Add( nowText + " " + addMessageText );
                this.LogMessageListBox.TopIndex = this.LogMessageListBox.Items.Count - 1;
            }
            else
            {
                this.LogMessageListBox.Items.Add( "" );
            }
        }


        /// <summary>
        /// BackgroundWorker処理完了
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TranslationBackgroundWorker_RunWorkerCompleted( object sender , RunWorkerCompletedEventArgs e )
        {

            //無効にたボタン類をもとに戻す
            this.ChangeEnabledControl( true );

            if ( e.Error != null )
            {
                //エラー発生のため、エラー内容表示
                this.AppendLogText( "エラー発生" );
                this.AppendLogText( e.Error.Message );
                this.DispErrorMessage( e.Error.Message , true );
            }
            else if ( e.Cancelled )
            {
                //キャンセルされた
                this.AppendLogText( "処理中止" );
                //this.DispErrorMessage( "処理を中止しました。" , false );
            }
            else
            {
                //処理完了した。
                //フォルダを開く
                if ( this.SettingsParameters.IsAfterOpenFolderCheckBox )
                {
                    System.Diagnostics.Process.Start( this.SettingsParameters.SavePath );
                }

                this.TranslationProgressBar.Value = this.TranslationProgressBar.Maximum;
                this.AppendLogText( "処理終了" );
                //this.DispErrorMessage( "処理が終了しました。" , false );
            }



            if ( this.IsSaveLogFileCheckBox.Checked )
            {
                try
                {
                    //ログをテキストファイルへ保存する
                    string logFilename = Common.File.CombinePath (Common.File.GetApplicationDirectory(), "logfile.txt");
                    using ( var sw = new System.IO.StreamWriter( logFilename , false , System.Text.Encoding.UTF8 ) )
                    {
                        foreach ( string text in this.LogMessageListBox.Items )
                        {
                            sw.WriteLine( text );
                        }
                    }
                }
                catch ( System.Exception ex )
                {
                    this.DispErrorMessage( ex.Message , true );
                }
            }
        }

    }
}
