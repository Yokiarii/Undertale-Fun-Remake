using System;
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
    public Dictionary<string,IScene> AllScenes = new Dictionary<string, IScene>();
    public Enemy CurrentEnemy;

    public GameObject[] ScenesGameObjects = new GameObject[] { };

    void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        AllScenes.Add("Attack",ScenesGameObjects[0].GetComponent<AttackScene>());
        AllScenes.Add("Act",ScenesGameObjects[1].GetComponent<Act>());
        AllScenes.Add("Items",ScenesGameObjects[2].GetComponent<Item>());
        AllScenes.Add("Mercy",ScenesGameObjects[3].GetComponent<Mercy>());

        Answer.Instance.Type("Какой-то текст, что бы заполнить пустоту в сердце!!!");
    }

    public void QuitMenu()
    {
        Answer.Instance.SwitchActive(false);
        Main.Instance.AllSpace.SetActive(false);
    }

    public void ChangeScene(Scenes scene)
    {
        CurrentScene = scene;
        foreach (var item in AllScenes)
        {
            item.Value.QuitScene();
            if(item.Value.Name == CurrentScene)
            {
                item.Value.InitializeScene();
            }
        }
    }

}

public interface IScene
{
    public Scenes Name { get; }
    public bool IsActiveRightNow { get; }
    public void InitializeScene();
    public void QuitScene();
}

public abstract class ListenInputBase : MonoBehaviour
{
    // Поля для хранения состояния 
    protected bool isListening = true;
    protected bool isChanging;
    protected bool isReady;
    protected bool isAccepting;
    protected bool isSilent;
    protected bool next;
    [SerializeField] protected int currentCell;
    [SerializeField] protected string[] textOfCells;
    protected List<CellLine> cellLines = new List<CellLine>();

    //реализовать в наследнике.
    [SerializeField] public GameObject[] cellObjects;
    [SerializeField] public TextMeshProUGUI[] rawCellTexts;
    [SerializeField] public GameObject[] rawHearts;

    // Свойства 
    public virtual bool IsListening => isListening;
    public virtual bool IsChanging => isChanging;
    public virtual bool IsAccepting => isAccepting;
    public virtual bool IsReady => isReady;
    public virtual bool IsSilent => isSilent;
    public virtual int CurrentCell => currentCell;
    public virtual bool Next => next;
    public virtual string[] TextOfCells => textOfCells;
    public virtual List<CellLine> CellLines => cellLines;
    public virtual void SetListening(bool value) => isListening = value;
    public virtual void ChangeCellStatement(int i)
    {
        isChanging = true;
        currentCell += i;
        var temp = cellLines[0].CellList.Count + CellLines[1].CellList.Count;
        if (currentCell > temp - 1)
        {
            currentCell = 0;
        }
        if (currentCell < 0)
        {
            currentCell = temp - 1;
        }
        UpdateHeartStatement();
        if(!isSilent)
            SoundManagerUi.Instance.PlaySound("click");
    }
    public virtual void ChangeCellStatementHorizontal()
    {
        isChanging = true;
        switch (currentCell)
        {
            case 0:
                if(cellObjects.Length < 3)
                    break;
                if(!isSilent)
                    SoundManagerUi.Instance.PlaySound("click");
                currentCell = 2;
                break;
            case 1:
                if(cellObjects.Length < 4)
                    break;
                if(!isSilent)
                    SoundManagerUi.Instance.PlaySound("click");
                currentCell = 3;
                break;
            case 2:
                if(!isSilent)
                    SoundManagerUi.Instance.PlaySound("click");
                currentCell = 0;
                break;
            case 3:
                if(!isSilent)
                    SoundManagerUi.Instance.PlaySound("click");
                currentCell = 1;
                break;
            default:
                break;
        }
        UpdateHeartStatement();
    }
    public virtual IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.25f);
        isReady = true;
    }

    public abstract void Accept();
    public virtual void AcceptNext(){}
    public virtual void CellAcceptingInput()
    {
        if (!Keyboard.current.zKey.isPressed
            && !Keyboard.current.enterKey.isPressed)
        {
            isAccepting = false;
        }
        if (!IsListening || IsAccepting || next) return;
        if (Keyboard.current.enterKey.isPressed || Keyboard.current.zKey.isPressed)
        {
            Accept();
            isAccepting = true;
            next = true;
            isReady = false;
            StartCoroutine(Delay());

            if(!isSilent)
                SoundManagerUi.Instance.PlaySound("accept");
        }
    }

    public virtual void CellChangingInput()
    {
        if (!Keyboard.current.upArrowKey.isPressed
            && !Keyboard.current.downArrowKey.isPressed
            && !Keyboard.current.leftArrowKey.isPressed
            && !Keyboard.current.rightArrowKey.isPressed)
        {
            isChanging = false;
        }
        if (!IsListening || IsChanging) 
            return;
        if (Keyboard.current.upArrowKey.isPressed)
        {
            ChangeCellStatement(-1);
        }
        if (Keyboard.current.downArrowKey.isPressed)
        {
            ChangeCellStatement(1);
        }
        if (Keyboard.current.leftArrowKey.isPressed)
        {
            ChangeCellStatementHorizontal();
        }
        if (Keyboard.current.rightArrowKey.isPressed)
        {
            ChangeCellStatementHorizontal();
        }
    }


    public void InitializeCells(TextMeshProUGUI[] RawCellText, GameObject[] RawHeart, GameObject[] CellObjects)
    {
        cellLines.Clear();

        cellLines.Add(new CellLine());
        cellLines.Add(new CellLine());

        isSilent = false;
        next = false;
        

        // Заполняем линии
        for (int i = 0; i < CellObjects.Length; i++)
        {
            if (i < 2)
            {
                cellLines[0].CellList.Add(new Cell(RawCellText[i], RawHeart[i], CellObjects[i]));
                cellLines[0].CellList[i].Text.text = TextOfCells[i];
                cellLines[0].CellList[i].CellObject.SetActive(true);
                continue;
            }
            if (i < 4)
            {
                cellLines[1].CellList.Add(new Cell(RawCellText[i], RawHeart[i], CellObjects[i]));
                cellLines[1].CellList[i - 2].Text.text = TextOfCells[i];
                cellLines[1].CellList[i - 2].CellObject.SetActive(true);
            }

        }
        LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
    }
    public void ChangePresence(bool value)
    {
        foreach (var item in cellLines)
        {
            foreach (var cell in item.CellList)
            {
                cell.CellObject.SetActive(value);
            }
        }
        isSilent = true;
    }
    public void ListenInput()
    {
        if (!IsReady || next) return;
        CellAcceptingInput();
        CellChangingInput();
    }
    public void ListenNext()
    {
        if(!next || !isReady)
            return;
        if (Keyboard.current.enterKey.isPressed || Keyboard.current.zKey.isPressed)
        {
            AcceptNext();
        }
    }
    public void UpdateHeartStatement()
    {
        // выключаем все сердца
        foreach (var line in cellLines)
            foreach (var cell in line.CellList)
                cell.Heart.SetActive(false);

        // включаем нужное 
        switch (CurrentCell)
        {
            case 0: cellLines[0].CellList[0].Heart.SetActive(true); break;
            case 1: cellLines[0].CellList[1].Heart.SetActive(true); break;
            case 2: cellLines[1].CellList[0].Heart.SetActive(true); break;
            case 3: cellLines[1].CellList[1].Heart.SetActive(true); break;
            default: break;
        }
    }
    public void ChangeCellText(int number, string value)
    {
        switch (number)
        {
            case 0:
                CellLines[0].CellList[0].Text.text = value;
                break;
            case 1:
                CellLines[0].CellList[1].Text.text = value;
                break;
            case 2:
                CellLines[1].CellList[0].Text.text = value;
                break;
            case 3:
                CellLines[1].CellList[1].Text.text = value;
                break;
            default:
                break;
        }
    }
    public void Type(string text)
    {
        FunnyButtons.Instance.TurnOffButtons();
        FunnyButtons.Instance.CanCancel = false;
        FunnyButtons.Instance.IsActive = false;
        Answer.Instance.SwitchActive(true);
        ChangePresence(false);
        isSilent = true;
        Answer.Instance.Type(text);
    }
}
public class CellLine
{
    public List<Cell> CellList = new List<Cell>();
}
public class Cell
{
    public TextMeshProUGUI Text;
    public GameObject Heart;
    public GameObject CellObject;
    public Cell(TextMeshProUGUI text, GameObject heart, GameObject cellObject)
    {
        Text = text;
        Heart = heart;
        CellObject = cellObject;
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