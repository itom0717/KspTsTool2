namespace KspTsTool2.ConfigurationData.Translate
{
    /// <summary>
    /// 翻訳するテキストデータ(Departments)
    /// </summary>
    public class TranslateTextDepartments : TranslateText
    {

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="sourceText"></param>
        public TranslateTextDepartments( string sourceText )
        {
            this.SourceText = sourceText;
            this.JapaneseText = "";
            this.Comment = "";
        }


    }
}
