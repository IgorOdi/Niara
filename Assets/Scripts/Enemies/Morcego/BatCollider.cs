using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatCollider : MonoBehaviour {

	EnemyBehaviour enemy;

	void Awake() {

		enemy = (EnemyBehaviour)GetComponentInParent (typeof(EnemyBehaviour));
	}

	void OnCollisionEnter2D(Collision2D other) {

		if (other.gameObject.tag == "Player" && enemy.vivo) { //com o Player,

			PlayerController.Damage (enemy.danoBase, false);

            enemy.vidas -= 1;
		}
	}
}
