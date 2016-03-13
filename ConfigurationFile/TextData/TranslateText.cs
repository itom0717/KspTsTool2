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
        /// 読み込みテキスト（翻訳前は英語、翻訳データ取り込み字は日本語になる）
        /// </summary>
        public string SourceText { get; private set; } = "";


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
        public TranslateText( string sourceText )
        {
            this.Result.ResultText  = "";
            this.Result.ResultIndex = 0;
            this.SourceText = sourceText;
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
                              string sourceText )
        {
            this.Result.ResultText  = keyText;
            this.Result.ResultIndex = keyIndex;
            this.SourceText = sourceText;
            this.JapaneseText   = "";
            this.Comment        = "";
        }

    }
}
