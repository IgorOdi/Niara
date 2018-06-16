using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRespawn : MonoBehaviour {

	public static bool respawnEnemy;
	private bool respawnLocal;
    private bool caiporaAlada;
    [SerializeField]
    private GameObject enemyToSpawn;
    private LayerMask enemyLayer;

    private void Start()
    {
        enemyLayer = LayerMask.GetMask("Inimigo");
    }

    private void Update() {
		
        caiporaAlada = Physics2D.OverlapBox(transform.position, new Vector2(25, 3), 0, enemyLayer);
        
		respawnLocal = respawnEnemy;

		if (respawnLocal) {
			
			if (!caiporaAlada) {   
				
				Instantiate (enemyToSpawn, transform.position, Quaternion.identity);
				respawnLocal = false;
			}
		}
    }

//	void OnDrawGizmos() {
//
//		Gizmos.DrawCube (transform.position, new Vector2 (25, 3));
//	}

	void LateUpdate() {

		respawnEnemy = false;
	}
}
