using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatGunShot : ProjectileBase {

	private float speed;
	private float timeControl;
	private int dano;

	private void Start() {

		speed = 0.3f;
		dano = 1;

		Destroy (gameObject, 5f);
	}

	void Update() {

		transform.Translate(new Vector3(-speed, 0));
	}

	void OnTriggerEnter2D(Collider2D other) {

		if (other.gameObject.tag == "Player") { //com o Player,

			if (PlayerController.vulneravel) { //Se o Player estiver vulneravel:

				PlayerController.recebeDano = true;
				PlayerController.vulneravel = false; //Deixa o jogador invulnerável (Tempo limitado).
				PlayerController.vidas -= dano; //Subtrai o dano da habilidade/jogador;
			}
			PlayerController.recebeKnockBack = true;
			Destructor ();

		} else if (other.gameObject.tag == "Chão") {

			Destructor ();
		}
	}
}
