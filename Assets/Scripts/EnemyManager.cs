using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private static EnemyManager _instance;
    public static EnemyManager Instance => _instance;

    void Start()
    {
        Enemy.CurrentEnemy = Data.Instance.EnemyData.Get("Sharoku");
        
    }
}
