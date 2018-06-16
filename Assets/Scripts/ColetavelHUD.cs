using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColetavelHUD : MonoBehaviour {

    [SerializeField]
    private GameObject[] coleta1;

    // Use this for initialization
    void Start()
    {

        //coleta1[0] = gameObject.GetComponentsInChildren<GameObject>()[0];
        //coleta1[1] = gameObject.GetComponentsInChildren<GameObject>()[1];

    }
    // Update is called once per frame
    void Update () {

        coleta1[0].SetActive(!PlayerController.podeExecutar);

        coleta1[1].SetActive(!PlayerController.podeExecutar);
        
        
    }
}
