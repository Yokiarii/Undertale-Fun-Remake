using System.Collections;
using TMPro;
using Unity.VectorGraphics;
using UnityEngine;

public class Answer : TextGenerator
{
    private static Answer _instance;
    public static Answer Instance => _instance;
    [SerializeField] private TextMeshProUGUI TextFieldStar;

    void Awake()
    {
        _instance = this;
    }
    void OnEnable()
    {
        TextField.text = "";
        SwitchActive(true);
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

    public void EnterAnswer() // ааа эээ 
    {
        
        //переключаем на главную сцену.
        SceneManager.Instance.ChangeScene(Scenes.Menu);

        //получаем массив классов наследников с интерфейсом IScene
        var temp = SceneManager.Instance.ScenesInterface;

        //закрываем каждую сцену
        foreach (var item in temp)
        {
            item.QuitScene();
        }

        //отключаем нижнии кнопки и отключаем сердечко у кнопок (на всякий случай)
        FunnyButtons.Instance.TurnOffButtons();

        //отключаем возможность назад нажать.
        FunnyButtons.Instance.CanCancel = false;

        //включаем ансвер
        SwitchActive(true);
    }
    public void ExitAnswer(bool fightAfter)
    {
        
    }

}

public class TextGenerator : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI TextField;
    protected string CurrentText = "";
    protected string TempCurrentText = "";
    protected float DefaultDurationPerSymbol = 0.06f;
    protected string TypingSound = "typing";
    protected bool IsActive = false;
    protected bool IsBreak = true;
    protected bool IsComplete = false;
    public void Type(string text, float duration = 0.06f, string sound = "typing")
    {
        IsBreak = true;
        TempCurrentText = text;
        DefaultDurationPerSymbol = duration;
        TypingSound = sound;
        CurrentText = TempCurrentText;
        IsComplete = false;
        StopTyping();
        StartCoroutine(TypingAnimation());
    }
    protected void StopTyping()
    {
        StopCoroutine(TypingAnimation());
        StopAllCoroutines();
    }
    protected IEnumerator TypingAnimation()
    {
        yield return new WaitForSeconds(0.25f);
        IsBreak = false;
        TextField.text = "";

        string currentText = "";

        string[] words = CurrentText.Split(' ');

        foreach (string word in words)
        {
            if (IsBreak || !IsActive)
            {
                IsComplete = true;
                yield break;
            }
            // Проверяем перенос
            if (!WillWordFit(currentText, word))
            {
                currentText += "\n";
                TextField.text = currentText;
            }

            // Печатаем слово ПОСИМВОЛЬНО
            foreach (char c in word)
            {
                if (IsBreak || !IsActive)
                {
                    IsComplete = true;
                    yield break;
                }
                currentText += c;
                TextField.text = currentText;
                SoundManagerUi.Instance.PlaySound(TypingSound);
                yield return new WaitForSeconds(DefaultDurationPerSymbol);
            }
            if (IsBreak || !IsActive)
            {
                IsComplete = true;
                yield break;
            }
            // Пробел после слова
            currentText += " ";
            TextField.text = currentText;
            yield return new WaitForSeconds(DefaultDurationPerSymbol);
        }
        IsComplete = true;
    }
    protected bool WillWordFit(string currentText, string nextWord)
    {
        TextField.text = currentText + " " + nextWord;
        TextField.ForceMeshUpdate();

        float preferredWidth = TextField.preferredWidth;
        float rectWidth = TextField.rectTransform.rect.width;

        return preferredWidth <= rectWidth;
    }
}