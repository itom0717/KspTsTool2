using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace KspTsTool2.ConfigurationData.NodeInfo
{
    /// <summary>
    /// StoryDefs解析
    /// </summary>
    class NodeAnalysisStoryDefs : NodeAnalysis
    {
        /// <summary>
        ///  StoryDef用正規表現
        /// </summary>
        private Regex RegexStoryDef = new Regex(@"^[@]*STORY_DEF($|\s)", RegexOptions.IgnoreCase);

        /// <summary>
        ///  StoryDef用正規表現(インポート用)
        /// </summary>
        private Regex RegexStoryDefImport = new Regex(@"^[@]*STORY_DEF($|\s|\s*:)", RegexOptions.IgnoreCase);


        /// <summary>
        /// 記述ノード用正規表現
        /// </summary>
        private Regex RegexTextNode = new Regex(@"^[@]*(.*)($|\s)", RegexOptions.IgnoreCase);

        /// <summary>
        ///  key＆text用正規表現
        /// </summary>
        private Regex RegexTextNodeText = new Regex(@"^([^=]+)\s*=\s*(.+)$", RegexOptions.IgnoreCase);

        /// <summary>
        ///  key＆text用正規表現(インポート用)
        /// </summary>
        private Regex RegeTextNodeTextImport = new Regex(@"^[@]*([^=,]+),(\d+)\s*=\s*(.+)", RegexOptions.IgnoreCase);

        /// <summary>
        /// KeyIndex
        /// </summary>
        private Dictionary< string , int > KeyIndex = new Dictionary< string , int>();

        /// <summary>
        /// STORY_DEFノードが見つかった
        /// </summary>
        /// <returns></returns>
        private bool FindNodeStoryDef { get; set; } = false;

        /// <summary>
        /// STORY_DEFノードの中にはいった
        /// </summary>
        /// <returns></returns>
        private bool InsideNodeStoryDef { get; set; } = false;

        /// <summary>
        /// 記述ノードが見つかった
        /// </summary>
        /// <returns></returns>
        private bool FindNodeTextNode { get; set; } = false;

        /// <summary>
        /// 記述ノードの中にはいった
        /// </summary>
        /// <returns></returns>
        private bool InsideNodeTextNode { get; set; } = false;


        /// <summary>
        /// 記述ノード名
        /// </summary>
        private string StoryDefTextNode { get; set; }

        /// <summary>
        /// 記述ノードテキスト
        /// </summary>
        private List<Translate.TranslateText> StoryDefTextNodeText { get; set; } = null;



        /// <summary>
        /// コンストラクタ
        /// </summary>
        public NodeAnalysisStoryDefs()
        {
            this.FindNodeStoryDef = false;
            this.InsideNodeStoryDef = false;
            this.FindNodeTextNode = false;
            this.InsideNodeTextNode = false;
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


            // StoryDefノードが見つかったか？
            if ( !this.InsideNodeStoryDef && nestLevel == 0 && RegexStoryDef.IsMatch( blockText ) )
            {
                //TechTreeノードが見つかった
                this.FindNodeStoryDef = true;
                this.InsideNodeStoryDef = false;
                this.FindNodeTextNode = false;
                this.InsideNodeTextNode = false;
                this.KeyIndex.Clear();
                return;
            }

            //StoryDefノードの中に入ったか確認
            if ( this.FindNodeStoryDef && !this.InsideNodeStoryDef && nestLevel == 1 )
            {
                //StoryDefノードの中に入った
                this.InsideNodeStoryDef = true;
            }

            if ( this.InsideNodeStoryDef && nestLevel == 0 )
            {
                //StoryDefノードの外に出た
                this.FindNodeStoryDef = false;
                this.InsideNodeStoryDef = false;
                this.FindNodeTextNode = false;
                this.InsideNodeTextNode = false;
                this.KeyIndex.Clear();
                return;
            }

            if (!this.InsideNodeStoryDef)
            {
                //StoryDefノードの外
                return;
            }



            //記述ノードが見つかったか？
            if ( this.InsideNodeStoryDef && !this.InsideNodeTextNode && nestLevel == 1 )
            {
                if ( blockText != "" )
                {
                    mc = this.RegexTextNode.Matches( blockText );
                    if ( mc.Count >= 1 )
                    {
                        ///記述ノードが見つかった
                        this.FindNodeTextNode = true;
                        this.InsideNodeTextNode = false;

                        this.StoryDefTextNode = mc[0].Groups[1].Value;

                        this.StoryDefTextNodeText = new List<Translate.TranslateText>();
                        this.StoryDefTextNodeText.Clear();

                        this.KeyIndex.Clear();
                    }
                }
            }


            if ( this.FindNodeTextNode && !this.InsideNodeTextNode && nestLevel == 2 )
            {
                //記述ノードの中に入った
                this.InsideNodeTextNode = true;
            }
            if( !this.InsideNodeTextNode )
            {
                //記述ノードの外
                return;
            }

            if ( this.InsideNodeTextNode && nestLevel <= 1 )
            {
                ///記述ノードの外に出た
                this.FindNodeTextNode = false;
                this.InsideNodeTextNode = false;

                //翻訳元テキストデータを記憶する
                if ( !this.StoryDefTextNode.Equals( "" ) && this.StoryDefTextNodeText.Count>0 )
                {
                    this.TextDataList.Add(
                            new Text.TextDataStoryDefs( this.StoryDefTextNode ,
                                           this.StoryDefTextNodeText )
                                          );
                }
            }


            if ( this.InsideNodeTextNode && nestLevel == 2 )
            {

                // 記述ノードテキスト
                mc = this.RegexTextNodeText.Matches( blockText );
                if ( mc.Count >= 1 )
                {
                    string textTitle  = mc[0].Groups[1].Value.Trim();
                    int    textIndex = 0;
                    string text     = mc[0].Groups[2].Value.Trim();
                    if ( this.KeyIndex.ContainsKey( textTitle.ToUpper() ) )
                    {
                        textIndex = this.KeyIndex[textTitle.ToUpper()];
                        textIndex++;
                        this.KeyIndex[textTitle.ToUpper()] = textIndex;
                    }
                    else
                    {
                        textIndex = 0;
                        this.KeyIndex.Add( textTitle.ToUpper() , textIndex );
                    }

                    this.StoryDefTextNodeText.Add( new Translate.TranslateTextStoryDefs( textTitle ,
                                                                                         textIndex ,
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


            // StoryDefノードが見つかったか？
            if ( !this.InsideNodeStoryDef && nestLevel == 0 && RegexStoryDefImport.IsMatch( blockText ) )
            {
                //TechTreeノードが見つかった
                this.FindNodeStoryDef = true;
                this.InsideNodeStoryDef = false;
                this.FindNodeTextNode = false;
                this.InsideNodeTextNode = false;
                this.KeyIndex.Clear();
                return;
            }

            //StoryDefノードの中に入ったか確認
            if ( this.FindNodeStoryDef && !this.InsideNodeStoryDef && nestLevel == 1 )
            {
                //StoryDefノードの中に入った
                this.InsideNodeStoryDef = true;
            }

            if ( this.InsideNodeStoryDef && nestLevel == 0 )
            {
                //StoryDefノードの外に出た
                this.FindNodeStoryDef = false;
                this.InsideNodeStoryDef = false;
                this.FindNodeTextNode = false;
                this.InsideNodeTextNode = false;
                this.KeyIndex.Clear();
                return;
            }

            if (!this.InsideNodeStoryDef)
            {
                //StoryDefノードの外
                return;
            }



            //記述ノードが見つかったか？
            if ( this.InsideNodeStoryDef && !this.InsideNodeTextNode && nestLevel == 1 )
            {
                if ( blockText != "" )
                {
                    mc = this.RegexTextNode.Matches( blockText );
                    if ( mc.Count >= 1 )
                    {
                        ///記述ノードが見つかった
                        this.FindNodeTextNode = true;
                        this.InsideNodeTextNode = false;

                        this.StoryDefTextNode = mc[0].Groups[1].Value;

                        this.StoryDefTextNodeText = new List<Translate.TranslateText>();
                        this.StoryDefTextNodeText.Clear();

                        this.KeyIndex.Clear();
                    }
                }
            }


            if ( this.FindNodeTextNode && !this.InsideNodeTextNode && nestLevel == 2 )
            {
                //記述ノードの中に入った
                this.InsideNodeTextNode = true;
            }
            if( !this.InsideNodeTextNode )
            {
                //記述ノードの外
                return;
            }

            if ( this.InsideNodeTextNode && nestLevel <= 1 )
            {
                ///記述ノードの外に出た
                this.FindNodeTextNode = false;
                this.InsideNodeTextNode = false;

                //翻訳元テキストデータを記憶する
                if ( !this.StoryDefTextNode.Equals( "" ) && this.StoryDefTextNodeText.Count>0 )
                {
                    this.TextDataList.Add(
                            new Text.TextDataStoryDefs( this.StoryDefTextNode ,
                                           this.StoryDefTextNodeText )
                                          );
                }
            }


            if ( this.InsideNodeTextNode && nestLevel == 2 )
            {

                // 記述ノードテキスト
                mc = this.RegeTextNodeTextImport.Matches( blockText );
                if ( mc.Count >= 1 )
                {
                    string textTitle = mc[0].Groups[1].Value.Trim();
                    int    textIndex = int.Parse(mc[0].Groups[2].Value.Trim());
                    string text      = mc[0].Groups[3].Value.Trim();

                    this.StoryDefTextNodeText.Add( new Translate.TranslateTextStoryDefs( textTitle ,
                                                                                textIndex ,
                                                                                text ) );
                }


            }




        }

    }
}
