using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZumbiJumper : EnemyBehaviour {

	private int currentState;

	private float idleTime;
	private float jumpTime;

	private Vector2 jumpForce;

	private bool grounded;

	private bool idlou;

	[SerializeField]
	private GameObject moido;

	[FMODUnity.EventRef]
	public string somIdle = "event:/Inimigos/idleZumbi";
	public static FMOD.Studio.EventInstance idleZumbiEv;

	void Start() {

		vivo = true;
		vidas = 2;
		vulneravel = true;
		currentState = 0;

		rb.gravityScale = 0;

		anim.SetBool ("New Bool", true);

		idleZumbiEv = FMODUnity.RuntimeManager.CreateInstance (somIdle);
	}

	void FixedUpdate() {

		grounded = Physics2D.OverlapBox (new Vector2(transform.position.x, transform.position.y-0.75f), new Vector2 (1, 1), 0, LayerMask.GetMask("Piso"));

		if (vivo) {
			if (ativo) {
				switch (currentState) {

				case 0: 
					idleState ();
					break;

				case 1:
					jumpState ();
					break;
				}
			} 
		} else {

			StartCoroutine (respawnEnemy ());
		}
	}

//	void OnDrawGizmos() {
//
//		Gizmos.DrawCube(new Vector2(transform.position.x, transform.position.y-0.75f), new Vector2(1, 1));
//	}

	void idleState() {

		idleTime += Time.fixedDeltaTime;

		if (!idlou) {

			idleZumbiEv.start();
			idlou = true;
		}

		if (idleTime > 2) {

			if (grounded) {

				idleTime = 0;
				currentState = 1;
			} else {

				if (distancia <= 6 && distancia >= -6) {

					anim.SetBool ("New Bool", false);
					rb.gravityScale = 1;
					idleTime = 0;
				} else {

					rb.gravityScale = 0;
				}
			}

			flip ();
		} else if (idleTime > 1 && idleTime < 2) {

			GetComponent<Collider2D> ().offset = new Vector2(0, 0.5f);
		}
	}

	void jumpState() {

		jumpTime += Time.fixedDeltaTime;

		if (jumpTime < 1.5f) {

			flip ();
			anim.SetTrigger ("New Trigger 0");
		} else {
			
			anim.SetTrigger ("New Trigger 1");
			jumpForce = new Vector2 ((-distancia/2) * 120000, 500000);
			rb.AddForce (jumpForce);
			jumpTime = 0;
			currentState = 0;
		}
	}

	IEnumerator respawnEnemy() {

		GetComponent<Collider2D> ().enabled = false;
		rb.constraints = RigidbodyConstraints2D.FreezePosition;

		moido.SetActive (true);

		for (int i = 0; i < GetComponentsInChildren<SkinnedMeshRenderer> ().Length; i++) {

			GetComponentsInChildren<SkinnedMeshRenderer> () [i].enabled = false;
		}

		yield return new WaitForSeconds (6f);
		vivo = true;
		vidas = 2;
		GetComponent<Collider2D> ().enabled = true;
		rb.constraints = RigidbodyConstraints2D.FreezeRotation;

		moido.SetActive (false);

		for (int i = 0; i < GetComponentsInChildren<SkinnedMeshRenderer> ().Length; i++) {

			GetComponentsInChildren<SkinnedMeshRenderer> () [i].enabled = true;
		}
	}

	void OnCollisionEnter2D(Collision2D other) {

		if (other.gameObject.tag == "Player" || other.gameObject.tag == "Chão") {

			GetComponent<Collider2D> ().offset = new Vector2(0, 1f);
			anim.SetTrigger ("New Trigger");
		}
	}
}
