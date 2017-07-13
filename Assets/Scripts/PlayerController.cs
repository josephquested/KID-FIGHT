using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	// SYSTEM //

	Rigidbody2D rb;
	Animator anim;
	CapsuleCollider2D col;
	SpriteRenderer sprite;

	void Start ()
	{
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		sprite = GetComponent<SpriteRenderer>();
		col = GetComponent<CapsuleCollider2D>();
	}

	void Update ()
	{
		UpdateSpeed();
		UpdateMovement();
		UpdateFire();
		UpdateJump();
		UpdateFacing();
		UpdateCrouch();
		UpdateAnimator();
	}

	// MOVEMENT //

	float horizontal;

	public float speed;

	void UpdateMovement ()
	{
		horizontal = Input.GetAxisRaw("Horizontal");
		Move();
	}

	void Move ()
	{
		Vector2 force = new Vector2(horizontal, 0);
		rb.AddForce(force * _speed);
	}

	// SPEED & FORCE //

	float _speed;
	float _jumpForce;

	void UpdateSpeed ()
	{
		_speed = speed;
		_jumpForce = jumpForce;

		if (firing) _speed = 0;

		if (crouching)
		{
			_speed *= 0.75f;
			_jumpForce *= 0.75f;
		}
	}

	// FACING //

	public bool facingRight;

	void UpdateFacing ()
	{
		if (!Input.GetButton("LockOn"))
		{
			if (horizontal > 0.01) facingRight = true;
			if (horizontal < -0.01) facingRight = false;
		}
	}

	// JUMP //

	public float jumpForce;

	void UpdateJump ()
	{
		if (Input.GetButtonDown("Jump") && grounded)
		{
			rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
		}
	}

	// WEAPON //

	public Weapon weapon;
	public float fireDuration;

	bool aiming;
	bool firing;

	void UpdateFire ()
	{
		if (!firing)
		{
			if (Input.GetButtonUp("Fire")) StartCoroutine(FireRoutine());
			aiming = Input.GetButton("Fire");
		}
	}

	IEnumerator FireRoutine ()
	{
		firing = true;
		weapon.ReceiveFire();
		yield return new WaitForSeconds(fireDuration);
		firing = false;
	}

	// GROUNDED //

	public bool grounded;

	void OnCollisionEnter2D (Collision2D collision)
	{
		if (collision.gameObject.layer == 8 && !grounded) grounded = true;
	}

	void OnCollisionExit2D (Collision2D collision)
	{
		if (collision.gameObject.layer == 8 && grounded) grounded = false;
	}

	// CROUCH //

	public bool crouching;

	void UpdateCrouch ()
	{
		crouching = Input.GetButton("Crouch");
		UpdateCrouchCollider();
	}

	void UpdateCrouchCollider ()
	{
		if (crouching)
		{
			col.offset = new Vector2(0, -0.25f);
			col.size = new Vector2(0.37f, 0.5f);
		}
		else
		{
			col.offset = new Vector2(0, 0);
			col.size = new Vector2(0.37f, 1);
		}
	}

	// ANIMATOR //

	public Animator armAnim;
	public SpriteRenderer armSprite;

	void UpdateAnimator ()
	{
		anim.SetBool("Moving", horizontal != 0);
		anim.SetBool("Crouching", crouching);
		anim.SetBool("Grounded", grounded);
		anim.SetBool("Aiming", aiming);
		anim.SetBool("Firing", firing);
		sprite.flipX = !facingRight;
		UpdateArmAnimator();
	}

	void UpdateArmAnimator ()
	{
		armAnim.SetBool("Moving", horizontal != 0);
		armAnim.SetBool("Crouching", crouching);
		armAnim.SetBool("Grounded", grounded);
		armAnim.SetBool("Aiming", aiming);
		armAnim.SetBool("Firing", firing);
		armSprite.flipX = !facingRight;
	}
}
