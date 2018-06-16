using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RespawnPlayer : MonoBehaviour
{

    public Respawn res2;
    public bool Spawn;
    public bool Check1;
    public bool Check2;
    public string nomeCena;
	
	public GameManager gm;

    private GameObject spawns;

    private void Start()
    {
        spawns = GameObject.FindGameObjectWithTag("Enemy Respawn");
		gm = GameObject.Find("Game Manager(Clone)").GetComponent<GameManager>();
    }

    public void OnTriggerEnter2D(Collider2D other) { //Se tiver colisão

        if (other.tag == "Spawn") { //Se tiver a tag Inicio da Fase 

            Spawn = true;
            Check1 = false;
            Check2 = false;
			GameManager.shouldSave = true;
        }

        if (other.tag == "Check1") { //Se tiver a tag Inicio da Fase 

            Spawn = false;
            Check1 = true;
            Check2 = false;
			GameManager.shouldSave = true;
        }

        if (other.tag == "Check2") { //Se tiver a tag Inicio da Fase 

            Spawn = false;
            Check1 = false;
            Check2 = true;
			GameManager.shouldSave = true;
        }    
    }

	public void OnCollisionEnter2D(Collision2D other) {

		if (PlayerController.vidas < 2) {

			if (other.gameObject.tag == "Inimigo") {
                //aparece imagem de gameover que se você clicar restarta tudo 

                nomeCena = SceneManager.GetActiveScene().name;
                StartCoroutine (GameOver ());
                
			}

		}

		if (other.gameObject.tag == "Boss") {

            if (PlayerController.vidas < 2)
            {
                nomeCena = SceneManager.GetActiveScene().name;
                StartCoroutine(GameOver());
                //restartCurrentScene ();
            }

            
		}
	}

    public void OnTriggerExit2D(Collider2D other) {//Se tiver colisão

        if (other.tag == "Espinho")
        {

            if (PlayerController.vidas < 1)
            {
                nomeCena = SceneManager.GetActiveScene().name;
                StartCoroutine(GameOver());

            }
            
        }

		if (PlayerController.vidas < 1) {

			if (other.gameObject.tag == "Inimigo") {
                //aparece imagem de gameover que se você clicar restarta tudo 
                nomeCena = SceneManager.GetActiveScene().name;
                StartCoroutine (GameOver ());

			}

		}

		if (other.gameObject.tag == "Boss") {

			if (PlayerController.vidas < 1)
			{


                nomeCena = SceneManager.GetActiveScene().name;
                StartCoroutine(GameOver());
				//restartCurrentScene ();
			}


		}

    }
    
   
    public IEnumerator GameOver()
    {

        // suspend execution for 5 seconds
        yield return new WaitForSeconds(0.7f);
        SceneManager.LoadScene("Fim");
        //this.transform.position = res2.spawnPoints[0].transform.position; //O player vai pra posição do Spawn (Inicio do Jogo)
        // PlayerController.vidas = 5; //E reseta as vidas

    }



   /* IEnumerator Reviver(){

        // suspend execution for 5 seconds
        yield return new WaitForSeconds(0.2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //this.transform.position = res2.spawnPoints[0].transform.position; //O player vai pra posição do Spawn (Inicio do Jogo)
       // PlayerController.vidas = 5; //E reseta as vidas

    }
    */
    public void restartCurrentScene(){
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //Debug.Log("funcionou");
    }


}