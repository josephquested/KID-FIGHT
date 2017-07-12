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
	public Vector2 spawnRightPos;
	public Vector2 spawnLeftPos;

	void UpdateProjectileSpawn ()
	{
		if (player.facingRight) projectileSpawn.localPosition = spawnRightPos;
		else { projectileSpawn.localPosition = spawnLeftPos; }
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
