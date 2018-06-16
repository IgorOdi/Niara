using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EnemySpawner))]
public class EnemySpawnerEditor : Editor {

	public GameObject source;

	public override void OnInspectorGUI() {

		DrawDefaultInspector();

		EnemySpawner enemySpawner = (EnemySpawner)target;

		if (GUILayout.Button("Spawn Random Enemy")) {

			enemySpawner.spawnRandomEnemy();
		}
	}
}
