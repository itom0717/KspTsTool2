using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KspTsTool2.ConfigurationData.Export
{
    /// <summary>
    /// ModuleManager用cgfファイル書き出し(Strategies)
    /// </summary>
    public class ExportCfgFileStrategies : ExportCfgFile
    {

        /// <summary>
        /// データ書き出し
        /// </summary>
        /// <param name="configurationFolder"></param>
        public override void Export( string directoryName , string savePath , List<ConfigurationFile> cnfigurationFile )
        {
            //ファイル名
            string cfgFilename = Common.File.CombinePath(savePath, directoryName + "_Strategies" + ".cfg");


            //格納用
            var exportData = new System.Text.StringBuilder();



            //パーツ用書き出し用データ作成
            foreach ( ConfigurationFile cfgFile in cnfigurationFile )
            {
                foreach ( Text.TextData textData in cfgFile.TextDataList )
                {
                    if ( textData.DataType == DataType.Strategies )
                    {
                        var tData = ( Text.TextDataStrategies ) textData;

                        if ( tData.TranslateTextList.Count >= 1 )
                        {
                            //スペースが含まれている場合は、?に変換
                            string name = tData.Name;
                            name = name.Replace( " " , "?" );

                            if ( directoryName.Equals( VanillaDirectoryName , StringComparison.CurrentCultureIgnoreCase ) )
                            {
                                //(Vanilla
                                exportData.AppendLine( String.Format( "@STRATEGY:HAS[#name[{0}]]" , name ) );
                            }
                            else
                            {
                                //MOD
                                exportData.AppendLine( String.Format( "@STRATEGY:HAS[#name[{0}]]:NEEDS[{1}]:FINAL" , name , directoryName ) );
                            }
                            exportData.AppendLine( "{" );

                            exportData.AppendLine( "\t//Title" );
                            exportData.AppendLine( "\t//\t" + tData.Title );
                            exportData.AppendLine( "\t//English Text" );
                            exportData.AppendLine( "\t//\t" + String.Format( @"@desc = {0}" , tData.TranslateTextList[0].SourceText ) );
                            exportData.AppendLine( "\t//Japanese Text" );
                            if ( tData.TranslateTextList[0].JapaneseText.Equals( "" ) || tData.TranslateTextList[0].JapaneseText.Equals( tData.TranslateTextList[0].SourceText ) )
                            {
                                exportData.AppendLine( "\t//\t" + @"@desc = " );
                            }
                            else
                            {
                                if ( !tData.TranslateTextList[0].Comment.Equals( "" ) )
                                {
                                    exportData.AppendLine( "\t//\t" + tData.TranslateTextList[0].Comment );
                                }
                                exportData.AppendLine( "\t\t" + String.Format( @"@desc = {0}" , tData.TranslateTextList[0].JapaneseText ) );
                            }

                            exportData.AppendLine( "}" );
                            exportData.AppendLine( "" );
                        }

                    }
                }
            }

            //データがあればファイル書き出し
            this.DataWrite( cfgFilename , exportData );
        }

    }
}
