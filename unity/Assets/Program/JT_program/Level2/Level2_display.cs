using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2_display : MonoBehaviour
{


	public List<GameObject> ball_sequence = new List<GameObject> ();
	private List<GameObject> ball = new List<GameObject> ();
	[SerializeField]private Material TiffanyBlue;
	[SerializeField]private Material Red;

	public static int ball_Arr = 0;


	// Use this for initialization
	void Start ()
	{
		makeRandomArr ();
		InvokeRepeating ("ShowLight", 1f, 1f);
	}
	
	// Update is called once per frame
	void Update ()
	{

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
}
