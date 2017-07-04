using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	// SYSTEM //

	Rigidbody2D rb;
	Animator anim;
	SpriteRenderer spriteRenderer;

	void Start ()
	{
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	void Update ()
	{
		UpdateMovement();
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

	// ANIMATOR //

	void UpdateAnimator ()
	{
		anim.SetBool("Moving", horizontal != 0);
		if (horizontal > 0.01) spriteRenderer.flipX = false;
		if (horizontal < -0.01) spriteRenderer.flipX = true;
	}
}
