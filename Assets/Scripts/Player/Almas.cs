using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Almas : MonoBehaviour
{

    //private bool flutuaCima;
    public Vector2 posicao;

    // Use this for initialization
    void Awake()
    {
       posicao = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        //transform.Rotate(10 * Time.deltaTime, 0 * Time.deltaTime, 0f * Time.deltaTime); 

    }

    void FixedUpdate()
    {
        float variacao = Mathf.Sin(Time.fixedTime);
           
        transform.position = posicao + variacao * Vector2.up;
    }

}
   
