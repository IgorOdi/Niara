using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour { //Código que se aplicará a todos, ou pelo menos boa parte dos inimigos.

	//Código padrão para todos inimigos.

	// CONTOLE DE VIDAS //
	[HideInInspector]
	public float vidas; //Número de vidas do Inimigo.
	[HideInInspector]
	public float vidasMax;
	[HideInInspector]
	public bool vivo; //Se o Inimigo está vivo ou não.

	// MOVIMENTAÇÃO //

	protected bool ativo; //Define se o inimigo pode se ativar na cena.

	protected float distancia; // Calcular distância entre Inimigo e o Player.
	protected float distanciaY;
	[HideInInspector]
	public float lado; // Define o lado que o Player está em relação ao Inimigo.

	// DANO //

	[HideInInspector]
	public int danoBase; //Dano base do inimigo.
	[HideInInspector]
	public bool recebeuDano; //Define se recebeu dano ou não.
	[HideInInspector]
	public bool vulneravel;
	[HideInInspector]
	public bool quebraLanca = false;
	protected float damageTime; //Tempo de feedback de dano.

	// COMPONENTES //
	protected GameObject player; // Observa o Player.
	protected Rigidbody2D rb; // Rigidbody 2D do Inimigo.
	[HideInInspector]
	public Animator anim; //Aniamator do Inimigo.
	[SerializeField]
	protected GameObject destroy;

    // TIPO DE INIMIGO //

    protected bool boss;

    [SerializeField]
    protected GameObject alma;

	protected LayerMask wallLayer;

	void Awake() {

		player = GameObject.FindWithTag ("Player"); //Referencia o player.
		rb = GetComponent<Rigidbody2D> (); //Corpo Rigido Bi-dimensional.
		anim = GetComponentInChildren<Animator> (); //Animator

		wallLayer = LayerMask.GetMask ("Piso");
	}

	void Update () {

		vivo = vidas > 0 ? true : false; //O que define se o Inimigo está vivo ou não.

		distancia = transform.position.x - player.transform.position.x; // Cálculo da distância entre player e inimigo.
		distanciaY = transform.position.y - player.transform.position.y;

		if (!boss) {

			ativo = ((distancia <= 16 && distancia >= -12) && (distanciaY <= 8 && distanciaY >= -8)) ? true : false; //Inimigo se torna ativo, caso a distância entre ele o player for menor ou igual 18.
		} else {
			CamFollow.cameraShouldStop = false;
		}

		lado = distancia > 0 ? -1 : 1; //O valor da variável lado será 1 caso a distância for maior que 0, caso contrário será -1.

		if (recebeuDano) { //Se receber dano...

			damageState (); //Chama o método damageState.
		}
	}

	public void flip() {

		Vector2 scale = transform.localScale; //Vector2 que pega o localScale do inimigo.
		scale.x = -(lado); //X do localScale multiplicado por -1...
		transform.localScale = scale; //Torna o localScale atual no inimigo(Flip).
	}

	public void coloreVermelho() {

		for (int i = 0; i < GetComponentsInChildren<Anima2D.SpriteMeshInstance> (includeInactive: true).Length; i++) {

			GetComponentsInChildren<Anima2D.SpriteMeshInstance> (includeInactive: true) [i].color = Color.red;
		}
	}

	public void coloreNormal() {

		for (int i = 0; i < GetComponentsInChildren<Anima2D.SpriteMeshInstance> (includeInactive: true).Length; i++) {

			GetComponentsInChildren<Anima2D.SpriteMeshInstance> (includeInactive: true) [i].color = Color.white;
		}
	}

	void damageState() { //Método de feedback visual após receber dano.

		damageTime += Time.fixedDeltaTime; //Tempo++

		if (damageTime < 0.15f) { //Se o tempo for menor que 0,2 segundos...

			coloreVermelho ();

			rb.AddForce(new Vector2(6000 * -lado, 0));
		} else if (damageTime > 0.2f && damageTime < 0.4f) { //Se o tempo for maior que 0,2 e menor que 0,4 segundos:
			
			coloreNormal ();

		} else if (damageTime > 0.4f) { //Se o tempo for maior que 0,4 segundos...


			damageTime = 0; //Zera o tempo,
			recebeuDano = false; //Encerra o feedback visual de dano.
		}
	}

	public IEnumerator preMorte() {

		rb.velocity = new Vector2 (0, 0);
		yield return new WaitForSeconds (0.1f);
		Morte ();
	}

	public void Morte() {
        
		if (!boss) {
			Instantiate (alma, transform.position, Quaternion.identity);
		}

		Instantiate (destroy, transform.position, transform.rotation);
		DestroyImmediate (gameObject);
	}
}
