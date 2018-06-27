using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RochaB : MonoBehaviour {

	int danoBase;
	Vector3 startPosition;

	void Start () {

		danoBase = 1;
		transform.position += Vector3.up * Random.Range (-3f, 3f);
		startPosition = transform.position;
	}

	void Update() {

		transform.position += Vector3.down * 0.4f;

		if (transform.position.y < -15) {

			transform.position = startPosition + Vector3.up * Random.Range (-2f, 2f);
		}
	}

	void OnCollisionEnter2D(Collision2D other) {

		if (other.gameObject.tag == "Player") { //com o Player,

			if (PlayerController.vulneravel) { //Se o Player estiver vulneravel:

				PlayerController.recebeDano = true;
				PlayerController.vulneravel = false; //Deixa o jogador invulnerável (Tempo limitado).
				PlayerController.vidas -= danoBase; //Subtrai o dano da habilidade/jogador;
			}

			PlayerController.recebeKnockBack = true;
		} else if (other.gameObject.tag == "Inimigo") {

			var enemy = other.gameObject.GetComponent<EnemyBehaviour> ();

			if (enemy.vulneravel && enemy.vivo) {

				enemy.recebeuDano = true; //Avisa que o inimigo recebeu dano;
				enemy.vidas -= danoBase; //Perde vida igual ao dano do Player.
			}
		}

		transform.position = startPosition + Vector3.up * Random.Range (-2f, 2f);
	}
}
