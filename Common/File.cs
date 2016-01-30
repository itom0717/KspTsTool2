using System;
using System.Collections.Generic;
using Microsoft.VisualBasic;

/// <summary>
/// Namespace Common
/// </summary>
namespace Common
{
    /// <summary>
    /// File関連処理クラス（C#版）
    /// </summary>
    static public class File
    {
        /// <summary>
        /// パスの最後に\を付ける
        /// </summary>
        /// <param name="targetPath">対象パス</param>
        /// <returns>対象パスの最後に\を付けた文字列</returns>
        /// <remarks>最後に\が着いていたら対象パスをそのまま返す</remarks>
        public static string AddDirectorySeparator( string targetPath )
        {
            if ( String.IsNullOrEmpty( targetPath ) )
            {
                //null or Empty
                return "";
            }
            //一旦削除して追加する
            return targetPath.TrimEnd( System.IO.Path.DirectorySeparatorChar ) + System.IO.Path.DirectorySeparatorChar;
        }

        /// <summary>
        /// パスの最後の\を取り除く
        /// </summary>
        /// <param name="targetPath">対象パス</param>
        /// <returns>\を取り除いたパス</returns>
        /// <remarks>最後が\ではない場合は、対象パスをそのまま返す</remarks>
        public static string DeleteDirectorySeparator( string targetPath )
        {
            if ( String.IsNullOrEmpty( targetPath ) )
            {
                //null or Empty
                return "";
            }
            //削除して返す
            return targetPath.TrimEnd( System.IO.Path.DirectorySeparatorChar );
        }

        /// <summary>
        /// パスからファイル名のみ取得する。
        /// </summary>
        /// <param name="targetPath">対象パス</param>
        /// <returns>対象パスから取得したファイル名</returns>
        public static string GetFileName( string targetPath )
        {
            if ( String.IsNullOrEmpty( targetPath ) )
            {
                //null or Empty
                return "";
            }
            return System.IO.Path.GetFileName( targetPath );
        }

        /// <summary>
        /// パスからファイル名を除いたパス名のみ取得する。
        /// </summary>
        /// <param name="targetPath">対象パス</param>
        /// <returns>対象パスから取得したパス名</returns>
        public static string GetDirectoryName( string targetPath )
        {
            if ( String.IsNullOrEmpty( targetPath ) )
            {
                //null or Empty
                return "";
            }

            return File.AddDirectorySeparator( System.IO.Path.GetDirectoryName( targetPath ) );
        }

        /// <summary>
        /// 拡張子を取り除いたパスを取得する
        /// </summary>
        /// <param name="targetPath">対象パス</param>
        /// <returns>対象パスから拡張子を取り除いたパス</returns>
        public static string GetWithoutExtension( string targetPath )
        {
            if ( String.IsNullOrEmpty( targetPath ) )
            {
                //null or Empty
                return "";
            }

            //IO.Path.GetFileNameWithoutExtensionはファイル名しか返さないので、パスを付ける
            return File.GetDirectoryName( targetPath ) + System.IO.Path.GetFileNameWithoutExtension( targetPath );
        }

        /// <summary>
        /// パスから拡張子のみを取得する
        /// </summary>
        /// <param name="targetPath">対象パス</param>
        /// <returns>取得した拡張子</returns>
        /// <remarks>ピリオドは付かない</remarks>
        public static string GetExtension( string targetPath )
        {
            if ( String.IsNullOrEmpty( targetPath ) )
            {
                //null or Empty
                return "";
            }

            //IO.Path.GetExtensionはピリオドが付くので取り除く
            targetPath = System.IO.Path.GetExtension( targetPath );

            if ( String.IsNullOrEmpty( targetPath ) )
            {
                //null or Empty
                return "";
            }

            if ( targetPath.Substring( 0 , 1 ).Equals( "." ) )
            {
                //ピリオド取り除く
                return targetPath.Substring( 1 );//ピリオド以降の文字を返す
            }
            else
            {
                return targetPath;
            }
        }

        /// <summary>
        /// ファイルが存在するか確認
        /// </summary>
        /// <param name="targetPath">対象パス</param>
        /// <returns>存在すればTrueを返す</returns>
        public static bool ExistsFile( string targetPath )
        {
            if ( String.IsNullOrEmpty( targetPath ) )
            {
                //null or Empty
                return false;
            }

            try
            {
                return System.IO.File.Exists( targetPath );
            }
            catch
            {
                //アクセス権がないなどの場合
                return false;
            }
        }

        /// <summary>
        /// ディレクトリが存在するか確認
        /// </summary>
        /// <param name="targetPath">対象パス</param>
        /// <returns>存在すればTrueを返す</returns>
        public static bool ExistsDirectory( string targetPath )
        {
            if ( String.IsNullOrEmpty( targetPath ) )
            {
                //null or Empty
                return false;
            }

            try
            {
                return System.IO.Directory.Exists( targetPath );
            }
            catch
            {
                //アクセス権がないなどの場合
                return false;
            }
        }

        /// <summary>
        /// ディレクトリが空かチェック
        /// </summary>
        /// <param name="targetPath">チェックするパス</param>
        /// <returns>空の場合はTrueを返す</returns>
        public static bool IsEmptyDirectory( string targetPath )
        {
            if ( String.IsNullOrEmpty( targetPath ) )
            {
                //null or Empty
                return false;
            }

            if ( !File.ExistsDirectory( targetPath ) )
            {
                //ディレクトリが存在しない場合
                return false;
            }


            try
            {
                String[] entries = System.IO.Directory.GetFileSystemEntries(targetPath);
                if ( entries.Length == 0 )
                {
                    //空
                    return true;
                }
                else
                {
                    //何らかのファイルが存在する
                    return false;
                }

            }
            catch
            {
                //アクセス権がないなどの場合
                return false;
            }

        }

        /// <summary>
        /// ファイルをコピー
        /// </summary>
        /// <param name="sourcePath">コピー元ファイル名</param>
        /// <param name="destinationPath">コピー先ファイル名</param>
        /// <param name="isOverwrite">上書きするか？</param>
        public static void CopyFile( string sourcePath ,
                                     string destinationPath ,
                                     bool isOverwrite = false )
        {
            if ( String.IsNullOrEmpty( sourcePath ) || String.IsNullOrEmpty( destinationPath ) )
            {
                //null or Empty
                return;
            }

            System.IO.File.Copy( sourcePath , destinationPath , isOverwrite );
        }

        /// <summary>
        /// ファイルの移動、名前変更
        /// </summary>
        /// <param name="targetPath">移動元ファイル名</param>
        /// <param name="destinationPath">移動先ファイル名</param>
        public static void MoveFile( string targetPath ,
                                     string destinationPath )
        {
            if ( String.IsNullOrEmpty( targetPath ) || String.IsNullOrEmpty( destinationPath ) )
            {
                //null or Empty
                return;
            }

            System.IO.File.Move( targetPath , destinationPath );
        }


        /// <summary>
        /// ファイルを削除
        /// </summary>
        /// <param name="targetPath">削除するファイルのフルパス</param>
        /// <param name="useRecycleBox">削除時にゴミ箱に入れるか？</param>
        /// <returns>成功したらTrueを返す。ファイルが存在しない場合もTrueを返す</returns>
        public static bool DeleteFile( string targetPath ,
                                       bool useRecycleBox = false )
        {
            if ( String.IsNullOrEmpty( targetPath ) )
            {
                //null or Empty
                return false;
            }

            try
            {
                if ( File.ExistsFile( targetPath ) )
                {
                    if ( useRecycleBox )
                    {
                        //ゴミ箱へ入れる
                        Microsoft.VisualBasic.FileIO.FileSystem.DeleteFile(
                            targetPath ,
                            Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs ,
                            Microsoft.VisualBasic.FileIO.RecycleOption.SendToRecycleBin );

                    }
                    else
                    {
                        //直接削除
                        Microsoft.VisualBasic.FileIO.FileSystem.DeleteFile(
                            targetPath ,
                            Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs ,
                            Microsoft.VisualBasic.FileIO.RecycleOption.DeletePermanently );

                    }

                }
                return true;
            }
            catch
            {
                //削除失敗
                return false;
            }
        }

        /// <summary>
        /// ディレクトリ作成
        /// </summary>
        /// <param name="targetPath">作成するディレクトリ名</param>
        /// <returns>作成できたらTrueを返す。</returns>
        public static bool CreateDirectory( string targetPath )
        {
            if ( String.IsNullOrEmpty( targetPath ) )
            {
                //null or Empty
                return false;
            }
            try
            {
                System.IO.Directory.CreateDirectory( targetPath );

                return File.ExistsDirectory( targetPath );
            }
            catch
            {
                //作成失敗
                return true;
            }
        }

        /// <summary>
        /// ディレクトリを削除
        /// </summary>
        /// <param name="targetPath">削除するディレクトリ</param>
        /// <param name="useRecycleBox">削除時にゴミ箱に入れるか？</param>
        /// <returns>成功したらTrueを返す</returns>
        public static bool DeleteDirectry( string targetPath ,
                                           bool useRecycleBox = false )
        {
            if ( String.IsNullOrEmpty( targetPath ) )
            {
                //null or Empty
                return false;
            }

            try
            {

                if ( useRecycleBox )
                {
                    //ゴミ箱へ入れる
                    Microsoft.VisualBasic.FileIO.FileSystem.DeleteDirectory(
                        targetPath ,
                        Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs ,
                        Microsoft.VisualBasic.FileIO.RecycleOption.SendToRecycleBin );

                }
                else
                {
                    //直接削除
                    Microsoft.VisualBasic.FileIO.FileSystem.DeleteDirectory(
                        targetPath ,
                        Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs ,
                        Microsoft.VisualBasic.FileIO.RecycleOption.DeletePermanently );

                }
                return true;
            }
            catch
            {
                //削除失敗
                return false;
            }
        }



        /// <summary>
        /// ファイル一覧を取得
        /// </summary>
        /// <param name="targetPath">検索パス</param>
        /// <param name="wildcards">ワイルドカード</param>
        /// <param name="findSubDir">サブディレクトリも検索するか？</param>
        /// <returns>検索結果のリスト</returns>
        public static List<String> GetFileList( string targetPath ,
                                                string wildcards ,
                                                bool findSubDir = false )
        {

            if ( String.IsNullOrEmpty( targetPath ) )
            {
                //null or Empty
                return new List<String>();
            }
            var searchType = Microsoft.VisualBasic.FileIO.SearchOption.SearchTopLevelOnly;
            if ( findSubDir )
            {
                searchType = Microsoft.VisualBasic.FileIO.SearchOption.SearchAllSubDirectories;
            }

            //ファイル一覧取得
            //System.Collections.ObjectModel.ReadOnlyCollection(Of String)では使いにくいので、List(Of String)に変換して戻す
            return new List<String>( Microsoft.VisualBasic.FileIO.FileSystem.GetFiles( targetPath , searchType , wildcards ) );
        }


        /// <summary>
        /// フォルダ一覧を取得
        /// </summary>
        /// <param name="targetPath">検索パス</param>
        /// <param name="wildcards">ワイルドカード</param>
        /// <param name="findSubDir">サブディレクトリも検索するか？</param>
        /// <returns>検索結果のリスト</returns>
        public static List<String> GetFolderList( string targetPath ,
                                                  string wildcards ,
                                                  bool findSubDir = false )
        {

            if ( String.IsNullOrEmpty( targetPath ) )
            {
                //null or Empty
                return new List<String>();
            }
            var searchType = System.IO.SearchOption.TopDirectoryOnly;
            if ( findSubDir )
            {
                searchType = System.IO.SearchOption.AllDirectories;
            }

            //ファイル一覧取得
            //System.Collections.ObjectModel.ReadOnlyCollection(Of String)では使いにくいので、List(Of String)に変換して戻す
            return new List<String>( System.IO.Directory.GetDirectories( targetPath , wildcards , searchType ) );
        }



        /// <summary>
        /// パスにスペースが含まれている場合は、前後にダブルクオートをつける
        /// </summary>
        /// <param name="targetPath">対象パス</param>
        /// <returns></returns>
        public static string AddDoubleQuotesIfTthereSpace( string targetPath )
        {
            if ( targetPath.IndexOf( @" " ) == -1 )
            {
                //スペースが含まれていないので、そのまま返す
                return targetPath;
            }
            else
            {
                //スペースが含まれているので前後に"をつける
                return '"' + targetPath + '"';
            }
        }


        /// <summary>
        /// ファイルのサイズを返す。
        /// </summary>
        /// <param name="targetPath">対象ファイル</param>
        /// <returns>ファイルサイズのバイト数</returns>
        /// <remarks>ファイルが存在しない場合や、アクセス権が無いファイルの場合はnullを返す。</remarks>
        public static Nullable<long> GetFilesize( string targetPath )
        {
            try
            {
                var fi = new System.IO.FileInfo(targetPath);
                return fi.Length;
            }
            catch
            {
                return null;
            }

        }

        /// <summary>
        /// ファイルの作成日時を取得する
        /// </summary>
        /// <param name="targetPath">対象ファイル</param>
        /// <returns>ファイルサイズの作成日時</returns>
        /// <remarks>ファイルが存在しない場合や、アクセス権が無いファイルの場合はnullを返す。</remarks>
        public static Nullable<DateTime> GetCreatedDateTime( string targetPath )
        {
            try
            {
                return System.IO.File.GetCreationTime( targetPath );
            }
            catch
            {
                return null;
            }
        }


        /// <summary>
        /// ファイルの更新日時を取得する
        /// </summary>
        /// <param name="targetPath">対象ファイル</param>
        /// <returns>ファイルサイズの更新日時</returns>
        /// <remarks>ファイルが存在しない場合や、アクセス権が無いファイルの場合はnullを返す。</remarks>
        public static Nullable<DateTime> GetLastModifiedDateTime( string targetPath )
        {
            try
            {
                return System.IO.File.GetLastWriteTime( targetPath );
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// ファイルの最終アクセス日時を取得する
        /// </summary>
        /// <param name="targetPath">対象ファイル</param>
        /// <returns>ファイルサイズの最終アクセス日時</returns>
        /// <remarks>ファイルが存在しない場合や、アクセス権が無いファイルの場合はnullを返す。</remarks>
        public static Nullable<DateTime> GetLastAccessDateTime( string targetPath )
        {
            try
            {
                return System.IO.File.GetLastAccessTime( targetPath );
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 現在のユーザーのデスクトップのパスを返す
        /// </summary>
        /// <returns></returns>
        public static string GetDesktopDirectory()
        {
            return File.AddDirectorySeparator( System.Environment.GetFolderPath( Environment.SpecialFolder.DesktopDirectory ).ToString() );
        }


        /// <summary>
        /// 現在のユーザーのマイドキュメントのパスを返す
        /// </summary>
        /// <returns></returns>
        public static string GetMyDocumentsDirectory()
        {
            return File.AddDirectorySeparator( System.Environment.GetFolderPath( Environment.SpecialFolder.Personal ).ToString() );
        }

        /// <summary>
        /// ファイルロック中か判定
        /// </summary>
        /// <param name="targetPath">対象ファイル</param>
        /// <returns>ロック中の場合はTrueを返す。</returns>
        public static bool IsLocked( string targetPath )
        {
            try
            {
                using ( var stream = System.IO.File.OpenRead( targetPath ) )
                {
                    stream.Close();
                }
                return false;
            }
            catch
            {
                return true;
            }
        }



        /// <summary>
        /// ファイルの内容をMD5ハッシュ値で比較する
        /// </summary>
        /// <param name="targetPath1"></param>
        /// <param name="targetPath2"></param>
        /// <returns>同じ場合はTrueを返す</returns>
        public static bool IsSameFile( string targetPath1 ,
                                       string targetPath2 )
        {
            return File.IsSameFile( targetPath1 , targetPath2 , 0 );
        }

        /// <summary>
        /// ファイルの内容をMD5ハッシュ値で比較する
        /// </summary>
        /// <param name="targetPath1"></param>
        /// <param name="targetPath2"></param>
        /// <param name="errorRetryCount">エラー時のリトライ回数</param>
        /// <returns>同じ場合はTrueを返す</returns>
        private static bool IsSameFile( string targetPath1 ,
                                       string targetPath2 ,
                                       int errorRetryCount )
        {
            try
            {
                //File.OpenReadではロック中のファイルを開くとエラーになる
                //そのため、 FileStream でオプションを指定して開く
                if ( File.ExistsFile( targetPath1 ) && File.ExistsFile( targetPath2 ) )
                {
                    var  md5  = System.Security.Cryptography.MD5.Create();
                    string file1md5 = File.GetFileMd5HashValue(targetPath1);
                    string file2md5 = File.GetFileMd5HashValue(targetPath2);


                    if ( file1md5.Equals( file2md5 ) )
                    {
                        //同じ内容
                        return true;
                    }
                    else
                    {
                        //違う内容
                        return false;
                    }
                }
                else
                {
                    //どちらかのファイルが存在しない。
                    return false;
                }

            }
            catch
            {
                //エラー
                if ( errorRetryCount <= 0 )
                {
                    //1回はリトライする
                    System.Threading.Thread.Sleep( 3000 ); //数秒待つ

                    return File.IsSameFile( targetPath1 , targetPath2 , errorRetryCount + 1 );
                }
                else
                {
                    return false;
                }
            }




        }

        /// <summary>
        /// ファイルのMD5ハッシュ値を求める
        /// </summary>
        /// <param name="targetPath">対象ファイル</param>
        /// <returns></returns>
        private static string GetFileMd5HashValue( string targetPath )
        {
            using ( var stream = new System.IO.FileStream( targetPath ,
                                                           System.IO.FileMode.Open ,
                                                           System.IO.FileAccess.Read ,
                                                           System.IO.FileShare.ReadWrite ) )
            {
                var  md5  = System.Security.Cryptography.MD5.Create();
                byte[] bs = md5.ComputeHash(stream);
                return BitConverter.ToString( bs ).ToLower().Replace( "-" , "" );
            }
        }





        /// <summary>
        /// ファイル名に使用禁止の文字がれば、その文字を置換する
        /// </summary>
        /// <param name="targetPath">ファイル名</param>
        /// <param name="toText">置換する文字</param>
        /// <returns>置換後の文字列</returns>
        /// <remarks>URLの使用不可文字も対応</remarks>
        public static string ChangeInvalidFileName( string targetPath,
                                                    string toText )
        {
            string newFilename = targetPath;

            //ファイル名に使用できない文字を取得
            char[] invalidChars = System.IO.Path.GetInvalidFileNameChars();

            //使用禁止文字を置換
            foreach ( char c in invalidChars )
            {
                newFilename = newFilename.Replace( c.ToString() , toText );
            }

            //追加の禁止文字
            newFilename = newFilename.Replace( @"&" , toText );
            newFilename = newFilename.Replace( @" " , toText );
            newFilename = newFilename.Replace( @"?" , toText );
            newFilename = newFilename.Replace( @"+" , toText );
            newFilename = newFilename.Replace( @"#" , toText );
            newFilename = newFilename.Replace( @"!" , toText );
            newFilename = newFilename.Replace( @"%" , toText );

            return newFilename;
        }




        /// <summary>
        /// ショートカット作成
        /// </summary>
        /// <param name="shortcutPath">作成するショートカットのフルパス</param>
        /// <param name="targetLinkPath">ショートカットのリンク先</param>
        /// <param name="iocnPath">アイコンのファイル名</param>
        /// <param name="iconIndex">アイコンのインデックス番号</param>
        /// <param name="workingDirectory">作業フォルダ</param>
        /// <param name="arguments">コマンドパラメータ 「リンク先」の後ろに付く </param>
        /// <param name="description">コメント</param>
        /// <param name="hotkey">ホットキー</param>
        /// <param name="windowStyle">起動時のウィンドウの状態</param>
        public static void CreateShortcut( string shortcutPath ,
                                           string targetLinkPath ,
                                           string iocnPath = "" ,
                                           int iconIndex = 0 ,
                                           string workingDirectory = "" ,
                                           string arguments = "" ,
                                           string description = "" ,
                                           string hotkey = "" ,
                                           System.Diagnostics.ProcessWindowStyle windowStyle = System.Diagnostics.ProcessWindowStyle.Normal )
        {

            //WshShellを作成
            Type t = Type.GetTypeFromCLSID(new Guid("72C24DD5-D70A-438B-8A42-98424B88AFB8"));
            dynamic shell = Activator.CreateInstance(t);

            //WshShortcutを作成
            var shortcut = shell.CreateShortcut(shortcutPath);

            //リンク先
            shortcut.TargetPath = targetLinkPath;

            //アイコン
            if ( iocnPath.Equals( "" ) )
            {
                //ショートカットのリンク先ファイルから
                shortcut.IconLocation = targetLinkPath + "," + iconIndex.ToString();
            }
            else
            {
                //アイコンは別ファイルから
                shortcut.IconLocation = iocnPath + "," + iconIndex.ToString();
            }

            //作業フォルダ
            if ( !workingDirectory.Equals( "" ) )
            {
                shortcut.WorkingDirectory = workingDirectory;
            }

            //コマンドパラメータ 「リンク先」の後ろに付く
            if ( !arguments.Equals( "" ) )
            {
                shortcut.Arguments = arguments;
            }

            //ショートカットキー（ホットキー） 
            //ex "Ctrl+Alt+Shift+F12"
            if ( !hotkey.Equals( "" ) )
            {
                shortcut.Hotkey = hotkey;
            }

            //コメント
            if ( !description.Equals( "" ) )
            {
                shortcut.Description = description;
            }


            //起動時のウィンドウの状態
            //System.Diagnostics.ProcessWindowStyle.Maximized --- 最大化されたウィンドウスタイルを指定します。
            //System.Diagnostics.ProcessWindowStyle.Minimized --- 最小化されたウィンドウスタイルを指定します。
            //System.Diagnostics.ProcessWindowStyle.Normal --- 通常の表示ウィンドウスタイルを指定します。デフォルト。
            shortcut.WindowStyle = windowStyle;


            //ショートカットを作成
            shortcut.Save();

            //後始末
            System.Runtime.InteropServices.Marshal.FinalReleaseComObject( shortcut );
            System.Runtime.InteropServices.Marshal.FinalReleaseComObject( shell );
        }


        /// <summary>
        /// ディレクトリ パスやファイル名を結合して１つのパスにする
        /// </summary>
        /// <param name="targetPath">連結するパス、複数指定可能</param>
        /// <returns></returns>
        public static string CombinePath( params string[] targetPath )
        {
            if ( targetPath.Length <= 0 )
            {
                //引数0の場合は空欄を返す
                return "";
            }
            string combinePath = targetPath[0];
            for ( int i = 1; i < targetPath.Length; i++ )
            {
                combinePath = Microsoft.VisualBasic.FileIO.FileSystem.CombinePath( combinePath , targetPath[i] );
            }
            return combinePath;
        }


        /// <summary>
        /// 自分自身のアプリケーションディレクトリを取得する
        /// </summary>
        /// <returns></returns>
        public static string GetApplicationDirectory()
        {

            string path = System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase;

            //URIを通常のパス形式に変換する
            Uri u = new Uri(path);
            path = u.LocalPath + Uri.UnescapeDataString( u.Fragment );
            return File.GetDirectoryName( path );
        }



    }
}
