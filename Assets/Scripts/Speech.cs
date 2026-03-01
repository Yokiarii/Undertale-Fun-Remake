using System.Collections;
using System.Linq.Expressions;
using DG.Tweening;
using UnityEngine;

public class Speech : TextGenerator
{
    private static Speech _instance;
    public static Speech Instance => _instance;
    public GameObject TextBabel;
    void Awake()
    {
        _instance = this;
    }
    void Start()
    {
        Say("Манга йоу йоу манга йоу йоу йоу манга манга йоу");
    }
    public void Say(string text,bool autoClose = true, float duration = 0.06f, float right = -172f, float bottom = -44.4f)
    {
        TextBabel.GetComponent<CanvasGroup>().DOFade(1,0.01f);
        SwitchActive(true);
        var tempMin = TextBabel.GetComponent<RectTransform>().offsetMin;
        var tempMax = TextBabel.GetComponent<RectTransform>().offsetMax;
        TextBabel.GetComponent<RectTransform>().offsetMax = new Vector2(-right,tempMax.y);
        TextBabel.GetComponent<RectTransform>().offsetMin = new Vector2(tempMin.x,bottom);

        Type(text,duration);
        if(autoClose)
            StartCoroutine(Delay());
    } 
    void OnEnable()
    {
        TextField.text = "";
        SwitchActive(true);
    }
    public void SwitchActive(bool value)
    {
        if (value == false)
        {
            TextField.text = "";
            CurrentText = "";
            TempCurrentText = "";
        }
        IsActive = value;
    }
    IEnumerator Delay()
    {
        while (!IsComplete)
        {
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(2f);
        TextBabel.GetComponent<CanvasGroup>().DOFade(0,0.3f);
        yield return new WaitForSeconds(0.3f);
        SwitchActive(false);
    }
}
