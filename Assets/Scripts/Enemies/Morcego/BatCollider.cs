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

			if (PlayerController.vulneravel) { //Se o Player estiver vulneravel:

				PlayerController.recebeDano = true;
				PlayerController.vulneravel = false; //Deixa o jogador invulnerável (Tempo limitado).
				PlayerController.vidas -= enemy.danoBase; //Subtrai o dano da habilidade/jogador;
			}

            PlayerController.recebeKnockBack = true;
            enemy.vidas -= 1;
		}
	}
}
