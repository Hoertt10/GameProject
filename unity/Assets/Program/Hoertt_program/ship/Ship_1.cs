using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ship_1 : MonoBehaviour {
    [SerializeField]
    private List<GameObject> objlist = new List<GameObject>();

    public static List<List <Vector3>> Postable = new List<List <Vector3>>();
    static int  Level = 0;
    int turn = 1;
    //GameObject ship = GameObject.Find("ship");
    //GameObject ShipRotaPoint = GameObject.Find("ShipRotaPoint");
    private static int i = 0, x = 0,Qusnum, Randnum;
    public static bool ObjMoveStu=false;
    static bool AnsCover = false;
    public static int[] RandomArr,SequArr;
    public static string[] QusArr,AnsArr;
    float ShipMoveSp;

    private GameObject[] LoadAnimal;

    public static List<GameObject> AnimalList = new List<GameObject>();

    public static Vector3[] AppearPos = new Vector3[]
    {
        new Vector3(-4.22f,-2.09f),
        new Vector3(-2.75f,-2.73f),
        new Vector3(-1.19f,-3.67f),
        new Vector3(0.26f,-5.16f)
    };

    Vector3[] LV1Vec3 = new Vector3[3];

    static int Score = 0;
    static bool Anstatus = true,DelayStatus=false;
    // Use this for initialization

    public class CreatAnimal
    {
        public static int P_Pos = 0;

        public GameObject GetAnimal { get; private set; }

        public CreatAnimal (GameObject GO)
        {
            GO = Instantiate(GO, parent: GameObject.Find("AnimalGroup").transform)as GameObject;

            GO.transform.localPosition = AppearPos[P_Pos%4];

            GO.transform.localScale = Vector3.one;

            TweenPosition.Begin(GO, 1f, Postable[Level][P_Pos++]);

            AnimalList.Add(GO);

            GetAnimal(GO);

        }

    }
    void Start () {

                //初始化
        Initialization();
        StartCoroutine(delay());
    }

    //傳入變數SP,Qusnum,Rannum
    public  virtual void Initialization()
    {
        objlist.Clear();
        objlist.Add(GameObject.Find("11"));
        objlist.Add(GameObject.Find("21"));
        objlist.Add(GameObject.Find("31"));//foreach
        objlist.Add(GameObject.Find("12"));
        objlist.Add(GameObject.Find("22"));
        objlist.Add(GameObject.Find("32"));
        objlist.Add(GameObject.Find("13"));
        objlist.Add(GameObject.Find("23"));
        objlist.Add(GameObject.Find("33"));


        ImportTable();

        LoadAnimal = Resources.LoadAll<GameObject>("Hoertt/AnimalGroup");

        ShipMoveSp = Ship_select._floatFieldRight[Level];
        //ShipMoveSp = 50;;
        //Debug.Log("Postable"+Postable.Count);
        

        ObjMoveStu = false;
        click.clickopen = false;

        //makeRandomArr(3,1);
        i = 0;
        QusArr = new string[Ship_select._Rannum[Level]];
        AnsArr = new string[Ship_select._Rannum[Level]];
        SequArr = new int[Ship_select._Qusnum[Level]];
        makeRandomArr(Ship_select._Qusnum[Level], Ship_select._Rannum[Level]);

        ShowRandom();
    }

    // Update is called once per frame
    void Update () {
		if(ObjMoveStu==true)
        {
            if (GameObject.Find("ship").transform.localRotation.eulerAngles.z >= 350)
            {
                turn = 1;
            }
            else if (GameObject.Find("ship").transform.localRotation.eulerAngles.z >= 65)
            {
                turn = -1;
            }
            //Debug.Log(ship.transform.localRotation.eulerAngles.z + "   " + turn);
            GameObject.Find("ship").transform.RotateAround(GameObject.Find("ShipRotaPoint").transform.position, Vector3.forward, turn * ShipMoveSp * Time.deltaTime);

        }
    }
    
    /// <summary>
    /// 顯示或隱藏obj，In objlist[]
    /// </summary>
    void ShowObj()
    {
        Postable[Level].ForEach(GO => AnimalList.Add(new CreatAnimal(LoadAnimal[])));

        for (int i = 0; i <= objlist.IndexOf(null) - 1; i++)
        {
            //objlist[i].SetActive(true);
            objlist[i].GetComponent<Renderer>().sortingOrder = 4;
        }
        for (int i = Qusnum; i <= objlist.IndexOf(null) - 1; i++)
        {
            //objlist[i].SetActive(false);
            objlist[i].GetComponent<Renderer>().sortingOrder = 0;

        }
    }

    /// <summary>
     /// 製作亂數，儲存於RandomArr[_Randnum]，
     /// 有SequArr[_Randnum]可使用
     /// </summary>
     /// <param name="_Qusnum"></param>
     /// <param name="_Randnum"></param>
     void makeRandomArr(int _Qusnum, int _Randnum)
    {
        
            RandomArr = new int[_Randnum];
            Qusnum = _Qusnum;
            Randnum = _Randnum;
            for (int i = 0; i < SequArr.Length; i++)
            {
                SequArr[i] = i + 1;
            }
            int end = SequArr.Length - 1;
            for (int i = 0; i < _Randnum; i++)
            {
                int num = Random.Range(0, end + 1);
                RandomArr[i] = SequArr[num];
                SequArr[num] = SequArr[end];
                end--;
            }
    }

    /// <summary>
    /// 將random到的題目顯示，把題目紀錄至QusArr[]，呼叫delay()
    /// </summary>
    public void ShowRandom()
    {
            x = 0;
            foreach (int Arr in RandomArr)
            {
                //Debug.Log(Arr);
                //紀錄題目至QusArr
                Debug.Log(Arr + " and " + x);
                QusArr[x] = objlist[Arr - 1].name;
                Debug.Log(QusArr[x]);
                x++;
                if (x >= Qusnum) x = 0;
                //顯示題目為black
                


                objlist[Arr - 1].GetComponent<SpriteRenderer>().sprite = Resources.Load("Hoertt/black", typeof(Sprite)) as Sprite;
            }
            
        DelayStatus = true;
        //DelayRecover();

    }
    //呼叫後2秒執行下列程序
    /// <summary>
    /// delay n 秒，將題目顯示回去，讓物件ship開始移動，變可點擊
    /// </summary>
    /// <returns></returns>
    IEnumerator delay()
    {
        while (true)
        {
            if (DelayStatus)
            {
                DelayStatus = false;
                float WaitSecond = Ship_select._floatFieldLeft[Level];
                //等2秒後
                yield return new WaitForSeconds(WaitSecond);
                //ShowQusStatus = false;
                //將顯示為black的題目返回至white

                //Debug.Log(Arr);


                try
                {
                    foreach (int Arr in RandomArr)
                    {
                        objlist[Arr - 1].GetComponent<SpriteRenderer>().sprite = Resources.Load("Hoertt/white", typeof(Sprite)) as Sprite;
                        //Debug.Log(Arr-1);
                    }
                }
                catch (System.Exception e)
                {

                }

                makeRandomArr(3, 3);

                for (int x = 0; x < 3; x++)
                {
                    TweenPosition.Begin(objlist[x], 0.5f, Postable[0][RandomArr[x]-1]);
                }
                yield return new WaitForSeconds(1f);
                //click.clickopen = true;

                //開始移動
                //StartMove.StartObjMove();


                ObjMoveStu = true;
                    click.clickopen = true;
               
            
    }
            yield return null;
}
        //StopCoroutine(delay());
    }
    //呼叫delay函數
    //void DelayRecover()
    //{
        
    //    //ShowQusStatus = true;
    //}

    /// <summary>
    /// 物件點擊後回傳name，紀錄至AnsArr[]，且使物件顯示
    /// </summary>
    /// <param name="name"></param>
    public static void AnsList(string name)
    {
        Debug.Log("i:"+i+" QusArr:"+QusArr.Length+"   "+Qusnum);
        if (name != null)
        {

            if (i >= QusArr.Length)
            {
                i = 0;
                AnsCover = true;
            }
            //若點選超過3個覆蓋最舊的
            if (AnsCover)
            {
                GameObject.Find(AnsArr[i]).GetComponent<SpriteRenderer>().sprite = Resources.Load("Hoertt/white", typeof(Sprite)) as Sprite;
            }
            //紀錄
            AnsArr[i] = name;
            GameObject.Find(AnsArr[i]).GetComponent<SpriteRenderer>().sprite = Resources.Load("Hoertt/chack", typeof(Sprite)) as Sprite;
            i++;

        }
    }

    public void showorderlist()
    {
        for (int i = 0; i < AnsArr.Length; i++)
        {
            Debug.Log(AnsArr[i]);
        }
    }

    public  void CompareArr()
    {
        for (int c = 0; c < QusArr.Length; c++)
        {
            for (int c2 = 0; c2 < QusArr.Length; c2++)
            {
                //比對相等回傳0
                if (string.Compare(QusArr[c], AnsArr[c2]) != 0)
                {
                    //比對失敗(有錯誤)
                    Anstatus = false;
                }

            }
        }
        Debug.Log("Anstatus: "+Anstatus);
        for (int i = 0; i < AnsArr.Length; i++)
        {
            GameObject.Find(AnsArr[i]).GetComponent<SpriteRenderer>().sprite = Resources.Load("Hoertt/white", typeof(Sprite)) as Sprite;
        }
        if (Anstatus)
        {
            GameObject.Find("ScoreText").GetComponent<Text>().text = "Great";
            Level++;
            Initialization();

        }
        else
        {
            GameObject.Find("ScoreText").GetComponent<Text>().text = "NOPE";
            Anstatus = true;
            Initialization(); 
        }

        Debug.Log("(Level: " + Level);
    }
    public static void WantCompare()
    {
        Ship_1 a = new Ship_1();
        a.CompareArr();
    }


    void ImportTable()
    {
        Postable.Add(new List<Vector3>());
        Postable[0].Add(new Vector3(-1.51f,0.49f,0.4f));
        Postable[0].Add(new Vector3(-0.2f,-0.5f,0.41f));
        Postable[0].Add(new Vector3(1.07f,-1.18f, 0.4f));


        Postable.Add(new List<Vector3>());  
        for(int c=0; c<Ship_select._Qusnum[Level];c++)
        {
            Debug.Log("c2:" + c);
            Postable[1].Add(objlist[c].transform.localPosition);
        }
    }
}

