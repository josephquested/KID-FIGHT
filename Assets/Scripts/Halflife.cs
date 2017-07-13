using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Halflife : MonoBehaviour {

	// SYSTEM //

	public float lifetime;

	void Start ()
	{
		Destroy(gameObject, lifetime);
	}
}
