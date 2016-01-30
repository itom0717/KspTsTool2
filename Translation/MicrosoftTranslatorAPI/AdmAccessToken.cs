using System.Runtime.Serialization;

namespace KspTsTool2.Translation.MicrosoftTranslatorAPI
{
    /// <summary>
    /// アクセストークン
    /// </summary>
    [DataContract]
    public class AdmAccessToken
    {
        /// <summary>
        /// Microsoftの翻訳APIへのアクセスの認証に使用できるアクセストークン
        /// </summary>
        [DataMember]
        public string access_token { get; set; }

        /// <summary>
        /// アクセストークンの形式
        /// </summary>
        [DataMember]
        public string token_type { get; set; }

        /// <summary>
        /// アクセス・トークンの有効期限(秒数)
        /// </summary>
        [DataMember]
        public string expires_in { get; set; }

        /// <summary>
        /// このトークンが有効であるドメイン。マイクロソフトの翻訳APIの場合、ドメインである
        /// </summary>
        [DataMember]
        public string scope { get; set; }
    }
}
