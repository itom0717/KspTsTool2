using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace KspTsTool2.ConfigurationFile
{
    /// <summary>
    /// cfgファイル解析
    /// </summary>
    public class ConfigurationFile
    {

        /// <summary>
        /// テキストデータ
        /// </summary>
        public  List<TextData.TextData> TextDataList =  new List<TextData.TextData>();

        /// <summary>
        /// CURLY BRACKET({/})のネストレベル
        /// </summary>
        private int NestLevel;

        /// <summary>
        /// Paet Nodeの状態
        /// </summary>
        private NodeInfo.PartNode PartNode;

        /// <summary>
        /// EXPERIMENT_DEFINITION Nodeの状態
        /// </summary>
        private NodeInfo.ScienceDefsNode ExperimentDefinitionNode;

        /// <summary>
        /// cfgファイル名
        /// </summary>
        private string CfgFilename= "";

        /// <summary>
        /// ファイルを読み込んで、解析する
        /// </summary>
        /// <param name="cfgFile">読み込みファイル</param>
        /// <param name="importFileMode">インポートする場合True</param>
        /// <returns>
        /// 対象データが存在した場合、trueを返す。
        /// </returns>
        public bool AnalysisCfgFile( string cfgFile,
                                     bool importFileMode = false)
        {
            //処理ファイル名
            this.CfgFilename = cfgFile;


            try
            {
                //取得するデータは
                //PART内のName,title,description
                //EXPERIMENT_DEFINITION内のidとtitle
                //EXPERIMENT_DEFINITION内の内のRESULTS内の値
                //PARTにEXPERIMENT_DEFINITIONは存在しない。
                //EXPERIMENT_DEFINITIONにPARTは存在しない。

                //正規表現定義

                //コメント削除用
                var regexComment = new Regex( "//.*$" , RegexOptions.IgnoreCase );

                //初期化
                this.NestLevel = 0;
                this.PartNode = new NodeInfo.PartNode( this.TextDataList );
                this.ExperimentDefinitionNode = new NodeInfo.ScienceDefsNode( this.TextDataList );

                //ファイルの解析
                using ( var sr = new System.IO.StreamReader( cfgFile , System.Text.Encoding.UTF8 ) )
                {
                    while ( sr.Peek() > -1 )
                    {
                        //1行データ
                        string lineText = sr.ReadLine();

                        //コメント削除
                        lineText = regexComment.Replace( lineText , "" );

                        //前後の空白取り除き
                        lineText = lineText.Trim();
                        if ( lineText.Equals( "" ) )
                        {
                            //データなければ次の行へ
                            continue;
                        }

                        //1行解析
                        this.AnalysisOneLine( lineText , importFileMode );
                    }
                    //閉じる
                    sr.Close();
                }

                //テキストデータが１件以上あればtrueを返す
                return this.TextDataList.Count > 0 ? true : false;
            }
            catch
            {
                throw;
            }

        }


        /// <summary>
        /// 1行解析
        /// </summary>
        private void AnalysisOneLine( string lineText ,
                                      bool importFileMode )
        {

            //{ 分割用（LEFT CURLY BRACKET）
            var regexLeftCurlyBracket  = new Regex("{", RegexOptions.IgnoreCase);

            //} 分割用（RIGHT CURLY BRACKET）
            var regexRightCurlyBracket = new Regex("}", RegexOptions.IgnoreCase);

            //{で分割する(分割文字がない場合は、元のテキストが入る。)
            string[] blockTextLeft = regexLeftCurlyBracket.Split(lineText);

            //分割個数分ループ
            for ( int i = 0; i < blockTextLeft.Length; i++ )
            {
                //}で分割する(分割文字がない場合は、元のテキストが入る。)
                string[] blockTextRight = regexRightCurlyBracket.Split(blockTextLeft[i]);

                for ( int j = 0; j < blockTextRight.Length; j++ )
                {
                    //1ブロック解析


                    if(!importFileMode )
                    {
                        //パーツ
                        this.PartNode.AnalysisOneBlock(
                                                    this.NestLevel ,
                                                    blockTextRight[j] );

                        //サイエンスレポート
                        this.ExperimentDefinitionNode.AnalysisOneBlock(
                                                    this.NestLevel ,
                                                    blockTextRight[j] );
                    }
                    else
                    {
                        //インポートモード
                        //パーツ
                        this.PartNode.AnalysisOneBlockImport(
                                                this.NestLevel ,
                                                blockTextRight[j] );

                        //サイエンスレポート
                        this.ExperimentDefinitionNode.AnalysisOneBlockImport(
                                                this.NestLevel ,
                                                blockTextRight[j] );
                    }


                    if ( blockTextRight.Length >= 2 && j < blockTextRight.Length - 1 )
                    {
                        //}で分割ができた場合のみネスト-1
                        this.NestLevel -= 1;
                    }
                }

                if ( blockTextLeft.Length >= 2 && i < blockTextLeft.Length - 1 )
                {
                    //{で分割ができた場合のみネスト+1
                    this.NestLevel += 1;
                }
            }
        }


        /// <summary>
        /// 翻訳件数
        /// </summary>

        public int TranslationMaxCount
        {
            get
            {
                int count = 0;

                foreach ( TextData.TextData textData in this.TextDataList )
                {
                    foreach ( TextData.TranslateText trText in textData.TranslateTextList )
                    {
                        count++;
                    }
                }

                return count;

            }
        }


    }
}
