using System.Data;

namespace KspTsTool2.ConfigurationData
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

            //cfgファイルを読み込んで解析
            var configurationFile = new ConfigurationData.ConfigurationFile();
            if ( !configurationFile.AnalysisCfgFile( filename , true ) )
            {
                //データなし
                return 0;
            }

            //DBのデータ置換処理
            foreach ( ConfigurationData.Text.TextData textData in configurationFile.TextDataList )
            {
                //処理対象DBを判定
                DataTable.TranslationDataTable tgtDB = this.TranslationDataTable[textData.DataType];

                //件数分ループ
                foreach ( ConfigurationData.Translate.TranslateText translateText in textData.TranslateTextList )
                {
                    if ( translateText.SourceText != "" )
                    {
                        //データが存在するか検索
                        DataRow[] selectRow = tgtDB.GetExistsDataRow(null,textData,translateText);
                        if ( selectRow.Length > 0 )
                        {
                            //データ存在
                            foreach ( DataRow tgtRow in selectRow )
                            {
                                //データがあるため、値が異なっていたら置換する
                                tgtDB.SetDataValue( tgtRow , DataTable.TranslationDataTable.ColumnNameJapaneseText , translateText.SourceText );
                                if ( tgtRow.RowState != DataRowState.Unchanged )
                                {
                                    importCount++;

                                    // 更新日
                                    tgtDB.SetDataValue( tgtRow , DataTable.TranslationDataTable.ColumnNameUpdateDate , System.DateTime.Now.ToString( "yyyy-MM-dd HH:mm:ss" ) );
                                    tgtDB.SetDataValue( tgtRow , DataTable.TranslationDataTable.ColumnNameAutoTrans , false );
                                    tgtDB.SetDataValue( tgtRow , DataTable.TranslationDataTable.ColumnNameMemo , "翻訳取込" );
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
