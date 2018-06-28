using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour {

	EnemyBehaviour enemy;

	void Awake() {

		enemy = (EnemyBehaviour)GetComponentInParent (typeof(EnemyBehaviour));
	}

	void OnTriggerEnter2D(Collider2D other) {

		if (other.gameObject.tag == "Player" && enemy.vivo) { //com o Player,

			PlayerController.Damage (enemy.danoBase, false);
		}
	}
}
