using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour {

	
	public BoxCollider2D[] colisor;

	public Animator an;

	private string somPlatQuebra = "event:/PlatQuebra/PlatQuebrando";


	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") { //Se a plataforma colidir com o player:
			StartCoroutine ("cair"); //Inicia uma contagem de nome "cair"
			an.SetBool("Entrou",true);
			FMODUnity.RuntimeManager.PlayOneShot(somPlatQuebra, transform.position);
		}
	}

	IEnumerator cair() {
		yield return new WaitForSeconds (1.5f); //Começa a contar até 1,5 segundos, depois desse tempo:

		for (int i = 0; i < colisor.Length; i++) {
			colisor[i].enabled = false; //Desativa a plataforma (cair).
			an.SetBool("Caiu",true);
		}

		StartCoroutine("voltar");	

	}

	IEnumerator voltar() {
		yield return new WaitForSeconds (2f); //Começa a contar até 1,5 segundos, depois desse tempo:

		for (int i = 0; i < colisor.Length; i++) {
			colisor[i].enabled = true; //Desativa a plataforma (cair).
		}

		an.SetBool("Caiu",false);
		an.SetBool("Entrou",false);

	}
}
