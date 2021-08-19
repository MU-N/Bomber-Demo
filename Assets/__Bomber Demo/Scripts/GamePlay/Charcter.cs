using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Charcter : MonoBehaviour
{

    public abstract bool hasTheBomb { get; set; }
    public abstract bool isWalking { get; set; }
    public abstract bool isDead { get; set; }
    public abstract bool isWin { get; set; }
    public static bool canSwitch;

    Animator ainm;
    WaitForSeconds waitForSeconds = new WaitForSeconds(5f);
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
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Player"))
        {
            if (/*hasTheBomb &&*/ canSwitch && GetComponentInChildren<BombController>()!=null)
            {
                Debug.Log("Called");
                MoveBombToOther(collision);
                StartCoroutine(Fade(collision));
                
            }
        }

    }

    private void MoveBombToOther(Collision collision)
    {
        GameObject bomb = GetComponentInChildren<BombController>().gameObject;
        bomb.transform.parent = collision.gameObject.transform;
        bomb.transform.position = new Vector3(collision.transform.position.x, collision.transform.position.y + 1, collision.transform.position.z + 0.5f);
    }

    IEnumerator Fade(Collision col)
    {
        /*col.gameObject.GetComponent<Charcter>().hasTheBomb = true;
        gameObject.GetComponent<Charcter>().hasTheBomb = false;*/
        canSwitch = false;
        yield return waitForSeconds;
        canSwitch = true;

        

    }

}
