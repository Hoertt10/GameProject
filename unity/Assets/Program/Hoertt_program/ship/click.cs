using System.Collections;
using System.Collections.Specialized;
using UnityEngine;

public class click : MonoBehaviour
{
    public GameObject obj;

    public static bool clickopen = false;
    //int ID;
    new string name;



    // Use this for initialization
    void Start()
    {
        UIEventListener.Get(obj).onClick = clickenvent;
    }

    // Update is called once per frame
    void Update()
    {
     
    }
    void clickenvent (GameObject go)
    {
        Debug.Log(go.name);
        Debug.Log("click: " + clickopen);
        if (clickopen)
        {
            //show.StaPos(obj.transform.position);
            //obj.SetActive(false);
            //  obj.s
            //Debug.Log( "消失" );
            //ID = obj.GetInstanceID ();
            //Debug.Log(ID);
            //Debug.Log(name);
            name = go.name;
            Ship_1.AnsList(name);
        }
    }

    void OnMouseDown()
    {
        
    }
}