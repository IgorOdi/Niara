using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tremor : MonoBehaviour {

	private int danoTremor;

	void Awake() {

		danoTremor = 1;
	}

	void OnTriggerStay2D(Collider2D other) {

		if ((other.gameObject.tag == "Player" && Gorila.tremor) && (PlayerController.vulneravel && PlayerController.grounded)) {

			PlayerController.Damage (danoTremor, true);

		}
	}
}
