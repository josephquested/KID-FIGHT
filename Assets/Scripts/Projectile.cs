using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	// SYSTEM //

	void Start ()
	{

	}

	void Update ()
	{

	}

	// ON COLLISION //

	public GameObject sparkParticles;

	void OnCollisionEnter2D (Collision2D col)
	{
		Instantiate(sparkParticles, transform.position, transform.rotation);
		Destroy(gameObject);
	}
}
