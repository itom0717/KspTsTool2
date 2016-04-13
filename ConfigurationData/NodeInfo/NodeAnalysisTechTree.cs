using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace KspTsTool2.ConfigurationData.NodeInfo
{
    /// <summary>
    /// TechTree解析
    /// </summary>
    class NodeAnalysisTechTree : NodeAnalysis
    {
        /// <summary>
        ///  TechTree用正規表現
        /// </summary>
        private Regex RegexTechTree = new Regex(@"^[@]*TechTree($|\s)", RegexOptions.IgnoreCase);

        /// <summary>
        ///  RDNode用正規表現
        /// </summary>
        private Regex RegexRDNode = new Regex(@"^RDNode($|\s|:)", RegexOptions.IgnoreCase);

        /// <summary>
        ///  RDNode用正規表現(Import/ModuleManage定義)
        /// </summary>
        private Regex RegexRDNodeImport = new Regex(@"^@RDNode\s*:\s*HAS\s*\[\s*#id\s*\[\s*(.+?)\s*\]\s*\]", RegexOptions.IgnoreCase);


        /// <summary>
        ///  id用正規表現
        /// </summary>
        private Regex RegexID = new Regex(@"^id\s*=\s*(.+)$", RegexOptions.IgnoreCase);

        /// <summary>
        ///  title用正規表現
        /// </summary>
        private Regex RegexTitle = new Regex(@"^[@]*title\s*=\s*(.+)$", RegexOptions.IgnoreCase);

        /// <summary>
        /// description用正規表現
        /// </summary>
        private Regex RegexDescription  = new Regex(@"^[@]*description\s*=\s*(.+)$", RegexOptions.IgnoreCase);

        /// <summary>
        /// description用正規表現
        /// </summary>
        private Regex RegexDescriptionImport  = new Regex(@"^@description\s*=\s*(.+)$", RegexOptions.IgnoreCase);











        /// <summary>
        /// TechTreeノードが見つかった
        /// </summary>
        /// <returns></returns>
        private bool FindNodeTechTree { get; set; } = false;

        /// <summary>
        /// TechTreeノードの中にはいった
        /// </summary>
        /// <returns></returns>
        private bool InsideNodeTechTree { get; set; } = false;

        /// <summary>
        /// RDNodeノードが見つかった
        /// </summary>
        /// <returns></returns>あ
        private bool FindNodeRDNode { get; set; } = false;

        /// <summary>
        /// RDNodeノードの中にはいった
        /// </summary>
        /// <returns></returns>
        private bool InsideNodeRDNode { get; set; } = false;



        /// <summary>
        /// ID
        /// </summary>
        private string TechTreeID { get; set; }

        /// <summary>
        /// タイトル
        /// </summary>
        private string TechTreeTitle { get; set; }

        /// <summary>
        /// 説明
        /// </summary>
        private string TechTreeDescription { get; set; }


        /// <summary>
        /// コンストラクタ
        /// </summary>
        public NodeAnalysisTechTree()
        {
            this.FindNodeTechTree = false;
            this.InsideNodeTechTree = false;
            this.FindNodeRDNode = false;
            this.InsideNodeRDNode = false;
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


            //TechTreeノードが見つかったか？
            if ( !this.InsideNodeTechTree && nestLevel == 0 && RegexTechTree.IsMatch( blockText ) )
            {
                //TechTreeノードが見つかった
                this.FindNodeTechTree = true;
                this.InsideNodeTechTree = false;
                this.FindNodeRDNode = false;
                this.InsideNodeRDNode = false;
                return;
            }

            //ノードの中に入ったか確認
            if ( this.FindNodeTechTree && !this.InsideNodeTechTree && nestLevel == 1 )
            {
                //TechTreeノードの中に入った
                this.InsideNodeTechTree = true;
            }

            if ( this.InsideNodeTechTree && nestLevel == 0 )
            {
                //TechTreeノードの外に出た
                this.FindNodeTechTree = false;
                this.InsideNodeTechTree = false;
                this.FindNodeRDNode = false;
                this.InsideNodeRDNode = false;

            }




            //RDNodeノードが見つかったか？
            if ( this.InsideNodeTechTree && !this.InsideNodeRDNode && nestLevel == 1 )
            {
                if ( blockText != "" )
                {
                    mc = this.RegexRDNodeImport.Matches( blockText );
                    if ( mc.Count >= 1 )
                    {
                        //RDNodeノードが見つかった
                        this.FindNodeRDNode = true;
                        this.InsideNodeRDNode = false;

                        this.TechTreeID = mc[0].Groups[1].Value;
                        this.TechTreeTitle = "";
                        this.TechTreeDescription = "";
                    }
                    else  if ( this.RegexRDNode.IsMatch( blockText ) )
                    {
                        //RDNodeノードが見つかった
                        this.FindNodeRDNode = true;
                        this.InsideNodeRDNode = false;

                        this.TechTreeID = "";
                        this.TechTreeTitle = "";
                        this.TechTreeDescription = "";
                    }
                }
            }


            if ( this.FindNodeRDNode && !this.InsideNodeRDNode && nestLevel == 2 )
            {
                //RDNodeノードの中に入った
                this.InsideNodeRDNode = true;
            }

            if ( this.InsideNodeRDNode && nestLevel <= 1 )
            {
                //Resultノードの外に出た
                this.FindNodeRDNode = false;
                this.InsideNodeRDNode = false;

                //翻訳元テキストデータを記憶する
                if ( !this.TechTreeID.Equals( "" )  && !this.TechTreeDescription.Equals( "" ) )
                {
                    this.TextDataList.Add(
                            new Text.TextDataTechTree( this.TechTreeID ,
                                          this.TechTreeTitle ,
                                          this.TechTreeDescription )
                                          );
                }
            }


            if ( this.InsideNodeRDNode && nestLevel == 2 )
            {
                //ID
                mc = this.RegexID.Matches( blockText );
                if ( mc.Count >= 1 )
                {
                    this.TechTreeID = mc[0].Groups[1].Value;
                }

                //タイトル
                mc = this.RegexTitle.Matches( blockText );
                if ( mc.Count >= 1 )
                {
                    this.TechTreeTitle = mc[0].Groups[1].Value;
                }

                //説明
                mc = this.RegexDescription.Matches( blockText );
                if ( mc.Count >= 1 )
                {
                    this.TechTreeDescription = mc[0].Groups[1].Value;
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


            //TechTreeノードが見つかったか？
            if ( !this.InsideNodeTechTree && nestLevel == 0 && RegexTechTree.IsMatch( blockText ) )
            {
                //TechTreeノードが見つかった
                this.FindNodeTechTree = true;
                this.InsideNodeTechTree = false;
                this.FindNodeRDNode = false;
                this.InsideNodeRDNode = false;
                return;
            }

            //ノードの中に入ったか確認
            if ( this.FindNodeTechTree && !this.InsideNodeTechTree && nestLevel == 1 )
            {
                //TechTreeノードの中に入った
                this.InsideNodeTechTree = true;
            }

            if ( this.InsideNodeTechTree && nestLevel == 0 )
            {
                //TechTreeノードの外に出た
                this.FindNodeTechTree = false;
                this.InsideNodeTechTree = false;
                this.FindNodeRDNode = false;
                this.InsideNodeRDNode = false;

            }







            //RDNodeノードが見つかったか？
            if ( this.InsideNodeTechTree && !this.InsideNodeRDNode && nestLevel == 1 )
            {

                mc = this.RegexRDNodeImport.Matches( blockText );
                if ( mc.Count >= 1 )
                {
                    //RDNodeノードが見つかった
                    this.FindNodeRDNode = true;
                    this.InsideNodeRDNode = false;

                    this.TechTreeID = mc[0].Groups[1].Value;
                    this.TechTreeTitle = "";
                    this.TechTreeDescription = "";
                }

            }


            if ( this.FindNodeRDNode && !this.InsideNodeRDNode && nestLevel == 2 )
            {
                //RDNodeノードの中に入った
                this.InsideNodeRDNode = true;
            }

            if ( this.InsideNodeRDNode && nestLevel <= 1 )
            {
                //Resultノードの外に出た
                this.FindNodeRDNode = false;
                this.InsideNodeRDNode = false;

                //翻訳元テキストデータを記憶する
                if ( !this.TechTreeID.Equals( "" ) && !this.TechTreeDescription.Equals( "" ) )
                {
                    this.TextDataList.Add(
                            new Text.TextDataTechTree( this.TechTreeID ,
                                          "" ,
                                          this.TechTreeDescription )
                                          );
                }
            }


            if ( this.InsideNodeRDNode && nestLevel == 2 )
            {
                //説明
                mc = this.RegexDescriptionImport.Matches( blockText );
                if ( mc.Count >= 1 )
                {
                    this.TechTreeDescription = mc[0].Groups[1].Value;
                }
            }



        }

    }
}
