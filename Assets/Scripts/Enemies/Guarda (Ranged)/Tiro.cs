using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiro : ProjectileBase {

    private float timeControl;
	private int dano;
	private int lado;
	private float distancia;
	private Rigidbody2D rb;
	private Transform player;

    private void Start() {

		dano = 1;

		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ();

		distancia = transform.position.x - player.transform.position.x;

		rb = GetComponent<Rigidbody2D> ();
		rb.AddForce(new Vector2(-100 * distancia, 500));

		Destroy (gameObject, 2f);
    }

	void OnTriggerEnter2D(Collider2D other) {

		if (other.gameObject.tag == "Player") { //com o Player,

			if (PlayerController.vulneravel) { //Se o Player estiver vulneravel:

				PlayerController.recebeDano = true;
				PlayerController.vulneravel = false; //Deixa o jogador invulnerável (Tempo limitado).
				PlayerController.vidas -= dano; //Subtrai o dano da habilidade/jogador;
			}

			PlayerController.recebeKnockBack = true;
			Destructor ();
        }

		if (other.gameObject.tag == "Chão") {

			Destructor ();
		}
	}
}
