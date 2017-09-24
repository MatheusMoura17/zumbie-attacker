using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : CharacterBase {

	public int inputNumber=0;
	public BulletSpawner bulletSpawner;

	protected override void CalculateInputs(){
		Move (Input.GetAxis ("Horizontal" + inputNumber));
		if (Input.GetAxis ("Jump" + inputNumber) > Constants.INPUT_ERROR_MARGIN)
			Jump ();

		if (Input.GetAxis ("Attack" + inputNumber) > Constants.INPUT_ERROR_MARGIN)
			Attack ();
	}

	protected override void OnShoot(){
		bulletSpawner.Spawn ();
	}

	protected override void OnHitEnter (int damage)
	{
		
	}
}
