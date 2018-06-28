using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ganhou : MonoBehaviour {

    public GameManager gm;
    
	void Start() {

		StartCoroutine(ChamaGM ());
	}

	IEnumerator ChamaGM()
    {

		yield return new WaitForSeconds (1.5f);
        gm.ChamaFase();

    }


}
