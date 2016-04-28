using System;
using System.Data;


namespace KspTsTool2.ConfigurationData.DataTable
{
    /// <summary>
    /// 翻訳データベース処理(Departments)
    /// </summary>
    public class TranslationDataTableDepartments : TranslationDataTable
    {


        /// <summary>
        /// Name
        /// </summary>
        private static string ColumnNameName  = @"Name";


        /// <summary>
        /// コンストラクタ
        /// </summary>
        public TranslationDataTableDepartments() : base( DataType.Departments )
        {
            // name
            {
                var column = this.Columns.Add( ColumnNameName , typeof( System.String ) );
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
            Text.TextDataDepartments tData = ( Text.TextDataDepartments ) textData;

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
            Text.TextDataDepartments tData = ( Text.TextDataDepartments ) textData;

            if ( row.RowState == DataRowState.Added || row.RowState == DataRowState.Detached )
            {
                // パーツ名
                this.SetDataValue( row , ColumnNameName , tData.Name );
            }

        }

    }
}
