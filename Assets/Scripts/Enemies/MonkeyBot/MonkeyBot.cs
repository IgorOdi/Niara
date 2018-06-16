using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeyBot : EnemyBehaviour {

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
	}

	void FixedUpdate() {

		if (vivo && ativo) {
			switch (currentState) {

			case 0:

				idleState ();
				break;

			case 1:

				throwState ();
				break;

			}
		} else if (!vivo) {

			StartCoroutine (preMorte ());
		}
	}

	void idleState() {

		idleTime += Time.deltaTime;
		flip ();

		if (idleTime > 2f) {

			currentState = 1;
			idleTime = 0;
		}
	}

	void throwState() {

        throwTime += Time.fixedDeltaTime;

        if (throwTime < 0.6f){
			
            anim.SetBool("Throw", true);
        } else {
			
            anim.SetBool("Throw", false);
            Instantiate(arremessavel, spawn.position, spawn.rotation);
			currentState = 0;
			throwTime = 0;
        }
	}
}
