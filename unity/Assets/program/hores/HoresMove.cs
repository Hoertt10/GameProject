using UnityEngine;
using System.Collections;

public class HoresMove : MonoBehaviour {
     [SerializeField]private float TimerX,TimerY;
    private Vector3 pos;
	// Use this for initialization
	void Start () {
        pos = GameObject.Find("background2").transform.position - NewVector3(0,2f,0);
	}
	static Vector3 NewVector3(float x ,float y ,float z)
    {
        Vector3 test = new Vector3(x, y, z);
        return test;
    }
	// Update is called once per frame
	void Update () {
        //TimerX += 0.02f;
        TimerY += 0.05f;
        Vector3 rPos = pos;
        rPos.x = rPos.x + Mathf.Cos(TimerX) * 10f;
        rPos.y = rPos.y - (Mathf.Sin(TimerY)) * 1.5f;
        if (rPos.x>=GameObject.Find("background2").transform.position.x + 7)
        {
            transform.rotation=Quaternion.Euler(0f, 0f, 0f);
        }
        else if (rPos.x <= GameObject.Find("background2").transform.position.x - 7)
        {
            transform.rotation=Quaternion.Euler(0f,180f,0f);
            
            
        }
        //Debug.Log((int)(Mathf.Cos(TimerX) * 100));
        transform.position = rPos;
    }
}
