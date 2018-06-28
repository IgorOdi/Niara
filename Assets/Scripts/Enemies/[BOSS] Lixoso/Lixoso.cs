using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lixoso : EnemyBehaviour {

	private float horizontal;
	private float speed;
    private float jumpForce;

	private Vector2 move;

	private float idleTime;
	private float movingTime;
	private float waveTime;
	private float ballTime;
	private float pullTime;
	private float stunTime;

    [SerializeField]
	private int currentState;

	private int ballCount;
	private int waveCount;
    private int skillCount;
	private int movingCount;

    private float timeToMove;
    private float distanceToWaypoint;

    private int randomizador;

    [SerializeField]
    private GameObject lixoWave;
    [SerializeField]
    private GameObject lixoBall;
    [SerializeField]
    private Transform waveSpawn;
    [SerializeField]
    private Transform lixoSpawn;

    [SerializeField]
    private Transform waypoint;

    [SerializeField]
    private GameObject dropLixoObject;
    [SerializeField]
    private Transform[] dropLixoTransform;

	public SpriteRenderer placa;

	private bool atirou, socou, pulou, idlou;

	[FMODUnity.EventRef]
	public string somJump = "event:/somJump";

	[FMODUnity.EventRef]
	public string somSoco = "event:/somSoco";

	[FMODUnity.EventRef]
	public string somTiro = "event:/somTiro";

	[FMODUnity.EventRef]
	public string somIdle = "event:/Passos";
	public static FMOD.Studio.EventInstance idleLixosoEv;

	void Start() {

		vivo = true;
		vidasMax = 80;
		vidas = vidasMax;
		vulneravel = true;

        danoBase = 1;
		horizontal = 0;
		speed = 2;

        boss = true;

		idleLixosoEv = FMODUnity.RuntimeManager.CreateInstance (somIdle);
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
			waveState ();
			break;

		case 3:
			ballState ();
			break;

		case 4:
			pullState ();
			break;

		case 5:
			stunState ();
			break;
		}

        if (!vivo) {
            Morte();
        }

		move = new Vector2 (horizontal * speed, 0);
	}

	void changeState() {

        if (skillCount >= 4) {

            currentState = 4;
        } else {

            if (distancia >= 10 || distancia <= -10) {

                randomizador = Random.Range(0, 2);

				if (randomizador == 0 && ballCount <= 2) {

					currentState = 3;
				} else if (movingCount <= 2) {

					currentState = 1;
				} else {

					currentState = 2;
				}
            } else {

                randomizador = Random.Range(0, 2);

				if (randomizador == 0 && waveCount <= 2) {

					currentState = 2;
				} else if (movingCount <= 2) {

					currentState = 1;
				} else {

					currentState = 3;
				}
            }
        }
	}

	new void flip() {

		Vector2 scale = transform.localScale; //Vector2 que pega o localScale do inimigo.
		scale.x = -lado; //X do localScale multiplicado por -1...
		transform.localScale = scale; //Torna o localScale atual no inimigo(Flip).
		horizontal = lado;
	}

	void idleState() {

		idleTime += Time.fixedDeltaTime;

		if (idleTime >= 1f) {

			idlou = false;
			flip ();
			idleTime = 0;
			changeState ();
		} else {

			if (!idlou) {

				idleLixosoEv.start ();
				idlou = true;
			}
		}
	}

	void movingState() {

		movingTime += Time.fixedDeltaTime;
		rb.velocity = move;

		if (movingTime >= 2f) {

			anim.SetBool ("New Bool", false);
			movingCount++;
			movingTime = 0;
			currentState = 0;
		} else {

			anim.SetBool ("New Bool", true);
		}
	}

	void waveState() {

		waveTime += Time.fixedDeltaTime;

		if (waveTime >= 1.5f) {

			anim.SetBool ("New Bool 1", false);

			socou = false;
			placa.enabled = false;

			if (PlayerController.grounded) PlayerController.Damage (danoBase, true);

//			Instantiate (lixoWave, waveSpawn.position, waveSpawn.rotation);
			skillCount++;
			waveCount++;
			waveTime = 0;
			currentState = 0;
		} else {

			placa.enabled = true;

			if (!socou) {

				FMODUnity.RuntimeManager.PlayOneShot (somSoco, transform.position);
				socou = true;
			}

			anim.SetBool ("New Bool 1", true);
		}
	}

	void ballState() {

		ballTime += Time.fixedDeltaTime;

		if (ballTime > 0 && ballTime < 1.2f) {

			atirou = false;
			anim.SetBool ("New Bool 2", true);
		} else if (ballTime >= 1.2f) {

			flip ();

			if (ballCount < 2) {
				if (!atirou) {

					FMODUnity.RuntimeManager.PlayOneShot (somTiro, transform.position);
					atirou = true;
				}
				
				anim.SetBool ("New Bool 2", false);

				Instantiate (lixoBall, lixoSpawn.position, lixoSpawn.rotation);
				ballTime = 0;
				ballCount++;
				skillCount++;
			} else {

				if (!atirou) {

					FMODUnity.RuntimeManager.PlayOneShot (somTiro, transform.position);
					atirou = true;
				}

				Instantiate (lixoBall, lixoSpawn.position, lixoSpawn.rotation);

				atirou = false;
				anim.SetBool ("New Bool 2", false);
				ballTime = 0;
				ballCount = 0;
				currentState = 0;
			}
		}
	}

	void pullState() {

		pullTime += Time.fixedDeltaTime;

        if (pullTime <= 0.1f) {
			
            distanceToWaypoint = transform.position.x - waypoint.position.x;
            timeToMove = Mathf.Abs(distanceToWaypoint / speed);
        }

		if (pullTime <= timeToMove) {

			horizontal = distanceToWaypoint > 0 ? -1 : 1;
			rb.velocity = move;
			anim.SetBool ("New Bool", true);
		} else if (pullTime >= timeToMove && pullTime <= timeToMove+1) {

			if (!pulou) {

				FMODUnity.RuntimeManager.PlayOneShot (somJump, transform.position);
				pulou = true;
			}

			anim.SetBool ("New Bool", false);
			anim.SetBool ("New Bool 0", true);
	

		} else if (pullTime > timeToMove + 1) {
			
			pulou = false;
			anim.SetBool ("New Bool 0", false);
			StartCoroutine (tempoDrop ());
			skillCount = 0;
			pullTime = 0;
			movingCount = 0;
			currentState = 0;

		}
	}

    IEnumerator tempoDrop() {

        yield return new WaitForSeconds(1f);

        for (int i = 0; i < dropLixoTransform.Length; i++) {
            Instantiate(dropLixoObject, dropLixoTransform[i].position, Quaternion.identity);
        }
    }

    void stunState() {

		stunTime += Time.fixedDeltaTime;
        //Ativa animação.

		if (stunTime >= 3f) {

			stunTime = 0;
			currentState = 0;
		}
	}

	void OnDestroy() {
		
		idleLixosoEv.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);

	}
}