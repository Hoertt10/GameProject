using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Level1_Display_Hard : Level1_Display
{

	void Start ()
	{
		//初始化
		Initialization ();

		makeRandomArr (5);

		UFO_Random.ForEach (i => UIEventListener.Get (i).onClick = new ClickEvent ().Action);

		//持續比對答案
		StartCoroutine (AnswerCompare ());
	}

	public override void Initialization ()
	{
		Panel = GameObject.Find ("Panel");
		Bingo = Resources.Load<GameObject> ("JT/" + "checked-Bingo");
		Error = Resources.Load<GameObject> ("JT/" + "checked-Error");

		for (int i = 0; i < 5; i++) {
			UFO_sequence.Add (GameObject.Find ("UFO-" + i));
		}
	}


	protected override void MakeDifference ()
	{
		List<GameObject> check = UFO_Random.ToList ();
		makeRandomArr (5);
		if (UFO_Random.SequenceEqual (check))
			makeRandomArr (5);
	}

	void Update ()
	{
		
	}

}
