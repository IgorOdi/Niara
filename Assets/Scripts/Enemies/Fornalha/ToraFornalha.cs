using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToraFornalha : MonoBehaviour {

    private Rigidbody2D rb;
	[SerializeField]
	private GameObject fogo;
    private float distancia;
    private int danoArremessavel;

    private void Start() {

        rb = GetComponent<Rigidbody2D>();
        danoArremessavel = 1;

        rb.AddForce (new Vector2(-300, 1000));

        Destroy(gameObject, 4f);
    }

	void OnTriggerEnter2D(Collider2D other) {

		if (other.gameObject.tag == "Colisor Fogo") {

			Vector2 spawnPos = transform.position;
			spawnPos.x = transform.position.x - 1;
			spawnPos.y = transform.position.y + 0.75f;

			Instantiate (fogo, spawnPos, Quaternion.identity);
			Destroy (gameObject, 0f);
		}

		if (other.gameObject.tag == "Player") { //com o Player,

				PlayerController.recebeKnockBack = true;

			if (PlayerController.vulneravel) { //Se o Player estiver vulneravel:

				PlayerController.recebeDano = true;                
				PlayerController.vidas -= danoArremessavel; //Subtrai o dano da habilidade/jogador;
				PlayerController.vulneravel = false; //Deixa o jogador invulnerável (Tempo limitado).
			}
		}
	}
}
