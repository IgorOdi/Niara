using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Serrinha : EnemyBehaviour {

	private float wanderingTime; //Tempo para ficar vagando.

	private float speed; //Velocidade do movimento.
	private float horizontal; //??

	private Vector2 move; //Vector2 do movimento.
	private Vector2 stop;

	[SerializeField]
	private Transform wallCheck;

	public override void Start () {

		// Controle de Vidas //

		vivo = true; //Inimigo começa vivo.
		vidas = 1; //Esse inimigo tem apenas uma vida.
		vulneravel = true;

		// FSM //

		speed = 4f; //Velocidade do inimigo.
		horizontal = -1; //?

		// Dano //

		danoBase = 1; //Dano básico do inimigo.

	}

	void FixedUpdate() {
		
		move = new Vector2 (horizontal * speed, rb.velocity.y); //Vector2 de Movimentação do Inimigo.
		stop = new Vector2 (0, rb.velocity.y);
		changeState(); //Chamar método de troca de estados.

		if (Physics2D.Linecast(transform.position,wallCheck.position, wallLayer)) {
			
			Vector2 scale = transform.localScale; //Vector2 que pega o localScale do inimigo.
			scale.x *= -1; //X do localScale multiplicado por -1...
			transform.localScale = scale; //Torna o localScale atual no inimigo(Flip).
			wanderingTime = 0; //Zera o tempo, para andar mais 2,5 segundos.
		}
	}

	void changeState() { //Método de troca de estados.

		if (vivo) { //Se o inimigo estiver vivo,

			if (ativo) { //Se ele estiver ativo,
				if (recebeuDano) { //Se recebeu dano...

					stopState (); //Chama o método de ficar parado.
				} else { //Caso não tenha recebido dano...

					wanderingState (); //Chama o método de ficar vagando.
				}
			} else { //Caso não esteja ativo,

				stopState (); //Chama o método de ficar parado.
			}
		} else { //Caso não esteja vivo,

			StartCoroutine (preMorte ());
		}
	}

	void wanderingState() { //Estado de vagar por aí.

		wanderingTime += Time.fixedDeltaTime; //Tempo++
		rb.velocity = move;

		if (wanderingTime < 2.5f) { //Enquanto o tempo for < 2,5 segundos: se move
			horizontal = transform.localScale.x * -1;


		} else { // Se tempo for > 2,5 segundos: flipa de lado.
			Vector2 scale = transform.localScale; //Vector2 que pega o localScale do inimigo.
			scale.x *= -1; //X do localScale multiplicado por -1...
			transform.localScale = scale; //Torna o localScale atual no inimigo(Flip).
			wanderingTime = 0; //Zera o tempo, para andar mais 2,5 segundos.
		}
	}

	void stopState() { //Estado de ficar parado.

		rb.velocity = stop;
	}
}


