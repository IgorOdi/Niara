using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour {
    //ISSO TÁ NA FUCKING CENA DO PAUSE 
    string[] botoes = new string[3] { "continuar", "menu", "sair"};
    //public string somSeta = "event:/Seta";
    public string somSetaUp = "event:/Seta Up";
    public string somSetaDown = "event:/Seta Down";
    int atual;
    public GUIStyle myGUIStyle;
    public Camera camera1;    

    // Use this for initialization
    void Awake()
    {
        atual = 0;           
        camera1 = Camera.main.GetComponent<Camera>();
     
    }

    // Update is called once per frame
    void Update()
    {
        if (Pause1.boss)
        {

            transform.localScale = new Vector3(1.5f, 1.5f, 0f);

        }
        else
        {
            transform.localScale = new Vector3(1, 1, 0f);
        }

        this.transform.position = new Vector2(camera1.transform.position.x, camera1.transform.position.y);

        if (Time.timeScale != 1)
        {
            PlayerController.passosEv.setPaused(true);
        }

        else
        {
            PlayerController.passosEv.setPaused(false);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (Time.timeScale != 1) 
            {
                
                FMODUnity.RuntimeManager.PlayOneShot(somSetaUp, transform.position);

            }
            
            atual = selecionaMenu(botoes, atual, "up");

        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (Time.timeScale != 1)
            {

                FMODUnity.RuntimeManager.PlayOneShot(somSetaDown, transform.position);

            }
            atual = selecionaMenu(botoes, atual, "down");

        }
        

    }

    void OnGUI() {
        
        organizaBotoes();
       
    }

    int selecionaMenu(string[] arrayBotoes, int selecionado, string direcao){

        if (direcao == "up"){

            if (selecionado == 0) selecionado = arrayBotoes.Length - 1;
            else selecionado -= 1;
                        
        }

        if (direcao == "down"){

            if (selecionado == arrayBotoes.Length - 1) selecionado = 0;
            else selecionado += 1;

        }

        return selecionado;

    }
    

    void gerenciaBotoes()
    {

        if (Event.current.isKey && Event.current.keyCode == KeyCode.Z && Input.anyKeyDown) {

            //Vai pro game manager
            if (GUI.GetNameOfFocusedControl() == botoes[0]){

                Time.timeScale = 1;
                PauseVolta.ResumeGame();
                PlayerController.podeExecutar = true;
                //hidePaused();

            }

            //Vai pras opções
            if (GUI.GetNameOfFocusedControl() == botoes[1]){
            	GameManager.bgmEv.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            	MainMenu.musicaJaTocou = false;
                GameManager.instance.OnChangeScene("Menu");
            }

            if (GUI.GetNameOfFocusedControl() == botoes[2])
            {

                Application.Quit();
                //Debug.Log("saiu");

            }


        }       
        
    }

	void organizaBotoes()
	{
		if (Time.timeScale == 0)
		{
			GUI.SetNextControlName(botoes[0]);

			//Quando Continuar estiver selecionado   
			if (GUI.Button(new Rect(960, 350, 120, 100), botoes[0], myGUIStyle)) { }

			GUI.SetNextControlName(botoes[1]);

			//Quando Menu estiver selecionado 
			if (GUI.Button(new Rect(960, 480, 120, 100), botoes[1], myGUIStyle)) { }

			GUI.SetNextControlName(botoes[2]);

			//Quando Opções estiver selecionado
			if (GUI.Button(new Rect(960, 610, 120, 100), botoes[2], myGUIStyle)) { }


			gerenciaBotoes();

			GUI.FocusControl(botoes[atual]);

		}
	}
}
