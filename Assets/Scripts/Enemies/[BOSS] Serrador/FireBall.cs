﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour {

	private int danoArremessavel;

	private Rigidbody2D rb;

	[SerializeField]
	private GameObject fogo;

	void Start() {

		rb = GetComponent<Rigidbody2D> ();
		rb.AddForce(new Vector2(0, 2400));

		danoArremessavel = 1;

		Destroy (gameObject, 5f);
	}

	void OnTriggerEnter2D(Collider2D other) {

		if (other.gameObject.tag == "Colisor Fogo") {

			Vector2 spawnPos = transform.position;
			spawnPos.x = transform.position.x - 1;
			spawnPos.y = transform.position.y + 0.75f;

			Instantiate (fogo, spawnPos, transform.rotation);
			Destroy (gameObject, 0f);
		}

		if (other.gameObject.tag == "Player") { //com o Player,

			PlayerController.Damage (danoArremessavel, false);


			Destroy (gameObject, 0.4f);
		}
	}
}
