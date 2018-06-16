using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Serrador : EnemyBehaviour {

	private float speed;
	private float horizontal;

	private Vector2 move;

	[SerializeField]
	private int currentState;

	private float idleTime;
	private float movingTime;
	private float serrarTime;
	private float flameTime;
	private float toraTime;
	private float superIdleTime;

	private float walkCount;
	private float serrarCount;
	private float flameCount;
	private float toraCount;

	[HideInInspector]
	public float ladoTora;
	private int randomizador;

	[SerializeField]
	private GameObject serra, chamas, spawn, tora;

	private bool moveu;
	private bool fogou;
	private bool serrou;
	private bool torou;

	[FMODUnity.EventRef]
	public string somPassos = "event:/Passos";
	public static FMOD.Studio.EventInstance passosSerraEv;

	[FMODUnity.EventRef]
	public string somFogo = "event:/Fogo";

	[FMODUnity.EventRef]
	public string somSerra = "event:/Serra";

	[FMODUnity.EventRef]
	public string somTora = "event:/Tora";

	void Start() {

		vivo = true;
		vidasMax = 60;
		vidas = vidasMax;
		vulneravel = true;

		horizontal = 0;
		speed = 6;

		currentState = 0;
		danoBase = 1;

		ladoTora = -1;
        boss = true;

		passosSerraEv = FMODUnity.RuntimeManager.CreateInstance (somPassos);
	}

	void FixedUpdate() {

		switch (currentState) {

		case 0:

			idleState ();
			break;

		case 1:

			movingState ();
			break;

		case 2:

			serrarState ();
			break;

		case 3:

			flameState ();
			break;

		case 4:

			toraState ();
			break;

		case 5:

			superIdleState ();
			break;
		}

		if (!vivo) {

			StartCoroutine (preMorte ());
		}

		move = new Vector2 (horizontal * speed, 0);
	}

	void changeState() {

		if (distancia <= 10 && distancia >= -10) {

			if (serrarCount <= 2 && flameCount <= 2) {
				
				randomizador = Random.Range (0, 2);

				if (randomizador == 0) {
				
					currentState = 2;
				} else {

					currentState = 3;
				}
			} else {

				currentState = 1;
			}
		} else {

			if (walkCount <= 2) {

				currentState = 1;
			} else {

				currentState = 4;
			}
		}
	}

	void idleState() {

		idleTime += Time.fixedDeltaTime;
		flip ();

		if (idleTime < 1f) {

		} else {

			idleTime = 0;
			changeState ();
		}
	}

	new void flip() {

		Vector2 scale = transform.localScale; //Vector2 que pega o localScale do inimigo.
		scale.x = -(lado); //X do localScale multiplicado por -1...
		transform.localScale = scale; //Torna o localScale atual no inimigo(Flip).

		horizontal = lado;
	}

	void movingState() {

		movingTime += Time.fixedDeltaTime;

		if (serrarCount < 2 && flameCount < 2) {
			
			if (movingTime < 2f) {

				if (!moveu) {

					passosSerraEv.start ();
					moveu = true;
				}

				anim.SetBool ("Walk", true);
				rb.velocity = move;
			} else {

				passosSerraEv.stop (FMOD.Studio.STOP_MODE.IMMEDIATE);
				moveu = false;

				anim.SetBool ("Walk", false);
				walkCount++;
				movingTime = 0;
				currentState = 0;
			}

		} else {

			if (movingTime < 1f) {

				if (!moveu) {

					passosSerraEv.start ();
					moveu = true;
				}
				if (distancia < 10 && distancia > -10) {
					
					anim.SetBool ("Walk", true);
					rb.velocity = -move;
				}
			} else {

				passosSerraEv.stop (FMOD.Studio.STOP_MODE.IMMEDIATE);
				moveu = false;

				anim.SetBool ("Walk", false);
				walkCount++;
				movingTime = 0;
				currentState = 4;
			}

		}
	}

	void serrarState() {

		serrarTime += Time.fixedDeltaTime;	

		if (serrarTime < 0.5f) {
			
			if (!serrou) {

				FMODUnity.RuntimeManager.PlayOneShot (somSerra, transform.position);
				serrou = true;
			}

			anim.SetBool ("Sarrada", true);
		} else if (serrarTime > 1.2f && serrarTime < 1.6f) {

			serra.SetActive (true);
		} else if (serrarTime >= 1.6f) {

			serrou = false;

			serra.SetActive (false);
			anim.SetBool ("Sarrada", false);
			serrarTime = 0;
			serrarCount++;
			currentState = 0;
		}
	}

	void flameState() {

		flameTime += Time.fixedDeltaTime;

		if (flameTime < 1) {

			anim.SetBool ("Fogada", true);
		} else if (flameTime > 1.7f && flameTime < 2f) {

			if (!fogou) {

				FMODUnity.RuntimeManager.PlayOneShot (somFogo, transform.position);
				fogou = true;
			}

			chamas.SetActive (true);
		} else if (flameTime > 2.35f) {

			fogou = false;

			chamas.SetActive (false);
			anim.SetBool ("Fogada", false);
			flameTime = 0;
			flameCount++;
			currentState = 0;
		}
	}

	void toraState() {

		toraTime += Time.fixedDeltaTime;

		if (toraTime < 0.5f) {

			torou = false;
			anim.SetBool ("Torrada", true);
		} else if (toraTime >= 0.5f) {

			if (!torou) {

				FMODUnity.RuntimeManager.PlayOneShot (somTora, transform.position);
				torou = true;
			}

			anim.SetBool ("Torrada", false);
			ladoTora *= -1;
			Instantiate (tora, spawn.transform.position, spawn.transform.rotation);
			toraCount++;
			toraTime = 0;
		}

		if (toraCount >= 3) {

			torou = false;

			walkCount = 0;
			toraCount = 0;
			serrarCount = 0;
			flameCount = 0;
			currentState = 0;
			toraTime = 0;
		}
	}

	void superIdleState() {

		superIdleTime += Time.fixedDeltaTime;

		if (superIdleTime > 2.5f) {

			superIdleTime = 0;
			currentState = 0;
		}
	}

	void OnDestroy() {
		
		passosSerraEv.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);

	}
}
