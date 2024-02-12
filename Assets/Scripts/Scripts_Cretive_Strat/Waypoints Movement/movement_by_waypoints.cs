using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement_by_waypoints : MonoBehaviour
{
    Transform targetWaypoint;
    public float speed = 3f;
    int wavepointIndex = 0;

    void Start()
    {
        targetWaypoint = waypoints.points[0];
    }

    void Update()
    {
        Vector3 dir1 = targetWaypoint.position - transform.position;
        transform.Translate(dir1.normalized * speed * Time.deltaTime, Space.World);

        Quaternion targetRotation = Quaternion.LookRotation(dir1);
        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, targetRotation, Time.deltaTime * 10);

        if (Vector3.Distance(transform.position, targetWaypoint.position) <= 0.2f)
        {
            GetNextWaypoint();
        }
    }

    void GetNextWaypoint()
    {
        if (wavepointIndex >= waypoints.points.Length)
        {
            //Destroy(gameObject);
            targetWaypoint = waypoints.points[0];
            wavepointIndex = 0;
            return;
        }
        else
        {
            wavepointIndex++;
            targetWaypoint = waypoints.points[wavepointIndex];
        }
    }
}
