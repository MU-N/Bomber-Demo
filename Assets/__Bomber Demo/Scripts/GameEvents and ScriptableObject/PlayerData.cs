using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Game Data", menuName = "Game Data/PlayerData", order = 1)]
public class PlayerData : ScriptableObject
{
    public float moveSpeed;
    public float rotaionSpeed;
}
