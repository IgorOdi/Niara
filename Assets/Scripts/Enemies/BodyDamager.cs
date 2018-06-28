using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyDamager : MonoBehaviour {

	EnemyBehaviour enemy;

	void Awake() {

		enemy = (EnemyBehaviour)GetComponentInParent (typeof(EnemyBehaviour));
	}

	void OnCollisionEnter2D(Collision2D other) {

		if (other.gameObject.tag == "Player" && enemy.vivo) { //com o Player,

			PlayerController.Damage (enemy.danoBase, false);
		}
	}
}
