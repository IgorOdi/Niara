using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lixomancer : EnemyBehaviour {

    [SerializeField]
    private GameObject lixoSpawn;
    [SerializeField]
    private GameObject lixo;

    private float idleTime;
    private float magicTime;

    private int currentState;

	public override void Start() {

        vivo = true;
        vidas = 2;
        vulneravel = true;
		currentState = 0;
    }

    private void FixedUpdate() {

		if (vivo) {

			if (ativo) {

				switch (currentState) {

				case 0:
					idleState ();
					break;

				case 1:
					magicState ();
					break;
				}
			}
		} else {

			Morte ();
		}
	}

    void idleState() {

        idleTime += Time.fixedDeltaTime;
        flip();

		if (idleTime > 0.5f && idleTime < 2f) {

		} else if (idleTime >= 2f) {

			idleTime = 0;
            currentState = 1;
        }
    }

    void magicState() {

        magicTime += Time.fixedDeltaTime;

		if (magicTime > 0.5f && magicTime < 1f) {

			anim.SetBool ("New Bool", true);
		} else if (magicTime > 1f) {

			anim.SetBool ("New Bool", false);
			Instantiate (lixo, lixoSpawn.transform.position, lixoSpawn.transform.rotation);
            magicTime = 0;
            currentState = 0;
        }
    }
}
