using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class LongPressButton : 
MonoBehaviour,
IPointerDownHandler,
IPointerUpHandler
{
		/// <summary>
		/// 長押し時に呼び出すイベント
		/// </summary>
		public UnityEvent onLongPress = new UnityEvent ();
		/// <summary>
		/// 長押し判定の間隔（この間隔毎にイベントが呼ばれる）
		/// </summary>
		public float intervalAction = 0.1f;
		/// <summary>
		/// 長押し判定開始までの間隔 (この間隔後に判定が開始される)
		/// </summary>
		public float intervalInit = 0.5f;
		// 押下開始時にもイベントを呼び出すフラグ
		public bool callEventFirstPress;

		// 次の押下判定時間
		float nextTime = 0f;

		/// <summary>
		/// 押下状態
		/// </summary>
		public bool isPressed
		{
				get;
				private set;
		}
		/// <summary>
		/// スクローラーの参照
		/// </summary>
		public LongPressScroll scroll
		{
				get;
				set;
		}

		void Update ()
		{
				if (isPressed && nextTime < Time.realtimeSinceStartup) {
						onLongPress.Invoke ();
						nextTime = Time.realtimeSinceStartup + intervalAction;
				}
		}

		public void OnPointerDown (PointerEventData eventData)
		{				
				if (callEventFirstPress) {
						onLongPress.Invoke ();
				}
				StartCoroutine ("DelayLongPress");
		}

		// intervalInit秒後に長押し判定開始
		IEnumerator DelayLongPress(){
				yield return new WaitForSeconds (intervalInit);
				isPressed = true;
				nextTime = Time.realtimeSinceStartup + intervalAction;
		}

		public void OnPointerUp (PointerEventData eventData)
		{
				// 長押し判定開始前に離したorスクロールした場合
				if (!isPressed) {
						StopCoroutine ("DelayLongPress");	//コルーチン停止
						EndPress ();
						return;
				}
				// 押下状態が続いている(isDrag:true)なら何もしない
				if ((scroll??(scroll = FindObjectOfType<LongPressScroll>())).CheckPressedStill (this))
						return;
				EndPress ();
		}

		/// <summary>
		/// 押下状態終了時に呼ぶメソッド
		/// </summary>
		public void EndPress(){
				isPressed = false;
		}

}
