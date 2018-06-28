using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterShot : ProjectileBase {

	private int danoBase = 1;

	void OnTriggerEnter2D(Collider2D other) {

		if (other.gameObject.tag == "Player") { //com o Player,

			PlayerController.Damage (danoBase, false);

			Destructor ();
		}

		if (other.gameObject.tag == "Chão") {

			Destructor ();
		}
	}
}
