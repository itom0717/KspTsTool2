using System;
using System.Data;

namespace KspTsTool2.ConfigurationData.DataTable
{
    /// <summary>
    /// 翻訳データベース処理(TechTree)
    /// </summary>
    public class TranslationDataTableTechTree : TranslationDataTable
    {

        /// <summary>
        /// ID
        /// </summary>
        private  static string ColumnNameTechTreeID  = @"ID";

        /// <summary>
        /// タイトル
        /// </summary>
        private static string ColumnNameTechTreeTitle  = @"Title";


        /// <summary>
        /// コンストラクタ
        /// </summary>
        public TranslationDataTableTechTree() : base( DataType.TechTree )
        {

            // ID
            {
                var column = this.Columns.Add( ColumnNameTechTreeID , typeof( System.String ) );
                column.DefaultValue = "";
                column.AllowDBNull = false;
                column.SetOrdinal( SetOrdinalCount++ );
            }
            // タイトル
            {
                var column = this.Columns.Add( ColumnNameTechTreeTitle , typeof( System.String ) );
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
            Text.TextDataTechTree textDataTechTree = (Text.TextDataTechTree)textData;

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
                                         ColumnNameTechTreeID ,
                                         this.DoubleSiglQrt( textDataTechTree.TechTreeID ) ) );


            return this.Select( where.ToString() );
        }


        /// <summary>
        /// タイトル等データをセット
        /// </summary>
        public override void SetTitleData( DataRow row ,
                                      Text.TextData textData ,
                                      Translate.TranslateText translateText )
        {
            Text.TextDataTechTree textDataTechTree = (Text.TextDataTechTree)textData;


            if ( row.RowState == DataRowState.Added || row.RowState == DataRowState.Detached )
            {
                // ID
                this.SetDataValue( row , ColumnNameTechTreeID , textDataTechTree.TechTreeID  );
            }

            // タイトル
            this.SetDataValue( row , ColumnNameTechTreeTitle , textDataTechTree.TechTreeTitle  );


        }



    }
}
