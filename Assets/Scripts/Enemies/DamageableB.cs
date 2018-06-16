using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableB : MonoBehaviour {

	[SerializeField]
	EnemyBehaviour enemy;
	[SerializeField]
	private GameObject hit;

	public string somEnemyHit = "event:/Enemy Hit";

	void Awake() {

		enemy = (EnemyBehaviour)GetComponentInParent (typeof(EnemyBehaviour));
	}

	void OnTriggerEnter2D(Collider2D other) { //Se houver colisão do tipo Trigger

		if (enemy.vulneravel) {

			if (other.gameObject.name == "Pedra(Clone)" && enemy.vivo && (Onca.atacou || Aguia.arremessou)) { //Com uma arma,
				
				Destroy (other.gameObject, 0f);
				enemy.recebeuDano = true; //Avisa que o inimigo recebeu dano;
				enemy.vidas -= 1; //Perde vida igual ao dano do Player.
				Instantiate(hit, transform.position, transform.rotation);
				if (!GameManager.sfxStop) FMODUnity.RuntimeManager.PlayOneShot(somEnemyHit, transform.position);
				//enemy.anim.SetTrigger("damage");
			}
		}
	}
}
