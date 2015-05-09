using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class LongPressScroll : ScrollRect{

	/// <summary>
	/// ドラッグ状態
	/// </summary>
	public bool isDrag
	{
		get;
		private set;
	}

	[SerializeField]
	private LongPressButton pressedButton;

	new void Start () {
		base.Start ();	// 必要かどうか不明
		// 子のLongPressButtonに自身の参照を持たせる
		LongPressButton[] buttons = GetComponentsInChildren<LongPressButton> ();
		foreach (LongPressButton item in buttons) {
			item.scroll = this;
		}
	}

	/// <summary>
	/// 現在も押下状態であるかの判定を返す
	/// </summary>
	public bool CheckPressedStill(LongPressButton button){
		pressedButton = button;
		if (isDrag)
			return true;
		return false;
	}

	public override void OnBeginDrag(PointerEventData eventData){
		base.OnBeginDrag (eventData);	// 削除した場合、挙動に影響有り
		isDrag = true;
	}

	public override void OnEndDrag(PointerEventData eventData){
		base.OnEndDrag (eventData);	// 削除した場合、挙動に影響有り
		isDrag = false;
		if(pressedButton)
			pressedButton.EndPress ();
		pressedButton = null;
	}

}
