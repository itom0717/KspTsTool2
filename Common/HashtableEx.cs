using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    /// <summary>
    /// Hashtable拡張
    /// </summary>
    public class HashtableEx : System.Collections.Hashtable
    {
        /// <summary>
        /// インデクサで定義
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public override object this[object key]
        {
            get
            {
                //keyがnull または "" の場合、""を返す。
                if ( key == null || key.Equals( "" ) )
                {
                    return "";
                }

                if ( base.ContainsKey( key ) )
                {
                    //存在する場合は値を返す。
                    return base[key];
                }
                else
                {
                    //存在しない場合は""を返す。
                    return "";
                }
            }
            set
            {
                //keyがnull または "" の場合、何もしない。
                if ( key != null && !key.Equals( "" ) )
                {
                    if ( base.ContainsKey( key ) )
                    {
                        //存在する場合は値の変更
                        base[key] = value;
                    }
                    else
                    {
                        //存在しない場合は新規追加
                        base.Add( key , value );

                    }
                }
            }
        }

        /// <summary>
        /// keyを正規表現で検索し条件に合ったHashtableのみを返す
        /// </summary>
        /// <returns></returns>
        public HashtableEx SearchKey( string pattern )
        {
            var rExp = new System.Text.RegularExpressions.Regex(
                                                        pattern,
                                                        System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            HashtableEx returnHash = new HashtableEx();
            foreach ( string key in base.Keys )
            {
                if ( rExp.IsMatch( key ) )
                {
                    returnHash.Add( key , base[key] );
                }
            }
            return returnHash;
        }

        /// <summary>
        /// KeysをソートしてListで返す
        /// </summary>
        /// <returns></returns>
        public List<string> SortKeys()
        {
            var returnList = new List<string>();
            foreach ( string key in base.Keys )
            {
                returnList.Add( key );
            }
            returnList.Sort();
            return returnList;
        }
    }
}
