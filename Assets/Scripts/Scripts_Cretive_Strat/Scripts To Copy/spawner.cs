using UnityEngine;
using System.Collections;

public class spawner : MonoBehaviour {

	public GameObject tanks;
	float time = 0.4f;

	void Update () {
		if (time <= 0) {
			Instantiate (tanks, transform.position, transform.rotation);
			time = 0.2f;
		}
		time -= Time.deltaTime;
	}
}
