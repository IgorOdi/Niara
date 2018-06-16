using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fornalha : EnemyBehaviour {

    private float spawnTime;
    [SerializeField]
    private Transform spawn;
    [SerializeField]
    private GameObject tora;
    [SerializeField]
    private GameObject shootFX;

	private bool shootou;

	[FMODUnity.EventRef]
	public string somShoot = "event:/Inimigos/tiro3";

    private void Start() {

        vivo = true;
        vidas = 3;

		vulneravel = true;
		danoBase = 1;
    }

    private void FixedUpdate() {

		if (ativo) {
			
			spawnTora ();
		}

        if(!vivo) {

            StartCoroutine(preMorte());
        }
    }

    void spawnTora() {

        spawnTime += Time.fixedDeltaTime;

		if (spawnTime >= 2.5f && spawnTime <= 4) {
			
		} else if (spawnTime >= 4) {

			if (!shootou) {

				FMODUnity.RuntimeManager.PlayOneShot (somShoot, transform.position);
				shootou = true;
			}

			shootou = false;

            Instantiate(tora, spawn.transform.position, spawn.transform.rotation);
			Instantiate(shootFX, spawn.transform.position, spawn.transform.rotation);
            spawnTime = 0;
        }
    }
}
