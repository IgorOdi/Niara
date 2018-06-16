using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaioLaser : MonoBehaviour {

	private float laserTime;
	[SerializeField]
	private GameObject laser;

	void Update() {

		laserTime += Time.deltaTime;

		if (laserTime > 0 && laserTime < 2) {

			laser.SetActive (false);
		} else if (laserTime > 2 && laserTime < 4) {

			laser.SetActive (true);
		} else if (laserTime > 4) {

			laserTime = 0;
		}
	}
}
