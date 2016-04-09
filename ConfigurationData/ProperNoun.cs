using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace KspTsTool2.ConfigurationData
{
    /// <summary>
    /// 固有名詞翻訳機能
    /// </summary>
    public class ProperNoun
    {

        /// <summary>
        /// 置換データクラス
        /// </summary>
        private class ReplaceTable
        {
            /// <summary>
            /// ID
            /// </summary>
            public int ID { get; set; }  = 0;

            /// <summary>
            /// IDText
            /// </summary>
            public string IDText { get { return String.Format("({0:000000000})",this.ID); } }

            /// <summary>
            /// DirectoryName
            /// </summary>
            public string DirectoryName { get; set; } = "";

            /// <summary>
            /// 元テキスト
            /// </summary>
            public string OriginalText { get; set; } = "";

            /// <summary>
            /// 置換テキスト
            /// </summary>
            public string ReplaceText { get; set; } = "";


        }

        /// <summary>
        /// 置換データ
        /// </summary>
        private List<ReplaceTable> ReplaceTableList = new  List<ReplaceTable>();


        /// <summary>
        /// 置換テーブルファイル名
        /// </summary>
        private const string ProperNounTableFileName = "ProperNounTable.txt";


        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ProperNoun()
        {

        }

        /// <summary>
        /// 翻訳テーブル読み込み
        /// </summary>
        public void LoadProperNounTable()
        {
            string fileName = Common.File.CombinePath (Common.File.GetApplicationDirectory(), ProperNounTableFileName);
            if (!Common.File.ExistsFile( fileName ) )
            {
                //ファイル見つからない
                return;
            }

            //コメント削除用
            var regexComment = new Regex( @"#.*$" , RegexOptions.IgnoreCase );

            //DirectoryName用
            var regexDirectoryName = new Regex( @"^\[(.*?)\]" , RegexOptions.IgnoreCase );

            //テキスト用
            var regexText = new Regex( @"^([^\t]*)\t([^\t]*)$" , RegexOptions.IgnoreCase );
            var regexTextNoTab = new Regex( @"^([^\t]*)($|\t)" , RegexOptions.IgnoreCase );


            System.Text.RegularExpressions.MatchCollection mc;

            string nowDirectoryName = "";
            int id = 0;

            //ファイルの解析
            using ( var sr = new System.IO.StreamReader( fileName, System.Text.Encoding.UTF8 ) )
            {
                while ( sr.Peek() > -1 )
                {
                    //1行データ
                    string lineText = sr.ReadLine();

                    //コメント削除
                    lineText = regexComment.Replace( lineText, "" );

                    //前後の空白取り除き
                    lineText = lineText.Trim();
                    if ( lineText.Equals( "" ) )
                    {
                        //データなければ次の行へ
                        continue;
                    }

                    //DirectoryName用
                    mc = regexDirectoryName.Matches( lineText );
                    if ( mc.Count >= 1 )
                    {
                        nowDirectoryName = mc[0].Groups[1].Value;
                        continue;
                    }



                    //テキスト用
                    mc = regexText.Matches( lineText );
                    if ( mc.Count >= 1 )
                    {
                        var replaceTable = new ReplaceTable();

                        replaceTable.ID = id++;
                        replaceTable.DirectoryName = nowDirectoryName;
                        replaceTable.OriginalText = mc[0].Groups[1].Value;
                        replaceTable.ReplaceText = mc[0].Groups[2].Value;

                        this.ReplaceTableList.Add( replaceTable );
                        continue;
                    }

                    //テキスト用(タブなし)
                    mc = regexTextNoTab.Matches( lineText );
                    if ( mc.Count >= 1 )
                    {
                        var replaceTable = new ReplaceTable();

                        replaceTable.ID = id++;
                        replaceTable.DirectoryName = nowDirectoryName;
                        replaceTable.OriginalText = mc[0].Groups[1].Value;
                        replaceTable.ReplaceText = replaceTable.OriginalText;

                        this.ReplaceTableList.Add( replaceTable );
                        continue;
                    }


                }
                //閉じる
                sr.Close();
            }

            //文字の長い順でソートする
            this.ReplaceTableList.Sort( CompareByTextLength );

        }

        /// <summary>
        /// 文字長い順でソート
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        private static int CompareByTextLength( ReplaceTable a, ReplaceTable b )
        {
            return b.OriginalText.Length - a.OriginalText.Length;
        }


        /// <summary>
        /// 名詞部分をダミーのテキストに置換
        /// </summary>
        /// <param name="orgText"></param>
        /// <returns></returns>
        public string ReplaceDummyText(string directoryName, string orgText)
        {
            var replaceText = new  StringBuilder();
            replaceText.Append( orgText );

            foreach ( var replaceTable in this.ReplaceTableList )
            {
                if ( replaceTable.DirectoryName == "" || replaceTable.DirectoryName == directoryName )
                {
                    var regexReplace = new Regex( @"(^|\s*|"")" + "(" + replaceTable.OriginalText + ")" + @"($|\s|""|\.|\?|\!)"  );
                    System.Text.RegularExpressions.MatchCollection mc = regexReplace.Matches( replaceText.ToString() );
                    if ( mc.Count >= 1 )
                    {
                        string checkText = replaceText.ToString();
                        replaceText.Clear();
                        int lastIndex = 0;

                        foreach ( Match item in mc )
                        {
                            replaceText.Append( checkText.Substring( lastIndex, item.Index - lastIndex ) );
                            replaceText.Append( item.Groups[1].Value );//前部分
                            replaceText.Append( replaceTable.IDText );//ID埋め込み
                            replaceText.Append( item.Groups[3].Value );//後ろ部分
                            lastIndex = item.Index + item.Length;
                        }
                        replaceText.Append( checkText.Substring( lastIndex ) );

                    }
                }
            }


            return replaceText.ToString();
        }

        /// <summary>
        /// ダミーのテキストを元に戻す
        /// </summary>
        /// <param name="orgText"></param>
        /// <returns></returns>
        public string ReinstateDummyText( string orgText )
        {
            string replaceText = orgText;
            foreach ( var replaceTable in this.ReplaceTableList )
            {
                replaceText = replaceText.Replace( replaceTable.IDText, replaceTable.ReplaceText );
            }
            return replaceText;
        }





    }
}
