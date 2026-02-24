using System.Collections;
using System.Text;
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
        if (IsTyping && Wait)
            return;
        if (CurrentText == TempCurrentText)
        {
            return;
        }
        else
        {
            CurrentText = TempCurrentText;
            StartCoroutine(TypingAnimation());
        }
    }

    IEnumerator TypingAnimation()
    {
        IsTyping = true;

        TextField.text = "";

        string currentText = "";

        string[] words = CurrentText.Split(' ');

        foreach (string word in words)
        {

            // Проверяем перенос
            if (!WillWordFit(currentText, word))
            {
                currentText += "\n";
                TextField.text = currentText;
            }

            // Печатаем слово ПОСИМВОЛЬНО
            foreach (char c in word)
            {
                if (!IsActive)
                {
                    IsTyping = false;
                    yield break;
                }
                currentText += c;
                TextField.text = currentText;
                SoundManagerUi.Instance.PlaySound("typing");
                yield return new WaitForSeconds(DurationPerSymbol);
            }

            // Пробел после слова
            currentText += " ";
            TextField.text = currentText;
            yield return new WaitForSeconds(DurationPerSymbol);

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
        if (value == true)
        {
            TypeAgain();
        }
        if (value == false)
        {
            TextField.text = "";
        }
        IsActive = value;
    }
    public void TypeAgain()
    {
        CurrentText = "";
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
