namespace KspTsTool2.ConfigurationData.Translate
{
    /// <summary>
    /// 翻訳するテキストデータ(Strategies)
    /// </summary>
    public class TranslateTextStrategies : TranslateText
    {

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="sourceText"></param>
        public TranslateTextStrategies( string sourceText )
        {
            this.SourceText = sourceText;
            this.JapaneseText = "";
            this.Comment = "";
        }


    }
}
