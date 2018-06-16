using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RochaB : MonoBehaviour {

	int danoBase;
	private bool ativa;
	private float distancia;
	Transform player;
	Rigidbody2D rb;

	void Start () {

		danoBase = 1;
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ();
		rb = GetComponent<Rigidbody2D> ();
		rb.gravityScale = 0;
	}

	void Update() {

		distancia = transform.position.x - player.transform.position.x;

		ativa = distancia < 3 ? true : false;

		if (ativa) {

			rb.gravityScale = 1;
		} else {

			rb.gravityScale = 0;
		}
	}

	void OnCollisionEnter2D(Collision2D other) {

		if (rb.velocity.magnitude > 3) {
			
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
		}

		Destroy (gameObject, 0f);
	}
}
