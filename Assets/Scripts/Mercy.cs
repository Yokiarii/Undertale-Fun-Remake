using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Mercy : ListenInputBase, IScene
{
    public Scenes Name {get;private set;} = Scenes.Mercy;

    public bool IsActiveRightNow {get;private set;} = false;

    void FixedUpdate()
    {
        ListenInput();
    }

    public override void Accept()
    {
        throw new System.NotImplementedException();
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
