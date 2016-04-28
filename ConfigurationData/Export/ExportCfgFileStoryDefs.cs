using System;
using System.Collections.Generic;

namespace KspTsTool2.ConfigurationData.Export
{
    /// <summary>
    /// ModuleManager用cgfファイル書き出し（StoryDefs)
    /// </summary>
    class ExportCfgFileStoryDefs : ExportCfgFile
    {

        /// <summary>
        /// データ書き出し
        /// </summary>
        /// <param name="configurationFolder"></param>
        public override void Export( string directoryName , string savePath , List<ConfigurationFile> cnfigurationFile )
        {
            //ファイル名
            string cfgFilename = Common.File.CombinePath(savePath, directoryName + "_StoryDefs"+".cfg");

            //格納用
            var exportData = new System.Text.StringBuilder();

            //書き出し用データ作成
            foreach ( ConfigurationFile cfgFile in cnfigurationFile )
            {
                foreach ( Text.TextData textData in cfgFile.TextDataList )
                {
                    if ( textData.DataType == DataType.StoryDefs )
                    {
                        ///StoryDefs
                        var tData = ( Text.TextDataStoryDefs ) textData;


                        if ( tData.TranslateTextList.Count >= 1 )
                        {
                            if ( directoryName.Equals( VanillaDirectoryName , StringComparison.CurrentCultureIgnoreCase ) )
                            {
                                //(Vanilla
                                exportData.AppendLine( String.Format( "@{0}" , tData.Title ) );
                            }
                            else
                            {
                                //MOD
                                exportData.AppendLine( String.Format( "@{0}:NEEDS[{1}]:FINAL" , tData.Title , directoryName ) );
                            }
                            exportData.AppendLine( "{" );

                            foreach ( Translate.TranslateText translateText in tData.TranslateTextList )
                            {
                                var tText = ( Translate.TranslateTextStoryDefs ) translateText;

                                exportData.AppendLine( "\t//English Text" );
                                exportData.AppendLine( "\t//\t" + String.Format( @"{0},{1} = {2}" , tText.TextNode.TextTitle , tText.TextNode.TextIndex , tText.SourceText ) );
                                exportData.AppendLine( "\t//Japanese Text" );
                                if ( tText.JapaneseText.Equals( "" ) || tText.JapaneseText.Equals( tText.SourceText ) )
                                {
                                    exportData.AppendLine( "\t//\t" + String.Format( @"@{0},{1} = " , tText.TextNode.TextTitle , tText.TextNode.TextIndex ) );
                                }
                                else
                                {
                                    if ( !tText.Comment.Equals( "" ) )
                                    {
                                        exportData.AppendLine( "\t//\t" + tText.Comment );
                                    }
                                    exportData.AppendLine( "\t\t" + String.Format( @"@{0},{1} = {2}" , tText.TextNode.TextTitle , tText.TextNode.TextIndex , tText.JapaneseText ) );
                                }
                                exportData.AppendLine( "" );

                            }

                            exportData.AppendLine( "}" );
                            exportData.AppendLine( "" );

                        }
                    }
                }
            }


            //データが存在する場合
            if ( exportData.Length > 0 )
            {
                exportData.Insert( 0 , "@STORY_DEF:FINAL {\n" );
                exportData.AppendLine( "}" );
            }

            //データがあればファイル書き出し
            this.DataWrite( cfgFilename , exportData );


        }

    }
}
