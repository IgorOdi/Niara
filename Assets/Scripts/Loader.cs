using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class Loader : MonoBehaviour 
{
	public GameObject gameManager;          //GameManager prefab to instantiate.

	[SerializeField]
	private GameManager gm;

	void Awake ()
	{
		//Check if a GameManager has already been assigned to static variable GameManager.instance or if it's still null
		if (GameManager.instance == null) {

			//Instantiate gameManager prefab
			Instantiate (gameManager);
		
		}

		if (!GameManager.shouldLoad || !SelecionaFase.onStageSelect) {
			SceneManager.LoadScene ("Z1F0");
			GameManager.fase = 0;
			GameManager.zona = 1;
	
		}

	}
}