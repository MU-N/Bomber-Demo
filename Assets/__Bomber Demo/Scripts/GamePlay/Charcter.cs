using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Charcter : MonoBehaviour
{

    public bool hasTheBomb;
    public bool isWalking;
    public bool isDead;
    public bool isWin;

    Animator ainm;
    public abstract void Move();

    public void UpdateAnimation()
    {
        ainm.SetBool("isWalking", isWalking);
        ainm.SetBool("hasBomb", hasTheBomb);
        ainm.SetBool("isWin", isWin);
        ainm.SetBool("isDead", isDead);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Enemy"))
        {
            
            if (hasTheBomb)
            {
                Debug.Log(collision.gameObject.tag);
                GameObject bomb = GetComponentInChildren<BombController>().gameObject;
                bomb.transform.parent = collision.gameObject.transform;
                bomb.transform.position = new Vector3(collision.transform.position.x, collision.transform.position.y + 1, collision.transform.position.z + 0.5f);
                
                /*if (collision.gameObject.GetComponent<Enemy>() != null)
                    collision.gameObject.GetComponent<Enemy>().hasTheBomb = true;
                else if (collision.gameObject.GetComponent<PlayerController>() != null)
                    collision.gameObject.GetComponent<PlayerMoveControlles>().hasTheBomb = true;*/

                hasTheBomb = false;
            }
        }
    }
}
