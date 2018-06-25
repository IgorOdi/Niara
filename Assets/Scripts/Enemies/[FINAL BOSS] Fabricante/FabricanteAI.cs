using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FabricanteAI : EnemyBehaviour {
	
	[SerializeField]
	private int currentState;

	private float idleTime;
	private float bengalaTime;
	private float hatGunTime;

	private int HatGunCount;
	private int bengalaCount;

	public static bool traficanteIsDead;

	[SerializeField]
	private GameObject bengalaHit;
	[SerializeField]
	private Transform shotSpawn;
	[SerializeField]
	private GameObject shot;

	[FMODUnity.EventRef]
	public string somBengala = "event:/somJump";

	[FMODUnity.EventRef]
	public string somTiro = "event:/somTiro";

	[FMODUnity.EventRef]
	public string somIdle = "event:/Idle";
	public static FMOD.Studio.EventInstance idleTraficanteEv;

	private bool idlou, atirou, bateu;

	void Start() {

		vivo = true;
		vidasMax = 6;
		vidas = vidasMax;
		vulneravel = true;
		boss = true;
		danoBase = 1;

		idleTraficanteEv = FMODUnity.RuntimeManager.CreateInstance (somIdle);
	}

	void FixedUpdate() {

		switch (currentState) {

		case 0:
			idleState ();
			break;

		case 1:
			bengalaState ();
			break;

		case 2:
			hatGunState ();
			break;

		case 3:
			deadState ();
			break;
		}

		if (!vivo) {

			currentState = 3;
		}
	}

	void changeState() {

		if (vivo) {
			
			if (distancia < 5 && distancia > -4) {

				currentState = 1;
			} else {

				currentState = 2;
			}
		}
	}

	void idleState() {

		idleTime += Time.fixedDeltaTime;
		flip ();

		if (idleTime > 3) {

			idleTraficanteEv.stop (FMOD.Studio.STOP_MODE.IMMEDIATE);
			idlou = false;
			idleTime = 0;
			changeState ();
		} else {

			if (!idlou) {

				idleTraficanteEv.start ();
				idlou = true;
			}

		}
	}

	void bengalaState() {

		bengalaTime += Time.fixedDeltaTime;

		if (bengalaTime < 1) {

			anim.SetBool ("New Bool", true);
		} else if (bengalaTime > 1.2f && bengalaTime < 1.5f) {

			if (!bateu) {

				FMODUnity.RuntimeManager.PlayOneShot (somBengala, transform.position);
				bateu = true;
			}

			bengalaHit.SetActive (true);
		} else if (bengalaTime > 1.5f) {

			anim.SetBool ("New Bool", false);
			bateu = false;
			bengalaHit.SetActive (false);
			bengalaTime = 0;
			currentState = 0;
		}
	}

	void hatGunState() {

		hatGunTime += Time.fixedDeltaTime;
		flip ();

		if (hatGunTime > 0 && hatGunTime < 1.5f) {

			anim.SetBool ("New Bool 0", true);
		} else if (hatGunTime > 1.5f && hatGunTime < 1.7f) {

			if (HatGunCount < 3) {

				if (!atirou) {

					FMODUnity.RuntimeManager.PlayOneShot (somTiro, transform.position);
					atirou = true;
				}

				atirou = false;
				Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
				hatGunTime = 0f;
				HatGunCount++;
			} else {

				anim.SetBool ("New Bool 0", false);
				Instantiate (shot, shotSpawn.position, shotSpawn.rotation);

				if (!atirou) {

					FMODUnity.RuntimeManager.PlayOneShot (somTiro, transform.position);
					atirou = true;
				}

				atirou = false;
				HatGunCount = 0;
				hatGunTime = 0;
				currentState = 0;
			}

		} else if (hatGunTime > 1.7f) {


			anim.SetBool ("New Bool 0", false);
		}
	}

	void deadState() {

		anim.SetTrigger ("New Trigger");

		transform.localScale = new Vector3 (-1, 1, 1);
		rb.velocity = new Vector2 (10, rb.velocity.y);;
		Physics2D.IgnoreCollision (GetComponent<Collider2D> (), player.GetComponent<Collider2D>());
	}

	void OnCollisionEnter2D(Collision2D other) {

		if (currentState == 3) {

			if (other.gameObject.tag == "Untagged") {
				
				traficanteIsDead = true;
				Destroy (gameObject, 0f);
			}
		}
	}

	void OnDestroy() {
		
		idleTraficanteEv.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);

	}

}