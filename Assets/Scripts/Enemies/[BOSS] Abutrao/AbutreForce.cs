using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbutreForce : MonoBehaviour {

	[SerializeField]
	private Transform enemy;
	AreaEffector2D areaEffector;

	void Awake() {

		enemy = GetComponentsInParent<Transform> ()[1];
		areaEffector = GetComponent<AreaEffector2D> ();
	}

	void Update() {

		if (enemy.localScale.x == 1) {

			areaEffector.forceMagnitude = -500;
		} else {

			areaEffector.forceMagnitude = 500;
		}
	}
}
