using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class LevelManger : MonoBehaviour
{
    [SerializeField] GameEvent winEvent;
    [SerializeField] GameEvent loseEvent;
    [SerializeField] GameData gameData;

    [SerializeField] float maxDis = 16;

    [SerializeField] TMP_Text uiExplodeTimeText;
    [SerializeField] TMP_Text uiEnemyText;

    GameObject[] charcters = new GameObject[61];

    GameObject bombPrefab, explodeEffect ,chacterHasBomb;

    int index, randomIndex;

    float countDown, enemyCount;


    WaitForSeconds wait = new WaitForSeconds(1);
    bool lose = false;
    void Start()
    {
        gameData.canSwitchTheBomb = true;
        for (int i = 0; i < gameData.amountOfEnemy; i++)
        {
            charcters[i] = ObjectPool.SharedInstance.GetFromPool();
            charcters[i].transform.position = new Vector3(Random.Range(maxDis, -maxDis), 0.5f, Random.Range(maxDis, -maxDis));
            charcters[i].SetActive(true);
            index = i + 1;
        }



        charcters[index] = FindObjectOfType<PlayerMoveControlles>().gameObject;
        uiEnemyText.text = gameData.amountOfEnemy.ToString();
        uiExplodeTimeText.text = gameData.timeForBomb.ToString();
        SpwanNewBomb(index);

        countDown = gameData.timeForBomb;
        enemyCount = gameData.amountOfEnemy;
    }



    void Update()
    {

        if (countDown > 0)
        {
            countDown -= Time.deltaTime;
            uiExplodeTimeText.text = countDown.ToString("0");
        }
        else
        {
            SpwnExpoldeEffect();
            CheckTherParenOfTheBomb(index);
            ObjectPoolForTwoItems.SharedInstance.ReturnToPool(bombPrefab, 0);
            SwapWithLastItem();
            SpwanNewBomb(--index);
            countDown = gameData.timeForBomb;
            
        }
        if (index == 0 )
        {
            for (int i = 0; i < charcters.Length-1; i++)
            {
                if(charcters[i].CompareTag("Player")&& charcters[index].GetComponentInChildren<BombController>() != null)
                {
                    lose = true;
                }
            }
            if ( lose)
                loseEvent.Raise();
            else
                winEvent.Raise();
        }
    }

    private void CheckTherParenOfTheBomb(int ind)
    {
        for (int i = 0; i < ind; i++)
        {
            if (charcters[i].GetComponentInChildren<BombController>() != null)
            {
                randomIndex = i;
               // chacterHasBomb = charcters[i];
            }
        }
    }

    private void SwapWithLastItem()
    {
        var temp = charcters[randomIndex];
        charcters[randomIndex] = charcters[index];
        charcters[index] = temp;
        ObjectPool.SharedInstance.ReturnToPool(charcters[index]);
        enemyCount--;
        uiEnemyText.text = enemyCount.ToString();
        
    }

    private void SpwnExpoldeEffect()
    {
        explodeEffect = ObjectPoolForTwoItems.SharedInstance.GetFromPool(1);
        explodeEffect.transform.position = bombPrefab.transform.position;
        explodeEffect.SetActive(true);
        explodeEffect.GetComponent<ParticleSystem>().Play();

        StartCoroutine(WaitForOneSec(bombPrefab.transform.parent.gameObject)) ;
        
    }

    private void SpwanNewBomb(int ind)
    {
        randomIndex = Random.Range(0, ind);
        bombPrefab = ObjectPoolForTwoItems.SharedInstance.GetFromPool(0);
        bombPrefab.SetActive(true);
        bombPrefab.transform.parent = charcters[randomIndex].transform;
        bombPrefab.transform.position = new Vector3(charcters[randomIndex].transform.position.x, charcters[randomIndex].transform.position.y + 1, charcters[randomIndex].transform.position.z + 0.5f);
        
    }

    IEnumerator WaitForOneSec(GameObject bombHolder)
    {

        yield return wait;
        ObjectPoolForTwoItems.SharedInstance.ReturnToPool(explodeEffect, 1);
        if (bombHolder.GetComponent<PlayerMoveControlles>() != null)
            loseEvent.Raise();

    }
}
