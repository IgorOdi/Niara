using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Morcego : EnemyBehaviour {
    
    private float explosaoTime;

	[SerializeField]
	private float moveTimeA;
	[SerializeField]
	private float moveTimeB;

    [SerializeField]
    private GameObject rest;

    Alcance restScript;

    private void Start() {

        vivo = true;
        vidas = 1;
        vulneravel = true;

        danoBase = 1;

        transform.position = rest.transform.position;

        restScript = rest.GetComponent<Alcance>();
    }

    private void FixedUpdate() {

        if (vivo) {
            if (restScript.ativar) {

                chaseState();
            } else {

                returnState();
            }
        } else {

            explosaoMorte();
        }


    }

    void chaseState() {
        
		anim.SetBool ("New Bool", true);

		moveTimeA += Time.fixedDeltaTime/20;
		moveTimeB = 0;

		transform.position = Vector3.Lerp (transform.position, player.transform.position, moveTimeA);
    }

    void returnState() {

		moveTimeB += Time.fixedDeltaTime/20;
		moveTimeA = 0;

		if (Vector2.Distance (transform.position, rest.transform.position) < 1) {

			anim.SetBool ("New Bool", false);
		}


		transform.position = Vector3.Lerp (transform.position, rest.transform.position, moveTimeB);
    }

    void explosaoMorte() {

        explosaoTime += Time.fixedDeltaTime;

        if (explosaoTime < 2f) {

			anim.SetBool ("New Bool", false);
			rb.gravityScale = 1;
        } else {

            Morte();
        }
    }

	void OnTriggerStay2D(Collider2D other) {

		if (other.gameObject.tag == "Rest") {

			rest = other.gameObject;
			restScript = rest.GetComponent<Alcance>();
		}
	}
}
