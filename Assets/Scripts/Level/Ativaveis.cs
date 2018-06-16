using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ativaveis : MonoBehaviour {

	[SerializeField]
	protected GameObject objetoAlterado;

	Animator anim;

	public bool activated;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> (); 
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (activated);
	}

	void OnTriggerEnter2D(Collider2D other) {

		if (other.gameObject.tag == "Weapon" || other.gameObject.tag == "Pedra") {
			
			activated = true;
			anim.SetBool ("Ativar", true);
		}
	}
}
