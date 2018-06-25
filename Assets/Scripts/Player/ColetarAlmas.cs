using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class ColetarAlmas : MonoBehaviour {

   
    public SpriteRenderer almaN2;
    public static bool ganhaVida = false;
    public Sprite[] sprites;

	public string vidaUpSom = "event:/GanhaVida";

	private string almaUpSom = "event:/Alma Up";
   
    //public Text almasText;
    //public GameObject almaN;
   

    // Use this for initialization
    void Awake () {

       
        //sprites = new Sprite[10];
        //almaN2.sprite = sprites[PlayerController.almas];
        PlayerController.almas = 0;
        //almasText.text = "" + PlayerController.almas;
    }

    void Update()
    {
        //almaN3.gameObject.
       // almaN3.sprite = sprites[PlayerController.almas];
        almaN2.sprite = sprites[PlayerController.almas];

    } 

    void OnTriggerEnter2D (Collider2D col)
    {
        if (col.tag == "AlmaC") //Se player colidir com as alminhas flutuantes
        {
           
			StartCoroutine (AlmaLerp.instance.Lerp ());

            if (PlayerController.almas == 4)
            {
                if (PlayerController.vidas != 5)
                {
                    
                    PlayerController.vidas += 1;
                    ganhaVida = true;
                    FMODUnity.RuntimeManager.PlayOneShot(vidaUpSom, transform.position);
                    //PlayerController.almas += 1;
                    PlayerController.almas = 0;
                    col.gameObject.SetActive(false);
                }

                else
                {

                    PlayerController.almas = 0;
                    col.gameObject.SetActive(false);

                }



            }

            else if (PlayerController.almas != 4)
            { //E se as almas do player forem menores ou iguais a 4 

            	FMODUnity.RuntimeManager.PlayOneShot(almaUpSom);
                PlayerController.almas += 1; //Almas aumenta 1 alma 
                col.gameObject.SetActive(false); //                


            }

            //almasText.text = "" + PlayerController.almas; //Coloca o texto lá

        }

    }

}