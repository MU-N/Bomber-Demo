using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class LevelManger : MonoBehaviour
{
    [SerializeField] GameData gameData;
    [SerializeField] TMP_Text uiExplodeTimeText;
    [SerializeField] TMP_Text uiEnemyText;
    void Start()
    {
        gameData.canSwitchTheBomb = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
