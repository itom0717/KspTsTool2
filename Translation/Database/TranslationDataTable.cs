using System;
using System.Data;
using System.Xml;


namespace KspTsTool2.Translation.Database
{
    /// <summary>
    /// データテーブル
    /// </summary>
    public class TranslationDataTable : DataTable
    {





        /// <summary>
        /// フォルダ名
        /// </summary>
        public static string ColumnNameDirName  = @"DirName";

        /// <summary>
        /// cfgファイル名
        /// </summary>
        public static string ColumnNameCgfFileName  = @"CfgFile";




        /// <summary>
        /// パーツ名
        /// </summary>
        public static string ColumnNamePartName  = @"Name";

        /// <summary>
        /// パーツタイトル
        /// </summary>
        public static string ColumnNamePartTitle  = @"Title";






        /// <summary>
        /// サイエンスレポートID
        /// </summary>
        public static string ColumnNameScienceDefsID  = @"ID";

        /// <summary>
        /// サイエンスレポートタイトル
        /// </summary>
        public static string ColumnNameScienceDefsTitle  = @"Title";

        /// <summary>
        /// サイエンスレポートResultText
        /// </summary>
        public static string ColumnNameResultText  = @"ResultText";


        /// <summary>
        /// サイエンスレポートResultIndex
        /// </summary>
        public static string ColumnNameResultIndex  = @"ResultIndex";








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
        /// データタイプ
        /// </summary>
        private ConfigurationFile.TextData.DataType dataType;


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
        /// コンストラクタ
        /// </summary>
        /// <param name=""></param>
        public TranslationDataTable()
        {
            //GetChangesで必要なデフォルトコンストラクタ
        }


        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <remarks></remarks>
        public TranslationDataTable( ConfigurationFile.TextData.DataType dataType )
        {
            //データタイプ
            this.dataType = dataType;

            if ( this.dataType == ConfigurationFile.TextData.DataType.Part )
            {
                //tableName設定
                this.TableName = @"TranslationData";
            }
            else
            {
                //tableName設定
                this.TableName = @"TranslationData";
            }


            // フォルダ名
            {
                var column = this.Columns.Add( ColumnNameDirName , typeof( System.String ) );
                column.DefaultValue = "";
                column.AllowDBNull = false;

                column.ColumnMapping = MappingType.Attribute;

            }
            // cfgファイル名
            {
                var column = this.Columns.Add( ColumnNameCgfFileName , typeof( System.String ) );
                column.DefaultValue = "";
                column.AllowDBNull = false;

                column.ColumnMapping = MappingType.Attribute;

            }


            if ( this.dataType == ConfigurationFile.TextData.DataType.Part )
            {
                // パーツ名
                {
                    var column = this.Columns.Add( ColumnNamePartName , typeof( System.String ) );
                    column.DefaultValue = "";
                    column.AllowDBNull = false;
                }
                // パーツタイトル
                {
                    var column = this.Columns.Add( ColumnNamePartTitle , typeof( System.String ) );
                    column.DefaultValue = "";
                    column.AllowDBNull = false;
                }

            }
            else
            {

                // パーツ名/サイエンスレポートID
                {
                    var column = this.Columns.Add( ColumnNameScienceDefsID , typeof( System.String ) );
                    column.DefaultValue = "";
                    column.AllowDBNull = false;
                }
                // パーツタイトル/サイエンスレポートタイトル
                {
                    var column = this.Columns.Add( ColumnNameScienceDefsTitle , typeof( System.String ) );
                    column.DefaultValue = "";
                    column.AllowDBNull = false;
                }

                // サイエンスレポートResultText
                {
                    var column = this.Columns.Add( ColumnNameResultText , typeof( System.String ) );
                    column.DefaultValue = "";
                    column.AllowDBNull = false;
                }

                // サイエンスレポートResultIndex
                {
                    var column = this.Columns.Add( ColumnNameResultIndex , typeof( System.String ) );
                    column.DefaultValue = "0";
                    column.AllowDBNull = false;
                }

            }


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
        public void Load( string dbFileName )
        {
            if ( Common.File.ExistsFile( dbFileName ) )
            {
                //ファイル存在あり
                using ( System.IO.StreamReader sr = new System.IO.StreamReader( dbFileName ,
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
        public void Save( string dbFileName )
        {
            if ( this.GetChanges() == null )
            {
                //変更無し
                return;
            }


            //*.tmpファイル名生成
            string tmpFilename = Common.File.GetWithoutExtension( dbFileName ) + ".tmp";
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
                sortOrder.Append(ColumnNameDirName + " ASC");
                sortOrder.Append( ",");
                sortOrder.Append( ColumnNameExists + " DESC");
                sortOrder.Append( ",");
                sortOrder.Append( ColumnNameSortOrder + " ASC");


                // dtのスキーマや制約をコピーしたDataTableを作成します。
                DataTable sortTable =  this.Clone();
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
            if ( Common.File.ExistsFile( dbFileName ) )
            {
                for ( int i = 9; i >= 0; i-- )
                {
                    string file1 = Common.File.GetWithoutExtension( dbFileName ) + (i == 0 ? ".bak" : ".bk" + i.ToString());
                    if ( Common.File.ExistsFile( file1 ) )
                    {
                        //存在したら消す
                        Common.File.DeleteFile( file1 );
                    }
                    string file2 = Common.File.GetWithoutExtension( dbFileName );
                    switch ( i - 1 )
                    {
                        case -1:
                            file2 += "." + Common.File.GetExtension( dbFileName );
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
            Common.File.MoveFile( tmpFilename , dbFileName );

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





    }
}