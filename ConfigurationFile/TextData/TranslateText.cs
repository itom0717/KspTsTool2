namespace KspTsTool2.ConfigurationFile.TextData
{

    /// <summary>
    /// 翻訳するテキストデータ
    /// </summary>
    public class TranslateText
    {


        /// <summary>
        /// Resultテキスト/Resultインデックス(サイセンスレポート時に使用)
        /// </summary>
        public ScienceDefsResult Result = new ScienceDefsResult();


        /// <summary>
        /// 英語テキスト
        /// </summary>
        public string EnglishText { get; private set; } = "";


        /// <summary>
        /// 日本語テキスト
        /// </summary>
        public string JapaneseText { get;  set; } = "";


        /// <summary>
        /// 翻訳コメント
        /// </summary>
        public string Comment { get; set; } = "";




        /// <summary>
        /// コンストラクタ(パーツ)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="text"></param>
        public TranslateText( string   englishText )
        {
            this.Result.ResultText  = "";
            this.Result.ResultIndex = 0;
            this.EnglishText  = englishText;
            this.JapaneseText = "";
            this.Comment      = "";
        }

        /// <summary>
        /// コンストラクタ(サイエンスレポート)
        /// </summary>
        /// <param name="keyText"></param>
        /// <param name="keyIndex"></param>
        /// <param name="englishText"></param>
        public TranslateText( string   keyText ,
                              int      keyIndex,
                              string   englishText )
        {
            this.Result.ResultText  = keyText;
            this.Result.ResultIndex = keyIndex;
            this.EnglishText    = englishText;
            this.JapaneseText   = "";
            this.Comment        = "";
        }

    }
}
