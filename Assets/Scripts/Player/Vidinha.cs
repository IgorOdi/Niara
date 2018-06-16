using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Vidinha : MonoBehaviour
{
    public GameObject img;
    public Transform começa;
    public Transform fim;
    public float speed = 1.0F;
    private float startTime;
    private float journeyLength;


    void Start()
    {
        //  img.SetActive(false);
        Vector2 scaleVida = transform.localScale; // ... Pega o scale do jogador e guarda em um Vector2.

       // Debug.Log("Scale1: " + transform.localScale);
        scaleVida.x = Mathf.Abs(1);  // ... Inverte o valor X do Vector2 de escala.
        transform.localScale = scaleVida;  // ... Usa o valor invertido do Vector2 para inverter a escala e virar o jogador.
      //  Debug.Log("Scale2: " + transform.localScale);

        gameObject.transform.SetParent(null);
        startTime = Time.time;
        journeyLength = Vector3.Distance(começa.position, fim.position);
    }
    void FixedUpdate()
    {

       

        float distCovered = (Time.time - startTime) * speed;
        float fracJourney = distCovered / journeyLength;
        img.transform.position = Vector3.Lerp(começa.position, fim.position, fracJourney);
        Destroy(gameObject, 1f);

    }
    


}
