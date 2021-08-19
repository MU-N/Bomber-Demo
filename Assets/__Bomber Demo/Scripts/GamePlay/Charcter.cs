using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Charcter : MonoBehaviour
{

    public abstract bool hasTheBomb { get; set; }
    public abstract bool isWalking { get; set; }
    public abstract bool isDead { get; set; }
    public abstract bool isWin { get; set; }

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
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (hasTheBomb)
            {
                Debug.Log("Called");
                MoveBombToOther(collision);
                collision.gameObject.GetComponent<Charcter>().hasTheBomb = true;
                gameObject.GetComponent<Charcter>().hasTheBomb = false;
            }
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            if (hasTheBomb)
            {
                Debug.Log("Called player");
                MoveBombToOther(collision);
                collision.gameObject.GetComponent<Charcter>().hasTheBomb = true;
                gameObject.GetComponent<Charcter>().hasTheBomb = false;
            }
        }
    }

    private void MoveBombToOther(Collision collision)
    {
        GameObject bomb = GetComponentInChildren<BombController>().gameObject;
        bomb.transform.parent = collision.gameObject.transform;
        bomb.transform.position = new Vector3(collision.transform.position.x, collision.transform.position.y + 1, collision.transform.position.z + 0.5f);
    }
}
