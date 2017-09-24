using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IAtackable {

	public float speed = 10;
	public float lifeTime=1;
	public int damage;
	public bool isFatallity = false;

	// Use this for initialization
	void Start () {
		Invoke ("Disable", lifeTime);
	}

	private void OnEnable(){
		Invoke ("Disable", lifeTime);
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (speed*Time.deltaTime,0,0);
	}

	public int GetDamage(){
		return damage;
	}

	public bool IsFatallity(){
		return isFatallity;
	}

	public void Disable(){
		gameObject.SetActive (false);
	}

}
