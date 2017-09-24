using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterBase : MonoBehaviour {

	#region Vars
	[Header("Movement")]
	public float moveSpeed=6;
	public float gravity=20;
	public float jumpSpeed=8;
	private Vector3 moveDirection=Vector3.zero;

	/// <summary>
	/// The character is running.
	/// </summary>
	private bool running;
	/// <summary>
	/// The character is attacking.
	/// </summary>
	private bool attacking;
	/// <summary>
	/// The character is killed.
	/// </summary>
	public bool killed;

	[Header("Components")]
	public GameObject muzzleEffect;
	public Transform lifeBar;
	public Animator myAnimator;
	public CharacterController characterController;

	[Header("Other Tools")]
	public int life=100;
	public float attackRatio=0.2f;
	public float timeToDisable=2;
	private float attackTimer;
	#endregion

	#region Voids to childreen class 
	protected abstract void OnShoot();
	protected abstract void OnHitEnter(int damage);
	protected abstract void CalculateInputs ();
	protected abstract void OnStart ();
	#endregion

	#region Life and Attack functions

	/// <summary>
	/// Called when the player takes damage. Determines if the player died
	/// </summary>
	/// <param name="damage">Damage.</param>
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

	/// <summary>
	/// Updates the life bar in scene.
	/// </summary>
	protected void UpdateLifeBar(){
		Vector2 scale=lifeBar.localScale;
		scale.x=(float)life/100f;
		lifeBar.localScale = scale;
	}

	/// <summary>
	/// Disable the player in scene
	/// </summary>
	public void Disable(){
		gameObject.SetActive (false);
	}

	/// <summary>
	/// Makes true attacking when the attackTimer hits the attackRatio
	/// </summary>
	public void Attack(){
		if (attackTimer <= Time.time) {
			attackTimer = Time.time+attackRatio;
			attacking = true;
			muzzleEffect.SetActive (true);
			OnShoot ();
		}
	}

	#endregion

	#region Animations and Sounds
	/// <summary>
	/// Apply base animations
	/// </summary>
	private void ApplyAnimations(){
		myAnimator.SetBool ("jumping", !characterController.isGrounded);
		myAnimator.SetBool ("running", running);
		myAnimator.SetBool ("attacking", attacking);
		myAnimator.SetBool ("dead", killed);
	}
	#endregion

	#region Movementation functions

	/// <summary>
	/// Makes the player jump if on the floor
	/// </summary>
	public void Jump(){
		if (characterController.isGrounded)
			moveDirection.y = jumpSpeed;
	}

	/// <summary>
	/// Move the character in horizontal position
	/// </summary>
	/// <param name="direction">Horizontal direction.</param>
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

	#endregion

	#region Unity functions
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

	void OnTriggerEnter(Collider collider){
		if (collider.tag == "Atackable") {
			IAtackable item = collider.GetComponent<IAtackable> ();
			Hit (item.GetDamage ());
			item.Disable ();
		}
	}
	#endregion

}
