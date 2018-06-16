using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Aguia : MonoBehaviour {

	private PlayerController player;
	[SerializeField]
	private GameObject araraSprite;
	[SerializeField]
	private Image barraCooldown;

	private bool arremessoSom;

	[FMODUnity.EventRef]
	public string somArara = "event:/Niara/araraSpear1";

	//Dados da Águia:

	public Transform verificaLanca; //Transform da lança
	public Rigidbody2D projetilAguia; //Rigidbody que guarda as lanças a serem instanciadas na função Aguia()
	public Transform posLança; //Puxa a posição do GameObject DireçãoProjetil (objeto que guarda a posição na qual será instanciada a lança)
	[SerializeField]
	private float cooldownLança; //Tempo que o jogador precisa esperar para atirar de novo 
	private bool isShootingWall; //Checa se o personagem estaria atirando direto em uma parede
    public static bool arremessou;

	void Awake() {

		cooldownLança = 3;
		barraCooldown = GameObject.Find ("Arara Cooldown").GetComponent<Image> ();
		player = (PlayerController)GetComponent (typeof(PlayerController));
	}

	void FixedUpdate() {

		barraCooldown.enabled = GameManager.araraAtivo;

		if (arremessou) {

			PlayerController.canShoot = false;
			cooldownLança += Time.deltaTime;
		}

		if (cooldownLança >= 3) {

			PlayerController.canShoot = true;
			arremessou = false;
			arremessoSom = false;
		}

		barraCooldown.fillAmount = cooldownLança / 3;
	}

	public void AguiaAtaque () {  //Lida com o poder da Águia

		Vector2 range = new Vector2 (verificaLanca.position.x + 0.5f, verificaLanca.position.y); //Cria um Vector2 à frente da posição verificaLanca

		isShootingWall = Physics2D.Linecast(transform.position, range, 1 << LayerMask.NameToLayer("Piso")); //Linecast para testar se o jogador está diretamnte virado para uma parede

		if ((Input.GetButtonDown("Throw") || Input.GetAxis("Throw") != 0) && PlayerController.canShoot && GameManager.araraAtivo) { //Caso o jogador aperte o botão da Águia e ele consiga atirar...

			if (!arremessoSom) {

				FMODUnity.RuntimeManager.PlayOneShot (somArara);
				arremessoSom = true;
			}

			araraSprite.SetActive (true);
			StartCoroutine (someAsa ());
			PlayerController.canShoot = false; //... ele não pode mais atirar até o cooldown ter passado
			PlayerController.danoCausado = 1; // ... diz que o dano a ser causado será o de um ataque fraco

			cooldownLança = 0;

			if (!isShootingWall) { //... se ele não estiver virado para a parede...

				if (player.facingRight) {
					// ... Instancia a Lança com direção e velocidade para a direita. 
					Rigidbody2D bulletInstance = Instantiate (projetilAguia, posLança.transform.position, Quaternion.Euler (new Vector3 (0, 0, 0))) as Rigidbody2D;
					bulletInstance.velocity = new Vector2 (15f, 0);

					arremessou = true;
				} else if (!player.facingRight) {
					
					// ... Instancia a Lança com direção e velociade para a esquerda.
					Rigidbody2D bulletInstance = Instantiate (projetilAguia, posLança.transform.position, Quaternion.Euler (new Vector3 (0, 0, 180f))) as Rigidbody2D;
					bulletInstance.velocity = new Vector2 (-15f, 0);

					arremessou = true;
				}
			} else {


				arremessou = true;
			}

		}
	}

	IEnumerator someAsa() {

		yield return new WaitForSeconds (1f);
		araraSprite.SetActive (false);
	}
}
