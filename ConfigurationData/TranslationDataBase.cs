using System;
using System.Collections.Generic;
using System.Data;

namespace KspTsTool2.ConfigurationData
{
    /// <summary>
    /// 翻訳データベース処理
    /// </summary>
    /// <remarks>
    /// TranslationDataBase + DataType名でそれぞれのクラスを継承で作成すること
    /// </remarks>
    public partial class TranslationDataBase
    {
        /// <summary>
        /// 翻訳DataTable(各DataType別）
        /// </summary>
        private Dictionary< DataType, DataTable.TranslationDataTable > TranslationDataTable = new Dictionary< DataType, DataTable.TranslationDataTable>();

        /// <summary>
        /// 自動翻訳処理用
        /// </summary>
        private MicrosoftTranslatorAPI.TranslatorApi TranslatorApi { get; set; } = null;

        /// <summary>
        /// 翻訳データベースのデータ順番カウンタ（各DataType別）
        /// </summary>
        private Dictionary<string, Dictionary< DataType, int > > DataOrder = new Dictionary<string, Dictionary< DataType, int > >();

        /// <summary>
        /// 固有名詞翻訳機能
        /// </summary>
        private ProperNoun ProperNoun { get; set; } = null;


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
                //固有名詞翻訳機能
                this.ProperNoun = new ProperNoun();
                this.ProperNoun.LoadProperNounTable();
            }

            //DataTypeを列挙
            foreach ( DataType dataType in Enum.GetValues( typeof( DataType ) ) )
            {
                //データベーステーブル
                Type t = Type.GetType(typeof( DataTable.TranslationDataTable ).FullName + Enum.GetName(typeof(DataType), dataType));
                object translationDataTable = Activator.CreateInstance(t);
                this.TranslationDataTable.Add( dataType , ( DataTable.TranslationDataTable ) translationDataTable );
            }
        }

        /// <summary>
        /// デストラクタ
        /// </summary>
        ~TranslationDataBase()
        {
            this.TranslatorApi = null;

            //DataTypeを列挙
            foreach ( DataType dataType in Enum.GetValues( typeof( DataType ) ) )
            {
                this.TranslationDataTable[dataType] = null;
            }
            this.TranslationDataTable = null;
        }

        /// <summary>
        /// データベース読み込み
        /// </summary>
        public void Load()
        {
            //DataTypeを列挙
            foreach ( DataType dataType in Enum.GetValues( typeof( DataType ) ) )
            {
                this.TranslationDataTable[dataType].Load();
            }
        }

        /// <summary>
        /// データベース保存
        /// </summary>
        public void Save()
        {
            //DataTypeを列挙
            foreach ( DataType dataType in Enum.GetValues( typeof( DataType ) ) )
            {
                if ( this.TranslationDataTable[dataType] != null )
                {
                    //保存
                    this.TranslationDataTable[dataType].Save();
                }
            }
        }













        /// <summary>
        /// 英語から日本語へ翻訳
        /// </summary>
        /// <param name="TranslateText"></param>
        public void Translate( string directoryName ,
                               string cfgFilename ,
                               Text.TextData textData ,
                               Translate.TranslateText translateText )
        {

            if ( directoryName.Equals( "" )
                || cfgFilename.Equals( "" )
                || translateText.SourceText.Equals( "" ) )
            {
                return;
            }

            //dataOrder
            if ( !this.DataOrder.ContainsKey( directoryName ) )
            {
                this.DataOrder.Add( directoryName , new Dictionary<DataType , int>() );
            }
            if ( !( this.DataOrder[directoryName] ).ContainsKey( textData.DataType ) )
            {
                ( this.DataOrder[directoryName] ).Add( textData.DataType , 1 );
            }
            int dataOrder =  (this.DataOrder[directoryName])[textData.DataType]++;

            try
            {
                //処理対象DB
                DataTable.TranslationDataTable tgtDB = this.TranslationDataTable[textData.DataType];

                //DBに存在するかチェック
                DataRow[] selectRow = tgtDB.GetExistsDataRow(directoryName, textData, translateText);
                if ( selectRow.Length > 0 )
                {
                    //データが存在する

                    DataRow tgtRow = selectRow[0];
                    DataRow sameTextRow = null;

                    //データあり
                    translateText.JapaneseText = ( string ) tgtRow[DataTable.TranslationDataTable.ColumnNameJapaneseText];

                    //存在チェックありに変更
                    tgtDB.SetDataValue( tgtRow , DataTable.TranslationDataTable.ColumnNameExists , true );

                    //Order
                    tgtDB.SetDataValue( tgtRow , DataTable.TranslationDataTable.ColumnNameSortOrder , dataOrder );

                    //変更無しに設定
                    tgtRow.AcceptChanges();





                    //元テキスト変更の場合
                    if ( !translateText.SourceText.Equals( ( string ) tgtRow[DataTable.TranslationDataTable.ColumnNameEnglishText] ) )
                    {
                        //英語テキスト変更
                        tgtDB.SetDataValue( tgtRow , DataTable.TranslationDataTable.ColumnNameEnglishText , translateText.SourceText );
                        tgtDB.SetDataValue( tgtRow , DataTable.TranslationDataTable.ColumnNameMemo , "英語テキスト変更" );

                        //英語テキスト変更のため再翻訳
                        //翻訳済みテキストと同じデータがあればそれを使用する
                        sameTextRow = tgtDB.SearchSameText( translateText.SourceText );
                        if ( sameTextRow != null )
                        {
                            translateText.JapaneseText = ( string ) sameTextRow[DataTable.TranslationDataTable.ColumnNameJapaneseText];

                            //備考
                            tgtDB.SetDataValue( tgtRow , DataTable.TranslationDataTable.ColumnNameMemo , "英語テキスト変更：同じ英語テキストから日本語を取得" );
                            tgtDB.SetDataValue( tgtRow , DataTable.TranslationDataTable.ColumnNameAutoTrans , sameTextRow[DataTable.TranslationDataTable.ColumnNameAutoTrans] );
                        }
                    }
                    else if ( translateText.JapaneseText.Equals( "" ) )
                    {
                        //未翻訳
                        //翻訳済みテキストと同じデータがあればそれを使用する
                        sameTextRow = tgtDB.SearchSameText( translateText.SourceText );
                        if ( sameTextRow != null )
                        {
                            translateText.JapaneseText = ( string ) sameTextRow[DataTable.TranslationDataTable.ColumnNameJapaneseText];

                            //備考
                            tgtDB.SetDataValue( tgtRow , DataTable.TranslationDataTable.ColumnNameMemo , "同じ英語テキストから日本語を取得" );
                            tgtDB.SetDataValue( tgtRow , DataTable.TranslationDataTable.ColumnNameAutoTrans , sameTextRow[DataTable.TranslationDataTable.ColumnNameAutoTrans] );
                        }
                    }
                    else if ( tgtRow[DataTable.TranslationDataTable.ColumnNameAutoTrans].Equals( true ) )
                    {
                        //自動翻訳の場合で、英語データが同じで手動翻訳の場合はそちらを使用する
                        sameTextRow = tgtDB.SearchSameText( translateText.SourceText );
                        if ( sameTextRow != null && sameTextRow[DataTable.TranslationDataTable.ColumnNameAutoTrans].Equals( false ) )
                        {
                            translateText.JapaneseText = ( string ) sameTextRow[DataTable.TranslationDataTable.ColumnNameJapaneseText];

                            //備考
                            tgtDB.SetDataValue( tgtRow , DataTable.TranslationDataTable.ColumnNameMemo , "同じ英語テキストから日本語を取得" );
                            tgtDB.SetDataValue( tgtRow , DataTable.TranslationDataTable.ColumnNameAutoTrans , sameTextRow[DataTable.TranslationDataTable.ColumnNameAutoTrans] );
                        }
                    }


                    //空欄の場合や元テキストが変更され、同じ翻訳データが無い場合は再翻訳
                    if ( translateText.JapaneseText.Equals( "" ) && this.TranslatorApi != null )
                    {
                        //自動翻訳
                        translateText.JapaneseText = this.ProperNoun.ReinstateDummyText(
                                                                    this.TranslatorApi.TranslateEnglishToJapanese(
                                                                            this.ProperNoun.ReplaceDummyText( directoryName , translateText.SourceText , textData.DataType )
                                                                                    )
                                                                                 );
                        if ( !translateText.JapaneseText.Equals( "" ) )
                        {
                            tgtDB.SetDataValue( tgtRow , DataTable.TranslationDataTable.ColumnNameMemo , "自動翻訳で日本語を取得" );
                            tgtDB.SetDataValue( tgtRow , DataTable.TranslationDataTable.ColumnNameAutoTrans , true );
                        }
                    }

                    //日本語が変更されていたらデータ保存
                    tgtDB.SetDataValue( tgtRow , DataTable.TranslationDataTable.ColumnNameJapaneseText , translateText.JapaneseText );

                    //タイトル等データをセット
                    tgtDB.SetTitleData( tgtRow , textData , translateText );


                    if ( tgtRow.RowState != DataRowState.Unchanged )
                    {
                        //変更された
                        // 更新日
                        tgtDB.SetDataValue( tgtRow , DataTable.TranslationDataTable.ColumnNameUpdateDate , System.DateTime.Now.ToString( "yyyy-MM-dd HH:mm:ss" ) );
                    }

                    //コメント設定
                    translateText.Comment = String.Format( "{0} {1}" ,
                        tgtRow[DataTable.TranslationDataTable.ColumnNameUpdateDate] ,
                        tgtRow[DataTable.TranslationDataTable.ColumnNameMemo] );


                }
                else
                {
                    //データ無し

                    //新規レコード追加
                    //新規行
                    DataRow newRow = tgtDB.NewRow();
                    DataRow sameTextRow = null;

                    // 備考
                    tgtDB.SetDataValue( newRow , DataTable.TranslationDataTable.ColumnNameMemo , "新規追加" );

                    //翻訳済みで英語テキストが同じデータがあればそれを使用する
                    sameTextRow = tgtDB.SearchSameText( translateText.SourceText );
                    if ( sameTextRow != null )
                    {
                        translateText.JapaneseText = ( string ) sameTextRow[DataTable.TranslationDataTable.ColumnNameJapaneseText];

                        tgtDB.SetDataValue( newRow , DataTable.TranslationDataTable.ColumnNameMemo , "新規追加：同じ英語テキストから日本語を取得" );
                        tgtDB.SetDataValue( newRow , DataTable.TranslationDataTable.ColumnNameAutoTrans , sameTextRow[DataTable.TranslationDataTable.ColumnNameAutoTrans] );
                    }
                    else
                    {
                        //データ無しの場合は自動翻訳
                        if ( translateText.JapaneseText.Equals( "" ) && this.TranslatorApi != null )
                        {
                            //自動翻訳
                            translateText.JapaneseText = this.ProperNoun.ReinstateDummyText(
                                                                        this.TranslatorApi.TranslateEnglishToJapanese(
                                                                                this.ProperNoun.ReplaceDummyText( directoryName , translateText.SourceText , textData.DataType )
                                                                                        )
                                                                                     );

                            if ( !translateText.JapaneseText.Equals( "" ) )
                            {
                                tgtDB.SetDataValue( newRow , DataTable.TranslationDataTable.ColumnNameMemo , "新規追加：自動翻訳で日本語を取得" );
                                tgtDB.SetDataValue( newRow , DataTable.TranslationDataTable.ColumnNameAutoTrans , true );
                            }
                        }
                    }

                    //フォルダ名
                    tgtDB.SetDataValue( newRow , DataTable.TranslationDataTable.ColumnNameDirName , directoryName );
                    // 元テキスト
                    tgtDB.SetDataValue( newRow , DataTable.TranslationDataTable.ColumnNameEnglishText , translateText.SourceText );
                    // 日本語テキスト
                    tgtDB.SetDataValue( newRow , DataTable.TranslationDataTable.ColumnNameJapaneseText , translateText.JapaneseText );
                    // cfgファイル名
                    tgtDB.SetDataValue( newRow , DataTable.TranslationDataTable.ColumnNameCgfFileName , cfgFilename );

                    // 追加日
                    tgtDB.SetDataValue( newRow , DataTable.TranslationDataTable.ColumnNameAddDate , System.DateTime.Now.ToString( "yyyy-MM-dd HH:mm:ss" ) );
                    // 更新日
                    tgtDB.SetDataValue( newRow , DataTable.TranslationDataTable.ColumnNameUpdateDate , System.DateTime.Now.ToString( "yyyy-MM-dd HH:mm:ss" ) );

                    //Order
                    tgtDB.SetDataValue( newRow , DataTable.TranslationDataTable.ColumnNameSortOrder , dataOrder );

                    //存在チェック
                    tgtDB.SetDataValue( newRow , DataTable.TranslationDataTable.ColumnNameExists , true );

                    //タイトル等データをセット
                    tgtDB.SetTitleData( newRow , textData , translateText );

                    //新規追加
                    tgtDB.Rows.Add( newRow );

                    //コメント設定
                    translateText.Comment = String.Format( "{0} {1}" ,
                        newRow[DataTable.TranslationDataTable.ColumnNameUpdateDate] ,
                        newRow[DataTable.TranslationDataTable.ColumnNameMemo] );
                }


            }
            catch
            {
                throw;
            }


        }


    }
}
