using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour {

	[SerializeField]
	private Image imageToFade;
	private float fadeTime;

	public void FadeIn() {
		imageToFade = GetComponent<Image> ();
		imageToFade.CrossFadeAlpha (0, 1, false);
	}

	public void FadeOut() {
		imageToFade = GetComponent<Image> ();
		imageToFade.CrossFadeAlpha (1, 1, false);


	}
}
