using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace KspTsTool2.ConfigurationData.NodeInfo
{
    /// <summary>
    /// サイエンスレポート解析
    /// </summary>
    public class NodeAnalysisScienceDefs : NodeAnalysis
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
        ///  key＆text用正規表現(インポート用)
        /// </summary>
        private Regex RegexResultTextImport = new Regex(@"^@([^=,]+),(\d+)\s*=\s*(.+)", RegexOptions.IgnoreCase);


        /// <summary>
        /// KeyIndex
        /// </summary>
        private Dictionary< string , int > KeyIndex = new Dictionary< string , int>();


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
        private string ScienceDefsID { get; set; }

        /// <summary>
        /// サイエンスレポートタイトル
        /// </summary>
        private string ScienceDefsTitle { get; set; }

        /// <summary>
        /// サイエンスレポートタイトル
        /// </summary>
        private List<Translate.TranslateText> ScienceDefsResultText { get; set; } = null;




        /// <summary>
        /// コンストラクタ
        /// </summary>
        public NodeAnalysisScienceDefs()
        {
            this.FindNode = false;
            this.InsideNode = false;
            this.FindNodeResult = false;
            this.InsideNodeResult = false;
            this.KeyIndex.Clear();
        }


        /// <summary>
        /// 1ブロック解析
        /// </summary>
        /// <param name="blockText"></param>
        public override void AnalysisOneBlock( int nestLevel ,
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

                this.ScienceDefsResultText = new List<Translate.TranslateText>();
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
                    this.TextDataList.Add(
                        new Text.TextDataScienceDefs( this.ScienceDefsID ,
                                                        this.ScienceDefsTitle ,
                                                        this.ScienceDefsResultText )
                                         );
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
                    if ( this.KeyIndex.ContainsKey( resultText.ToUpper() ) )
                    {
                        resultIndex = this.KeyIndex[resultText.ToUpper()];
                        resultIndex++;
                        this.KeyIndex[resultText.ToUpper()] = resultIndex;
                    }
                    else
                    {
                        resultIndex = 0;
                        this.KeyIndex.Add( resultText.ToUpper() , resultIndex );
                    }

                    this.ScienceDefsResultText.Add( new Translate.TranslateTextScienceDefs( resultText ,
                                                                                resultIndex ,
                                                                                text ) );
                }
            }

        }


        /// <summary>
        /// 1ブロック解析(インポートモード時)
        /// </summary>
        /// <param name="blockText"></param>
        public override void AnalysisOneBlockImport( int nestLevel ,
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

                this.ScienceDefsID = mc[0].Groups[1].Value;

                this.ScienceDefsResultText = new List<Translate.TranslateText>();
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
                    this.TextDataList.Add(
                        new Text.TextDataScienceDefs( this.ScienceDefsID ,
                                                      "" ,
                                                      this.ScienceDefsResultText )
                                          );
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

                    this.ScienceDefsResultText.Add( new Translate.TranslateTextScienceDefs( resultText ,
                                                                                resultIndex ,
                                                                                text ) );
                }
            }
        }

    }
}
