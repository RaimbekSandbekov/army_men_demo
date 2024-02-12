using UnityEngine;
using System.Collections;

public class TankDemo : MonoBehaviour {
	public float speed = 10f;
	public float angleOfFire = 5f;
	public float range = 20f;
	public float fireRate = 1f;
	float fireCountdown = 0f;
	public GameObject shellPrefab;
	public GameObject TURRET;
	public Transform firePoint;

	Transform targetWaypoint;
	int wavepointIndex = 0;

	public Transform partToRotate;
	public float turretTurnSpeed = 12f;
	Transform target;

	void Start(){
		InvokeRepeating ("UpdateTarget", 0f, 0.5f);// starts after 0 sec, interval 0,5s
		targetWaypoint = Waypoints.points [0];
	}

	void Update(){
		Vector3 dir1 = targetWaypoint.position - transform.position;
		transform.Translate (dir1.normalized * speed * Time.deltaTime, Space.World);

		Quaternion targetRotation = Quaternion.LookRotation( dir1 );
		this.transform.rotation = Quaternion.Lerp(this.transform.rotation, targetRotation, Time.deltaTime * 10);

		if (Vector3.Distance (transform.position, targetWaypoint.position) <= 0.7f) {
			GetNextWaypoint (); }



		if (target == null) {
			return;
		} 
		Turn ();

		Vector3 piu = new Vector3 (target.transform.position.x - TURRET.transform.position.x, 0, target.transform.position.z - TURRET.transform.position.z);
		float angref = Vector3.Angle (piu,TURRET.transform.forward);

		if (fireCountdown <= 0f) {
			if (angref <= angleOfFire) {
				Shoot ();
				fireCountdown = 1f / fireRate;
			}
		}

		fireCountdown -= Time.deltaTime;



	}

	void UpdateTarget(){
		GameObject[] enemies = GameObject.FindGameObjectsWithTag ("Tower");

		float shortestDistance = Mathf.Infinity;
		GameObject nearestEnemy = null;

		foreach (GameObject enemy in enemies) {
			float distanceToEnemy = Vector3.Distance (transform.position, enemy.transform.position);
			if (distanceToEnemy < shortestDistance) {
				shortestDistance = distanceToEnemy;
				nearestEnemy = enemy;
			}
		}

		if (nearestEnemy != null && shortestDistance <= range) {
			target = nearestEnemy.transform;
		} 
		else {
			target = null;
		}

	}

	void Turn(){
		Vector3 dir = target.position - transform.position;
		Quaternion lookRotation = Quaternion.LookRotation (dir);
		Vector3 rotation = Quaternion.RotateTowards (partToRotate.rotation, lookRotation, Time.deltaTime * turretTurnSpeed).eulerAngles;
		partToRotate.rotation = Quaternion.Euler (0f, rotation.y, 0f);
	}

	void Shoot(){
		GameObject ShellGO = (GameObject)Instantiate (shellPrefab, firePoint.position, firePoint.rotation);
		Shell shell = ShellGO.GetComponent<Shell> ();

		if (shell != null) {
			shell.Seek (target); }
	}

	void GetNextWaypoint(){
		if (wavepointIndex >= Waypoints.points.Length - 1) {
			Destroy (gameObject);
			return; }

		wavepointIndex++;
		targetWaypoint = Waypoints.points [wavepointIndex];
	}



}
