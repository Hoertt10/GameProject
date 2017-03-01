using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ship_1 : MonoBehaviour {
    [SerializeField]
    private List<GameObject> objlist = new List<GameObject>();
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

    static int Score = 0;
    static bool Anstatus = true,DelayStatus=false;
    // Use this for initialization
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
            objlist.Add(GameObject.Find("31"));
            objlist.Add(GameObject.Find("12"));
            objlist.Add(GameObject.Find("22"));
            objlist.Add(GameObject.Find("32"));
            objlist.Add(GameObject.Find("13"));
            objlist.Add(GameObject.Find("23"));
            objlist.Add(GameObject.Find("33"));

        //ShipMoveSp = Ship_select._floatFieldRight[Level];
        ShipMoveSp = 50;

            makeRandomArr(3,1);
        //makeRandomArr(Ship_select._Qusnum[Level], Ship_select._Rannum[Level]);
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
     void makeRandomArr(int _Qusnum, int _Randnum)
    {
            for (int i = 0; i <= 8; i++)
            {
                //objlist[i].SetActive(true);
                objlist[i].GetComponent<Renderer>().sortingOrder = 4;
            }
            for (int i = _Qusnum; i <= 8; i++)
            {
                //objlist[i].SetActive(false);
                objlist[i].GetComponent<Renderer>().sortingOrder = 0;

            }
            QusArr = new string[_Randnum];
            AnsArr = new string[_Randnum];
            SequArr = new int[_Qusnum];
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
                    }
                }
                catch (System.Exception e)
                {

                }


                //click.clickopen = true;

                //開始移動
                //StartMove.StartObjMove();

                try
                {
                    ObjMoveStu = true;
                    click.clickopen = true;
                }
                catch (System.Exception e)
                {

                }
            
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
    public static void AnsList(string name)
    {
        i = 0;
        if (name != null)
        {

            if (i >= Randnum)
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
        for (int c = 0; c < Randnum; c++)
        {
            for (int c2 = 0; c2 < Randnum; c2++)
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

        if (Anstatus)
        {
            GameObject.Find("ScoreText").GetComponent<Text>().text = "Great";
            Level++;
            Initialization();

        }
        else
        {
            GameObject.Find("ScoreText").GetComponent<Text>().text = "NOPE";
            Initialization();
            Anstatus = true;
        }

        Debug.Log("(Level: " + Level);
    }
    public static void WantCompare()
    {
        Ship_1 a = new Ship_1();
        a.CompareArr();
    }
}

