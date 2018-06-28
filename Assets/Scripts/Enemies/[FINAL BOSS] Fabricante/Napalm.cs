using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Napalm : ProjectileBase {

	private int dano = 1;

	[FMODUnity.EventRef]
	public string somDestruir = "event:/Destruir";

	void OnTriggerEnter2D(Collider2D other) {

		if (other.gameObject.tag == "Player") { //com o Player,

			PlayerController.Damage (dano, false);

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

