using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraYControl : MonoBehaviour {

	public float yOffset;
	public bool lerpar;

	void LateUpdate() {

		if (lerpar) {

			CamFollow.offsetY = Mathf.Lerp (CamFollow.offsetY, 0, 0.05f);
		} 
	}

	void OnTriggerStay2D(Collider2D other) {

		if (other.gameObject.tag == "Player") {

			CamFollow.offsetY = Mathf.Lerp (CamFollow.offsetY, yOffset, 0.05f);
			CamFollow.lockCamera = false;
		}
	}

	void OnTriggerExit2D(Collider2D other) {

		if (other.gameObject.tag == "Player") {

			lerpar = true;
			CamFollow.lockCamera = true;
			StartCoroutine (stopLerping ());

		}
	}

	IEnumerator stopLerping() {

		yield return new WaitForSeconds (0.5f);
		lerpar = false;
	}
}
