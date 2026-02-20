using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public string Name {get;private set;}
    public int[] HP {get;private set;} = new int[2]{20,20};

    private static Player _instance;
    public static Player Instance => _instance;
    public GameObject PlayerGameObject;
    public Slider SliderHP;
    public TextMeshProUGUI TextHP;
    bool IsTakeDamage = false;
    void Awake()
    {
        _instance = this;
    }

    public void ChangeName(string name) => Name = name;
    public void ChangeHP(int vector)
    {
        if(IsTakeDamage)
            return;

        var temp = HP[0];
        HP[0] += vector;
        if(HP[0] < 0)
            HP[0] = 0;
        if(HP[0] > HP[1])
            HP[0] = HP[1];
        if(temp > HP[0])
        {
            Main.Instance.AllSpace.transform.DOShakePosition(0.5f,6,15,50);
            IsTakeDamage = true;
            StartCoroutine(FadeAnimationHeart());
        }
        SliderHP.value = HP[0];
        TextHP.text = HP[0].ToString();
    }
    public IEnumerator FadeAnimationHeart()
    {
        var canvasGroup = PlayerGameObject.GetComponent<CanvasGroup>();
        canvasGroup.DOFade(0.05f,0.2f);
        yield return new WaitForSeconds(0.2f);
        canvasGroup.DOFade(1,0.2f);
        yield return new WaitForSeconds(0.2f);
        canvasGroup.DOFade(0.05f,0.2f);
        yield return new WaitForSeconds(0.2f);
        canvasGroup.DOFade(1,0.2f);
        yield return new WaitForSeconds(0.2f);
        canvasGroup.DOFade(0.05f,0.2f);
        yield return new WaitForSeconds(0.2f);
        canvasGroup.DOFade(1,0.2f);
        IsTakeDamage = false;
    }
}
