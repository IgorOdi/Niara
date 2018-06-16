using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeyBoss : EnemyBehaviour {

	private float idleTime;
	private float throwTime;

	private int currentState;

	[SerializeField]
	private GameObject arremessavel;
	[SerializeField]
	private Transform spawn;

	void Start() {

		vivo = true;
		vidas = 1;
		vulneravel = true;

		currentState = 0;

		danoBase = 1;

		boss = true;
	}

	void FixedUpdate() {

		switch (currentState) {

		case 0:

			idleState ();
			break;

		case 1:

			throwState ();
			break;

		}

		if (!vivo) {

			Morte ();
		}
	}

	void changeState() {

		if (vivo) {
			
			if (currentState == 0) {

				currentState = 1;
			} else {

				currentState = 0;
			}
		}
	}

	void idleState() {

		idleTime += Time.deltaTime;
		flip ();

		if (idleTime > 2f) {

			changeState ();
			idleTime = 0;
		}
	}

	void throwState() {

		throwTime += Time.fixedDeltaTime;

		if (throwTime < 0.6f) {
			anim.SetBool("Throw", true);
		}
		else {
			anim.SetBool("Throw", false);
			throwTime = 0;
			Instantiate(arremessavel, spawn.position, spawn.rotation);
			changeState();
		}
	}
}
