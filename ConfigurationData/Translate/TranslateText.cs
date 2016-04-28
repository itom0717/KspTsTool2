namespace KspTsTool2.ConfigurationData.Translate
{
    /// <summary>
    /// 翻訳するテキストデータ
    /// </summary>
    /// <remarks>
    /// TranslateText + DataType名でそれぞれのクラスを継承で作成すること
    /// </remarks>
    public abstract class TranslateText
    {
        /// <summary>
        /// 読み込みテキスト（翻訳前は英語、翻訳データ取り込み字は日本語になる）
        /// </summary>
        public string SourceText { get; protected set; } = "";


        /// <summary>
        /// 日本語テキスト
        /// </summary>
        public string JapaneseText { get; set; } = "";


        /// <summary>
        /// 翻訳コメント
        /// </summary>
        public string Comment { get; set; } = "";

    }
}
