using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour {

	public string somEnemyHit = "event:/Enemy Hit";

	EnemyBehaviour enemy;
	[SerializeField]
	//private GameObject hit;

	void Awake() {

		enemy = (EnemyBehaviour)GetComponentInParent (typeof(EnemyBehaviour));
	}
		
	void OnTriggerEnter2D(Collider2D other) { //Se houver colisão do tipo Trigger

		if (other.gameObject.tag == "Weapon" && enemy.vivo && (Onca.atacou || Aguia.arremessou)) { //Com uma arma,
			
			if (enemy.vulneravel) {
			
				enemy.recebeuDano = true; //Avisa que o inimigo recebeu dano;
				enemy.vidas -= PlayerController.danoCausado; //Perde vida igual ao dano do Player.
				if (!GameManager.sfxStop) FMODUnity.RuntimeManager.PlayOneShot(somEnemyHit, transform.position);
			}
		}
	}
}
