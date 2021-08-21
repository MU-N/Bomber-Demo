using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flee : MonoBehaviour, ISteer
{
    
    [HideInInspector] public Transform target;
    [SerializeField] float speed = 5;
    [SerializeField] float inRange = 5;

    Vector3 velocity;


    public Vector3 GetForce()
    {
        Vector3 ds = transform.position - target.position;
        Vector3 dir = ds.normalized;
        transform.forward = dir;
        if (ds.magnitude < inRange)
        {
            velocity = dir * speed;
        }
        else
        {
            velocity *= Time.fixedDeltaTime * 10;
        }

        return velocity;
    }
}