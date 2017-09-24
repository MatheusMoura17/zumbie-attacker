using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelController : MonoBehaviour {

	#region Vars
	public static LevelController instance;

	public PlayerBehaviour[] players;
	public EnemyBehaviour[] enemies;
	public CameraController cameraController;

	public Text scoreText;
	public GameObject gameOver;
	public GameObject gameWon;

	private int score=0;

	#endregion

	void Awake(){
		instance = this;
	}

	// Use this for initialization
	void Start () {
		foreach (EnemyBehaviour enemy in enemies){
			enemy.players = new GameObject[players.Length];
			cameraController.players = new GameObject[players.Length];
			for (int i = 0; i < players.Length; i++) {
				enemy.players [i] = players [i].gameObject;
				cameraController.players [i] = players [i].gameObject;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (!players [0].gameObject.activeSelf && !players [1].gameObject.activeSelf)
			gameOver.SetActive (true);
	}

	public void ReviveAt(Vector3 position){
		if (!players [0].gameObject.activeSelf) {
			players [0].transform.position = position;
			players [0].gameObject.SetActive (true);
			players [0].Reset ();
		}
		if (!players [1].gameObject.activeSelf) {
			players [1].transform.position = position;
			players [1].gameObject.SetActive (true);
			players [1].Reset ();
		}
	}

	public void SetGameWon(){
		gameWon.SetActive (true);
	}

	public void RestartLevel(){
		SceneManager.LoadScene ("Level1");
	}


	/// <summary>
	/// Adds the value to scoreboard and update in screen.
	/// </summary>
	/// <param name="score">Value to update.</param>
	public void AddScore(int value){
		score += value;
		scoreText.text = score.ToString();
	}
}
