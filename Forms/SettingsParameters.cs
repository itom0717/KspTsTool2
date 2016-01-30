namespace KspTsTool2.Forms
{

    /// <summary>
    /// 処理実行時の設定データ受け渡し用
    /// </summary>
    class SettingsParameters
    {
        /// <summary>
        /// 読込フォルダ
        /// </summary>
        /// <returns></returns>
        public string GameDataPath { get; set; } = "";

        /// <summary>
        /// 保存先フォルダ名
        /// </summary>
        public string SaveFolderName { get; set; } = "";

        /// <summary>
        /// 保存先フォルダ
        /// </summary>
        /// <returns></returns>
        public string SavePath { get; set; } = "";

        /// <summary>
        /// 処理後に処理フォルダを開くか？
        /// </summary>
        public bool IsAfterOpenFolderCheckBox { get; set; } = false;

        /// <summary>
        ///自動翻訳を行うか
        /// </summary>
        public bool IsMachineTranslation { get; set; } = false;

        /// <summary>
        /// クライアントID
        /// </summary>
        /// <returns></returns>
        public string MicrosoftTranslatorAPIClientId { get; set; } = "";

        /// <summary>
        /// 顧客の秘密
        /// </summary>
        /// <returns></returns>
        public  string MicrosoftTranslatorAPIClientSecret { get; set; } = "";
    }
}
