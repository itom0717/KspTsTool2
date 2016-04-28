using System.Text.RegularExpressions;


namespace KspTsTool2.ConfigurationData.NodeInfo
{
    /// <summary>
    /// Departments解析
    /// </summary>
    public class NodeAnalysisDepartments : NodeAnalysis
    {
        /// <summary>
        /// Departments用正規表現
        /// </summary>
        private Regex RegexDepartments = new Regex(@"^STRATEGY_DEPARTMENT($|\s|:)", RegexOptions.IgnoreCase);

        /// <summary>
        /// Name用正規表現
        /// </summary>
        private Regex RegexName = new Regex(@"^[@]*name\s*=\s*(.+)$", RegexOptions.IgnoreCase);

        /// <summary>
        /// desc用正規表現
        /// </summary>
        private Regex RegexrDescription  = new Regex(@"^[@]*desc\s*=\s*(.+)$", RegexOptions.IgnoreCase);


        /// <summary>
        /// Departments用正規表現（インポート用）
        /// </summary>
        private Regex RegexDepartmentsImport = new Regex(@"^@STRATEGY_DEPARTMENT\s*:\s*HAS\[\s*#name\[\s*(.[^\}]+)\s*\]\s*\]($|\s|:)", RegexOptions.IgnoreCase);

        /// <summary>
        /// desc用正規表現（インポート用）
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
        private string DepartmentsName { get; set; }

        /// <summary>
        /// 説明
        /// </summary>
        private string DepartmentsDescription { get; set; }


        /// <summary>
        /// コンストラクタ
        /// </summary>
        public NodeAnalysisDepartments()
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
                && ( this.RegexDepartments.IsMatch( blockText ) ) )
            {
                //ノードが見つかった
                this.FindNode = true;
                this.InsideNode = false;

                this.DepartmentsName = "";
                this.DepartmentsDescription = "";

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
                if ( !this.DepartmentsName.Equals( "" ) )
                {
                    this.TextDataList.Add(
                            new Text.TextDataDepartments( this.DepartmentsName ,
                                          this.DepartmentsDescription )
                                          );
                }
            }



            //ノードの中の場合
            if ( this.InsideNode && nestLevel == 1 )
            {
                //Name
                mc = this.RegexName.Matches( blockText );
                if ( mc.Count >= 1 )
                {
                    this.DepartmentsName = mc[0].Groups[1].Value;
                }


                //説明
                mc = this.RegexrDescription.Matches( blockText );
                if ( mc.Count >= 1 )
                {
                    this.DepartmentsDescription = mc[0].Groups[1].Value;
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
            mc = this.RegexDepartmentsImport.Matches( blockText );
            if ( !this.InsideNode && nestLevel == 0 && mc.Count >= 1 )
            {
                //ノードが見つかった
                this.FindNode = true;
                this.InsideNode = false;

                this.DepartmentsName = mc[0].Groups[1].Value;
                this.DepartmentsDescription = "";

                //スペースが含まれている場合は、？に変換されているので、?をスペースへ変換
                this.DepartmentsName = this.DepartmentsName.Replace( "?" , " " );

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
                if ( !this.DepartmentsName.Equals( "" ) )
                {
                    this.TextDataList.Add(
                        new Text.TextDataDepartments( this.DepartmentsName ,
                                                      this.DepartmentsDescription )
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
                    this.DepartmentsDescription = mc[0].Groups[1].Value;
                }
            }
        }

    }
}
