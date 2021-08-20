using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] GameData gameData;



    private bool hasTheBomb;
    private bool isWalking;
    private bool isDead;
    private bool isWin;

    Rigidbody rb;
    Animator anim;


    WaitForSeconds waitForSeconds = new WaitForSeconds(3f);





    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAnimation();
    }

    private  void UpdateAnimation()
    {
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

    IEnumerator WaitSomeSec()
    {
        yield return waitForSeconds;
        gameData.canSwitchTheBomb = true;
    }
}
