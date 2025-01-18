using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SimpleMovementEntity : MonoBehaviour
{
    [SerializeField] private List<Vector3> waypoints = new List<Vector3>();

    [SerializeField] private float speed = 5f;
    [SerializeField] private bool loop = true;

    public int waypointCount = 0;
    private Rigidbody rb;

    private void Awake()
    {
        waypointCount = 0;
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (loop)
        {
            LogicTravelWaypoint();
        }
        else
        {
            if(waypointCount <= waypoints.Count - 1)
            {
                LogicTravelWaypoint();
            }
            else
            {
                rb.velocity = Vector3.zero;
            }
        }
    }

    private void LogicTravelWaypoint()
    {
        if (waypoints.Count > 1)
        {
            for (int i = 0; i < waypoints.Count; i++)
            {
                if (i == waypointCount)
                {
                    MoveToWaypoint(waypoints[i]);
                }
            }
        }
    }

    private void MoveToWaypoint(Vector3 waypoint)
    {
        if(Vector3.Distance(transform.position, waypoint) > 0.2f)
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/Weapon/ShootV1");
            Vector3 direction = (waypoint - transform.position).normalized;

            rb.velocity = direction * speed;
            
        }
        else
        {
            if (loop && waypointCount >= waypoints.Count - 1)
            {
                waypointCount = 0;
                return;
            }

            if(waypointCount != waypoints.Count)
            {
                waypointCount++;
            }

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if(waypoints.Count > 1)
        {
            for (int i = 0; i < waypoints.Count; i++)
            {
                if(i + 1 < waypoints.Count)
                {
                    Gizmos.DrawLine(waypoints[i], waypoints[i + 1]);
                }               
            }            
        }
    }
}
