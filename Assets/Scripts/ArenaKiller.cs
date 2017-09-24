using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaKiller : MonoBehaviour, IAtackable {

	public int damage=5000;
	public bool isFatallity = true;


	public int GetDamage(){
		return damage;
	}

	public bool IsFatallity(){
		return isFatallity;
	}

	public void Disable(){
		//gameObject.SetActive (false);
	}
}
