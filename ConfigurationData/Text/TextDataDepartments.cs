using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KspTsTool2.ConfigurationData.Text
{
    /// <summary>
    /// Departments用テキストデータ
    /// </summary>
    public class TextDataDepartments : TextData
    {
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; private set; } = "";

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public TextDataDepartments( string name ,
                                    string description )
        {
            this.DataType = DataType.Departments;
            this.Name = name;
            this.TranslateTextList.Add( new Translate.TranslateTextDepartments( description ) );
        }
    }
}
