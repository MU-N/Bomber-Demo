using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringBase : MonoBehaviour
{
    [SerializeField]
    float maxSpeed = 10;
    [SerializeField]
    float speed;
    [SerializeField]
    float steeringSensitivity = 10;

    private Rigidbody rb;
    private IEnumerable<ISteer> steerBehaviors;
    //private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //animator = GetComponentInChildren<Animator>();
        steerBehaviors = GetComponents<ISteer>();
    }

    private void FixedUpdate()
    {
        Vector3 appliedVelocity = Vector3.zero;
        foreach (ISteer steerBehavior in steerBehaviors)
        {
            appliedVelocity += steerBehavior.GetForce();
        }
        appliedVelocity.y = 0;
        Vector3 velocity = transform.forward * appliedVelocity.magnitude;
        speed = velocity.magnitude;

        if (appliedVelocity.magnitude > 0)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(appliedVelocity), Time.fixedDeltaTime * steeringSensitivity);

        if (velocity.magnitude > maxSpeed)
        {
            velocity = velocity.normalized * maxSpeed;
        }

        rb.velocity = velocity - rb.velocity;
       // animator.SetFloat("Speed", velocity.magnitude * 2);
    }
}
