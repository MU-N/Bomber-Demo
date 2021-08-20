using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectPoolForTwoItems : MonoBehaviour
{
    public static ObjectPoolForTwoItems SharedInstance { get; private set; }

    [SerializeField] GameObject[] objectToPool;
    [SerializeField] int amountToPool;

    private Queue<GameObject> pooledObjects = new Queue<GameObject>();
    private Queue<GameObject> pooledEffectObjects = new Queue<GameObject>();

    GameObject tmp;

    void Awake()
    {
        SharedInstance = this;
    }

    void Start()
    {
        AddToPool(amountToPool, 0);
        AddToPool(amountToPool, 1);
    }

    public GameObject GetFromPool(int index)
    {
        if (pooledObjects.Count == 0)
        {
            AddToPool(1, index);

        }
        if (index == 0)
            return pooledObjects.Dequeue();
        else
            return pooledEffectObjects.Dequeue();

    }

    private void AddToPool(int count, int index)
    {
        for (int i = 0; i < count; i++)
        {
            tmp = Instantiate(objectToPool[index]);
            tmp.SetActive(false);
            if (index == 0)
                pooledObjects.Enqueue(tmp);
            else
                pooledEffectObjects.Enqueue(tmp);
        }
    }

    public void ReturnToPool(GameObject returnObject, int index)
    {
        returnObject.SetActive(false);
        if (index == 0)
            pooledObjects.Enqueue(returnObject);
        else
            pooledEffectObjects.Enqueue(returnObject);
       
    }
}
