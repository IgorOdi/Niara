using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prensa : MonoBehaviour {

	private GameObject player;
	[SerializeField]
	private Rigidbody2D rb;


	private bool caiu;

	private bool active = false;

	[FMODUnity.EventRef]
	public string somPrensaUp;

	[FMODUnity.EventRef]
	public string somPrensaHit;

	public bool upPlayed;
	public bool hitPlayed = false;

	void Awake () {

		player = GameObject.FindGameObjectWithTag("Player");

	}
	
	void Update ()
	{

		if (Vector2.Distance(new Vector2(this.transform.position.x,0), new Vector2(player.transform.position.x,0)) < 12f) active = true;
		else active = false;

		if (active) {
			rb.bodyType = RigidbodyType2D.Dynamic;
			rb.gravityScale = 0.5f;

			if (caiu) {
				if (!hitPlayed) {
					FMODUnity.RuntimeManager.PlayOneShot(somPrensaHit, this.transform.position);
					hitPlayed = true;
				}

				upPlayed = false;

				StartCoroutine (Subir ());
			} else {
				
			}
		}

		else {
			rb.bodyType = RigidbodyType2D.Static;
		}
	}

	void OnCollisionEnter2D (Collision2D other)
	{
		if (other.gameObject.tag == "Player" && rb.velocity.y < 0 && PlayerController.vulneravel) {
			PlayerController.vidas--;
			PlayerController.recebeDano = true;
			PlayerController.vulneravel = false;
		}
		else if (other.gameObject.tag == "Chão") {
			
			caiu = true;
		}
	}

	void OnCollisionExit2D (Collision2D other)
	{
		if (other.gameObject.tag == "Player") {
			
		}
	}

	IEnumerator Subir() {
			yield return new WaitForSeconds(0.5f);
			caiu = false;
			rb.gravityScale = -1f;
			hitPlayed = false;
			if (!upPlayed) {
			FMODUnity.RuntimeManager.PlayOneShot(somPrensaUp, this.transform.position);
			upPlayed = true;
			}

	}
}
