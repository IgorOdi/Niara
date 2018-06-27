using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TatuStates : EnemyBehaviour {

    private float speed;
    private float rollingSpeed;
    private float horizontal;

	private Vector2 move;
	private Vector2 rollingMove;

    public int currentState;

    private float idleTime;
    private float movingTime;
    private float hittingTime;
    private float rollingTime;
    private float divingTime;
    private float jumpBackTime;
    private float superIdleTime;

    private int hittingCount;
    private int rollingCount;
    private int divingCount;

    private int randomizador;
    private bool canRoll;

    [SerializeField]
    private GameObject hits;
	[SerializeField]
	private GameObject barrigadaCollider;

	// Sons //

	private bool passosStarted;
	private bool jumped;
	private bool rolou;
	private bool socou;
	private bool caiu;

	[FMODUnity.EventRef]
	public string somPassos = "event:/Passos";
	public static FMOD.Studio.EventInstance passosTatuEv;

	[FMODUnity.EventRef]
	public string somPulo = "event:/Pulo";

	[FMODUnity.EventRef]
	public string somQueda = "event:/Queda";

	[FMODUnity.EventRef]
	public string somSoco = "event:/Soco";

	[FMODUnity.EventRef]
	public string somRolar = "event:/Rolar";
	public static FMOD.Studio.EventInstance rolarTatuEv;

    private void Start() {

        vivo = true;
        vidasMax = 30;
		vidas = vidasMax;
        vulneravel = true;

        horizontal = 0;
        speed = 3;
        rollingSpeed = 12;
        currentState = 0;

		danoBase = 1;
        boss = true;

		passosTatuEv = FMODUnity.RuntimeManager.CreateInstance(somPassos);
		rolarTatuEv = FMODUnity.RuntimeManager.CreateInstance (somRolar);
    }

    void FixedUpdate() {

        switch (currentState) {

            case 0:

                idleState();
                break;

            case 1:

                movingState();
                break;

            case 2:

                hittingState();
                break;

            case 3:

                rollingState();
                break;

            case 4:

                divingState();
                break;

            case 5:

                jumpBackState();
                break;

            case 6:

                superIdleState();
                break;
        }

        if (!vivo) {

            Morte();
        }

		move = new Vector2(horizontal * speed, 0);
		rollingMove = new Vector2 (horizontal * rollingSpeed, 0);
    }

    void changeState() {

        if (vivo) {

            if (distancia < 5 && distancia > -5 && hittingCount < 1) {

                currentState = 2;
            } else if (distancia > 8 || distancia < -8) {

                randomizador = Random.Range(0, 2);

                if (randomizador == 0 && rollingCount <= 2) {

                    currentState = 3;
                } else {

                    currentState = 1;
                }
            } else if (rollingCount > 3 && divingCount > 2) {

                currentState = 6;
            }  else {

                if (divingCount < 2) {

					randomizador = Random.Range (0, 2);

					if (randomizador == 0) {
						
						currentState = 4;
					} else {
						
						if (rollingCount < 3) {
							
							currentState = 3;
						} else {
							
							currentState = 4;
							rollingCount = 0;
						}
					}

					hittingCount = 0;
                } else if (rollingCount < 3) {
					
					randomizador = Random.Range (0, 2);

					if (randomizador == 0) {

						currentState = 3;
					} else {

						if (divingCount < 2) {

							currentState = 4;
						} else {

							currentState = 3;
							rollingCount = 0;
						}
					}

					hittingCount = 0;
                } else {

                    currentState = 5;
                }
            }
        }
    }

    new void flip() {

        Vector2 scale = transform.localScale; //Vector2 que pega o localScale do inimigo.
	    scale.x = -lado; //X do localScale multiplicado por -1...
		transform.localScale = scale; //Torna o localScale atual no inimigo(Flip).
    }

    void idleState() {

        idleTime += Time.deltaTime;
        horizontal = lado;

        move = new Vector2(0,0);

		if (idleTime < 1) {

			anim.SetTrigger ("Idle");
		} else {

            flip();
            idleTime = 0;
            changeState();
        }
    }

    void movingState() {

        movingTime += Time.deltaTime;

        if (movingTime < 2) {

			if (!passosStarted) {
				passosTatuEv.start ();
				passosStarted = true;
			}

			anim.SetTrigger ("Walk");
			rb.velocity = move;
        } else {

			passosTatuEv.stop (FMOD.Studio.STOP_MODE.IMMEDIATE);
			passosStarted = false;
            movingTime = 0;
            currentState = 0;
        }
    }

    void hittingState() {

        hittingTime += Time.fixedDeltaTime;

		if (hittingTime > 1 && hittingTime < 1.1f) {

		} else if (hittingTime > 1.1f && hittingTime < 1.7f) {

			anim.SetBool ("Tapa", true);
		} else if (hittingTime > 2.1f && hittingTime < 2.6f) {

			if (!socou) {

				FMODUnity.RuntimeManager.PlayOneShot (somSoco, transform.position);
				socou = true;
			}

			hits.SetActive (true);
		} else if (hittingTime > 2.6f) {

			socou = false;
			anim.SetBool ("Tapa", false);
			hits.SetActive (false);
			hittingTime = 0;
			currentState = 0;
			hittingCount++;
		}
    }

    void rollingState() {

        rollingTime += Time.deltaTime;

        if (rollingTime < 0.2f) {

			if (!jumped) {

				FMODUnity.RuntimeManager.PlayOneShot (somPulo, transform.position);
				jumped = true;
			}

            anim.SetBool("Girar", true);
            canRoll = true;
            rb.AddForce(new Vector2(0, 80000));

        } else if (rollingTime > 1f) {

            if (canRoll) {

				if (!rolou) {

					rolarTatuEv.start ();
					rolou = true;
				}

                rb.velocity = rollingMove;
                rb.gravityScale = 1;
            } else {

				rolarTatuEv.stop (FMOD.Studio.STOP_MODE.IMMEDIATE);
				rolou = false;
				jumped = false;

                anim.SetBool("Girar", false);
                rollingCount++;
				rollingTime = 0;
                currentState = 0;
            }
        }
    }

    void divingState() {

        divingTime += Time.deltaTime;

		if (divingTime < 0.3f) {

			if (!jumped) {

				FMODUnity.RuntimeManager.PlayOneShot (somPulo, transform.position);
				jumped = true;
			}
				
			anim.SetBool ("Barrigada", true);
			rb.AddForce (new Vector2 (6000 * -(distancia / 2), 80000));
		} else if (divingTime > 0.3f && divingTime < 0.6f) {

			barrigadaCollider.SetActive (true);
		} else if (divingTime > 1f && divingTime < 1.5f) {



			//anim.SetBool ("Barrigada", false);
		} else if (divingTime > 1.5f) {

			if (!caiu) {

				FMODUnity.RuntimeManager.PlayOneShot (somQueda, transform.position);
				caiu = true;
			}

			jumped = false;
			caiu = false;
            anim.SetBool("Barrigada", false);
            barrigadaCollider.SetActive (false);
			divingTime = 0;
			divingCount++;
			currentState = 0;
		}
    }

    void jumpBackState() {

        jumpBackTime += Time.deltaTime;

        if (jumpBackTime < 1) {

            rb.AddForce(new Vector2(5000 * -horizontal, 0));
        } else {

            if (rollingCount >= 3) {

                rollingCount = 0;
            }

            if (divingCount >= 2) {

                divingCount = 0;
            }

            jumpBackTime = 0;
            currentState = 0;
        }

    }

    void superIdleState() {

        superIdleTime += Time.deltaTime;
        move = new Vector2(0, 0);

        if (superIdleTime > 3f) {

            superIdleTime = 0;
            hittingCount = 0;
            rollingCount = 0;
            divingCount = 0;
            currentState = 0;
        } 
    }

    void OnCollisionEnter2D(Collision2D other) {

        if (other.gameObject.tag == "LimiteTatu") {

            canRoll = false;
        }
    }

	void OnDestroy() {
		
		passosTatuEv.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);

	}
}
