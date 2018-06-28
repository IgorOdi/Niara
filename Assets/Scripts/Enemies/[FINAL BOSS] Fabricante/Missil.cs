using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missil : ProjectileBase {

	private Transform player;
	private Vector3 startPos;
	private float myTime;
	private int dano;

	private float tempoDestruir;

	[FMODUnity.EventRef]
	public string somSeguir = "event:/Seguir";
	public static FMOD.Studio.EventInstance somSeguirEv;

	[FMODUnity.EventRef]
	public string somDestruir = "event:/Destruir";

	private bool playou, destruiu;

	void Start() {

		startPos = transform.position;
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform>();
		dano = 1;

		somSeguirEv = FMODUnity.RuntimeManager.CreateInstance (somSeguir);
	}

	void Update() {

		tempoDestruir += Time.deltaTime;

		if (tempoDestruir >= 3) {

			somSeguirEv.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
			somSeguirEv.release();

			if (!destruiu) {

				FMODUnity.RuntimeManager.PlayOneShot (somDestruir);
				somSeguirEv.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
				somSeguirEv.release();
				destruiu = true;
			}

			Destructor ();
		}

		myTime += Time.fixedDeltaTime;

		if (myTime < 0.5f) {

			transform.rotation = Quaternion.Euler (0, 0, 90);
			transform.Translate (new Vector2 (0.2f, 0));
		} else if (myTime > 0.5f) {

			if (!playou) {

				somSeguirEv.start ();
				playou = true;
			}

			transform.Translate (new Vector2 (0.12f, 0));

			var Z = Mathf.Atan2 (player.transform.position.y - transform.position.y, player.transform.position.x - transform.position.x) * Mathf.Rad2Deg;

			transform.rotation = Quaternion.Euler (0, 0, Z);
		}
	}

	void OnTriggerEnter2D(Collider2D other) {

		if (other.gameObject.tag == "Player") { //com o Player,

			PlayerController.Damage (dano, false);
				
			somSeguirEv.stop (FMOD.Studio.STOP_MODE.IMMEDIATE);
			playou = false;

			if (!destruiu) {

				FMODUnity.RuntimeManager.PlayOneShot (somDestruir);
				destruiu = true;
			}

			PlayerController.recebeKnockBack = true;
			Destructor ();

		} else if (other.gameObject.tag == "Chão") {

			if (!destruiu) {

				FMODUnity.RuntimeManager.PlayOneShot (somDestruir);
				destruiu = true;
			}

			somSeguirEv.stop (FMOD.Studio.STOP_MODE.IMMEDIATE);
			playou = false;

			Destructor ();
		}


	}

   void OnDestroy ()
	{
		somSeguirEv.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        somSeguirEv.release();
	}
}
 