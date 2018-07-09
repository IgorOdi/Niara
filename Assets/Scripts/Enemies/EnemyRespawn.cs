using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRespawn : MonoBehaviour {

	public static EnemyRespawn instance;
	public EnemyBehaviour[] inimigos;
	public Vector3[] positions;

	void Start() {

		instance = this;

		inimigos = new EnemyBehaviour[transform.childCount];
		positions = new Vector3[inimigos.Length];

		for (int i = 0; i < inimigos.Length; i++) {

			inimigos [i] = transform.GetChild (i).GetComponent<EnemyBehaviour>();
			positions [i] = inimigos [i].transform.position;
		}
	}

	public void Respawn() {

		for (int i = 0; i < inimigos.Length; i++) {

			inimigos [i].gameObject.SetActive (true);
			inimigos [i].Start ();
		}
	}
}
