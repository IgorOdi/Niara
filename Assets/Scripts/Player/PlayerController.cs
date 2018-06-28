using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using FMODUnity;

public class PlayerController: MonoBehaviour {

	public Rigidbody2D rb;  //Rigidbody do jogador

	public Animator an;	//Animator do jogador

	public CamFollow cam;

	public GameManager gm;

	public GameObject fadeObj;

	[FMODUnity.EventRef]
	public string somPassos = "event:/Passos";
	public static FMOD.Studio.EventInstance passosEv;
	protected FMOD.Studio.ParameterInstance terrenoParam;
	protected FMOD.Studio.ParameterInstance andandoParam;

	[FMODUnity.EventRef]
	public string somPulo = "event:/Pulo";

	[FMODUnity.EventRef]
	public string somDano = "event:/Niara Hit";
	public static bool hitPlayed = false;

	public string somBasicAttack = "event:/Niara Basic Attack";

	private bool jumpBool;

	[FMODUnity.EventRef]
	public string somMico = "event:/Niara/doubleJump1";

	public float terreno;

	//Scripts de Referência:

	private Onca oncaScript;
	private Aguia aguiaScript;
	private Tartaruga tartarugaScript;
    private VidaPlayer vidaScript;

    //Dados do personagem:

    [SerializeField]
    private Transform groundCheck;

    public Vector2 groundCheckSize = new Vector2(1,1);
    public LayerMask groundLayer;

	public static int almas;
    public static bool podeExecutar;

    // public GameObject vida;
    public float force;
	public float forceMultiplier;
    public GameObject Niara;
	public static int vidas; //Máximo de vidas do personagem
    private int vidaAtual;
	public static bool vulneravel = true;
	public static bool recebeDano = false;
    public static bool recebeKnockBack = false;
	public static bool recebeKnockBackVert = false;
    private float tempoDano;
	private float tempoKB; //Tempo que passa tomando Knockback.
	private float tempoKBV; //Tempo que passa tomando Knockback Vertical.
	private float axis;	//Eixo do personagem.
	public float speed; // Velocidade que será multiplicada à variavel horizontal.
	private float horizontal; //Variável para guardar o input Horizontal do jogador.
    public float jumpPower; //Força adicionada ao personagem quando ele pula
	public float doubleJumpPower; //Força adicionada ao personagem quando ele dá o pulo duplo
	public bool facingRight = true; //Checa a direção do jogador.
	public static bool grounded; //Checa se o personagem está no chão
	public static bool canMove; //Checa se o personagem pode ou não se mover
	public static bool moveBlock;
	[SerializeField]
	private bool canDoubleJump; //Checa se o peronagem usou o pulo duplo
	public static float danoCausado = 1; //O quanto de vida o jogador tira dos inimigos
	public static bool canShoot; //Checa se o jogador pode atirar novamente

    

    [SerializeField]
	private GameObject micoSprite;

	void Awake () {  //Acontece quando o prefab é "acordado"

        podeExecutar = true;
        vidas = 5;
        almas = 0;  

		aguiaScript = (Aguia)GetComponent (typeof(Aguia));
		oncaScript = (Onca)GetComponent (typeof(Onca));
		tartarugaScript = (Tartaruga)GetComponent (typeof(Tartaruga));

		rb = GetComponent<Rigidbody2D>(); //Encontra o componente Rigidbody do jogador.
		an = GetComponentInChildren<Animator>();
		cam = Camera.main.GetComponent<CamFollow>();
		canMove = true;
		moveBlock = false;
		canShoot = true;

		facingRight = true;

		force = 1500;

		groundCheckSize = new Vector2 (0.4f, 0.3f);

		passosEv = FMODUnity.RuntimeManager.CreateInstance(somPassos);
		passosEv.getParameter("Andando", out andandoParam);
		passosEv.getParameter ("Terreno", out terrenoParam);
		passosEv.start();

		Instantiate(gm);
		StartCoroutine(gm.habilidadesIniciais());

		Time.timeScale = 1;

		hitPlayed = true;
		vulneravel = true;
	}

	void Start() {
		
		fadeObj = GameObject.Find ("Fade");
		fadeObj.GetComponent<Image> ().enabled = true;
		fadeObj.GetComponent<Fade> ().FadeIn ();
	}

	void OnDestroy () {
		passosEv.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
		passosEv.release();
	}  

	void Update () //Acontece todo frame (varia de acordo com o framerate que o computador aguenta).
	{

        horizontal = Input.GetAxisRaw ("Horizontal");   //Assimila o valor do Input "Horizontal" à variável.


		if (Time.timeScale != 0 && !CamFollow.cameraShouldStop) Player();    // Chama a função que controla o jogador

		else if (CamFollow.cameraShouldStop && grounded) { 
			rb.velocity = new Vector2 (speed,0);
			an.SetBool ("Moving", true);
			an.SetBool ("Fall", false);
		}

		else {
			an.SetLayerWeight (1, 0);
		}

	}

	void HandleLayers ()
	{
		if (!grounded) {
			an.SetLayerWeight (1, 1);
		}

		else {
			an.SetLayerWeight (1, 0);
		}
	}

	void OnTriggerEnter2D (Collider2D other) {

		if (other.gameObject.tag == "Limite") {
			CamFollow.cameraShouldStop = true;
			Debug.Log(other);
			gm.zonaFase();
			gm.ChamaBoss ();
			fadeObj.GetComponent<Fade> ().FadeOut ();
		}

		if (other.transform.position.x >= transform.position.x) {

			forceMultiplier = -1;
		} else {

			forceMultiplier = 1;
		}

	}

	void OnCollisionEnter2D(Collision2D other) {
		
		if (other.transform.position.x >= transform.position.x) {

			forceMultiplier = -1;
		} else {

			forceMultiplier = 1;
		}
	}


	void OnTriggerStay2D(Collider2D other) {
//		if (other.tag == "CameraPan") {      // Checa se o jogador está na área de Pan da Câmera
//			cam.cameraShouldPan = true;
//			cam.cameraExitPan = false;
//		}


		if (other.gameObject.tag == "Vapor") an.SetBool("Fall",true);  // Checa se o jogador está na área do Vapor
	}


//	void OnTriggerExit2D(Collider2D other) {
//		if (other.tag == "CameraPan") {
//			cam.cameraShouldPan = false;	
//			cam.cameraExitPan = true;
//		}
//	}
		
    
	void Player ()
	{  //Função para controlar ações do jogador como andar, pular, etc...
		HandleLayers();

		if (canMove) Pular (); //Faz o jogador pular
		if (!moveBlock) MoverJogador ();//Move o jogador

		playerAudio();

		if(canShoot) aguiaScript.AguiaAtaque (); //Controla a Águia
		oncaScript.OncaAtaqueAr(); //Controla a Onça no ar
		tartarugaScript.TartarugaDefender (); // Controla a Tartaruga

		if(canMove) oncaScript.OncaAtaqueChao (); //Controla a Onça no chão

                
		if (!vulneravel) { //Caso o jogador não esteja vulneravel...

			if (recebeDano) {

				dano (); //Chame o método dano() que faz a Niara piscar enquanto está invulnerável.
				StartCoroutine (Invulneravel ()); //...comece a Coroutina Invulneravel() e
			}
            if (recebeKnockBack) knockBack();
			if (recebeKnockBackVert) knockBackVert();
        }

        else {
            
            if (recebeKnockBack) knockBack();
			if (recebeKnockBackVert) knockBackVert();

			for (int i = 0; i < Niara.GetComponentsInChildren<SkinnedMeshRenderer> ().Length; i++) {
				//Coloquei isso aqui porque tava bugando, se quiser dá uma fuçada no tempo e tente resolver.
				Niara.GetComponentsInChildren<SkinnedMeshRenderer> () [i].enabled = true;
			}

			for (int i = 0; i < Niara.GetComponentsInChildren<MeshRenderer> ().Length; i++) {
				//Coloquei isso aqui porque tava bugando, se quiser dá uma fuçada no tempo e tente resolver.
				Niara.GetComponentsInChildren<MeshRenderer> () [i].enabled = true;
			}
        }
    }


	void playerAudio ()
	{ 

		if (rb.velocity.x != 0 && grounded && !GameManager.sfxStop) { 		//AUDIO DO JOGADOR ANDANDO
			andandoParam.setValue (1f);
		} else {
			andandoParam.setValue (0f);
		}

		if (recebeDano && !hitPlayed && !GameManager.sfxStop) {
			FMODUnity.RuntimeManager.PlayOneShot (somDano, rb.position);
			hitPlayed = true;
		}

		if (!recebeDano) hitPlayed = false;

		terrenoParam.setValue (ChecaTerreno.terreno);


	}


    
    void dano ()
    {

        tempoDano += Time.deltaTime; //Tempo 
        
        if (tempoDano <= 0.1f) { //Se tempo menor que 0,2 segundos.
            //Debug.Log("Recebe dano: " + recebeDano);
            for (int i = 0; i < Niara.GetComponentsInChildren<SkinnedMeshRenderer> ().Length; i++) {
                //rb.AddForce(-transform.position * Time.deltaTime * force);
               
                Niara.GetComponentsInChildren<SkinnedMeshRenderer> () [i].enabled = false;
			}

			for (int i = 0; i < Niara.GetComponentsInChildren<MeshRenderer> ().Length; i++) {
				//rb.AddForce(-transform.position * Time.deltaTime * force);

				Niara.GetComponentsInChildren<MeshRenderer> () [i].enabled = false;
			}

        }

        else if (tempoDano > 0.1f && tempoDano < 0.2f) { //Se tempo maior que 0,2 segundos e menor que 0,4 segundos.

			for (int i = 0; i < Niara.GetComponentsInChildren<SkinnedMeshRenderer> ().Length; i++) {

				Niara.GetComponentsInChildren<SkinnedMeshRenderer> () [i].enabled = true;
			}

			for (int i = 0; i < Niara.GetComponentsInChildren<MeshRenderer> ().Length; i++) {
				//rb.AddForce(-transform.position * Time.deltaTime * force);

				Niara.GetComponentsInChildren<MeshRenderer> () [i].enabled = true;
			}

		}

        else if (tempoDano >= 0.2f) { //Se o tempo for maior que 0.4 segundos.

			tempoDano = 0;

		}	
    }

    void knockBack() {

        tempoKB += Time.fixedDeltaTime;

        if (tempoKB < 0.1f) {

			rb.AddForce (new Vector2(forceMultiplier * force, 0));
		} else {

            recebeKnockBack = false;
            tempoKB = 0;
        }
    }

	void knockBackVert() {

		tempoKBV += Time.fixedDeltaTime;

		if (tempoKBV < 0.1f) {

			rb.AddForce(Vector2.up * force/7);
		} else {

			recebeKnockBackVert = false;
			tempoKBV = 0;
		}

	}

    IEnumerator Invulneravel() 	{

        yield return new WaitForSeconds(1.2f); //Esperar 1,2 segundos
		//tempoKB = 0;
        vulneravel = true; //Torna o player vulnerável novamente.
		recebeDano = false;

	}
    
	void flip() {  //Função para virar o jogador
		if (canMove) { //Caso o jogador possa se mover...
			facingRight = !facingRight;  // ... Inverte o valor da boolean (se estiver true vira false e vice versa) assim que a função de virá-lo é chamada. 

			Vector2 playerScale = transform.localScale; // ... Pega o scale do jogador e guarda em um Vector2.
			playerScale.x *= -1;  // ... Inverte o valor X do Vector2 de escala.
			transform.localScale = playerScale;  // ... Usa o valor invertido do Vector2 para inverter a escala e virar o jogador.
		}
	}

	void MoverJogador() { // Função para mover o jogador.

		if (horizontal > 0 && !facingRight)
            {  //Se o input estiver para a direita e o jogador ainda não estiver naquela direção...
                flip();                        //Vire o jogador para a direita.
            }

            else if (horizontal < 0 && facingRight)
            {  //Se o input estiver para a esquerda e o jogador ainda não estiver naquela direção...
                flip();                            //Vire o jogador para a esquerda.
            }
			
		Vector2 move = new Vector2 (horizontal * speed, rb.velocity.y);  //Cria um novo Vector2 chamado "move". Seu X é a multiplicação do input Horizontal pelo float da velocidade.
		rb.velocity = move;  //Iguala a velocidade do jogador ao Vector2 criado acima.

		if (!canMove) {  //Caso o jogador não possa se mover...
			speed = 0;  //... a velocidade dele será 0
		} else { // Se não...

			speed = 6; //... a velocidade dele será normal
		}

		if (rb.velocity.x != 0) {
			an.SetBool ("Moving", true);
		} else {
			an.SetBool ("Moving", false);
		}

		if (rb.velocity.y < 0 && !grounded) {
			an.SetBool ("Fall", true);
		}
    }

    void Pular() { //Função para o jogador pular

		grounded = Physics2D.OverlapBox(groundCheck.position, groundCheckSize, 0, groundLayer);

    	if (grounded) {

			jumpBool = false;
			an.ResetTrigger ("Jump");
			an.SetBool ("Fall", false);
			canDoubleJump = true;
			rb.gravityScale = 1.5f;

    	}

		if (Input.GetButtonDown ("Jump") && grounded) { // Caso o jogador aperte para pular e ele esteja no chão...

			an.SetTrigger ("Jump");
			rb.AddForce (transform.up * jumpPower);  // ... adiciona uma força vertical ao personagem de acordo com o jumpPower
			if (!GameManager.sfxStop) FMODUnity.RuntimeManager.PlayOneShot(somPulo, rb.position);
             
		} else if (Input.GetButtonDown ("Jump") && canDoubleJump && GameManager.micoAtivo) { // Caso o jogador esteja no ar, aperte para pular novamente e ainda não tenha pulado duas vezes...

			if (!jumpBool) {

				FMODUnity.RuntimeManager.PlayOneShot (somMico);
				jumpBool = true;
			}

			micoSprite.SetActive (true);
			StartCoroutine (someCauda ());
			rb.velocity = new Vector2 (rb.velocity.x, 0); // ... zera a velocidade vertical para que o segundo pulo seja consistente
			rb.AddForce (transform.up * doubleJumpPower); // ... adiciona uma força vertical ao personagem de acordo com o doubleJumpPower
			canDoubleJump = false;
			if (!GameManager.sfxStop) FMODUnity.RuntimeManager.PlayOneShot(somPulo, rb.position);

		}
	}

	IEnumerator someCauda() {

		yield return new WaitForSeconds (1f);
		micoSprite.SetActive (false);
	}

	public static void Damage(int _dano, bool vert) {

		if (PlayerController.vulneravel) { //Se o Player estiver vulneravel:

			PlayerController.recebeDano = true;
			PlayerController.vulneravel = false; //Deixa o jogador invulnerável (Tempo limitado).
			PlayerController.vidas -= _dano; //Subtrai o dano da habilidade/jogador;

			if (vert) PlayerController.recebeKnockBackVert = true;
			else PlayerController.recebeKnockBack = true;

			if (PlayerController.vidas <= 0) {

				RespawnPlayer.instance.VerifyDeath ();
			}
		}
	}
}