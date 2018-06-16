using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vomito : MonoBehaviour {

	float speed;
	int dano;
	float lado;
	Transform player;
	Rigidbody2D rb;

	private void Start() {

		speed = 0.25f;
		dano = 1;
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ();
		rb = GetComponent<Rigidbody2D> ();

		if (transform.position.x > player.transform.position.x) {

			lado = -1;
		} else {
			lado = 1;
		}


			
		if (transform.position.y > player.transform.position.y + 2) {

			rb.gravityScale = 1;
		} else {

			rb.gravityScale = 0;
		}

		Destroy(gameObject, 5f);
	}

	void Update() {

		transform.Translate(new Vector3(speed*lado, 0));
	}

	void OnTriggerEnter2D(Collider2D other) {

		if (other.gameObject.tag == "Player") { //com o Player,

			if (PlayerController.vulneravel) { //Se o Player estiver vulneravel:

				PlayerController.recebeDano = true;
				PlayerController.vulneravel = false; //Deixa o jogador invulnerável (Tempo limitado).
				PlayerController.vidas -= dano; //Subtrai o dano da habilidade/jogador;
			}

			PlayerController.recebeKnockBack = true;
			Destroy (gameObject, 0f);
		} else if (other.gameObject.tag == "Chão") {

			Destroy (gameObject, 0f);
		}
	}
}
