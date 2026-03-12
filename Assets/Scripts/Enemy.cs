using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;


public class Enemy : MonoBehaviour
{
    private static Enemy _instance;
    public static Enemy Instance => _instance;

    public static EnemyBase CurrentEnemy;

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

        var plus = Math.Abs(value);
        if(plus == Player.Instance.Damage || plus+1 == Player.Instance.Damage)
        {
            StartCoroutine(PerfectDamageAnimation());
            value = -(Player.Instance.Damage*2);
        }

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

        Speech.Instance.Say("Ты правда думаешь, что сможешь меня победить?",true,0.03f,-190);
    }

    public IEnumerator PerfectDamageAnimation()
    {
        var duration = 0.25f;
        for (int i = 0; i < 3; i++)
        {
            FirstNumber.DOColor(Color.blue, duration);
            SecondNumber.DOColor(Color.blue, duration);
            yield return new WaitForSeconds(duration);
            FirstNumber.DOColor(Color.red, duration);
            SecondNumber.DOColor(Color.red, duration);
            yield return new WaitForSeconds(duration);
            FirstNumber.DOColor(Color.yellow, duration);
            SecondNumber.DOColor(Color.yellow, duration);
            yield return new WaitForSeconds(duration);
            FirstNumber.DOColor(Color.purple, duration);
            SecondNumber.DOColor(Color.purple, duration);
            yield return new WaitForSeconds(duration);
            FirstNumber.DOColor(Color.green, duration);
            SecondNumber.DOColor(Color.green, duration);
            yield return new WaitForSeconds(duration);
        }
        FirstNumber.DOColor(Color.red,0.1f);
        SecondNumber.DOColor(Color.red,0.1f);
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
#region Enemy Abstraction
public class EnemyBase
{
    public string Name {get; private set;} = "test";
    public int Relation = 0;
    public Dictionary<string,StateRelationBase> StateRelation = new();

    public EnemyBase(string name)
    {
        Name = name;
    }

}
#region StateRelationBase
public class StateRelationBase
{
    public MovesetBase Moveset {get; private set;} = new();
    public RetortManagerBase RetortManager {get; private set;} = new();
}
#endregion
#region Moveset
public class MovesetBase
{
    public Dictionary<string, Attack> ListOfAttack = new();
    public Attack Get(string name) => ListOfAttack[name];
    public void Add(Attack attack) => ListOfAttack.Add(attack.Name, attack); 
}
#endregion
#region Attack
public class Attack //Обычная атака. Атака босса 
{
    public string Name;
    public int Damage;

    //ссылка на инициализированный объект атаки или линк
    [SerializeField] private GameObject Link;
    public float TimeForAttack = 1;
    public Attack(string name, float timeForAttack = 1)
    {
        Name = name;
        TimeForAttack = timeForAttack;
    }
    public void Start()
    {
        //получаем трансформ родителя
        var place = FunnyBox.Instance.gameObject.transform;

        //создаем объект на сцене
        var obj = Resources.Load<GameObject>("Data/Enemies/" + Enemy.CurrentEnemy.Name + "/Moveset/" + Name);
        Link = UnityEngine.Object.Instantiate(obj,place);

        //записываем объект в лист текущих атак

        //стартуем таймер !! или не стартуем, пусть что то выше этим занимается
    }

    public void Stop()
    {
        //проверяем готов ли объект к удалению.
        if(Link == null) {throw new Exception($"The object has already been deleted. ({Name})");}
        
        //уничтожаем линк
        UnityEngine.Object.Destroy(Link);
    }
}
#endregion
#region RetortManagerBase
public class RetortManagerBase //хранилище ответов
{
    public Dictionary<string, EnemyRetort> Retortion = new();
    public void Add(EnemyRetort enemyRetort) => Retortion.Add(enemyRetort.Name, enemyRetort);
    public EnemyRetort Get(string name) => Retortion[name];
    public void Clear() => Retortion.Clear();
}
#endregion
#region Enemy RetortBase
public class EnemyRetort //ответ это текст в веселой коробке после действия игрока и 
// фраза врага после того, как текст в веселой коробке появится.
{
    public string Name {get; private set;} // имя ответа 
    public string TargetAction {get; private set;} // Действие игрока. Item, Mercy, Act или название определенного предмета
    public int TargetPhase {get; private set;} // фаза на которой ответ сработает. Фаза именно действия игрока.
    public string Answer {get; private set;} // текст в веселой коробке 
    public string Speech {get; private set;} // фраза персонажа
    public EnemyRetort(string name,string targetAction, int targetPhase, string answer, string speech)
    {
        Name = name;
        TargetAction = targetAction;
        TargetPhase = targetPhase;
        Answer = answer;
        Speech = speech;
    } 
}
#endregion
#endregion

