using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace KspTsTool2.ConfigurationFile.NodeInfo
{
    /// <summary>
    /// EXPERIMENT_DEFINITION解析
    /// </summary>
    public class ScienceDefsNode
    {


        /// <summary>
        ///  EXPERIMENT_DEFINITION用正規表現
        /// </summary>
        private Regex RegexExpDef = new Regex(@"^EXPERIMENT_DEFINITION($|\s)", RegexOptions.IgnoreCase);

        /// <summary>
        ///  RESULTS用正規表現
        /// </summary>
        private Regex RegexResult = new Regex(@"^RESULTS($|\s)", RegexOptions.IgnoreCase);

        /// <summary>
        ///  id用正規表現
        /// </summary>
        private Regex RegexID = new Regex(@"^id\s*=\s*(.+)$", RegexOptions.IgnoreCase);

        /// <summary>
        ///  title用正規表現
        /// </summary>
        private Regex RegexTitle = new Regex(@"^title\s*=\s*(.+)$", RegexOptions.IgnoreCase);

        /// <summary>
        ///  key＆text用正規表現
        /// </summary>
        private Regex RegexResultText = new Regex(@"^([^=]+)\s*=\s*(.+)$", RegexOptions.IgnoreCase);




        /// <summary>
        ///  EXPERIMENT_DEFINITION用正規表現
        /// </summary>
        private Regex RegexExpDefImport = new Regex(@"^@EXPERIMENT_DEFINITION\s*:\s*HAS\[\s*#id\[\s*(.[^\}]+)\s*\]\s*\]($|\s)", RegexOptions.IgnoreCase);

        /// <summary>
        ///  RESULTS用正規表現
        /// </summary>
        private Regex RegexResultImport = new Regex(@"^@RESULTS($|\s)", RegexOptions.IgnoreCase);

        /// <summary>
        ///  key＆text用正規表現
        /// </summary>
        private Regex RegexResultTextImport = new Regex(@"^@([^=,]+),(\d+)\s*=\s*(.+)", RegexOptions.IgnoreCase);



        /// <summary>
        /// 翻訳テキストデータ
        /// </summary>
        private List<TextData.TextData> TextDataList { get; set; }


        /// <summary>
        /// KeyIndex
        /// </summary>
        private Common.HashtableEx KeyIndex { get; set; } = new Common.HashtableEx();


        /// <summary>
        /// ノードが見つかった
        /// </summary>
        /// <returns></returns>
        private bool FindNode { get; set; } = false;

        /// <summary>
        /// ノードの中にはいった
        /// </summary>
        /// <returns></returns>
        private bool InsideNode { get; set; } = false;

        /// <summary>
        /// Resultノードが見つかった
        /// </summary>
        /// <returns></returns>あ
        private bool FindNodeResult { get; set; } = false;

        /// <summary>
        /// Resultノードの中にはいった
        /// </summary>
        /// <returns></returns>
        private bool InsideNodeResult { get; set; } = false;

        /// <summary>
        /// サイエンスレポートID
        /// </summary>
        private string ScienceDefsID;

        /// <summary>
        /// サイエンスレポートタイトル
        /// </summary>
        private string ScienceDefsTitle;

        /// <summary>
        /// サイエンスレポートタイトル
        /// </summary>
        private List<TextData.TranslateText> ScienceDefsResultText { get; set; } = new List<TextData.TranslateText>();


        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ScienceDefsNode( List<TextData.TextData> textDataList )
        {
            this.FindNode = false;
            this.InsideNode = false;
            this.FindNodeResult = false;
            this.InsideNodeResult = false;
            this.TextDataList = textDataList;
            this.TextDataList.Clear();
            this.KeyIndex.Clear();
        }

        /// <summary>
        /// 1ブロック解析
        /// </summary>
        /// <param name="blockText"></param>
        public void AnalysisOneBlock( int nestLevel ,
                                     string blockText )
        {

            //正規表現用
            System.Text.RegularExpressions.MatchCollection mc;


            //EXPERIMENT_DEFINITIONノードが見つかったか？
            if ( !this.InsideNode && nestLevel == 0 && RegexExpDef.IsMatch( blockText ) )
            {
                //EXPERIMENT_DEFINITIONノードが見つかった
                this.FindNode = true;
                this.InsideNode = false;
                this.FindNodeResult = false;
                this.InsideNodeResult = false;

                this.ScienceDefsID = "";
                this.ScienceDefsTitle = "";
                this.ScienceDefsResultText.Clear();
                this.KeyIndex.Clear();
                return;
            }

            //ノードの中に入ったか確認
            if ( this.FindNode && !this.InsideNode && nestLevel == 1 )
            {
                //EXPERIMENT_DEFINITIONノードの中に入った
                this.InsideNode = true;
            }

            if ( this.InsideNode && nestLevel == 0 )
            {
                //EXPERIMENT_DEFINITIONノードの外に出た
                this.FindNode = false;
                this.InsideNode = false;
                this.FindNodeResult = false;
                this.InsideNodeResult = false;

                //翻訳元テキストデータを記憶する
                if ( !this.ScienceDefsID.Equals( "" ) )
                {
                    var textData = new TextData.TextData();
                    textData.SetScienceDefsText( this.ScienceDefsID , this.ScienceDefsTitle , this.ScienceDefsResultText );

                    this.TextDataList.Add( textData );
                }
            }


            if ( this.InsideNode && !this.InsideNodeResult && nestLevel == 1 )
            {
                // サイエンスレポートID
                mc = this.RegexID.Matches( blockText );
                if ( mc.Count >= 1 )
                {
                    this.ScienceDefsID = mc[0].Groups[1].Value;
                }

                //サイエンスレポートタイトル
                mc = this.RegexTitle.Matches( blockText );
                if ( mc.Count >= 1 )
                {
                    this.ScienceDefsTitle = mc[0].Groups[1].Value;
                }
            }


            //Resultノードが見つかったか？
            if ( this.InsideNode && !this.InsideNodeResult && nestLevel == 1 && RegexResult.IsMatch( blockText ) )
            {
                //Resultノードが見つかった
                this.FindNodeResult = true;
                this.InsideNodeResult = false;
            }

            if ( this.FindNodeResult && !this.InsideNodeResult && nestLevel == 2 )
            {
                //Resultノードの中に入った
                this.InsideNodeResult = true;
            }

            if ( this.InsideNodeResult && nestLevel <= 1 )
            {
                //Resultノードの外に出た
                this.FindNodeResult = false;
                this.InsideNodeResult = false;
            }


            if ( this.InsideNodeResult && nestLevel == 2 )
            {
                // サイエンスレポート説明
                mc = this.RegexResultText.Matches( blockText );
                if ( mc.Count >= 1 )
                {
                    string resultText  = mc[0].Groups[1].Value.Trim();
                    int    resultIndex = 0;
                    string text     = mc[0].Groups[2].Value.Trim();

                    if ( this.KeyIndex.ContainsKey( resultText ) )
                    {
                        resultIndex = ( int ) this.KeyIndex[resultText];
                        resultIndex++;
                        this.KeyIndex[resultText] = resultIndex;
                    }
                    else
                    {
                        resultIndex = 0;
                        this.KeyIndex.Add( resultText , resultIndex );
                    }

                    this.ScienceDefsResultText.Add( new TextData.TranslateText( resultText ,
                                                                                resultIndex ,
                                                                                text ) );
                }
            }
        }




        /// <summary>
        /// 1ブロック解析（インポートモード時）
        /// </summary>
        /// <param name="blockText"></param>
        public void AnalysisOneBlockImport( int nestLevel ,
                                     string blockText )
        {
            //正規表現用
            System.Text.RegularExpressions.MatchCollection mc;


            //EXPERIMENT_DEFINITIONノードが見つかったか？
            mc = this.RegexExpDefImport.Matches( blockText );
            if ( !this.InsideNode && nestLevel == 0 && mc.Count >= 1 )
            {
                //EXPERIMENT_DEFINITIONノードが見つかった
                this.FindNode = true;
                this.InsideNode = false;
                this.FindNodeResult = false;
                this.InsideNodeResult = false;

                this.ScienceDefsID = mc[ 0 ].Groups[ 1 ].Value;
                this.ScienceDefsResultText.Clear();
                this.KeyIndex.Clear();
                return;
            }

            //ノードの中に入ったか確認
            if ( this.FindNode && !this.InsideNode && nestLevel == 1 )
            {
                //EXPERIMENT_DEFINITIONノードの中に入った
                this.InsideNode = true;
            }

            if ( this.InsideNode && nestLevel == 0 )
            {
                //EXPERIMENT_DEFINITIONノードの外に出た
                this.FindNode = false;
                this.InsideNode = false;
                this.FindNodeResult = false;
                this.InsideNodeResult = false;

                //翻訳元テキストデータを記憶する
                if ( !this.ScienceDefsID.Equals( "" ) )
                {
                    var textData = new TextData.TextData();
                    textData.SetScienceDefsText( this.ScienceDefsID , "", this.ScienceDefsResultText );

                    this.TextDataList.Add( textData );
                }
            }



            //Resultノードが見つかったか？
            if ( this.InsideNode && !this.InsideNodeResult && nestLevel == 1 && RegexResultImport.IsMatch( blockText ) )
            {
                //Resultノードが見つかった
                this.FindNodeResult = true;
                this.InsideNodeResult = false;
            }

            if ( this.FindNodeResult && !this.InsideNodeResult && nestLevel == 2 )
            {
                //Resultノードの中に入った
                this.InsideNodeResult = true;
            }

            if ( this.InsideNodeResult && nestLevel <= 1 )
            {
                //Resultノードの外に出た
                this.FindNodeResult = false;
                this.InsideNodeResult = false;
            }


            if ( this.InsideNodeResult && nestLevel == 2 )
            {
                // サイエンスレポート説明
                mc = this.RegexResultTextImport.Matches( blockText );
                if ( mc.Count >= 1 )
                {
                    string resultText  = mc[0].Groups[1].Value.Trim();
                    int    resultIndex = int.Parse(mc[0].Groups[2].Value.Trim());
                    string text        = mc[0].Groups[3].Value.Trim();

                    this.ScienceDefsResultText.Add( new TextData.TranslateText( resultText ,
                                                                                resultIndex ,
                                                                                text ) );
                }
            }


        }

    }
}
