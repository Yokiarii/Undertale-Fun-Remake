using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour
{
    private static SceneManager _instance;
    public static SceneManager Instance => _instance;

    public Scenes CurrentScene = Scenes.Menu;
    
    void Awake()
    {
        _instance = this;
    }

    
}

public enum Scenes
{
    Menu,
    Attack,
    Act,
    Items,
    Mercy
}