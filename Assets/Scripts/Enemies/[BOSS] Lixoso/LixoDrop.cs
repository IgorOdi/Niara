using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LixoDrop : MonoBehaviour {

    private int dano;

    private void Start()
    {
        dano = 1;

        Destroy(gameObject, 4f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {

		if (other.gameObject.tag == "Player") { //com o Player,

			PlayerController.Damage (dano, false);

			Destroy (gameObject, 0f);
		} else if (other.gameObject.tag == "Chão") {

			Destroy(gameObject, 0f);
		}

    }
}
