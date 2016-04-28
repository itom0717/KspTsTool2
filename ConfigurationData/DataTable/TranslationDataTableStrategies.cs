using System;
using System.Data;


namespace KspTsTool2.ConfigurationData.DataTable
{
    /// <summary>
    /// 翻訳データベース処理(Strategies)
    /// </summary>
    public class TranslationDataTableStrategies : TranslationDataTable
    {


        /// <summary>
        /// name
        /// </summary>
        private static string ColumnNameName  = @"Name";

        /// <summary>
        /// title
        /// </summary>
        private static string ColumnNameTitle  = @"Title";



        /// <summary>
        /// コンストラクタ
        /// </summary>
        public TranslationDataTableStrategies() : base( DataType.Strategies )
        {
            // name
            {
                var column = this.Columns.Add( ColumnNameName , typeof( System.String ) );
                column.DefaultValue = "";
                column.AllowDBNull = false;
                column.SetOrdinal( SetOrdinalCount++ );
            }
            // title
            {
                var column = this.Columns.Add( ColumnNameTitle , typeof( System.String ) );
                column.DefaultValue = "";
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
            Text.TextDataStrategies tData = ( Text.TextDataStrategies ) textData;

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
                                         ColumnNameName ,
                                         this.DoubleSiglQrt( tData.Name ) ) );


            return this.Select( where.ToString() );
        }


        /// <summary>
        /// タイトル等データをセット
        /// </summary>
        public override void SetTitleData( DataRow row ,
                                      Text.TextData textData ,
                                      Translate.TranslateText translateText )
        {
            Text.TextDataStrategies tData = ( Text.TextDataStrategies ) textData;

            if ( row.RowState == DataRowState.Added || row.RowState == DataRowState.Detached )
            {
                // パーツ名
                this.SetDataValue( row , ColumnNameName , tData.Name );
            }


            // パーツタイトル
            this.SetDataValue( row , ColumnNameTitle , tData.Title );
        }


    }
}
