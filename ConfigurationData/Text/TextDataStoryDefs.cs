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
        /// Title
        /// </summary>
        public string Title { get; private set; } = "";

        /// <summary>
        /// StoryDefs用データ設定
        /// </summary>
        public TextDataStoryDefs( string title ,
                                 List<Translate.TranslateText> textNodeText )
        {
            this.DataType = DataType.StoryDefs;
            this.Title = title;
            this.TranslateTextList = textNodeText;
        }


    }
}
