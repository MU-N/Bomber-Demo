using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManger : MonoBehaviour
{
    [SerializeField] GameData gameData;
    // Start is called before the first frame update
    void Start()
    {
        gameData.canSwitchTheBomb = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
