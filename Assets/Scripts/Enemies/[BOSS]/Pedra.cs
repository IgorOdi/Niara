using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pedra : MonoBehaviour {

	Rigidbody2D rb;
	[SerializeField]
	EnemyBehaviour enemy;
	public int dano;

	void Start() {

		rb = GetComponent<Rigidbody2D> ();
		enemy = GameObject.Find("Z2F2 Boss").GetComponent<EnemyBehaviour> ();
		rb.AddForce (new Vector2 (300 * enemy.lado, 1000));
		dano = 1;
		Destroy (gameObject, 6f);
	}

	void OnTriggerEnter2D (Collider2D other) {

		if (other.gameObject.tag == "Weapon") {

			float distancia = other.transform.position.x - transform.position.x;
			int lado = distancia > 0 ? -1 : 1;
			rb.AddForce (new Vector2 (1500 * lado, 300));
		}

		if (other.gameObject.tag == "Player") { //com o Player,

			PlayerController.Damage (dano, false);

		}
	}
}
