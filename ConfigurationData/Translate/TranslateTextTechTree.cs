namespace KspTsTool2.ConfigurationData.Translate
{
    /// <summary>
    /// 翻訳するテキストデータ(TechTree)
    /// </summary>
    class TranslateTextTechTree : TranslateText
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="sourceText"></param>
        public TranslateTextTechTree( string sourceText )
        {
            this.SourceText = sourceText;
            this.JapaneseText = "";
            this.Comment = "";
        }
    }
}
