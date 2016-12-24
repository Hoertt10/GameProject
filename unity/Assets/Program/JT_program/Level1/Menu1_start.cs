using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Menu1_start : MonoBehaviour {

	[SerializeField]private GameObject obj;

	void Start () {
		UIEventListener.Get (obj).onClick = game;
	}

	void game(GameObject obj){
		//Application.LoadLevel("1");
		SceneManager.LoadScene (3);
	}
}
