using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : ProjectileBase {

	private int dano;
	private float speed;

	void Start() {

		dano = 1;
		speed = 0.3f;

		Destroy (gameObject, 4f);
	}

	void Update() {

		transform.Translate(-Vector2.right * speed);
	}

	void OnTriggerEnter2D(Collider2D other) {

		Destructor ();

		if (other.gameObject.tag == "Player") { //com o Player,

			PlayerController.Damage (dano, false);
		}
	}
}
