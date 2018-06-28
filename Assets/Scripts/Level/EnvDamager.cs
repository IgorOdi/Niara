using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvDamager : MonoBehaviour {

    [SerializeField]
    private int dano;

    void OnTriggerEnter2D(Collider2D other) {

	    if (other.gameObject.tag == "Player") { //com o Player,

			PlayerController.Damage (dano, true);
	    }
    }
}
