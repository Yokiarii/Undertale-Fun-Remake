using System.Collections;
using TMPro;
using UnityEngine;

public class Answer : MonoBehaviour
{
    private static Answer _instance;
    public static Answer Instance => _instance;

    [SerializeField] private TextMeshProUGUI TextField;
    [SerializeField] private TextMeshProUGUI TextFieldStar;

    string CurrentText = "";
    string TempCurrentText = "";
    float DurationPerSymbol = 0.06f;
    string TypingSound = "typing"; 
    bool IsTyping = false;
    bool Wait = true;
    public bool IsActive = false;

    void Awake()
    {
        _instance = this;
    }
    void OnEnable()
    {
        TextField.text = "";
        SwitchActive(true);
        TypeAgain();
    }

    void Update()
    {
        if (!IsActive)
        {
            return;
        }
        if(IsTyping && Wait)
            return;
        if(CurrentText == TempCurrentText)
        {
            return;
        } else
        {
            CurrentText = TempCurrentText;
            StartCoroutine(TypingAnimation());
        }
    }

    IEnumerator TypingAnimation()
    {
        IsTyping = true;
        TextField.text = "";

        for (int i = 0; i < CurrentText.Length; i++)
        {
            if (!IsActive)
            {
                IsTyping = false;
                yield break;
            }
            TextField.text += CurrentText[i];
            SoundManagerUi.Instance.PlaySound(TypingSound);
            
            var tempDuration = (CurrentText[i] == '.' 
            || CurrentText[i] == ',' 
            || CurrentText[i] == '!'
            || CurrentText[i] == '?') 
                ? DurationPerSymbol+0.1f 
                : DurationPerSymbol;

            yield return new WaitForSeconds(tempDuration);
        }
        IsTyping = false;
    }

    public void Type(string text, bool wait = true, float duration = 0.06f, string sound = "typing")
    {
        TempCurrentText = text;
        DurationPerSymbol = duration;
        TypingSound = sound;
        Wait = wait;
    }
    public void SwitchActive(bool value)
    {
        TextField.gameObject.SetActive(value);
        TextFieldStar.gameObject.SetActive(value);
        if(value == true)
        {
            TypeAgain();
        }
        if(value == false)
        {
            TextField.text = "";
        }
        IsActive = value;
    }
    public void TypeAgain()
    {
        CurrentText = "";
    }
}
