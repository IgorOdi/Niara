using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RisingPlatform : Ativaveis {

	private float speed;
	private float difference;
	private float initialPos;


	// Use this for initialization
	void Awake () {
		speed = 0.1f;
		difference = 0;
		initialPos = objetoAlterado.transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log (initialPos);

		if(activated) {
			if (objetoAlterado.transform.position.y - initialPos < 9f) {
				objetoAlterado.transform.Translate (0, speed, 0);
			}
		}
	}
}
