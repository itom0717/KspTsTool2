using System;
using System.Data;

namespace KspTsTool2.Translation.Database
{
    /// <summary>
    /// インポート処理部分
    /// </summary>
    public partial class TranslationDataBase
    {

        /// <summary>
        /// 翻訳データ取り込み
        /// </summary>
        /// <returns>取り込み件数</returns>
        public int ImportTranslationFile( string filename )
        {
            //取り込み件数
            int importCount = 0;
            var where = new System.Text.StringBuilder();
            TranslationDataTable tgtDB;



            //cfgファイルを読み込んで解析
            var configurationFile = new ConfigurationFile.ConfigurationFile();
            if ( !configurationFile.AnalysisCfgFile( filename , true ) )
            {
                //データなし
                return 0;
            }

            //DBのデータ置換処理
            foreach ( ConfigurationFile.TextData.TextData textData in configurationFile.TextDataList )
            {
                //処理対象DBを判定
                if ( textData.DataType == ConfigurationFile.TextData.DataType.Part )
                {
                    //パーツ
                    tgtDB = this.PartsTranslationDataTable;
                }
                else
                {
                    //サイセンスレポート
                    tgtDB = this.ScienceDefsTranslationDataTable;
                }

                //件数分ループ
                foreach ( ConfigurationFile.TextData.TranslateText trText in textData.TranslateTextList )
                {
                    if ( trText.SourceText != "" )
                    {

                        where.Clear();
                        if ( textData.DataType == ConfigurationFile.TextData.DataType.Part )
                        {
                            //パーツ
                            where.Append( String.Format( "{0}='{1}'" , TranslationDataTable.ColumnNamePartName ,
                                                            tgtDB.DoubleSiglQrt( textData.PartName ) ) );
                        }
                        else
                        {
                            //サイセンスレポート
                            where.Append( String.Format( "{0}='{1}'" , TranslationDataTable.ColumnNameScienceDefsID ,
                                                                tgtDB.DoubleSiglQrt( textData.ScienceDefsID ) ) );
                            where.Append( " AND " );
                            where.Append( String.Format( "{0}='{1}'" , TranslationDataTable.ColumnNameResultText ,
                                                                tgtDB.DoubleSiglQrt( trText.Result.ResultText ) ) );
                            where.Append( " AND " );
                            where.Append( String.Format( "{0}='{1}'" , TranslationDataTable.ColumnNameResultIndex ,
                                                                trText.Result.ResultIndex.ToString() ) );
                        }


                        DataRow[] selectRow = tgtDB.Select( where.ToString() );//検索
                        if ( selectRow.Length > 0 )
                        {
                            //データ存在
                            foreach ( DataRow tgtRow in selectRow )
                            {
                                //データがあるため、値が異なっていたら置換する
                                this.SetDataValue( tgtRow , TranslationDataTable.ColumnNameJapaneseText , trText.SourceText );
                                if ( tgtRow.RowState != DataRowState.Unchanged )
                                {
                                    importCount++;

                                    // 更新日
                                    this.SetDataValue( tgtRow , TranslationDataTable.ColumnNameUpdateDate , System.DateTime.Now.ToString( "yyyy-MM-dd HH:mm:ss" ) );
                                    this.SetDataValue( tgtRow , TranslationDataTable.ColumnNameAutoTrans , false );
                                    this.SetDataValue( tgtRow , TranslationDataTable.ColumnNameMemo , "翻訳取込" );
                                }

                            }
                        }
                        else
                        {
                            //データ存在しないため登録しない
                        }
                    }

                }

            }
            return importCount;
        }

    }
}
