using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterBase : MonoBehaviour {

	[Header("Movement")]
	public float moveSpeed=6;
	public float gravity=20;
	public float jumpSpeed=8;
	private Vector3 moveDirection=Vector3.zero;
	private bool running;
	private bool attacking;

	[Header("Tools")]
	public int life=100;
	public Animator myAnimator;
	public float attackRatio=0.2f;
	private float attackTimer;
	public CharacterController characterController;

	protected CharacterStatus currentStatus;

	protected abstract void OnHitEnter();
	protected abstract void CalculateInputs ();

	void Update(){
		CalculateInputs ();
		moveDirection.y -= gravity*Time.deltaTime;
		characterController.Move (moveDirection*Time.deltaTime);
		ApplyAnimations ();
	}
		

	private void ApplyAnimations(){
		myAnimator.SetBool ("jumping", !characterController.isGrounded);
		myAnimator.SetBool ("running", running);
		myAnimator.SetBool ("attacking", attacking);
	}

	public void Attack(){
		if (attackTimer <= Time.time) {
			attackTimer += attackRatio;
			attacking = true;
			Invoke("DisableAttack",attackRatio);
		}
	}

	private void DisableAttack(){
		if (attackTimer <= Time.time) {
			attacking = false;
		}
	}

	public void Jump(){
		if (characterController.isGrounded)
			moveDirection.y = jumpSpeed;
	}

	public void Move(float direction){
		if (direction > Constants.INPUT_ERROR_MARGIN) {
			if (transform.eulerAngles.y != 0) {
				Vector3 euler = transform.eulerAngles;
				euler.y = 0;
				transform.eulerAngles = euler;
			}
			running = true;
			moveDirection.x = direction * moveSpeed;
		} else if (direction < -Constants.INPUT_ERROR_MARGIN) {
			if (transform.eulerAngles.y != 180) {
				Vector3 euler = transform.eulerAngles;
				euler.y = 180;
				transform.eulerAngles = euler;
			}
			running = true;
			moveDirection.x = direction * moveSpeed;
		} else {
			running = false;
			moveDirection.x = 0;
		}

		
	}


}
