using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrilhaPoint : MonoBehaviour {

	private string checkSound = "event:/Checkpoint";

	private bool hasPlayed;

	Animator anim;

	void Start() {

		anim = GetComponent<Animator> ();
	}

	void OnTriggerEnter2D (Collider2D other)
	{

		if (other.gameObject.tag == "Player") {

			anim.SetBool ("Brilhar", true);
			if (!hasPlayed) {
				FMODUnity.RuntimeManager.PlayOneShot(checkSound, transform.position);
				hasPlayed = true;
			}
		}
	}
}
