using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roda : EnemyBehaviour {

	private float idleTime;
	private float rollingTime;

	private int currentState;

	private float horizontal;
	private float speed;

	private bool danificou;

	private Vector2 move;

	void Start() {

		vivo = true;
		vidas = 1;
		vulneravel = true;

		horizontal = 0;
		speed = 8;

		danoBase = 1;
	}

	void FixedUpdate() {

		switch (currentState) {

		case 0:

			idleState ();
			break;

		case 1:

			rollingState ();
			break;
		}

		if (!vivo) {

			StartCoroutine (preMorte ());
		}

		move = new Vector2 (horizontal * speed, rb.velocity.y);
	}

	void changeState() {

		if (vivo) {

			if (ativo) {

				if (currentState == 0) {

					currentState = 1;
				} else {

					currentState = 0;
				}
			}
		}
	}

	new void flip() {

		Vector2 scale = transform.localScale; //Vector2 que pega o localScale do inimigo.

		if (lado == -1) {

			scale.x = 1; //X do localScale multiplicado por -1...
		} else {
			scale.x = -1;
		}
		transform.localScale = scale; //Torna o localScale atual no inimigo(Flip).

		horizontal = lado;
	}

	void idleState() {

		idleTime += Time.fixedDeltaTime;
		rb.velocity = new Vector2 (0, rb.velocity.y);
		anim.SetTrigger ("Idle");
		vulneravel = true;
		danificou = false;
		flip ();

		if (idleTime > 2) {

			horizontal = lado;
			changeState ();
			idleTime = 0;
		}
	}

	void rollingState() {

		rollingTime += Time.fixedDeltaTime;
		anim.SetTrigger ("Girar");
		vulneravel = false;

		if (rollingTime < 0.25f) {

			rb.AddForce(new Vector2(0, 600));
		} else if (rollingTime > 1f && rollingTime < 2.5f) {

			if (!danificou) {
				
				rb.velocity = move;
			} else {
				rb.velocity = -move;
			}
		} else  if(rollingTime > 2.5f) {
			
			changeState ();
			rollingTime = 0;
		}
	}

	void OnCollisionEnter2D(Collision2D other) {

		if (other.gameObject.tag == "Player") {
			
			danificou = true;
		}
	}
}
