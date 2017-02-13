using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjMove : MonoBehaviour
{
    //[SerializeField] private List<GameObject> objlist = new List<GameObject>();
    private float timer; //for record time
    private  Vector3 pos; //initial position
    private int count = 0;
    // Use this for initialization
    void Start()
    {

        timer = 0;
        pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //record time
        timer += 0.03f;
      //  timer += Time.deltaTime;

        Vector3 rPos = pos;
        rPos.x = rPos.x + Mathf.Cos(timer) * 50f;
        rPos.y = rPos.y - Mathf.Abs(Mathf.Sin(timer)) * 70f;
        
       
      //  Debug.Log(timer);
        //Debug.Log(Mathf.Cos(timer));
        //update position
        transform.position = rPos;

        if((int)(Mathf.Cos(timer)*100)==0)
        {
             count += 1;
            if(count %3==0)
            {
                count = 0;
                click.clickopen = true;

                gameObject.GetComponent<ObjMove>().enabled = false;
                }
        }
        /*
        if (timer  >=1)
        {
            if ((timer - timer % 0.01) % 4.50 == 0)
            {
                gameObject.GetComponent<ObjMove>().enabled = false;
                timer += 0.1f;
            }
        }
        */
        
    }
  
}
