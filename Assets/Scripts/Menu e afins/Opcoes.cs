using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Opcoes : MonoBehaviour {
    
    string[] botoes = new string[2] { "som", "musica"};
    int atual = 0;
    public GUIStyle myGUIStyle;
    public GameObject noS, noM;
    bool ativaNoM, ativaNoS;
    public string somSetaUp = "event:/Seta Up";
    public string somSetaDown = "event:/Seta Down";


    void Start()
    {
        atual = 0;
        noS.SetActive(false);
        noM.SetActive(false);

    }


    void Update()
    {
		
		MainMenu.gerenciaSom();

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            FMODUnity.RuntimeManager.PlayOneShot(somSetaUp, transform.position);
            atual = selecionaMenu(botoes, atual, "up");

        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            FMODUnity.RuntimeManager.PlayOneShot(somSetaDown, transform.position);
            atual = selecionaMenu(botoes, atual, "down");

        }

    }


    void OnGUI()
    {

        GUI.SetNextControlName(botoes[0]);

        //Quando Jogar estiver selecionado
        // if (GUI.Button(new Rect(-722, -467, 390, 75), botoes[0])){        
        if (GUI.Button(new Rect(600, 235, 120, 120), botoes[0], myGUIStyle)) { }


        GUI.SetNextControlName(botoes[1]);

        //Quando Opções estiver selecionado 
        //if (GUI.Button(new Rect(-230, -467, 390, 75), botoes[1])){
        if (GUI.Button(new Rect(600, 420, 120, 120), botoes[1], myGUIStyle)) { }


        gerenciaBotoes();


        GUI.FocusControl(botoes[atual]);


    }

    int selecionaMenu(string[] arrayBotoes, int selecionado, string direcao)
    {

        if (direcao == "up")
        {

            if (selecionado == 0)
            {

                selecionado = arrayBotoes.Length - 1;

            }

            else
            {

                selecionado -= 1;

            }

        }

        if (direcao == "down")
        {

            if (selecionado == arrayBotoes.Length - 1)
            {

                selecionado = 0;

            }

            else
            {

                selecionado += 1;

            }

        }

        return selecionado;

    }

    void gerenciaBotoes()
    {

		if (Event.current.isKey && Event.current.keyCode == KeyCode.Z && Input.anyKeyDown)
        {

            //Vai pro game manager
            if (GUI.GetNameOfFocusedControl() == botoes[0])
            {

                noS.SetActive(!ativaNoS);
                ativaNoS = !ativaNoS;
                GameManager.sfxStop = !GameManager.sfxStop;
               

            }

            //Vai pras opções
            if (GUI.GetNameOfFocusedControl() == botoes[1])
            {

                noM.SetActive(!ativaNoM);
                ativaNoM = !ativaNoM;
                MainMenu.pararMusica = !MainMenu.pararMusica;
                
            }

        }


        else if (Event.current.isKey && Event.current.keyCode == KeyCode.X)
        {

            SceneManager.LoadScene("Menu");

        }
                       
        

    }

}
