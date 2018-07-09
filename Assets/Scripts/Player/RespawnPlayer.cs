using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RespawnPlayer : MonoBehaviour
{

	public static RespawnPlayer instance;
    public bool Spawn;
    public bool Check1;
    public bool Check2;
    public string nomeCena;
	
	public GameManager gm;
	[HideInInspector]
	public Transform lastCheckpoint;
	[HideInInspector]
	public PlayerController player;

    private void Start()
    {
		instance = this;
		gm = GameManager.instance;
		player = GameObject.Find ("Player").GetComponent<PlayerController> ();
    }

    public void OnTriggerEnter2D(Collider2D other) { //Se tiver colisão

        if (other.tag == "Spawn") { //Se tiver a tag Inicio da Fase 

            Spawn = true;
            Check1 = false;
            Check2 = false;
			lastCheckpoint = other.transform;
			GameManager.shouldSave = true;
        }

        if (other.tag == "Check1") { //Se tiver a tag Inicio da Fase 

            Spawn = false;
            Check1 = true;
            Check2 = false;
			lastCheckpoint = other.transform;
			GameManager.shouldSave = true;
        }

        if (other.tag == "Check2") { //Se tiver a tag Inicio da Fase 

            Spawn = false;
            Check1 = false;
            Check2 = true;
			lastCheckpoint = other.transform;
			GameManager.shouldSave = true;
        }    
    }
   
	public void VerifyDeath() {

		string cena = GameManager.instance.cena.name;

		if (cena.Contains ("Boss")) {

			FabricanteAI.traficanteIsDead = false;
			GameManager.instance.OnChangeScene (cena);
		} else {

			EnemyRespawn.instance.Respawn ();
			player.rb.velocity = Vector2.zero;
			player.transform.position = lastCheckpoint.position;
			PlayerController.vidas = 5;
			StartCoroutine (ReturnMove ());
		}
	}

    public IEnumerator GameOver()
    {

        // suspend execution for 5 seconds
        yield return new WaitForSeconds(0.5f);
        GameManager.instance.OnChangeScene("Fim");
    }

	public IEnumerator ReturnMove() {

		float startTime = Time.time;
		while (Time.time < startTime + 0.75f)
			yield return null;

		PlayerController.moveBlock = false;
	}
}