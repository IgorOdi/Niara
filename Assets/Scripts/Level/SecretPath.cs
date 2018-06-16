using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretPath : MonoBehaviour {

	Renderer rr;

	void Start() {

		rr = GetComponent<MeshRenderer> ();
	}

	void OnTriggerEnter2D(Collider2D other) {
		
		if (other.gameObject.tag == "Player" || other.gameObject.tag == "Inimigo") {
			
			rr.enabled = false;
		}
	}

	void OnTriggerExit2D(Collider2D other) {

		if (other.gameObject.tag == "Player" || other.gameObject.tag == "Inimigo") {

			rr.enabled = true;
		}
	}
}
