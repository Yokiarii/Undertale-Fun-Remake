using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public string Name { get; private set; }
    public int[] HP { get; private set; } = new int[2] { 20, 20 };
    public int Damage {get; private set; } = 20;

    public bool IsDead { get; private set; } = false;

    private static Player _instance;
    public static Player Instance => _instance;

    public GameObject PlayerGameObject;
    public Slider SliderHP;
    public TextMeshProUGUI TextHP;
    bool IsTakeDamage = false;
    public Animator Anim;
    public GameObject ShatteredHeart;

    void Awake()
    {
        _instance = this;
    }

    public void ChangeName(string name) => Name = name;
    public void ChangeHP(int vector)
    {
        if (IsTakeDamage || IsDead)
            return;

        var temp = HP[0];
        HP[0] += vector;
        if (HP[0] <= 0)
        {
            HP[0] = 0;
            IsDead = true;
            SoundManagerUi.Instance.PlaySound("dead");

            UpdateSlider();

            DeadAnimation();
            return;
        }
        if (HP[0] > HP[1])
            HP[0] = HP[1];
        if (temp > HP[0])
        {
            Main.Instance.AllSpace.transform.DOShakePosition(0.5f, 6, 15, 50);
            IsTakeDamage = true;
            StartCoroutine(FadeAnimationHeart());
            SoundManagerUi.Instance.PlaySound("damagePlayer");
            StartCoroutine(Main.Instance.ShakeCA());
        }
        if(temp < HP[0])
        {
            SoundManagerUi.Instance.PlaySound("healing");
        }
        UpdateSlider();
    }
    
    public IEnumerator FadeAnimationHeart()
    {
        var duration = 0.1f;
        var canvasGroup = PlayerGameObject.GetComponent<CanvasGroup>();
        canvasGroup.DOFade(0.05f, duration);
        yield return new WaitForSeconds(duration);
        canvasGroup.DOFade(1, duration);
        yield return new WaitForSeconds(duration);
        canvasGroup.DOFade(0.05f, duration);
        yield return new WaitForSeconds(duration);
        canvasGroup.DOFade(1, duration);
        yield return new WaitForSeconds(duration);
        canvasGroup.DOFade(0.05f, duration);
        yield return new WaitForSeconds(duration);
        canvasGroup.DOFade(1, duration);
        IsTakeDamage = false;
    }

    void DeadAnimation()
    {
        PlayerGameObject.transform.SetParent(Main.Instance.MainCanvas.transform);
        Main.Instance.AllSpace.SetActive(false);
        StartCoroutine(GameOverAnimation());
    }

    IEnumerator GameOverAnimation()
    {

        Anim.Play("Shattered Heart");
        yield return new WaitForSeconds(2f);

        var shardHeart = Instantiate(ShatteredHeart, Main.Instance.MainCanvas.transform);
        shardHeart.transform.position = PlayerGameObject.transform.position;
        PlayerGameObject.SetActive(false);

        yield return new WaitForSeconds(1.5f);

        var tempObj = Main.Instance.GameOver;
        tempObj.SetActive(true);
        var temp = tempObj.GetComponent<SpriteRenderer>();
        temp.DOFade(0.6f, 5f);

    }

    public void ReturnPLayer()
    {
        PlayerGameObject.transform.SetParent(Main.Instance.FightScene.transform);
    }

    public void ReturnPlayerPosition()
    {
        PlayerGameObject.transform.localPosition = new Vector3(0,0,PlayerGameObject.transform.localPosition.z);
    }

    public void Revive()
    {
        IsDead = false;
        HP[0] = HP[1];

        UpdateSlider();

        Anim.Play("idle");
    }

    public void UpdateSlider()
    {
        SliderHP.value = HP[0];
        TextHP.text = HP[0].ToString();
    }
}
