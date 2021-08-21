using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] GameData gameData;
    [SerializeField] LayerMask whatIsCharcter;

    [SerializeField] float maxSpeed = 10;
    [SerializeField] float speed;
    [SerializeField] float steeringSensitivity = 10;
    [SerializeField] float radius = 5;

    private bool hasTheBomb;
    private bool isWalking;
    private bool isDead;
    private bool isWin;
    private bool isInRnage;

    private Rigidbody rb;
    private Animator anim;

    private Seek seek;
    private Wander wander;
    private Flee flee;



    WaitForSeconds waitForSeconds = new WaitForSeconds(2f);

    private Vector3 appliedVelocity;

    private float shortDistance = Mathf.Infinity;
    private Transform shortObject;
    private Transform hasBombObject;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        seek = GetComponent<Seek>();
        wander = GetComponent<Wander>();
        flee = GetComponent<Flee>();

    }

    void Update()
    {
        UpdateAnimation();

    }


    private void FixedUpdate()
    {
        appliedVelocity = Vector3.zero;

        appliedVelocity += CheckForappliedVelocity();

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
    }

    private void UpdateAnimation()
    {
        isWalking = true;
        anim.SetBool("isWalking", isWalking);
        anim.SetBool("hasBomb", hasTheBomb);
        anim.SetBool("isWin", isWin);
        anim.SetBool("isDead", isDead);
    }



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Player"))
        {
            if (gameData.canSwitchTheBomb && GetComponentInChildren<BombController>() != null)
            {
                MoveBombToOther(collision);
                StartCoroutine(WaitSomeSec());
                gameData.canSwitchTheBomb = false;

            }
        }

    }

    private void MoveBombToOther(Collision collision)
    {
        GameObject bomb = GetComponentInChildren<BombController>().gameObject;
        bomb.transform.parent = collision.gameObject.transform;
        bomb.transform.position = new Vector3(collision.transform.position.x, collision.transform.position.y + 1, collision.transform.position.z + 0.5f);
    }







    private Vector3 CheckForappliedVelocity()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius , whatIsCharcter);

        if (GetComponentInChildren<BombController>() != null)
        {
            for (int i = 0; i < hitColliders.Length; i++)
            {
                float dis = Vector3.Distance(transform.position, hitColliders[i].transform.position);
                if (dis < shortDistance)
                {
                    shortDistance = dis;
                    shortObject = hitColliders[i].transform;
                }
            }
            return SeekTarget(shortObject);

        }
        else
        {
            if(hitColliders.Length ==0)
            {
                return WanderTarget();
            }
            else
            {
                for (int i = 0; i < hitColliders.Length; i++)
                {
                    
                    if (hitColliders[i].GetComponentInChildren<BombController>()!=null)
                    {
                        
                        hasBombObject = hitColliders[i].transform;
                    }
                }
                return FleeTarget(hasBombObject);
            }
        }




    }

    private Vector3 WanderTarget()
    {
        Debug.Log("Wander");
        return wander.GetForce();
    }

    private Vector3 FleeTarget(Transform targetTransform)
    {
        flee.target = targetTransform;
        Debug.Log("fleee");
        return flee.GetForce();
    }

    private Vector3 SeekTarget(Transform targetTransform)
    {
        seek.target = targetTransform;
        Debug.Log("Seek");
        return seek.GetForce();
    }







    IEnumerator WaitSomeSec()
    {
        yield return waitForSeconds;
        gameData.canSwitchTheBomb = true;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

}
