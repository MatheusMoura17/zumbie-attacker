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
	public bool killed;

	[Header("Weapon")]
	public GameObject muzzleEffect;

	public Transform lifeBar;

	[Header("Tools")]
	public int life=100;
	public Animator myAnimator;
	public float attackRatio=0.2f;
	private float attackTimer;
	public CharacterController characterController;
	public float timeToDisable=2;

	protected abstract void OnShoot();
	protected abstract void OnHitEnter(int damage);
	protected abstract void CalculateInputs ();
	protected abstract void OnStart ();

	void Start(){
		UpdateLifeBar ();
		OnStart ();
	}

	void Update(){
		CalculateInputs ();
		if (attackTimer <= Time.time) {
			attacking = false;
			muzzleEffect.SetActive (false);
		}
		moveDirection.y -= gravity*Time.deltaTime;
		characterController.Move (moveDirection*Time.deltaTime);
		ApplyAnimations ();
	}
		
	public void Hit(int damage){
		life -= damage;
		if (life <= 0) {
			life = 0;
			killed = true;
			Invoke ("Disable", timeToDisable);
		}
		UpdateLifeBar ();
		OnHitEnter (damage);
	}

	protected void UpdateLifeBar(){
		Vector2 scale=lifeBar.localScale;
		scale.x=(float)life/100f;
		lifeBar.localScale = scale;
	}

	public void Disable(){
		gameObject.SetActive (false);
	}

	private void ApplyAnimations(){
		myAnimator.SetBool ("jumping", !characterController.isGrounded);
		myAnimator.SetBool ("running", running);
		myAnimator.SetBool ("attacking", attacking);
		myAnimator.SetBool ("dead", killed);
	}

	public void Attack(){
		if (attackTimer <= Time.time) {
			attackTimer = Time.time+attackRatio;
			attacking = true;
			muzzleEffect.SetActive (true);
			OnShoot ();
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

	void OnTriggerEnter(Collider collider){
		if (collider.tag == "Atackable") {
			IAtackable item = collider.GetComponent<IAtackable> ();
			Hit (item.GetDamage ());
			item.Disable ();
		}
	}

}
