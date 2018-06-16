using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chamas : MonoBehaviour {

	private int danoBase;

	void Awake() {
	
		danoBase = 1;
		Destroy (gameObject, 8f);
	}

	void OnTriggerEnter2D(Collider2D other) {

		if (other.gameObject.tag == "Player") { //com o Player,

			if (PlayerController.vulneravel) { //Se o Player estiver vulneravel:

				PlayerController.recebeKnockBack = true;
				PlayerController.recebeDano = true;
				PlayerController.vulneravel = false; //Deixa o jogador invulnerável (Tempo limitado).
				PlayerController.vidas -= danoBase; //Subtrai o dano da habilidade/jogador;
			} else {

				PlayerController.recebeKnockBack = true;
			}
		}
	}
}
