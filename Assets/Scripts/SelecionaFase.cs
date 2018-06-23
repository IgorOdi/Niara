using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelecionaFase : MonoBehaviour {

    public int fase;

    public static float faseDesbloq;

    public GameManager gm;

    public GameObject[] num = new GameObject[7];

    public GameObject niarinhas;

    public static bool onStageSelect = false;

	public string somSetaUp = "event:/Seta Up";

	public string somSetaDown = "event:/Seta Down";

	// Use this for initialization
	void Start () {
		gm.zonaFase();
		StartCoroutine(gm.habilidadesIniciais());
	            
        niarinhas = GameObject.Find("Niarinhas");
        
        for (int i = 0; i < num.Length; i++)
        {
            int nome = i + 1;
            num[i] = GameObject.Find(nome.ToString());
            Debug.Log(i);
        }

        onStageSelect = true;
        }
	
	// Update is called once per frame
	void Update () {

		gm.zonaFase();

        faseDesbloq = Mathf.Clamp(faseDesbloq, 0, 6);

        trocaFase();

        DesbloqueiaFase();

		if (Input.GetKeyDown(KeyCode.RightArrow))
        {
			FMODUnity.RuntimeManager.PlayOneShot(somSetaUp, transform.position);

            if (fase < faseDesbloq - 1)
            fase++;
            
        }

        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
			FMODUnity.RuntimeManager.PlayOneShot(somSetaDown, transform.position);

            if (fase > 0)
            fase--;

        }

        if (Input.GetButtonDown("Attack"))
        {

            ChamaFase();

        }

        else if (Input.GetButtonDown("Throw")) {
      		GameManager.instance.OnChangeScene("Menu");
        }
	}

    void trocaFase()
    {
        int i = 0;
        foreach (Transform carinha in niarinhas.transform)
        {
            if (i == fase)
            {

                carinha.gameObject.SetActive(true);

            }
            else
            {

                carinha.gameObject.SetActive(false);
                
            }
            i++;
        }

        
    }

    void ChamaFase()
    {

    	gm.zonaFase();


        if (fase == 0) GameManager.instance.OnChangeScene("Z1F1");
        else if (fase == 1) GameManager.instance.OnChangeScene("Z1F2");
        else if (fase == 2) GameManager.instance.OnChangeScene("Z1F3");
        else if (fase == 3) GameManager.instance.OnChangeScene("Z2F1");
        else if (fase == 4) GameManager.instance.OnChangeScene("Z2F2");
        else if (fase == 5) GameManager.instance.OnChangeScene("Z2F3");
        else if (fase == 6) GameManager.instance.OnChangeScene("Z3F1");

        //GameManager.hasPlayed = false;
        MainMenu.menuEv.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        MainMenu.menuEv.release();
		GameManager.hasPlayed = false;
    }

    void DesbloqueiaFase()
    {

        for (int i = 0; i < num.Length; i++)
        {
            if(faseDesbloq > i) num[i].SetActive(true);
            else num[i].SetActive(false);
        }

    }


    private void OnDestroy()
    {
        onStageSelect = false;
    }


}
