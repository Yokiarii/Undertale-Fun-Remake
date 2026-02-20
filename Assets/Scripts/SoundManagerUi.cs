using System;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerUi : MonoBehaviour
{
    private static SoundManagerUi _instance;
    public static SoundManagerUi Instance => _instance;

    Dictionary<string,AudioClip> AllSounds = new();
    [SerializeField] private AudioSource Source; 

    void Awake()
    {
        _instance = this;
        Init();
    }

    void Init()
    {
        var obj = Resources.LoadAll<AudioClip>("Sounds/Fx");
        foreach (var item in obj)
        {
            AllSounds.Add(item.name,item);
        }
        foreach (var item in AllSounds)
        {
            Debug.Log(item.Key);
        }
    }
    public void PlaySound(string name)
    {
        foreach (var item in AllSounds)
        {
            if(item.Key == name)
                Source.PlayOneShot(item.Value);
        }
    }
}
