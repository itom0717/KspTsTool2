using System.Collections.Generic;

namespace KspTsTool2.ConfigurationData.NodeInfo
{
    /// <summary>
    /// Nodeの解析処理（継承元クラス）
    /// </summary>
    /// <remarks>
    /// NodeAnalysis + DataType名でそれぞれのクラスを継承で作成すること
    /// </remarks>
    public abstract class NodeAnalysis
    {
        /// <summary>
        /// TextDataList
        /// </summary>
        public List<Text.TextData> TextDataList { protected get; set; } = null;


        /// <summary>
        /// 1ブロック解析
        /// </summary>
        /// <param name="blockText"></param>
        public abstract void AnalysisOneBlock( int nestLevel , string blockText );


        /// <summary>
        /// 1ブロック解析(インポートモード時)
        /// </summary>
        /// <param name="blockText"></param>
        public abstract void AnalysisOneBlockImport( int nestLevel , string blockText );


    }

}
