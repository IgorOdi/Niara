using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllResolutions : MonoBehaviour {

    public float originalWidth = 1920f;
    public float originalHeight = 1080f;
    private Vector3 escala;
    public Transform carinha;
    public Texture[] imageVida;
    public Texture niaraHUD;
    private int wArray;
    private int cont;

    void OnGUI()
    {
        escala.x = Screen.width / originalWidth; // calculate hor scale
        escala.y = Screen.height / originalHeight; // calculate vert scale
        escala.z = 1;
        Matrix4x4 salvaMat = GUI.matrix; // save current matrix
                                // substitute matrix - only scale is altered from standard
        GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, escala);
        // draw your GUI controls here:

        GUI.Label(new Rect(carinha.position.x, carinha.position.y, 100, 100), niaraHUD);
        wArray = 200;

        for (cont = 0; cont < PlayerController.vidas; cont++)
        {

            GUI.Label(new Rect(wArray, 20, 100, 130), imageVida[cont]);
            wArray += 55;

        }

        //...
        // restore matrix before returning
        GUI.matrix = salvaMat; // restore matrix
    }
}
