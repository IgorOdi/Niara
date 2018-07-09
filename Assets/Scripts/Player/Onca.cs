using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Onca : MonoBehaviour {

	//Script de Referência:

	private PlayerController player;
	[SerializeField]
	private GameObject oncaSprite;
	[SerializeField]
	private GameObject oncaChargeSprite;

	private int danoFraco = 1; // O quanto de vida o Ataque Fraco tira dos inimigos
	private int danoMedio = 2;	// O quanto de vida o Ataque Medio tira dos inimigos
	private int danoForte = 4;	// O quanto de vida o Ataque Forte tira dos inimigos
	[SerializeField]
	private GameObject ataqueQueda;
	private int danoQueda = 1;

	//Dados da Onça:

	public float oncaCharge;  //O quanto o jogador carregou o ataque da Onça ( 0 -> Fraco | 30 -> Médio | 100 -> Forte)
	private float duracaoDash; //Tempo que o jogador permanece no ataque da Onça
	private bool dashOnca; //Checa se o jogador está no meio de um ataque da Onça
	private float tempodeAtaque; //Tempo de ataque
	private float oncaFallPower = 200f; //Força adicionada na queda da Onça

	public bool ataquePlayed;
	public bool oncaPlayed;
	public bool downPlayed;
	private bool canOncaDown = true;

    public static bool atacou;

	private bool oncaBool;

	[FMODUnity.EventRef]
	public string somOnca = "event:/Niara/ataqueOnca1";

	void Awake() {

		player = (PlayerController)GetComponent (typeof(PlayerController));
		oncaChargeSprite = this.gameObject.transform.GetChild (8).gameObject;
	}

	void Update() {

		tempodeAtaque += Time.fixedDeltaTime; //Soma 1 a tempodeAtaque todo segundo

		duracaoDash += Time.fixedDeltaTime;  //Soma 1 a duracaoDash todo segundo

		if (duracaoDash > 0.3f) dashOnca = false; //Se o duracaoDash passar de certo tempo, o dash será interrompido

        if (atacou) StartCoroutine(attackCool());

		if (PlayerController.grounded && !canOncaDown) canOncaDown = true;

    }

	void FixedUpdate() {

		if (dashOnca) {	//Enquanto o jogador estiver na duração do dash...

			player.rb.AddForce (transform.right * 1000 * transform.localScale.x);  //... adicione uma força para a frente
		}
	}

	public void OncaAtaqueChao () { //Lida com o poder da Onça

		if (Input.GetButton ("Attack")) { // Se o jogador apertar A...
			oncaCharge += 1f; //... aumentará a carga do ataque da Onça

			if (GameManager.oncaAtiva) {
				
				if (oncaCharge > 10 && oncaCharge < 65) {

					oncaChargeSprite.SetActive (true);
				} else if (oncaCharge >= 65) {

					oncaChargeSprite.SetActive (false);
				}
			}
		} else {

			oncaChargeSprite.SetActive (false);
		}

		if (Input.GetButtonDown ("Attack")) {
			//... Dar Ataque Fraco 

				atacou = true;
				PlayerController.danoCausado = danoFraco; // ... diz que o dano causado será o de um Ataque Fraco
				tempodeAtaque = 0; // ... reseta o tempodeAtaque
				oncaCharge = 0; // ... reseta a carga do Ataque da Onça

				player.an.SetTrigger ("Attack");

				if (!ataquePlayed) {

					FMODUnity.RuntimeManager.PlayOneShot(player.somBasicAttack, player.rb.position);
					ataquePlayed = true;
					StartCoroutine(somCooldown());

				}
		}

		if (Input.GetButtonUp("Attack") && !player.an.GetCurrentAnimatorStateInfo(0).IsName("Attack")) { // Se o jogador soltar o A...

			duracaoDash = 0; // ... a duração do dash será resetada

			if (oncaCharge > 50 && GameManager.oncaAtiva) { // Se a carga da Onça for maior que 100...

				if (!oncaBool) {

					FMODUnity.RuntimeManager.PlayOneShot (somOnca);
					oncaBool = true;
				}

				//... Dar Ataque Forte
				PlayerController.vulneravel = false;
				StartCoroutine (tempoVul ());

				oncaSprite.SetActive(true);
				dashOnca = true; // ... Torna verdadeiro a variável que checa se o jogador está no meio do ataque
				atacou = true;
				PlayerController.danoCausado = danoForte; // ... diz que o dano a ser causado é o dano de um Ataque Forte
				tempodeAtaque = 0; // ... reseta o tempodeAtaque
				oncaCharge = 0; // ... reseta a carga do ataque da Onça

				player.an.SetTrigger ("Attack");

			} 
		}
	}

	public void OncaAtaqueAr () {
		if ((Input.GetButtonDown ("Shield")  || Input.GetAxis("Shield") == 1) && !PlayerController.grounded && GameManager.oncaAtiva && canOncaDown) {
			PlayerController.vulneravel = false;
			StartCoroutine (tempoVul ());

			if (!oncaBool) {

				FMODUnity.RuntimeManager.PlayOneShot (somOnca);
				oncaBool = true;
			}
				
			player.rb.AddForce(transform.up * oncaFallPower * -1);
			ataqueQueda.SetActive(true);
			player.an.SetBool ("Onca Down", true);
			PlayerController.danoCausado = danoQueda;
            atacou = true;
		}

		else if (PlayerController.grounded) {
			ataqueQueda.SetActive(false);
			player.an.SetBool ("Onca Down", false);
		}
	}

    IEnumerator attackCool() {
        yield return new WaitForSeconds(0.2f);
		oncaSprite.SetActive(false);
        atacou = false;
		oncaBool = false;
    }

	IEnumerator tempoVul() {

		yield return new WaitForSeconds (2f);
		PlayerController.vulneravel = true;
	}

	void OnCollisionEnter2D(Collision2D other) {

		if (ataqueQueda.activeSelf && (other.gameObject.tag == "Inimigo" || other.gameObject.tag == "Boss")) {

			canOncaDown = false;
			GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0, 1000));
			ataqueQueda.SetActive(false);
			player.an.SetBool ("Onca Down", false);
		}

	}

	IEnumerator somCooldown() {
        yield return new WaitForSeconds(0.5f);
		ataquePlayed = false;
    }

}
