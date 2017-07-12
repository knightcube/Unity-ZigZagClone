using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {

	public GameObject particle;
	public static BallController instance;
	[SerializeField]
	private float speed;
	public bool started;
	bool gameOver;

	Rigidbody rb ;
	void Awake(){

		if (instance == null)
			instance = this;
		
		rb = GetComponent<Rigidbody> ();

	}

	// Use this for initialization
	void Start () {

		started = false;
		gameOver = false;
	}
	
	// Update is called once per frame
	void Update () {

		if (!started) {

			if (Input.GetMouseButtonDown (0)) {
				
				rb.velocity = new Vector3 (speed, 0, 0);
				started = true;

				GameManager.instance.startGame();
			}

		}

		//ball is not colliding with any other game object
		if(!Physics.Raycast(transform.position,Vector3.down,4f)){
		
			gameOver = true;
			rb.velocity = new Vector3 (0, -25f, 0);
			Camera.main.GetComponent<CameraFollow> ().gameOver = true;
			GameManager.instance.GameOver();


		}

		if (Input.GetMouseButtonDown (0) && !gameOver)
			SwitchDirection ();

	}


	void SwitchDirection(){

		if (rb.velocity.z > 0)
			rb.velocity = new Vector3 (speed, 0, 0);
		else if (rb.velocity.x > 0)
			rb.velocity = new Vector3 (0, 0, speed);



	}

	void OnTriggerEnter(Collider col){

		if (col.gameObject.tag == "Diamond") {
		
			Destroy (col.gameObject);

			GameObject part = Instantiate (particle, col.gameObject.transform.position, Quaternion.identity);

			Destroy (col.gameObject);
			Destroy(part,2f);


		}


	}



}
