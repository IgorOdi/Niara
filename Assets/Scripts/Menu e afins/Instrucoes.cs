using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Instrucoes : MonoBehaviour {

    public string somSetaDown = "event:/Seta Down";
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

    }

    void OnGUI()
    {

        if (Event.current.isKey && Input.GetButtonDown("Throw"))
        {


            FMODUnity.RuntimeManager.PlayOneShot(somSetaDown, transform.position);
            GameManager.instance.OnChangeScene("Menu");
            

        }

    }
}
