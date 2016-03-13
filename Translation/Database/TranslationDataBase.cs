using System;
using System.Data;

namespace KspTsTool2.Translation.Database
{
    /// <summary>
    /// 翻訳データベース処理
    /// </summary>
    public partial class TranslationDataBase
    {

        /// <summary>
        /// データベース保存ファイル名(パーツ)
        /// </summary>
        private const string PartsTranslationDataTableFilename = "PartsTranslationDB.xml";

        /// <summary>
        /// データベース保存ファイル名(サイエンスレポート)
        /// </summary>
        private const string ScienceDefsTranslationDataTableFilename = "ScienceDefsTranslationDB.xml";


        /// <summary>
        /// データベース保存ファイル名フルパス(パーツ)
        /// </summary>
        private string PartsTranslationDataTableFilePath { get; set; }


        /// <summary>
        /// データベース保存ファイル名フルパス(サイエンスレポート)
        /// </summary>
        private string ScienceDefsTranslationDataTableFilePath { get; set; }



        /// <summary>
        /// 翻訳DataTable(パーツ)
        /// </summary>
        private TranslationDataTable PartsTranslationDataTable { get; set; } = null;


        /// <summary>
        /// 翻訳DataTable(サイエンスレポート)
        /// </summary>
        private TranslationDataTable ScienceDefsTranslationDataTable { get; set; } = null;





        /// <summary>
        /// 自動翻訳処理用
        /// </summary>
        private MicrosoftTranslatorAPI.TranslatorApi TranslatorApi { get; set; } = null;

        /// <summary>
        /// インスタンスを生成
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="clientSecret"></param>
        public TranslationDataBase( string clientId = "" ,
                                   string clientSecret = "" )
        {
            //clientId/clientSecretの指定があればMicrosoftTranslatorAPIのインスタンスを生成
            if ( !clientId.Equals( "" ) && !clientSecret.Equals( "" ) )
            {
                this.TranslatorApi = new MicrosoftTranslatorAPI.TranslatorApi( clientId ,
                                                                               clientSecret );
            }

            // データベース保存ファイル名フルパス
            this.PartsTranslationDataTableFilePath       = Common.File.CombinePath( Common.File.GetApplicationDirectory() ,
                                                                        　　　PartsTranslationDataTableFilename );
            this.ScienceDefsTranslationDataTableFilePath = Common.File.CombinePath( Common.File.GetApplicationDirectory() ,
                                                                              ScienceDefsTranslationDataTableFilename );
            //データベーステーブル
            this.PartsTranslationDataTable       = new TranslationDataTable( ConfigurationFile.TextData.DataType.Part );
            this.ScienceDefsTranslationDataTable = new TranslationDataTable( ConfigurationFile.TextData.DataType.ScienceDefs  );

        }

        /// <summary>
        /// デストラクタ
        /// </summary>
        ~TranslationDataBase()
        {
            this.TranslatorApi = null;
            this.PartsTranslationDataTable = null;
            this.ScienceDefsTranslationDataTable = null;
        }

        /// <summary>
        /// データベース読み込み
        /// </summary>
        public void Load()
        {
            this.PartsTranslationDataTable.Load(       this.PartsTranslationDataTableFilePath );
            this.ScienceDefsTranslationDataTable.Load( this.ScienceDefsTranslationDataTableFilePath );
        }

        /// <summary>
        /// データベース保存
        /// </summary>
        public void Save()
        {
            if ( this.PartsTranslationDataTable != null )
            {
                //保存
                this.PartsTranslationDataTable.Save( this.PartsTranslationDataTableFilePath ); ;
            }
            if ( this.ScienceDefsTranslationDataTable != null )
            {
                //保存
                this.ScienceDefsTranslationDataTable.Save( this.ScienceDefsTranslationDataTableFilePath ); ;
            }
        }
       
        
        /// <summary>
        /// 英語から日本語へ翻訳
        /// </summary>
        /// <param name="TranslateText"></param>
        public void Translate( string directoryName,
                               string cfgFilename,
                               ConfigurationFile.TextData.TextData      textData ,
                               ConfigurationFile.TextData.TranslateText translateText,
                               int dataOrder)
        {

            if ( directoryName.Equals( "" )
                || cfgFilename.Equals( "" )
                || translateText.SourceText.Equals( "" ) )
            {
                return;
            }




            try
            {
                //処理対象DB
                TranslationDataTable tgtDB;
                if ( textData.DataType == ConfigurationFile.TextData.DataType.Part )
                {
                    tgtDB = this.PartsTranslationDataTable;
                }
                else
                {
                    tgtDB = this.ScienceDefsTranslationDataTable;
                }

                //DBに存在するかチェック
                var where = new System.Text.StringBuilder();
                if ( textData.DataType == ConfigurationFile.TextData.DataType.Part )
                {
                    where.Clear();
                    where.Append( String.Format( "{0}='{1}'" , TranslationDataTable.ColumnNameDirName ,
                                                        tgtDB.DoubleSiglQrt( directoryName ) ) );
                    where.Append( " AND " );
                    where.Append( String.Format( "{0}='{1}'" , TranslationDataTable.ColumnNamePartName ,
                                                        tgtDB.DoubleSiglQrt( textData.PartName ) ) );
                }
                else
                {
                    where.Clear();
                    where.Append( String.Format( "{0}='{1}'" , TranslationDataTable.ColumnNameDirName ,
                                                        tgtDB.DoubleSiglQrt( directoryName ) ) );
                    where.Append( " AND " );
                    where.Append( String.Format( "{0}='{1}'" , TranslationDataTable.ColumnNameScienceDefsID ,
                                                        tgtDB.DoubleSiglQrt( textData.ScienceDefsID ) ) );
                    where.Append( " AND " );
                    where.Append( String.Format( "{0}='{1}'" , TranslationDataTable.ColumnNameResultText ,
                                                        tgtDB.DoubleSiglQrt( translateText.Result.ResultText ) ) );
                    where.Append( " AND " );
                    where.Append( String.Format( "{0}='{1}'" , TranslationDataTable.ColumnNameResultIndex ,
                                                        translateText.Result.ResultIndex.ToString() ) );

                }
                DataRow[] selectRow = tgtDB.Select(where.ToString());
                if ( selectRow.Length > 0 )
                {
                    DataRow tgtRow = selectRow[0];
                    DataRow sameTextRow = null;

                    //データあり
                    translateText.JapaneseText = ( string ) tgtRow[TranslationDataTable.ColumnNameJapaneseText];

                    //存在チェックありに変更
                    this.SetDataValue( tgtRow , TranslationDataTable.ColumnNameExists , true );

                    //Order
                    this.SetDataValue( tgtRow , TranslationDataTable.ColumnNameSortOrder , dataOrder );

                    //変更無しに設定
                    tgtRow.AcceptChanges();





                    //元テキスト変更の場合
                    if ( !translateText.SourceText.Equals( ( string ) tgtRow[TranslationDataTable.ColumnNameEnglishText] ) )
                    {
                        //英語テキスト変更
                        this.SetDataValue( tgtRow , TranslationDataTable.ColumnNameEnglishText , translateText.SourceText );
                        this.SetDataValue( tgtRow , TranslationDataTable.ColumnNameMemo , "英語テキスト変更" );

                        //英語テキスト変更のため再翻訳
                        //翻訳済みテキストと同じデータがあればそれを使用する
                        sameTextRow = tgtDB.SearchSameText( translateText.SourceText );
                        if( sameTextRow != null)
                        {
                            translateText.JapaneseText = ( string ) sameTextRow[TranslationDataTable.ColumnNameJapaneseText];

                            //備考
                            this.SetDataValue( tgtRow , TranslationDataTable.ColumnNameMemo , "英語テキスト変更：同じ英語テキストから日本語を取得" );
                            this.SetDataValue( tgtRow , TranslationDataTable.ColumnNameAutoTrans , sameTextRow[TranslationDataTable.ColumnNameAutoTrans] );
                        }
                    }
                    else if ( translateText.JapaneseText.Equals( "" ) )
                    {
                        //未翻訳
                        //翻訳済みテキストと同じデータがあればそれを使用する
                        sameTextRow = tgtDB.SearchSameText( translateText.SourceText );
                        if ( sameTextRow != null )
                        {
                            translateText.JapaneseText = ( string ) sameTextRow[TranslationDataTable.ColumnNameJapaneseText];

                            //備考
                            this.SetDataValue( tgtRow , TranslationDataTable.ColumnNameMemo , "同じ英語テキストから日本語を取得" );
                            this.SetDataValue( tgtRow , TranslationDataTable.ColumnNameAutoTrans , sameTextRow[TranslationDataTable.ColumnNameAutoTrans] );
                        }
                    }
                    else if ( tgtRow[TranslationDataTable.ColumnNameAutoTrans].Equals( true ) )
                    {
                        //自動翻訳の場合で、英語データが同じで手動翻訳の場合はそちらを使用する
                        sameTextRow = tgtDB.SearchSameText( translateText.SourceText );
                        if ( sameTextRow != null && sameTextRow[TranslationDataTable.ColumnNameAutoTrans].Equals( false ) )
                        {
                            translateText.JapaneseText = ( string ) sameTextRow[TranslationDataTable.ColumnNameJapaneseText];

                            //備考
                            this.SetDataValue( tgtRow , TranslationDataTable.ColumnNameMemo , "同じ英語テキストから日本語を取得" );
                            this.SetDataValue( tgtRow , TranslationDataTable.ColumnNameAutoTrans , sameTextRow[TranslationDataTable.ColumnNameAutoTrans] );
                        }
                    }


                    //空欄の場合や元テキストが変更され、同じ翻訳データが無い場合は再翻訳
                    if ( translateText.JapaneseText.Equals( "" ) && this.TranslatorApi != null )
                    {
                        //自動翻訳
                        translateText.JapaneseText = this.TranslatorApi.TranslateEnglishToJapanese( translateText.SourceText );
                        if ( !translateText.JapaneseText.Equals( "" ) )
                        {
                            this.SetDataValue( tgtRow , TranslationDataTable.ColumnNameMemo , "自動翻訳で日本語を取得" );
                            this.SetDataValue( tgtRow , TranslationDataTable.ColumnNameAutoTrans , true );
                        }
                    }


                    //日本語が変更されていたらデータ保存
                    this.SetDataValue( tgtRow , TranslationDataTable.ColumnNameJapaneseText , translateText.JapaneseText );


                    //データが変更されていたら変更しておく
                    if ( textData.DataType == ConfigurationFile.TextData.DataType.Part )
                    {
                        // パーツタイトル
                        this.SetDataValue( tgtRow , TranslationDataTable.ColumnNamePartTitle , textData.PartTitle );
                    }
                    else
                    {
                        // サイエンスレポートタイトル
                        this.SetDataValue( tgtRow , TranslationDataTable.ColumnNameScienceDefsTitle , textData.ScienceDefsTitle );
                    }



                    if( tgtRow .RowState != DataRowState.Unchanged )
                    {
                        //変更された
                        // 更新日
                        this.SetDataValue( tgtRow , TranslationDataTable.ColumnNameUpdateDate , System.DateTime.Now.ToString( "yyyy-MM-dd HH:mm:ss" ) );
                    }

                    //コメント設定
                    translateText.Comment = String.Format( "{0} {1}" ,
                        tgtRow[TranslationDataTable.ColumnNameUpdateDate] ,
                        tgtRow[TranslationDataTable.ColumnNameMemo] );


                }
                else
                {
                    //データ無し

                    //新規レコード追加
                    //新規行
                    DataRow newRow = tgtDB.NewRow();
                    DataRow sameTextRow = null; 

                    // 備考
                    this.SetDataValue( newRow , TranslationDataTable.ColumnNameMemo , "新規追加" );

                    //翻訳済みで英語テキストが同じデータがあればそれを使用する
                    sameTextRow = tgtDB.SearchSameText( translateText.SourceText );
                    if ( sameTextRow != null )
                    {
                        translateText.JapaneseText = ( string ) sameTextRow[TranslationDataTable.ColumnNameJapaneseText];

                        this.SetDataValue( newRow , TranslationDataTable.ColumnNameMemo , "新規追加：同じ英語テキストから日本語を取得" );
                        this.SetDataValue( newRow , TranslationDataTable.ColumnNameAutoTrans , sameTextRow[TranslationDataTable.ColumnNameAutoTrans] );
                    }
                    else
                    {
                        //データ無しの場合は自動翻訳
                        if ( translateText.JapaneseText.Equals( "" ) && this.TranslatorApi != null )
                        {
                            //自動翻訳
                            translateText.JapaneseText = this.TranslatorApi.TranslateEnglishToJapanese( translateText.SourceText );
                            if ( !translateText.JapaneseText.Equals( "" ) )
                            {
                                this.SetDataValue( newRow , TranslationDataTable.ColumnNameMemo , "新規追加：自動翻訳で日本語を取得" );
                                this.SetDataValue( newRow , TranslationDataTable.ColumnNameAutoTrans , true );
                            }
                        }
                    }

                    //フォルダ名
                    this.SetDataValue( newRow , TranslationDataTable.ColumnNameDirName , directoryName );
                    // 元テキスト
                    this.SetDataValue( newRow , TranslationDataTable.ColumnNameEnglishText , translateText.SourceText );
                    // 日本語テキスト
                    this.SetDataValue( newRow , TranslationDataTable.ColumnNameJapaneseText , translateText.JapaneseText );
                    // cfgファイル名
                    this.SetDataValue( newRow , TranslationDataTable.ColumnNameCgfFileName , cfgFilename );

                    // 追加日
                    this.SetDataValue( newRow , TranslationDataTable.ColumnNameAddDate , System.DateTime.Now.ToString( "yyyy-MM-dd HH:mm:ss" ) );
                    // 更新日
                    this.SetDataValue( newRow , TranslationDataTable.ColumnNameUpdateDate , System.DateTime.Now.ToString( "yyyy-MM-dd HH:mm:ss" ) );

                    //Order
                    this.SetDataValue( newRow , TranslationDataTable.ColumnNameSortOrder , dataOrder );

                    //存在チェック
                    this.SetDataValue( newRow , TranslationDataTable.ColumnNameExists , true );

                    if ( textData.DataType == ConfigurationFile.TextData.DataType.Part )
                    {
                        // パーツ名
                        this.SetDataValue( newRow , TranslationDataTable.ColumnNamePartName , textData.PartName );
                        // パーツタイトル
                        this.SetDataValue( newRow , TranslationDataTable.ColumnNamePartTitle , textData.PartTitle );
                    }
                    else
                    {
                        // サイエンスレポートID
                        this.SetDataValue( newRow , TranslationDataTable.ColumnNameScienceDefsID , textData.ScienceDefsID );
                        // サイエンスレポートタイトル
                        this.SetDataValue( newRow , TranslationDataTable.ColumnNameScienceDefsTitle , textData.ScienceDefsTitle );
                        // サイエンスレポートResultText
                        this.SetDataValue( newRow , TranslationDataTable.ColumnNameResultText , translateText.Result.ResultText );
                        // サイエンスレポートResultIndex
                        this.SetDataValue( newRow , TranslationDataTable.ColumnNameResultIndex , translateText.Result.ResultIndex );

                    }
                    //新規追加
                    tgtDB.Rows.Add( newRow );

                    //コメント設定
                    translateText.Comment = String.Format( "{0} {1}" ,
                        newRow[TranslationDataTable.ColumnNameUpdateDate] ,
                        newRow[TranslationDataTable.ColumnNameMemo] );
                }


            }
            catch
            {
                throw;
            }


        }

        /// <summary>
        /// 値が異なっていたらデータをセットする。
        /// </summary>
        private void SetDataValue( DataRow row , string columnName, object value )
        {
            if ( !row[columnName].Equals( value ) )
            {
                row[columnName] = value;
            }
        }





    }
}
