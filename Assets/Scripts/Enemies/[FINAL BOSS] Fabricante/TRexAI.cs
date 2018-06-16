using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TRexAI : EnemyBehaviour {

	private BarraInimigo barraInimigo;

	public static bool endGame;

	private bool canStartKilling;

	private float speed;
	[SerializeField]
	private int currentState;

	private float idleTime;
	private float laserTime;
	private float laserBTime;
	private float laserCTime;
	private float misselTime;
	private float napalmTime;
	private float fireTime;
	private float tapaTime;

	private int laserCount;
	private int misselCount;
	private int misselCounter;

	private bool canNapalm;
	private int canNapalmCount;
	private float vidaPerc;

	private int[] usedPoints;

	private float f;
	private int sideToRotate;

	[SerializeField]
	private Transform laserSpawn;
	[SerializeField]
	private Transform misselSpawns;
	[SerializeField]
	private Transform[] napalmSpawns;

	[SerializeField]
	private GameObject laser;
	[SerializeField]
	private GameObject missel;
	[SerializeField]
	private GameObject rotatorB;
	[SerializeField]
	private GameObject napalm;
	[SerializeField]
	private GameObject tapa;

	[SerializeField]
	private GameObject[] moveLazor;

	[SerializeField]
	private GameObject fire;

	[FMODUnity.EventRef]
	public string somLaser = "event:/somLaser";

	[FMODUnity.EventRef]
	public string somLaserB = "event:/somLaserB";

	[FMODUnity.EventRef]
	public string somLaserC = "event:/somLaserC";

	[FMODUnity.EventRef]
	public string somFire = "event:/somFire";

	[FMODUnity.EventRef]
	public string somRoar = "event:/somRoar";

	[FMODUnity.EventRef]
	public string somMissil = "event:/somMissel";

	private bool laserA, laserB, laserC, shootFire, roar, missou;

	private float tempoExplosao;

	void Start() {

		vivo = true;
		vidasMax = 200;
		vidas = vidasMax;
		vulneravel = true;
		boss = true;
		danoBase = 1;

		speed = 3f;
		canNapalmCount = 3;
		canStartKilling = false;

		barraInimigo = GetComponent<BarraInimigo> ();
	}
		
	void FixedUpdate() {

		if (vivo) {
			
			if (canStartKilling) {
			
				switch (currentState) {

				case 0:
					idleState ();
					break;

				case 1:
					laserState ();
					break;

				case 2:
					laserBState ();
					break;

				case 3:
					laserCState ();
					break;

				case 4:
					misselState ();
					break;

				case 5:
					napalmState ();
					break;

				case 6:
					fireState ();
					break;

				case 7:
					tapaState ();
					break;
				}
			}
		}

		if (FabricanteAI.traficanteIsDead && transform.position.y < -1) {

			rb.velocity = new Vector2 (rb.velocity.x, speed);
			StartCoroutine (startKilling ());
		} else {
			
			rb.velocity = new Vector2 (0, 0);
		}

		vidaPerc = vidas / vidasMax;
		canNapalm = canNapalmCount > 3 ? true : false;

		if (vidas <= 0) {
			
			tempoExplosao += Time.deltaTime;


			endGame = true;
			rb.velocity = new Vector2 (0, -1f);
			Destroy (gameObject, 10f);

			if (tempoExplosao >= 0.2f) {
				
				Instantiate (destroy, new Vector2 (transform.position.x+Random.Range(-4, 4), transform.position.y + Random.Range(-4, 4))
					, Quaternion.identity);
				tempoExplosao = 0;
			}
		}
	}

	IEnumerator startKilling() {

		yield return new WaitForSeconds (6f);
		canStartKilling = true;
		barraInimigo.vidaInimigo = GameObject.Find ("Barra de vida Frente").GetComponent<Image> ();
		GameManager.traficanteDeadParam.setValue(1);
	}

	void changeState() {

		if (((vidaPerc < 0.9f && vidaPerc > 0.7f) || (vidaPerc < 0.6f && vidaPerc > 0.5f) || (vidaPerc < 0.4f && vidaPerc > 0.3f) || (vidaPerc < 0.2f && vidaPerc > 0f)) && canNapalm) {

			currentState = 5;
		} else {

			if (vidaPerc >= 0.6f) {

				if (distancia < 7 && distancia > -7) {

					int randomizador = Random.Range (0, 4);

					if (randomizador == 0) {

						currentState = 3;
					} else if (randomizador == 1) {

						currentState = 7;
					} else if (randomizador == 3) {

						currentState = 2;
					} else {

						currentState = 4;
					}
				} else {

					int randomizador = Random.Range (0, 3);

					if (randomizador == 0) {

						currentState = 3;
					} else if (randomizador == 1) {

						currentState = 6;
					} else {

						currentState = 1;
					}
				}

			} else if (vidaPerc < 0.6f) {

				if (distancia < 7 && distancia > -7) {

					int randomizador = Random.Range (0, 7);

					currentState = randomizador;


				} else {

					int randomizador = Random.Range (0, 3);

					switch (randomizador) {

					case 0:
						currentState = 1;
						break;

					case 1:
						currentState = 3;
						break;

					case 2:
						currentState = 4;
						break;

					case 3:
						currentState = 6;
						break;
					}
				}
			}
		}
	}

	void idleState() {

		idleTime += Time.fixedDeltaTime;

		if (idleTime > 1) {

			roar = false;
			idleTime = 0;
			changeState ();
		} else {

			if (!roar) {

				FMODUnity.RuntimeManager.PlayOneShot (somRoar);
				roar = true;
			}
		}
	}

	void laserState() {

		laserTime += Time.fixedDeltaTime;

		if (laserTime > 0 && laserTime < 1.5f) {

			if (anim.GetBool ("New Bool") == false) {

				anim.SetBool ("New Bool", true);
			}
				
		} else if (laserTime > 1.5f) {

			anim.SetBool ("New Bool", false);

			if (!laserA) {

				FMODUnity.RuntimeManager.PlayOneShot (somLaser);
				laserA = true;
			}

			laserA = false;
			Instantiate (laser, laserSpawn.position, laserSpawn.rotation);
			canNapalmCount++;
			laserTime = 0;
			currentState = 0;
		}
	}

	void laserBState() {

		laserBTime += Time.fixedDeltaTime;

		rotatorB.transform.rotation = Quaternion.Euler (0, 0, f);



		if (laserBTime > 0 && laserBTime < 1) {

		} else if (laserBTime > 1 && laserBTime < 2) {

			rotatorB.SetActive (true);
		} else if (laserBTime > 2 && laserBTime < 3.75f) {

			if (!laserB) {

				FMODUnity.RuntimeManager.PlayOneShot (somLaserB);
				laserB = true;
			}

			f += 2;
		} else if (laserBTime > 3.75f && laserBTime < 4.75) {

			laserB = false;
			rotatorB.SetActive (false);
		} else if (laserBTime > 4.75f && laserBTime < 7) {

			rotatorB.SetActive (true);
		} else if (laserBTime > 7 && laserBTime < 8.75f) {

			if (!laserB) {

				FMODUnity.RuntimeManager.PlayOneShot (somLaserB);
				laserB = true;
			}

			f -= 2;
		} else if (laserBTime > 9) {

			laserB = false;
			rotatorB.SetActive (false);
			canNapalmCount++;
			currentState = 0;
		}
	}

	void laserCState() {
		
		laserCTime += Time.fixedDeltaTime;

		for (int i = 0; i < moveLazor.Length; i++) {

			Vector2 pos = moveLazor [i].transform.position;

			if (moveLazor [i].transform.position.x < -15 || moveLazor [i].transform.position.x > 15) {

				pos.y = 9.9f;
			} else {

				pos.y = 3.7f;
			}

			moveLazor [i].transform.position = pos;
		}

		if (laserCTime > 0 && laserCTime < 1.5f) {

			if (anim.GetBool ("New Bool 2") == false) {

				anim.SetBool ("New Bool 2", true);
			}
				
		} else if (laserCTime > 1.5f && laserCTime < 8) {

			anim.SetBool ("New Bool 2", false);

			if (!laserC) {

				FMODUnity.RuntimeManager.PlayOneShot (somLaserC);
				laserC = true;
			}
				
			moveLazor [0].SetActive (true);
			moveLazor [0].transform.Translate (new Vector2 (0, -0.2f));

		} else if (laserCTime > 8 && laserCTime < 16) {
			
			if (laserC) {

				FMODUnity.RuntimeManager.PlayOneShot (somLaserC);
				laserC = false;
			}

			moveLazor [1].SetActive (true);
			moveLazor [1].transform.Translate (new Vector2 (0, 0.2f));

		} else if (laserCTime > 16) {

			moveLazor [0].transform.position = new Vector2 (-17, 3.7f);
			moveLazor [1].transform.position = new Vector2 (17, 3.7f);

			moveLazor [0].SetActive (false);
			moveLazor [1].SetActive (false);

			laserC = false;

			canNapalmCount++;
			laserCTime = 0;
			currentState = 0;
		}
	}

	void misselState() {

		misselTime += Time.fixedDeltaTime;

		if (misselTime > 0 && misselTime < 1.9f) {

			if (anim.GetBool ("New Bool 0") == false) {

				anim.SetBool ("New Bool 0", true);
			}
				
		} else if (misselTime > 1.9f) {

			if (misselCount < 2) {

				if (!missou) {

					FMODUnity.RuntimeManager.PlayOneShot (somMissil);
					missou = true;
				}

				missou = false;
				Instantiate (missel, misselSpawns.position, misselSpawns.rotation);
				misselCount++;
				misselTime = 0;
				anim.SetBool ("New Bool 0", false);
			} else {

				missou = false;
				anim.SetBool ("New Bool 0", false);
				misselCount = 0;
				misselTime = 0;
				currentState = 0;
			}
		}
	}
		
	void napalmState() {

		napalmTime += Time.fixedDeltaTime;

		if (napalmTime < 1) {

		} else {
			int random = Random.Range (0, 4);

			if (napalmSpawns [random].position != null) {

				Instantiate (napalm, napalmSpawns [random].position, Quaternion.identity);
				DestroyImmediate (napalmSpawns [random].gameObject);
			}
				
			canNapalmCount = 0;
			napalmTime = 0;
			currentState = 0;
			canNapalm = false;
		}
	}

	void fireState() {

		fireTime += Time.fixedDeltaTime;

		if (fireTime > 0 && fireTime < 1.2f) {

			if (anim.GetBool ("New Bool 1") == false) {

				anim.SetBool ("New Bool 1", true);
			}
		} else if (fireTime > 1.4f && fireTime < 2.2f) {

			if (!shootFire) {

				FMODUnity.RuntimeManager.PlayOneShot (somFire);
				shootFire = true;
			}

			fire.SetActive (true);
		} else if (fireTime > 2.2f) {

			anim.SetBool ("New Bool 1", false);
			shootFire = false;
			fire.SetActive (false);
			fireTime = 0;
			currentState = 0;
		}
	}

	void tapaState() {

		tapaTime += Time.deltaTime;

		if (tapaTime > 0 && tapaTime < 1.2f) {

			if (anim.GetBool ("New Bool 3") == false) {
				
				anim.SetBool ("New Bool 3", true);
			}
		} else if (tapaTime > 1.5f && tapaTime < 2) {

			tapa.SetActive (true);
		} else if (tapaTime > 2f) {

			anim.SetBool ("New Bool 3", false);

			tapa.SetActive (false);
			tapaTime = 0;
			currentState = 0;
		}
	}
}