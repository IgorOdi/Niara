using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause1 : MonoBehaviour {
    //ISSO TA NO PLAYER
    private Tartaruga turtle;
    public bool pausado = false;
    public GameManager gm;
    public static bool boss = false;
    public Scene cena;

    private void Awake()
    {

        turtle = GetComponent<Tartaruga>(); 

    }
    private void Update()
    {

        cena = SceneManager.GetActiveScene();

        if (cena.name == "Z" + GameManager.zona + "F" + GameManager.fase + " Boss")
        {
            boss = true;
        } else
        {
            boss = false;
        }


        if (Input.GetKeyDown(KeyCode.P))
        {


            if (Time.timeScale == 1)
            {
                PlayerController.podeExecutar = false;
                turtle.shieldAtivo = false;
                Time.timeScale = 0;
                PauseVolta.PauseGame();

            }
        
            else if (Time.timeScale == 0)
            {
                PlayerController.podeExecutar = true;
                turtle.shieldAtivo = false;
                Time.timeScale = 1;
                PauseVolta.ResumeGame();

            }

        }

        
        
        
    } 
    
}
