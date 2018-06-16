﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Napalm : ProjectileBase {

	private int dano = 1;

	[FMODUnity.EventRef]
	public string somDestruir = "event:/Destruir";

	void OnTriggerEnter2D(Collider2D other) {

		if (other.gameObject.tag == "Player") { //com o Player,

			if (PlayerController.vulneravel) { //Se o Player estiver vulneravel:

				PlayerController.recebeDano = true;
				PlayerController.vulneravel = false; //Deixa o jogador invulnerável (Tempo limitado).
				PlayerController.vidas -= dano; //Subtrai o dano da habilidade/jogador;
			}

			FMODUnity.RuntimeManager.PlayOneShot (somDestruir);

			PlayerController.recebeKnockBack = true;
			Destructor ();

		} else if (other.gameObject.tag == "Chão") {

			FMODUnity.RuntimeManager.PlayOneShot (somDestruir);

			Destroy (other.gameObject, 0f);
			Destructor ();
		}
	}
}

