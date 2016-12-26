using UnityEngine;
using System.Collections;

public class test2 : MonoBehaviour {

	float time = 0;

//	test1 test;

	void Awake(){
		//Debug.Log (gameObject.GetComponent<test1> ().ok);
	}


	void Start () {
		
//		test = gameObject.AddComponent<test1> ();
//		gameObject.AddComponent<Rigidbody> ();
//		Debug.Log (gameObject.GetComponent<Rigidbody> ());
//		int x = gameObject.GetComponent<test1> ().ok;


//		test = gameObject.AddComponent<test1>();
//		int x  = gameObject.GetComponent<test1> ().ok;
//

//		run ();

//		test = gameObject.GetComponent<test1> ();

//		test.abc [0] = 1;
//		test.abc [1] = 2;
//		test.abc [2] = 3;
//
	}
	void Update(){
//		//Debug.Log (test.go ());
//		time += Time.deltaTime;
//		if (time > 3) {
////			Destroy (gameObject);
////			gameObject.GetComponent<Rigidbody>().useGravity = false;
//			Debug.Log ("testestset");
//		}
	}

	/// <summary>
	/// 跑跑跑跑
	/// </summary>
//	void run(){
//		
//	}
}
/*
using ProgressBar.Utils;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace ProgressBar
{
    /// <summary>
    /// This Script is directed at radially progressing designs.
    /// </summary>
    [RequireComponent(typeof(Image))]
    public class ProgressRadialBehaviour : MonoBehaviour
    {
        /// <summary>
        /// This script's Filler is its own Image component
        /// </summary>
        private Image m_Fill;

        /// <summary>
        /// Current value as a fraction of Filling.
        /// </summary>
        private float m_Value;

        /// <summary>
        /// This is the core of the Filler animation. If there is a difference between m_Value and TransitoryValue,
        /// the latter will play catch up in the Update Method.
        /// </summary>
        /// <remarks>
        /// Keep in mind that this means that you get the actual Filler value from the property Value only,
        /// If the animation is playing, TransitoryValue will be different until it catches up.
        /// </remarks>
		public float TransitoryValue {get;private set;}


        /// <summary>
        /// In pixels per seconds, the speed at which the Filler will be animated.
        /// </summary>
        public float ProgressSpeed;

        void Start()
        {
            m_Fill = GetComponent<Image>();
            m_Value = 1;//設定開關
            SetFillerSize(0);
        }

        void Update()
        {
			
            //If theses two values aren't equals this means m_Value has been updated and the animation needs to start.
            if (TransitoryValue != m_Value)
            {
                //The difference between the two values.
                float Dvalue = m_Value - TransitoryValue;


                //If the difference is positive:
                //  TransitoryValue needs to be incremented.
                if (Dvalue > 0)
				{
					TransitoryValue += (1f/show.maxvalue)* Time.deltaTime;
                    if (TransitoryValue >= m_Value)
                        TransitoryValue = m_Value;
                }
                //If the difference is negative:
                //  TransitoryValue needs to be decremented.
                else if (Dvalue < 0)
                {
					TransitoryValue -= (1f/show.maxvalue) * Time.deltaTime;
                    if (TransitoryValue <= m_Value)
                        TransitoryValue = m_Value;
                }

                //Clamping:
                //  If the TransitoryValue is now over the max value we set it to be equal to it.
                if (TransitoryValue >= 1)
                    TransitoryValue = 1;
                //  If the TransitoryValue is inferior to zero, we set it to zero
                else if (TransitoryValue < 0)
                    TransitoryValue = 0;

                //We set the Filler to be the new value
                //We don't pass the value as a percentage because we directly use SetFillerSize here which takes a fraction as a parameter.
                SetFillerSize(TransitoryValue);

            }
        }

        /// <summary>
        /// This method is used to set the Fill amount
        /// </summary>
        /// <param name="fill">new Fill amount as a fraction</param>
        public void SetFillerSize(float fill){
          m_Fill.fillAmount = fill;
        }

    }
}


		void GameObjectFind(){
//			time = GameObject.Find ("timelabel").GetComponent<GUIText> ();
			//level = GameObject.Find ("Level").GetComponent<GUIText> ();
		}



//原本UFO移動程式碼

using UnityEngine;
using System.Collections;
using System.Collections.Specialized;

//[ExecuteInEditMode]
public class click : MonoBehaviour {
	public static int interval;//UFO間距
	//public GameObject obj;//UFO遊戲物件
	public int nameID;//UFO的ID
    new string name;//UFO名稱
    bool move = false;//UFO移動開關
	public static bool checkmove = true;//UFO移動指標

	//UFO X,Y軸
	int posx=-1300;
	int posy=-600;
	//UFO縮小大小
	float UFOScale = 0.6f;
	//UFO間隔
	int intervalplus = 500;

	Vector2 current;


	void Start () {
		//取得當前UFO位置
		current = transform.position;
		interval=0;



	}
		
	void Update () {
		if (move) {    //UFO移動開關預設false
			MoveUFO ();
		}
	}

	//滑鼠點擊事件
	void OnMouseDown(){
		//if (checkmoveTorF ()) {
			//gameObject.SetActive(false);//點擊後消失
			name = gameObject.name;//紀錄點擊物件名稱
			show.orderlist (name, nameID);//傳送物件名稱、ID至orderlist
			//move = true;//開啟移動開關



			iTween.MoveTo(gameObject,iTween.Hash("position",new Vector3(-1200+interval,-500,0),"time",1));

			click.interval += intervalplus;
//			checkmove = false;
//
//			//StartCoroutine (wait ());
//			checkmove = true;

		//}
	}

///判斷一次移動一個UFO
// bool checkmoveTorF(){
//		if ((!move && !checkmove)||(move && checkmove)) {
//			checkmove = true;
//			return true;
//		}
//		else
//			return false;
	//}

	///移動+縮小UFO到下方
	//void MoveUFO(){
//		GameObject pos= GameObject.Find (gameObject.name);                                  //移動速度
//		current = Vector2.MoveTowards (current, new  Vector2(posx+click.interval,posy), 70f);
//		if (pos.transform.localScale.x > UFOScale && gameObject.transform.localScale.y > UFOScale) {
//				for(int i=0;i<=3;i++){pos.transform.localScale -= new Vector3 (0.01f, 0.01f, 0);}
//			}
//		if(pos.transform.position.y <= posy && pos.transform.localScale.x <= UFOScale){
//				move = false;
//			    checkmove = false;
//			}
//		pos.transform.position = current;
	//}
}

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class Bingo : MonoBehaviour {

	//設定GUIText
	private GUIText gui = new GUIText();
	private Vector3 pos;
	//GUIText移動函數時間軸
	private float timer; 
	public static int levelnumber=1;

	void Start () {
//		//GUIText函數時間週期
//		timer = 2f;
//		//取得GUIText位置
//		pos = transform.position;
//		gui = gameObject.GetComponent<GUIText> ();

	}
		

	void Update () {
//		Vector3 rPos = pos;
//
//		timer += (Time.deltaTime);
//
//		//讓GUIText從左到右跑
//		rPos.x = rPos.x + Mathf.Tan (timer) * 0.25f;
//
//		//顯示GUIText的文字
//		gui.text = "你答對了"+show.BingoCount+"題";
//
//	    //持續更新GUIText位置
//		transform.position = rPos;
//
//	    //GUIText由左往右跑後消失
//		if (timer>4.5) {             //好像不能加 == 
//			gameObject.SetActive(false);
//			if (show.maxvalue < 5) {
//				show.maxvalue++;
//				levelnumber++;
//			}
//			
//			SceneManager.LoadScene ("text");
//			Resources.UnloadUnusedAssets ();
//			click.clickopen = false;//前置倒數完成使滑鼠能點擊
//			//Destroy(gameObject);
//		}
	}
}
using UnityEngine;
using System.Collections;

public class show : MonoBehaviour
{
	public GameObject[] obj = new GameObject[10];//建立UFO遊戲物件
	public GameObject showbingo;//GUIText-Bingo
	public static int BingoCount;//點擊UFO正確數量
	public static int AnswerCount;//點擊UFO數量
	bool CheckTheSameTorF = true;//確認順序開關
	private float time;//每秒UFO亮燈計秒

	//製作介於minvalue到maxvalue之間的不重複亂數
	private static int minvalue = 1;//大於等於1 
	public static int maxvalue = 3 ;//小於6
	int [] sequence = new int[maxvalue];
	int [] RandomArr = new int[maxvalue];
	public static string[] order = new string[5];//點擊物件名稱順序表
	public static int[] orderID = new int[5];//點擊物件ID順序表
	private static int ordernumber;//物件順序表旗標


	Bingo bingo;


	[SerializeField] private Sprite Sprite;


//	GameObject love;

	void Start (){
		
		time = 1;
		BingoCount = 0;
		ordernumber = 0;
		AnswerCount = 0;
		makeRandomArr();
		prefab ();//hearts prefab



	for(int i=maxvalue;i<(obj.Length/2);i++) {
			obj [i].SetActive (false);
		}
	}

	void Update (){
		//當點擊數量到達UFO數量時，開始確認順序and關閉計時器
		if ((AnswerCount == maxvalue)&&CheckTheSameTorF) {
			CheckTheSame ();//確認點擊順序與隨機順序是否一致
			CheckTheSameTorF = false;
			ProgressBar.ProgressRadialBehaviour.TimeCount = false;//計時停止
		}
	}
		
	void FixedUpdate(){
		//速度7以上幾乎看不見
//		time += 3*Time.deltaTime;
//		time += Time.deltaTime;
//		//每秒顯示隨機UFO
//		switch ((int)time) {
//		case 1:
//			Debug.Log (RandomArr [0]);
//			obj [RandomArr[0]+4].SetActive (true);
//			break;
//		case 2:
//			Debug.Log (RandomArr [1]);
//			obj [RandomArr[1]+4].SetActive (true);
//			obj [RandomArr[0]+4].SetActive (false);
//			break;
//		case 3:
//			Debug.Log (RandomArr [2]);
//			obj [RandomArr[2]+4].SetActive (true);
//			obj [RandomArr[1]+4].SetActive (false);
//			break;
//		case 4:
//			if (maxvalue > 3) {
//				Debug.Log (RandomArr [3]);
//				obj [RandomArr [3] + 4].SetActive (true);
//				obj [RandomArr [2] + 4].SetActive (false);
//				break;
//			} else
//				goto default;
//		case 5:
//			if (maxvalue > 4) {
//				Debug.Log (RandomArr [4]);
//				obj [RandomArr [4] + 4].SetActive (true);
//				obj [RandomArr [3] + 4].SetActive (false);
//				break;
//			} else
//				goto default;
//		default:
//			for (int i = maxvalue; i < obj.Length; i++) {
//				//obj [i].SetActive (false);
//				Destroy (obj [i]);//Destroy全部UFO
//			}
//			break;
//		}

		GameObject.Find("New Sprite").GetComponent<SpriteRenderer> ().sprite = Sprite;
	






	}


	///製作隨機亂數
	void makeRandomArr(){
		for (int i = 0; i < RandomArr.Length; i++) {
			sequence [i] = i+1;
		}
		int end = RandomArr.Length-1;
		for (int i = 0; i < RandomArr.Length; i++) {
			int num = Random.Range (0, end+1);
			RandomArr[i] = sequence [num];
			sequence[num]=sequence[end];
			end--;
		}
	}

	///顯示隨機亂數
	public void ShowRandom (){
		foreach (int Arr in RandomArr) {
			Debug.Log (Arr);
		}
	}

    ///顯示所有隱藏圖UFO
	public void ShowAll (){
		for (int i = 0; i < maxvalue; i++) {
			obj [i].SetActive (true);
		}		
	}

	///點擊物件名稱順序表
	public  static  void orderlist (string name,int nameID){
		if (name != null) {
			order [ordernumber] = name;
			orderID [ordernumber] = nameID;
			ordernumber++;
			AnswerCount++;
		if (ordernumber > maxvalue - 1)
				ordernumber = 0;
		}
	}

	///顯示點擊物件名稱順序表
	public void showorderlist (){	
		for (int i = 0; i < maxvalue; i++) {
			Debug.Log (order [i]);
		}
	}		

	//確認點擊順序與隨機順序是否一致
	public void CheckTheSame(){
		print("點擊"+AnswerCount+"個開始確認");
		for (int i = 0; i < maxvalue; i++) {
			if (orderID[i] == RandomArr [i]) {
				BingoCount++;
			}
		}
//		if(BingoCount != AnswerCount){
//			GameObject heart = GameObject.Find("hearts"+5);
//			Destroy (heart);
//			TimerGUIText.index--;
//		}

		//掛載Bingo腳本，顯示"checked"打勾
		bingo = gameObject.AddComponent<Bingo> ();

	}

	void prefab(){
		// Quaternion.identity( READ ONLY) : 毫無旋轉，回歸原始旋轉值，簡單來說就rotation(0,0,0)
		//Instantiate (love,new Vector2(love.transform.position.x+20,love.transform.position.y),Quaternion.identity);

//		love = GameObject.Find("hearts");

		for(int i=1;i<=ProgressBar.ProgressRadialBehaviour.index;i++){
			GameObject obj = Instantiate (Resources.Load("hearts"), new Vector2(-1450+(i*140),710), transform.rotation)as GameObject;
			obj.name = "hearts"+i;
		}
	}
}

		//GameObject.Find("New Sprite").GetComponent<SpriteRenderer> ().sprite = Sprite;


		// Quaternion.identity( READ ONLY) : 毫無旋轉，回歸原始旋轉值，簡單來說就rotation(0,0,0)
		//Instantiate (love,new Vector2(love.transform.position.x+20,love.transform.position.y),Quaternion.identity);


using UnityEngine;
using System.Collections;

public class camera2 : MonoBehaviour {

	[SerializeField]private GameObject target;//Camera要面向的物件
	[SerializeField]private float speed;//相機環繞移動的速度
	private Vector3 cameraPosition;//相機要移動的位置
	private float number;
	[SerializeField] private float radius;//移動的半徑

	private void Start ()
	{
		//由於我這個範例是x和z軸的移動，而y軸不會改變，所以要先設定好y軸的初始位置
		//		cameraPosition.y = gameObject.transform.position.y;
		//		gameObject.transform.position = cameraPosition;

		//計算當前攝影機和目標物件的半徑
		radius = Vector3.Distance(target.transform.position, gameObject.transform.position);
	}


	private void Update ()
	{
		//使用Time.deltaTime，使得移動時更加平滑
		//將速度進行一定比例縮放，方便控制速度(縮放多少都隨意，自己覺得數值修改方便就好)
		number += Time.deltaTime * speed * 0.1f;

		//計算並設定新的x和y軸位置
		//負數是順時針旋轉，正數是逆時針旋轉
		cameraPosition.x = radius * Mathf.Cos(-number+180);
		cameraPosition.z = radius * Mathf.Sin(-number+180);
		gameObject.transform.position = cameraPosition;

		//使相機永遠面對著目標物件
		transform.LookAt(target.transform.position);

	
//gameObject.transform.RotateAround (obj.transform.localPosition, Vector3.up , speed * Time.deltaTime*10);
	}
}



		//速度控制
//		number += Time.deltaTime * speed;

		//計算並設定新的x和y軸位置
		//負數是順時針旋轉，正數是逆時針旋轉

//		cup_position1.x = radius * Mathf.Cos (number);
//		cup_position1.z = radius * Mathf.Sin (number);
//		cup1.transform.localPosition = cup_position1;
//
//		cup_position2.x = radius * Mathf.Cos (number+179.1f);
//		cup_position2.z = radius * Mathf.Sin (number+179.1f);
//		cup2.transform.localPosition = cup_position2;

		//使相機面對著目標物件
//		cup1.transform.LookAt(obj.transform.position);
//		cup2.transform.LookAt(obj.transform.position);


//		circle.transform.Rotate(new Vector3 (0f, -1.5f, 0f));
using UnityEngine;
using System.Collections;

public class MakeUFO : MonoBehaviour {

	public GameObject obj;
	public GameObject obj2;

	// Use this for initialization
	void Start (){
		//GameObject.Find("UI Root (3D)").transform.parent = Instantiate (Resources.Load("UFO_group"),new Vector3(0,496,0),transform.rotation)as Transform;
		GameObject k = NGUITools.AddChild (obj,Resources.Load("Display"));

	//Instantiate (Resources.Load("hearts"), new Vector2(-1450+(i*140),710), transform.rotation)as GameObject;
		//UIPanel ui = NGUITools.FindInParents<UIPanel>(Panel);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Display : MonoBehaviour {

	[SerializeField]private GameObject Panel;
	[SerializeField]private GameObject prefab;
	public static int ReferencePoint = 0;



	public class ClickEvent{
		bool op  = true;
		int i = 0;
		public void Action(GameObject obj){
			Debug.Log(obj.name);

			i++;
			switch(i){
			case 1:
				obj.GetComponent<TweenPosition> ().from = new Vector3 (ReferencePoint, -316, 0);
				obj.GetComponent<TweenPosition>().to = obj.transform.localPosition;
				ReferencePoint += 150;
			    break;
			default:
				ReferencePoint -= 150;
				i = 0;
				break;
			}
//
//			if(op){
////				obj.GetComponent<TweenPosition> ().from = obj.transform.localPosition;
////				obj.GetComponent<TweenPosition>().to = new Vector3 (0, -316, 0);
//				obj.GetComponent<TweenPosition> ().from = new Vector3 (0, -316, 0);
//				obj.GetComponent<TweenPosition>().to = obj.transform.localPosition;
//				ReferencePoint += 150;
//				op = false;
//			}
//			iTween.MoveTo (obj, iTween.Hash ("position", new Vector3 (0,-319f,0), "islocal", true,"easetype",iTween.EaseType.easeOutBack,"time",0.5));
		}
	}

	void Start (){		
		//NGUITools.AddChild (Panel,prefab);

		UIEventListener.Get(GameObject.Find("UFO-0")).onClick = new ClickEvent().Action;
		UIEventListener.Get(GameObject.Find("UFO-1")).onClick = new ClickEvent().Action;
		UIEventListener.Get(GameObject.Find("UFO-2")).onClick = new ClickEvent().Action;
	  //UIEventListener.Get(GameObject.Find("UFO-2")).onClick = new ClickEvent(GameObject.Find("UFO-2")).Action;
	}


	void Update () {

	}

	public void OpenBoxCollider(){
		GameObject.Find("UFO-0").GetComponent<BoxCollider> ().enabled = true;
		GameObject.Find("UFO-1").GetComponent<BoxCollider> ().enabled = true;
		GameObject.Find("UFO-2").GetComponent<BoxCollider> ().enabled = true;
		GameObject.Find("UFO-0").GetComponent<TweenPosition> ().duration = 0.5f;
		GameObject.Find("UFO-1").GetComponent<TweenPosition> ().duration = 0.5f;
		GameObject.Find("UFO-2").GetComponent<TweenPosition> ().duration = 0.5f;
		GameObject.Find("UFO-0").GetComponent<TweenPosition>().delay = 0f;
		GameObject.Find("UFO-1").GetComponent<TweenPosition>().delay = 0f;
		GameObject.Find("UFO-2").GetComponent<TweenPosition>().delay = 0f;
//		UIEventListener.Get(GameObject.Find("UFO-1")).GetComponent<BoxCollider> ().enabled = true;
//		UIEventListener.Get(GameObject.Find("UFO-2")).GetComponent<BoxCollider> ().enabled = true;

	}
}
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Display : MonoBehaviour {

	[SerializeField]private GameObject Panel;
	[SerializeField]private GameObject prefab;
	public static int ReferencePoint = -200;
	public static int interval = 200;


	public class ClickEvent{
		int i = 0;
		public void Action(GameObject obj){
			Debug.Log(obj.name);

			i++;
			switch(i){
			case 1:
				obj.GetComponent<TweenPosition> ().from = new Vector3 (ReferencePoint, -316, 0);
				obj.GetComponent<TweenPosition>().to = obj.transform.localPosition;
				ReferencePoint += interval;
			    break;
			default:
				ReferencePoint -= interval;
				i = 0;
				break;
			}
		}
	}

	void Start (){		
		//NGUITools.AddChild (Panel,prefab);

		UIEventListener.Get(GameObject.Find("UFO-0")).onClick = new ClickEvent().Action;
		UIEventListener.Get(GameObject.Find("UFO-1")).onClick = new ClickEvent().Action;
		UIEventListener.Get(GameObject.Find("UFO-2")).onClick = new ClickEvent().Action;
//      UIEventListener.Get (GameObject.Find ("UFO-0")).onClick = new Display.ClickEvent ().Action;

//		//汽車
//		Car BMW = new Car();
//		BMW.Fly(30);
//
//
//		//小鳥
//		Bird bird1 = new Bird();
//		bird1.Fly(5);
//	
//		//機車
//		Motocycle Yamaha = new Motocycle();
//		Yamaha.Add_Oil(6);  //加油
//		Yamaha.Fly(10);   //實作IFly

	}


	void Update () {

	}


	class box{
		public box(GameObject obj){
			obj.GetComponent<BoxCollider> ().enabled = true;
			obj.GetComponent<TweenPosition> ().duration = 0.5f;
			obj.GetComponent<TweenPosition>().delay = 0f;
		}
	}


	public void OpenBoxCollider(){
		new box (GameObject.Find ("UFO-0"));
		new box (GameObject.Find ("UFO-1"));
		new box (GameObject.Find ("UFO-2"));


//		GameObject.Find("UFO-0").GetComponent<BoxCollider> ().enabled = true;
//		GameObject.Find("UFO-1").GetComponent<BoxCollider> ().enabled = true;
//		GameObject.Find("UFO-2").GetComponent<BoxCollider> ().enabled = true;
//		GameObject.Find("UFO-0").GetComponent<TweenPosition> ().duration = 0.5f;
//		GameObject.Find("UFO-1").GetComponent<TweenPosition> ().duration = 0.5f;
//		GameObject.Find("UFO-2").GetComponent<TweenPosition> ().duration = 0.5f;
//		GameObject.Find("UFO-0").GetComponent<TweenPosition>().delay = 0f;
//		GameObject.Find("UFO-1").GetComponent<TweenPosition>().delay = 0f;
//		GameObject.Find("UFO-2").GetComponent<TweenPosition>().delay = 0f;
//		UIEventListener.Get(GameObject.Find("UFO-1")).GetComponent<BoxCollider> ().enabled = true;




	}




//
//	interface IFly //定義IFly介面 
//	{
//		void Fly(int n); //宣告Fly方法
//	}
//	class Car : IFly    //Car類別實作IFly介面
//	{
//		public void SpeedUp(int n)
//		{
//			Debug.Log("車子加速前進 {0} 公里"+n);
//		}
//		//Car類別的Fly方法實作IFly介面的Fly方法
//
//		public void Fly(int n)
//		{
//			Debug.Log("車子飛上天前進 {0} 公里"+n);
//		}
//	}
//	class Bird : IFly //Bird類別實作IFly介面
//	{
//		public void Eat(int n)
//		{
//			Debug.Log("小鳥吃了 {0} 公斤的飼料"+n);
//		}
//		//Bird類別的Fly方法實作IFly介面的Fly方法
//		public void Fly(int n)
//		{
//			Debug.Log("小鳥飛上天前進 {0} 公里"+n);
//		}
//	}
//	class Motocycle : IFly //Motocycle類別實作IFly介面
//	{
//		public void Add_Oil(int n)
//		{
//			Debug.Log("機車加油 {0} 公升 "+n);
//		}
//		//Motocycle類別的Fly方法實作IFly介面的Fly方法
//		public void Fly(int n)
//		{
//			Debug.Log("機車飛上天前進 {0} 公里"+n);
//		}
//	}
}
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Display : MonoBehaviour {

	[SerializeField]private GameObject Panel;
	[SerializeField]private GameObject prefab;
	public static int ReferencePoint = -200;
	public static int interval = 200;


	public class ClickEvent{
		int i = 0;
		public void Action(GameObject obj){
			Debug.Log(obj.name);

			i++;
			switch(i){
			case 1:
				obj.GetComponent<TweenPosition> ().from = new Vector3 (ReferencePoint, -316, 0);
				obj.GetComponent<TweenPosition>().to = obj.transform.localPosition;
				ReferencePoint += interval;
			    break;
			default:
				ReferencePoint -= interval;
				i = 0;
				break;
			}
		}
	}

	void Start (){		
		//NGUITools.AddChild (Panel,prefab);

		UIEventListener.Get(GameObject.Find("UFO-0")).onClick = new ClickEvent().Action;
		UIEventListener.Get(GameObject.Find("UFO-1")).onClick = new ClickEvent().Action;
		UIEventListener.Get(GameObject.Find("UFO-2")).onClick = new ClickEvent().Action;
//      UIEventListener.Get (GameObject.Find ("UFO-0")).onClick = new Display.ClickEvent ().Action;

//		//汽車
//		Car BMW = new Car();
//		BMW.Fly(30);
//
//
//		//小鳥
//		Bird bird1 = new Bird();
//		bird1.Fly(5);
//	
//		//機車
//		Motocycle Yamaha = new Motocycle();
//		Yamaha.Add_Oil(6);  //加油
//		Yamaha.Fly(10);   //實作IFly

	}


	void Update () {

	}


	class ClickTweenset{
		public ClickTweenset(int key){
			for(int i=0;i<=key;i++){
				GameObject.Find ("UFO-"+i).GetComponent<BoxCollider> ().enabled = true;
				GameObject.Find ("UFO-"+i).GetComponent<TweenPosition> ().duration = 0.5f;
				GameObject.Find ("UFO-"+i).GetComponent<TweenPosition>().delay = 0f;
			}
		}
	}


	public void OpenBoxCollider(){
		new ClickTweenset (2);
//		new ClickTweenset (GameObject.Find ("UFO-1"));
//		new ClickTweenset (GameObject.Find ("UFO-2"));
	}




//
//	interface IFly //定義IFly介面 
//	{
//		void Fly(int n); //宣告Fly方法
//	}
//	class Car : IFly    //Car類別實作IFly介面
//	{
//		public void SpeedUp(int n)
//		{
//			Debug.Log("車子加速前進 {0} 公里"+n);
//		}
//		//Car類別的Fly方法實作IFly介面的Fly方法
//
//		public void Fly(int n)
//		{
//			Debug.Log("車子飛上天前進 {0} 公里"+n);
//		}
//	}
//	class Bird : IFly //Bird類別實作IFly介面
//	{
//		public void Eat(int n)
//		{
//			Debug.Log("小鳥吃了 {0} 公斤的飼料"+n);
//		}
//		//Bird類別的Fly方法實作IFly介面的Fly方法
//		public void Fly(int n)
//		{
//			Debug.Log("小鳥飛上天前進 {0} 公里"+n);
//		}
//	}
//	class Motocycle : IFly //Motocycle類別實作IFly介面
//	{
//		public void Add_Oil(int n)
//		{
//			Debug.Log("機車加油 {0} 公升 "+n);
//		}
//		//Motocycle類別的Fly方法實作IFly介面的Fly方法
//		public void Fly(int n)
//		{
//			Debug.Log("機車飛上天前進 {0} 公里"+n);
//		}
//	}
}
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Display : MonoBehaviour {

	[SerializeField]private GameObject Panel;
	[SerializeField]private GameObject prefab;
	public static int ReferencePoint = -200;
	public static int interval = 200;

	public static List<GameObject> UFO = new List<GameObject>();

	public class ClickEvent{
		int i = 0;
		static int j = -1;




		public void Action(GameObject obj){
			Debug.Log(obj.name);

			i++;


	 switch(i){
			case 1:
				obj.GetComponent<TweenPosition> ().from = new Vector3 (ReferencePoint, -316, 0);
				obj.GetComponent<TweenPosition> ().to = obj.transform.localPosition;
				ReferencePoint += interval;
				UFO.Add (obj);

				switch(j){
				case 0:
					UFO [0].GetComponent<BoxCollider> ().enabled = false;
					break;
				case 1:
					UFO [1].GetComponent<BoxCollider> ().enabled = false;
					break;
				}
//
//				if(j == 0)
//					UFO [0].GetComponent<BoxCollider> ().enabled = false;
//				if(j == 1)
//					UFO [1].GetComponent<BoxCollider> ().enabled = false;
				j++;
			    break;
			default:
				ReferencePoint -= interval;
				i = 0;
				j--;
				switch(j){
				  case 0:UFO [0].GetComponent<BoxCollider> ().enabled = true;break;
				  case 1:UFO [1].GetComponent<BoxCollider> ().enabled = true;break;
				}
//				if (j == 0)
//					UFO [0].GetComponent<BoxCollider> ().enabled = true;
//				if (j == 1)
//					UFO [1].GetComponent<BoxCollider> ().enabled = true;

				UFO.Remove (obj);

				break;
			}
		}
	}

	public void show(){
//		Debug.Log(UFO[0].name);
		foreach (GameObject obj in UFO) {
			Debug.Log (obj.name);
		}
	}


	void Start (){		
		//NGUITools.AddChild (Panel,prefab);

		UIEventListener.Get(GameObject.Find("UFO-0")).onClick = new ClickEvent().Action;
		UIEventListener.Get(GameObject.Find("UFO-1")).onClick = new ClickEvent().Action;
		UIEventListener.Get(GameObject.Find("UFO-2")).onClick = new ClickEvent().Action;
//      UIEventListener.Get (GameObject.Find ("UFO-0")).onClick = new Display.ClickEvent ().Action;


	}


	void Update () {

	}
		
	public void OpenBoxCollider(){
		for(int i=0;i<=2;i++){
			GameObject.Find ("UFO-"+i).GetComponent<BoxCollider> ().enabled = true;
			GameObject.Find ("UFO-"+i).GetComponent<TweenPosition> ().duration = 0.5f;
			GameObject.Find ("UFO-"+i).GetComponent<TweenPosition>().delay = 0f;
		}
	}
}
	public enum UFO_second : int{
		second1 = 1,
		second2 = 2,
		second3 = 3,
	}


	void Update () {

		time += Time.deltaTime;
		switch(Mathf.CeilToInt(time)){
		case (int)UFO_second.second1:
			UFO_second A = UFO_second.second1;
			Debug.Log (A);
			break;
		case (int)UFO_second.second2:
			UFO_second B = UFO_second.second2;
			Debug.Log (B);
			break;
		case (int)UFO_second.second3:
			UFO_second C = UFO_second.second3;
			Debug.Log (C);
			break;
		}
	}
		GameObject [] target;
		target = GameObject.FindGameObjectsWithTag ("UFO");
		for(int i=0;i<=2;i++) {
			target[i].GetComponent<BoxCollider> ().enabled = true;
			target[i].GetComponent<TweenPosition> ().duration = 0.5f;
			target[i].GetComponent<TweenPosition>().delay = 0f;
		}
		using UnityEngine;
using System.Collections;
using System.Collections.Generic;
 

public class Display : MonoBehaviour {

	[SerializeField]private GameObject Panel;
	[SerializeField]private GameObject prefab;
	public static int ReferencePoint = -200;//UFO移動至下方X軸之位置
	public static int interval = 200;//UFO間隔

	public static List<GameObject> UFO = new List<GameObject>();//UFO陣列

	int [] sequence = new int[3];
	int [] RandomArr = new int[3];


	private float time = -5;




	//點擊事件
	public class ClickEvent{
//		public bool op = true;
	    public static int j = -1;//BoxCollider開關

//		public  void iget(GameObject obj){
//			this.i = 0;
//		}

//		public bool iget{
//			get {return this.op;}
//			set {this.op = value;}
//		}

//		public void iget(){
//			this.op = true;
//		}

		public void Action(GameObject obj){
			Debug.Log(obj.name);





			if(obj.GetComponent<Listener> ().op){

				obj.GetComponent<TweenPosition> ().from = new Vector3 (ReferencePoint, -316, 0);
				obj.GetComponent<TweenPosition> ().to = obj.transform.localPosition;
				ReferencePoint += interval;
				UFO.Add (obj);

		switch(j){
				   case 0:UFO [0].GetComponent<BoxCollider> ().enabled = false;break;
				   case 1:UFO [1].GetComponent<BoxCollider> ().enabled = false;break;
				   case 2:UFO [2].GetComponent<BoxCollider> ().enabled = false;break;
		       }

				j++;
				obj.GetComponent<Listener> ().op = false;

			}else{
				ReferencePoint -= interval;
				obj.GetComponent<Listener> ().op = true;
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

	public void show(){
//		Debug.Log(UFO[0].name);
		foreach (GameObject obj in UFO) {
			Debug.Log (obj.name);
		}
	}


	void Start (){	
	
	


		//NGUITools.AddChild (Panel,prefab);
		makeRandomArr();


//		ClickEvent clk = new ClickEvent ();
//		ClickEvent clk2 = new ClickEvent ();
//
//		clk.iget = 10;
//		Debug.Log ("rrr"+clk.iget);
//
//		clk2.iget = 20;
//		Debug.Log ("2222"+clk2.iget);

		UIEventListener.Get(GameObject.Find("UFO-0")).onClick = new ClickEvent().Action;
		UIEventListener.Get(GameObject.Find("UFO-1")).onClick = new ClickEvent().Action;
		UIEventListener.Get(GameObject.Find("UFO-2")).onClick = new ClickEvent().Action;
//      UIEventListener.Get (GameObject.Find ("UFO-0")).onClick = new Display.ClickEvent ().Action;
	}

	public enum UFO_redlight : int{
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


	void Update () {

		Debug.Log ("ReferencePoint"+ ReferencePoint);
//		Debug.Log ("interval"+ interval);

		time += Time.deltaTime;
		switch(Mathf.CeilToInt(time)){
		case 1:
			GameObject.Find ("UFO-"+RandomArr[0]).GetComponent<UITexture>().mainTexture = Resources.Load<Texture> ("UFO_light/" + (UFO_redlight)5);
			break;
		case 2:
			GameObject.Find ("UFO-"+RandomArr[1]).GetComponent<UITexture>().mainTexture = Resources.Load<Texture> ("UFO_light/" + (UFO_redlight)5);
			GameObject.Find ("UFO-"+RandomArr[0]).GetComponent<UITexture>().mainTexture = Resources.Load<Texture> ("UFO_light/" + (UFO_redlight)0);
			break;
		case 3:
			GameObject.Find ("UFO-"+RandomArr[2]).GetComponent<UITexture>().mainTexture = Resources.Load<Texture> ("UFO_light/" + (UFO_redlight)5);
			GameObject.Find ("UFO-"+RandomArr[1]).GetComponent<UITexture>().mainTexture = Resources.Load<Texture> ("UFO_light/" + (UFO_redlight)0);
			break;
		default:
			GameObject.Find ("UFO-"+RandomArr[2]).GetComponent<UITexture>().mainTexture = Resources.Load<Texture> ("UFO_light/" + (UFO_redlight)0);
			break;
		}
	}
		
	public void OpenBoxCollider(){
		GameObject [] target;
		target = GameObject.FindGameObjectsWithTag ("UFO");
		for(int i=0;i<=2;i++) {
			target[i].GetComponent<BoxCollider> ().enabled = true;
			target[i].GetComponent<TweenPosition> ().duration = 0.5f;
			target[i].GetComponent<TweenPosition>().delay = 0f;
		}
	}

	void makeRandomArr(){
		for (int i = 0; i < RandomArr.Length; i++) {
			sequence [i] = i;
		}
		int end = RandomArr.Length-1;
		for (int i = 0; i < RandomArr.Length; i++) {
			int num = Random.Range (0, end+1);
			RandomArr[i] = sequence [num];
			sequence[num]=sequence[end];
			end--;
		}

		//寫一個判斷兩次random值一樣的話在執行makeRandomArr一次

	}

	public void ShowRandom (){
		foreach (int Arr in RandomArr) {
			Debug.Log (Arr);
		}
	}

	public void restart(){
		time = -2;

		makeRandomArr();
	

		GameObject.Find (UFO[2].name).GetComponent<UIPlayTween> ().Play (true);
		GameObject.Find (UFO[1].name).GetComponent<UIPlayTween> ().Play (true);
		GameObject.Find (UFO[0].name).GetComponent<UIPlayTween> ().Play (true);

	
		GameObject.Find (UFO[2].name).GetComponent<Listener> ().op = true;
		GameObject.Find (UFO[1].name).GetComponent<Listener> ().op = true;
		GameObject.Find (UFO[0].name).GetComponent<Listener> ().op = true;

		GameObject.Find ("UFO-0").GetComponent<BoxCollider> ().enabled = true;
		GameObject.Find ("UFO-1").GetComponent<BoxCollider> ().enabled = true;
		GameObject.Find ("UFO-2").GetComponent<BoxCollider> ().enabled = true;


		ReferencePoint = -200;








		UFO.Clear ();

//		UIEventListener.Get (GameObject.Find ("UFO-0")).onSubmit = new ClickEvent ().iget;
//		UIEventListener.Get (GameObject.Find ("UFO-1")).onSubmit = new ClickEvent ().iget;
//		UIEventListener.Get (GameObject.Find ("UFO-2")).onSubmit = new ClickEvent ().iget;

		ClickEvent.j = -1;






	}

}
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
 

public class Display : MonoBehaviour {

	[SerializeField]private GameObject Panel;
	[SerializeField]private GameObject prefab;
	public static int ReferencePoint = -200;//UFO移動至下方X軸之位置
	public static int interval = 200;//UFO間隔

	public static List<GameObject> UFO = new List<GameObject>();//UFO陣列

	int [] sequence = new int[3];
	int [] RandomArr = new int[3];


	private float time = -5;




	//點擊事件
	public class ClickEvent{
//		public bool op = true;
	    public static int j = -1;//BoxCollider開關

//		public  void iget(GameObject obj){
//			this.i = 0;
//		}

//		public bool iget{
//			get {return this.op;}
//			set {this.op = value;}
//		}

//		public void iget(){
//			this.op = true;
//		}

		public void Action(GameObject obj){
			Debug.Log(obj.name);





			if(obj.GetComponent<Listener> ().op){

				obj.GetComponent<TweenPosition> ().from = new Vector3 (ReferencePoint, -316, 0);
				obj.GetComponent<TweenPosition> ().to = obj.transform.localPosition;
				ReferencePoint += interval;
				UFO.Add (obj);

		switch(j){
				   case 0:UFO [0].GetComponent<BoxCollider> ().enabled = false;break;
				   case 1:UFO [1].GetComponent<BoxCollider> ().enabled = false;break;
				   case 2:UFO [2].GetComponent<BoxCollider> ().enabled = false;break;
		       }

				j++;
				obj.GetComponent<Listener> ().op = false;

			}else{
				ReferencePoint -= interval;
				obj.GetComponent<Listener> ().op = true;
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

	public void show(){
//		Debug.Log(UFO[0].name);
		foreach (GameObject obj in UFO) {
			Debug.Log (obj.name);
		}
	}


	void Start (){	
	
	


		//NGUITools.AddChild (Panel,prefab);
		makeRandomArr();


//		ClickEvent clk = new ClickEvent ();
//		ClickEvent clk2 = new ClickEvent ();
//
//		clk.iget = 10;
//		Debug.Log ("rrr"+clk.iget);
//
//		clk2.iget = 20;
//		Debug.Log ("2222"+clk2.iget);

		UIEventListener.Get(GameObject.Find("UFO-0")).onClick = new ClickEvent().Action;
		UIEventListener.Get(GameObject.Find("UFO-1")).onClick = new ClickEvent().Action;
		UIEventListener.Get(GameObject.Find("UFO-2")).onClick = new ClickEvent().Action;
//      UIEventListener.Get (GameObject.Find ("UFO-0")).onClick = new Display.ClickEvent ().Action;
	}

	public enum UFO_redlight : int{
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


	void Update () {

		Debug.Log ("ReferencePoint"+ ReferencePoint);
//		Debug.Log ("interval"+ interval);

		time += Time.deltaTime;
		switch(Mathf.CeilToInt(time)){
		case 1:
			GameObject.Find ("UFO-"+RandomArr[0]).GetComponent<UITexture>().mainTexture = Resources.Load<Texture> ("UFO_light/" + (UFO_redlight)5);
			break;
		case 2:
			GameObject.Find ("UFO-"+RandomArr[1]).GetComponent<UITexture>().mainTexture = Resources.Load<Texture> ("UFO_light/" + (UFO_redlight)5);
			GameObject.Find ("UFO-"+RandomArr[0]).GetComponent<UITexture>().mainTexture = Resources.Load<Texture> ("UFO_light/" + (UFO_redlight)0);
			break;
		case 3:
			GameObject.Find ("UFO-"+RandomArr[2]).GetComponent<UITexture>().mainTexture = Resources.Load<Texture> ("UFO_light/" + (UFO_redlight)5);
			GameObject.Find ("UFO-"+RandomArr[1]).GetComponent<UITexture>().mainTexture = Resources.Load<Texture> ("UFO_light/" + (UFO_redlight)0);
			break;
		default:
			GameObject.Find ("UFO-"+RandomArr[2]).GetComponent<UITexture>().mainTexture = Resources.Load<Texture> ("UFO_light/" + (UFO_redlight)0);
			break;
		}
	}
		
	public void OpenBoxCollider(){
		GameObject [] target;
		target = GameObject.FindGameObjectsWithTag ("UFO");
		for(int i=0;i<=2;i++) {
			target[i].GetComponent<BoxCollider> ().enabled = true;
			target[i].GetComponent<TweenPosition> ().duration = 0.5f;
			target[i].GetComponent<TweenPosition>().delay = 0f;
		}
	}

	void makeRandomArr(){
		for (int i = 0; i < RandomArr.Length; i++) {
			sequence [i] = i;
		}
		int end = RandomArr.Length-1;
		for (int i = 0; i < RandomArr.Length; i++) {
			int num = Random.Range (0, end+1);
			RandomArr[i] = sequence [num];
			sequence[num]=sequence[end];
			end--;
		}

		//寫一個判斷兩次random值一樣的話在執行makeRandomArr一次

	}

	public void ShowRandom (){
		foreach (int Arr in RandomArr) {
			Debug.Log (Arr);
		}
	}

	public void restart(){
		time = -2;

		makeRandomArr();
	
		for(int i=2;i>=0;i--){
			UFO[i].GetComponent<UIPlayTween> ().Play (true);
			UFO[i].GetComponent<Listener> ().op = true;
			UFO[i].GetComponent<BoxCollider> ().enabled = true;
		}



//		UFO[2].GetComponent<UIPlayTween> ().Play (true);
//		UFO[1].GetComponent<UIPlayTween> ().Play (true);
//		UFO[0].GetComponent<UIPlayTween> ().Play (true);
//
//		UFO[2].GetComponent<Listener> ().op = true;
//		UFO[1].GetComponent<Listener> ().op = true;
//		UFO[0].GetComponent<Listener> ().op = true;
//
//		UFO[2].GetComponent<BoxCollider> ().enabled = true;
//		UFO[1].GetComponent<BoxCollider> ().enabled = true;
//		UFO[0].GetComponent<BoxCollider> ().enabled = true;

//		GameObject.Find ("UFO-0").GetComponent<BoxCollider> ().enabled = true;
//		GameObject.Find ("UFO-1").GetComponent<BoxCollider> ().enabled = true;
//		GameObject.Find ("UFO-2").GetComponent<BoxCollider> ().enabled = true;


		ReferencePoint = -200;


		UFO.Clear ();


		ClickEvent.j = -1;






	}

}


  DieTimer dieTimer = newObject.AddComponent<DieTimer>() as DieTimer;
                dieTimer.SecondsToDie = Life;


[Header("Status")]
[Range(1,10)]
	public int speed =5;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Level2_display : MonoBehaviour
{

	[Header ("List")]
	public List<GameObject> ball_sequence = new List<GameObject> ();
	public List<GameObject> ball = new List<GameObject> ();

	[Header ("Material")]
	[SerializeField]private Material Red;

	public static Material TiffanyBlue_static;
	[SerializeField]private Material TiffanyBlue;

	public static Material Gray_static;
	[SerializeField]private Material Gray;

	public static int ball_Arr = 0;

	public GameObject Cylinder;


	void Awake ()
	{
		Gray_static = Gray;

		TiffanyBlue_static = TiffanyBlue;
	}


	public class ClickEvent
	{
		public void Action (GameObject obj)
		{
			Debug.Log (obj.name);

			if (obj.GetComponent<Renderer> ().material.color == TiffanyBlue_static.color)
				obj.GetComponent<Renderer> ().material = Gray_static;
			else
				obj.GetComponent<Renderer> ().material = TiffanyBlue_static;
		}
	}



	// Use this for initialization
	void Start ()
	{
		makeRandomArr ();
		InvokeRepeating ("ShowLight", 1f, 1f);

		UIEventListener.Get (ball [0]).onClick = new ClickEvent ().Action;
		UIEventListener.Get (ball [1]).onClick = new ClickEvent ().Action;
		UIEventListener.Get (ball [2]).onClick = new ClickEvent ().Action;
		UIEventListener.Get (ball [3]).onClick = new ClickEvent ().Action;
		UIEventListener.Get (ball [4]).onClick = new ClickEvent ().Action;
		UIEventListener.Get (ball [5]).onClick = new ClickEvent ().Action;
		UIEventListener.Get (ball [6]).onClick = new ClickEvent ().Action;
		UIEventListener.Get (ball [7]).onClick = new ClickEvent ().Action;
	}
	
	// Update is called once per frame
	void Update ()
	{
		gameObject.transform.LookAt (Cylinder.transform.position,Vector3.forward);
	}



	void makeRandomArr ()
	{
		for (int i = 0; i < 8; i++) {
			int num = Random.Range (0, ball_sequence.Count);
			ball.Add (ball_sequence [num]);
			ball_sequence [num] = ball_sequence [ball_sequence.Count - 1];
			ball_sequence.RemoveAt (ball_sequence.Count - 1);
		}
	}

	public void showarr ()
	{
		foreach (GameObject obj in ball) {
			Debug.Log (obj.name);
		}
	}

	public void ShowLight ()
	{
		if (ball_Arr == 8) {
			CancelInvoke ("ShowLight");
			ball [7].GetComponent<Renderer> ().material = TiffanyBlue;
		} else {
			if (ball_Arr != 0) {
				ball [ball_Arr].GetComponent<Renderer> ().material = Red;
				ball [ball_Arr - 1].GetComponent<Renderer> ().material = TiffanyBlue;
			} else {
				ball [ball_Arr].GetComponent<Renderer> ().material = Red;
			}
			ball_Arr++;
		}
	}


	public void CameraMove(){
		
//		gameObject.GetComponent<TweenPosition>().enabled = true;
//		gameObject.GetComponent<TweenOrthoSize> ().enabled = true;
		gameObject.GetComponent<UIPlayTween> ().Play (true);


	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Level2_display : MonoBehaviour
{
	[Header ("Object")]
	[SerializeField]private GameObject Panel;
	[SerializeField]private GameObject Bingo;
	[SerializeField]private GameObject Error;


	[Header ("List")]
	public List<GameObject> Ball_sequence = new List<GameObject> ();
	public List<GameObject> Ball_Random = new List<GameObject> ();
	public static List<GameObject> Ball = new List<GameObject> ();

	[Header ("Material")]
	[SerializeField]private Material Red;

	public static Material TiffanyBlue_static;
	[SerializeField]private Material TiffanyBlue;

	public static Material Gray_static;
	[SerializeField]private Material Gray;

	public static int ball_Arr = 0;

	bool compare = true;


	[Header ("LookAt")]
	//Camera將LookAt的中心點
	public GameObject Center;


	void Awake ()
	{
		Gray_static = Gray;

		TiffanyBlue_static = TiffanyBlue;
	}


	public class ClickEvent
	{
		public void Action (GameObject obj)
		{
			Debug.Log (obj.name);



			if (obj.GetComponent<Renderer> ().material.color == TiffanyBlue_static.color) {
				obj.GetComponent<Renderer> ().material = Gray_static;
				Ball.Add (obj);
			} else {
				obj.GetComponent<Renderer> ().material = TiffanyBlue_static;
				Ball.Remove (obj);
			}
		}
	}




	void Start ()
	{
		

		makeRandomArr ();
		InvokeRepeating ("ShowLight", 1f, 1f);

		UIEventListener.Get (Ball_Random [0]).onClick = new ClickEvent ().Action;
		UIEventListener.Get (Ball_Random [1]).onClick = new ClickEvent ().Action;
		UIEventListener.Get (Ball_Random [2]).onClick = new ClickEvent ().Action;
		UIEventListener.Get (Ball_Random [3]).onClick = new ClickEvent ().Action;
		UIEventListener.Get (Ball_Random [4]).onClick = new ClickEvent ().Action;
		UIEventListener.Get (Ball_Random [5]).onClick = new ClickEvent ().Action;
		UIEventListener.Get (Ball_Random [6]).onClick = new ClickEvent ().Action;
		UIEventListener.Get (Ball_Random [7]).onClick = new ClickEvent ().Action;

	

	}

//	delegate  GameObject Clone (GameObject obj);

	// Update is called once per frame
	void Update ()
	{
		gameObject.transform.LookAt (Center.transform.position, Vector3.forward);

		if (Ball.Count == 8 && compare) {
			
			GameObject clone;



			if (Ball.SequenceEqual (Ball_Random)) {
				//答對
				clone = NGUITools.AddChild (Panel, Bingo);
			} else {
				//答錯
				clone = NGUITools.AddChild (Panel, Error);
			}

			clone.transform.localPosition = new Vector3 (140729, 100632, 26417);
			clone.transform.Rotate (Vector3.right, 33.24981f);
			clone.transform.localScale = new Vector3 (25000, 25000, 0);
			iTween.ScaleFrom (clone, iTween.Hash ("scale", Vector3.zero, "delay", 0.2, "oncomplete", "DestroyClone", "oncompletetarget", gameObject, "oncompleteparams", clone));

			compare = false;
			Invoke ("restart", 1.5f);
		}

	}

	void DestroyClone (GameObject obj)
	{
		Destroy (obj);
	}


	public void restart ()
	{


		Ball [0].GetComponent<Renderer> ().material = TiffanyBlue_static;
		Ball [1].GetComponent<Renderer> ().material = TiffanyBlue_static;
		Ball [2].GetComponent<Renderer> ().material = TiffanyBlue_static;
		Ball [3].GetComponent<Renderer> ().material = TiffanyBlue_static;
		Ball [4].GetComponent<Renderer> ().material = TiffanyBlue_static;
		Ball [5].GetComponent<Renderer> ().material = TiffanyBlue_static;
		Ball [6].GetComponent<Renderer> ().material = TiffanyBlue_static;
		Ball [7].GetComponent<Renderer> ().material = TiffanyBlue_static;

		// This will copy all the items from Ball_Random to Ball_sequence
//		Ball_Random.ForEach (i => Ball_sequence.Add (i));
		Ball_sequence.AddRange (Ball_Random);

		Ball.Clear ();

		Ball_Random.Clear ();

//		compare = true;

		//找出可以複製Ball到Ball_Random的方法 
		//清空Ball

	}

	void makeRandomArr ()
	{
		for (int i = 0; i < 8; i++) {
			int num = Random.Range (0, Ball_sequence.Count);
			Ball_Random.Add (Ball_sequence [num]);
			Ball_sequence [num] = Ball_sequence [Ball_sequence.Count - 1];
			Ball_sequence.RemoveAt (Ball_sequence.Count - 1);
		}
	}

	public void showarr ()
	{
		foreach (GameObject obj in Ball_Random) {
			Debug.Log (obj.name);
		}
	}

	public void ShowLight ()
	{
		if (ball_Arr == 8) {
			CancelInvoke ("ShowLight");
			Ball_Random [7].GetComponent<Renderer> ().material = TiffanyBlue;
		} else {
			if (ball_Arr != 0) {
				Ball_Random [ball_Arr].GetComponent<Renderer> ().material = Red;
				Ball_Random [ball_Arr - 1].GetComponent<Renderer> ().material = TiffanyBlue;
			} else {
				Ball_Random [ball_Arr].GetComponent<Renderer> ().material = Red;
			}
			ball_Arr++;
		}
	}

	public void ShowClickBall ()
	{
		foreach (GameObject obj in Ball)
			Debug.Log (obj.name);
	}


	public void CameraMove ()
	{
		

		gameObject.GetComponent<UIPlayTween> ().Play (true);


	}
}



	delegate int test(int x);

	test t = (int y) => y + 5;
		Debug.Log (t (5));

*/