using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KspTsTool2.ConfigurationData.Text
{
    /// <summary>
    /// パーツ用テキストデータ
    /// </summary>
    public class TextDataParts : TextData
    {
        /// <summary>
        /// パーツ名
        /// </summary>
        public string PartName { get; private set; } = "";

        /// <summary>
        /// パーツタイトル
        /// </summary>
        public string PartTitle { get; private set; } = "";


        /// <summary>
        /// コンストラクタ
        /// </summary>
        public TextDataParts( string name ,
                       string title ,
                       string description )
        {
            this.DataType = DataType.Parts;
            this.PartName = name;
            this.PartTitle = title;
            this.TranslateTextList.Add( new Translate.TranslateTextParts( description ) );
        }
    }
}
