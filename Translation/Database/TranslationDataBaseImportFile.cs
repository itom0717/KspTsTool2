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
            //cfgファイルを読み込んで解析
            var configurationFile = new ConfigurationFile.ConfigurationFile();
            if ( !configurationFile.AnalysisCfgFile( filename , true ) )
            {
                //データなし
                return 0;
            }


            //データベーステーブル読み込み
            this.Load();

            //DBのデータ置換処理
            foreach ( ConfigurationFile.TextData.TextData textData in configurationFile.TextDataList )
            {
                foreach ( ConfigurationFile.TextData.TranslateText trText in textData.TranslateTextList )
                {









                }
            }

            //データベーステーブル保存
            this.Save();

            return 0;
        }

    }
}
