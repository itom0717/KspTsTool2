using System;
using System.Data;
using System.Xml;


namespace KspTsTool2.ConfigurationData.DataTable
{
    /// <summary>
    /// データテーブル
    /// </summary>
    /// <remarks>
    /// TranslationDataTable + DataType名でそれぞれのクラスを継承で作成すること
    /// </remarks>
    public abstract class TranslationDataTable : System.Data.DataTable
    {
        /// <summary>
        /// 翻訳DataTableファイル名
        /// </summary>
        protected string TranslationDataTableFilePath = "";





        /// <summary>
        /// フォルダ名
        /// </summary>
        public static string ColumnNameDirName  = @"DirName";

        /// <summary>
        /// cfgファイル名
        /// </summary>
        public static string ColumnNameCgfFileName  = @"CfgFile";


        /// <summary>
        /// 元テキスト
        /// </summary>
        public static string ColumnNameEnglishText  = @"EnglishText";

        /// <summary>
        /// 日本語テキスト
        /// </summary>
        public static string ColumnNameJapaneseText = @"JapaneseText";

        /// <summary>
        /// 備考
        /// </summary>
        public static string ColumnNameMemo  = @"Memo";



        /// <summary>
        /// 追加日
        /// </summary>
        public static string ColumnNameAddDate  = @"AddDate";

        /// <summary>
        /// 更新日
        /// </summary>
        public static string ColumnNameUpdateDate  = @"UpdateDate";



        /// <summary>
        /// SortOrder
        /// </summary>
        public static string ColumnNameSortOrder  = @"SortOrder";

        /// <summary>
        /// 存在チェック
        /// </summary>
        public static string ColumnNameExists  = @"Exists";

        /// <summary>
        /// 自動翻訳データ
        /// </summary>
        public static string ColumnNameAutoTrans  = @"AutoTrans";


        /// <summary>
        /// カラムの順番定義用（DataTypeの項目を追加する場所の指定用）
        /// </summary>
        protected int SetOrdinalCount = 0;


        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <remarks></remarks>
        public TranslationDataTable( DataType dataType )
        {

            //データベース保存ファイル名フルパス
            this.TranslationDataTableFilePath =
                Common.File.CombinePath( Common.File.GetApplicationDirectory() ,
                                         Enum.GetName( typeof( DataType ) , dataType )
                                         + "TranslationDB.xml" );


            //tableName設定
            this.TableName = @"TranslationData";


            // フォルダ名
            {
                var column = this.Columns.Add( ColumnNameDirName , typeof( System.String ) );
                column.DefaultValue = "";
                column.AllowDBNull = false;

                column.ColumnMapping = MappingType.Attribute;
                column.SetOrdinal( SetOrdinalCount++ );
            }

            // cfgファイル名
            {
                var column = this.Columns.Add( ColumnNameCgfFileName , typeof( System.String ) );
                column.DefaultValue = "";
                column.AllowDBNull = false;

                column.ColumnMapping = MappingType.Attribute;
                column.SetOrdinal( SetOrdinalCount++ );
            }



            //ここの間に各DataTypeの項目を追加するため、SetOrdinalCountのカウントは以下不要





            // 英語テキスト
            {
                var column = this.Columns.Add( ColumnNameEnglishText , typeof( System.String ) );
                column.DefaultValue = "";
                column.AllowDBNull = false;
            }

            // 日本語テキスト
            {
                var column = this.Columns.Add( ColumnNameJapaneseText , typeof( System.String ) );
                column.DefaultValue = "";
                column.AllowDBNull = false;
            }

            // 備考
            {
                var column = this.Columns.Add( ColumnNameMemo , typeof( System.String ) );
                column.DefaultValue = "";
                column.AllowDBNull = false;
            }


            // 追加日
            {
                var column = this.Columns.Add( ColumnNameAddDate , typeof( System.String ) );
                column.DefaultValue = "";
                column.AllowDBNull = false;

                column.ColumnMapping = MappingType.Attribute;
            }


            // 更新日
            {
                var column = this.Columns.Add( ColumnNameUpdateDate , typeof( System.String ) );
                column.DefaultValue = "";
                column.AllowDBNull = false;

                column.ColumnMapping = MappingType.Attribute;
            }


            // SortOrder
            {
                var column = this.Columns.Add( ColumnNameSortOrder , typeof( System.Int32 ) );
                column.DefaultValue = -1;
                column.AllowDBNull = false;
                column.ColumnMapping = MappingType.Attribute;
            }


            // 存在チェック
            {
                var column = this.Columns.Add( ColumnNameExists , typeof( System.Boolean  ) );
                column.DefaultValue = false;
                column.AllowDBNull = false;
                column.ColumnMapping = MappingType.Hidden;
            }


            // 自動翻訳データ
            {
                var column = this.Columns.Add( ColumnNameAutoTrans , typeof( System.Boolean  ) );
                column.DefaultValue = false;
                column.AllowDBNull = false;
                column.ColumnMapping = MappingType.Attribute;
            }
        }



        /// <summary>
        /// データベース読み込み
        /// </summary>
        public void Load()
        {
            if ( Common.File.ExistsFile( this.TranslationDataTableFilePath ) )
            {
                //ファイル存在あり
                using ( System.IO.StreamReader sr = new System.IO.StreamReader( this.TranslationDataTableFilePath ,
                                                                                new System.Text.UTF8Encoding( false ) ) )
                {
                    this.ReadXml( sr );
                    sr.Close();
                }
            }

            //変更無しに設定
            this.AcceptChanges();
        }




        /// <summary>
        /// データベース保存
        /// </summary>
        public void Save()
        {
            if ( this.GetChanges() == null )
            {
                //変更無し
                return;
            }


            //*.tmpファイル名生成
            string tmpFilename = Common.File.GetWithoutExtension( this.TranslationDataTableFilePath ) + ".tmp";
            if ( Common.File.ExistsFile( tmpFilename ) )
            {
                //*.tmpが存在したら消す
                Common.File.DeleteFile( tmpFilename );
            }


            //シリアライズしてXMLで保存
            //一旦*.tmpで保存
            using ( XmlTextWriter sw = new XmlTextWriter( tmpFilename ,
                                                          new System.Text.UTF8Encoding( false ) ) )
            {
                sw.Formatting = Formatting.Indented;
                sw.WriteStartDocument();

                //ソートしてXML保存する
                var sortOrder = new System.Text.StringBuilder();
                sortOrder.Append( ColumnNameDirName + " ASC" );
                sortOrder.Append( "," );
                sortOrder.Append( ColumnNameExists + " DESC" );
                sortOrder.Append( "," );
                sortOrder.Append( ColumnNameSortOrder + " ASC" );


                // dtのスキーマや制約をコピーしたDataTableを作成します。
                System.Data.DataTable sortTable =  this.Clone();
                DataRow[] rows      =  this.Select(null, sortOrder.ToString());
                foreach ( DataRow row in rows )
                {
                    DataRow addRow = sortTable.NewRow();
                    // カラム情報をコピーします。
                    addRow.ItemArray = row.ItemArray;
                    // DataTableに格納します。
                    sortTable.Rows.Add( addRow );
                }
                sortTable.WriteXml( sw );

                sw.WriteEndDocument();
                sw.Close();
            }


            //Bakファイル作成
            if ( Common.File.ExistsFile( this.TranslationDataTableFilePath ) )
            {
                for ( int i = 9; i >= 0; i-- )
                {
                    string file1 = Common.File.GetWithoutExtension( this.TranslationDataTableFilePath ) + (i == 0 ? ".bak" : ".bk" + i.ToString());
                    if ( Common.File.ExistsFile( file1 ) )
                    {
                        //存在したら消す
                        Common.File.DeleteFile( file1 );
                    }
                    string file2 = Common.File.GetWithoutExtension( this.TranslationDataTableFilePath );
                    switch ( i - 1 )
                    {
                        case -1:
                            file2 += "." + Common.File.GetExtension( this.TranslationDataTableFilePath );
                            break;
                        case 0:
                            file2 += ".bak";
                            break;
                        default:
                            file2 += ".bk" + ( i - 1 ).ToString();
                            break;
                    }
                    if ( Common.File.ExistsFile( file2 ) )
                    {
                        //存在したらファイル名変更
                        Common.File.MoveFile( file2 , file1 );
                    }
                }
            }


            //*.tmpを*.xmlへ変更
            Common.File.MoveFile( tmpFilename , this.TranslationDataTableFilePath );

            //正常に保存できたら、変更無しに設定
            this.AcceptChanges();
        }


        /// <summary>
        /// 翻訳済みのテキストと同じデータがあるか検索し、あればそのDataRowを返す
        /// </summary>
        /// <param name="srcText"></param>
        /// <returns></returns>
        public DataRow SearchSameText( string srcText )
        {
            DataRow returnRow = null;
            var where = new System.Text.StringBuilder();
            where.Append( String.Format( "{0}='{1}'" , TranslationDataTable.ColumnNameEnglishText ,
                                                       this.DoubleSiglQrt( srcText ) ) );

            var sortOrder = new System.Text.StringBuilder();
            sortOrder.Append( ColumnNameAutoTrans + " ASC" );//手動翻訳が優先

            DataRow[] selectRow = this.Select(where.ToString(),sortOrder.ToString());
            foreach ( DataRow row in selectRow )
            {
                if ( !row[TranslationDataTable.ColumnNameJapaneseText].ToString().Trim().Equals( "" ) )
                {
                    returnRow = row;
                    break;
                }
            }

            return returnRow;
        }

        /// <summary>
        /// シングルコーテーションを２つにする。
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public string DoubleSiglQrt( string text )
        {
            string returnValue = text;
            returnValue = returnValue.Replace( "'" , "''" );
            return returnValue;
        }


        /// <summary>
        /// 値が異なっていたらデータをセットする。
        /// </summary>
        public void SetDataValue( DataRow row , string columnName , object value )
        {
            if ( !row[columnName].Equals( value ) )
            {
                row[columnName] = value;
            }
        }

        /// <summary>
        /// DBに存在するかチェック
        /// </summary>
        public abstract DataRow[] GetExistsDataRow( string directoryName ,
                                                    Text.TextData textData ,
                                                    Translate.TranslateText translateText );

        /// <summary>
        /// タイトル等データをセット
        /// </summary>
        public abstract void SetTitleData( DataRow row ,
                             Text.TextData textData ,
                             Translate.TranslateText translateText );


    }
}