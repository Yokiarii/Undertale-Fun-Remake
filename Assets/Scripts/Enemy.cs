using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    private static Enemy _instance;
    public static Enemy Instance => _instance;

    void Awake()
    {
        _instance = this;
    }

    public string Name { get; private set; } = "Protivnic";
    public int[] HP { get; private set; } = new int[2] { 300, 300 };
    int LastHp = 300;
    public int Damage { get; private set; } = 1;

    public GameObject DamageAnimation;
    public GameObject EnemyImage;
    public GameObject DamageInfo;

    public Image FirstNumber;
    public Image SecondNumber;
    public Sprite[] Numbers;
    public Slider EnemyHp;

    public void ChangeHp(int value)
    {
        var temp = HP[0];
        
        HP[0] += value;

        if (HP[0] < 0)
            HP[0] = 0;
        if (HP[0] > HP[1])
            HP[0] = HP[1];

        if (temp > HP[0]) //Deal damage
        {
            StartCoroutine(DealDamageAnimation(value));
        }
    }

    public IEnumerator DealDamageAnimation(int damage)
    {
        DamageAnimation.SetActive(true);
        yield return new WaitForSeconds(1f); //режет противника
        SoundManagerUi.Instance.PlaySound("damageEnemy");
        DamageAnimation.SetActive(false);

        EnemyImage.transform.DOShakePosition(2f, 10).SetEase(Ease.Linear); // противник трясется 

        damage = Math.Abs(damage);

        if (damage.ToString().Length != 2)
        {
            FirstNumber.sprite = Numbers[0];
            var temp3 = damage.ToString();
            var temp4 = temp3[0].ToString();
            SecondNumber.sprite = Numbers[int.Parse(temp4)];
        }
        else
        {
            string temp = damage.ToString();
            string temp2 = temp[0].ToString();
            FirstNumber.sprite = Numbers[int.Parse(temp2)]; //делаем цифры

            temp = damage.ToString();
            temp2 = temp[1].ToString();
            SecondNumber.sprite = Numbers[int.Parse(temp2)];
        }

        EnemyHp.maxValue = HP[1];
        EnemyHp.value = LastHp;
        EnemyHp.DOValue(HP[0], 1.2f); // Поменять значение слайдера хп врага
        LastHp = HP[0]; 

        DamageInfo.SetActive(true);
        var dmg = DamageInfo.transform.localPosition; // анимация цифр
        DamageInfo.transform.DOLocalJump(dmg, 65, 1, 0.56f);

        yield return new WaitForSeconds(2);

        PlayerAttack.Instance.LineStop.SetActive(false);
        PlayerAttack.Instance.RangeImage.transform.DOScaleX(0, 0.5f);
        Fight.Instance.Init();

        yield return new WaitForSeconds(0.5f);

        PlayerAttack.Instance.gameObject.SetActive(false);
        PlayerAttack.Instance.RangeImage.transform.DOScaleX(2.71f, 1);

        FunnyButtons.Instance.TurnOffButtons();

    }

}

public class DamageCalculator
{
    public static float CalculateDamage(float value, float baseDamage, float falloffFactor)
    {
        float distance = Math.Abs(value);
        // Чем больше falloffFactor, тем быстрее падает урон

        return baseDamage / (1f + falloffFactor * distance);
    }
    public static int CalculateDamageInt(float value, float baseDamage = 20, float falloffFactor = 0.5f)
    {
        float distance = Math.Abs(value);
        // Чем больше falloffFactor, тем быстрее падает урон
        return (int)Math.Round(baseDamage / (1f + falloffFactor * distance));
    }
}