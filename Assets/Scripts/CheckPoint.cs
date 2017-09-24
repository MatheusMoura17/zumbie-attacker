using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour {

	private bool used;
	public bool winnerCheckPoint = false;
	public GameObject indicator;

	void OnTriggerEnter(Collider collider){
		if (collider.tag == "Player" && !used) {
			used=true;
			if (winnerCheckPoint)
				LevelController.instance.SetGameWon ();
			else {
				indicator.SetActive (false);
				LevelController.instance.ReviveAt (transform.position);
			}
		}
	}
}
