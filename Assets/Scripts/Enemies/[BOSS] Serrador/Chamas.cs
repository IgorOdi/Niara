using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chamas : MonoBehaviour {

	private int danoBase;

	void Awake() {
	
		danoBase = 1;
		Destroy (gameObject, 8f);
	}

	void OnTriggerEnter2D(Collider2D other) {

		if (other.gameObject.tag == "Player") { //com o Player,

			PlayerController.Damage (danoBase, false);

		}
	}
}
