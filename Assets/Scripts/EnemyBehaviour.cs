using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : CharacterBase
{

	#region Vars
	[HideInInspector]
	public GameObject[] players;

	[Header("Components")]
	public GameObject damageObject;
	public CharacterBase currentPlayer;

	[Header("IA Tools")]
	private int targetPlayer = -1;
	public float distanceToFollow = 5;
	public float distanceToAttack = 1;
	public float lookOutWaitTime=2;
	private float waitCounter = 0;
	private bool wait;

	[Header("Round Tools")]
	public int scoreValue=50;
	#endregion

	#region CharacterBase inherited functions

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
		
		if (targetPlayer == -1) {
			Move (0);
			return;
		}

		if (!players [targetPlayer].activeSelf) {
			Move (0);
			return;
		}
		else if (!currentPlayer)
			currentPlayer = players [targetPlayer].GetComponent <CharacterBase>();

		if (currentPlayer.killed) {
			Move (0);
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
						Move (0);
						wait = false;
						waitCounter = 0;
					}
				}
			}
		} else {
			targetPlayer = -1;
		}
	}

	protected override void OnShoot ()
	{
		damageObject.SetActive (true);
	}

	protected override void OnHitEnter (int damage)
	{
	}

	protected override void OnKilled(){
		LevelController.instance.AddScore (scoreValue);
	}

	#endregion

	#region Player utilities
	/// <summary>
	/// Gets the player direction in scene
	/// </summary>
	/// <returns>The player direction.</returns>
	/// <param name="playerIndex">Player index.</param>
	private float GetPlayerDirection (int playerIndex)
	{
		return transform.position.x - players [playerIndex].transform.position.x > 0 ? -1 : 1;
	}

	/// <summary>
	/// Returns the index of the closest player that is alive 
	/// </summary>
	/// <returns>The player index aproximated.</returns>
	private int GetPlayerIndexAproximated ()
	{
		for (int i = 0; i < players.Length; i++)
			if (players [i].activeSelf && Vector2.Distance (players [i].transform.position, transform.position) <= distanceToFollow)
				return i;
		return -1;
	}

	#endregion

}
