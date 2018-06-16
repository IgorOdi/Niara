using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingShooter : EnemyBehaviour {

	private Vector2 move, stop;
	private float horizontal, speed;
	private float wanderingTime, shootingTime;
	[SerializeField]
	private GameObject shot;
	[SerializeField]
	private Transform spawnShot;

	[SerializeField]
	private Transform wallCheck;

	void Start() {

		vivo = true;
		vidas = 1;
		vulneravel = true;

		danoBase = 1;
		horizontal = -1;
		speed = 3;
	}

	void FixedUpdate() {

		changeState (); //Chama o método de troca de estados.
		move = new Vector2 (horizontal * speed, rb.velocity.y); //Define o movimento do inimigo.
		stop = new Vector2 (0f, rb.velocity.y); //Vector2 de Movimentação do Inimigo é Zerada.

		if (wanderingTime >= 4 || Physics2D.Linecast(transform.position,wallCheck.position, wallLayer)) {

			Vector2 scale = transform.localScale; //Vector2 que pega o localScale do inimigo.
			scale.x *= -1; //X do localScale multiplicado por -1...
			transform.localScale = scale; //Torna o localScale atual no inimigo(Flip).
			horizontal = transform.localScale.x * -1;
			wanderingTime = 0; //Zera o tempo, para andar mais 2,5 segundos.
		}

		if (shootingTime > 2.5f) {

			if (ativo) {
				
				Instantiate (shot, spawnShot.position, transform.rotation);
				shootingTime = 0;
			}
		}
	}

	void changeState() {

		if (vivo) {

			rb.velocity = move;
			wanderingTime += Time.fixedDeltaTime;
			shootingTime += Time.fixedDeltaTime;
		} else {

			rb.velocity = stop;
			rb.gravityScale = 25;
		}
	}

	new IEnumerator preMorte() {

		yield return new WaitForSeconds (0.4f);
		Instantiate (destroy, transform.position, Quaternion.identity);
		Instantiate (alma, transform.position, Quaternion.identity);
		DestroyImmediate (gameObject);
	}

	void OnCollisionStay2D (Collision2D other) { //Quando entrar em colisão...

		if (other.gameObject.tag == "Chão" && !vivo) { //Se colidir com o chão e não estiver vivo.

			StartCoroutine (preMorte ());
		}
	}
}
