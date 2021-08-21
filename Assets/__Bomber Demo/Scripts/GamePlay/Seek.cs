using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seek : MonoBehaviour, ISteer
{
    
    [HideInInspector] public Transform target;
    [SerializeField]float speed = 5;



    public virtual Vector3 GetForce()
    {
        Vector3 velocity = Vector3.zero;
        Vector3 dir = (target.position - transform.position).normalized;

        transform.forward = dir;
        velocity = dir * speed;
        return velocity;
    }
}