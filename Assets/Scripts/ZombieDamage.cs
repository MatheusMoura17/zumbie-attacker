using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieDamage :  MonoBehaviour, IAtackable {

	public float lifeTime=0.1f;
	public int maxDamage=15;

	// Use this for initialization
	void Start () {
		Invoke ("Disable", lifeTime);
	}

	private void OnEnable(){
		Invoke ("Disable", lifeTime);
	}

	public int GetDamage(){
		return Random.Range(1,maxDamage);
	}

	public void Disable(){
		gameObject.SetActive (false);
	}
}
