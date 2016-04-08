using System.Collections.Generic;

namespace KspTsTool2.ConfigurationData.Text
{
    /// <summary>
    /// テキストデータ（継承元クラス）
    /// </summary>
    /// <remarks>
    /// TextData + DataType名でそれぞれのクラスを継承で作成すること
    /// </remarks>
    public abstract class TextData
    {
        /// <summary>
        /// データタイプ
        /// </summary>
        public DataType DataType { get; protected set; }


        /// <summary>
        /// 翻訳用テキストデータ
        /// </summary>
        public List<Translate.TranslateText> TranslateTextList { get; protected set; } = new List<Translate.TranslateText>();
    }
}