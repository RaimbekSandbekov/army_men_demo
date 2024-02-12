using UnityEngine;
using System.Collections;

public class ControllableTarget : MonoBehaviour {
	float moveSpeed = 15f;
//	Vector2 obg;
//	Vector2 obh;
//	public GameObject Turret;

	void Start(){
		
	}

	void Update () {
//		if (Input.GetKeyDown (KeyCode.Q)) {
//			Vector2 obh = new Vector2 (transform.position.x, transform.position.y);
//			Vector2 obg = new Vector2 (Turret.transform.position.x, Turret.transform.position.y);
//			Vector2 piu = obg - obh;
//			float angref = Vector2.Angle (transform.up, piu);
//			print ("ANGLE (" + angref + ")");
//		}

		if (Input.GetKeyDown (KeyCode.T)) {
			if (gameObject.tag == "Untagged")
				gameObject.tag = "Enemy";
			else {
				gameObject.tag = "Untagged";
			}
		}

		if(Input.GetKey(KeyCode.W))
			transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

		if(Input.GetKey(KeyCode.S))
			transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);

		if(Input.GetKey(KeyCode.A))
			transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);

		if(Input.GetKey(KeyCode.D))
			transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
		
		if (Input.GetKey (KeyCode.LeftShift))
			moveSpeed = 30f;
		
		if (Input.GetKeyUp (KeyCode.LeftShift))
			moveSpeed = 15f;
	}
}
