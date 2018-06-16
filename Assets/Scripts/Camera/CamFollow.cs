using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour {

	public static byte panIndex;
	
	private GameObject[] cameraPanPosition;
	private GameObject endPosition;
	[SerializeField]
	protected Transform player; //Pega posição do player.

	[HideInInspector]
	public static bool cameraShouldPan;
	[HideInInspector]
	public static bool cameraExitPan;
	[HideInInspector]
	public static bool cameraShouldStop;

	public static bool lockCamera = true;

	[HideInInspector]
	public static float offset = 3; //Distancia que será usada entre o centro da câmera e a posição X do player em relação a câmera.
	[HideInInspector]
	public static float offsetY = 0;
	float camXpos; //Variável pra guardar a posição X final da câmera. { Será baseada na posição do player + offsets
	float camYpos; //Variável pra guardar a posição Y final da câmera. }
	float camSize = 7;

	void Awake () {

		endPosition = GameObject.FindGameObjectWithTag ("Limite");
		cameraPanPosition = GameObject.FindGameObjectsWithTag ("CameraPan");
		cameraShouldStop = false;
		cameraShouldPan = false;

		for (int i = 0; i < cameraPanPosition.Length; i++) {

			Debug.Log(cameraPanPosition[i].name);

		}

	}

	void Update() {

		camXpos = player.position.x + offset; //Pega a posição do player + diferença entre a posição x do player em relação a camera.
		camYpos = player.position.y + offsetY;



		if (camXpos <= 0f) { //Limita o movimento da câmera em x no inicio da fase.
			camXpos = 0f;
		}

//		else if (camXpos >= 210f) { //Limita o movimento da câmera em x no final da fase.
//			camXpos = 210f;
//		}

		if (lockCamera) {

			if (camYpos <= 0) { //Limita o movimento da câmera em y.
				camYpos = 0;
			}
		} else {

		}


		Camera.main.orthographicSize = camSize;


}

	void LateUpdate () {


		if (cameraShouldPan) {
			transform.position = Vector3.Lerp (transform.position, cameraPanPosition[panIndex].transform.position, Time.fixedDeltaTime * 0.8f);
			camSize = Mathf.Lerp (camSize, 10f, Time.fixedDeltaTime * 3f);
		}

		  else if (cameraExitPan) {
			camSize = Mathf.Lerp (camSize, 7f, Time.fixedDeltaTime * 3f);

			Vector3 endPosition = new Vector3 (camXpos, camYpos, transform.position.z); //Posição final da câmera após movimento do player: move em X, Y e se mantêm fixa em Z.

			transform.position = Vector3.Lerp (transform.position, endPosition, Time.fixedDeltaTime * 1.5f); //Faz uma interpolação linear entre a posição inicial e final da câmera.

			if (transform.position == endPosition) cameraExitPan = false;
		} 


		else if (cameraShouldStop) {
			transform.position = transform.position;
		}



		else {

			Vector3 endPosition = new Vector3 (camXpos, camYpos, transform.position.z); //Posição final da câmera após movimento do player: move em X, Y e se mantêm fixa em Z.

			transform.position = Vector3.Lerp (transform.position, endPosition, Time.fixedTime); //Faz uma interpolação linear entre a posição inicial e final da câmera.
		}


	}


}