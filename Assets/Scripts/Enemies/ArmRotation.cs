using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmRotation : MonoBehaviour {

    private GameObject player;
	[SerializeField]
	private Transform enemy;

    private void Start() {

        player = GameObject.FindGameObjectWithTag("Player");
		enemy = GetComponentsInParent<Transform> ()[1];
    }

    void Update() {

		//ROTAÇÃO//

		var Z = Mathf.Atan2 (transform.position.y - player.transform.position.y, transform.position.x - player.transform.position.x) * Mathf.Rad2Deg;

		transform.rotation = Quaternion.Euler (0, 0, Z);

		//FLIP//

		if (enemy.localScale.x == 1) {

			transform.localScale = new Vector3 (1, 0, 0);
		} else {

			transform.localScale = new Vector3 (-1, 0, 0);
		}
    }
}
