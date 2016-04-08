namespace KspTsTool2.ConfigurationData.Translate
{
    /// <summary>
    /// 翻訳するテキストデータ(パーツ用)
    /// </summary>
    public class TranslateTextParts : TranslateText
    {

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="key"></param>
        /// <param name="text"></param>
        public TranslateTextParts( string sourceText )
        {
            this.SourceText = sourceText;
            this.JapaneseText = "";
            this.Comment = "";
        }


    }
}
