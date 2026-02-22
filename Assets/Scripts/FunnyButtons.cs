using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class FunnyButtons : MonoBehaviour
{
    private static FunnyButtons _instance;
    public static FunnyButtons Instance => _instance;

    [SerializeField] public GameObject[] Buttons = new GameObject[4];
    [SerializeField] public Sprite[] DefaultButtons = new Sprite[4];
    [SerializeField] public Sprite[] ActiveButtons = new Sprite[4];
    [SerializeField] public GameObject[] Hearts = new GameObject[4];

    public int CurrentActiveButton = 0;
    public bool IsActive = true;
    public bool IsChanging = false;

    void Awake()
    {
        _instance = this;
    }

    void OnEnable()
    {
        UpdateButtonAndHeart();
    }

    void Update()
    {
        ButtonUpdate();
    }

    void ButtonUpdate()
    {
        if(Keyboard.current.enterKey.isPressed && Keyboard.current.xKey.isPressed)
            return;
        if(!Keyboard.current.leftArrowKey.isPressed && !Keyboard.current.rightArrowKey.isPressed)
        {
            IsChanging = false;
        }

        if (Keyboard.current.xKey.isPressed && !IsActive)
        {
            Menu();
            UpdateButtonAndHeart();
        }

        if(IsChanging)
            return;
        if(!IsActive)
            return;

        if (Keyboard.current.leftArrowKey.isPressed)
        {
            ChangeCurrentButton(true);
        }
        if (Keyboard.current.rightArrowKey.isPressed)
        {
            ChangeCurrentButton(false);
        }

        if (Keyboard.current.enterKey.isPressed || Keyboard.current.zKey.isPressed)
        {
            switch (CurrentActiveButton)
            {
                case 0:
                    Fight();
                    break;
                case 1:
                    Act();
                    break;
                case 2: 
                    Items();
                    break;
                case 3: 
                    Mercy();
                    break;
                default:
                    break;
            }
            SoundManagerUi.Instance.PlaySound("accept");
        }
    }
    void ChangeCurrentButton(bool negative)
    {
        IsChanging = true;
        
        CurrentActiveButton = negative ? CurrentActiveButton-1 : CurrentActiveButton+1;
        SoundManagerUi.Instance.PlaySound("click");

        if(CurrentActiveButton > 3) 
            CurrentActiveButton = 0;
        if(CurrentActiveButton < 0) 
            CurrentActiveButton = 3;

        UpdateButtonAndHeart();
    }
    void UpdateButtonAndHeart()
    {
        for (int g = 0; g < Buttons.Length; g++)
        {
            Buttons[g]
                .GetComponent<Image>()
                .sprite = DefaultButtons[g];
            Hearts[g].SetActive(false);

            if (g == CurrentActiveButton)
            {
                Buttons[g]
                    .GetComponent<Image>()
                    .sprite = ActiveButtons[g];
                Hearts[g].SetActive(true);
            }
        }
    }
    void TurnOffButtons()
    {
        for (int g = 0; g < Buttons.Length; g++)
        {
            Buttons[g]
                .GetComponent<Image>()
                .sprite = DefaultButtons[g];
            Hearts[g].SetActive(false);
        }
    }
    void TurnOffButtonsWithOutHeart()
    {
        for (int g = 0; g < Buttons.Length; g++)
        {
            if (g == CurrentActiveButton)
            {
                Buttons[g]
                    .GetComponent<Image>()
                    .sprite = ActiveButtons[g];
                Hearts[g].SetActive(false);
            }
        }
    }
    public void Fight()
    {
        SceneManager.Instance.ChangeScene(Scenes.Attack);
        TurnOffButtonsWithOutHeart();
        IsActive = false;
        Answer.Instance.SwitchActive(false);
    }
    public void Act()
    {
        SceneManager.Instance.ChangeScene(Scenes.Act);
        TurnOffButtonsWithOutHeart();
        IsActive = false;
        Answer.Instance.SwitchActive(false);
    }
    public void Items()
    {
        SceneManager.Instance.ChangeScene(Scenes.Items);
        TurnOffButtonsWithOutHeart();
        IsActive = false;
        Answer.Instance.SwitchActive(false);
    }
    public void Mercy()
    {
        SceneManager.Instance.ChangeScene(Scenes.Mercy);
        TurnOffButtonsWithOutHeart();
        IsActive = false;
        Answer.Instance.SwitchActive(false);
    }
    public void Menu()
    {
        SceneManager.Instance.ChangeScene(Scenes.Menu);
        IsActive = true;
        Answer.Instance.SwitchActive(true);
    }
}
