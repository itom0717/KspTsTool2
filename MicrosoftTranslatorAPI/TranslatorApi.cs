using System;
using System.IO;
using System.Net;
using System.Text;

namespace KspTsTool2.MicrosoftTranslatorAPI
{
    /// <summary>
    /// Microsoft Translator APIを使用した自動翻訳
    /// </summary>
    /// <remarks>
    /// 参考
    /// https://msdn.microsoft.com/en-us/library/ff512421
    /// </remarks>
    public class TranslatorApi
    {

        /// <summary>
        /// AdmAccessToken
        /// </summary>
        private  AdmAccessToken  AdmToken  = null;

        /// <summary>
        /// AdmAuthentication
        /// </summary>
        private AdmAuthentication AdmAuth = null;

        /// <summary>
        /// アクセストークン取得からの時間計測用
        /// </summary>
        private System.Diagnostics.Stopwatch StopWatch = new System.Diagnostics.Stopwatch();

        /// <summary>
        /// アクセストークンの有効期限(秒）
        /// </summary>
        private int AccessTokenExpiresIn = 0;

        /// <summary>
        /// インスタンスを生成
        /// </summary>
        public TranslatorApi( string clientId ,
                              string clientSecret )
        {
#if DEBUG
#else
            //Get Client Id And Client Secret from https//datamarket.azure.com/developer/applications/
            //Refer obtaining AccessToken (http://msdn.microsoft.com/en-us/library/hh454950.aspx) 
            // clientIDには、事前にマイクロソフトへ登録した「クライアントID」を設定。
            // client secretには、事前にマイクロソフトへ登録した「顧客の秘密」を設定。
            this.AdmAuth = new AdmAuthentication( clientId ,
                                                  clientSecret );

#endif
        }

        /// <summary>
        /// 英語から日本語へ翻訳
        /// </summary>
        /// <param name="englishText"></param>
        /// <returns></returns>
        public string TranslateEnglishToJapanese( string englishText )
        {

#if DEBUG
            return "Debug - " + englishText;
#else
            try
            {
                //アクセストークン取得
                //アクセストークンは10分間有効であるため、余裕を見て9分以上経過している場合に再度取得する
                if ( AdmToken == null
                        || this.StopWatch.ElapsedMilliseconds > ( this.AccessTokenExpiresIn * 1000 ) )
                {
                    this.StopWatch.Reset();
                    this.StopWatch.Start();
                    this.AdmToken = this.AdmAuth.GetAccessToken();

                    // アクセストークンの有効期限(秒）を記憶
                    // 余裕を見て95%にする
                    this.AccessTokenExpiresIn = ( int ) ( int.Parse( this.AdmToken.expires_in ) * 0.95 );
                }

                //Create a header with the access_token property of the returned token
                string headerValue = "Bearer " + this.AdmToken.access_token;

                //翻訳実施
                string japaneseText = this.TranslateMethod(headerValue, 
                                                           englishText,
                                                           "en",
                                                           "ja");
                System.Threading.Thread.Sleep( 100 ); //wait

                return japaneseText;
            }
            catch ( WebException ex )
            {
                throw new ApplicationException( this.GetErrorMessage( ex ) , ex );
            }
#endif
        }


        /// <summary>
        /// TranslateMethod
        /// </summary>
        /// <param name="authToken"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        private string TranslateMethod( string authToken ,
                                        string text ,
                                        string from ,
                                        string to )
        {
            string translation = "";
            string format = @"http://api.microsofttranslator.com/v2/Http.svc/Translate?text={0}&from={1}&to={2}";
            string uri = String.Format(format, System.Web.HttpUtility.UrlEncode(text), from, to);

            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
            httpWebRequest.Headers.Add( "Authorization" , authToken );
            WebResponse response = null;


            try
            {
                response = httpWebRequest.GetResponse();
                using ( System.IO.Stream stream = response.GetResponseStream() )
                {
                    System.Runtime.Serialization.DataContractSerializer dcs =
                        new System.Runtime.Serialization.DataContractSerializer(Type.GetType("System.String"));
                    translation = ( string ) dcs.ReadObject( stream );
                }
            }
            finally
            {
                if ( response != null )
                {
                    response.Close();
                    response = null;
                }

            }
            return translation;
        }

        /// <summary>
        /// GetErrorMessage
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        private string GetErrorMessage( System.Net.WebException e )
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine( e.ToString() );

            // Obtain detailed error information
            string strResponse = string.Empty;
            using ( HttpWebResponse response = ( HttpWebResponse ) e.Response )
            {
                using ( Stream responseStream = response.GetResponseStream() )
                {
                    using ( StreamReader sr = new StreamReader( responseStream , System.Text.Encoding.ASCII ) )
                    {
                        strResponse = sr.ReadToEnd();
                    }
                }
            }
            sb.AppendLine( "Http status code=" + e.Status + ", error message=" + strResponse );

            return sb.ToString();
        }


    }
}
