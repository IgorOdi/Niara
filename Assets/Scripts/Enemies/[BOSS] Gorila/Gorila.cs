using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gorila : EnemyBehaviour {

	/*
	- Tremor de tela na habilidade Tremor
	*/

	private float speed;
	private float horizontal;

	private Vector2 move;

	[SerializeField]
	private int currentState;

	private float idleTime;
	private float hitTime;
	private float movingTime;
	private float tremorTime;
	private float pendurarTime;

	private float idleExtreme;

	private bool rage;

	private int randomizador;

	private bool canSpawn;
	private bool canJump;
	private int canJumpCount;

	private int hittingCount;
	private int movingCount;
	public static bool tremor;

	[SerializeField]
	private GameObject hits;
	[SerializeField]
	private GameObject macacos;
	[SerializeField]
	private Transform[] spawnPoints;
	[SerializeField]
	private GameObject findMacacos;

	private SpriteRenderer plaquinha;

	// Sons //
	private bool idleStarted;
	private bool groundPunchou;
	private bool jumpou;
	private bool socou;
	private bool shoutou;
	private bool moveu;

	[FMODUnity.EventRef]
	public string somGroundPunch = "event:/GroundPunch";

	[FMODUnity.EventRef]
	public string somJump = "event:/somJump";

	[FMODUnity.EventRef]
	public string somSoco = "event:/somSoco";

	[FMODUnity.EventRef]
	public string somBrabo = "event:/somBrabo";

	[FMODUnity.EventRef]
	public string somIdle = "event:/Idle";
	public static FMOD.Studio.EventInstance idleGorilaEv;

	[FMODUnity.EventRef]
	public string somPassos = "event:/Passos";
	public static FMOD.Studio.EventInstance passosGorilaEv;

	void Start() {

		vivo = true;
		vidasMax = 45;
		vidas = vidasMax;
		vulneravel = true;

		horizontal = 0;
		speed = 4;

		currentState = 0;
		canSpawn = true;

		danoBase = 1;

        boss = true;

		plaquinha = GameObject.Find ("JUMP").GetComponent<SpriteRenderer> ();

		idleGorilaEv = FMODUnity.RuntimeManager.CreateInstance (somIdle);
		passosGorilaEv = FMODUnity.RuntimeManager.CreateInstance (somPassos);
	}

	void FixedUpdate() {

		rage = vidas < 16 ? true : false;
		canJump = canJumpCount > 3 ? true : false;

		if (rage) {

			idleExtreme = 1;
			speed = 6;
		} else {

			idleExtreme = 1.5f;
			speed = 4;
		}

		switch (currentState) {

		case 0:

			idleState ();
			break;

		case 1:

			movingState ();
			break;

		case 2:

			hitState ();
			break;

		case 3:

			tremorState ();
			break;

		case 4:

			pendurarState ();
			break;
		}

		if (!vivo) {

			Morte ();
		}

		move = new Vector2 (horizontal * speed, 0);
	}
		
	void changeState() {

		if ((vidas <= 30 && vidas >= 20 || vidas >= 10 && vidas <= 15) && 
			(transform.position.x > spawnPoints[0].position.x && transform.position.x < spawnPoints[1].position.x) && canJump) {

			currentState = 4;
		} else {

			if (distancia < 8 && distancia > -8) {

				if (hittingCount < 2) {
					
					currentState = 2;
				} else {

					if (canJump) {
						
						currentState = 4;
						hittingCount = 0;
					} else {

						currentState = 3;
						hittingCount = 0;
					}
				}
			} else {

				if (movingCount <= 2) {
					
					currentState = 1;
				} else {

					currentState = 3;
					movingCount = 0;
				}
			}
		}
	}

	new void flip() {

		Vector2 scale = transform.localScale; //Vector2 que pega o localScale do inimigo.
		scale.x = -(lado * 1.5f); //X do localScale multiplicado por -1...
		transform.localScale = scale; //Torna o localScale atual no inimigo(Flip).

		horizontal = lado;
	}

	void idleState() {

		idleTime += Time.fixedDeltaTime;
		anim.SetTrigger("Idle");

		if (idleTime > idleExtreme / 2 && idleTime < idleExtreme) {

			if (!idleStarted) {

				idleGorilaEv.start ();
				idleStarted = true;
			}
		} else if (idleTime > idleExtreme) {

			flip ();
			idleTime = 0;
			changeState ();

			idleStarted = false;
		}
	}

	void movingState() {

		movingTime += Time.fixedDeltaTime;

		if (movingTime < 2f) {

			if (!moveu) {

				passosGorilaEv.start ();
				moveu = true;
			}

			anim.SetBool ("Walk", true);
			rb.velocity = move;
		} else {

			passosGorilaEv.stop (FMOD.Studio.STOP_MODE.IMMEDIATE);

			moveu = false;
			anim.SetBool ("Walk", false);

			if (distancia < 6f && distancia > -6f) {

				currentState = 2;
			} else {

				movingCount++;
				movingTime = 0;
				currentState = 0;
			}
		}
	}

	void hitState() {

		hitTime += Time.fixedDeltaTime;

		if (hitTime > 1 && hitTime < 1.1f) {

		} else if (hitTime > 1.1f && hitTime < 2f) {
			
			if (!shoutou) {

				FMODUnity.RuntimeManager.PlayOneShot (somBrabo, transform.position);
				shoutou = true;
			}

			anim.SetBool ("Punch", true);
		} else if (hitTime > 2f && hitTime < 2.2f) {

			if (!socou) {

				FMODUnity.RuntimeManager.PlayOneShot (somSoco, transform.position);
				socou = true;
			}
			hits.SetActive (true);
		} else if (hitTime > 2.3f) {

			socou = false;
			shoutou = false;
			anim.SetBool ("Punch", false);
			canSpawn = true;
			hits.SetActive (false);
			canJumpCount++;
			hittingCount++;
			hitTime = 0;
			currentState = 0;
		}
	}

	void tremorState() {

		tremorTime += Time.fixedDeltaTime;

		if (tremorTime < 0.1f) {

			if (!shoutou) {

				FMODUnity.RuntimeManager.PlayOneShot (somBrabo, transform.position);
				shoutou = true;
			}

			plaquinha.enabled = true;
			anim.SetBool ("GroundPunch", true);
		} else if (tremorTime > 0.9f && tremorTime < 1f) {

			if (!groundPunchou) {

				FMODUnity.RuntimeManager.PlayOneShot (somGroundPunch, transform.position);
				groundPunchou = true;
			}

			tremor = true;
		} else if (tremorTime > 1f) {

			shoutou = false;
			groundPunchou = false;

			plaquinha.enabled = false;

			anim.SetBool ("GroundPunch", false);
			canSpawn = true;
			tremor = false;
			canJumpCount++;
			tremorTime = 0;
			currentState = 0;
		}
	}

	void pendurarState() {

		pendurarTime += Time.fixedDeltaTime;

		if (transform.position.y >= 3) {

			anim.SetBool ("Pendurado", true);
			anim.SetBool ("JumpX", false);
		}

		if (pendurarTime <= 0.5f) {

			anim.SetBool ("JumpX", true);
		} else if (pendurarTime >= 0.5f && pendurarTime < 10) {

			if (!jumpou) {

				FMODUnity.RuntimeManager.PlayOneShot (somJump, transform.position);
				jumpou = true;
			}
				
			rb.AddForce (new Vector2 (0, 60000));
			summonState ();
			canSpawn = false;
		} else if (pendurarTime >= 10) {

			jumpou = false;

			anim.SetBool ("Pendurado", false);
			canJumpCount = 0;
			canJump = false;
			pendurarTime = 0;
			currentState = 0;
		}
	}

	void summonState() {

		findMacacos = GameObject.Find ("MonkeyBoss(Clone)");

		if (findMacacos == null) {

			if (canSpawn) {
				for (int i = 0; i < spawnPoints.Length; i++) {
			
					Instantiate (macacos, spawnPoints [i].position, spawnPoints [i].rotation);
				}
			}
		}
	}

	void OnDestroy() {
		
		passosGorilaEv.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);

	}
}
