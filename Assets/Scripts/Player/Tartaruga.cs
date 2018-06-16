using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tartaruga : MonoBehaviour {

	private PlayerController player;
	private Onca oncaScript;
    public bool shieldAtivo;
	private bool ficouVulneravel = false;

	[SerializeField]
	private bool canTurtle = true;

	[SerializeField]
	private float turtleTime;

	[SerializeField]
	private GameObject shield; // Puxa o GameObject Escudo

	[SerializeField]
	private Image barraCooldown;

	private bool turtleBool;

	[FMODUnity.EventRef]
	public string somJabuti = "event:/Niara/tartaruga";

	void Awake() {

		barraCooldown = GameObject.Find ("Jabuti Cooldown").GetComponent<Image> ();

		player = (PlayerController)GetComponent (typeof(PlayerController));
		oncaScript = (Onca)GetComponent (typeof(Onca));
        shieldAtivo = false;
		canTurtle = true;

		turtleTime = 8;
	}

	void Update() {

		TartarugaDefender ();

		barraCooldown.enabled = GameManager.jabutiAtivo;
	}

	public void TartarugaDefender ()
	{ //Lida com o poder da Tartaruga

		if ((Input.GetButton ("Shield")  || Input.GetAxis("Shield") == 1) && PlayerController.grounded && GameManager.jabutiAtivo) {  //Se o jogador pressionar a seta pra baixo...

			if (canTurtle) {

				if (!turtleBool) {

					FMODUnity.RuntimeManager.PlayOneShot (somJabuti);
					turtleBool = true;
				}

				shield.SetActive (true); //... ativar o escudo
				ficouVulneravel = false;	
				PlayerController.vulneravel = false; //... o jogador se torna Invulnerável
				PlayerController.canMove = false; // ... o jogador não pode se mover
				PlayerController.canShoot = false; // ... o jogador não pode atirar
				shieldAtivo = true;
				player.rb.mass = 100;
				oncaScript.oncaCharge = 0; // ... reseta a carga do ataque da Onça
			}

		}

		 else {  //Se não...

			if (!ficouVulneravel) {
				PlayerController.vulneravel = true;
				ficouVulneravel = true;
			}

			turtleBool = false;

            shieldAtivo = false;
			shield.SetActive (false); //... desativar o Escudo
			player.rb.mass = 1;
			PlayerController.canMove = true; //... o jogador pode se mover novamente
		}

		if (!canTurtle) {

			turtleTime += Time.deltaTime;

			if (turtleTime >= 8) {

				canTurtle = true;

			}

			if (!ficouVulneravel) {
				PlayerController.vulneravel = true;
				ficouVulneravel = true;
			}
			shieldAtivo = false;
			shield.SetActive (false); //... desativar o Escudo
			player.rb.mass = 1;
			PlayerController.canMove = true; //... o jogador pode se mover novamente
		}

		barraCooldown.fillAmount = turtleTime / 8;
	}

	void OnCollisionEnter2D(Collision2D other) {

		if ((shieldAtivo) && (other.gameObject.tag == "Inimigo" || other.gameObject.tag == "Boss" || other.gameObject.tag == "Espinho")) {

			canTurtle = false;
			shieldAtivo = false;
			turtleTime = 0;
			turtleBool = false;
		}
	}

	void OnTriggerEnter2D(Collider2D other) {

		if ((shieldAtivo) && (other.gameObject.tag == "Inimigo" || other.gameObject.tag == "Boss" || other.gameObject.tag == "Espinho") && (other.gameObject.name != "TREX")) {
			canTurtle = false;
			shieldAtivo = false;
			turtleTime = 0;
			turtleBool = false;
		}
	}

	void OnTriggerStay2D (Collider2D other){

		if ((shieldAtivo) && (other.gameObject.tag == "Inimigo" || other.gameObject.tag == "Boss" || other.gameObject.tag == "Espinho") && (other.gameObject.name != "TREX")) {
			canTurtle = false;
			shieldAtivo = false;
			turtleTime = 0;
			turtleBool = false;
		}
	}

	void OnCollisionStay2D (Collision2D other) {

		if ((shieldAtivo) && (other.gameObject.tag == "Inimigo" || other.gameObject.tag == "Boss" || other.gameObject.tag == "Espinho")) {
			canTurtle = false;
			shieldAtivo = false;
			turtleTime = 0;
			turtleBool = false;
		}
	}
}
