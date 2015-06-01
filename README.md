SpriteStudioPlayerForUGUI
==================

SpriteStudioのデータをUnityのuGUIで再生するためのライブラリを開発中です。
まだ、<strong>結構未実装部分あります</strong>。
気が向いたときに順次実装してます。


Pull Request
==
基本的に歓迎です。
要望等も(実装するかは分かりませんが)受け付けてます。
適当にフォークして改造してもらっても一向に構いません。


ライセンス
==
ほぼBSDです。
License.txtを見てください。


インポート方法
==

## 自動インポート
メニューの「SpriteStudioForUGUI/自動インポート」から有効・無効を切り替えられます。
オンになっている場合は.sspjファイルをプロジェクトに追加した際に自動でインポートが開始されます。
オフの場合はインポート用ファイルのみ生成して、prefabは生成しません。


## 部分インポート
.sspjファイルをプロジェクトに追加した際に、sspjファイルと同じ場所に部分インポート用ファイルが生成されます。
このファイルのインスペクター上から、特定のアニメーションのみを指定してインポートする事が出来ます。
※セルマップは必ずインポートされます。


## 出力先
メニューの「SpriteStudioForUGUI/出力先の設定」から指定出来ます。
ここで設定された出力先に以下のような構造で出力されます。
<pre>sspjのファイル名(.sspj)
　　├ CellMaps
　　│　　├ 各セルマップ(.ssce) : assetファイル
　　│　　：　　：
　　│　　└ 各セルマップ(.ssce) : assetファイル
　　└ Sprites
　　　　├ 各アニメーショングループ(.ssae)
　　　　│　　├ 各アニメーション(.ssae内&lt;anime&gt;) : prefab
　　　　│　　：　　：
　　　　│　　└ 各アニメーション(.ssae内&lt;anime&gt;) : prefab
　　　　：
　　　　└各アニメーショングループ(.ssae)
　　　　　　　：
</pre>

Prefab
==

インポートするとセルマップの情報が格納されたassetファイルと、スプライト情報が構築されたPrefabが生成されます。
uGUIのCanvas内にPrefabをInstantiateすることで表示する事が出来ます。


その他
==
以下のフォルダは開発の都合で.gitignoreに登録されています。
/SpriteStudioForUGUI/Assets/Exports
/SpriteStudioForUGUI/Assets/SampleData

