using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ship_1 : MonoBehaviour {
    static int  Level = 0 /*,GameMode=3*/;
    public static int GoodAns=0, BadAns = 0;
    public static List<GameObject> objlist = new List<GameObject>();
    public static List<int> Quiznum = new List<int>();
    public static List<int> TKrandomArr = new List<int>();
    public static List<int> NQusArr = new List<int>();
    public static List<List <Vector3>> Postable = new List<List <Vector3>>();
    static List<Vector3> TopPostable = new List<Vector3>();
    public static List<int> QuizMoveSet = new List<int>();
    public static List<int> ShipMoveSet = new List<int>();
    float timer = 0;

    int turn = 1;//繞園 左右state
    static bool WaveStat = true;

    //GameObject ShipRotaPoint = GameObject.Find("ShipRotaPoint");

    private static int i = 0, x = 0;
    public static bool ObjMoveStu=false;
    static bool AnsCover = false;//是否需要覆蓋舊答案
    public static int[] RandomArr,SequArr;
    static List<string> AnsArr = new List<string>();//儲存答題答案
    public static string[] QusArr;//儲存題目
    float ShipMoveSp;

    public static bool WantDelayStu = false;
    public static float WantDelayTime = 1;
    private GameObject[] LoadAnimal;

    public static List<CreatAnimal> AnimalList = new List<CreatAnimal>();//動態產生後的所有陣列


    public static Vector3[] AppearPos = new Vector3[]
    {
        new Vector3(0f,0f)   ,new Vector3(250f,0f)   , new Vector3(500f,0f)    , new Vector3(750f,0f),
        new Vector3(0f,-250f),new Vector3(250f,-250f), new Vector3(500f,-250f), new Vector3(750f,-250f),
        new Vector3(0f,-500f),new Vector3(250f,-500f), new Vector3(500f,-500f), new Vector3(750f,-500f),
        new Vector3(0f,-750f),new Vector3(250f,-750f), new Vector3(500f,-750f), new Vector3(750f,-750f),
    };
    

    static int Score = 0;
    static bool Anstatus = true,DelayStatus=false;
    // Use this for initialization

    public class CreatAnimal
    {
        public static int P_Pos = 0;

        public GameObject GetAnimal { get; private set; }

        public CreatAnimal (GameObject GO)
        {
            GO = Instantiate(GO, parent: GameObject.Find("point").transform)as GameObject;

            

            GO.transform.localScale = Vector3.one;

            GO.name = "Player("+P_Pos +")";
            
            switch (QuizMoveSet[Level])
            {
                case 1:
                case 2:
                    GO.transform.position = Postable[Level][P_Pos++];
                    break;
                default:
                    TweenPosition.Begin(GO, 0.01f, AppearPos[P_Pos++]);
                    //GO.transform.position = AppearPos[P_Pos++];
                    //TweenPosition.Begin(GO, 1f, Postable[Level][P_Pos++]);
                    if (P_Pos >= 16)
                    {
                        P_Pos = 0;
                    }
                    break;
            }
           
            //  AnimalList.Add(GO);

            objlist.Add(GO);
            
            GetAnimal=GO;

        }

    }




    void Start () {

        //初始化

        timer = 0;
        ImportTable();


        Initialization();

        StartCoroutine(Wantdelay());
        StartCoroutine(delay());


    }

    //傳入變數SP,Qusnum,Rannum
    public virtual void Initialization()
    {
        TKrandomArr  = new List<int>();
        NQusArr = new List<int>();
        AnsCover = false;
        Anstatus = true;
        i = 0;
        ObjMoveStu = false;
        click.clickopen = false;
        WaveStat = true;
        foreach (GameObject item in objlist)
        {
            Destroy(item);
        }

        objlist = new List<GameObject>();

        LoadAnimal = Resources.LoadAll<GameObject>("Hoertt/AnimalGroup");

        //Postable[Level].ForEach(GO => AnimalList.Add(new CreatAnimal(LoadAnimal[Random.Range(0,8)])));
        int tmpnum;
        #region switch
        switch (QuizMoveSet[Level])
        {
            case 1:
                for (int i = 0; i < Quiznum[Level]; i++)
                {
                    AnimalList.Add(new CreatAnimal(LoadAnimal[Random.Range(0, 8)]));
                }
                break;
            case 2:
                tmpnum = Random.Range(0, 8);
                for (int i = 0; i < Quiznum[Level]; i++)
                {
                    AnimalList.Add(new CreatAnimal(LoadAnimal[tmpnum]));
                }
                break;
            default:
                tmpnum =  Random.Range(0, 8);
                for (int i = 0; i < 16; i++)
                {
                    AnimalList.Add(new CreatAnimal(LoadAnimal[tmpnum]));
                }
                break;
        }
        #endregion
        //製造物件NM 範圍N M=3 MAX=16
        iarray nm = new n_m_array(Quiznum[Level], 3, 16);
        //將題目陣列N從入TKrandomArr
        foreach (var num in nm.n()){ TKrandomArr.Add(num); }
        //將混亂題目存入NQusArr
        foreach (var i in nm.n_m()){ NQusArr.Add(i); }

        //將混亂題目移動至下方
        for (int x = 0; x < NQusArr.Count; x++){TweenPosition.Begin(objlist[NQusArr[x]], 0.1f, Postable[0][x]);}
                

        ShipMoveSp = Ship_select._floatFieldRight[Level];
        //ShipMoveSp = 50;;
        //Debug.Log("Postable"+Postable.Count);


        //makeRandomArr(3,1);
        QusArr = new string[Quiznum[Level]];
        AnsArr.Clear();
        //AnsArr = new string[Quiznum[Level]];
        SequArr = new int[objlist.Count];
        //makeRandomArr( Quiznum[Level]);
        //takerandom(0, 16, Quiznum[Level]);


        ShowRandom();
        DelayStatus = true;

    }

    // Update is called once per frame
    void Update () {
        timer += Time.deltaTime;
        GameObject.Find("TimeText").GetComponent<Text>().text = "Time:" + timer.ToString("0.00");

        if (ObjMoveStu==true)
        {
            switch (ShipMoveSet[Level])
            {
                case 1:
                    break;

                case 2:
                    if (GameObject.Find("ship").transform.localRotation.eulerAngles.z + 35 > 355 + 35)
                    {
                        turn = 1;
                    }
                    else if (GameObject.Find("ship").transform.localRotation.eulerAngles.z >= 65)
                    {
                        turn = -1;
                    }
                    GameObject.Find("ship").transform.RotateAround(GameObject.Find("ShipRotaPoint").transform.position, Vector3.forward, turn * ShipMoveSp * Time.deltaTime);
                    break;
                case 3:
                    //Debug.Log(timer);
                    GameObject.Find("point").transform.Translate(Mathf.Cos(timer*0.9f) * 0.01f * Vector3.right);
                    GameObject.Find("point").transform.Translate(Mathf.Sin(timer*0.4f) * 0.01f * Vector3.up);
                    break;
                case 4:
                    if (!WaveStat)
                    {
                        GameObject.Find("ship").transform.Translate(Mathf.Cos(timer) * 0.01f * Vector3.right);
                        GameObject.Find("ship").transform.Translate(Mathf.Sin(timer * 1.9f) * 0.01f * Vector3.up);
                    }
                    break;
                default:
                    break;
            }
            

        }
    }               
    
 
    /// <summary>
    /// 製作亂數list TKramdonArr，由min~max中取count個存入
    /// </summary>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <param name="count"></param>
    void takerandom(int min,int max,int count)
    {
        TKrandomArr = new List<int>();
        int tmp;
        for (int i = 0; i < count; i++)
        {
            tmp = Random.Range(min, max);
            while (TKrandomArr.Contains(tmp))
            {
                tmp = Random.Range(min, max);
            }
            TKrandomArr.Add(tmp);
        }
    }
    /// <summary>
     /// 製作亂數，儲存於RandomArr[_Randnum]，
     /// 產生0~(Randnum-1)亂數儲存於陣列中
     /// </summary>
     /// <param name="_Randnum"></param>
     void makeRandomArr(int _Randnum)
    {
            RandomArr = new int[_Randnum];
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
            foreach (int Arr in TKrandomArr)
            {
                //Debug.Log(Arr);
                //紀錄題目至QusArr
                //Debug.Log(Arr + " and " + x);
                QusArr[x] = objlist[Arr].name;
               //Debug.Log(QusArr[x]);
                x++;
            //顯示題目為black
            //    objlist[Arr - 1].GetComponent<SpriteRenderer>().sprite = Resources.Load("Hoertt/black", typeof(Sprite)) as Sprite;
            objlist[Arr].GetComponent<UI2DSprite>().color = new Color(1, 1, 0, 1);
        }
            

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
                yield return new WaitForSeconds(1f);
                foreach (int Arr in TKrandomArr) { objlist[Arr].GetComponent<UI2DSprite>().color = new Color(1, 1, 1, 1); }
                //ShowQusStatus = false;
                //將顯示為black的題目返回至white

                //Debug.Log(Arr);


                //yield return  null;

                //makeRandomArr(objlist.Count);

                //Debug.Log("obj:" + objlist.Count);
                //Debug.Log( RandomArr.Count());
                switch (QuizMoveSet[Level])
                {
                    case 1:
                        for (int x = 0; x < NQusArr.Count; x++)
                        {
                            //Debug.Log("x:"+x);
                            TweenPosition.Begin(objlist[NQusArr[x]], 0.5f, AppearPos[NQusArr[x]]);
                        }
                        break;
                    case 2:
                        for (int x = 0; x < NQusArr.Count; x++)
                        {
                            //Debug.Log("x:"+x);

                            TweenPosition.Begin(objlist[NQusArr[x]], 0.5f, AppearPos[NQusArr[x]]);
                          //  yield return new WaitForSeconds(0.7f);
                        }
                        break;
                    case 3:

                        for (int x = 0; x < NQusArr.Count; x++)
                        {
                            //Debug.Log("x:"+x);
                            TweenPosition.Begin(objlist[NQusArr[x]], 0.5f, Postable[0][TKrandomArr[x]]);
                        }
                        break;
                    //case 4:
                    //    //移動船到頂點pingpong回來
                    //    iTween.MoveTo(GameObject.Find("ship"), iTween.Hash("position", new Vector3(-1.69f, 1.32f, 0f), "loopType", iTween.LoopType.pingPong, "delay", 0.1f, "time", 1f, "easeType", iTween.EaseType.easeInOutBack, "oncomplete", "shipWaveEnd", "oncompletetarget", GameObject.Find("ship")));
                    //    yield return new WaitForSeconds(2.2f);
                    //    iTween.Stop(GameObject.Find("ship"));
                    //    //移動所有點至TOP
                    //    for (int x = 0; x < objlist.Count; x++)
                    //        TweenPosition.Begin(objlist[x], 0.5f, TopPostable[x]);
                    //    yield return new WaitForSeconds(1.7f);
                    //    //shine
                    //    foreach (string name in QusArr)
                    //    {
                    //        //GameObject.Find(name).GetComponent<SpriteRenderer>().sprite = Resources.Load("Hoertt/black", typeof(Sprite)) as Sprite;
                    //        GameObject.Find(name).GetComponent<UI2DSprite>().color = new Color(0, 1, 1, 1);
                    //    }
                    //    yield return new WaitForSeconds(1f);
                    //    foreach (int Arr in TKrandomArr)
                    //    {
                    //        //objlist[Arr - 1].GetComponent<SpriteRenderer>().sprite = Resources.Load("Hoertt/white", typeof(Sprite)) as Sprite;
                    //        GameObject.Find(name).GetComponent<UI2DSprite>().color = new Color(1, 1, 1, 1);
                    //    }
                    //    //移動至船 onebyone
                    //    for (int x = 0; x < objlist.Count; x++)
                    //    {
                    //    TweenPosition.Begin(objlist[x], 0.5f, Postable[Level][TKrandomArr[x]]);
                    //    yield return new WaitForSeconds(0.7f);

                    //    }

                    //    break;
                    default:
                       
                        //移動至船 onebyone
                        

                        break;
                }

                yield return new WaitForSeconds(1f);

                ObjMoveStu = true;
                click.clickopen = true;


            }
            yield return null;
}
        //StopCoroutine(delay());
    }

    void IWantDelay(float DelayTime)
    {
        WantDelayTime = DelayTime;
        WantDelayStu = true;
    }
    IEnumerator Wantdelay()
    {
        while (true)
        {
            if (WantDelayStu == true)
            { 
                yield return new WaitForSeconds(WantDelayTime);
                WantDelayStu = false;
            }
            yield return null;
        }
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
        //Debug.Log("i:"+i+" QusArr:"+QusArr.Length+"   "+Qusnum);
        if (name != null)
        {

            if (i >= QusArr.Length)
            {
                i = 0;
                AnsCover = true;
            }
            //若點選超過n個覆蓋最舊的
            if (AnsCover)
            {
                //GameObject.Find(AnsArr[i]).GetComponent<SpriteRenderer>().sprite = Resources.Load("Hoertt/white", typeof(Sprite)) as Sprite;
                GameObject.Find(AnsArr[i]).GetComponent<UI2DSprite>().color = new Color(1, 1, 1, 1);
                AnsArr[i] = name;
            }
            else
            {
                AnsArr.Add(name);
            }
            //紀錄
            //AnsArr[i] = name;
            //GameObject.Find(AnsArr[i]).GetComponent<SpriteRenderer>().sprite = Resources.Load("Hoertt/chack", typeof(Sprite)) as Sprite;
            GameObject.Find(name).GetComponent<UI2DSprite>().color = new Color(0, 1, 1, 1);
            i++;

        }
    }

    //public void showorderlist()
    //{
    //    for (int i = 0; i < AnsArr.Length; i++)
    //    {
    //        Debug.Log(AnsArr[i]);
    //    }
    //}

    public  void CompareArr()
    {
        //比對答案
        foreach (string name in QusArr)
        {
            if(!AnsArr.Contains(name)) Anstatus = false; 
        }

        //for (int c = 0; c < QusArr.Length; c++)
        //{
        //    for (int c2 = 0; c2 < QusArr.Length; c2++)
        //    {
        //        //比對相等回傳0
        //        if (string.Compare(QusArr[c], AnsArr[c2]) != 0)
        //        {
        //            //比對失敗(有錯誤)
        //            Anstatus = false;
        //        }

        //    }
        //}
  //      Debug.Log("Anstatus: "+Anstatus);
        for (int i = 0; i < AnsArr.Count; i++)
        {
            Debug.Log(AnsArr[i]);
            GameObject.Find(AnsArr[i]).GetComponent<UI2DSprite>().color = new Color(1, 1, 1, 1);
            //GameObject.Find(AnsArr[i]).GetComponent<SpriteRenderer>().sprite = Resources.Load("Hoertt/white", typeof(Sprite)) as Sprite;
        }
        if (Anstatus)
        {
            GoodAns+=1;
            GameObject.Find("ScoreText").GetComponent<Text>().text = "Great O:"+GoodAns+" X:"+BadAns;
            Level++;
            Initialization();

        }
        else
        {
            BadAns += 1;
            GameObject.Find("ScoreText").GetComponent<Text>().text = "NOPE O:" + GoodAns + " X:" + BadAns;
            Initialization(); 
        }

        //Debug.Log("(Level: " + Level);
    }
    public static void WantCompare()
    {
        Ship_1 a = new Ship_1();
        a.CompareArr();
    }



    void ImportTable()
    {
        //題目有幾個
        Quiznum.Add(1);
        Quiznum.Add(1);
        Quiznum.Add(2);
        Quiznum.Add(2);
        Quiznum.Add(2);
        Quiznum.Add(2);
        Quiznum.Add(2);
        Quiznum.Add(2);
        Quiznum.Add(2);
        Quiznum.Add(3);
        //船移動方式(1.不動 2.轉圓 3.晃動 4.遇浪)
        ShipMoveSet.Add(1);
        ShipMoveSet.Add(1);
        ShipMoveSet.Add(1);
        ShipMoveSet.Add(2);
        ShipMoveSet.Add(2);
        ShipMoveSet.Add(2);
        ShipMoveSet.Add(3);
        ShipMoveSet.Add(3);
        ShipMoveSet.Add(3);
        ShipMoveSet.Add(4);
        //題目移動方式(1.一起移動(不變位置) 2.個別移動 3.一起移動(位置調換) 4.遇浪特殊移動)
        QuizMoveSet.Add(4);
        QuizMoveSet.Add(4);
        QuizMoveSet.Add(4);
        QuizMoveSet.Add(4);
        QuizMoveSet.Add(4);
        QuizMoveSet.Add(4);
        QuizMoveSet.Add(4);
        QuizMoveSet.Add(3);
        QuizMoveSet.Add(3);
        QuizMoveSet.Add(4);


        Postable.Add(new List<Vector3>());
        Postable[0].Add(new Vector3(-324f, -1016f, 0.4f));
        Postable[0].Add(new Vector3(0f, -1016f, 0.4f));
        Postable[0].Add(new Vector3(311f, -1016f, 0.4f));
        Postable[0].Add(new Vector3(668f, -1016f, 0.4f));
        Postable[0].Add(new Vector3(972f, -1016f, 0.4f));
        Postable[0].Add(new Vector3(1258f, -1016f, 0.4f));

        Postable.Add(new List<Vector3>());
        Postable[1].Add(new Vector3(-2.6f, 0.08f, 0.4f));
        Postable[1].Add(new Vector3(-0.2f, 0.08f, 0.41f));
        Postable[1].Add(new Vector3(2.07f, 0.08f, 0.4f));

        Postable.Add(new List<Vector3>());
        Postable[2].Add(new Vector3(-3f, 0.2f, 0.4f));
        Postable[2].Add(new Vector3(-1f, 0.2f, 0.4f));
        Postable[2].Add(new Vector3(1f, 0.2f, 0.4f));
        Postable[2].Add(new Vector3(3f, 0.2f, 0.4f));

        Postable.Add(new List<Vector3>());
        Postable[3].Add(new Vector3(-3f, 0.2f, 0.4f));
        Postable[3].Add(new Vector3(-1f, 0.2f, 0.4f));
        Postable[3].Add(new Vector3(1f, 0.2f, 0.4f));
        Postable[3].Add(new Vector3(3f, 0.2f, 0.4f));

        Postable.Add(new List<Vector3>());
        Postable[4].Add(new Vector3(-3f, 0.2f, 0.4f));
        Postable[4].Add(new Vector3(-1f, 0.2f, 0.4f));
        Postable[4].Add(new Vector3(1f, 0.2f, 0.4f));
        Postable[4].Add(new Vector3(3f, 0.2f, 0.4f));

        Postable.Add(new List<Vector3>());
        Postable[5].Add(new Vector3(-1f, 1.5f, 0.4f));
        Postable[5].Add(new Vector3(2f, 1.5f, 0.4f));
        Postable[5].Add(new Vector3(2, -1.5f, 0.4f));
        Postable[5].Add(new Vector3(-1f, -1.5f, 0.4f));

        Postable.Add(new List<Vector3>());
        Postable[6].Add(new Vector3(-1f, 1.5f, 0.4f));
        Postable[6].Add(new Vector3(2f, 1.5f, 0.4f));
        Postable[6].Add(new Vector3(2f, -1.5f, 0.4f));
        Postable[6].Add(new Vector3(-1f, -1.5f, 0.4f));

        Postable.Add(new List<Vector3>());
        Postable[7].Add(new Vector3(-1.9f, 1.52f, 0.4f));
        Postable[7].Add(new Vector3(1.74f, 1.52f, 0.4f));
        Postable[7].Add(new Vector3(-0f, 0f, 0.4f));
        Postable[7].Add(new Vector3(-1.9f, -1.5f, 0.4f));
        Postable[7].Add(new Vector3(1.74f, -1.5f, 0.4f));
        
        Postable.Add(new List<Vector3>());
        Postable[8].Add(new Vector3(-1f, 1.3f, 0.4f));
        Postable[8].Add(new Vector3(2f, 1.3f, 0.4f));
        Postable[8].Add(new Vector3(-3f, -1.5f, 0.4f));
        Postable[8].Add(new Vector3(0.6f, -1.5f, 0.4f));
        Postable[8].Add(new Vector3(4f, -1.5f, 0.4f));

        Postable.Add(new List<Vector3>());
        Postable[9].Add(new Vector3(-3.9f, -1f, 0.4f));
        Postable[9].Add(new Vector3(-1.35f, -1f, 0.4f));
        Postable[9].Add(new Vector3(1.1f, -1f, 0.4f));
        Postable[9].Add(new Vector3(3.65f, -1f, 0.4f));
        Postable[9].Add(new Vector3(6.2f, -1f, 0.4f));
        Postable[9].Add(new Vector3(-1.8f, 1.6f, 0.4f));
        Postable[9].Add(new Vector3(0.75f, 1.6f, 0.4f));
        Postable[9].Add(new Vector3(3.5f, 1.6f, 0.4f));
        Postable[9].Add(new Vector3(-1.25f, 3.9f, 0.4f));
        Postable[9].Add(new Vector3(1.77f, 3.9f, 0.4f));
        Postable[9].Add(new Vector3(-6f, 2f, 0.4f));
        Postable[9].Add(new Vector3(7.9f, 1.9f, 0.4f));



        TopPostable.Add(new Vector3(-3.9f, 4f, 0.4f));
        TopPostable.Add(new Vector3(-1.35f, 4f, 0.4f));
        TopPostable.Add(new Vector3(1.1f, 4f, 0.4f));
        TopPostable.Add(new Vector3(3.65f, 4f, 0.4f));
        TopPostable.Add(new Vector3(6.2f, 4f, 0.4f));
        TopPostable.Add(new Vector3(-1.8f, 5.6f, 0.4f));
        TopPostable.Add(new Vector3(0.75f, 5.6f, 0.4f));
        TopPostable.Add(new Vector3(3.5f, 5.6f, 0.4f));
        TopPostable.Add(new Vector3(-1.25f, 8f, 0.4f));
        TopPostable.Add(new Vector3(1.77f, 8f, 0.4f));
        TopPostable.Add(new Vector3(-6f, 6f, 0.4f));
        TopPostable.Add(new Vector3(7.9f, 6f, 0.4f));
        //for(int c=0; c<Ship_select._Qusnum[Level];c++)
        //{
        //    Debug.Log("c2:" + c);
        //    Postable[1].Add(objlist[c].transform.localPosition);
        //}
    }
   
}

