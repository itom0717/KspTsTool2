namespace KspTsTool2.ConfigurationData.Translate
{
    /// <summary>
    /// 翻訳するテキストデータ(StoryDefs)
    /// </summary>
    class TranslateTextStoryDefs : TranslateText
    {

        /// <summary>
        /// テキスト/インデックス
        /// </summary>
        public class StoryDefsTextNode
        {
            /// <summary>
            ///記述ノードテキストタイトル
            /// </summary>
            public string TextTitle { get; set; } = "";


            /// <summary>
            /// 記述ノードインデックス
            /// </summary>
            public int TextIndex { get; set; } = 0;
        }

        /// <summary>
        /// Resultテキスト/Resultインデックス(サイセンスレポート時に使用)
        /// </summary>
        public StoryDefsTextNode TextNode = new StoryDefsTextNode();


        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="keyText"></param>
        /// <param name="keyIndex"></param>
        /// <param name="englishText"></param>
        public TranslateTextStoryDefs( string keyText ,
                                       int keyIndex ,
                                       string sourceText )
        {
            this.TextNode.TextTitle = keyText;
            this.TextNode.TextIndex = keyIndex;
            this.SourceText = sourceText;
            this.JapaneseText = "";
            this.Comment = "";
        }
    }
}
