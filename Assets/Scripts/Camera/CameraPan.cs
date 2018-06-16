using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPan : MonoBehaviour {

	[SerializeField]
	private byte index;

	void OnTriggerEnter2D (Collider2D other)
	{
		
		if (other.gameObject.tag == "Player") {
			Debug.Log(gameObject.name);

			if (gameObject.name == "Arvore") GameManager.arvoreParam.setValue(1);

			CamFollow.cameraShouldPan = true;
			CamFollow.cameraExitPan = false;
			CamFollow.panIndex = index;
		}
	}

	void OnTriggerExit2D (Collider2D other)
	{
		if (other.gameObject.tag == "Player") {

			if (gameObject.name == "Arvore") GameManager.arvoreParam.setValue(0);

			CamFollow.cameraShouldPan = false;
			CamFollow.cameraExitPan = true;
		}
	}

}
