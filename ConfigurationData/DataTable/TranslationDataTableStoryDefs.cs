﻿using System;
using System.Data;

namespace KspTsTool2.ConfigurationData.DataTable
{
    /// <summary>
    /// 翻訳データベース処理(StoryDefs)
    /// </summary>
    public class TranslationDataTableStoryDefs : TranslationDataTable
    {

        /// <summary>
        /// タイトル
        /// </summary>
        private static string ColumnNameStoryDefsTitle  = @"Title";

        /// <summary>
        /// NoteText
        /// </summary>
        private static string ColumnNameNoteText  = @"NodeText";

        /// <summary>
        /// NoteIndex
        /// </summary>
        private static string ColumnNameNoteIndex  = @"NodeIndex";


        /// <summary>
        /// コンストラクタ
        /// </summary>
        public TranslationDataTableStoryDefs() : base( DataType.StoryDefs )
        {

            // タイトル
            {
                var column = this.Columns.Add( ColumnNameStoryDefsTitle , typeof( System.String ) );
                column.DefaultValue = "";
                column.AllowDBNull = false;
                column.SetOrdinal( SetOrdinalCount++ );
            }

            // NoteText
            {
                var column = this.Columns.Add( ColumnNameNoteText , typeof( System.String ) );
                column.DefaultValue = "";
                column.AllowDBNull = false;
                column.SetOrdinal( SetOrdinalCount++ );
            }

            // NoteIndex
            {
                var column = this.Columns.Add( ColumnNameNoteIndex , typeof( System.String ) );
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
            Text.TextDataStoryDefs textDataStoryDefs = (Text.TextDataStoryDefs)textData;
            Translate.TranslateTextStoryDefs translateTextStoryDefs = ( Translate.TranslateTextStoryDefs ) translateText;

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
                                         ColumnNameStoryDefsTitle ,
                                         this.DoubleSiglQrt( textDataStoryDefs.TextNodeTitle  ) ) );

            where.Append( " AND " );
            where.Append( String.Format( "{0}='{1}'" ,
                                         ColumnNameNoteText ,
                                         this.DoubleSiglQrt( translateTextStoryDefs.TextNode.TextTitle ) ) );

            where.Append( " AND " );
            where.Append( String.Format( "{0}='{1}'" ,
                                         ColumnNameNoteIndex ,
                                         translateTextStoryDefs.TextNode.TextIndex.ToString() ) );


            return this.Select( where.ToString() );
        }


        /// <summary>
        /// タイトル等データをセット
        /// </summary>
        public override void SetTitleData( DataRow row ,
                                      Text.TextData textData ,
                                      Translate.TranslateText translateText )
        {
            Text.TextDataStoryDefs textDataStoryDefs = (Text.TextDataStoryDefs)textData;
            Translate.TranslateTextStoryDefs translateTextStoryDefs = ( Translate.TranslateTextStoryDefs ) translateText;


            if ( row.RowState == DataRowState.Added || row.RowState == DataRowState.Detached )
            {
                // サイエンスレポートID
                this.SetDataValue( row , ColumnNameStoryDefsTitle , textDataStoryDefs.TextNodeTitle );

                // サイエンスレポートResultText
                this.SetDataValue( row , ColumnNameNoteText , translateTextStoryDefs.TextNode.TextTitle );

                // サイエンスレポートResultIndex
                this.SetDataValue( row , ColumnNameNoteIndex , translateTextStoryDefs.TextNode.TextIndex );
            }

        }




    }
}