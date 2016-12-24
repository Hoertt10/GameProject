using UnityEngine;
using System.Collections;

public class level3test2 : MonoBehaviour {

	public float speed = 30;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		gameObject.transform.Rotate (Vector3.up, -Time.deltaTime * speed*10);
	}
}
