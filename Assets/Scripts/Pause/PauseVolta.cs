using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseVolta : MonoBehaviour {
    //ISSO EXISTE SÓ 
    // Pausing Game
    public static void PauseGame()
    {
        SceneManager.LoadScene("Pause", LoadSceneMode.Additive);
    }

    // Resuming Game
    public static void ResumeGame()
    {
        Pause1 p1 = FindObjectOfType<Pause1>();
        p1.pausado = false;
        SceneManager.UnloadSceneAsync("Pause");
    }

}
