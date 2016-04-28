using System.Collections.Generic;

namespace KspTsTool2.Forms
{

    /// <summary>
    /// 処理実行時の設定データ受け渡し用
    /// </summary>
    public class SettingsParameters
    {

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SettingsParameters()
        {
            ///旧バージョンの設定値を引き継ぐ
            if ( Properties.Settings.Default.UpdateRequired )
            {
                Properties.Settings.Default.Upgrade();
                Properties.Settings.Default.UpdateRequired = false;
                Properties.Settings.Default.Save();
            }
        }


        /// <summary>
        /// 読込フォルダ(処理実行時に使用)
        /// </summary>
        /// <returns></returns>
        public string GameDataPath { get; set; } = "";

        /// <summary>
        /// 保存先フォルダ(処理実行時に使用)
        /// </summary>
        /// <returns></returns>
        public string SavePath { get; set; } = "";



        /// <summary>
        /// 設定値保存
        /// </summary>
        public void Save()
        {
            //設定値保存
            Properties.Settings.Default.Save();
        }


        /// <summary>
        /// 保存先フォルダリスト
        /// </summary>
        public List<string> GameDataPathList
        {
            get
            {
                return new List<string>( Properties.Settings.Default.GameDataPathList.Split( '\t' ) );
            }
            set
            {
                Properties.Settings.Default.GameDataPathList = string.Join( "\t" , value.ToArray() );
            }
        }

        /// <summary>
        /// 前回の選択Index
        /// </summary>
        public int LastSelectedGameDataPathIndex
        {
            get
            {
                return Properties.Settings.Default.LastSelectedGameDataPathIndex;
            }
            set
            {
                Properties.Settings.Default.LastSelectedGameDataPathIndex = value;
            }
        }


        /// <summary>
        /// 保存先フォルダ名
        /// </summary>
        public string SaveFolderName
        {
            get
            {
                return Properties.Settings.Default.SaveFolderName;
            }
            set
            {
                Properties.Settings.Default.SaveFolderName = value;
            }
        }


        /// <summary>
        /// 処理後に処理フォルダを開くか？
        /// </summary>
        public bool IsAfterOpenFolder
        {
            get
            {
                return Properties.Settings.Default.IsAfterOpenFolder;
            }
            set
            {
                Properties.Settings.Default.IsAfterOpenFolder = value;
            }
        }


        /// <summary>
        /// ログファイルを保存するか？
        /// </summary>
        public bool IsSaveLogFile
        {
            get
            {
                return Properties.Settings.Default.IsSaveLogFile;
            }
            set
            {
                Properties.Settings.Default.IsSaveLogFile = value;
            }
        }


        /// <summary>
        ///自動翻訳を行うか
        /// </summary>
        public bool IsMachineTranslation
        {
            get
            {
                return Properties.Settings.Default.IsMachineTranslation;
            }
            set
            {
                Properties.Settings.Default.IsMachineTranslation = value;
            }
        }

        /// <summary>
        /// クライアントID
        /// </summary>
        /// <returns></returns>
        public string MicrosoftTranslatorAPIClientId
        {
            get
            {
                return Properties.Settings.Default.MicrosoftTranslatorAPIClientId; ;
            }
            set
            {
                Properties.Settings.Default.MicrosoftTranslatorAPIClientId = value.Trim();
            }
        }

        /// <summary>
        /// 顧客の秘密
        /// </summary>
        /// <returns></returns>
        public string MicrosoftTranslatorAPIClientSecret
        {
            get
            {
                return Properties.Settings.Default.MicrosoftTranslatorAPIClientSecret;
            }
            set
            {
                Properties.Settings.Default.MicrosoftTranslatorAPIClientSecret = value.Trim();
            }
        }










    }
}
