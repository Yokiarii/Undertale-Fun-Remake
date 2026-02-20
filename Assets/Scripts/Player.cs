using DG.Tweening;
using UnityEngine;

public class Player : MonoBehaviour
{
    public string Name {get;private set;}
    public int[] HP {get;private set;} = new int[2]{20,20};

    private static Player _instance;
    public static Player Instance => _instance;
    public GameObject PlayerGameObject;
    void Awake()
    {
        _instance = this;
    }

    public void ChangeName(string name) => Name = name;
    public void ChangeHP(int vector)
    {
        var temp = HP[0];
        HP[0] += vector;
        if(HP[0] < 0)
            HP[0] = 0;
        if(HP[0] > HP[1])
            HP[0] = HP[1];
        if(temp > HP[0])
        {
            PlayerGameObject.transform.DOShakePosition(2);
        }
    }
}
