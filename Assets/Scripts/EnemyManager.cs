using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

}
public interface IHaveDialog
{
    public Dialog DialogCell {get;}
}
public class BaseEnemy
{
    public string Name {get; private set;}
    public int Health {get; private set;}
    public BaseEnemy(string name, int health)
    {
        Name = name;
        Health = health;
    }
}

public class Dialog
{
    public Dictionary<int,string> Phrases {get;private set;}
    public void AddPhrase(string newPhrase)
    {
        Phrases.Add(Phrases.Count,newPhrase);
    }
}