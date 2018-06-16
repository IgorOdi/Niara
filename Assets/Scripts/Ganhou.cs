using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ganhou : MonoBehaviour {

    public GameManager gm;
    
	void Start() {

		ChamaGM ();
	}

    void ChamaGM()
    {

        gm.ChamaFase();

    }


}
