using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere_rotation : MonoBehaviour {

	public int speed=5;

	public  static bool rotation = false;

	public Level2_display L2d = new Level2_display ();


	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(rotation){
			gameObject.transform.Rotate (Vector3.up, Time.deltaTime * speed * 10);
		}
	}


	public void click(){
		if (rotation) {
			rotation = false;
			CancelInvoke ("invoke_L2d");
		}
		else {
			rotation = true;
			Level2_display.ball_Arr = 0;
			InvokeRepeating ("invoke_L2d", 1f, 1f);

		}
	}
	void invoke_L2d(){
		L2d.ShowLight();
	}
}
