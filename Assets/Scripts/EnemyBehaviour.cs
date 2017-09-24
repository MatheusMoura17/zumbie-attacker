using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : CharacterBase
{

	public GameObject[] players;

	public GameObject damageObject;
	public CharacterBase currentPlayer;

	private int targetPlayer = -1;
	public float distanceToFollow = 5;
	public float distanceToAttack = 1;
	public float lookOutWaitTime=2;
	private float waitCounter = 0;

	private bool wait;

	protected override void OnStart ()
	{
		players = GameObject.FindGameObjectsWithTag (Constants.PLAYER_TAG);
	}

	protected override void CalculateInputs ()
	{
		if (killed) {
			Move (0);
			damageObject.SetActive (false);
			return;
		}
		
		if (targetPlayer == -1)
			targetPlayer = GetPlayerIndexAproximated ();
		
		if (targetPlayer == -1)
			return;

		if (!players [targetPlayer].activeSelf)
			return;
		else if (!currentPlayer)
			currentPlayer = players [targetPlayer].GetComponent <CharacterBase>();

		if (currentPlayer.killed) {
			currentPlayer = null;
			targetPlayer = -1;
			return;
		}

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
			if (players [i].activeSelf && Vector2.Distance (players [i].transform.position, transform.position) <= distanceToFollow)
				return i;
		return -1;
	}

	protected override void OnShoot ()
	{
		damageObject.SetActive (true);
	}

	protected override void OnHitEnter (int damage)
	{
	}

}
