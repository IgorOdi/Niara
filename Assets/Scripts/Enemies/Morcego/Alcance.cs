using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alcance : MonoBehaviour {

    public bool ativar;

    private void OnTriggerEnter2D(Collider2D other) {
         
        if (other.gameObject.tag == "Player") {

            ativar = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
         
        if (other.gameObject.tag == "Player") {

            ativar = false;
        }
    }
}
