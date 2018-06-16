using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coletavel : MonoBehaviour {

	[FMODUnity.EventRef]
	public string somColetavel = "event:/Coletavel";

	public static int coletaveis;
	public static int pegouColetavel;

    void OnTriggerEnter2D(Collider2D other) {

		if (other.gameObject.tag == "Player") {

			FMODUnity.RuntimeManager.PlayOneShot(somColetavel, transform.position);
			pegouColetavel++;
			coletaveis++;
			Destroy (gameObject, 0f);
		}
	}
}