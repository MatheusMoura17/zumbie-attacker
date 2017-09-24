using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {

	#region Vars
	public PlayerBehaviour[] players;
	public EnemyBehaviour[] enemies;
	#endregion

	// Use this for initialization
	void Start () {
		foreach (EnemyBehaviour enemy in enemies)
			enemy.players = players;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	/// <summary>
	/// Adds the value to scoreboard and update in screen.
	/// </summary>
	/// <param name="score">Value to update.</param>
	public void AddScore(int value){

	}
}
