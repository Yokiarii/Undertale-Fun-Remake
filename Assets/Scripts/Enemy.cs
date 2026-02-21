using UnityEngine;

public class Enemy : MonoBehaviour
{
    public string Name {get; private set;} = "";
    public int[] HP {get; private set;} = new int[2]{60,60};
    public int Damage {get; private set;} = 1;
    
}

