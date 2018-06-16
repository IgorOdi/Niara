using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Tutorial : MonoBehaviour {

    public Sprite[] tuto;
    public GameObject[] tutoGO;
    private GameObject niara, inim;
    private float alpha1;
    private float alpha2;
    private float alpha3;



    // Use this for initialization
    void Start () {

        inim = GameObject.Find("InimigoSerra");
       // alm = GameObject.Find("AlmaC(Clone)");
       niara = GameObject.Find("Player");
      // tutoGO[1].SetActive(true);
       //tutoGO[2].SetActive(false);

    }

    private void Update()
    {

        GOActive();



    }
    
    
    void GOActive()
    {
        if (niara.transform.position.x > -9)
        {

            tutoGO[0].GetComponent<SpriteRenderer>().color = new Color(255, 255, 225, alpha1);
            //tutoMico.SetActive(true);
            alpha1 += 0.1f;
            //  tutoGO[1].SetActive(false);
            //tutoGO[2].SetActive(true);

        }

        if (niara.transform.position.x > 12.75f)
        { 

            tutoGO[1].GetComponent<SpriteRenderer>().color = new Color(255, 255, 225, alpha2);
            //tutoMico.SetActive(true);
            alpha2 += 0.1f;
            //  tutoGO[1].SetActive(false);
            //tutoGO[2].SetActive(true);

        }

        if (niara.transform.position.x > 34f)
            {

                tutoGO[2].GetComponent<SpriteRenderer>().color = new Color(255, 255, 225, alpha3);
                //tutoMico.SetActive(true);
                alpha3 += 0.1f;
            //  tutoGO[1].SetActive(false);
                //tutoGO[2].SetActive(true);

            }
        
       
        

    }

    

    

}
//se a niara estiver no tutorial e estiver em tal lugar, a imagem aparece
