using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower2 : MonoBehaviour
{
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private float[] speeds;

    private int currentWaypointIndex = 0;

    private void Update()
    {
        if (Vector2.Distance(waypoints[currentWaypointIndex].position, transform.position) < .1f)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0;
            }
        }

        float currentSpeed = speeds[currentWaypointIndex];
        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex].position, Time.deltaTime * currentSpeed);
    }
}
