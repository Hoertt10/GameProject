using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Level2_display : MonoBehaviour
{

	[Header ("List")]
	public List<GameObject> ball_sequence = new List<GameObject> ();
	public List<GameObject> ball = new List<GameObject> ();

	[Header ("Material")]
	[SerializeField]private Material Red;

	public static Material TiffanyBlue_static;
	[SerializeField]private Material TiffanyBlue;

	public static Material Gray_static;
	[SerializeField]private Material Gray;

	public static int ball_Arr = 0;

	public GameObject Cylinder;


	void Awake ()
	{
		Gray_static = Gray;

		TiffanyBlue_static = TiffanyBlue;
	}


	public class ClickEvent
	{
		public void Action (GameObject obj)
		{
			Debug.Log (obj.name);

			if (obj.GetComponent<Renderer> ().material.color == TiffanyBlue_static.color)
				obj.GetComponent<Renderer> ().material = Gray_static;
			else
				obj.GetComponent<Renderer> ().material = TiffanyBlue_static;
		}
	}



	// Use this for initialization
	void Start ()
	{
		makeRandomArr ();
		InvokeRepeating ("ShowLight", 1f, 1f);

		UIEventListener.Get (ball [0]).onClick = new ClickEvent ().Action;
		UIEventListener.Get (ball [1]).onClick = new ClickEvent ().Action;
		UIEventListener.Get (ball [2]).onClick = new ClickEvent ().Action;
		UIEventListener.Get (ball [3]).onClick = new ClickEvent ().Action;
		UIEventListener.Get (ball [4]).onClick = new ClickEvent ().Action;
		UIEventListener.Get (ball [5]).onClick = new ClickEvent ().Action;
		UIEventListener.Get (ball [6]).onClick = new ClickEvent ().Action;
		UIEventListener.Get (ball [7]).onClick = new ClickEvent ().Action;
	}
	
	// Update is called once per frame
	void Update ()
	{
		gameObject.transform.LookAt (Cylinder.transform.position,Vector3.forward);
	}



	void makeRandomArr ()
	{
		for (int i = 0; i < 8; i++) {
			int num = Random.Range (0, ball_sequence.Count);
			ball.Add (ball_sequence [num]);
			ball_sequence [num] = ball_sequence [ball_sequence.Count - 1];
			ball_sequence.RemoveAt (ball_sequence.Count - 1);
		}
	}

	public void showarr ()
	{
		foreach (GameObject obj in ball) {
			Debug.Log (obj.name);
		}
	}

	public void ShowLight ()
	{
		if (ball_Arr == 8) {
			CancelInvoke ("ShowLight");
			ball [7].GetComponent<Renderer> ().material = TiffanyBlue;
		} else {
			if (ball_Arr != 0) {
				ball [ball_Arr].GetComponent<Renderer> ().material = Red;
				ball [ball_Arr - 1].GetComponent<Renderer> ().material = TiffanyBlue;
			} else {
				ball [ball_Arr].GetComponent<Renderer> ().material = Red;
			}
			ball_Arr++;
		}
	}


	public void CameraMove(){
		
//		gameObject.GetComponent<TweenPosition>().enabled = true;
//		gameObject.GetComponent<TweenOrthoSize> ().enabled = true;
		gameObject.GetComponent<UIPlayTween> ().Play (true);


	}
}
