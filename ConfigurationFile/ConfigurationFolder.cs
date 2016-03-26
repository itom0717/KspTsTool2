using System;
using System.Collections.Generic;

namespace KspTsTool2.ConfigurationFile
{
    /// <summary>
    /// フォルダ情報
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
            //ファイル名
            string partFilename        = Common.File.CombinePath(this.SavePath, this.DirectoryName + ".cfg");
            string scienceDefsFilename = Common.File.CombinePath(this.SavePath, this.DirectoryName + "_ScienceDefs"+".cfg");

            //ファイル削除
            Common.File.DeleteFile( partFilename );
            Common.File.DeleteFile( scienceDefsFilename );

            //格納用
            var partData        = new System.Text.StringBuilder();
            var scienceDefsData = new System.Text.StringBuilder();



            //パーツ用書き出し用データ作成
            foreach ( ConfigurationFile cfgFile in this )
            {
                foreach ( TextData.TextData textData in cfgFile.TextDataList )
                {
                    if ( textData.DataType == TextData.DataType.Part )
                    {
                        //パーツ
                        if ( textData.TranslateTextList.Count >= 1 )
                        {
                            //スペースが含まれている場合は、?に変換
                            string pName = textData.PartName;
                            pName = pName.Replace( " " , "?" );

                            partData.AppendLine( String.Format( "@PART[{0}]:NEEDS[{1}]:FINAL", pName, this.DirectoryName ) );
                            partData.AppendLine( "{" );

                            partData.AppendLine( "\t//Title" );
                            partData.AppendLine( "\t//\t" + textData.PartTitle );
                            partData.AppendLine( "\t//English Text" );
                            partData.AppendLine( "\t//\t" + String.Format( @"@description = {0}" , textData.TranslateTextList[0].SourceText ) );
                            partData.AppendLine( "\t//Japanese Text" );
                            if ( textData.TranslateTextList[0].JapaneseText.Equals( "" ) || textData.TranslateTextList[0].JapaneseText.Equals( textData.TranslateTextList[0].SourceText ) )
                            {
                                partData.AppendLine( "\t//\t" + @"@description = " );
                            }
                            else
                            {
                                if ( !textData.TranslateTextList[0].Comment.Equals( "" ) )
                                {
                                    partData.AppendLine( "\t//\t" + textData.TranslateTextList[0].Comment );
                                }
                                partData.AppendLine( "\t\t" + String.Format( @"@description = {0}" , textData.TranslateTextList[0].JapaneseText ) );
                            }

                            partData.AppendLine( "}" );
                            partData.AppendLine( "" );
                        }

                    }
                }
            }



            //サイエンスレポート用書き出し用データ作成
            foreach ( ConfigurationFile cfgFile in this )
            {
                foreach ( TextData.TextData textData in cfgFile.TextDataList )
                {
                    if ( textData.DataType == TextData.DataType.ScienceDefs )
                    {
                        if ( textData.TranslateTextList.Count >= 1 )
                        {
                            scienceDefsData.AppendLine( String.Format( "@EXPERIMENT_DEFINITION:HAS[#id[{0}]]:NEEDS[{1}]:FINAL", textData.ScienceDefsID, this.DirectoryName ) );
                            scienceDefsData.AppendLine( "{" );
                            scienceDefsData.AppendLine( "\t//Title" );
                            scienceDefsData.AppendLine( "\t//\t" + textData.ScienceDefsTitle );

                            scienceDefsData.AppendLine( "\t@RESULTS" );
                            scienceDefsData.AppendLine( "\t{" );

                            foreach ( TextData.TranslateText trText in textData.TranslateTextList )
                            {

                                scienceDefsData.AppendLine( "\t//English Text" );
                                scienceDefsData.AppendLine( "\t//\t" + String.Format( @"{0},{1} = {2}" , trText.Result.ResultText , trText.Result.ResultIndex , trText.SourceText ) );
                                scienceDefsData.AppendLine( "\t//Japanese Text" );
                                if ( trText.JapaneseText.Equals( "" ) || trText.JapaneseText.Equals( trText.SourceText ) )
                                {
                                    scienceDefsData.AppendLine( "\t//\t" + String.Format( @"{0},{1} = " , trText.Result.ResultText , trText.Result.ResultIndex ) );
                                }
                                else
                                {
                                    if ( !trText.Comment.Equals( "" ) )
                                    {
                                        scienceDefsData.AppendLine( "\t//\t" + trText.Comment );
                                    }
                                    scienceDefsData.AppendLine( "\t\t" + String.Format( @"{0},{1} = {2}" , trText.Result.ResultText , trText.Result.ResultIndex , trText.JapaneseText ) );
                                }
                                scienceDefsData.AppendLine( "" );

                            }

                            scienceDefsData.AppendLine( "\t}" );
                            scienceDefsData.AppendLine( "}" );
                            scienceDefsData.AppendLine( "" );

                        }
                    }
                }
            }



            //データがあればファイル書き出し
            if ( partData.Length > 0 )
            {
                using ( System.IO.StreamWriter sw = new System.IO.StreamWriter( partFilename ,
                                                                                false ,
                                                                                System.Text.Encoding.UTF8 ) )
                {
                    sw.Write( partData.ToString() );
                    sw.Close();
                }
            }
            if ( scienceDefsData.Length > 0 )
            {
                using ( System.IO.StreamWriter sw = new System.IO.StreamWriter( scienceDefsFilename ,
                                                                                false ,
                                                                                System.Text.Encoding.UTF8 ) )
                {
                    sw.Write( scienceDefsData.ToString() );
                    sw.Close();
                }
            }

        }

    }
}
