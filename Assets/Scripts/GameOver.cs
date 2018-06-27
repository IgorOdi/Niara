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

		PlayerController.hitPlayed = true;
		FabricanteAI.traficanteIsDead = false;
		StartCoroutine (GameOverTime ());
    }

	IEnumerator GameOverTime() {

		yield return new WaitForSeconds (2f);
		GameManager.instance.OnChangeScene("Z" + GameManager.zona + "F" + GameManager.fase);
	}
}
