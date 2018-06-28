using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prensa : MonoBehaviour {

	private Animator anim;
	[SerializeField]
	private float startTime;

	void Start() {

		anim = GetComponentInParent<Animator> ();
		StartCoroutine (StartAnimator ());
	}

	IEnumerator StartAnimator() {

		yield return new WaitForSeconds (startTime);
		anim.enabled = true;
	}

	void OnCollisionEnter2D (Collision2D other)
	{
		if (other.gameObject.tag == "Player") {

			PlayerController.Damage (1, false);
		}
	}
}
