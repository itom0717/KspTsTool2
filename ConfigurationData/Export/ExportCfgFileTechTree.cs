using System;
using System.Collections.Generic;

namespace KspTsTool2.ConfigurationData.Export
{
    /// <summary>
    /// ModuleManager用cgfファイル書き出し（TechTree)
    /// </summary>
    class ExportCfgFileTechTree : ExportCfgFile
    {

        /// <summary>
        /// データ書き出し
        /// </summary>
        /// <param name="configurationFolder"></param>
        public override void Export( string directoryName , string savePath , List<ConfigurationFile> cnfigurationFile )
        {
            //ファイル名
            string cfgFilename = Common.File.CombinePath(savePath, directoryName + "_TechTree"+".cfg");

            //格納用
            var exportData = new System.Text.StringBuilder();

            //書き出し用データ作成
            foreach ( ConfigurationFile cfgFile in cnfigurationFile )
            {
                foreach ( Text.TextData textData in cfgFile.TextDataList )
                {
                    if ( textData.DataType == DataType.TechTree )
                    {
                        //パーツ
                        var textDataTechTree = ( Text.TextDataTechTree ) textData;
                        if ( textDataTechTree.TranslateTextList.Count >= 1 )
                        {
                            //スペースが含まれている場合は、?に変換
                            string tID = textDataTechTree.TechTreeID;
                            tID = tID.Replace( " " , "?" );

                            if ( directoryName.Equals( VanillaDirectoryName , StringComparison.CurrentCultureIgnoreCase ) )
                            {
                                //(Vanilla
                                exportData.AppendLine( String.Format( "@RDNode:HAS[#id[{0}]]" , tID ) );
                            }
                            else
                            {
                                //MOD
                                exportData.AppendLine( String.Format( "@RDNode:HAS[#id[{0}]]:NEEDS[{1}]:FINAL" , tID , directoryName ) );
                            }
                            exportData.AppendLine( "{" );

                            exportData.AppendLine( "\t//Title" );
                            exportData.AppendLine( "\t//\t" + textDataTechTree.TechTreeTitle );
                            exportData.AppendLine( "\t//English Text" );
                            exportData.AppendLine( "\t//\t" + String.Format( @"@description = {0}" , textDataTechTree.TranslateTextList[0].SourceText ) );
                            exportData.AppendLine( "\t//Japanese Text" );
                            if ( textDataTechTree.TranslateTextList[0].JapaneseText.Equals( "" ) || textDataTechTree.TranslateTextList[0].JapaneseText.Equals( textDataTechTree.TranslateTextList[0].SourceText ) )
                            {
                                exportData.AppendLine( "\t//\t" + @"@description = " );
                            }
                            else
                            {
                                if ( !textDataTechTree.TranslateTextList[0].Comment.Equals( "" ) )
                                {
                                    exportData.AppendLine( "\t//\t" + textDataTechTree.TranslateTextList[0].Comment );
                                }
                                exportData.AppendLine( "\t\t" + String.Format( @"@description = {0}" , textDataTechTree.TranslateTextList[0].JapaneseText ) );
                            }

                            exportData.AppendLine( "}" );
                            exportData.AppendLine( "" );
                        }

                    }
                }
            }


            //データが存在する場合
            if( exportData .Length>0 )
            {
                exportData.Insert( 0 , "@TechTree {\n" );
                exportData.AppendLine("}");
            }



            //データがあればファイル書き出し
            this.DataWrite( cfgFilename , exportData );


        }

    }
}
