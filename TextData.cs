using UnityEngine;
using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;

[CreateAssetMenu]
public class TextData : ScriptableObject
{
	// データリスト
	[SerializeField,
	 LabelText      ("Items (JP / EN)"),
	 InfoBox        ("IDが重複してるよ〜", InfoMessageType.Warning, "HasOverlappingItem"),
	 OnValueChanged ("SetDefaultID")]
	List<TextDataItem> items = new List<TextDataItem> ();

	// IDのリストを取得
	public List<string> GetIDsList ()
	{
		List<string> list = new List<string> ();

		foreach (TextDataItem item in items)
			list.Add (item.GetID ());

		return list;
	}

	// 項目新規作成時にIDに番号を振る
	void SetDefaultID ()
	{
		for (int i = 0; i < items.Count; i++)
			if (items [i].GetID () == "")
				items [i].SetID ((i + 1).ToString ());
	}

	// リストの項目が重複してるかチェック
	// * HashSetで調べる
	public bool HasOverlappingItem ()
	{
		HashSet<string> list = new HashSet<string> ();
		bool overlapping = false;

		foreach (TextDataItem item in items)
			if (!list.Add (item.GetID ()))
			{
				overlapping = true;
				Debug.LogWarning ("TextData : ID [ " + item.GetID () + " ] が重複してるよ〜");
			}

		return overlapping;
	}

	// IDから日本語テキストを取得
	public string GetTextJP (string ID)
	{
		string txt = "";

		foreach (TextDataItem item in items)
			if (item.GetID () == ID)
				txt = item.GetTextJP ();

		return txt;
	}

	// IDから英語テキストを取得
	public string GetTextEN (string ID)
	{
		string txt = "";

		foreach (TextDataItem item in items)
			if (item.GetID () == ID)
				txt = item.GetTextEN ();

		return txt;
	}

	public string GetLocalizedText (string ID)
	{
		return
			(Application.systemLanguage == SystemLanguage.Japanese) ?
				GetTextJP (ID):
				GetTextEN (ID);
	}

	// IDがあるか判別
	public bool HasID (string ID)
	{
		bool hasID = false;

		foreach (TextDataItem item in items)
			if (item.GetID () == ID)
				hasID = true;

		return hasID;
	}
}

// リスト項目
[Serializable]
class TextDataItem
{
	// ID
	[SerializeField, LabelWidth (20), Required ("IDを入力してね〜")]
	string ID = "";

	// 日本語テキスト
	[SerializeField, HideLabel, MultiLineProperty (3),
	 HorizontalGroup ("G", 0.5f),
	 VerticalGroup   ("G/JP")]
	string strJP = "<JP>";

	// 英語テキスト
	[SerializeField, HideLabel, MultiLineProperty (3),
	 HorizontalGroup ("G", 0.5f),
	 VerticalGroup   ("G/EN")]
	string strEN = "<EN>";

	// IDを取得
	public string GetID () => ID;
	public string SetID (string ID) => this.ID = ID;

	// テキストを取得
	public string GetTextJP () => strJP;
	public string GetTextEN () => strEN;
}