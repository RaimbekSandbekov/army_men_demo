using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

	public float health = 1f;

	public void LoseLife(){
		health -= 1f;
	}

	void Update(){
		if (health <= 0) {
			Destroy (gameObject);
		}
	}
}
