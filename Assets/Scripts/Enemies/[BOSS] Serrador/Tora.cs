using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tora : MonoBehaviour {

	private int lado;
	private int force;
	private int danoArremessavel;

	private Rigidbody2D rb;

	private Serrador serra;

	[SerializeField]
	private GameObject fogo;

	void Start() {

		serra = GameObject.Find ("Fornalheira").GetComponent<Serrador> ();

		rb = GetComponent<Rigidbody2D> ();
		rb.AddForce (new Vector2(-300 * serra.ladoTora + Random.Range(-150, 150), 800));

		danoArremessavel = 1;

		Destroy (gameObject, 3f);
	}

	void OnTriggerEnter2D(Collider2D other) {

		if (other.gameObject.tag == "Colisor Fogo") {

			Vector2 spawnPos = transform.position;
			spawnPos.x = transform.position.x - 1;
			spawnPos.y = transform.position.y + 0.75f;

			Instantiate (fogo, spawnPos, Quaternion.identity);
			Destroy (gameObject, 0f);
		}

		if (other.gameObject.tag == "Player") { //com o Player,

			PlayerController.Damage (danoArremessavel, false);


			Destroy (gameObject, 0.4f);
		}
	}

}
