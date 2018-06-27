using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlmaLerp : MonoBehaviour {

	public static AlmaLerp instance;

	public RectTransform almaRect;

	private Vector2 basePosition = new Vector2(-900, -700);
	private Vector2 finalPosition = Vector2.zero;

	void Awake() {

		instance = this;
	}

	public IEnumerator Lerp() {

		almaRect.sizeDelta = Vector2.one * 75f;

		float t = 0;
		almaRect.gameObject.SetActive (true);

		while (t < 1) {
			
			almaRect.anchoredPosition = Vector2.Lerp (basePosition, finalPosition, t);
			t += Time.deltaTime * 2f;
			yield return null;
		}

		almaRect.gameObject.SetActive (false);
	}
}
