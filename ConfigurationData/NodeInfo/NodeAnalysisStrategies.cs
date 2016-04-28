using System.Text.RegularExpressions;


namespace KspTsTool2.ConfigurationData.NodeInfo
{
    /// <summary>
    /// Strategies解析
    /// </summary>
    public class NodeAnalysisStrategies : NodeAnalysis
    {
        /// <summary>
        /// Strategies用正規表現
        /// </summary>
        private Regex RegexStrategies = new Regex(@"^STRATEGY($|\s|:)", RegexOptions.IgnoreCase);


        /// <summary>
        /// Name用正規表現
        /// </summary>
        private Regex RegexName = new Regex(@"^[@]*name\s*=\s*(.+)$", RegexOptions.IgnoreCase);

        /// <summary>
        /// title用正規表現
        /// </summary>
        private Regex RegexTitle  = new Regex(@"^[@]*title\s*=\s*(.+)$", RegexOptions.IgnoreCase);

        /// <summary>
        /// description用正規表現
        /// </summary>
        private Regex RegexrDescription  = new Regex(@"^[@]*desc\s*=\s*(.+)$", RegexOptions.IgnoreCase);


        /// <summary>
        /// Strategies用正規表現（インポート用）
        /// </summary>
        private Regex RegexStrategiesImport = new Regex(@"^@STRATEGY\s*:\s*HAS\[\s*#name\[\s*(.[^\}]+)\s*\]\s*\]($|\s|:)", RegexOptions.IgnoreCase);

        /// <summary>
        /// description用正規表現（インポート用）
        /// </summary>
        private Regex RegexrDescriptionImport  = new Regex(@"@desc\s*=\s*(.+)", RegexOptions.IgnoreCase);



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
        /// Name
        /// </summary>
        private string StrategiesName { get; set; }

        /// <summary>
        /// タイトル
        /// </summary> 
        private string StrategiesTitle { get; set; }

        /// <summary>
        /// 説明
        /// </summary>
        private string StrategiesDescription { get; set; }


        /// <summary>
        /// コンストラクタ
        /// </summary>
        public NodeAnalysisStrategies()
        {
            this.FindNode = false;
            this.InsideNode = false;
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

            //ノードが見つかったか？
            if ( !this.InsideNode && nestLevel == 0
                && ( this.RegexStrategies.IsMatch( blockText ) ) )
            {
                //ノードが見つかった
                this.FindNode = true;
                this.InsideNode = false;

                this.StrategiesName = "";
                this.StrategiesTitle = "";
                this.StrategiesDescription = "";

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
                if ( !this.StrategiesName.Equals( "" ) )
                {
                    this.TextDataList.Add(
                            new Text.TextDataStrategies( this.StrategiesName ,
                                          this.StrategiesTitle ,
                                          this.StrategiesDescription )
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
                    this.StrategiesName = mc[0].Groups[1].Value;
                }

                //パーツタイトル
                mc = this.RegexTitle.Matches( blockText );
                if ( mc.Count >= 1 )
                {
                    this.StrategiesTitle = mc[0].Groups[1].Value;
                }

                //パーツ説明
                mc = this.RegexrDescription.Matches( blockText );
                if ( mc.Count >= 1 )
                {
                    this.StrategiesDescription = mc[0].Groups[1].Value;
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
            mc = this.RegexStrategiesImport.Matches( blockText );
            if ( !this.InsideNode && nestLevel == 0 && mc.Count >= 1 )
            {
                //ノードが見つかった
                this.FindNode = true;
                this.InsideNode = false;

                this.StrategiesName = mc[0].Groups[1].Value;
                this.StrategiesDescription = "";

                //スペースが含まれている場合は、？に変換されているので、?をスペースへ変換
                this.StrategiesName = this.StrategiesName.Replace( "?" , " " );

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
                if ( !this.StrategiesName.Equals( "" ) )
                {
                    this.TextDataList.Add(
                        new Text.TextDataStrategies( this.StrategiesName ,
                                                "" ,
                                                this.StrategiesDescription )
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
                    this.StrategiesDescription = mc[0].Groups[1].Value;
                }
            }
        }

    }
}
