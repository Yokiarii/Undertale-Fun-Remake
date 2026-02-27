using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class AttackScene : ListenInputBase, IScene
{
    private static AttackScene _instance;
    public static AttackScene Instance => _instance;
    public GameObject PlayerAttackGameObject;
    public Scenes Name {get;private set;} = Scenes.Attack;
    public bool IsActiveRightNow {get;private set;} = false;

    void Awake()
    {
        _instance = this;
    }
    void FixedUpdate()
    {
        ListenInput();
    }
    public void InitializeScene()
    {
        gameObject.SetActive(true);
        InitializeCells(rawCellTexts, rawHearts, cellObjects);
        StartCoroutine(Delay());
    }

    public override void Accept()
    {
        SceneManager.Instance.ChangeScene(Scenes.Fight);
        PlayerAttackGameObject.SetActive(true);
    }
    public void QuitScene()
    {
        isReady = false;
        if(gameObject.activeSelf)
            gameObject.SetActive(false);
    }
}
