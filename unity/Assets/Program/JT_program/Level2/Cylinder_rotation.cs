using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cylinder_rotation : MonoBehaviour {

	public int speed =5;


	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.Rotate (Vector3.up, Time.deltaTime*speed*10);
	}
}
