// Menu
// Attached to Main Camera? 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

public class MainMenu : MonoBehaviour {

    string[] botoes = new string[5] { "      jogar", "      fases", "      instruções", "      créditos", "      sair" };
    int atual = 0;
    public GUIStyle myGUIStyle;

    [FMODUnity.EventRef]
    public static FMOD.Studio.EventInstance menuEv;
    public static FMOD.Studio.ParameterInstance pararParam;

    public string somSetaUp = "event:/Seta Up";
    public string somSetaDown = "event:/Seta Down";

    public static bool musicaJaTocou;

    public static bool pararMusica;
    public static bool killMusic;

	public GameManager gm;

	public Color32 hoverColor;


    void Awake ()
	{
        Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;

        if (!musicaJaTocou) {
			menuEv = FMODUnity.RuntimeManager.CreateInstance("event:/Menu");
			menuEv.start ();
		}
		musicaJaTocou = true;
        atual = 0;
        menuEv.getParameter("Parar", out pararParam);

        Instantiate(gm);
        GameManager.hasPlayed = true;

		hoverColor = new Color32(166, 242, 79, 255);
    }

    void Update()
    {

        //Debug.Log("MENU");

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

        gerenciaSom();


    }


    public static void gerenciaSom()
    {
        if (pararMusica) pararParam.setValue(1f);
        else pararParam.setValue(0f);
    }


    void OnGUI()
    {
		
        GUI.SetNextControlName(botoes[0]);

        //Quando Jogar estiver selecionado
        if (GUI.Button(new Rect(Screen.width * .025f, Screen.height * .38f, Screen.width * .05f, Screen.height * .08f), botoes[0], myGUIStyle)) { }


        GUI.SetNextControlName(botoes[1]);

		if (File.Exists (Application.dataPath + "/gameInfo.pug")) {

			myGUIStyle.normal.textColor = Color.white;
			myGUIStyle.hover.textColor = hoverColor;
		} else {
			myGUIStyle.normal.textColor = Color.gray;
			myGUIStyle.hover.textColor = Color.gray;
		}

		//Quando Jogar estiver selecionado
		if (GUI.Button (new Rect (Screen.width * .025f, Screen.height * .49f, Screen.width * .05f, Screen.height * .08f), botoes [1], myGUIStyle)) { }

		myGUIStyle.normal.textColor = Color.white;
		myGUIStyle.hover.textColor = hoverColor;


        GUI.SetNextControlName(botoes[2]);

        //Quando Opções estiver selecionado 
        if (GUI.Button(new Rect(Screen.width * .025f, Screen.height * .60f, Screen.width * .05f, Screen.height * .08f), botoes[2], myGUIStyle)) { }

        GUI.SetNextControlName(botoes[3]);

        //Quando Créditos estiver selecionado 
        if (GUI.Button(new Rect(Screen.width * .025f, Screen.height * .71f, Screen.width * .05f, Screen.height * .08f), botoes[3], myGUIStyle)) { }


        GUI.SetNextControlName(botoes[4]);

        //Quando Sair estiver selecionado 
        if (GUI.Button(new Rect(Screen.width * .025f, Screen.height * .82f, Screen.width * .05f, Screen.height * .08f), botoes[4], myGUIStyle)) { }


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

    void gerenciaBotoes ()
	{

		if (Event.current.isKey && (Input.GetButtonDown("Attack") && Input.anyKeyDown || Input.GetKeyDown(KeyCode.JoystickButton0))) {

			//Vai pro game manager
			if (GUI.GetNameOfFocusedControl () == botoes [0]) {
				GameManager.instance.OnChangeScene ("Cutscene");
			}

			/*    //Vai pra onde parou
            if (GUI.GetNameOfFocusedControl() == botoes[1])
            {
				if (GameManager.instance == null) Instantiate();
				GameManager.shouldLoad = true;
                //GameManager.hasPlayed = false;
                MainMenu.menuEv.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                MainMenu.menuEv.release();
            }*/

			//Vai pra onde parou
			if (GUI.GetNameOfFocusedControl () == botoes [1] && File.Exists (Application.dataPath + "/gameInfo.pug")) {

				GameManager.instance.OnChangeScene ("Fases");
                


			} else if (GUI.GetNameOfFocusedControl () == botoes [1] && !File.Exists (Application.dataPath + "/gameInfo.pug")) {
				myGUIStyle.hover.textColor = Color.gray;
			}


            //Vai pras instruções
            if (GUI.GetNameOfFocusedControl() == botoes[2])
                GameManager.instance.OnChangeScene("Instrucoes");

            //Vai pros créditos
            if (GUI.GetNameOfFocusedControl() == botoes[3])
                GameManager.instance.OnChangeScene("Creditos");

            
            //Sai do jogo
            if (GUI.GetNameOfFocusedControl() == botoes[4])
                Application.Quit();

           
        }

    }


}
