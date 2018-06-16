using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour {

	[SerializeField]
	private GameObject destroy;

	public void Destructor() {

		Instantiate (destroy, transform.position, transform.rotation);
		Destroy (gameObject, 0f);
	}
}
