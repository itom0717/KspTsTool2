Kerbal Space Program Translation Support Tool 2 for KSP 1.1
====

このプログラムは Kerbal Space Program のパーツおよびサイエンスレポートの説明文を抽出し、Microsoft Translator APIを使用して自動で機械翻訳します。  
ModuleManager.dll で使用するcfgファイルを作成します。  

###※Kerbal Space Program本体のファイルは一切変更しません。ModuleManager.dll用のConfigファイルを作成するだけです。  
###※ModuleManager.dllは別途入手してください。  

## 開発環境
 Microsoft Visual Studio Community 2015

## 必要ランタイム
 .NET Framework 4.5.2  

## 使い方

###事前準備  
自動翻訳させる場合はMicrosoft Translator APIの「クライアントID」と「顧客の秘密」を取得する必要があります。  
※Windows Azure Marketplaceへ登録等が必要です。(有料版もありますが、無料版でOK）   
※自力で翻訳する場合で英語文章の抽出のみでしたら、IDの登録は不要です。  


###実行方法  
�@ 処理対象フォルダ(GameDataフォルダ)は Kerbal Space Programをインストールした先にある、GameDataフォルダを指定します。  
  
�A 翻訳済みデータ保存フォルダ名は、�@のGameData内に作成するファイル名を指定します。  
  
�B「翻訳設定」で、自動翻訳の有効/無効を設定します。  
   自動翻訳を行なう場合は、Microsoft Translator APIの「クライアントID」と「顧客の秘密」を入力します。  
  
�C 「処理実行」で処理を開始します。  自動で機械翻訳する場合は少し時間がかかりますので、気長に待ってください。
  
�D 終了すると�Aで指定したフォルダに ModuleManager用のcfgファイル が生成されますので、ModuleManager.dll で読み込ませてください。  

　　Kerbal Space Program  
　　　　　+--- GameData  <---------- �@で指定するフォルダ
　　　　　　　+--- Squad  
　　　　　　　+--- (各MODのフォルダ)   
　　　　　　　+---  ・  
　　　　　　　+---  ・  
　　　　　　　+---  ・  
　　　　　　　+--- ModuleManager.dll <--- を設置  
　　　　　　　+--- @toJapanese  <--- �Aで指定するフォルダ名  
  

###自分で翻訳または日本語を修正する場合  
機械翻訳では（ほとんど）意味が通じないので、自分で修正してください。  

�@翻訳データ保存先にある、*.cfgファイルの日本語部分を編集します。  
　※コメントアウト // になっている場合は　//を消してください。

�AModuleManager.dllで読み込ませます。  終わり。

�B次回処理時に修正した翻訳データを使用するため、翻訳データベースに取り込んでおきます。

�C「翻訳ファイル読込」で �@で修正した*.cfgファイルを選択しで取り込みます。

※有志の方々が翻訳された ModuleManager用の *.cfgファイルも取り込めるかも。  

 

## Licence
* MIT  
    * see LICENSE.txt

## Author

[itom0717](https://github.com/itom0717)
