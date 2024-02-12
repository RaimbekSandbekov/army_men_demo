using UnityEngine;
using System.Collections;

public class Shell : MonoBehaviour {

	Transform target;
	public float speed = 1f;
	public float damage = 1f;

	public void Seek(Transform _target){
		target = _target; }

	void Update () {
		if (target == null) {
			Destroy (gameObject);
			return; }
		
		Vector3 dir = target.position - transform.position;
		float distanceThisFrame = speed * Time.deltaTime;

		if (dir.magnitude <= distanceThisFrame) {
			HitTarget ();
			return; }
		
		transform.Translate (dir.normalized * distanceThisFrame, Space.World);
	}

	void HitTarget(){
		target.GetComponent<Health> ().LoseLife ();
		Destroy (gameObject); 
	}
}
