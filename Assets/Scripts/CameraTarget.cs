using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTarget : MonoBehaviour {

	// SYSTEM //

	PlayerController player;

	public Vector2 rightPosition;
	public Vector2 leftPosition;

	void Start ()
	{
		player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
	}

	void Update ()
	{
		if (player.facingRight) transform.localPosition = rightPosition;
		else { transform.localPosition = leftPosition; }
	}
}
