using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KspTsTool2.ConfigurationData.Text
{
    /// <summary>
    /// Strategies用テキストデータ
    /// </summary>
    public class TextDataStrategies : TextData
    {
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; private set; } = "";

        /// <summary>
        /// タイトル
        /// </summary>
        public string Title { get; private set; } = "";


        /// <summary>
        /// コンストラクタ
        /// </summary>
        public TextDataStrategies( string name ,
                       string title ,
                       string description )
        {
            this.DataType = DataType.Strategies;
            this.Name = name;
            this.Title = title;
            this.TranslateTextList.Add( new Translate.TranslateTextStrategies( description ) );
        }
    }
}
