using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using System.Threading;
using System.Reflection;

//清除監控的Error,可刪除

public class Level1_Display_Normal : MonoBehaviour
{
	[Header ("Object")]
	[Tooltip ("UIRoot-Panel")]
	[SerializeField]private GameObject Panel;
	[Tooltip ("BingoIcon")]
	[SerializeField]private GameObject Bingo;
	[Tooltip ("ErrorIcon")]
	[SerializeField]private GameObject Error;
	[Tooltip ("UFO Default Light")]
	[SerializeField]private string UFO_DefaultLight;
	[Tooltip ("UFO Red Light")]
	[SerializeField]private string UFO_RedLight;

	[Space (10)]

	///亂數序列
	public static List<GameObject> UFO_sequence = new List<GameObject> ();
	///亂數陣列
	public static List<GameObject> UFO_Random = new List<GameObject> ();
	///點擊陣列
	public static List<GameObject> UFO = new List<GameObject> ();
	///二維座標陣列
	public static  List<List<Vector3>> arrangement = new List<List<Vector3>> ();
	///UFO資訊陣列
	public static List<CreatUFO> UFOList = new List<CreatUFO> ();
	///UFO物件父節點
	public static GameObject UFO_group;
	///UFO移動至下方X軸之位置
	public static int s_iReferencePoint = -500;
	///UFO間隔
	public static int s_iInterval = 90;
	///下一關圖型
	public static bool s_bNextLevel = false;
	///關卡難度提升
	private bool LevelUP = false;

	//UFO隨機樣式
	private int g_iRandom;
	//UFO數量平衡
	private int g_iBalance;
	///暫存值
	private int g_iTempValue;
	///載入UFO
	private GameObject[] LoadUFO;


	int g_arrangementFlag = 32;

	private GameObject SliderBar;


	//GameObject BackGround;


	List<Vector3> Point = new List<Vector3> ();


	Vector3 CheckedScale;


	WaitUntil WaitValueChange;

	WaitUntil WaitUFOReady;

	WaitForSeconds LightTime;

	WaitForSeconds DarkTime;

	WaitForSeconds WaitOneSecond;


	///UFO生成座標
	public static Vector3[] GeneratePoint = new Vector3[] {
		new Vector3 (800, 500),
		new Vector3 (800, -500),
		new Vector3 (-800, -500),
		new Vector3 (-800, 500)
	};


	public class CreatUFO
	{
		//走訪旗標
		public static int s_iPos = 0;

		//物件
		public GameObject getUFO { get; private set; }

		//點擊移動開關 default = false
		public bool m_bToggle{ get; set; }

		//是否在移動 default = false
		public bool isMoving{ get; set; }

		//點擊事件
		public void OnClick (GameObject obj)
		{
			Debug.Log (obj.name + m_bToggle);

			isMoving = true;

			if (m_bToggle) {
				//移動到下方
				TweenPosition.Begin (obj, 0.5f, new Vector3 (s_iReferencePoint, -316, 0));
				//指向下一個位置
				s_iReferencePoint += s_iInterval;
				//加入UFO陣列
				UFO.Add (obj);

				for (int i = 0; i <= UFO.Count - 1; i++) {
					try {
						UFO [i - 1].GetComponent<BoxCollider> ().enabled = false;//設計當點下最後一個UFO時全關閉BoxCollider之後再統一開啟
					} catch (Exception e) {
					}	
				}

				m_bToggle = !m_bToggle;

			} else {
				//指向上一個位置
				s_iReferencePoint -= s_iInterval;

				m_bToggle = !m_bToggle;

				for (int i = 0; i <= UFO.Count - 1; i++) {
					try {
						UFO [i - 1].GetComponent<BoxCollider> ().enabled = true;
					} catch (Exception e) {
					}	
				}
				//從UFO陣列移除
				UFO.Remove (obj);
			}
		}


		//建構子
		public CreatUFO (GameObject go)
		{
			m_bToggle = true;

			isMoving = true;

			//實例化物件並設定父節點
			go = Instantiate (go, parent: UFO_group.transform) as GameObject;

			//設定實例化位置
			go.transform.localPosition = GeneratePoint [s_iPos % 4];

			//設定大小為Vector3(1,1,1)
			go.transform.localScale = Vector3.one;

			//移動到當前關卡座標
			TweenPosition.Begin (go, 1f, arrangement [Level1_Select._arrangement_index] [s_iPos++]);

			//加入UFO_sequence陣列
			UFO_sequence.Add (go);

			this.getUFO = go;

			//Listen
			UIEventListener.Get (go).onClick = OnClick;

			//設定onFinished Event   "oneshot = false"
			EventDelegate.Add (go.GetComponent<TweenPosition> ().onFinished, () => {
				isMoving = false;
				go.GetComponent<TweenPosition> ().duration = 0.5f;
				go.GetComponent<TweenPosition> ().delay = 0f;
			});
		}
	}

	void Start ()
	{
		//初始化
		Initialization ();

		//遊戲開始
		StartCoroutine (GameStart ());

		ClearLog ();//清除監控的Error,可刪除

	}

	void Update ()
	{

	}

	///初始化
	void Initialization ()
	{
		//取得Panel物件
		Panel = GameObject.Find ("Panel");

		//取得UFO_group物件
		UFO_group = GameObject.Find ("UFO_group");

		//取得Control - Colored Slider物件
		SliderBar = GameObject.Find ("Control - Colored Slider");

		//BackGround = GameObject.Find ("BackGround");

		//取得Bingo物件
		Bingo = Resources.Load<GameObject> ("JT/" + "checked-Bingo");

		//取得Error物件
		Error = Resources.Load<GameObject> ("JT/" + "checked-Error");

		//載入UFO
		LoadUFO = Resources.LoadAll<GameObject> ("JT/UFO_groupAll");


		WaitValueChange = new WaitUntil (() => g_iTempValue != Level1_Select._arrangement_index);

		WaitUFOReady = new WaitUntil (() => UFOList.All (list => !(list.isMoving)));

		LightTime = new WaitForSeconds (0.1f);

		DarkTime = new WaitForSeconds (0.1f);

		WaitOneSecond = new WaitForSeconds (1f);

		CheckedScale = new Vector3 (140, 140, 0);

	}

	//清除監控的Error,可刪除
	public void ClearLog ()
	{
		var assembly = Assembly.GetAssembly (typeof(UnityEditor.ActiveEditorTracker));
		var type = assembly.GetType ("UnityEditorInternal.LogEntries");
		var method = type.GetMethod ("Clear");
		method.Invoke (new object (), null);
	}

	IEnumerator GameStart ()
	{

		//UFO排列圖座標陣列
		UFO_position ();

		while (true) {
			//關卡設定
			yield return StartCoroutine (LevelManagement ());

			//隨機亂數
			yield return StartCoroutine (MakeRandom (4));

			//亮燈
			yield return StartCoroutine (ShowLight ());

			//答案比對
			yield return StartCoroutine (AnswerCompare ());

			//重置
			yield return StartCoroutine (Reset ());

			//等待下一幀
			yield return null;
		}
	}

	///關卡設定
	IEnumerator LevelManagement ()
	{

		DisplaySliderBar ();

		if (!LevelUP) {

			//進入遊戲後初始，只執行一次
			if (UFOList.Count == 0) {
				
				//設定亂數種子
				UnityEngine.Random.InitState (System.Guid.NewGuid ().GetHashCode ());

				//隨機值
				g_iRandom = UnityEngine.Random.Range (0, 5);

				//走訪座標 & 實例化UFO
				arrangement [Level1_Select._arrangement_index].ForEach (go => UFOList.Add (new CreatUFO (LoadUFO [g_iRandom])));

				//取得當前UFO原始圖名稱
				UFO_DefaultLight = UFO_sequence[0].GetComponent<UISprite> ().spriteName;

				//取得當前UFO的亮燈圖名稱
				UFO_RedLight = UFO_DefaultLight + "_red";
			}

			//當答錯時執行
			if (UFO.Any ()) {
				
				UFO.ForEach (go1 => {
					
					//UFO歸位
					go1.GetComponent<UIPlayTween> ().Play (true);

					//isMoving = true
					(UFOList.Find (go2 => go2.getUFO.Equals (go1))).isMoving = true;
				});
			}
		} else {
			
			//暫存值
			g_iTempValue = Level1_Select._arrangement_index;

			//下一關圖形
			s_bNextLevel = true;

			//值改變時執行
			yield return WaitValueChange;

			//缺(多)幾台UFO
			g_iBalance = arrangement [g_iTempValue].Count - arrangement [Level1_Select._arrangement_index].Count;

			//lack
			if (g_iBalance < 0) {

				//少幾台UFO
				for (int j = 0; j < Math.Abs (g_iBalance); j++) {
					UFOList.Add (new CreatUFO (LoadUFO [g_iRandom]));
				}

				//extra
			} else if (g_iBalance > 0) {

				//Destroy到第幾台
				int iDestroyFlagCount = 0;

				//Destroy幾台
				var iDestroyFlag = arrangement [g_iTempValue].Count - arrangement [Level1_Select._arrangement_index].Count;
	
				//UFO_sequence差集UFO
				UFO_sequence.Except (UFO).ToList ().ForEach (go1 => {

					if ((iDestroyFlagCount++ < iDestroyFlag)) {
	
						//移回生成座標
						TweenPosition.Begin (go1, 1f, GeneratePoint [g_iBalance++ % 4]);

						Destroy (go1, 1.5f);

						//刪除在UFOList中同樣的資料
						UFOList.Remove (UFOList.Find (go2 => go2.getUFO.Equals (go1)));

						//在UFO_sequence刪除此物件
						UFO_sequence.Remove (go1);
					}
				});
				CreatUFO.s_iPos = 0;//歸零
			}

			//走訪旗標
			int iFlag = 0;
	
			UFO.ForEach (go1 => { 
				//設定下個座標
				TweenPosition.Begin (go1, 0.5f, arrangement [Level1_Select._arrangement_index] [iFlag++]);

				//開始移動
				go1.GetComponent<UIPlayTween> ().Play (true);

				//isMoving = true
				(UFOList.Find (go2 => go2.getUFO.Equals (go1))).isMoving = true;
			});

			if (UFO_sequence.Except (UFO).Any ()) {
				//設定下個座標
				UFO_sequence.Except (UFO).ToList ().ForEach (go => TweenPosition.Begin (go, 0.5f, arrangement [Level1_Select._arrangement_index] [iFlag++]));
			}

			//重新給暫存值
			g_iTempValue = Level1_Select._arrangement_index;

			DisplaySliderBar ();
		}
	}
		
	//隨機亂數
	IEnumerator MakeRandom (int key)//key->需要幾個亂數
	{
		//清空
		UFO_Random.Clear ();

//		設定亂數種子
//		UnityEngine.Random.InitState (System.Guid.NewGuid ().GetHashCode ());

		for (int i = 0; i < key; i++) {
			int num = UnityEngine.Random.Range (0, UFO_sequence.Count);
			UFO_Random.Add (UFO_sequence [num]);
			UFO_sequence [num] = UFO_sequence [UFO_sequence.Count - 1];
			UFO_sequence.RemoveAt (UFO_sequence.Count - 1);
		}

		//將UFO_Random加回UFO_sequence
		UFO_sequence.AddRange (UFO_Random);

		yield return null;
	}

	IEnumerator ShowLight ()
	{
		//當陣列全部等於false時執行
		yield return WaitUFOReady;

		foreach (GameObject arr in UFO_Random) {

			arr.GetComponent<UISprite> ().spriteName = UFO_RedLight;

			yield return LightTime;

			arr.GetComponent<UISprite> ().spriteName = UFO_DefaultLight;

			//當走訪至最後時跳出迴圈
			if (arr == UFO_Random.Last ())
				break;

			yield return DarkTime;
		}
			
		//開啟點擊
		UFO_Random.ForEach (go => go.GetComponent<BoxCollider> ().enabled = true);

		//開啟點擊
		UFO_sequence.ForEach (go => go.GetComponent<BoxCollider> ().enabled = true);
	}

	IEnumerator AnswerCompare ()
	{
		//清空
		UFO.Clear ();

		while (true) {
			//UFO數量到達時判斷
			if (UFO.Count == UFO_Random.Count) {

				//關閉點擊
				UFO_sequence.ForEach (go => go.GetComponent<BoxCollider> ().enabled = false);

				//答對答錯
				if (UFO.SequenceEqual (UFO_Random))
					LevelUP = true;
				else
					LevelUP = false;

				//比對陣列是否一致  答對=>clone = Bingo,反之,答對=>clone = Error
				GameObject clone = (UFO.SequenceEqual (UFO_Random)) ? NGUITools.AddChild (Panel, Bingo) : NGUITools.AddChild (Panel, Error);

				//設定大小
				clone.transform.localScale = CheckedScale;

				//設定大小tween
				iTween.ScaleFrom (clone, iTween.Hash ("scale", Vector3.zero, "delay", 0.2));

				Destroy (clone, 1f);

				break;//跳出while迴圈
			}
			yield return null;//等待下一幀
		}
		yield return WaitOneSecond;
	}

	//重置
	IEnumerator Reset ()
	{
		//重置點擊移動開關
		UFOList.ForEach (go => go.m_bToggle = true);

		s_iReferencePoint = -500;

		yield return null;
	}


	void DisplaySliderBar ()
	{
		if (Level1_Select._arrangement_index >= 0) {
			if (Level1_Select._arrangement_index >= 0 && Level1_Select._arrangement_index <= 10)
				SliderBar.GetComponent<UISlider> ().value = 0.25f;
			if (Level1_Select._arrangement_index >= 11 && Level1_Select._arrangement_index <= 21)
				SliderBar.GetComponent<UISlider> ().value = 0.5f;
			if (Level1_Select._arrangement_index >= 22 && Level1_Select._arrangement_index <= 31)
				SliderBar.GetComponent<UISlider> ().value = 0.75f;
			if (Level1_Select._arrangement_index >= 32)
				SliderBar.GetComponent<UISlider> ().value = 1f;
		}
	}

	void RandomPos ()
	{
		UnityEngine.Random.InitState (System.Guid.NewGuid ().GetHashCode ());
		
		for (int x = -2; x <= 2; x++) {
			arrangement.Add (new List<Vector3> ());
			for (int y = -2; y <= 2; y++) {
				Point.Add (new Vector3 ((200f * x), (150f * y), 0));
			}
		}
			
		for (int i = 0; i < 10; i++) {
			arrangement.Add (new List<Vector3> ());
			for (int j = 0; j < 5; j++) {
				int RandomPos = UnityEngine.Random.Range (0, Point.Count);
				arrangement [g_arrangementFlag].Add (Point [RandomPos]); 
				Point [RandomPos] = Point [Point.Count - 1];
				Point.RemoveAt (Point.Count - 1);	
			}
			Point.AddRange (arrangement [g_arrangementFlag++]);
		}
	}

	//UFO排列圖座標陣列
	void UFO_position ()
	{
		//圖0
		arrangement.Add (new List<Vector3> ());
		arrangement [0].Add (new Vector3 (320, 0, 0)); 
		arrangement [0].Add (new Vector3 (110, 0, 0));
		arrangement [0].Add (new Vector3 (-110, 0, 0));
		arrangement [0].Add (new Vector3 (-320, 0, 0));

		//圖1
		arrangement.Add (new List<Vector3> ());
		arrangement [1].Add (new Vector3 (0, 300, 0)); 
		arrangement [1].Add (new Vector3 (0, 100, 0));
		arrangement [1].Add (new Vector3 (0, -100, 0));
		arrangement [1].Add (new Vector3 (0, -300, 0));

		//圖2
		arrangement.Add (new List<Vector3> ());
		arrangement [2].Add (new Vector3 (-300, 285, 200)); 
		arrangement [2].Add (new Vector3 (-100, 85, 200));
		arrangement [2].Add (new Vector3 (100, -115, 200));
		arrangement [2].Add (new Vector3 (300, -315, 200));

		//圖3
		arrangement.Add (new List<Vector3> ());
		arrangement [3].Add (new Vector3 (300, 285, 200)); 
		arrangement [3].Add (new Vector3 (100, 85, 200));
		arrangement [3].Add (new Vector3 (-100, -115, 200));
		arrangement [3].Add (new Vector3 (-300, -315, 200));

		//圖4
		arrangement.Add (new List<Vector3> ());
		arrangement [4].Add (new Vector3 (-487, 0, 100)); 
		arrangement [4].Add (new Vector3 (-287, 0, 100));
		arrangement [4].Add (new Vector3 (-87, 0, 100));
		arrangement [4].Add (new Vector3 (113, 0, 100));
		arrangement [4].Add (new Vector3 (313, 0, 100));
		arrangement [4].Add (new Vector3 (513, 0, 100));

		//圖5
		arrangement.Add (new List<Vector3> ());
		arrangement [5].Add (new Vector3 (0, 397, 200)); 
		arrangement [5].Add (new Vector3 (0, 235, 200));
		arrangement [5].Add (new Vector3 (0, 72, 200));
		arrangement [5].Add (new Vector3 (0, -87, 200));
		arrangement [5].Add (new Vector3 (0, -246, 200));
		arrangement [5].Add (new Vector3 (0, -406, 200));

		//圖6
		arrangement.Add (new List<Vector3> ());
		arrangement [6].Add (new Vector3 (-400, 200, 200)); 
		arrangement [6].Add (new Vector3 (-200, 102, 200));
		arrangement [6].Add (new Vector3 (0, 0, 200));
		arrangement [6].Add (new Vector3 (200, -98, 200));
		arrangement [6].Add (new Vector3 (400, -206, 200));
		arrangement [6].Add (new Vector3 (600, -298, 200));

		//圖7
		arrangement.Add (new List<Vector3> ());
		arrangement [7].Add (new Vector3 (500, 200, 200)); 
		arrangement [7].Add (new Vector3 (300, 102, 200));
		arrangement [7].Add (new Vector3 (100, 0, 200));
		arrangement [7].Add (new Vector3 (-100, -98, 200));
		arrangement [7].Add (new Vector3 (-300, -206, 200));
		arrangement [7].Add (new Vector3 (-500, -298, 200));

		//圖8
		arrangement.Add (new List<Vector3> ());
		arrangement [8].Add (new Vector3 (150, -150, 0)); 
		arrangement [8].Add (new Vector3 (-150, 150, 0));
		arrangement [8].Add (new Vector3 (150, 150, 0));
		arrangement [8].Add (new Vector3 (-150, -150, 0));

		//圖9
		arrangement.Add (new List<Vector3> ());
		arrangement [9].Add (new Vector3 (-125, 225, 100)); 
		arrangement [9].Add (new Vector3 (125, 225, 100));
		arrangement [9].Add (new Vector3 (-125, 0, 100));
		arrangement [9].Add (new Vector3 (125, 0, 100));
		arrangement [9].Add (new Vector3 (-125, -225, 100));
		arrangement [9].Add (new Vector3 (125, -225, 100));

		//圖10
		arrangement.Add (new List<Vector3> ());
		arrangement [10].Add (new Vector3 (-218, -100, 0));
		arrangement [10].Add (new Vector3 (-218, 100, 0)); 
		arrangement [10].Add (new Vector3 (218, 100, 0));
		arrangement [10].Add (new Vector3 (218, -100, 0));
		arrangement [10].Add (new Vector3 (0, -100, 0));
		arrangement [10].Add (new Vector3 (0, 100, 0));

		//圖11
		arrangement.Add (new List<Vector3> ());
		arrangement [11].Add (new Vector3 (285, -90, 0)); 
		arrangement [11].Add (new Vector3 (85, 100, 0));
		arrangement [11].Add (new Vector3 (-315, 100, 0));
		arrangement [11].Add (new Vector3 (-115, -90, 0));

		//圖12
		arrangement.Add (new List<Vector3> ());
		arrangement [12].Add (new Vector3 (-115, 100, 0));
		arrangement [12].Add (new Vector3 (-315, -90, 0));
		arrangement [12].Add (new Vector3 (85, -90, 0));
		arrangement [12].Add (new Vector3 (285, 100, 0)); 

		//圖13
		arrangement.Add (new List<Vector3> ());
		arrangement [13].Add (new Vector3 (100, -210, 0)); 
		arrangement [13].Add (new Vector3 (-100, -80, 0));
		arrangement [13].Add (new Vector3 (-100, 180, 0));
		arrangement [13].Add (new Vector3 (100, 50, 0));

		//圖14
		arrangement.Add (new List<Vector3> ());
		arrangement [14].Add (new Vector3 (100, -80, 0)); 
		arrangement [14].Add (new Vector3 (100, 180, 0));
		arrangement [14].Add (new Vector3 (-100, 50, 0));
		arrangement [14].Add (new Vector3 (-100, -210, 0));

		//圖15
		arrangement.Add (new List<Vector3> ());
		arrangement [15].Add (new Vector3 (-250, -147, 0)); 
		arrangement [15].Add (new Vector3 (-125, 103, 0));
		arrangement [15].Add (new Vector3 (125, 103, 0));
		arrangement [15].Add (new Vector3 (250, -147, 0));
		arrangement [15].Add (new Vector3 (0, -147, 0));

		//圖16
		arrangement.Add (new List<Vector3> ());
		arrangement [16].Add (new Vector3 (-250, 103, 0)); 
		arrangement [16].Add (new Vector3 (-125, -103, 0));
		arrangement [16].Add (new Vector3 (0, 103, 0));
		arrangement [16].Add (new Vector3 (125, -103, 0));
		arrangement [16].Add (new Vector3 (250, 103, 0));

		//圖17
		arrangement.Add (new List<Vector3> ());
		arrangement [17].Add (new Vector3 (-120, 100, 0)); 
		arrangement [17].Add (new Vector3 (120, 200, 0));
		arrangement [17].Add (new Vector3 (-120, -103, 0));
		arrangement [17].Add (new Vector3 (120, 0, 0));
		arrangement [17].Add (new Vector3 (120, -200, 0));

		//圖18
		arrangement.Add (new List<Vector3> ());
		arrangement [18].Add (new Vector3 (-120, 200, 0)); 
		arrangement [18].Add (new Vector3 (-120, 0, 0));
		arrangement [18].Add (new Vector3 (-120, -200, 0));
		arrangement [18].Add (new Vector3 (120, -103, 0));
		arrangement [18].Add (new Vector3 (120, 100, 0));

		//圖19
		arrangement.Add (new List<Vector3> ());
		arrangement [19].Add (new Vector3 (0, 240, 0)); 
		arrangement [19].Add (new Vector3 (-125, -10, 0));
		arrangement [19].Add (new Vector3 (125, -10, 0));
		arrangement [19].Add (new Vector3 (250, -260, 0));
		arrangement [19].Add (new Vector3 (0, -260, 0));
		arrangement [19].Add (new Vector3 (-250, -260, 0));

		//圖20
		arrangement.Add (new List<Vector3> ());
		arrangement [20].Add (new Vector3 (-250, 167, 0)); 
		arrangement [20].Add (new Vector3 (-130, -62, 0));
		arrangement [20].Add (new Vector3 (0, -280, 0));
		arrangement [20].Add (new Vector3 (110, -62, 0));
		arrangement [20].Add (new Vector3 (0, 167, 0));
		arrangement [20].Add (new Vector3 (250, 167, 0));

		//圖21
		arrangement.Add (new List<Vector3> ());
		arrangement [21].Add (new Vector3 (-200, 56, 0)); 
		arrangement [21].Add (new Vector3 (0, 194, 0));
		arrangement [21].Add (new Vector3 (200, 56, 0));
		arrangement [21].Add (new Vector3 (200, -144, 0));
		arrangement [21].Add (new Vector3 (0, -280, 0));
		arrangement [21].Add (new Vector3 (-200, -144, 0));

		//圖22
		arrangement.Add (new List<Vector3> ());
		arrangement [22].Add (new Vector3 (0, 210, 0)); 
		arrangement [22].Add (new Vector3 (0, 0, 250));
		arrangement [22].Add (new Vector3 (-170, -170, 0));
		arrangement [22].Add (new Vector3 (170, -170, 0));

		//圖23
		arrangement.Add (new List<Vector3> ());
		arrangement [23].Add (new Vector3 (0, 273, 0)); 
		arrangement [23].Add (new Vector3 (0, 175, 400));
		arrangement [23].Add (new Vector3 (0, 0, 800));
		arrangement [23].Add (new Vector3 (185, -98, 400));
		arrangement [23].Add (new Vector3 (-200, -98, 400));
		arrangement [23].Add (new Vector3 (-280, -223, 0));
		arrangement [23].Add (new Vector3 (271, -223, 0));

		//圖24
		arrangement.Add (new List<Vector3> ());
		arrangement [24].Add (new Vector3 (0, 302, 0)); 
		arrangement [24].Add (new Vector3 (0, 218, 400));
		arrangement [24].Add (new Vector3 (0, 0, 1000));
		arrangement [24].Add (new Vector3 (220, -182, 400));
		arrangement [24].Add (new Vector3 (-179, 37, 400));
		arrangement [24].Add (new Vector3 (180, 37, 400));
		arrangement [24].Add (new Vector3 (271, -223, 0));
		arrangement [24].Add (new Vector3 (-220, -182, 400));
		arrangement [24].Add (new Vector3 (-280, -223, 0));
		arrangement [24].Add (new Vector3 (0, -182, 400));

		//圖25
		arrangement.Add (new List<Vector3> ());
		arrangement [25].Add (new Vector3 (0, 302, 0)); 
		arrangement [25].Add (new Vector3 (0, 218, 400));
		arrangement [25].Add (new Vector3 (0, 0, 1000));
		arrangement [25].Add (new Vector3 (220, -182, 400));
		arrangement [25].Add (new Vector3 (-179, 37, 400));
		arrangement [25].Add (new Vector3 (180, 37, 400));
		arrangement [25].Add (new Vector3 (271, -223, 0));
		arrangement [25].Add (new Vector3 (-220, -182, 400));
		arrangement [25].Add (new Vector3 (-280, -223, 0));
		arrangement [25].Add (new Vector3 (0, -182, 400));
		arrangement [25].Add (new Vector3 (280, 75, 0));
		arrangement [25].Add (new Vector3 (-280, 75, 0));
		arrangement [25].Add (new Vector3 (0, -290, 0));

		//圖26
		arrangement.Add (new List<Vector3> ());
		arrangement [26].Add (new Vector3 (150, -150, 0)); 
		arrangement [26].Add (new Vector3 (-150, 150, 0));
		arrangement [26].Add (new Vector3 (150, 150, 0));
		arrangement [26].Add (new Vector3 (-150, -150, 0));
		arrangement [26].Add (new Vector3 (0, 0, 150));

		//圖27
		arrangement.Add (new List<Vector3> ());
		arrangement [27].Add (new Vector3 (150, -150, 200)); 
		arrangement [27].Add (new Vector3 (-150, 150, 200));
		arrangement [27].Add (new Vector3 (150, 150, 200));
		arrangement [27].Add (new Vector3 (-150, -150, 200));
		arrangement [27].Add (new Vector3 (265, -275, 0)); 
		arrangement [27].Add (new Vector3 (-253, 264, 0));
		arrangement [27].Add (new Vector3 (251, 264, 0));
		arrangement [27].Add (new Vector3 (-264, -275, 0));
		arrangement [27].Add (new Vector3 (0, 0, 600));

		//圖28
		arrangement.Add (new List<Vector3> ());
		arrangement [28].Add (new Vector3 (320, -250, 400)); 
		arrangement [28].Add (new Vector3 (-320, 250, 400));
		arrangement [28].Add (new Vector3 (320, 250, 400));
		arrangement [28].Add (new Vector3 (-320, -250, 400));
		arrangement [28].Add (new Vector3 (350, -275, 0)); 
		arrangement [28].Add (new Vector3 (-350, 278, 0));
		arrangement [28].Add (new Vector3 (350, 278, 0));
		arrangement [28].Add (new Vector3 (-350, -275, 0));
		arrangement [28].Add (new Vector3 (-200, 0, 500)); 
		arrangement [28].Add (new Vector3 (0, 150, 500));
		arrangement [28].Add (new Vector3 (200, 0, 400));
		arrangement [28].Add (new Vector3 (0, -150, 500));
		arrangement [28].Add (new Vector3 (0, 0, 900));

		//圖29
		arrangement.Add (new List<Vector3> ());
		arrangement [29].Add (new Vector3 (250, -200, 400)); 
		arrangement [29].Add (new Vector3 (-250, 200, 400));
		arrangement [29].Add (new Vector3 (250, 200, 400));
		arrangement [29].Add (new Vector3 (-250, -200, 400));
		arrangement [29].Add (new Vector3 (311, -275, 0)); 
		arrangement [29].Add (new Vector3 (-306, 278, 0));
		arrangement [29].Add (new Vector3 (311, 278, 0));
		arrangement [29].Add (new Vector3 (-306, -275, 0));
		arrangement [29].Add (new Vector3 (0, 278, 0)); 
		arrangement [29].Add (new Vector3 (311, 0, 0));
		arrangement [29].Add (new Vector3 (0, -275, 0));
		arrangement [29].Add (new Vector3 (-306, 0, 0));
		arrangement [29].Add (new Vector3 (-250, 0, 400)); 
		arrangement [29].Add (new Vector3 (0, 200, 400));
		arrangement [29].Add (new Vector3 (250, 0, 400));
		arrangement [29].Add (new Vector3 (0, -200, 400));
		arrangement [29].Add (new Vector3 (0, 0, 900));

		//圖30
		arrangement.Add (new List<Vector3> ());
		arrangement [30].Add (new Vector3 (-270, 273, 0));
		arrangement [30].Add (new Vector3 (0, 185, 300));
		arrangement [30].Add (new Vector3 (270, 273, 0));
		arrangement [30].Add (new Vector3 (185, -140, 300));
		arrangement [30].Add (new Vector3 (-270, -247, 0));
		arrangement [30].Add (new Vector3 (-185, -140, 300));
		arrangement [30].Add (new Vector3 (270, -247, 0));
		arrangement [30].Add (new Vector3 (0, 0, 600));

		//圖31
		arrangement.Add (new List<Vector3> ());
		arrangement [31].Add (new Vector3 (-270, 273, 0));
		arrangement [31].Add (new Vector3 (0, 185, 300));
		arrangement [31].Add (new Vector3 (270, 273, 0));
		arrangement [31].Add (new Vector3 (185, -140, 300));
		arrangement [31].Add (new Vector3 (-270, -247, 0));
		arrangement [31].Add (new Vector3 (-185, -140, 300));
		arrangement [31].Add (new Vector3 (270, -247, 0));
		arrangement [31].Add (new Vector3 (0, 0, 600));
		arrangement [31].Add (new Vector3 (0, 273, 0));
		arrangement [31].Add (new Vector3 (270, 0, 0));
		arrangement [31].Add (new Vector3 (0, -247, 0));
		arrangement [31].Add (new Vector3 (-270, 0, 0));
		arrangement [31].Add (new Vector3 (160, 60, 300));
		arrangement [31].Add (new Vector3 (0, -150, 300));
		arrangement [31].Add (new Vector3 (-160, 60, 300));

		////
		RandomPos ();


	}
}
