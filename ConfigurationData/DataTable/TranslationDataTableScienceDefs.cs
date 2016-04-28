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
        private static string ColumnNameID  = @"ID";

        /// <summary>
        /// サイエンスレポートタイトル
        /// </summary>
        private static string ColumnNameTitle  = @"Title";

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
                var column = this.Columns.Add( ColumnNameID , typeof( System.String ) );
                column.DefaultValue = "";
                column.AllowDBNull = false;
                column.SetOrdinal( SetOrdinalCount++ );
            }
            // サイエンスレポートタイトル
            {
                var column = this.Columns.Add( ColumnNameTitle , typeof( System.String ) );
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
            Text.TextDataScienceDefs tData = (Text.TextDataScienceDefs)textData;
            Translate.TranslateTextScienceDefs tText = ( Translate.TranslateTextScienceDefs ) translateText;

            var where = new System.Text.StringBuilder();
            where.Clear();

            if ( directoryName != null )
            {
                where.Append( String.Format( "{0}='{1}'" ,
                                             ColumnNameDirName ,
                                             this.DoubleSiglQrt( directoryName ) ) );
                where.Append( " AND " );
            }


            where.Append( String.Format( "{0}='{1}'" ,
                                         ColumnNameID ,
                                         this.DoubleSiglQrt( tData.ID ) ) );

            where.Append( " AND " );
            where.Append( String.Format( "{0}='{1}'" ,
                                         ColumnNameResultText ,
                                         this.DoubleSiglQrt( tText.Result.ResultText ) ) );

            where.Append( " AND " );
            where.Append( String.Format( "{0}='{1}'" ,
                                         ColumnNameResultIndex ,
                                         tText.Result.ResultIndex.ToString() ) );


            return this.Select( where.ToString() );
        }


        /// <summary>
        /// タイトル等データをセット
        /// </summary>
        public override void SetTitleData( DataRow row ,
                                      Text.TextData textData ,
                                      Translate.TranslateText translateText )
        {
            Text.TextDataScienceDefs tData = (Text.TextDataScienceDefs)textData;
            Translate.TranslateTextScienceDefs tText = ( Translate.TranslateTextScienceDefs ) translateText;


            if ( row.RowState == DataRowState.Added || row.RowState == DataRowState.Detached )
            {
                // サイエンスレポートID
                this.SetDataValue( row , ColumnNameID , tData.ID );

                // サイエンスレポートResultText
                this.SetDataValue( row , ColumnNameResultText , tText.Result.ResultText );

                // サイエンスレポートResultIndex
                this.SetDataValue( row , ColumnNameResultIndex , tText.Result.ResultIndex );
            }

            // サイエンスレポートタイトル
            this.SetDataValue( row , ColumnNameTitle , tData.Title );


        }



    }
}
