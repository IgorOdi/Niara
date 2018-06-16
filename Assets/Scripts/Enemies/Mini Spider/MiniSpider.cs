using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniSpider : EnemyBehaviour {

	private int currentState;

	private float hitTime;
	[SerializeField]
	private bool canHit;

	private float speed;
	private float horizontal;

	private Vector2 move;
	[SerializeField]
	private GameObject arma;
	[SerializeField]
	private Transform detectaChao;

	private bool andou;
	private bool voiceou;

	[FMODUnity.EventRef]
	public string somPassos = "event:/Inimigos/spiderWalk";
	public static FMOD.Studio.EventInstance passosAranhaEv;

	[FMODUnity.EventRef]
	public string danielVoice = "event:/Inimigos/danielVoice";

	void Start() {

		vivo = true;
		vidas = 1;
		vulneravel = true;

		currentState = 0;

		speed = 4;
		horizontal = 1;

		danoBase = 1;
		canHit = true;

		passosAranhaEv = FMODUnity.RuntimeManager.CreateInstance (somPassos);
	}

	void FixedUpdate()
	{

		switch (currentState)
		{

		case 0:

			waitState();
			break;

		case 1:

			chaseState();
			break;

		case 2:

			hitState();
			break;
		}

		horizontal = lado;
		changeState();

		if (transform.position.y <= -6) {

			move = new Vector2 (horizontal * speed, rb.velocity.y);

		} else {

			move = new Vector2 (horizontal * speed, -25);
		}
	}

	void changeState() {

		if (vivo) {

			flip();

			if (!canHit) {

				currentState = 0;
			}
			else if ((distancia < 3 && distancia > -3) && (canHit)) {

				currentState = 2;
			}
			else if (canHit) {

				currentState = 1;
			}
			else {

				currentState = 0;
			}
		} else {

			currentState = 0;
			anim.SetTrigger ("New Trigger 0");
			StartCoroutine (morteCooldown ());
		}
	}

	void waitState() {

		passosAranhaEv.release ();
		passosAranhaEv.stop (FMOD.Studio.STOP_MODE.IMMEDIATE);
		anim.SetBool("New Bool", false);
		rb.velocity = new Vector2 (0, 0);

		if (!vivo) {

			GetComponent<Collider2D> ().enabled = false;
			rb.gravityScale = 0;
		}
	}

	void chaseState() {

		arma.SetActive(false);

		rb.velocity = new Vector2 (horizontal * speed, rb.velocity.y);
		anim.SetBool ("New Bool", true);

			if (!andou) {

				passosAranhaEv.start ();
				andou = true;

		} else {

			passosAranhaEv.release ();
			passosAranhaEv.stop (FMOD.Studio.STOP_MODE.IMMEDIATE);
			andou = false;
			anim.SetBool ("New Bool", false);
		}
	}

	void hitState() {

		hitTime += Time.fixedDeltaTime;
		passosAranhaEv.stop (FMOD.Studio.STOP_MODE.IMMEDIATE);

		if (hitTime < 0.1f) {

			anim.SetTrigger("New Trigger");
		} else if (hitTime > 0.6f && hitTime < 0.75f) {

			if (!voiceou) {

				FMODUnity.RuntimeManager.PlayOneShot (danielVoice, transform.position);
				voiceou = true;
			}

			arma.SetActive(true);
		}
		else if (hitTime > 0.75f) {

			voiceou = false;
			arma.SetActive(false);
			canHit = false;
			currentState = 0;
			StartCoroutine(hitCooldown());
			hitTime = 0;
		}
	}

	IEnumerator hitCooldown() {

		yield return new WaitForSeconds(2f);
		canHit = true;
	}

	IEnumerator morteCooldown() {

		yield return new WaitForSeconds (6f);
		Instantiate (destroy, transform.position, Quaternion.identity);
		DestroyImmediate (gameObject);
	}
}
