﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level3test3 : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.Rotate (Vector3.up, Time.deltaTime*10);
	}
}