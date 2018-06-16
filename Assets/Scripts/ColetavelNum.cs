using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColetavelNum : MonoBehaviour {

    public SpriteRenderer coletaN;
    public Sprite[] spritesC;


    // Use this for initialization
    void Awake()
    {
        coletaN = GetComponent<SpriteRenderer>();
      //  Coletavel.coletaveis = 0;

    }

    void Update()
    {

        
        coletaN.sprite = spritesC[Coletavel.coletaveis];

    }
   
}
