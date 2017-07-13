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

	public Transform projectileSpawn;

	public float spawnRightX;
	public float spawnLeftX;
	public float spawnStandY;
	public float spawnCrouchY;

	void UpdateProjectileSpawn ()
	{
		float xPos = spawnRightX;
		float yPos = spawnStandY;
		if (!player.facingRight) xPos = spawnLeftX;
		if (player.crouching) yPos = spawnCrouchY;
		projectileSpawn.localPosition = new Vector2(xPos, yPos);
	}

	// FIRE //

	public GameObject projectilePrefab;
	public float projectileSpeed;

	public void ReceiveFire ()
	{
		GameObject projectile = Instantiate(projectilePrefab, projectileSpawn.position, projectileSpawn.rotation);
		projectile.GetComponent<SpriteRenderer>().flipX = !player.facingRight;
		projectile.GetComponent<Rigidbody2D>().AddForce(ProjectileForce() * projectileSpeed);
	}

	Vector2 ProjectileForce ()
	{
		if (player.facingRight) return Vector2.right;
		else return Vector2.left;
	}
}
