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
    public Enemy CurrentEnemy;

    public GameObject[] ScenesGameObjects = new GameObject[]{};
    
    void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        InitializeMenu();
    }

    void Update()
    {
        Menu();
        Attack();
        Act();
        Items();
        Mercy();
        Fight();
    }

    public void InitializeMenu()
    {
        ChangeScene(Scenes.Menu);
    }

    public void QuitMenu()
    {
        Answer.Instance.SwitchActive(false);
        Main.Instance.AllSpace.SetActive(false);
    }

    public void ChangeScene(Scenes scene)
    {
        CurrentScene = scene;
    }
    
    #region Scenes
    void Menu()
    {
        if(CurrentScene != Scenes.Menu)
            return;

        Answer.Instance.Type("Какой то текст, что бы заполнить пустоту в сердце!"); //temp
        ScenesGameObjects[0].SetActive(false);
    }

    void Attack()
    {
        if(CurrentScene != Scenes.Attack)
            return;
        ScenesGameObjects[0].SetActive(true);
        
    }

    void Act()
    {
        if(CurrentScene != Scenes.Act)
            return;
        
    }

    void Items()
    {
        if(CurrentScene != Scenes.Items)
            return;
        
    }

    void Mercy()
    {
        if(CurrentScene != Scenes.Mercy)
            return;
        
    }

    void Fight()
    {
        if(CurrentScene != Scenes.Fight)
            return;
        
    }
    #endregion

    public void ClearSpace()
    {
        Answer.Instance.SwitchActive(false);
    }
}

public enum Scenes
{
    Menu,
    Attack,
    Act,
    Items,
    Mercy,
    Fight
}