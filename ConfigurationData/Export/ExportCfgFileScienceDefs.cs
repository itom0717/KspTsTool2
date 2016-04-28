using System;
using System.Collections.Generic;

namespace KspTsTool2.ConfigurationData.Export
{
    /// <summary>
    /// ModuleManager用cgfファイル書き出し（サイエンスレポート用)
    /// </summary>
    public class ExportCfgFileScienceDefs : ExportCfgFile
    {
        /// <summary>
        /// データ書き出し
        /// </summary>
        /// <param name="configurationFolder"></param>
        public override void Export( string directoryName , string savePath , List<ConfigurationFile> cnfigurationFile )
        {
            //ファイル名
            string cfgFilename = Common.File.CombinePath(savePath, directoryName + "_ScienceDefs"+".cfg");

            //格納用
            var exportData = new System.Text.StringBuilder();


            //サイエンスレポート用書き出し用データ作成
            foreach ( ConfigurationFile cfgFile in cnfigurationFile )
            {
                foreach ( Text.TextData textData in cfgFile.TextDataList )
                {
                    if ( textData.DataType == DataType.ScienceDefs )
                    {
                        ///サイエンスレポート
                        var tData = ( Text.TextDataScienceDefs ) textData;


                        if ( tData.TranslateTextList.Count >= 1 )
                        {
                            if ( directoryName.Equals( VanillaDirectoryName , StringComparison.CurrentCultureIgnoreCase ) )
                            {
                                //(Vanilla
                                exportData.AppendLine( String.Format( "@EXPERIMENT_DEFINITION:HAS[#id[{0}]]" , tData.ID ) );
                            }
                            else
                            {
                                //MOD
                                exportData.AppendLine( String.Format( "@EXPERIMENT_DEFINITION:HAS[#id[{0}]]:NEEDS[{1}]:FINAL" , tData.ID , directoryName ) );
                            }
                            exportData.AppendLine( "{" );
                            exportData.AppendLine( "\t//Title" );
                            exportData.AppendLine( "\t//\t" + tData.Title );

                            exportData.AppendLine( "\t@RESULTS" );
                            exportData.AppendLine( "\t{" );

                            foreach ( Translate.TranslateText translateText in tData.TranslateTextList )
                            {
                                var tText = ( Translate.TranslateTextScienceDefs ) translateText;

                                exportData.AppendLine( "\t//English Text" );
                                exportData.AppendLine( "\t//\t" + String.Format( @"{0},{1} = {2}" , tText.Result.ResultText , tText.Result.ResultIndex , tText.SourceText ) );
                                exportData.AppendLine( "\t//Japanese Text" );
                                if ( tText.JapaneseText.Equals( "" ) || tText.JapaneseText.Equals( tText.SourceText ) )
                                {
                                    exportData.AppendLine( "\t//\t" + String.Format( @"@{0},{1} = " , tText.Result.ResultText , tText.Result.ResultIndex ) );
                                }
                                else
                                {
                                    if ( !tText.Comment.Equals( "" ) )
                                    {
                                        exportData.AppendLine( "\t//\t" + tText.Comment );
                                    }
                                    exportData.AppendLine( "\t\t" + String.Format( @"@{0},{1} = {2}" , tText.Result.ResultText , tText.Result.ResultIndex , tText.JapaneseText ) );
                                }
                                exportData.AppendLine( "" );

                            }

                            exportData.AppendLine( "\t}" );
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
