using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Creditos : MonoBehaviour {
    public GameObject[] nomes;
    string[] botoes = new string[2] { "inicio", "maru" };
    int atual = 0;

    public string somSetaUp = "event:/Seta Up";
    public string somSetaDown = "event:/Seta Down";

    // Use this for initialization
    void Start () {

        atual = 0;
        nomes[0].SetActive(true);

        for (int i = 1; i < nomes.Length; i++)
        {

            nomes[i].SetActive(false);

        }

    }
	

    void OnGUI()
    {

		if (Event.current.isKey && Input.GetButtonDown("Throw") && Input.anyKeyDown)
        {
            FMODUnity.RuntimeManager.PlayOneShot(somSetaDown, transform.position);
            GameManager.instance.OnChangeScene("Menu");

        }

        if (Event.current.isKey && Event.current.keyCode == KeyCode.LeftArrow && Input.anyKeyDown)
        {
            FMODUnity.RuntimeManager.PlayOneShot(somSetaDown, transform.position);
            if (botoes[1] != "inicio" && atual != 0)
            {
                atual--;
                telaVolta();

            }
            else
            {

                atual = 0;

            }


        }

        if (Event.current.isKey && Event.current.keyCode == KeyCode.RightArrow && Input.anyKeyDown)
        {
            FMODUnity.RuntimeManager.PlayOneShot(somSetaUp, transform.position);
            if (botoes[0] == "inicio" && atual != 7)
            {
                atual++;
                telaAtual();
            }

            else
            {
                atual = 7;

            }

        }



    }

    void telaVolta()
    {
        if (atual == 0)
        {
            botoes[0] = "inicio";
            nomes[0].SetActive(true);

            for (int i = 1; i < nomes.Length; i++)
            {

                nomes[i].SetActive(false);

            }

        }

        if (atual == 1)
        {
            nomes[0].SetActive(false);
            nomes[1].SetActive(true);

            for (int i = 2; i < nomes.Length; i++)
            {

                nomes[i].SetActive(false);

            }

        }

        if (atual == 2)
        {
            for (int i2 = 0; i2 < 2; i2++)
            {
                nomes[i2].SetActive(false);
            }

            nomes[2].SetActive(true);

            for (int i = 3; i < nomes.Length; i++)
            {

                nomes[i].SetActive(false);

            }

        }

        if (atual == 3)
        {
            for (int i2 = 0; i2 < 3; i2++)
            {
                nomes[i2].SetActive(false);
            }

            nomes[3].SetActive(true);

            for (int i = 4; i < nomes.Length; i++)
            {

                nomes[i].SetActive(false);

            }

        }

        if (atual == 4)
        {
            for (int i2 = 0; i2 < 4; i2++)
            {
                nomes[i2].SetActive(false);
            }

            nomes[4].SetActive(true);

            for (int i = 5; i < nomes.Length; i++)
            {

                nomes[i].SetActive(false);

            }

        }

        if (atual == 5)
        {
            for (int i2 = 0; i2 < 5; i2++)
            {
                nomes[i2].SetActive(false);
            }

            nomes[5].SetActive(true);

            for (int i = 6; i < nomes.Length; i++)
            {

                nomes[i].SetActive(false);

            }

        }

        if (atual == 6)
        {
            for (int i2 = 0; i2 < 6; i2++)
            {
                nomes[i2].SetActive(false);
            }

            nomes[6].SetActive(true);

            for (int i = 7; i < nomes.Length; i++)
            {

                nomes[i].SetActive(false);

            }

        }

        if (atual == 7)
        {
            botoes[1] = "maru";
            for (int i2 = 0; i2 < 7; i2++)
            {
                nomes[i2].SetActive(false);
            }

            nomes[7].SetActive(true);

            for (int i = 8; i < nomes.Length; i++)
            {

                nomes[i].SetActive(false);

            }

        }


    }

    void telaAtual()
    {

        if (atual == 0)
        {

            nomes[0].SetActive(true);

            for (int i = 1; i < nomes.Length; i++)
            {

                nomes[i].SetActive(false);

            }

        }

        if (atual == 1)
        {
            nomes[0].SetActive(false);
            nomes[1].SetActive(true);

            for (int i = 2; i < nomes.Length; i++)
            {

                nomes[i].SetActive(false);

            }

        }

        if (atual == 2)
        {
            for (int i2 = 0; i2 < 2; i2++)
            {
                nomes[i2].SetActive(false);
            }

            nomes[2].SetActive(true);

            for (int i = 3; i < nomes.Length; i++)
            {

                nomes[i].SetActive(false);

            }

        }

        if (atual == 3)
        {
            for (int i2 = 0; i2 < 3; i2++)
            {
                nomes[i2].SetActive(false);
            }

            nomes[3].SetActive(true);

            for (int i = 4; i < nomes.Length; i++)
            {

                nomes[i].SetActive(false);

            }

        }

        if (atual == 4)
        {
            for (int i2 = 0; i2 < 4; i2++)
            {
                nomes[i2].SetActive(false);
            }

            nomes[4].SetActive(true);

            for (int i = 5; i < nomes.Length; i++)
            {

                nomes[i].SetActive(false);

            }

        }

        if (atual == 5)
        {
            for (int i2 = 0; i2 < 5; i2++)
            {
                nomes[i2].SetActive(false);
            }

            nomes[5].SetActive(true);

            for (int i = 6; i < nomes.Length; i++)
            {

                nomes[i].SetActive(false);

            }

        }

        if (atual == 6)
        {
            for (int i2 = 0; i2 < 6; i2++)
            {
                nomes[i2].SetActive(false);
            }

            nomes[6].SetActive(true);

            for (int i = 7; i < nomes.Length; i++)
            {

                nomes[i].SetActive(false);

            }

        }

        if (atual == 7)
        {
            botoes[1] = "maru";
            for (int i2 = 0; i2 < 7; i2++)
            {
                nomes[i2].SetActive(false);
            }

            nomes[7].SetActive(true);

            for (int i = 8; i < nomes.Length; i++)
            {

                nomes[i].SetActive(false);

            }

        }


    }

}

