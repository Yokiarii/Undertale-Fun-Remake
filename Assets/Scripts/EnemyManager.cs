using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private static EnemyManager _instance;
    public static EnemyManager Instance => _instance;
    void Awake()
    {
        _instance = this;
    }
    void Start()
    {
        Enemy.CurrentEnemy = Data.Instance.EnemyData.Get("Sharoku");
        //Answer.Instance.Type(Enemy.CurrentEnemy.StateRelation[Enemy.CurrentEnemy.CurrentRelation].BaseAnswer);
    }
}
