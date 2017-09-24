using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {

	#region Vars
	public static LevelController instance;

	public PlayerBehaviour[] players;
	public EnemyBehaviour[] enemies;
	#endregion

	void Awake(){
		instance = this;
	}

	// Use this for initialization
	void Start () {
		foreach (EnemyBehaviour enemy in enemies){
			enemy.players = new GameObject[players.Length];
			for (int i = 0; i < players.Length; i++)
				enemy.players [i] = players [i].gameObject;
		}
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
