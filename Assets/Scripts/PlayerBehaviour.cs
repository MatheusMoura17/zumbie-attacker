﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : CharacterBase {

	public int inputNumber=0;

	protected override void CalculateInputs(){
		Move (Input.GetAxis ("Horizontal" + inputNumber));
		if (Input.GetAxis ("Vertical" + inputNumber) > Constants.INPUT_ERROR_MARGIN)
			Jump ();
	}

	protected override void OnHitEnter ()
	{
		
	}
}