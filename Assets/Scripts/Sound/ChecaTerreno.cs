using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChecaTerreno : MonoBehaviour {

	[HideInInspector]
	public static float terreno;
	public float terrenoAtual;

	void OnCollisionEnter2D (Collision2D other) {
		if (other.gameObject.tag == "Player") {
			terreno = terrenoAtual;
		}
	}
}
