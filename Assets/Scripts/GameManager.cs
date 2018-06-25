using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using FMODUnity;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

[Serializable]
public class GameManager : MonoBehaviour {

	[FMODUnity.EventRef]
	private string somBGM = "event:/Musica Fases";
	public static FMOD.Studio.EventInstance bgmEv;
	public static FMOD.Studio.ParameterInstance arvoreParam;
	public static FMOD.Studio.ParameterInstance pararParam;
	public static FMOD.Studio.ParameterInstance zonaParam;
	public static FMOD.Studio.ParameterInstance faseParam;
	public static FMOD.Studio.ParameterInstance onBossParam;
	public static FMOD.Studio.ParameterInstance traficanteDeadParam;

	public GameObject vidinha1Prefab;
	public GameObject vidinha2Prefab;

	public GameObject player;
	bool instStats = false;
	bool instStats2 = false;
	public Transform posplayer;

	public static bool naArvore;
	public bool hasChanged;

	public static bool oncaAtiva;
	public static bool micoAtivo;
	public static bool araraAtivo;
	public static bool jabutiAtivo;

	public static byte fase;
	public static byte zona;

	public byte SavedFase;
	public byte SavedZona;

	public static bool hasPlayed = true;
	public bool pegouHabilidadesIniciais = false;

	public static GameManager instance = null;
	public Scene cena;

	public static bool sfxStop;

	public bool loaded;

	public static bool gameLoaded;

	private bool sceneLoaded;

	public static bool shouldLoad, shouldSave;

	// Use this for initialization
	void Awake () {

		if (shouldLoad) {

//			Load ();
			shouldLoad = false;
		}

		if (instance == null) instance = this;

		else if (instance != this) Destroy(gameObject);    

		DontDestroyOnLoad(gameObject);

		hasChanged = false;

		//MainMenu.menuEv.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
		//MainMenu.menuEv.release();

		if (!hasPlayed) {
			bgmEv = FMODUnity.RuntimeManager.CreateInstance(somBGM);
			bgmEv.getParameter("naArvore", out arvoreParam);
			bgmEv.getParameter("Parar", out pararParam);
			bgmEv.getParameter("Zona", out zonaParam);
			bgmEv.getParameter("Fase", out faseParam);
			bgmEv.getParameter("OnBoss", out onBossParam);
			bgmEv.getParameter("FabricanteDead", out traficanteDeadParam);
			bgmEv.start();
			hasPlayed = true;
		}

//		Load ();

		//traficanteDeadParam.setValue(0);

	}

	public void OnChangeScene(string sceneName) {
		sceneLoaded = false;
		if (sceneName != SceneManager.GetActiveScene().name && !sceneLoaded) {
			StartCoroutine (ChangeScene(sceneName));
		}
	}

	IEnumerator ChangeScene(string sceneName) {
		sceneLoaded = true;

		SceneManager.LoadScene ("LoadingScene");

		yield return new WaitForSeconds (0.5f);

		AsyncOperation asyncLoad = SceneManager.LoadSceneAsync (sceneName);

		while (!asyncLoad.isDone) {
			yield return null;
		}

		sceneLoaded = false;

	}

	void Update () {

		cena = SceneManager.GetActiveScene ();

		print(cena.name);

		zonaFase();

		if (zonaParam != null) zonaParam.setValue(zona);
		if (faseParam != null) faseParam.setValue(fase);

		if (cena.name.Contains("Z")) player = GameObject.FindGameObjectWithTag("Player");


		if (cena.name == "Z" + zona + "F" + fase + " Boss") {
			if (onBossParam != null) onBossParam.setValue (1);
			Debug.Log ("TÁ NO BOSS IHAAAA");
		} else {

			if (onBossParam != null) onBossParam.setValue (0);
		}

		if (MainMenu.pararMusica) {
			if (pararParam != null) pararParam.setValue (1f);
		} else {
			
			if (pararParam != null)pararParam.setValue(0f);
		}

		if (!pegouHabilidadesIniciais) {
			StartCoroutine(habilidadesIniciais());
			pegouHabilidadesIniciais = true;
		}

		if (PlayerController.recebeDano)
		{

			if (!instStats)
			{

				Instantiate(vidinha1Prefab, player.transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
				instStats = true;

			}

		}

		else
		{

			instStats = false;

		}

		if (ColetarAlmas.ganhaVida)
		{
			if (!instStats2)
			{
				Instantiate(vidinha2Prefab, player.transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
				instStats2 = true;
			}
		}
		else
		{

			instStats2 = false;

		}

		if (TRexAI.endGame)
			Invoke ("LoadFinal", 11f);

		if (shouldSave) {

			Save ();
			shouldSave = false;
		}

		if (shouldLoad) {

//			Load ();
			shouldLoad = false;
		}


	}

	public IEnumerator habilidadesIniciais ()
	{
		yield return new WaitForSeconds(0.5f);

		if (cena.name == "Z1F0") {
			micoAtivo = false;
			araraAtivo = false;
			jabutiAtivo = false;
			oncaAtiva = false;
			zona = 1;
			fase = 0;
		}  if (cena.name == "Z1F1") {
			micoAtivo = false;
			araraAtivo = false;
			jabutiAtivo = false;
			oncaAtiva = false;
			zona = 1;
			fase = 1;
			print("FASE 1");
		}  if (cena.name == "Z1F2") {
			araraAtivo = false;
			oncaAtiva = false;
			jabutiAtivo = false;
			micoAtivo = true;
			zona = 1;
			fase = 2;
		}  if (cena.name == "Z1F3") {
			oncaAtiva = false;
			jabutiAtivo = false;
			micoAtivo = true;
			araraAtivo = true;
			zona = 1;
			fase = 3;
		}  if (cena.name == "Z2F1") {
			oncaAtiva = false;
			jabutiAtivo = false;
			micoAtivo = true;
			araraAtivo = true;
			zona = 2;
			fase = 1;
		}  if (cena.name == "Z2F2") {
			oncaAtiva = false;
			micoAtivo = true;
			araraAtivo = true;
			jabutiAtivo = true;
			zona = 2;
			fase = 2;
		}  if (cena.name == "Z2F3") {
			micoAtivo = true;
			araraAtivo = true;
			jabutiAtivo = true;
			oncaAtiva = true;
			zona = 2;
			fase = 3;
		}
	}


	public void ChamaBoss() {
		zonaFase();
		Debug.Log ("Zona: " + zona + "  Fase: " + fase);
		if (fase > 0 && !IsInvoking())  Invoke ("LoadBoss", 2f);
		else if (!IsInvoking()) Invoke ("LoadFase", 2f);
	}

	public void ChamaFase () {
		if (!IsInvoking()) Invoke ("LoadFase", 1f);
		SelecionaFase.faseDesbloq++;
	}

	public void ChamaVitoria()
	{
		if (!IsInvoking()) Invoke("LoadVitoria", 1.5f);
	}

	public void LoadVitoria() {
		GameManager.instance.OnChangeScene("Ganhou");
	}

	public void LoadBoss() {
		GameManager.instance.OnChangeScene ("Z" + zona + "F" + fase + " Boss");
	}

	public void LoadFase () {
		if (fase < 3) fase++;
		else
		{
			fase = 1;
			zona++;
		}
		Coletavel.pegouColetavel = 0;
		GameManager.instance.OnChangeScene ("Z" + zona + "F" + fase);
	}

	void LoadFinal() {

		if (!loaded) {

			GameManager.instance.OnChangeScene ("Final");
			loaded = true;
		}
	}

	public static void GerenciaHabilidades (string habilidade) {		
		if (habilidade == "onca") oncaAtiva = true;
		else if (habilidade == "mico") micoAtivo = true;
		else if (habilidade == "jabuti") jabutiAtivo = true;
		else if (habilidade == "arara") araraAtivo = true;
	}


	public void zonaFase ()
	{
		if (cena.name == "Z1F0") {
			zona = 1;
			fase = 0;
		} else if (cena.name == "Z1F1") {
			zona = 1;
			fase = 1;
			if (SelecionaFase.faseDesbloq < 1) SelecionaFase.faseDesbloq = 1;
		} else if (cena.name == "Z1F2") {
			zona = 1;
			fase = 2;
			if (SelecionaFase.faseDesbloq < 2) SelecionaFase.faseDesbloq = 2;
		} else if (cena.name == "Z1F3") {
			zona = 1;
			fase = 3;
			if (SelecionaFase.faseDesbloq < 3) SelecionaFase.faseDesbloq = 3;
		} else if (cena.name == "Z2F1") {
			zona = 2;
			fase = 1;
			if (SelecionaFase.faseDesbloq < 4) SelecionaFase.faseDesbloq = 4;
		} else if (cena.name == "Z2F2") {
			zona = 2;
			fase = 2;
			if (SelecionaFase.faseDesbloq < 5) SelecionaFase.faseDesbloq = 5;
		} else if (cena.name == "Z2F3") {
			zona = 2;
			fase = 3;
			if (SelecionaFase.faseDesbloq < 6) SelecionaFase.faseDesbloq = 6;
		} else if (cena.name == "Z3F1") {
			zona = 3;
			fase = 1;
			if (SelecionaFase.faseDesbloq < 7) SelecionaFase.faseDesbloq = 7;
		}
	}

	public void Save() {

		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = new FileStream (Application.dataPath + "/gameInfo.pug", FileMode.Create);
		GameInfo gameInfo = new GameInfo ();

		gameInfo.oncaAtiva = oncaAtiva;
		gameInfo.micoAtivo = micoAtivo;
		gameInfo.araraAtivo = araraAtivo;
		gameInfo.jabutiAtivo = jabutiAtivo;
		gameInfo.faseDebloq = SelecionaFase.faseDesbloq;

		bf.Serialize (file, gameInfo);
		file.Close ();
	}

	public void Load() {

		if (File.Exists (Application.dataPath + "/gameInfo.pug")) {

			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = new FileStream (Application.dataPath + "/gameInfo.pug", FileMode.Open);
			GameInfo gameInfo = (GameInfo)bf.Deserialize (file);
			file.Close ();

			oncaAtiva = gameInfo.oncaAtiva;
			micoAtivo = gameInfo.micoAtivo;
			print (micoAtivo);
			araraAtivo = gameInfo.araraAtivo;
			jabutiAtivo = gameInfo.jabutiAtivo;
			SelecionaFase.faseDesbloq = gameInfo.faseDebloq;
			gameLoaded = true;

		}
	}
}

[Serializable]
class GameInfo {

	public float faseDebloq;

	public bool oncaAtiva;
	public bool micoAtivo;
	public bool araraAtivo;
	public bool jabutiAtivo;

}