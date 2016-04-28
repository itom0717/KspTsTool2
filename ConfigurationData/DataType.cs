namespace KspTsTool2.ConfigurationData
{

    /// <summary>
    /// データ種類
    /// </summary>
    ///<remarks>
    ///ここの値によって処理を追加する。
    ///ここの値を追加した場合、各クラスを作成のこと
    ///</remarks>
    public enum DataType
    {
        /// <summary>
        /// パーツ
        /// </summary>
        Parts = 0,

        /// <summary>
        /// サイエンスレポート
        /// </summary>
        ScienceDefs,

        /// <summary>
        /// TechTree
        /// </summary>
        TechTree,

        /// <summary>
        /// StoryDefs
        /// </summary>
        StoryDefs,

        /// <summary>
        /// Departments
        /// </summary>
        Departments,

        /// <summary>
        /// Strategies
        /// </summary>
        Strategies,


    }

}
