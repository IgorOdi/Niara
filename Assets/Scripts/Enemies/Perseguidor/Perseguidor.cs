using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Perseguidor : EnemyBehaviour {

    private int currentState;

    private float hitTime;
    private bool canHit;

    private float speed;
    private float horizontal;

    private Vector2 move;
    [SerializeField]
    private GameObject arma;

	private bool barkou;

	[FMODUnity.EventRef]
	public string somBark = "event:/Inimigos/latidoagudo";

	public override void Start() {

        vivo = true;
        vidas = 2;
        vulneravel = true;

        currentState = 0;

        speed = 4;
        horizontal = 0;

        danoBase = 1;
        canHit = true;
    }

    void FixedUpdate() {

        switch (currentState) {

            case 0:

                waitState();
                break;

            case 1:

                chaseState();
                break;

            case 2:

                hitState();
                break;
        }

        horizontal = lado;
        flip();
        changeState();

        move = new Vector2(horizontal * speed, rb.velocity.y);
    }

    void changeState() {

        if (vivo) {

            if ((!ativo) || (!canHit)) {

                currentState = 0;
            } else if ((distancia < 3.5f && distancia > -3.5f) && (canHit)) {

                currentState = 2;
            } else if (canHit) {

                currentState = 1;
            } else {

                currentState = 0;
            }
        } else {

            currentState = 0;
            StartCoroutine(preMorte());
        }
    }

    void waitState() {
		
        anim.SetBool("Walk", false);
    }

    void chaseState() {

        arma.SetActive(false);
        rb.velocity = move;

        anim.SetBool("Walk", true);
    }

    void hitState() {

        hitTime += Time.fixedDeltaTime;

		if (hitTime < 1f) {

		} else if (hitTime > 1f && hitTime < 1.1f) {

			if (!barkou) {

				FMODUnity.RuntimeManager.PlayOneShot (somBark, transform.position);
				barkou = true;
			}

            anim.SetTrigger("Bite");
        } else if (hitTime > 1.6f && hitTime < 1.75f) {

            arma.SetActive(true);
        } else if (hitTime > 1.75f) {

			barkou = false;

            arma.SetActive(false);
            canHit = false;
            currentState = 0;
            StartCoroutine(hitCooldown());
			hitTime = 0;
        }
    }

    IEnumerator hitCooldown() {

        yield return new WaitForSeconds(2f);
        canHit = true;
    }
}
