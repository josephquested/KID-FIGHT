﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystem2D : MonoBehaviour {

	// SYSTEM //

	void Start ()
	{
		if (singlePop)
		{
			Pop();
		}
	}

	void Update ()
	{
		if (canPop && !singlePop)
		{
			Pop();
		}
	}

	// PARTICLES //

	public GameObject prefab;
	public Sprite[] sprites;

	public float popDelay;

	public bool singlePop;

	public int minParticles;
	public int maxParticles;

	public float minForce;
	public float maxForce;

	bool canPop = true;

	IEnumerator PopRoutine ()
	{
		canPop = false;
		int quantity = Random.Range(minParticles, maxParticles);
		while (quantity > 0)
		{
			InstantiateParticle();
			quantity--;
		}
		yield return new WaitForSeconds(popDelay);
		canPop = true;
	}

	void Pop ()
	{
		canPop = false;
		int quantity = Random.Range(minParticles, maxParticles);
		while (quantity > 0)
		{
			InstantiateParticle();
			quantity--;
		}
		canPop = true;
	}

	void InstantiateParticle ()
	{
		Vector2 force = new Vector2(Random.Range(minForce, maxForce), Random.Range(minForce, maxForce));
		GameObject _prefab = Instantiate(prefab, transform.position, transform.rotation);
		_prefab.transform.Rotate(0, 0, RandomRotation());
		_prefab.GetComponent<Rigidbody2D>().AddForce(force);
		if (sprites.Length > 0) RandomiseSprite(_prefab);
	}

	float RandomRotation ()
	{
		float[] rotations = new float[] {0, 90, 180, 270};
		return rotations[Random.Range(0, 4)];
	}

	void RandomiseSprite (GameObject _prefab)
	{
		_prefab.GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Length)];
	}
}
