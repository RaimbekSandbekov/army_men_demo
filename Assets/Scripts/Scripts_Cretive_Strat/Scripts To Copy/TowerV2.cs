using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerV2 : MonoBehaviour {

	Transform target;

	[Header("Changeable Vars")]
	public float angleOfFire = 5f;
	public float range = 20f;
	public float fireRate = 1f; //shots per seconds
	float fireCountdown = 0f;

	[Header("Unity Setup Fields")]

	public string enemyTag = "Enemy";
	public Transform partToRotate;
	public float turretTurnSpeed = 12f;

	public GameObject shellPrefab;
	public Transform firePoint;

	public GameObject TURRET;

	void Start () {
		InvokeRepeating ("UpdateTarget", 0f, 0.2f);// starts after 0 sec, interval 0,3s
	}

	void Update () {
		if (target == null) {
			return;
		}
		if (target != null) {

			Turn ();

			Vector3 piu = new Vector3 (target.transform.position.x - TURRET.transform.position.x, 0, target.transform.position.z - TURRET.transform.position.z);
			float angref = Vector3.Angle (piu, TURRET.transform.forward);

			if (fireCountdown <= 0f) {
				if (angref <= angleOfFire) {
					Shoot ();
					fireCountdown = 1f / fireRate;
				}
			}
		}
		fireCountdown -= Time.deltaTime;
	}

	void UpdateTarget(){
		GameObject[] enemies = GameObject.FindGameObjectsWithTag (enemyTag);

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

}

/*
	void OnDrawGizmosSelected(){ //Callback func, draws sphere to show range of tower
		Gizmos.color = Color.cyan;
		Gizmos.DrawWireSphere (transform.position, range);
	}
*/
