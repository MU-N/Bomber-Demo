using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicEnmeyWithNavMesh : MonoBehaviour
{



    [SerializeField] GameData gameData;
    [SerializeField] LayerMask whatIsCharcter;
    [SerializeField] LayerMask whatIsGround;

    [SerializeField] float maxSpeed = 10;
    [SerializeField] float speed;
    [SerializeField] float steeringSensitivity = 10;
    [SerializeField] float radius = 5;
    [SerializeField] float wandarRange = 5;

    private bool hasTheBomb;
    private bool isWalking;
    private bool isDead;
    private bool isWin;
    private bool isInRnage;




    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;




    private Rigidbody rb;
    private Animator anim;
    private NavMeshAgent agent;


    WaitForSeconds waitForSeconds = new WaitForSeconds(2f);

    private Vector3 appliedVelocity;

    private float shortDistance = 1e5f + 7f;
    private Transform shortObject;
    private Transform hasBombObject;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();

    }

    void Update()
    {
        UpdateAnimation();

    }


    private void FixedUpdate()
    {
        agent.SetDestination(CheckForappliedVelocity());
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
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius, whatIsCharcter);
        if (GetComponentInChildren<BombController>() != null)
        {
            float dis = 0;
            for (int i = 0; i < hitColliders.Length; i++)
            {
                dis = Vector3.Distance(transform.position, hitColliders[i].transform.position);
                if (dis < shortDistance)
                {
                    shortDistance = dis;
                    shortObject = hitColliders[i].transform;
                }
            }
            if (dis != 0f)
                return SeekTarget(shortObject);
            else
                return WanderTarget();

        }
        else
        {
            if (hitColliders.Length == 0)
            {
                return WanderTarget();
            }
            else
            {
                for (int i = 0; i < hitColliders.Length; i++)
                {

                    if (hitColliders[i].GetComponentInChildren<BombController>() != null)
                    {

                        hasBombObject = hitColliders[i].transform;
                    }


                }
                if (hasBombObject != null)
                    return FleeTarget(hasBombObject);
                else
                    return WanderTarget();

            }
        }




    }

    private Vector3 WanderTarget()
    {
        //Debug.Log("Wander");
        if (!walkPointSet)
            walkPoint = GetRandomPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 2f)
            walkPointSet = false;

        return walkPoint;
    }

    private Vector3 FleeTarget(Transform targetTransform)
    {
        //Debug.Log("fleee");
        return targetTransform.position * -1;
    }

    private Vector3 SeekTarget(Transform targetTransform)
    {
        //Debug.Log("Seek");

        return targetTransform.position;
    }


    Vector3 GetRandomPoint()
    {

        walkPoint = Random.insideUnitSphere * wandarRange;

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;

        return walkPoint;
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


