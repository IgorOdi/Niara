using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	public GameObject[] enemyToSpawn;
	public int enemyNumber;

	public enum Inimigo { Abutrinho, Escudeiro, Fornalho, GuardaMelee, GuardaRanged, Serrinha, Lixomancer, MonkeyBot, Morcego, Cachorrinho, Roda, Voador }

	[SerializeField]
	public Inimigo inimigo;

	public void spawnRandomEnemy() {

		switch (inimigo) {

		case Inimigo.Abutrinho:
			enemyNumber = 0;
			break;

		case Inimigo.Escudeiro:
			enemyNumber = 1;
			break;

		case Inimigo.Fornalho:
			enemyNumber = 2;
			break;

		case Inimigo.GuardaMelee:
			enemyNumber = 3;
			break;

		case Inimigo.GuardaRanged:
			enemyNumber = 4;
			break;

		case Inimigo.Serrinha:
			enemyNumber = 5;
			break;

		case Inimigo.Lixomancer:
			enemyNumber = 6;
			break;

		case Inimigo.MonkeyBot:
			enemyNumber = 7;
			break;

		case Inimigo.Morcego:
			enemyNumber = 8;
			break;

		case Inimigo.Cachorrinho:
			enemyNumber = 9;
			break;

		case Inimigo.Roda:
			enemyNumber = 10;
			break;

		case Inimigo.Voador:
			enemyNumber = 11;
			break;
		}

		Instantiate(enemyToSpawn[enemyNumber], transform.position, Quaternion.identity);
	}
}
