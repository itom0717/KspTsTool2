using System.Text.RegularExpressions;


namespace KspTsTool2.ConfigurationData.NodeInfo
{
    /// <summary>
    /// PartData解析
    /// </summary>
    public class NodeAnalysisParts : NodeAnalysis
    {
        /// <summary>
        /// Part用正規表現
        /// </summary>
        private Regex RegexPart = new Regex(@"^PART($|\s)", RegexOptions.IgnoreCase);

        /// <summary>
        /// Part用正規表現(ModulManagerで追加したパーツ)
        /// </summary>
        private Regex RegexPartAddPart = new Regex(@"^\+PART\[", RegexOptions.IgnoreCase);


        /// <summary>
        /// Name用正規表現
        /// </summary>
        private Regex RegexName = new Regex(@"^[@]*Name\s*=\s*(.+)$", RegexOptions.IgnoreCase);

        /// <summary>
        /// title用正規表現
        /// </summary>
        private Regex RegexTitle  = new Regex(@"^[@]*title\s*=\s*(.+)$", RegexOptions.IgnoreCase);

        /// <summary>
        /// description用正規表現
        /// </summary>
        private Regex RegexrDescription  = new Regex(@"^[@]*description\s*=\s*(.+)$", RegexOptions.IgnoreCase);


        /// <summary>
        /// Part用正規表現（インポート用）
        /// </summary>
        private Regex RegexPartImport  = new Regex(@"^@PART\s*\[([^\}]+)\]", RegexOptions.IgnoreCase);

        /// <summary>
        /// description用正規表現（インポート用）
        /// </summary>
        private Regex RegexrDescriptionImport  = new Regex(@"@description\s*=\s*(.+)", RegexOptions.IgnoreCase);



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
        /// パーツ名
        /// </summary>
        private string PartName { get; set; }

        /// <summary>
        /// パーツタイトル
        /// </summary> 
        private string PartTitle { get; set; }

        /// <summary>
        /// パーツ説明
        /// </summary>
        private string PartDescription { get; set; }


        /// <summary>
        /// コンストラクタ
        /// </summary>
        public NodeAnalysisParts()
        {
            this.FindNode = false;
            this.InsideNode = false;
        }

        /// <summary>
        /// 1ブロック解析
        /// </summary>
        /// <param name="blockText"></param>
        public  override  void AnalysisOneBlock( int nestLevel ,
                                               string blockText )
        {

            //正規表現用
            System.Text.RegularExpressions.MatchCollection mc;

            //ノードが見つかったか？
            if ( !this.InsideNode && nestLevel == 0
                && ( this.RegexPart.IsMatch( blockText ) || this.RegexPartAddPart.IsMatch( blockText ) ) )
            {
                //ノードが見つかった
                this.FindNode = true;
                this.InsideNode = false;

                this.PartName = "";
                this.PartTitle = "";
                this.PartDescription = "";

                return;
            }


            //ノードの中に入ったか確認
            if ( this.FindNode && !this.InsideNode && nestLevel == 1 )
            {
                //ノードの中に入った
                this.InsideNode = true;
            }

            //ノードの外に出たか確認
            if ( this.InsideNode && nestLevel == 0 )
            {
                //外に出た
                this.FindNode = false;
                this.InsideNode = false;

                //翻訳元テキストデータを記憶する
                if ( !this.PartName.Equals( "" ) )
                {
                    this.TextDataList.Add(
                            new Text.TextDataParts( this.PartName ,
                                          this.PartTitle ,
                                          this.PartDescription )
                                          );
                }
            }



            //ノードの中の場合
            if ( this.InsideNode && nestLevel == 1 )
            {
                //パーツ名
                mc = this.RegexName.Matches( blockText );
                if ( mc.Count >= 1 )
                {
                    this.PartName = mc[0].Groups[1].Value;
                }

                //パーツタイトル
                mc = this.RegexTitle.Matches( blockText );
                if ( mc.Count >= 1 )
                {
                    this.PartTitle = mc[0].Groups[1].Value;
                }

                //パーツ説明
                mc = this.RegexrDescription.Matches( blockText );
                if ( mc.Count >= 1 )
                {
                    this.PartDescription = mc[0].Groups[1].Value;
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

            //ノードが見つかったか？
            mc = this.RegexPartImport.Matches( blockText );
            if ( !this.InsideNode && nestLevel == 0 && mc.Count >= 1 )
            {
                //ノードが見つかった
                this.FindNode = true;
                this.InsideNode = false;

                this.PartName = mc[0].Groups[1].Value;
                this.PartDescription = "";

                //スペースが含まれている場合は、？に変換されているので、?をスペースへ変換
                this.PartName = this.PartName.Replace( "?" , " " );

                return;
            }

            //ノードの中に入ったか確認
            if ( this.FindNode && !this.InsideNode && nestLevel == 1 )
            {
                //ノードの中に入った
                this.InsideNode = true;
            }

            //ノードの外に出たか確認
            if ( this.InsideNode && nestLevel == 0 )
            {
                //外に出た
                this.FindNode = false;
                this.InsideNode = false;

                //翻訳元テキストデータを記憶する
                if ( !this.PartName.Equals( "" ) )
                {
                    this.TextDataList.Add(
                        new Text.TextDataParts(this.PartName ,
                                                "" ,
                                                this.PartDescription)
                                          );
                }
            }



            //ノードの中の場合
            if ( this.InsideNode && nestLevel == 1 )
            {
                //パーツ説明
                mc = this.RegexrDescriptionImport.Matches( blockText );
                if ( mc.Count >= 1 )
                {
                    this.PartDescription = mc[0].Groups[1].Value;
                }
            }
        }

    }
}
