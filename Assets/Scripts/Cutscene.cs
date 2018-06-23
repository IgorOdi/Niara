using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cutscene : MonoBehaviour {

	public float step;

	// Use this for initialization
	void Start () {
		step = 0;
	}
	
	// Update is called once per frame
	void Update ()
	{	
		if (Input.GetKeyDown (KeyCode.DownArrow) || Input.GetKeyDown (KeyCode.RightArrow)) {
			step -= 10;
		} else if (Input.GetKeyDown (KeyCode.UpArrow) || Input.GetKeyDown (KeyCode.LeftArrow)) {
			step += 10;
		}

		if (step >= 0) step = 0;

		else if (step <= -70) {
			 step = -60;
			 MainMenu.menuEv.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
			 MainMenu.menuEv.release();
	         GameManager.hasPlayed = false;
	         GameManager.instance.OnChangeScene("Game Manager");
	    }

		//Vector3 move = new Vector3 (transform.position.x, step, transform.position.z);    // Imagens sem o Lerp (estilo slideshow)

		Vector3 move = new Vector3 (transform.position.x, Mathf.Lerp(transform.position.y, step, Time.fixedDeltaTime * 1.2f) , transform.position.z); //Imagens com o Lerp
		transform.position = move;

	}
}
