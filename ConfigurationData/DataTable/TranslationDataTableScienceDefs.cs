using System;
using System.Data;

namespace KspTsTool2.ConfigurationData.DataTable
{
    /// <summary>
    /// 翻訳データベース処理(サイエンスレポート)
    /// </summary>
    public class TranslationDataTableScienceDefs : TranslationDataTable
    {

        /// <summary>
        /// サイエンスレポートID
        /// </summary>
        private static string ColumnNameScienceDefsID  = @"ID";

        /// <summary>
        /// サイエンスレポートタイトル
        /// </summary>
        private static string ColumnNameScienceDefsTitle  = @"Title";

        /// <summary>
        /// サイエンスレポートResultText
        /// </summary>
        private static string ColumnNameResultText  = @"ResultText";


        /// <summary>
        /// サイエンスレポートResultIndex
        /// </summary>
        private static string ColumnNameResultIndex  = @"ResultIndex";



        /// <summary>
        /// コンストラクタ
        /// </summary>
        public TranslationDataTableScienceDefs() : base( DataType.ScienceDefs )
        {

            // サイエンスレポートID
            {
                var column = this.Columns.Add( ColumnNameScienceDefsID , typeof( System.String ) );
                column.DefaultValue = "";
                column.AllowDBNull = false;
                column.SetOrdinal( SetOrdinalCount++ );
            }
            // サイエンスレポートタイトル
            {
                var column = this.Columns.Add( ColumnNameScienceDefsTitle , typeof( System.String ) );
                column.DefaultValue = "";
                column.AllowDBNull = false;
                column.SetOrdinal( SetOrdinalCount++ );
            }

            // サイエンスレポートResultText
            {
                var column = this.Columns.Add( ColumnNameResultText , typeof( System.String ) );
                column.DefaultValue = "";
                column.AllowDBNull = false;
                column.SetOrdinal( SetOrdinalCount++ );
            }

            // サイエンスレポートResultIndex
            {
                var column = this.Columns.Add( ColumnNameResultIndex , typeof( System.String ) );
                column.DefaultValue = "0";
                column.AllowDBNull = false;
                column.SetOrdinal( SetOrdinalCount++ );
            }

        }

        /// <summary>
        /// DBに存在するかチェック
        /// </summary>
        public override DataRow[] GetExistsDataRow( string directoryName ,
                                                    Text.TextData textData ,
                                                    Translate.TranslateText translateText )
        {
            Text.TextDataScienceDefs textDataScienceDefs = (Text.TextDataScienceDefs)textData;
            Translate.TranslateTextScienceDefs translateTextScienceDefs = ( Translate.TranslateTextScienceDefs ) translateText;

            var where = new System.Text.StringBuilder();
            where.Clear();

            if( directoryName != null)
            {
                where.Append( String.Format( "{0}='{1}'" ,
                                             ColumnNameDirName ,
                                             this.DoubleSiglQrt( directoryName ) ) );
                where.Append( " AND " );
            }


            where.Append( String.Format( "{0}='{1}'" ,
                                         ColumnNameScienceDefsID ,
                                         this.DoubleSiglQrt( textDataScienceDefs.ScienceDefsID ) ) );

            where.Append( " AND " );
            where.Append( String.Format( "{0}='{1}'" ,
                                         ColumnNameResultText ,
                                         this.DoubleSiglQrt( translateTextScienceDefs.Result.ResultText ) ) );

            where.Append( " AND " );
            where.Append( String.Format( "{0}='{1}'" ,
                                         ColumnNameResultIndex ,
                                         translateTextScienceDefs.Result.ResultIndex.ToString() ) );


            return this.Select( where.ToString() );
        }


        /// <summary>
        /// タイトル等データをセット
        /// </summary>
        public override void SetTitleData( DataRow row ,
                                      Text.TextData textData ,
                                      Translate.TranslateText translateText )
        {
            Text.TextDataScienceDefs textDataScienceDefs = (Text.TextDataScienceDefs)textData;
            Translate.TranslateTextScienceDefs translateTextScienceDefs = ( Translate.TranslateTextScienceDefs ) translateText;


            if ( row.RowState == DataRowState.Added || row.RowState == DataRowState.Detached )
            {
                // サイエンスレポートID
                this.SetDataValue( row , ColumnNameScienceDefsID , textDataScienceDefs.ScienceDefsID );

                // サイエンスレポートResultText
                this.SetDataValue( row , ColumnNameResultText , translateTextScienceDefs.Result.ResultText );

                // サイエンスレポートResultIndex
                this.SetDataValue( row , ColumnNameResultIndex , translateTextScienceDefs.Result.ResultIndex );
            }

            // サイエンスレポートタイトル
            this.SetDataValue( row , ColumnNameScienceDefsTitle , textDataScienceDefs.ScienceDefsTitle );


        }



    }
}
