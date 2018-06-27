using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrensaSounds : MonoBehaviour {

	[FMODUnity.EventRef]
	public string somPrensaUp;

	[FMODUnity.EventRef]
	public string somPrensaHit;

	public void Collide() {

		FMODUnity.RuntimeManager.PlayOneShot(somPrensaHit, transform.position);
	}

	public void Subir() {

		FMODUnity.RuntimeManager.PlayOneShot (somPrensaUp, transform.position);
	}
}
