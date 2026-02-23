using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private static Enemy _instance;
    public static Enemy Instance => _instance;

    void Awake()
    {
        _instance = this;
    }

    public string Name {get; private set;} = "";
    public int[] HP {get; private set;} = new int[2]{300,300};
    public int Damage {get; private set;} = 1;
    
    public GameObject DamageAnimation;
    public GameObject EnemyImage;

    public void ChangeHp(int value)
    {
        var temp = HP[0];
        HP[0] += value;

        if(HP[0] < 0)
            HP[0] = 0;
        if(HP[0] > HP[1])
            HP[0] = HP[1];

        if(temp > HP[0]) //Deal damage
        {
            StartCoroutine(DealDamageAnimation());
        }
        Debug.Log(temp - HP[0]);
    }

    public IEnumerator DealDamageAnimation()
    {
        DamageAnimation.SetActive(true);
        yield return new WaitForSeconds(1.3f);
        DamageAnimation.SetActive(false);
    }

}

public class DamageCalculator
{
    /// <summary>
    /// Рассчитывает урон в зависимости от отклонения от центра.
    /// </summary>
    /// <param name="deviation">Отклонение (может быть отрицательным).</param>
    /// <param name="baseDamage">Базовый урон при точном попадании (по умолчанию 20).</param>
    /// <param name="factor">Коэффициент крутизны (по умолчанию 56, получен из примера).</param>
    /// <returns>Итоговый урон (вещественное число).</returns>
    public static double CalculateDamage(double deviation, double baseDamage = 20, double factor = 56)
    {
        return baseDamage / (1 + Math.Abs(deviation) / factor);
    }

    /// <summary>
    /// Целочисленная версия с округлением до ближайшего целого.
    /// </summary>
    public static int CalculateDamageInt(float deviation, int baseDamage = 20, int factor = 56)
    {
        double damage = (double)baseDamage / (1 + (double)Math.Abs(deviation) / factor);
        return (int)Math.Round(damage);
    }
}