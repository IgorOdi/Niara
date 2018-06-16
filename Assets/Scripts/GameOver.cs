using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {

    public GameManager gm;

    void Start()
    {

		Coletavel.coletaveis = Coletavel.coletaveis - Coletavel.pegouColetavel;
		Coletavel.pegouColetavel = 0;

		SceneManager.LoadScene("Z" + GameManager.zona + "F" + GameManager.fase);
		PlayerController.hitPlayed = true;
		FabricanteAI.traficanteIsDead = false;
    }

}
