using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEditor;

public class Level1_Display : MonoBehaviour
{
	[Header ("Object")]
	[Tooltip ("UIRoot-Panel")]
	[SerializeField]protected GameObject Panel;
	[Tooltip ("BingoIcon")]
	[SerializeField]protected GameObject Bingo;
	[Tooltip ("ErrorIcon")]
	[SerializeField]protected GameObject Error;

	[Space (10)]

	[Header ("GameObject")]
	[Tooltip ("亂數序列")]
	public List<GameObject> UFO_sequence = new List<GameObject> ();
	[Tooltip ("亂數陣列")]
	public List<GameObject> UFO_Random = new List<GameObject> ();
	///點擊陣列
	public static List<GameObject> UFO = new List<GameObject> ();

	///比對開關
	bool compare = true;

	///ShowLightTiggle開關
	bool toggle = true;

	public static bool ready = false;

	///UFO移動至下方X軸之位置
	public static int ReferencePoint = -200;

	///UFO間隔
	public static int interval = 200;

	/// UFO指標
	public static int UFO_Arr = 0;

	/// UFO Level指標
	int UFO_Level = 0;

	float time1 = 0;
	float time2 = 0;


	//點擊事件
	public class ClickEvent
	{
		///BoxCollider開關
		public static int j = -1;

		///BoxCollider開關
		public void Action (GameObject obj)
		{
			Debug.Log (obj.name);

			//掛上<Level1_Listener>腳本
			if (!obj.GetComponent<Level1_Listener> ())
				obj.AddComponent<Level1_Listener> ();

			//讓UFO能返回上一步
			if (obj.GetComponent<Level1_Listener> ().op) {
				//修改NGUI數值
				obj.GetComponent<TweenPosition> ().from = new Vector3 (ReferencePoint, -316, 0);
				obj.GetComponent<TweenPosition> ().to = obj.transform.localPosition;
				//指向下一個位置
				ReferencePoint += interval;
				//加入UFO陣列
				UFO.Add (obj);

				switch (j) {
				case 0:
					UFO [0].GetComponent<BoxCollider> ().enabled = false;
					break;
				case 1:
					UFO [1].GetComponent<BoxCollider> ().enabled = false;
					break;
				case 2:
					UFO [2].GetComponent<BoxCollider> ().enabled = false;
					break;
				case 3:
					UFO [3].GetComponent<BoxCollider> ().enabled = false;
					break;
				}

				j++;
				obj.GetComponent<Level1_Listener> ().op = false;

			} else {
				//指向上一個位置
				ReferencePoint -= interval;
				obj.GetComponent<Level1_Listener> ().op = true;
				j--;
				switch (j) { 
				case 0:
					UFO [0].GetComponent<BoxCollider> ().enabled = true;
					break;
				case 1:
					UFO [1].GetComponent<BoxCollider> ().enabled = true;
					break;
				case 2:
					UFO [2].GetComponent<BoxCollider> ().enabled = true;
					break;
				case 3:
					UFO [3].GetComponent<BoxCollider> ().enabled = true;
					break;
				}
				//從UFO陣列移除
				UFO.Remove (obj);
			}
		}
	}

	void Start ()
	{
		//設定初始值
		Initialization ();

		//製作亂數
		makeRandomArr (3);

		//Listen所有UFO
		UFO_Random.ForEach (i => UIEventListener.Get (i).onClick = new ClickEvent ().Action);

	}

	void Update ()
	{
		//持續比對答案
		AnswerCompare ();
	}

	protected void ShowLightToggle ()
	{
		//timer.TimeReset ();
		//OpenBoxCollider ();
		//GameObject.Find ("ProgressBar").GetComponent<Level1_Timer> ().enabled = true;
		try {
			if (toggle) {
				//亮下一個燈
				if (UFO_Arr <= UFO_Random.Count - 1)
					UFO_Random [UFO_Arr].GetComponent<UITexture> ().mainTexture = Resources.Load<Texture> ("JT/UFO_light/ufo0_red");

				//toggle = false
				toggle = !toggle;

			} else {
				if (UFO_Arr == UFO_Random.Count - 1) {
					//停止Invoke
//				
//					CancelInvoke ("ShowLightToggle");


					//開啟Collider
					OpenBoxCollider ();

					//關閉最後一個燈
					UFO_Random [UFO_Arr].GetComponent<UITexture> ().mainTexture = Resources.Load<Texture> ("JT/UFO_light/ufo0");
				} else {
					//關閉上一個燈
					UFO_Random [UFO_Arr].GetComponent<UITexture> ().mainTexture = Resources.Load<Texture> ("JT/UFO_light/ufo0");
				
					//指向下一個
					UFO_Arr++;

					//toggle = true
					toggle = !toggle;
				}
			}
		} catch (System.Exception e) {
			Debug.Log ("ShowLight_fail" + e);
		}
	}

	///設定初始值
	public virtual void Initialization ()
	{
		//取得Panel物件
		Panel = GameObject.Find ("Panel");

		//取得Bingo物件
		Bingo = Resources.Load<GameObject> ("JT/" + "checked-Bingo");

		//取得Error物件
		Error = Resources.Load<GameObject> ("JT/" + "checked-Error");

		//關閉2台UFO
		GameObject.Find ("UFO-3").SetActive (false);
		GameObject.Find ("UFO-4").SetActive (false);

		//將現有UFO加入UFO_sequence
		for (int i = 0; i < 3; i++) {
			UFO_sequence.Add (GameObject.Find ("UFO-" + i));
		}

	}


	//比對答案
	protected void AnswerCompare ()
	{

		if (ready) {
			//UFO亮燈
			StartCoroutine (ShowLight_TimeManagement (0f, Level1_Select._floatFieldLeft [UFO_Level], Level1_Select._floatFieldRight [UFO_Level], UFO_Random.Count));
			ready = !ready;
		}

		if (UFO.Count == UFO_Random.Count && compare) {
			//比對陣列是否一致
			if (UFO.SequenceEqual (UFO_Random)) {
				//答對
				GameObject clone = NGUITools.AddChild (Panel, Bingo);
				clone.transform.localScale = new Vector3 (140, 140, 0);
				iTween.ScaleFrom (clone, iTween.Hash ("scale", Vector3.zero, "delay", 0.2, "oncomplete", "DestroyClone", "oncompletetarget", gameObject, "oncompleteparams", clone));
//				timer.pause ();
			} else {
				//答錯
				GameObject clone = NGUITools.AddChild (Panel, Error);
				clone.transform.localScale = new Vector3 (140, 140, 0);
				iTween.ScaleFrom (clone, iTween.Hash ("scale", Vector3.zero, "delay", 0.2, "oncomplete", "DestroyClone", "oncompletetarget", gameObject, "oncompleteparams", clone));
//				timer.pause ();
			}

			//關閉比較開關,使不會重複進行比對判斷
			compare = false;

			//1秒後調用restart()
			Invoke ("restart", 1f);
		}
	}

	///開啟Collider+設定Tween數值
	public void OpenBoxCollider ()
	{
		GameObject[] target;
		target = GameObject.FindGameObjectsWithTag ("UFO");
		//開啟Collider
		target.Select (i => i.GetComponent<BoxCollider> ().enabled = true).ToArray ();

		//修改NGUI數值
		target.Select (i => i.GetComponent<TweenPosition> ().duration = 0.5f).ToArray ();
		target.Select (i => i.GetComponent<TweenPosition> ().delay = 0f).ToArray ();
	}

	///製作RandomArr陣列
	protected void makeRandomArr (int key)
	{
		//設定亂數種子
		Random.InitState (System.Guid.NewGuid ().GetHashCode ());

		for (int i = 0; i < key; i++) {
			int num = Random.Range (0, UFO_sequence.Count);
			UFO_Random.Add (UFO_sequence [num]);
			UFO_sequence [num] = UFO_sequence [UFO_sequence.Count - 1];
			UFO_sequence.RemoveAt (UFO_sequence.Count - 1);
		}
	}

	///使前後亂數不同
	protected virtual void MakeDifference ()
	{
		List<GameObject> check = UFO_Random.ToList ();
		makeRandomArr (3);
		if (UFO_Random.SequenceEqual (check))
			makeRandomArr (3);
	}

	///顯示UFO_Random陣列
	public void ShowUFO_Random ()
	{
		UFO_Random.ForEach (i => Debug.Log (i.name));
	}

	///顯示UFO陣列
	public void ShowUFO ()
	{
		UFO.ForEach (i => Debug.Log (i.name));
	}


	///用來Destroy Bingo、Error物件
	void DestroyClone (GameObject obj)
	{
		Destroy (obj);
//			timer.rezero ();
	}

	///重置
	protected virtual void restart ()
	{
		//重置UFO位置
		foreach (GameObject i in UFO) {
			//執行UIPlayTween
			i.GetComponent<UIPlayTween> ().Play (true);
			//重置op開關
			i.GetComponent<Level1_Listener> ().op = true;
			//關閉所有BoxCollider
			i.GetComponent<BoxCollider> ().enabled = false;
		}

		//指標歸零
		UFO_Arr = 0;

		// This will copy all the items from Ball_Random to Ball_sequence
		UFO_sequence.AddRange (UFO_Random);

		//清空UFO陣列
		UFO.Clear ();

		//清空UFO_Random陣列
		UFO_Random.Clear ();

		//使前後亂數不同
		MakeDifference ();

		//重置方X軸之位置
		ReferencePoint = -200;

		if (UFO_Level < Level1_Select._item-1)
			UFO_Level++;
		else
			UFO_Level = 0;

		//UFO亮燈
		StartCoroutine (ShowLight_TimeManagement (1f, Level1_Select._floatFieldLeft [UFO_Level], Level1_Select._floatFieldRight [UFO_Level], UFO_Random.Count));

		//重置ShowLightTiggle開關
		toggle = true;

		//重置BoxCollider開關
		ClickEvent.j = -1;
		 
		//判斷RandomUFO與UFO 開關
		compare = true;
	}

	IEnumerator  aaa (bool a, int b, float delay)
	{
		yield return new WaitForSeconds (delay);
		if (a) {

			Debug.Log (b);
		}

	}

	/// <summary>
	/// Shows the light time management.
	/// </summary>
	/// delay1:延遲幾秒後執行
	/// delay2:亮燈時間
	/// delay3:亮燈間隔
	/// loop:輸入UFO數量
	protected IEnumerator ShowLight_TimeManagement (float delay1, float delay2, float delay3, int loop)
	{
		yield return new WaitForSeconds (delay1);

		for (int i = 0; i < loop; i++) {
			ShowLightToggle ();
			yield return new WaitForSeconds (delay2);
			ShowLightToggle ();
			yield return new WaitForSeconds (delay3);
		}
	}
}
