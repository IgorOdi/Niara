using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZumbiShooter : EnemyBehaviour {

	[SerializeField]
	private GameObject tiroSpawn;
	[SerializeField]
	private GameObject tiro;

	private float idleTime;
	private float vomitTime;
	private float vomitShooterTime;

	private int vomitCounter;

	[SerializeField]
	private GameObject moido;

	private bool vomitou;

	[FMODUnity.EventRef]
	public string somVomito = "event:/Inimigos/impactoVomito3";

	public override void Start() {

		vivo = true;
		vidas = 2;
		vulneravel = true;
	}

	private void FixedUpdate() {

		if (vivo) {

			if (ativo) {

//				flip ();

				if (ativo) {

					vomitState ();
				}
			}
		} else {

			if (!vivo) {

				StartCoroutine (preMorte ());
			}
		}
	}

	void vomitState() {

		vomitTime += Time.fixedDeltaTime;

		if (vomitTime > 0 && vomitTime < 2) {

			vomitou = false;

		} else if (vomitTime > 2 && vomitTime <= 3) {
			
			anim.SetBool ("New Bool", true);

			if (vomitTime >= 2.4f && !vomitou) {

				FMODUnity.RuntimeManager.PlayOneShot (somVomito);
				GameObject vomit = Instantiate (tiro, tiroSpawn.transform.position, tiroSpawn.transform.rotation) as GameObject;
				vomit.GetComponent<Vomito> ().enemy = transform;
				vomitTime = 0;
				vomitou = true;
				anim.SetBool ("New Bool", false);
			}
		}
	}

	IEnumerator respawnEnemy() {

		GetComponent<Collider2D> ().enabled = false;
		rb.constraints = RigidbodyConstraints2D.FreezePosition;

		moido.SetActive (true);

		for (int i = 0; i < GetComponentsInChildren<SkinnedMeshRenderer> ().Length; i++) {

			GetComponentsInChildren<SkinnedMeshRenderer> () [i].enabled = false;
		}

		yield return new WaitForSeconds (10f);
		vivo = true;
		vidas = 2;
		GetComponent<Collider2D> ().enabled = true;
		rb.constraints = RigidbodyConstraints2D.FreezeRotation;

		moido.SetActive (false);

		for (int i = 0; i < GetComponentsInChildren<SkinnedMeshRenderer> ().Length; i++) {

			GetComponentsInChildren<SkinnedMeshRenderer> () [i].enabled = true;
		}
	}
}
