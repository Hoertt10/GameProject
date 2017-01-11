using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trigger : MonoBehaviour {
	// Use this for initialization
	void Start () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        sound.playmusic("dog");
        Debug.Log("pass");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
     
    }
    
    // Update is called once per frame
    void Update () {
		
	}
}
