using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour {

    [SerializeField]
    private GameObject hitFX;
    [SerializeField]
    private Transform hitSpawn;

	void Awake () {

		Destroy (gameObject, 2.5f); //Destrói a lança depois de três segundos.
	}

	void OnTriggerEnter2D(Collider2D other) {
		
		if (other.gameObject.tag == "Chão" || other.gameObject.tag == "Pedra" || other.gameObject.tag == "Ativavel") {

			Instantiate (hitFX, hitSpawn.position, Quaternion.identity);
			Destroy (gameObject, 0.15f); //Quando a lança colide com algo que tenha essas tags ae, ela é destruída depois de 0.15 segundos.

		} else if (other.gameObject.tag == "Inimigo" || other.gameObject.tag == "Boss") {
			
			var enemy = other.gameObject.GetComponentInParent<EnemyBehaviour> ();

			if (enemy.vulneravel && !enemy.quebraLanca) {
				
				Instantiate (hitFX, hitSpawn.position, Quaternion.identity);
			}

			Destroy (gameObject, 0.15f);
		}
	}
}

	