using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abutre : EnemyBehaviour {

	private float idleTime;
	private float movingTime;
	private float windTime;
	private float shootingTime;
	private float penaTime;

	[SerializeField]
	private int windCount;
	[SerializeField]
	private int shootCount;
	[SerializeField]
	private int penaCount;

	[SerializeField]
	private int currentState;

	private int randomizador;

	[SerializeField]
	private GameObject wind;
	[SerializeField]
	private GameObject pena;
	[SerializeField]
	private Transform[] penaSpawn;
	[SerializeField]
	private GameObject egg;
	[SerializeField]
	private Transform eggSpawn;

	[SerializeField]
	private Transform waypoint;

	private bool ventou;
	private bool shootou;
	private bool voo;
	private bool chocouBall;

	[FMODUnity.EventRef]
	public string somWind = "event:/Wind";

	[FMODUnity.EventRef]
	public string somShoot = "event:/Shooting";

	[FMODUnity.EventRef]
	public string somPena = "event:/Pena";

	[FMODUnity.EventRef]
	public string somPenaStart = "event:/Pena";

	void Start() {

		vivo = true;
		vidasMax = 40;
		vidas = vidasMax;
		vulneravel = true;

		danoBase = 1;
        boss = true;
	}

	void FixedUpdate() {

		switch (currentState) {

		case 0:
			idleState ();
			break;

		case 1:
			//movingState ();
			break;

		case 2:
			windState ();
			break;

		case 3:
			shootingState ();
			break;

		case 4:
			alturaState ();
			break;

		case 5:
			penaState ();
			break;
		}

		if (!vivo) {

			Morte ();
		}
	}

	void changeState() {

		if (transform.position.y < waypoint.position.y-2) {

			if (shootCount <= 2 && windCount <= 2) {

				randomizador = Random.Range (0, 2);

				if (randomizador == 0) {

					currentState = 2;
				} else {

					currentState = 3;
				}
			} else {

				currentState = 4;
			}
		} else {

			if (penaCount < 10) {

				currentState = 5;
			} else {

				if (shootCount <= 1) {

					currentState = 3;
				} else {

					currentState = 4;
					penaCount = 0;
				}
			}
		}
	}

	void idleState() {

		idleTime += Time.fixedDeltaTime;
		anim.SetTrigger ("Idle");

		if (idleTime >= 2f) {

			flip ();
			idleTime = 0;
			changeState ();
		}
	}

	void windState() {

		windTime += Time.fixedDeltaTime;

		if (windTime > 0 && windTime < 2) {

			anim.SetBool ("AirPush", true);
		} else if (windTime >= 2f && windTime < 3.2f) {

			if (!ventou) {

				FMODUnity.RuntimeManager.PlayOneShot (somWind);
				ventou = true;
			}

			wind.SetActive (true);
		} else if (windTime > 3.2f) {

			ventou = false;

			anim.SetBool ("AirPush", false);
			wind.SetActive (false);
			windCount++;
			windTime = 0;
			currentState = 0;
		}
	}

	void shootingState() {

		shootingTime += Time.fixedDeltaTime;

		if (shootingTime > 0 && shootingTime < 1f) {

			anim.SetBool ("Caw", true);
		} else if (shootingTime >= 2f) {

			if (!chocouBall) {

				FMODUnity.RuntimeManager.PlayOneShot (somShoot, transform.position);
				chocouBall = true;
			}

			anim.SetBool ("Caw", false);
			Instantiate (egg, eggSpawn.position, eggSpawn.rotation);
			shootCount++;
			shootingTime = 0;
			currentState = 0;
			chocouBall = false;
		}
	}

	void alturaState() {

		if (transform.position.y < waypoint.position.y-1) {

			currentState = 5;
		} else {

			rb.gravityScale = 1;
			shootCount = 0;
			currentState = 0;
		}
	}

	void penaState() {

		penaTime += Time.fixedDeltaTime;
		rb.gravityScale = 0;

		if (penaTime < 2) {

			if (!voo) {

				FMODUnity.RuntimeManager.PlayOneShot (somPenaStart, transform.position);
				voo = true;
			}
				
			anim.SetBool ("Super", true);
			transform.position = Vector2.Lerp (transform.position, waypoint.position, penaTime/10);

		} else {

			transform.localScale = new Vector3 (1, 1, 1);

			if (!shootou) {

				FMODUnity.RuntimeManager.PlayOneShot (somPena, transform.position);
				shootou = true;
			}

			for (int i = 0; i <= 5; i++) {
			
				Instantiate (pena, penaSpawn [i].transform.position, penaSpawn [i].transform.rotation);
				penaCount++;
				shootCount = 0;
				windCount = 0;
				penaTime = 0;
				currentState = 0;

				anim.SetBool ("Super", false);
			}

			voo = false;
			shootou = false;
		}
	}
}