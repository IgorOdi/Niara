using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LixoBall : MonoBehaviour {

    private float speed;
    private int dano;

    private void Start()
    {

        speed = 0.25f;
        dano = 1;

        Destroy(gameObject, 5f);
    }

    void Update()
    {

    	transform.Translate(new Vector3(-speed, 0));

    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag == "Player")
        { //com o Player,

			PlayerController.Damage (dano, false);
            
        }

        Destroy(gameObject, 0f);
    }
}
