using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat_V_01 : MonoBehaviour
{
	private Transform target;
	float range = 15f;
	float fireRate = 1f;
	float fireCountdown = 0f;
	public string enemyTag = "Enemy";
	public Transform partToRotate;
	float turnSpeed = 10f;
	//public GameObject bulletPrefab;
	//public Transform firePoint;

	void Start()
	{
		InvokeRepeating("UpdateTarget", 0f, 0.5f);
	}

	void UpdateTarget()
	{
		GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
		float shortestDistance = Mathf.Infinity;
		GameObject nearestEnemy = null;
		foreach (GameObject enemy in enemies)
		{
			float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
			if (distanceToEnemy < shortestDistance)
			{
				shortestDistance = distanceToEnemy;
				nearestEnemy = enemy;
			}
		}
		if (nearestEnemy != null && shortestDistance <= range)
		{
			target = nearestEnemy.transform;
		}
		else
		{
			target = null;
		}
	}

	void Update()
	{
		if (target == null)
		{
			return;
		}
		Vector3 dir = target.position - transform.position;
		Quaternion lookRotation = Quaternion.LookRotation(dir);
		Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
		partToRotate.rotation = Quaternion.Euler(0, rotation.y, 0);
		if (fireCountdown <= 0f)
		{
			Shoot();
			fireCountdown = 1f / fireRate;
			//print("Shot!");
		}
		fireCountdown -= Time.deltaTime;
	}

	void Shoot()
	{
	}
}
