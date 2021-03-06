﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LixoWave : MonoBehaviour {

    private float speed;
    private int dano;

    private float distancia;
    private float sideMultiplier;
    private Transform player;

    private void Start()
    {

        speed = 0.3f;
        dano = 1;

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        distancia = transform.position.x - player.transform.position.x;

        Destroy(gameObject, 5f);
    }

    void Update()
    {
        sideMultiplier = distancia > 0 ? 1 : -1;

        transform.Translate(new Vector3(-speed * sideMultiplier, 0));
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
