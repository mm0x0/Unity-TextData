![Description](https://user-images.githubusercontent.com/26345138/83524090-10579a80-a51e-11ea-9186-2e182deb33ba.gif)

## 説明

- UnityのScriptableObjectでテキストデータベースっぽいものを作りたい
- IDでデータの取得（Odinで重複バリデーションをかける）
- データベースを取ってきてシーン上でプレビュー・ローカライズできるようにする（途中）

## 動作環境
 - C# 6.0
 - Odin Inspectorが必要

## ファイル
- TextData.cs … テキストデータベース用ScriptableObject
- TextDataLocalizer.cs … シーン上でローカライズ・プレビューできるコンポーネント

## 使い方 （TextData）

1. create > TextData
2. データを入力
    - IDは重複しないように
    - JPに日本語、ENに英語を入力

## 使い方 (TextDataLocalizer)

1. AddComponent > TextDataLocalizer
2. 作成したTextDataをDataにドラッグ&ドロップ
3. 日本語TextをJP、英語テキストをENにドラッグ&ドロップ
    - IDを選ぶとデータを取ってきてテキストを表示します
    - View Textボタンを押すとすべてのテキストをプレビューします（JP・ENのSetActiveを切り替え）
        - PlayModeではAwake時にローカライズされます
