using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardaR : EnemyBehaviour {

    [SerializeField]
	private GameObject tiroSpawn;
    [SerializeField]
	private GameObject tiro;
	[SerializeField]
	private GameObject shootFX;

    private float idleTime;
    private float magicTime;

	private bool activo;

	private bool atirou;

	[FMODUnity.EventRef]
	public string somTiro = "event:/Inimigos/tirao3";

	public override void Start() {

        vivo = true;
        vidas = 2;
        vulneravel = true;
    }

    private void FixedUpdate() {

		if (ativo) {

			flip ();
			magicState ();
		}

		if (!vivo) {

			Morte ();
		}
    }

    void magicState() {

        magicTime += Time.fixedDeltaTime;

		if (magicTime > 2f && magicTime < 3f) {

		} else if (magicTime > 3f) {

			anim.SetTrigger ("New Trigger");

			if (!atirou) {

				FMODUnity.RuntimeManager.PlayOneShot (somTiro, transform.position);
				atirou = true;
			}

			Instantiate (shootFX, tiroSpawn.transform.position, tiroSpawn.transform.rotation);
			Instantiate(tiro, tiroSpawn.transform.position, tiroSpawn.transform.rotation);
            magicTime = 0;
			atirou = false;
        }
    }
}
