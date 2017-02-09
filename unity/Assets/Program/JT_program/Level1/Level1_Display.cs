using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEditor;
using AnimationOrTween;
using System;

public class Level1_Display : MonoBehaviour
{
	[Header ("Object")]
	[Tooltip ("UIRoot-Panel")]
	[SerializeField]protected GameObject Panel;
	[Tooltip ("UIRoot-Panel-Display-UFO_group")]
	[SerializeField]protected GameObject UFO_group;
	[Tooltip ("BingoIcon")]
	[SerializeField]protected GameObject Bingo;
	[Tooltip ("ErrorIcon")]
	[SerializeField]protected GameObject Error;
	[Tooltip ("UFO Default Light")]
	[SerializeField]protected Texture UFO_DefaultLight;
	[Tooltip ("UFO Red Light")]
	[SerializeField]protected Texture UFO_RedLight;

	[Space (10)]

	[Header ("GameObject")]
	[Tooltip ("亂數序列")]
	public List<GameObject> UFO_sequence = new List<GameObject> ();
	[Tooltip ("亂數陣列")]
	public static List<GameObject> UFO_Random = new List<GameObject> ();

	//測試用,UFO_Random設為static無法顯示在inspector
	public  List<GameObject> UFO_Random_Clone = new List<GameObject> ();

	///點擊陣列
	public static List<GameObject> UFO = new List<GameObject> ();

	///二維座標陣列
	public static  List<List<Vector3>> arrangement = new List<List<Vector3>> ();

	///遊戲開始
	public static bool ready = false;

	///UFO移動至下方X軸之位置
	public static int ReferencePoint = -500;

	///UFO間隔
	public static int interval = 90;

	///ShowLightTiggle開關
	bool toggle = true;

	///作為鎖定當前數值的功能
	bool UFO_InstantiateAndDestory_toggle = false;

	///鎖定值
	int diff;

	/// UFO亮燈指標
	int UFO_Arr = 0;

	/// UFO Level指標
	int UFO_Level = 0;

	/// UFO Load 載入指標
	int UFO_Load = 0;


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

				switch (j++) {
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
					
				obj.GetComponent<Level1_Listener> ().op = false;

			} else {
				//指向上一個位置
				ReferencePoint -= interval;
				obj.GetComponent<Level1_Listener> ().op = true;

				switch (--j) { 
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
		makeRandomArr ((Level1_Select._arrangement_index > 3) ? 4 : 3);

		//Listen所有UFO
		UFO_Random.ForEach (i => UIEventListener.Get (i).onClick = new ClickEvent ().Action);

		//持續比對答案
		StartCoroutine (AnswerCompare ());

		//動態判斷新增或刪除UFO
		StartCoroutine (UFO_InstantiateAndDestory ());


		UFO_Random_Clone.AddRange (UFO_Random);//測試用
	}



	void Update ()
	{

	}

	///設定初始值
	public virtual void Initialization ()
	{
		//取得Panel物件
		Panel = GameObject.Find ("Panel");

		//取得UFO_group物件
		UFO_group = GameObject.Find ("UFO_group");

		//取得Bingo物件
		Bingo = Resources.Load<GameObject> ("JT/" + "checked-Bingo");

		//取得Error物件
		Error = Resources.Load<GameObject> ("JT/" + "checked-Error");

		//取得ufo0物件
		UFO_DefaultLight = Resources.Load<Texture> ("JT/UFO_light/ufo0");

		//取得Eufo0_red物件
		UFO_RedLight = Resources.Load<Texture> ("JT/UFO_light/ufo0_red");

		//UFO排列圖座標
		UFO_position ();

		//位移指標
		int i = 0;

		//走訪 JT/UFO_group的所有prefab
		foreach (GameObject arr in Resources.LoadAll("JT/UFO_group")) {

			//動態新增UFO(初始)
			UFO_Instantiate (arr, i++, "init");

			//判斷UFO數量
			if (Level1_Select._arrangement_index >= 4) {
				if (i == 4) {
					UFO_Load = i;
					break;
				}
			} else {
				if (i == 3) {
					UFO_Load = i;
					break;
				}
			}
		}
	}

	///製作RandomArr陣列
	///將 UFO_sequence亂數後放進 UFO_Random
	protected void makeRandomArr (int key)
	{
		//設定亂數種子
		UnityEngine.Random.InitState (System.Guid.NewGuid ().GetHashCode ());

		for (int i = 0; i < key; i++) {
			int num = UnityEngine.Random.Range (0, UFO_sequence.Count);
			UFO_Random.Add (UFO_sequence [num]);
			UFO_sequence [num] = UFO_sequence [UFO_sequence.Count - 1];
			UFO_sequence.RemoveAt (UFO_sequence.Count - 1);
		}
	}

	//比對答案
	protected IEnumerator AnswerCompare ()
	{
		while (true) {
			//UFO亮燈
			if (ready) {
				StartCoroutine (ShowLight_TimeManagement (0f, Level1_Select._floatFieldLeft [UFO_Level], Level1_Select._floatFieldRight [UFO_Level], UFO_Random.Count));
				ready = !ready;
			} else {
				//UFO數量到達時判斷
				if (UFO.Count == UFO_Random.Count) {
					//比對陣列是否一致  答對=>clone = Bingo,反之,答對=>clone = Error
					GameObject clone = (UFO.SequenceEqual (UFO_Random)) ? NGUITools.AddChild (Panel, Bingo) : NGUITools.AddChild (Panel, Error);

					//設定大小
					clone.transform.localScale = new Vector3 (140, 140, 0);

					//設定大小tween
					iTween.ScaleFrom (clone, iTween.Hash ("scale", Vector3.zero, "delay", 0.2, "oncomplete", "DestroyClone", "oncompletetarget", gameObject, "oncompleteparams", clone));
//					timer.pause ();

					//1秒後調用restart()
					Invoke ("restart", 1f);
                     
					break;//跳出while迴圈
				}
			}
			yield return null;//等待下一幀
		}
	}

	//持續判斷新增或刪除UFO
	protected IEnumerator UFO_InstantiateAndDestory ()
	{
		//開啟
		UFO_InstantiateAndDestory_toggle = true;

		while (true) {

			//給值後鎖定
			if (UFO_InstantiateAndDestory_toggle) {
				diff = Level1_Select._arrangement_index;
				//鎖定
				UFO_InstantiateAndDestory_toggle = false;
			}
			if (Level1_Select._arrangement_index > 3 && UFO_Random.Count < 4) {
				foreach (GameObject arr in Resources.LoadAll("JT/UFO_group")) {
					
					//陣列中不含有物件時執行
					if (!UFO_Random.Find (go => go.name == arr.name + "(Clone)")) {

						//動態新增UFO(初始後)
						UFO_Instantiate (arr, UFO_Load, "add");

					}
				}

				UFO_Random_Clone.Clear ();//測試用

				UFO_Random_Clone.AddRange (UFO_Random);//測試用

			} else if (Level1_Select._arrangement_index < 4) {
				//持續判斷
				while (GameObject.Find ("UFO-3(Clone)")) {
					Destroy (GameObject.Find ("UFO-3(Clone)"));
					UFO_Random.RemoveAt (3);
					yield return null;//等待下一幀
				}
			}

			//位移指標
			int i = 0;

			if (diff - Level1_Select._arrangement_index < 0) {
				foreach (GameObject arr in UFO_Random) {
					UFO_StartMove (arr, "up", i++);
				}

				UFO_InstantiateAndDestory_toggle = true;

			} else if (diff - Level1_Select._arrangement_index > 0) {
				foreach (GameObject arr in UFO_Random) {
					UFO_StartMove (arr, "down", i++);

				}
				UFO_InstantiateAndDestory_toggle = true;
			}
			yield return null;//等待下一幀
		}
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
					UFO_Random [UFO_Arr].GetComponent<UITexture> ().mainTexture = UFO_RedLight;

				//toggle = false
				toggle = !toggle;

			} else {
				if (UFO_Arr == UFO_Random.Count - 1) {

					//開啟Collider
					OpenBoxCollider ();

					//關閉最後一個燈
					UFO_Random [UFO_Arr].GetComponent<UITexture> ().mainTexture = UFO_DefaultLight;
				} else {
					//關閉上一個燈
					UFO_Random [UFO_Arr].GetComponent<UITexture> ().mainTexture = UFO_DefaultLight;
				
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



	///使前後亂數不同
	protected virtual void MakeDifference ()
	{
		//複製一份比對參考
		List<GameObject> check = UFO_Random.ToList ();

		//執行到與舊陣列值不同時
		while(UFO_Random.SequenceEqual (check)){
			
			// This will copy all the items from Ball_Random to Ball_sequence
			UFO_sequence.AddRange (UFO_Random);

			//清空UFO_Random陣列
			UFO_Random.Clear ();

			//製作RandomArr陣列
			makeRandomArr ((Level1_Select._arrangement_index > 3) ? 4 : 3);
		}
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

		//清空UFO陣列
		UFO.Clear ();

		//使前後亂數不同
		MakeDifference ();

		//重置方X軸之位置
		ReferencePoint = -500;

		//亮燈間格時間變換與重置
		UFO_Level = (UFO_Level < Level1_Select._item - 1) ? UFO_Level += 1 : 0;

		//UFO亮燈
		StartCoroutine (ShowLight_TimeManagement (1f, Level1_Select._floatFieldLeft [UFO_Level], Level1_Select._floatFieldRight [UFO_Level], UFO_Random.Count));

		//重置ShowLightTiggle開關
		toggle = true;

		//重置BoxCollider開關
		ClickEvent.j = -1;

		//持續比對答案
		StartCoroutine (AnswerCompare ());
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

	//動態新增UFO
	protected int UFO_Instantiate (GameObject arr, int key, string str)
	{
		//實例化prefab物件
		GameObject go = Instantiate (arr)as GameObject;

		//設定父節點
		go.transform.parent = UFO_group.transform;

		//設定座標為Vector3(0,0,0)
		go.transform.localPosition = Vector3.zero;

		//設定大小為Vector3(1,1,1)
		go.transform.localScale = Vector3.one;

		//加入UFO_sequence陣列
		if (str == "init")
			UFO_sequence.Add (go);
		//加入UFO_Random陣列
		else if (str == "add") {
			UFO_Random.Add (go);
			go.GetComponent<TweenPosition> ().duration = 0.5f;
			go.GetComponent<TweenPosition> ().delay = 0f;
		}

		//修改NGUI數值
		go.GetComponent<TweenPosition> ().from = Vector3.zero;

		try {
			go.GetComponent<TweenPosition> ().to = arrangement [Level1_Select._arrangement_index] [key];
		} catch (Exception e) {
		}
		return key;
	}

	//UFO tween start
	protected void UFO_StartMove (GameObject arr, string str, int i)
	{
		if (str == "up") {
			//修改NGUI數值
			try {
				arr.GetComponent<TweenPosition> ().from = arrangement [diff] [i];
			} catch (Exception e) {
			}

			arr.GetComponent<TweenPosition> ().to = arrangement [Level1_Select._arrangement_index] [i];

			//Reverse觸發模式
			arr.GetComponent<UIPlayTween> ().playDirection = Direction.Forward;
		} else if (str == "down") {
			//修改NGUI數值
			try {
				arr.GetComponent<TweenPosition> ().from = arrangement [Level1_Select._arrangement_index] [i];
			} catch (Exception e) {
			}
			arr.GetComponent<TweenPosition> ().to = arrangement [diff] [i];

			//Forward觸發模式
			arr.GetComponent<UIPlayTween> ().playDirection = Direction.Reverse;
		}

		//防止切換圖時UFO大小跟著改變,所以設定為不同群組
		arr.GetComponent<TweenScale> ().tweenGroup = 1;

		//執行UIPlayTween
		arr.GetComponent<UIPlayTween> ().Play (true);

		//設定為原本群組
		arr.GetComponent<TweenScale> ().tweenGroup = 0;

		//改回Toggle觸發模式
		arr.GetComponent<UIPlayTween> ().playDirection = Direction.Toggle;
	}

	//UFO排列圖座標陣列
	void UFO_position ()
	{
		//圖1,圖2,圖3,圖4
		arrangement.Add (new List<Vector3> ());
		arrangement [0].Add (new Vector3 (370, 0, 0)); 
		arrangement [0].Add (new Vector3 (0, 0, 0));
		arrangement [0].Add (new Vector3 (-370, 0, 0));

		//圖5
		arrangement.Add (new List<Vector3> ());
		arrangement [1].Add (new Vector3 (0, 280, 0)); 
		arrangement [1].Add (new Vector3 (0, 0, 0));
		arrangement [1].Add (new Vector3 (-0, -280, 0));

		//圖6
		arrangement.Add (new List<Vector3> ());
		arrangement [2].Add (new Vector3 (173, -100, 0)); 
		arrangement [2].Add (new Vector3 (0, 200, 0));
		arrangement [2].Add (new Vector3 (-173, -100, 0));

		//圖7
		arrangement.Add (new List<Vector3> ());
		arrangement [3].Add (new Vector3 (173, 100, 0)); 
		arrangement [3].Add (new Vector3 (0, -200, 0));
		arrangement [3].Add (new Vector3 (-173, 100, 0));

		//圖8
		arrangement.Add (new List<Vector3> ());
		arrangement [4].Add (new Vector3 (285, -90, 0)); 
		arrangement [4].Add (new Vector3 (85, 100, 0));
		arrangement [4].Add (new Vector3 (-115, -90, 0));
		arrangement [4].Add (new Vector3 (-315, 100, 0));

		//圖9
		arrangement.Add (new List<Vector3> ());
		arrangement [5].Add (new Vector3 (285, 100, 0)); 
		arrangement [5].Add (new Vector3 (85, -90, 0));
		arrangement [5].Add (new Vector3 (-115, 100, 0));
		arrangement [5].Add (new Vector3 (-315, -90, 0));

		//圖10
		arrangement.Add (new List<Vector3> ());
		arrangement [6].Add (new Vector3 (230, 100, 0)); 
		arrangement [6].Add (new Vector3 (-230, 100, 0));
		arrangement [6].Add (new Vector3 (230, -140, 0));
		arrangement [6].Add (new Vector3 (-230, -140, 0));

		//圖11
		arrangement.Add (new List<Vector3> ());
		arrangement [7].Add (new Vector3 (320, 0, 0)); 
		arrangement [7].Add (new Vector3 (110, 0, 0));
		arrangement [7].Add (new Vector3 (-110, 0, 0));
		arrangement [7].Add (new Vector3 (-320, 0, 0));

	}

}
