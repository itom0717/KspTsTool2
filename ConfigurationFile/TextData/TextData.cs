using System.Collections.Generic;

namespace KspTsTool2.ConfigurationFile.TextData
{
    public class TextData
    {


        /// <summary>
        /// パーツ説明
        /// </summary>
         public List<TranslateText> TranslateTextList { get; set; } = new List<TranslateText>();


        /// <summary>
        /// データタイプ
        /// </summary>
        public DataType DataType { get; set; }





        #region DataType= partの場合

        /// <summary>
        /// パーツ名
        /// </summary>
        /// <remarks>
        /// DataType.Part
        /// </remarks>
        public string PartName { get; set; } = "";

        /// <summary>
        /// パーツタイトル
        /// </summary>
        /// <remarks>
        /// DataType.Part
        /// </remarks>
        public string PartTitle { get; set; } = "";


        /// <summary>
        /// パーツ用データ設定
        /// </summary>
        public void SetPartText( string name ,
                                 string title ,
                                 string description )
        {
            this.DataType  = DataType.Part;
            this.PartName  = name;
            this.PartTitle = title;
            this.TranslateTextList.Add( new TranslateText( description ) );
        }

        #endregion




        #region DataType= サイエンスレポート


        /// <summary>
        /// サイエンスレポートID
        /// </summary>
        /// <remarks>
        /// DataType.ScienceDefs
        /// </remarks>
        public string ScienceDefsID = "";

        /// <summary>
        /// サイエンスレポートタイトル
        /// </summary>
        /// <remarks>
        /// DataType.ScienceDefs
        /// </remarks>
        public string ScienceDefsTitle = "";


        /// <summary>
        /// サイエンスレポート用データ設定
        /// </summary>
        public void SetScienceDefsText( string id ,
                                        string title ,
                                        List<TranslateText> resultText )
        {
            this.DataType          = DataType.ScienceDefs;
            this.ScienceDefsID     = id;
            this.ScienceDefsTitle  = title;
            this.TranslateTextList = resultText;
        }

        #endregion


    }
}