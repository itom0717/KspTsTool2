using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KspTsTool2.ConfigurationData.Text
{
    /// <summary>
    /// サイエンスレポート用テキストデータ
    /// </summary>
    public class TextDataScienceDefs : TextData
    {
        /// <summary>
        /// サイエンスレポートID
        /// </summary>
        public string ID { get; private set; } = "";

        /// <summary>
        /// サイエンスレポートタイトル
        /// </summary>
        public string Title { get; private set; } = "";


        /// <summary>
        /// サイエンスレポート用データ設定
        /// </summary>
        public TextDataScienceDefs( string id ,
                                    string title ,
                                    List<Translate.TranslateText> resultText )
        {
            this.DataType = DataType.ScienceDefs;
            this.ID = id;
            this.Title = title;
            this.TranslateTextList = resultText;
        }


    }
}
