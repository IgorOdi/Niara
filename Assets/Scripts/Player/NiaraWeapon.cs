using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NiaraWeapon : MonoBehaviour {

    private Collider2D arma;
    [SerializeField]
    private GameObject hitFX;
	private Transform pontohit;


    private void Start() {
        arma = GetComponent<Collider2D>();
		pontohit = GetComponent<Transform> ();
    }

    private void Update() {
		
        if (Onca.atacou) {
			
            arma.enabled = true;
        } else {
			
            arma.enabled = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
		
		if (other.gameObject.tag == "Inimigo" || other.gameObject.tag == "Boss") {

			var enemy = other.gameObject.GetComponent<EnemyBehaviour> ();

			if (enemy.vulneravel) {

				Instantiate (hitFX, pontohit.transform.position, Quaternion.identity);
			}
        }
    }
}
