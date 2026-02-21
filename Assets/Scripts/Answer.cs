using System.Collections;
using TMPro;
using UnityEngine;

public class Answer : MonoBehaviour
{
    private static Answer _instance;
    public static Answer Instance => _instance;

    [SerializeField] private TextMeshProUGUI TextField;

    string CurrentText = "";
    string TempCurrentText = "Привет! Как дела? Что нового? Я знаю, что это не диалоговое окно.";
    float DurationPerSymbol = 0.06f;
    string TypingSound = "click"; 
    bool IsTyping = false;
    bool Wait = true;

    void Awake()
    {
        _instance = this;
    }
    
    void Update()
    {
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

        for (int i = 0; i < CurrentText.Length-1; i++)
        {
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
    public void Type(string text, bool wait = true, float duration = 0.06f, string sound = "click")
    {
        TempCurrentText = text;
        DurationPerSymbol = duration;
        TypingSound = sound;
        Wait = wait;
    }
}
