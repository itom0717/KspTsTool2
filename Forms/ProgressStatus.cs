namespace KspTsTool2.Forms
{
    /// <summary>
    /// 進行状況表示処理
    /// </summary>
    public class ProgressStatus
    {


        /// <summary>
        /// BackgroundWorker
        /// </summary>
        private System.ComponentModel.BackgroundWorker BackgroundWorker { get; set; } = null;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ProgressStatus( System.ComponentModel.BackgroundWorker bw )
        {
            this.BackgroundWorker = bw;
        }




        /// <summary>
        /// 表示テキスト
        /// </summary>
        public class StatusText
        {

            /// <summary>
            /// リセットか？
            /// </summary>
            public bool IsReset { get; set; } = false;


            /// <summary>
            /// 追加ログテキスト
            /// </summary>
            public string AddLogText { get; set; } = "";
        }




        /// <summary>
        /// リセット
        /// </summary>
        public void Reset()
        {
            this.FirstLevelDirectoryMaxCount = 0;

            var statusText = new StatusText();
            statusText.IsReset = true;

            //ステータス表示
            this.BackgroundWorker.ReportProgress( (int)this.NowPercent , statusText );
        }



        /// <summary>
        /// 状況表示(テキスト表示のみ)
        /// </summary>
        public void DispStatus( string addLogText )
        {
            var statusText = new StatusText();
            statusText.AddLogText = addLogText;

            //ステータス表示
            this.BackgroundWorker.ReportProgress( ( int ) this.NowPercent , statusText );
        }


        /// <summary>
        /// 現在のパーセント
        /// </summary>
        private double NowPercent
        {
            get
            {
                //計算で求める
                double returnValue = 0.0;

                if ( this.FirstLevelDirectoryMaxCount <= 0 || this.FirstLevelDirectoryNowCount  <= 0)
                {
                    return returnValue;
                }
                double p1 = 100.0 / this.FirstLevelDirectoryMaxCount;
                returnValue += p1 * ( this.FirstLevelDirectoryNowCount - 1 );


                if ( this.CfgFileMaxCount <= 0 || this.CfgFileNowCount <= 0 )
                {
                    return returnValue;
                }
                double p2 = p1 / this.CfgFileMaxCount;
                returnValue += p2 * ( this.CfgFileNowCount - 1 );


                if ( this.TranslationMaxCount <= 0 || this.TranslationNowCount <= 0 )
                {
                    return returnValue;
                }
                double p3 = p2 / this.CfgFileMaxCount;
                returnValue += p3 * ( this.TranslationNowCount - 1 );


                return returnValue;
            }
        }



        /// <summary>
        /// 第一階層のフォルダ最大数
        /// </summary>
        public int FirstLevelDirectoryMaxCount
        {
            get
            {
                return this.firstLevelDirectoryMaxCount;
            }
            set
            {
                this.firstLevelDirectoryMaxCount = value;
                this.FirstLevelDirectoryNowCount = 0;
            }
        }
        private int firstLevelDirectoryMaxCount = 0;



        /// <summary>
        /// 第一階層のフォルダの現在処理中のカウント
        /// </summary>
        public int FirstLevelDirectoryNowCount
        {
            get
            {
                return this.firstLevelDirectoryNowCount;
            }
            set
            {
                this.firstLevelDirectoryNowCount = value;
                this.CfgFileMaxCount = 0;
            }
        }
        private int firstLevelDirectoryNowCount = 0;


        /// <summary>
        /// フォルダ内のcfgファイル数の最大数
        /// </summary>
        public int CfgFileMaxCount
        {
            get
            {
                return this.cfgFileMaxCount;
            }
            set
            {
                this.cfgFileMaxCount = value;
                this.cfgFileNowCount = 0;
            }
        }
        private int cfgFileMaxCount = 0;


        /// <summary>
        /// フォルダ内のcfgファイル数の現在処理中のカウント
        /// </summary>
        public int CfgFileNowCount
        {
            get
            {
                return this.cfgFileNowCount;
            }
            set
            {
                this.cfgFileNowCount = value;
                this.TranslationMaxCount = 0;
            }
        }
        private int cfgFileNowCount = 0;


        /// <summary>
        /// 翻訳件数の最大数
        /// </summary>
        public int TranslationMaxCount
        {
            get
            {
                return this.translationMaxCount;
            }
            set
            {
                this.translationMaxCount = value;
                this.TranslationNowCount = 0;
            }
        }
        private int translationMaxCount = 0;


        /// <summary>
        /// 翻訳件数の現在処理中のカウント
        /// </summary>
        public int TranslationNowCount
        {
            get
            {
                return this.translationNowCount;
            }
            set
            {
                this.translationNowCount = value;
            }
        }
        private int translationNowCount = 0;


    }


}
