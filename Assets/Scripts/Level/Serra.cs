using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Serra : MonoBehaviour {

		private GameObject player;
		[SerializeField]
		private Rigidbody2D rb;
		private float speed;
		private float initialPos;
		private float finalPos;
		public float distancia;

		private bool active = false;

		void Awake () {

			speed = 0.1f;
			initialPos = transform.position.y;
			finalPos = transform.position.y + distancia;
			player = GameObject.FindGameObjectWithTag("Player");
			rb = player.GetComponent<Rigidbody2D> ();

		}
		
		void FixedUpdate ()
		{	
			if (transform.position.x - player.transform.position.x < 10f) active = true;

			if (active) {
				if (transform.position.y > finalPos)
					speed *= -1;
				else if (transform.position.y < initialPos)
					speed *= -1;
				
				transform.Translate (0, speed, 0);
			}
		}

}
