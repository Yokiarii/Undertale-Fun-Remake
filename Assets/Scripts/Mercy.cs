using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Mercy : ListenInputBase, IScene
{
    private static Mercy _instance;
    public static Mercy Instance => _instance;
    public Scenes Name {get;private set;} = Scenes.Mercy;

    public GameObject END;

    public bool IsActiveRightNow {get;private set;} = false;

    void Awake()
    {

        _instance = this;
    }

    void OnEnable()
    {
        if (SharokuScript.Instance.READY_TO_MERCY.IsReady)
           rawCellTexts[0].color = Color.yellow;
    }

    void FixedUpdate()
    {
        ListenInput();
    }

    public override void Accept()
    {
        if (SharokuScript.Instance.READY_TO_MERCY.IsReady && CurrentCell == 0)
        {
            FunnyButtons.Instance.TurnOffButtons();
            FunnyButtons.Instance.IsActive = false;
            FunnyButtons.Instance.CanCancel = false;

            isListening = false;
            isAccepting = false;
            var temp = Instantiate(END,Main.Instance.MainCanvas.transform);
            temp.transform.SetAsLastSibling();

            Debug.Log("ИГРА ОКОНЧЕНА");
            return;
        }
        Answer.Instance.EnterAnswer(TextOfCells[CurrentCell], "typing");
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
