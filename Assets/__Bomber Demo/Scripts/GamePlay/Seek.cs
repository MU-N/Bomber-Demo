using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seek : MonoBehaviour, ISteer
{
    
    [SerializeField]float speed = 5;



    public virtual Vector3 GetForce(Transform targ)
    {
        Vector3 velocity = Vector3.zero;
        Vector3 dir = (targ.position - transform.position).normalized;

        transform.forward = dir;
        velocity = dir * speed;
        return velocity;
    }
}