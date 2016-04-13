using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KspTsTool2.ConfigurationData.Text
{
    /// <summary>
    /// StoryDefs用テキストデータ
    /// </summary>
    class TextDataStoryDefs : TextData
    {

        /// <summary>
        /// ID
        /// </summary>
        public string TextNodeTitle { get; private set; } = "";

        /// <summary>
        /// StoryDefs用データ設定
        /// </summary>
        public TextDataStoryDefs( string id ,
                                 List<Translate.TranslateText> textNodeText )
        {
            this.DataType = DataType.StoryDefs;
            this.TextNodeTitle = id;
            this.TranslateTextList = textNodeText;
        }


    }
}
