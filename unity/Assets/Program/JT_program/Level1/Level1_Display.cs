using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Level1_Display : MonoBehaviour
{

	[SerializeField]private GameObject Panel;
	[SerializeField]private GameObject prefab;

	//設定UFO
	[SerializeField]private GameObject UFO_0;
	[SerializeField]private GameObject UFO_1;
	[SerializeField]private GameObject UFO_2;

	//設定Bingo,Error圖
	[SerializeField]private GameObject Bingo;
	[SerializeField]private GameObject Error;

	//Timer物件
	[SerializeField]private Level1_Timer timer;

	///UFO移動至下方X軸之位置
	public static int ReferencePoint = -200;

	///UFO間隔
	public static int interval = 200;

	///UFO發光時間
	private float time = -5;

	///點擊UFO陣列，依序放入點擊UFO
	public static List<GameObject> UFO = new List<GameObject> ();

	///亂數UFO陣列，與點擊UFO比對
	public static List<GameObject> RandomUFO = new List<GameObject> ();


	///比對開關
	bool compare = true;

	//亂數元素
	int[] sequence = new int[3];
	int[] RandomArr = new int[3];


	//點擊事件
	public class ClickEvent
	{
		public static int j = -1;
		//BoxCollider開關
		public void Action (GameObject obj)
		{
			Debug.Log (obj.name);

			if (obj.GetComponent<Level1_Listener> ().op) {

				obj.GetComponent<TweenPosition> ().from = new Vector3 (ReferencePoint, -316, 0);
				obj.GetComponent<TweenPosition> ().to = obj.transform.localPosition;
				ReferencePoint += interval;
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
				}

				j++;
				obj.GetComponent<Level1_Listener> ().op = false;

			} else {
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
				}
				UFO.Remove (obj);
			}
		}
	}

	void Start ()
	{
		//NGUITools.AddChild (Panel,prefab);

		//製作亂數
		makeRandomArr ();

		//Listener每個UFO
		UIEventListener.Get (UFO_0).onClick = new ClickEvent ().Action;
		UIEventListener.Get (UFO_1).onClick = new ClickEvent ().Action;
		UIEventListener.Get (UFO_2).onClick = new ClickEvent ().Action;
//      UIEventListener.Get (GameObject.Find ("UFO-0")).onClick = new Display.ClickEvent ().Action;
	}

	public enum UFO_redlight : int
	{
		ufo0 = 0,
		ufo1 = 1,
		ufo2 = 2,
		ufo3 = 3,
		ufo4 = 4,
		ufo0_red = 5,
		ufo1_red = 6,
		ufo2_red = 7,
		ufo3_red = 8,
		ufo4_red = 9,
	}

	bool LightToggle = true;

	void LightToggleOp ()
	{
		LightToggle = true;
	}

	void UFOLight (int i)
	{
		if (LightToggle) {
			switch (i) {
			case 1:
				GameObject.Find ("UFO-" + RandomArr [0]).GetComponent<UITexture> ().mainTexture = Resources.Load<Texture> ("JT/UFO_light/" + (UFO_redlight)5);
				GameObject.Find ("ProgressBar").GetComponent<Level1_Timer> ().enabled = true;
				RandomUFO.Add (GameObject.Find ("UFO-" + RandomArr [0]));
				LightToggle = false;
				Invoke ("LightToggleOp", 1f);
				timer.TimeReset ();
				break;
			case 2:
				GameObject.Find ("UFO-" + RandomArr [1]).GetComponent<UITexture> ().mainTexture = Resources.Load<Texture> ("JT/UFO_light/" + (UFO_redlight)5);
				GameObject.Find ("UFO-" + RandomArr [0]).GetComponent<UITexture> ().mainTexture = Resources.Load<Texture> ("JT/UFO_light/" + (UFO_redlight)0);
				RandomUFO.Add (GameObject.Find ("UFO-" + RandomArr [1]));
				LightToggle = false;
				Invoke ("LightToggleOp", 1f);
				break;
			case 3:
				GameObject.Find ("UFO-" + RandomArr [2]).GetComponent<UITexture> ().mainTexture = Resources.Load<Texture> ("JT/UFO_light/" + (UFO_redlight)5);
				GameObject.Find ("UFO-" + RandomArr [1]).GetComponent<UITexture> ().mainTexture = Resources.Load<Texture> ("JT/UFO_light/" + (UFO_redlight)0);
				RandomUFO.Add (GameObject.Find ("UFO-" + RandomArr [2]));
				LightToggle = false;
				Invoke ("LightToggleOp", 1f);
				break;
			case 4:
				GameObject.Find ("UFO-" + RandomArr [2]).GetComponent<UITexture> ().mainTexture = Resources.Load<Texture> ("JT/UFO_light/" + (UFO_redlight)0);
				OpenBoxCollider ();
				break;
			}
		}
	}


	void Update ()
	{

		//UFO發光
		time += Time.deltaTime;
		UFOLight (Mathf.CeilToInt (time));

		if (UFO.Count == 3 && compare) 
		{
			if (UFO.SequenceEqual (RandomUFO))
			{
				//答對
				GameObject clone = NGUITools.AddChild (Panel, Bingo);
				clone.transform.localScale = new Vector3 (140, 140, 0);
				iTween.ScaleFrom (clone, iTween.Hash ("scale", Vector3.zero, "delay", 0.2, "oncomplete", "DestroyClone", "oncompletetarget", gameObject, "oncompleteparams", clone));
				timer.pause ();
			} else
			
			{
				//答錯
				GameObject clone = NGUITools.AddChild (Panel, Error);
				clone.transform.localScale = new Vector3 (140, 140, 0);
				iTween.ScaleFrom (clone, iTween.Hash ("scale", Vector3.zero, "delay", 0.2, "oncomplete", "DestroyClone", "oncompletetarget", gameObject, "oncompleteparams", clone));
				timer.pause ();
			}
			compare = false;
			Invoke ("restart", 1f);
		}
	}




	//設定Tween數值
	public void OpenBoxCollider ()
	{
		GameObject[] target;
		target = GameObject.FindGameObjectsWithTag ("UFO");
		for (int i = 0; i <= 2; i++) {
			target [i].GetComponent<BoxCollider> ().enabled = true;
			target [i].GetComponent<TweenPosition> ().duration = 0.5f;
			target [i].GetComponent<TweenPosition> ().delay = 0f;
		}
	}

	//製作RandomArr陣列
	void makeRandomArr ()
	{
		for (int i = 0; i < RandomArr.Length; i++)
		{
			sequence [i] = i;
		}

		int end = RandomArr.Length - 1;

		for (int i = 0; i < RandomArr.Length; i++) 
		{
			int num = Random.Range (0, end + 1);
			RandomArr [i] = sequence [num];
			sequence [num] = sequence [end];
			end--;
		}
	}
		
	//顯示RandomArr陣列
	public void ShowRandom ()
	{
		foreach (int Arr in RandomArr)
			Debug.Log (Arr);
	}

	//顯示點擊UFO陣列
	public void ShowClickUFO ()
	{
		foreach (GameObject obj in UFO)
			Debug.Log (obj.name);
	}

	//顯示亂數UFO陣列
	public void ShowRandomUFO ()
	{
		foreach (GameObject obj in RandomUFO)
			Debug.Log (obj.name);
	}


	void DestroyClone (GameObject obj)
	{
		Destroy (obj);
		timer.rezero ();
	}


	public void restart ()
	{
		//重製UFO發光時間
		time = -2;

		//使前後亂數不同
		int[] check = (int[])RandomArr.Clone ();
		makeRandomArr ();
		if (RandomArr.SequenceEqual (check))
			makeRandomArr ();

		//重製UFO位置
		for (int i = 2; i >= 0; i--) {
			UFO [i].GetComponent<UIPlayTween> ().Play (true);
			UFO [i].GetComponent<Level1_Listener> ().op = true;
			UFO [i].GetComponent<BoxCollider> ().enabled = false;
		}

		//重製方X軸之位置
		ReferencePoint = -200;

		//清空點擊UFO陣列
		UFO.Clear ();

		//清空亂數UFO陣列
		RandomUFO.Clear ();


		//重製BoxCollider開關
		ClickEvent.j = -1;
		 
		//判斷RandomUFO與UFO 開關
		compare = true;
	
	}
}
