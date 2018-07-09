using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{

    public RespawnPlayer res;
    public static GameObject[] spawnPoints;
    public static bool perdeVida;
  

    //private e array e essas porras findobjectwithtag  ???? wtf Stella

    void Awake()
    {
        perdeVida = false;
        spawnPoints = new GameObject[3];

        spawnPoints[0] = GameObject.FindGameObjectWithTag("Spawn");
        spawnPoints[1] = GameObject.FindGameObjectWithTag("Check1");
        spawnPoints[2] = GameObject.FindGameObjectWithTag("Check2");

        // respawn = GameObject.FindGameObjectsWithTag("Spawn");

    }

    public void OnTriggerEnter2D(Collider2D other)
    {//Se tiver colisão com a plataforma embaixo de tudo
        
		if (other.gameObject.tag == "Player") {
			PlayerController.Damage (1, true);

			if (PlayerController.vidas > 0) {

				EnemyRespawn.instance.Respawn ();
				RespawnPlayer.instance.player.rb.velocity = Vector2.zero;
				PlayerController.moveBlock = true;
				RespawnPlayer.instance.player.transform.position = RespawnPlayer.instance.lastCheckpoint.position;
				StartCoroutine (RespawnPlayer.instance.ReturnMove ());
			}
		}

//        if (other.tag == "Player") { //E quem colidiu é o Player
//
//            if (PlayerController.vidas <= 1) {   // Se o Player não tiver vida nenhuma 		
//
//                StartCoroutine(res.GameOver());
//               
//                 
//            }
//
//            else if (PlayerController.vidas > 1) { //Ou então, se o PLayer tiver vidas maior que 0, aka se tiver vidas 
//
//                if (res.Spawn) { //E se a variável Spawn for true 
//
//                    PlayerController.vidas -= 1; //Jogador perde uma vida 
//                    perdeVida = true;
//                    other.transform.position = spawnPoints[0].transform.position; //O player vai pra posição do Spawn (Inicio do Jogo)
//                    perdeVida = false;
//                    PlayerController.vulneravel = true;
//                    
//
//                }
//
//                if (res.Check1){ //Ou se a variável Check1 for true
//
//                    PlayerController.vidas -= 1; //Jogador perde uma vida 
//                    perdeVida = true;
//					other.transform.position = new Vector2 (spawnPoints[1].transform.position.x, spawnPoints[1].transform.position.y + 2); //O player vai pra posição do Check1 (CheckPoint A)
//                    perdeVida = false;
//					PlayerController.vulneravel = true;
//                }
//
//                if (res.Check2){ //Ou se a variável Check1 for true
//
//                    PlayerController.vidas -= 1; //Jogador perde uma vida
//                    perdeVida = true;
//					other.transform.position = new Vector2(spawnPoints[2].transform.position.x, spawnPoints[2].transform.position.y + 2); //O player vai pra posição do Check2 (CheckPoint B)
//                    perdeVida = false;
//					PlayerController.vulneravel = true;
//                }
//            }
//
//			EnemyRespawn.respawnEnemy = true;
//        }
    }
    
}
