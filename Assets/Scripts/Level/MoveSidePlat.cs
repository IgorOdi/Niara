using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSidePlat : MonoBehaviour {

	private GameObject player;
	[SerializeField]
	private Rigidbody2D rb;
	[SerializeField]
	private float speed;
	private float initialPos;
	private float finalPos;
	public float distancia;

	private bool active;

	void Awake () {

		speed = 0.1f;
		initialPos = transform.position.x;
		finalPos = transform.position.x + distancia;
		player = GameObject.FindGameObjectWithTag("Player");
		rb = player.GetComponent<Rigidbody2D> ();

	}
	
	void FixedUpdate ()
	{	

		if (transform.position.x - player.transform.position.x < 6f) active = true;

		if (active) {
			if (transform.position.x > finalPos)
				speed *= -1;
			else if (transform.position.x < initialPos)
				speed *= -1;
			
			transform.Translate (speed * transform.localScale.x, 0, 0);
		}
		
	}

	void OnCollisionStay2D (Collision2D other)
	{
		if (other.gameObject.tag == "Player") {
//			player.transform.SetParent (transform);
			if (Mathf.Sign(speed) == Mathf.Sign(player.transform.localScale.x) || rb.velocity.x == 0) {
				player.transform.Translate (speed * transform.localScale.x, 0, 0);
			}
		}
	}

	void OnCollisionExit2D (Collision2D other)
	{
		if (other.gameObject.tag == "Player") {
//			player.transform.SetParent (null);
		}
	}
}
