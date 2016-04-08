using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KspTsTool2.ConfigurationData.Text
{
    /// <summary>
    /// TechTree用テキストデータ
    /// </summary>
    class TextDataTechTree : TextData
    {

        /// <summary>
        /// ID
        /// </summary>
        public string TechTreeID { get; private set; } = "";

        /// <summary>
        /// タイトル
        /// </summary>
        public string TechTreeTitle { get; private set; } = "";


        /// <summary>
        /// TechTree用データ設定
        /// </summary>
        public TextDataTechTree( string id ,
                                 string title,
                                 string description )
        {
            this.DataType = DataType.TechTree;
            this.TechTreeID = id;
            this.TechTreeTitle = title;
            this.TranslateTextList.Add( new Translate.TranslateTextParts( description ) );
        }


    }
}
