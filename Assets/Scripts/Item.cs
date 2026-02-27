using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Item : ListenInputBase, IScene
{
    public Scenes Name {get;private set;} = Scenes.Items;
    public bool IsActiveRightNow {get;private set;} = false;
    public int[] Numbers = new int[4]{2,4,1,8};

    void FixedUpdate()
    {
        ListenInput();
        ListenNext();
    }

    public override void Accept()
    {
        Type("Скушано...");
    }
    public override void AcceptNext()
    {
        FunnyButtons.Instance.Menu();
        FunnyButtons.Instance.CanCancel = true;
        FunnyButtons.Instance.UpdateButtonAndHeart();
        Numbers[CurrentCell] -= 1;
        SoundManagerUi.Instance.PlaySound("healing");
    }
    public void InitializeScene()
    {
        gameObject.SetActive(true);
        InitializeCells(rawCellTexts, rawHearts, cellObjects);
        StartCoroutine(Delay());
        UpdateCellNumbers();
    }

    public void QuitScene()
    {
        isReady = false;
        if(gameObject.activeSelf)
            gameObject.SetActive(false);
    }
    void UpdateCellNumbers()
    {
        CellLines[0].CellList[0].Text.text = TextOfCells[0] + " x"+Numbers[0];
        CellLines[0].CellList[1].Text.text = TextOfCells[1] + " x"+Numbers[1];
        CellLines[1].CellList[0].Text.text = TextOfCells[2] + " x"+Numbers[2];
        CellLines[1].CellList[1].Text.text = TextOfCells[3] + " x"+Numbers[3];
    }
}
