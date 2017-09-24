using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheats : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.R))
			Time.timeScale = 0.2f;
		if (Input.GetKeyDown (KeyCode.T))
			Time.timeScale = 1f;
	}
}
