using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterShot : ProjectileBase {

	private int danoBase = 1;

	void OnTriggerEnter2D(Collider2D other) {

		if (other.gameObject.tag == "Player") { //com o Player,

			if (PlayerController.vulneravel) { //Se o Player estiver vulneravel:

				PlayerController.recebeDano = true;
				PlayerController.vulneravel = false; //Deixa o jogador invulnerável (Tempo limitado).
				PlayerController.vidas -= danoBase; //Subtrai o dano da habilidade/jogador;
			}

			PlayerController.recebeKnockBack = true;
			Destructor ();
		}

		if (other.gameObject.tag == "Chão") {

			Destructor ();
		}
	}
}
