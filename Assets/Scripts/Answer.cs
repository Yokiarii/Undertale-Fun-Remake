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
    public bool IsActive = false;
    public bool IsBreak = true;

    void Awake()
    {
        _instance = this;
    }
    void OnEnable()
    {
        TextField.text = "";
        SwitchActive(true);
    }

    void Update()
    {
        
    }

    public void Type(string text, float duration = 0.06f, string sound = "typing")
    {
        IsBreak = true;
        TempCurrentText = text;
        DurationPerSymbol = duration;
        TypingSound = sound;
        CurrentText = TempCurrentText;
        StopTyping();
        StartCoroutine(TypingAnimation());
    }

    void StopTyping()
    {
        StopCoroutine(TypingAnimation());
        StopAllCoroutines();
    }

    IEnumerator TypingAnimation()
    {
        yield return new WaitForSeconds(0.25f);
        IsBreak = false;
        TextField.text = "";

        string currentText = "";

        string[] words = CurrentText.Split(' ');

        foreach (string word in words)
        {
            if(IsBreak || !IsActive)
                yield break;
            // Проверяем перенос
            if (!WillWordFit(currentText, word))
            {
                currentText += "\n";
                TextField.text = currentText;
            }

            // Печатаем слово ПОСИМВОЛЬНО
            foreach (char c in word)
            {
                if(IsBreak || !IsActive)
                    yield break;
                currentText += c;
                TextField.text = currentText;
                SoundManagerUi.Instance.PlaySound(TypingSound);
                yield return new WaitForSeconds(DurationPerSymbol);
            }
            if(IsBreak || !IsActive)
                yield break;
            // Пробел после слова
            currentText += " ";
            TextField.text = currentText;
            yield return new WaitForSeconds(DurationPerSymbol);
        }
    }

    public void SwitchActive(bool value)
    {
        TextField.gameObject.SetActive(value);
        TextFieldStar.gameObject.SetActive(value);
        if (value == false)
        {
            TextField.text = "";
            CurrentText = "";
            TempCurrentText = "";
        }
        IsActive = value;
    }

    bool WillWordFit(string currentText, string nextWord)
    {
        TextField.text = currentText + " " + nextWord;
        TextField.ForceMeshUpdate();

        float preferredWidth = TextField.preferredWidth;
        float rectWidth = TextField.rectTransform.rect.width;

        return preferredWidth <= rectWidth;
    }
}
