using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BarraInimigo : MonoBehaviour {

    private EnemyBehaviour enemyScript;

    private string somBossMorte = "event:/Boss Morte";

    public GameManager gm;

    public static bool onBoss;

    [SerializeField]
    public Image vidaInimigo;

    void Start()
    {

        enemyScript = (EnemyBehaviour)GetComponent(typeof(EnemyBehaviour));
    }

    void Update()
    {
		if (vidaInimigo != null) vidaInimigo.fillAmount = enemyScript.vidas / enemyScript.vidasMax;

		if (enemyScript.vidas <= 0 && SceneManager.GetActiveScene ().name != "Z3F1 Boss") {
			if (!GameManager.sfxStop)
				FMODUnity.RuntimeManager.PlayOneShot (somBossMorte, transform.position);
			gm.ChamaVitoria ();
			onBoss = true;

		}
    }
}
