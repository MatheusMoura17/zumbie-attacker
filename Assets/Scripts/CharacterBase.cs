using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterBase : MonoBehaviour {

	[Header("Movement")]
	public float moveSpeed=6;
	public float gravity=20;
	public float jumpSpeed=8;
	private Vector3 moveDirection=Vector3.zero;

	[Header("Tools")]
	public int life=100;
	public Animator myAnimator;
	public CharacterController characterController;

	protected CharacterStatus currentStatus;

	void Update(){
		CalculateInputs ();
		moveDirection.y -= gravity*Time.deltaTime;
		characterController.Move (moveDirection*Time.deltaTime);
	}

	public void Jump(){
		if (characterController.isGrounded)
			moveDirection.y = jumpSpeed;
	}

	public void Move(float direction){
		moveDirection.x = direction*moveSpeed;
	}

	protected abstract void OnHitEnter();
	protected abstract void CalculateInputs ();

}
