using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class LevelManger : MonoBehaviour
{
    [SerializeField] GameData gameData;

    [SerializeField] float maxDis = 16;

    [SerializeField] TMP_Text uiExplodeTimeText;
    [SerializeField] TMP_Text uiEnemyText;

    GameObject[] charcters = new GameObject[61];

    GameObject bombPrefab, explodeEffect;

    int index, randomIndex;

    float countDown, enemyCount;


    WaitForSeconds wait = new WaitForSeconds(1);

    void Start()
    {
        gameData.canSwitchTheBomb = true;
        bombPrefab = ObjectPoolForTwoItems.SharedInstance.GetFromPool(0);
        bombPrefab.SetActive(true);
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
            ObjectPoolForTwoItems.SharedInstance.ReturnToPool(bombPrefab, 0);
            CheckTherParenOfTheBomb(index);
            SwapWithLastItem();
            SpwanNewBomb(--index);
            countDown = gameData.timeForBomb;
        }
        if (index == 0)
        {
            //ToDo WinSate;
            Debug.Log("Win");
        }
    }

    private void CheckTherParenOfTheBomb(int ind)
    {
        for (int i = 0; i < ind; i++)
        {
            if (charcters[i].GetComponentInChildren<BombController>() != null)
            {
                randomIndex = i;
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
        ObjectPool.SharedInstance.ReturnToPool(bombPrefab.transform.parent.gameObject);
        StartCoroutine(WaitForOneSec()) ;
    }

    private void SpwanNewBomb(int ind)
    {
        randomIndex = Random.Range(0, ind);
        //bombPrefab = ObjectPoolForTwoItems.SharedInstance.GetFromPool(0);
        bombPrefab.transform.parent = charcters[randomIndex].transform;
        bombPrefab.transform.position = new Vector3(charcters[randomIndex].transform.position.x, charcters[randomIndex].transform.position.y + 1, charcters[randomIndex].transform.position.z + 0.5f);
        bombPrefab.SetActive(true);
    }

    IEnumerator WaitForOneSec()
    {

        yield return wait;
        ObjectPoolForTwoItems.SharedInstance.ReturnToPool(explodeEffect, 1);

    }
}
