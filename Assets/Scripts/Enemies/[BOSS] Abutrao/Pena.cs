using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pena : ProjectileBase {

	private int dano;
	private float speed;

	void Start() {

		dano = 1;
		speed = 0.2f;

		Destroy (gameObject, 2f);
	}

	void Update() {

		transform.Translate(-Vector2.right * speed);
	}

	void OnTriggerEnter2D(Collider2D other) {

		if (other.gameObject.tag == "Player") { //com o Player,

			if (PlayerController.vulneravel) { //Se o Player estiver vulneravel:

				PlayerController.recebeDano = true;
				PlayerController.vulneravel = false; //Deixa o jogador invulnerável (Tempo limitado).
				PlayerController.vidas -= dano; //Subtrai o dano da habilidade/jogador;
			}

			PlayerController.recebeKnockBack = true;
		}

		Destructor ();
	}
}
