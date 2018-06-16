using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Voador : EnemyBehaviour {

	private float wanderingTime; //Tempo para ficar vagando.

	private float horizontal;
	private float speed; //Velocidade do movimento. 

	private Vector2 move; //Vector2 de movimentação
	private Vector2 stop;

	[SerializeField]
	private Transform wallCheck;

	private bool voar;

	[FMODUnity.EventRef]
	public string heliceSom = "event:/Inimigos/helice";
	public static FMOD.Studio.EventInstance heliceEv;
	protected FMOD.Studio.ParameterInstance ativoParam;

	void Start() {

		// Controle de Vidas //

		vivo = true; //Começa vivo.
		vidas = 1; //Esse inimigo tem apenas uma vida.
		vulneravel = true;

		// FSM //

		horizontal = 0;
		speed = 6f; //Velocidade do inimigo.

		// Dano //

		danoBase = 1; //Dano básico do inimigo.

		heliceEv = FMODUnity.RuntimeManager.CreateInstance(heliceSom);
		heliceEv.getParameter ("Ativo", out ativoParam);
		heliceEv.start();
	}

	void FixedUpdate() {

		changeState (); //Chama o método de troca de estados.
		move = new Vector2 (horizontal * speed, rb.velocity.y); //Define o movimento do inimigo.
		stop = new Vector2 (0f, rb.velocity.y); //Vector2 de Movimentação do Inimigo é Zerada.

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

				ativoParam.setValue(1f);

				if (recebeuDano) { //Se recebeu dano...

					stopState (); //Chama o método de ficar parado.
				} else { //Caso não tenha recebido dano...

					wanderingState (); //Chama o método de ficar vagando.
				}
			} else { //Caso não esteja ativo,
				ativoParam.setValue(0f);
				stopState (); //Chama o método de ficar parado.
			}
		} else { //Caso não esteja vivo,

			stopState ();
			rb.gravityScale = 25;
			ativoParam.setValue(0f);
		}
	}

	void wanderingState() { //Estado de vagar por aí.

		wanderingTime += Time.fixedDeltaTime; //Tempo++
		rb.gravityScale = 0; //Define o quanto a gravidade escala nesse corpo rígido como 0.
		rb.velocity = move;

		if (wanderingTime < 4) { //Se o tempo for menor que 4 segundos...

			horizontal = transform.localScale.x * -1; //

		} else { // Se tempo for > 4: flipa de lado.
			Vector2 scale = transform.localScale; //Vector2 que pega o localScale do inimigo.
			scale.x *= -1; //X do localScale multiplicado por -1...
			transform.localScale = scale; //Torna o localScale atual no inimigo(Flip).
			wanderingTime = 0; //Zera o tempo, para andar mais 4 segundos.
		}

	}

	void stopState() { //Estado de ficar parado.

		rb.velocity = stop;

	}

	new IEnumerator preMorte() {

		yield return new WaitForSeconds (0.4f);
		Instantiate (destroy, transform.position, Quaternion.identity);
		Instantiate (alma, transform.position, Quaternion.identity);
		DestroyImmediate (gameObject);
	}

	void OnCollisionStay2D (Collision2D other) { //Quando entrar em colisão...

		if ((other.gameObject.tag == "Chão" || other.gameObject.tag == "Espinho") && !vivo) { //Se colidir com o chão e não estiver vivo.

			StartCoroutine (preMorte ());
		}
	}
}
