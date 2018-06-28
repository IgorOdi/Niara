using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArremessavelMonkey : ProjectileBase {

	private int danoArremessavel;
	private float distanceMultiplier;
	[SerializeField]
	private Transform player;
	[SerializeField]
	private Rigidbody2D rb;

	void Awake() {

		danoArremessavel = 1;

		player = GameObject.Find ("Player").GetComponent<Transform>();

		distanceMultiplier = player.transform.position.x > transform.position.x ? 1 : -1;
		//distanceMultiplier = (transform.position.x - player.position.x) / 10;

		rb = GetComponent<Rigidbody2D> ();
		rb.AddForce (new Vector2(-450 * distanceMultiplier, 400));

		Destroy (gameObject, 2f);
	}

	void OnTriggerEnter2D(Collider2D other) {

		if (other.gameObject.tag == "Chão") {

			Destructor ();
		}

		if (other.gameObject.tag == "Player") { //com o Player,

			PlayerController.Damage (danoArremessavel, false);


			Destructor ();
		}
	}
}
