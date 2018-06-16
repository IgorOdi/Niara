using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escudeiro : EnemyBehaviour {

	private float hittingTime;
	private float idleTime;
	private float movingTime;

	private float horizontal;
	private float speed;
	private int currentState;

	private Vector2 move;

	void Start() {

		vivo = true;
		vidas = 1;
		vulneravel = true;

		speed = 2;
		horizontal = 0;
		currentState = 0;

		danoBase = 1;
	}

	void FixedUpdate() {

		switch (currentState) {

		case 0:

			idleState ();
			break;

		case 1:

			hittingState ();
			break;

		case 2:

			movingState ();
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
				
				if (distancia < 6 && distancia > -6) {
					
					currentState = 1;
				} else {

					currentState = 2;
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
		//anim.SetTrigger ("Idle");

		if (idleTime > 2f) {

			flip ();
			changeState ();
			idleTime = 0;
		}
	}

	void hittingState() {

		hittingTime += Time.fixedDeltaTime;


		if (hittingTime <= 0.1f) {

			//anim.SetTrigger ("Hit");
			rb.AddForce (new Vector2 (-12000 * -lado, 0));
		} else if (hittingTime > 0.1f && hittingTime < 0.3f) {

			rb.AddForce (new Vector2 (6000 * -lado, 4000));
		} else if (hittingTime > 0.6f){

			currentState = 0;
			hittingTime = 0;
		}
	}

	void movingState() {

		movingTime += Time.deltaTime;

		if (movingTime < 3f) {

			rb.velocity = move;
		} else {

			currentState = 0;
			movingTime = 0;
		}
	}
}
