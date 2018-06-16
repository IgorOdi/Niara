using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocha : MonoBehaviour {

	int danoBase; 
	Rigidbody2D rb;

	void Start () {

		danoBase = 1;
		rb = GetComponent<Rigidbody2D> ();
	}

	void OnCollisionEnter2D(Collision2D other) {

		if (rb.velocity.magnitude > 8) {
			
			if (other.gameObject.tag == "Player") { //com o Player,

				if (PlayerController.vulneravel) { //Se o Player estiver vulneravel:

					PlayerController.recebeDano = true;
					PlayerController.vulneravel = false; //Deixa o jogador invulnerável (Tempo limitado).
					PlayerController.vidas -= danoBase; //Subtrai o dano da habilidade/jogador;
				}

				PlayerController.recebeKnockBack = true;
			}

		} else if (rb.velocity.magnitude > 2) {
			
			if (other.gameObject.tag == "Inimigo") {

				var enemy = other.gameObject.GetComponent<EnemyBehaviour> ();

				if (enemy.vulneravel && enemy.vivo) {

						enemy.recebeuDano = true; //Avisa que o inimigo recebeu dano;
					enemy.vidas -= danoBase; //Perde vida igual ao dano do Player.
				}
			}
		}
	}
}
