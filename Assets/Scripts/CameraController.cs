using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public GameObject[] players;
	public float interpolation=1;
	private float startTime;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (players [0].activeSelf && players [1].activeSelf)
			FollowTwoPlayers ();
		else
			FollowOnePlayer ();
	}

	void FollowOnePlayer(){
		int activePlayer = players [0].activeSelf ? 0 : 1;

		Vector3 to = players [activePlayer].transform.position;
		to.y = transform.position.y;
		to.z = transform.position.z;

		transform.position = Vector3.Lerp (transform.position, to, interpolation*Time.deltaTime);
	}

	void FollowTwoPlayers(){
		float distanceBetweenPlayer = Vector2.Distance (players [0].transform.position, players [1].transform.position)/2;
		int backPlayer = players [0].transform.position.x < players [1].transform.position.x ? 0 : 1;

		Vector3 to = players [backPlayer].transform.position;
		to.x += distanceBetweenPlayer;
		to.y = transform.position.y;
		to.z = transform.position.z;

		transform.position = Vector3.Lerp (transform.position, to, interpolation*Time.deltaTime);
	}
}
