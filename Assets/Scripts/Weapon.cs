using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

	// SYSTEM //

	PlayerController player;

	void Start ()
	{
		player = GetComponent<PlayerController>();
	}

	void Update ()
	{
		UpdateProjectileSpawn();
	}

	// PROJECTILE SPAWN //

	public Transform projSpawn;
	public Vector2 spawnRightPos;
	public Vector2 spawnLeftPos;

	void UpdateProjectileSpawn ()
	{
		if (player.facingRight) projSpawn.localPosition = spawnRightPos;
		else { projSpawn.localPosition = spawnLeftPos; }
	}

	// FIRE //

	public GameObject projPrefab;

	public void ReceiveFire ()
	{
		GameObject proj = Instantiate(projPrefab, projSpawn.position, projSpawn.rotation);
		proj.GetComponent<SpriteRenderer>().flipX = !player.facingRight;
		// proj.GetComponent<Rigidbody2D>().AddForce(GetForceForProj());
	}
}
