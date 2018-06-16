using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SidePlatform : Ativaveis {

	private float speed;
	private float difference;
	private float initialPos;


	// Use this for initialization
	void Awake () {
		speed = 0.1f;
		difference = 0;
		initialPos = objetoAlterado.transform.position.x;
	}
	
	// Update is called once per frame
	void Update () {

		if(activated) {
			if (objetoAlterado.transform.position.x  * objetoAlterado.transform.localScale.x - initialPos  * objetoAlterado.transform.localScale.x < 2f) {
				objetoAlterado.transform.Translate (speed*objetoAlterado.transform.localScale.x, 0, 0);
			}
		}
	}
}
