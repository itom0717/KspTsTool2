using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KspTsTool2.ConfigurationData.Export
{
    /// <summary>
    /// ModuleManager用cgfファイル書き出し
    /// </summary>
    /// <remarks>
    /// ExportCfgFile + DataType名でそれぞれのクラスを継承で作成すること
    /// </remarks>
    public abstract class ExportCfgFile
    {

        /// <summary>
        /// VanillaのDirectoryName
        /// </summary>
        protected const string VanillaDirectoryName = "Squad";



        /// <summary>
        /// データ書き出し
        /// </summary>
        /// <param name="configurationFolder"></param>
        public abstract void Export( string directoryName , string savePath , List<ConfigurationFile> cnfigurationFile );


        /// <summary>
        /// データ書き出し
        /// </summary>
        /// <param name="cfgFilename"></param>
        protected void DataWrite( string cfgFilename , System.Text.StringBuilder exportData )
        {

            //ファイル削除
            Common.File.DeleteFile( cfgFilename );

            //データがあればファイル書き出し
            if ( exportData.Length > 0 )
            {
                using ( System.IO.StreamWriter sw = new System.IO.StreamWriter( cfgFilename ,
                                                                                false ,
                                                                                System.Text.Encoding.UTF8 ) )
                {
                    sw.Write( exportData.ToString() );
                    sw.Close();
                }
            }
        }



    }
}
