using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GanhaHabilidade : MonoBehaviour {


	[SerializeField]
	private string habilidadeGanha;

    //Tutoriais 
    [SerializeField]
    private GameObject tutoMico;
    [SerializeField]
    private GameObject tutoArara;
    [SerializeField]
    private GameObject tutoJabuti;
    [SerializeField]
    private GameObject[] tutoOnca;
    private float alpha1;
    private float alpha2;
    private float alpha3;
    private float alpha4;

    [SerializeField]
	private GameObject mico;
	[SerializeField]
	private GameObject arara;
	[SerializeField]
	private GameObject jabuti;
	[SerializeField]
	private GameObject onca;

	private GameObject powerUp;
	private bool instancia = true;
    private bool passouArv1 = false;
    private bool passouArv2 = false;
    private bool passouArv3 = false;
    private bool passouArv4 = false;

    void Awake ()
	{

    }

    private void Update()
    {
        
        if (passouArv1)
        {
            tutoMico.GetComponent<SpriteRenderer>().color = new Color(255, 255, 225, alpha1);
            //tutoMico.SetActive(true);
            alpha1 += 0.01f;
        }
         if (passouArv2)
        {
            tutoArara.GetComponent<SpriteRenderer>().color = new Color(255, 255, 225, alpha2);
            //tutoMico.SetActive(true);
            alpha2 += 0.01f;
        }
         if (passouArv3)
        {
            tutoJabuti.GetComponent<SpriteRenderer>().color = new Color(255, 255, 225, alpha3);
            //tutoMico.SetActive(true);
            alpha3 += 0.01f;
        }
        if (passouArv4)
        {
            tutoOnca[0].GetComponent<SpriteRenderer>().color = new Color(255, 255, 225, alpha4);
            tutoOnca[1].GetComponent<SpriteRenderer>().color = new Color(255, 255, 225, alpha4);

            //tutoMico.SetActive(true);
            alpha4 += 0.01f;
        }

    }

    void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.tag == "Player") { 

			GameManager.GerenciaHabilidades (habilidadeGanha);

			if (instancia) {

				switch (habilidadeGanha) {

				case "mico":
					powerUp = mico;
                    passouArv1 = true;                    
                    break;

				case "arara":
					powerUp = arara;
                        passouArv2 = true;
                        //tutoArara.SetActive(true);
                    break;

				case "jabuti":
					powerUp = jabuti;
                        passouArv3 = true;
                        //tutoJabuti.SetActive(true);
                    break;

				case "onca":
					powerUp = onca;
                        passouArv4 = true;
                        //tutoOnca[0].SetActive(true);
                        //tutoOnca[1].SetActive(true);
                        break;
                }

				Instantiate (powerUp, new Vector2 (transform.position.x, transform.position.y - 3), Quaternion.identity);
				instancia = false;
			}
		}
	}
}
