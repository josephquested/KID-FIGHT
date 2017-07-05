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
		UpdateMovement();
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
		rb.AddForce(force * speed);
	}

	// FACING //

	bool facingRight;

	void UpdateFacing ()
	{
		if (!Input.GetButton("LockOn"))
		{
			if (horizontal > 0.01) facingRight = true;
			if (horizontal < -0.01) facingRight = false;
		}
	}

	// CROUCH //

	bool crouching;

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
			col.size = new Vector2(0.375f, 0.5f);
		}
		else
		{
			col.offset = new Vector2(0, 0);
			col.size = new Vector2(0.375f, 1);
		}
	}

	// ANIMATOR //

	void UpdateAnimator ()
	{
		anim.SetBool("Moving", horizontal != 0);
		anim.SetBool("Crouching", crouching);
		sprite.flipX = !facingRight;
	}
}
