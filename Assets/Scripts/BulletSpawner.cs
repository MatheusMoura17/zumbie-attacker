using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour {

	public GameObject bulletPrefab;

	private List<GameObject> bullets=new List<GameObject>();
	
	public void Spawn(){
		foreach (GameObject gm in bullets) {
			if (!gm.activeSelf) {
				gm.transform.position = transform.position;
				gm.transform.eulerAngles = transform.eulerAngles;
				gm.SetActive (true);
				return;
			}
		}

		GameObject tempGameObject =Instantiate(bulletPrefab,transform.position,transform.rotation);
		bullets.Add (tempGameObject);
	}
}
