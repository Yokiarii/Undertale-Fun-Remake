using UnityEngine;

public class Act : ListenInputBase, IScene
{
    public Scenes Name {get;private set;} = Scenes.Act;

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
    public void UpdateCellText()
    {
        
    }
    public void QuitScene()
    {
        isReady = false;
        if(gameObject.activeSelf)
            gameObject.SetActive(false);
    }
}
