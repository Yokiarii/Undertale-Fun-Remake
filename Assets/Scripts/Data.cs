using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class Data : MonoBehaviour
{
    private static Data _instance;
    public static Data Instance => _instance;

    public EnemyDataBase EnemyData = new();
    public DataManagerBase DataManager;

    void Awake()
    {
        _instance = this;
        DataManager = new DataManagerBase(this);
    }
}
public interface IData
{
    public void Clear();
}
public class EnemyDataBase : IData
{
    private Dictionary<string, EnemyBase> EnemyList = new();
    public void Add(EnemyBase enemy) => EnemyList.Add(enemy.Name,enemy);
    public EnemyBase Get(string name) => EnemyList[name];
    public void Remove(string name) => EnemyList.Remove(name);
    public void Remove(EnemyBase enemy) => EnemyList.Remove(enemy.Name);
    public void Clear() => EnemyList.Clear(); 
}
public class DataManagerBase
{
    private Data _data;
    
    public DataManagerBase(Data data)
    {
        _data = data;
        ReadEnemyDataFromJson();
    }

    public void ReadEnemyDataFromJson()
    {
        _data.EnemyData.Clear();
        _data.EnemyData.Add(JsonResourceReader.Read<EnemyBase>("Data/Enemies/Sharoku/Sharoku"));
    }
}

public class JsonResourceReader
{
    public static T Read<T>(string path)
    {
        var temp = Resources.Load<TextAsset>(path);
        if (!Check(path))
        {
           throw new System.Exception($"Указанный файл {path} не существует в папке ресурсов."); 
        }
        return JsonConvert.DeserializeObject<T>(temp.text);
    }
    public static bool Check(string path)
    {
        return Resources.Load(path) != null;
    }
} 