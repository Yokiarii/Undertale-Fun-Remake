using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Mercy : ListenInputBase, IScene
{
    private static Mercy _instance;
    public static Mercy Instance => _instance;
    public Scenes Name {get;private set;} = Scenes.Mercy;

    public bool IsActiveRightNow {get;private set;} = false;

    void Awake()
    {
        _instance = this;
    }

    void FixedUpdate()
    {
        ListenInput();
    }

    public override void Accept()
    {
        Answer.Instance.EnterAnswer("пощадить");
        Answer.Instance.Type("Вы пытаетесь пощадить.");
    }

    public void InitializeScene()
    {
        gameObject.SetActive(true);
        InitializeCells(rawCellTexts, rawHearts, cellObjects);
        StartCoroutine(Delay());
    }

    public void QuitScene()
    {
        isReady = false;
        if(gameObject.activeSelf)
            gameObject.SetActive(false);
    }
}
