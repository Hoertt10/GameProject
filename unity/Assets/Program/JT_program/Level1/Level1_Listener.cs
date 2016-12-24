using UnityEngine;
using System.Collections;

public class Level1_Listener : MonoBehaviour {

	public string UFO_name;
	public int UFO_ID;
	public bool op = true;


	void Start (){	
		UFO_name = gameObject.name;
	}
}
