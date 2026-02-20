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
    public Sprite[] DefaultSpritesForButtons;
    public Sprite[] OnSpritesForButtons;
    [SerializeField] private Image[] _buttons;
    public Dictionary<int, Image> Buttons = new();
    public bool IsFighting = false;
    public GameObject FightScene;
    public GameObject Answer;

    Vector2 MenuPosition = new Vector2(1550,377);
    Vector2 FightPosition = new Vector2(526f,377f);

    public GameObject[] ButtonsHearts;
    public GameObject MainPanel;
    public TextMeshProUGUI NameText;
    private int CurrentButton = 0;

    bool IsChangingButton = false;

    void Start()
    {
        _instance = this;
        for (int i = 0; i < 4; i++)
        {
            Buttons.Add(i, _buttons[i]);
        }
    }
    public void ChangeScene(Scenes scene)
    {
        CurrentScene = scene;
    }
    public void FixedUpdate()
    {
        MenuUpdate();
    }
    void MenuUpdate()
    {
        if (Keyboard.current.xKey.isPressed)
        {
            CurrentScene = Scenes.Menu;
            if (IsFighting)
            {
                StartCoroutine(CloseFightPanel());
            }
        }
        if (CurrentScene != Scenes.Menu || IsFighting)
        {
            ButtonsHearts[CurrentButton].SetActive(false);
            return;
        }

        if (Keyboard.current.rightArrowKey.isPressed)
        {
            StartCoroutine(ChangeButton(false));
        }
        if (Keyboard.current.leftArrowKey.isPressed)
        {
            StartCoroutine(ChangeButton(true));
        }
        if (!Keyboard.current.rightArrowKey.isPressed
        && !Keyboard.current.leftArrowKey.isPressed)
        {
            IsChangingButton = false;
        }
        if (Keyboard.current.enterKey.isPressed || Keyboard.current.zKey.isPressed)
        {
            ActivateButton();
            SoundManagerUi.Instance.PlaySound("accept");
        }
        ButtonsUpdate();
    }
    public void ButtonsUpdate()
    {
        Buttons[CurrentButton].sprite = OnSpritesForButtons[CurrentButton];
        ButtonsHearts[CurrentButton].SetActive(true);

        foreach (var item in Buttons)
        {
            if (item.Key != CurrentButton)
            {
                item.Value.sprite = DefaultSpritesForButtons[item.Key];
            }
        }
        foreach (var item in ButtonsHearts)
        {
            if (item != ButtonsHearts[CurrentButton])
                item.SetActive(false);
        }
    }
    public IEnumerator ChangeButton(bool negative)
    {
        if (IsChangingButton)
            yield break;
        IsChangingButton = true;
        SoundManagerUi.Instance.PlaySound("click");
        if (negative)
        {
            CurrentButton--;
            if (CurrentButton < 0)
                CurrentButton = 3;
        }
        else
        {
            CurrentButton++;
            if (CurrentButton > 3)
                CurrentButton = 0;
        }
        yield return new WaitForSeconds(0.1f);
    }
    public void ChangeButtonClick(int number)
    {
        if(CurrentButton == number)
        {
            ActivateButton();
        }
        StartCoroutine(ChangeButton(number));
    }
    public IEnumerator ChangeButton(int number)
    {
        CurrentButton = number;
        yield return new WaitForSeconds(0.1f);
    }
    public void ActivateButton()
    {
        CurrentScene = (Scenes)CurrentButton+1;
        Debug.Log(CurrentButton + " " + CurrentScene);
        if(CurrentButton == 0)
        {
              DoFightPanel();  
        }
    }
    public void DoFightPanel()
    {
        MainPanel.GetComponent<RectTransform>().DOSizeDelta(FightPosition,0.5f);
        FightScene.SetActive(true);
        Player.Instance.PlayerGameObject.GetComponent<Transform>().localPosition = new Vector2(0,0);
        IsFighting = true;
        Answer.SetActive(false);
    }
    public IEnumerator CloseFightPanel()
    {
        MainPanel.GetComponent<RectTransform>().DOSizeDelta(MenuPosition,0.5f);
        FightScene.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        IsFighting = false;
        Answer.SetActive(true);
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