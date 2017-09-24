using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : CharacterBase
{

	public GameObject[] players;

	private int targetPlayer = -1;
	public float distanceToFollow = 5;
	public float distanceToAttack = 1;
	public float lookOutWaitTime=2;
	private float waitCounter = 0;

	private bool wait;

	void Start ()
	{
		players = GameObject.FindGameObjectsWithTag (Constants.PLAYER_TAG);
	}

	protected override void CalculateInputs ()
	{
		if (targetPlayer == -1)
			targetPlayer = GetPlayerIndexAproximated ();
		
		if (targetPlayer == -1)
			return;

		if (Vector2.Distance (players [targetPlayer].transform.position, transform.position) <= distanceToFollow) {

			if (Vector2.Distance (players [targetPlayer].transform.position, transform.position) <= distanceToAttack) {
				Move (0);
				Attack ();
				wait = true;
			} else {
				if (!wait) {
					Move (GetPlayerDirection (targetPlayer));
				} else {
					waitCounter += Time.deltaTime;
					if (waitCounter >= lookOutWaitTime) {
						wait = false;
						waitCounter = 0;
					}
				}
			}
		}
	}

	private float GetPlayerDirection (int playerIndex)
	{
		return transform.position.x - players [playerIndex].transform.position.x > 0 ? -1 : 1;
	}

	private int GetPlayerIndexAproximated ()
	{
		for (int i = 0; i < players.Length; i++)
			if (Vector2.Distance (players [i].transform.position, transform.position) <= distanceToFollow)
				return i;
		return -1;
	}

	protected override void OnShoot ()
	{
	}

	protected override void OnHitEnter ()
	{
	}

}
