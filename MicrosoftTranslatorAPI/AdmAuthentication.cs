using System.Web;
using System.Runtime.Serialization.Json;
using System;


namespace KspTsTool2.MicrosoftTranslatorAPI
{
    /// <summary>
    /// Microsoft Translator APIを使用した自動翻訳でAccessTokenを取得
    /// </summary>
    public class AdmAuthentication
    {

        /// <summary>
        /// AccessUri
        /// </summary>
        public static string DatamarketAccessUri = @"https://datamarket.accesscontrol.windows.net/v2/OAuth2-13";

        /// <summary>
        /// クライアントID
        /// </summary>
        private string  ClientId;

        /// <summary>
        /// 顧客の秘密
        /// </summary>
        private string CientSecret;

        /// <summary>
        /// RequestURI
        /// </summary>
        private string Request;

        /// <summary>
        /// インスタンスを生成
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="clientSecret"></param>
        public AdmAuthentication( string clientId ,
                                  string clientSecret )
        {
            this.ClientId = clientId;
            this.CientSecret = clientSecret;
            this.Request = String.Format( @"grant_type=client_credentials&client_id={0}&client_secret={1}&scope=http://api.microsofttranslator.com" ,
                                          HttpUtility.UrlEncode( clientId ) ,
                                          HttpUtility.UrlEncode( clientSecret )
                                        );
        }

        /// <summary>
        /// GetAccessTokenを取得
        /// </summary>
        /// <returns></returns>
        public AdmAccessToken GetAccessToken()
        {
            return this.HttpPost( this.Request );
        }

        /// <summary>
        /// HttpPostでAccessTokenを取得する
        /// </summary>
        /// <param name="datamarketAccessUri"></param>
        /// <param name="requestDetails"></param>
        /// <returns></returns>
        private AdmAccessToken HttpPost( string requestDetails )
        {
            System.Net.WebRequest webRequest = System.Net.WebRequest.Create(DatamarketAccessUri);

            webRequest.ContentType = @"application/x-www-form-urlencoded";
            webRequest.Method = @"POST";

            byte[] bytes = System.Text.Encoding.ASCII.GetBytes(requestDetails);

            webRequest.ContentLength = bytes.Length;

            using ( System.IO.Stream outputStream = webRequest.GetRequestStream() )
            {

                outputStream.Write( bytes , 0 , bytes.Length );
            }

            using ( System.Net.WebResponse webResponse = webRequest.GetResponse() )
            {
                var serializer = new DataContractJsonSerializer( typeof( AdmAccessToken ) );

                //Get deserialized object from JSON stream
                AdmAccessToken token = (AdmAccessToken)serializer.ReadObject(webResponse.GetResponseStream());
                return token;
            }
        }
    }
}
