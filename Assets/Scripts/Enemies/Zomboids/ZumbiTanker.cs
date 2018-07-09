using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZumbiTanker : EnemyBehaviour {

	private int currentState;

	private float hitTime;
	private bool canHit;

	private float waitTime;
	private float moveTime;
	private float speed;
	private float horizontal;

	private bool canSpawn;

	private Vector2 move;
	[SerializeField]
	private GameObject porradeira;

	[SerializeField]
	private Transform detectaChao;

	[SerializeField]
	private GameObject moido;
	[SerializeField]
	private GameObject shooter;

	private bool inIdle, atacou;

	[FMODUnity.EventRef]
	public string somIdle = "event:/Inimigos/idle3";

	[FMODUnity.EventRef]
	public string somAtaque = "event:/Inimigos/attack2";

	public override void Start() {

		vivo = true;
		vidas = 6;
		vulneravel = true;

		currentState = 0;

		speed = 2;
		horizontal = 0;

		danoBase = 1;
		canHit = true;
		quebraLanca = true;
		canSpawn = true;
	}

	void FixedUpdate() {

		switch (currentState) {

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
		flip();

		move = new Vector2(horizontal * speed, rb.velocity.y);
	}

	void changeState() {

		if (vivo) {

			if ((!ativo) || (!canHit)) {

				currentState = 0;
			} else if ((distancia < 3 && distancia > -3) && (canHit)) {

				currentState = 2;
			} else if (canHit) {

				currentState = 1;
			} else {

				currentState = 0;
			}
		} else {

			StartCoroutine (preMorte ());
		}
	}

	void waitState() {

		waitTime += Time.deltaTime;
		anim.SetBool("New Bool", false);

		if (waitTime > 1) {
			
			changeState ();
			waitTime = 0;
		}
	}

	void chaseState() {

		moveTime += Time.deltaTime;
		porradeira.SetActive(false);

		if (!inIdle) {

			FMODUnity.RuntimeManager.PlayOneShot (somIdle);
			inIdle = true;
		}

		if (moveTime < 1) {

			if (Physics2D.OverlapBox (detectaChao.position, new Vector2 (3, 3), 0, LayerMask.GetMask ("Piso"))) {
			
				rb.velocity = move;
				anim.SetBool ("New Bool", true);
			} else {

				anim.SetBool ("New Bool", false);
			}
		} else {

			moveTime = 0;
			changeState ();
		}
	}

	void hitState() {

		hitTime += Time.fixedDeltaTime;
		anim.SetBool("New Bool", false);

		inIdle = false;

		if (hitTime < 1 && hitTime > 1) {

		} else if (hitTime > 1.1f && hitTime < 1.6f) {

			if (!atacou) {

				FMODUnity.RuntimeManager.PlayOneShot (somAtaque);
				atacou = true;
			}

			anim.SetBool ("New Bool 0", true);
		} else if (hitTime > 1.8f && hitTime < 2f) {

			porradeira.SetActive(true);
		} else if (hitTime > 2f) {

			atacou = false;
			porradeira.SetActive(false);
			canHit = false;
			currentState = 0;
			hitTime = 0;
			anim.SetBool ("New Bool 0", false);
			StartCoroutine(hitCooldown());
			changeState ();
		}
	}

	IEnumerator hitCooldown() {

		anim.SetBool ("New Bool 0", false);
		yield return new WaitForSeconds(2f);
		canHit = true;
	}

	void spawn() {

		if (canSpawn) {

//			StartCoroutine (spawnEnemy ());
			canSpawn = false;

			moido.SetActive (true);

			for (int i = 0; i < GetComponentsInChildren<SkinnedMeshRenderer> ().Length; i++) {

				GetComponentsInChildren<SkinnedMeshRenderer> () [i].enabled = false;
			}
		}
	}

	IEnumerator spawnEnemy() {

		yield return new WaitForSeconds (1f);
		Instantiate(shooter, new Vector2(transform.position.x + 2, transform.position.y), Quaternion.identity);
		Instantiate(shooter, new Vector2(transform.position.x - 2, transform.position.y), Quaternion.identity);

		Instantiate (destroy, transform.position, Quaternion.identity);
		Instantiate (alma, transform.position, Quaternion.identity);
		DestroyImmediate (gameObject);
	}
}
