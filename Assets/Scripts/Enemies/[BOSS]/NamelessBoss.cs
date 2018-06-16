using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NamelessBoss : EnemyBehaviour {

	private float speed;
	[SerializeField]
	private int currentState;
	private Vector2 move;

	private float idleTime;
	private float movingTime;
	private float stateATime;
	private float stateBTime;
    private float stateCTime;
	private float stateDTime;

	private float jumpAttack;
	private float spawnJump;

	private bool canSpawn = true;

    [SerializeField]
    private GameObject grCollider;
    [SerializeField]
    private GameObject shots;
    [SerializeField]
    private Transform spawn;
	[SerializeField]
	private GameObject pontoFraco;
	[SerializeField]
	private GameObject spiderToSpawn;
	[SerializeField]
	private Transform[] spawnPoints;

	[SerializeField]
	private GameObject tetoCollider;

	private bool jumpou, teiou, passou;

	[FMODUnity.EventRef]
	public string somJump = "event:/somJump";

	[FMODUnity.EventRef]
	public string somTeia = "event:/somTeia";

	[FMODUnity.EventRef]
	public string somPassos = "event:/Passos";
	public static FMOD.Studio.EventInstance passosAranhaEv;

	void Start () {

		vivo = true;
		vidasMax = 50;
		vidas = vidasMax;
		vulneravel = true;
		boss = true;
		danoBase = 1;
		currentState = 1;
	}

	void FixedUpdate () {

		switch (currentState) {

		case 0:
			idleState ();
			break;

		case 1:
			jumpAttackState ();
			break;

		case 2:
			stateA ();
			break;

		case 3:
			stateB ();
			break;

        case 4:
            stateC();
            break;
		
		case 5:
			stateD ();
			break;

		case 6:
			jumpSpawnState ();
			break;
		}

		if (!vivo) {

			Morte ();
		}

		passosAranhaEv = FMODUnity.RuntimeManager.CreateInstance (somPassos);
	}

	void idleState() {

		idleTime += Time.fixedDeltaTime;
		anim.SetTrigger("New Trigger 3");

		if (idleTime >= 4f) {

			idleTime = 0;
			currentState = 1;
			flip ();
		}
	}

	void movingState() {

		movingTime += Time.fixedDeltaTime;

		if (movingTime >= 2) {

			movingTime = 0;
			currentState = 0;
		}
	}

	void stateA() {

		stateATime += Time.fixedDeltaTime;
        rb.velocity = new Vector2(0, 0);
        
		if (stateATime > 2) {
			stateATime = 0;
			currentState = 3;
			anim.SetTrigger ("New Trigger");
		}
	}

	void stateB() {

		stateBTime += Time.fixedDeltaTime;
        rb.velocity = new Vector2(0, 0);
		flip ();

        if (stateBTime > 1f) {

			if (!teiou) {

				FMODUnity.RuntimeManager.PlayOneShot (somTeia);
				teiou = true;
			}

            Instantiate(shots, spawn.position, transform.rotation);
            stateBTime = 0;
			currentState = 4;
        }
	}

    void stateC()  {

        stateCTime += Time.fixedDeltaTime;

		teiou = false;

        if (stateCTime < 4) {
			
			anim.SetBool ("New Bool 1", true);
            grCollider.SetActive(true);
        } else {
			
			anim.SetTrigger ("New Trigger 3");
            grCollider.SetActive(false);
			stateCTime = 0;
			currentState = 5;
        }
    }

	void stateD() {

		stateDTime += Time.fixedDeltaTime;

		if (stateDTime < 2) {

			pontoFraco.SetActive (true);
		} else {
			
			pontoFraco.SetActive (false);

			anim.SetBool ("New Bool 1", false);

			stateDTime = 0;
			currentState = 6;
		}
	}

	void jumpAttackState() {

		jumpAttack += Time.fixedDeltaTime;

		if (jumpAttack > 0 && jumpAttack < 2f) {

			if (!passou) {

				passosAranhaEv.start ();
				passou = true;
			}
				
		} else if (jumpAttack > 2f && jumpAttack < 3.5f) {

			anim.SetBool ("New Bool", true);

			rb.AddForce (new Vector2 (30000 * lado, 50));
		} else if (jumpAttack > 3f) {

			passosAranhaEv.stop (FMOD.Studio.STOP_MODE.IMMEDIATE);
			passou = false;

			anim.SetBool ("New Bool", false);
			canSpawn = true;
			jumpAttack = 0;
			currentState = 2;
		}
	}

	void jumpSpawnState() {

		spawnJump += Time.fixedDeltaTime;

		if (spawnJump >= 1 && spawnJump <= 2) {

			if (anim.GetBool("New Bool") == false) anim.SetBool ("New Bool 0", true);
		} else if (spawnJump >= 2f && spawnJump < 10f) {

			if (!jumpou) {

				FMODUnity.RuntimeManager.PlayOneShot (somJump, transform.position);
				jumpou = true;
			}

			tetoCollider.SetActive (true);
			rb.AddForce (new Vector2 (0, 40000));
			anim.SetTrigger ("New Trigger 1");
			summonState ();
			canSpawn = false;
		} else if  (spawnJump > 10 && spawnJump < 12) {

			anim.SetBool ("New Bool 0", false);
			jumpou = false;
			tetoCollider.SetActive (false);
			anim.SetTrigger ("New Trigger 2");
		} else if (spawnJump > 12f) {
			
			rb.AddForce (new Vector2 (0, -40000));
			spawnJump = 0;
			currentState = 0;
		}
	}

	void summonState() {

		if (canSpawn) {
			
			for (int i = 0; i < spawnPoints.Length; i++) {

				Instantiate (spiderToSpawn, spawnPoints [i].position, spawnPoints [i].rotation);
			}
		}
	}

	void OnDestroy() {
		
		passosAranhaEv.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);

	}
}
