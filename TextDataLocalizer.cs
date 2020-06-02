using UnityEngine;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class TextDataLocalizer : MonoBehaviour
{
	[SerializeField]
	List<TextDataLocalizerItem> items = new List<TextDataLocalizerItem> ();

	// 日本語テキストをすべて表示
	[Button, ButtonGroup ("Button"), PropertyOrder (-1)]
	void ViewTextsJP ()
	{
		foreach (TextDataLocalizerItem item in items)
			item.ViewTextJP ();
	}

	// 英語テキストをすべて表示
	[Button, ButtonGroup ("Button"), PropertyOrder (-1)]
	void ViewTextsEN ()
	{
		foreach (TextDataLocalizerItem item in items)
			item.ViewTextEN ();
	}

	// Awake時にローカライズ
	void Awake () => Localize ();

	// ローカライズ
	// * 日本以外は英語を表示
	public void Localize ()
	{
		if (Application.systemLanguage == SystemLanguage.Japanese)
			ViewTextsJP ();
		else
			ViewTextsEN ();
	}
}

// リスト項目
[Serializable]
class TextDataLocalizerItem
{
	// テキストデータ
	// * 重複したIDの項目を持ってたら警告を出す
	[SerializeField, LabelWidth (30),
	 InfoBox ("データにIDが重複してる項目があるよ〜", InfoMessageType.Warning, "DataHasDoubleID"),
	 HorizontalGroup ("G1", 0.5f)]
	TextData data;

	// ID
	// * プルダウンで表示、変更した時に自動でテキストを変える
	// * IDエラーのときはエラーを出す
	[SerializeField, LabelWidth (30), ValueDropdown ("GetIDList", DropdownWidth = 250), OnValueChanged ("PreviewInInspector"),
	 InfoBox ("IDとマッチするデータがないよ〜", InfoMessageType.Error, "ErrorID"),
	 HorizontalGroup ("G1", 0.5f)]
	string ID;

	// IDエラーの定義
	// * dataのリストにマッチしたIDがないときはエラー
	// * IDがnull or dataが未定義のときはエラーじゃないないことにする
	bool ErrorID => (ID == null) ? false : !(data?.HasID (ID)) ?? false;

	// データが重複したIDを持ってるか取得
	// * 持ってたら警告を出す
	bool DataHasDoubleID => data?.HasOverlappingItem () ?? false;

	// データからIDのリストを取得
	List<string> GetIDList () => data?.GetIDsList ();

	// 日本語テキスト用オブジェクト
	[SerializeField, LabelWidth (30), LabelText ("JP"),
	 HorizontalGroup ("G2", 0.5f)]
	Text textJP;

	// 英語テキスト用オブジェクト
	[SerializeField, LabelWidth (30), LabelText ("EN"),
	 HorizontalGroup ("G2", 0.5f)]
	Text textEN;

	// 日本語テキスト（プレビュー）
	[SerializeField, HideLabel, ReadOnly, MultiLineProperty (3),
	 HorizontalGroup ("G3", 0.5f)]
	string strJP;

	// 英語テキスト（プレビュー）
	[SerializeField, HideLabel, ReadOnly, MultiLineProperty (3),
	 HorizontalGroup ("G3", 0.5f)]
	string strEN;

	// インスペクタにテキストのプレビュー
	void PreviewInInspector ()
	{
		strJP = data?.GetTextJP (ID);
		strEN = data?.GetTextEN (ID);

		PreviewTexts ();
	}

	// 日本語テキストを表示
	public void ViewTextJP ()
	{
		textEN?.gameObject?.SetActive (false);
		textJP?.gameObject?.SetActive (true);

		PreviewTexts ();
	}

	// 英語テキストを表示
	public void ViewTextEN ()
	{
		textJP?.gameObject?.SetActive (false);
		textEN?.gameObject?.SetActive (true);

		PreviewTexts ();
	}

	// テキストオブジェクトにテキストを代入
	public void PreviewTexts ()
	{
		if (textJP != null) textJP.text = strJP;
		if (textEN != null) textEN.text = strEN;
	}
}