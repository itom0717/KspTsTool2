namespace KspTsTool2.ConfigurationData.Translate
{
    /// <summary>
    /// 翻訳するテキストデータ(サイセンスレポート用)
    /// </summary>
    public class TranslateTextScienceDefs : TranslateText
    {

        
        /// <summary>
        /// Resultテキスト/Resultインデックス
        /// </summary>
        public class ScienceDefsResult
        {
            /// <summary>
            /// Resultテキスト
            /// </summary>
            public string ResultText { get; set; } = "";


            /// <summary>
            /// Resultインデックス
            /// </summary>
            public int ResultIndex { get; set; } = 0;
        }

        /// <summary>
        /// Resultテキスト/Resultインデックス(サイセンスレポート時に使用)
        /// </summary>
        public ScienceDefsResult Result = new ScienceDefsResult();


        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="keyText"></param>
        /// <param name="keyIndex"></param>
        /// <param name="englishText"></param>
        public TranslateTextScienceDefs( string keyText ,
                                         int keyIndex ,
                                         string sourceText )
        {
            this.Result.ResultText = keyText;
            this.Result.ResultIndex = keyIndex;
            this.SourceText = sourceText;
            this.JapaneseText = "";
            this.Comment = "";
        }

    }
}
