using System;
using System.Collections.Generic;

namespace KspTsTool2.ConfigurationData
{
    /// <summary>
    /// フォルダ情報&データ書き出し
    /// </summary>
    public class ConfigurationFolder : List<ConfigurationFile>
    {

        /// <summary>
        /// ディレクトリ名
        /// </summary>
        /// <returns></returns>
        public string DirectoryName { get; set; } = "";

        /// <summary>
        /// 保存パス
        /// </summary>
        public string SavePath { get; set; } = "";


        /// <summary>
        /// Module Manager用cfgファイルに保存する
        /// </summary>
        public void ExportModuleManagerCfgFile()
        {

            //DataTypeを列挙
            foreach ( DataType dataType in Enum.GetValues( typeof( DataType ) ) )
            {
                //エクスポート
                Type t = Type.GetType(typeof( Export.ExportCfgFile ).FullName + Enum.GetName(typeof(DataType), dataType));
                object export = Activator.CreateInstance(t);

                ( ( Export.ExportCfgFile ) export ).Export(this.DirectoryName ,
                                                           this.SavePath,
                                                           this);
            }


        }

    }
}
