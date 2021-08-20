using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectPool : MonoBehaviour
{
    public static ObjectPool SharedInstance { get; private set; }

    [SerializeField] GameObject objectToPool;
    [SerializeField] int amountToPool;

    private Queue<GameObject> pooledObjects = new Queue<GameObject>();

    GameObject tmp;

    void Awake()
    {
        SharedInstance = this;
    }

    void Start()
    {
        AddToPool(amountToPool);
    }

    public GameObject GetFromPool()
    {
        if (pooledObjects.Count == 0)
        {
            AddToPool(1);

        }

        return pooledObjects.Dequeue();

    }

    private void AddToPool(int count)
    {
        for (int i = 0; i < count; i++)
        {
            tmp = Instantiate(objectToPool);
            tmp.SetActive(false);

            pooledObjects.Enqueue(tmp);

        }
    }

    public void ReturnToPool(GameObject returnObject)
    {
        returnObject.SetActive(false);
        pooledObjects.Enqueue(returnObject);


    }
}
