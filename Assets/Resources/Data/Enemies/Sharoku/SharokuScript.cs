using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SharokuScript : MonoBehaviour
{
    private static SharokuScript _instance;
    public static SharokuScript Instance => _instance;

    private string _state = "Idle";
    public string State
    {
        get
        {
            return _state;
        }
        set
        {
            _state = value;
        }

    }

    public GameObject Hat;
    public GameObject Head;
    public GameObject Body;
    public GameObject Arm_Left;
    public GameObject Arm_Right;
    public GameObject Leg_Left;
    public GameObject Leg_Right;

    public GameObject FullBody;
    public GameObject Parts_Head;
    public GameObject Parts_Right_Hand;
    public GameObject Parts_Left_Hand;
    public GameObject Parts_Legs;
    public GameObject Parts_Body;

    public List<GameObject> Parts = new List<GameObject>();

    public FLAG FIRST_HAT_MOVEMENT = new FLAG();
    public FLAG SECOND_HAT_MOVEMENT = new FLAG();
    public FLAG THIRD_HAT_MOVEMENT = new FLAG();
    public FLAG FOURTH_HAT_MOVEMENT = new FLAG();
    public FLAG FINAL_STAGE = new FLAG();
    public FLAG ZABAVKA = new FLAG();
    public FLAG READY_TO_MERCY = new();

    public string CURRENT_ACTION = "none";

    void Awake()
    {
        _instance = this;

        Parts.Add(Hat);
        Parts.Add(Head);
        Parts.Add(Body);
        Parts.Add(Arm_Left);
        Parts.Add(Arm_Right);
        Parts.Add(Leg_Left);
        Parts.Add(Leg_Right);

        ChangeState();
    }

    void Start()
    {
        ZABAVKA.phase = 0;
        READY_TO_MERCY.IsReady = true;
    }

    void FixedUpdate()
    {
        if (FIRST_HAT_MOVEMENT.IsReady && !FIRST_HAT_MOVEMENT.IsClose)
        {
            Hat.transform.localPosition = new Vector3(Hat.transform.localPosition.x - 6f, Hat.transform.localPosition.y);
            FIRST_HAT_MOVEMENT.IsClose = true;
        }

        if (SECOND_HAT_MOVEMENT.IsReady && !SECOND_HAT_MOVEMENT.IsClose)
        {
            Hat.transform.localPosition = new Vector3(Hat.transform.localPosition.x - 6f, Hat.transform.localPosition.y);
            SECOND_HAT_MOVEMENT.IsClose = true;
        }

        if (THIRD_HAT_MOVEMENT.IsReady && !THIRD_HAT_MOVEMENT.IsClose)
        {
            Hat.transform.localPosition = new Vector3(Hat.transform.localPosition.x - 6f, Hat.transform.localPosition.y - 4);
            THIRD_HAT_MOVEMENT.IsClose = true;
        }

        if (FOURTH_HAT_MOVEMENT.IsReady && !FOURTH_HAT_MOVEMENT.IsClose)
        {
            Hat.transform.localPosition = new Vector3(Hat.transform.localPosition.x + 4f, Hat.transform.localPosition.y - 15);
            FOURTH_HAT_MOVEMENT.IsClose = true;
        }
        if (ZABAVKA.phase == 2)
        {
            ZABAVKA.IsClose = true;
        }
        if (Enemy.CurrentEnemy.ACTS["украсть шляпу"] == 2)
        {
            Enemy
                .CurrentEnemy
                .StateRelation[Enemy.CurrentEnemy.CurrentRelation]
                .Moveset
                .ListOfAttack["Pistol"]
                .IsActive = false;

            Enemy
                .CurrentEnemy
                .StateRelation[Enemy.CurrentEnemy.CurrentRelation]
                .Moveset
                .ListOfAttack["Pistol_alt_1"]
                .IsActive = true;
        }
        if (Enemy.CurrentEnemy.ACTS["украсть шляпу"] == 3)
        {
            Enemy
                .CurrentEnemy
                .StateRelation[Enemy.CurrentEnemy.CurrentRelation]
                .Moveset
                .ListOfAttack["Bomb"]
                .IsActive = false;

            Enemy
                .CurrentEnemy
                .StateRelation[Enemy.CurrentEnemy.CurrentRelation]
                .Moveset
                .ListOfAttack["Bomb_Rush_Main"]
                .IsActive = true;
        }
    }

    public void ChangeState(string state = "Idle")
    {
        State = state;

        StopAllCoroutines();
        switch (State)
        {
            case "Idle":
                ShakeAnimation(Parts_Right_Hand);
                ShakeAnimation(Parts_Left_Hand);
                ShakeAnimation(Parts_Head);
                ShakeAnimation(Parts_Body);
                break;
            case "Death":
                foreach (var item in Parts)
                {
                    DeathAnimation(item);
                }
                break;
            default:
                break;
        }
    }

    void ShakeAnimation(GameObject obj)
    {
        StartCoroutine(ShakeCoroutine(obj));
    }

    void DeathAnimation(GameObject obj)
    {
        StartCoroutine(DeathCoroutine(obj));
    }

    IEnumerator ShakeCoroutine(GameObject obj)
    {
        var OriginalPosition = obj.transform.localPosition;

        //-->>
        obj.transform.DOLocalMove(new Vector3(
            OriginalPosition.x + Random.Range(-1, 1),
            OriginalPosition.y + Random.Range(7, 11), 0), 1.5f).SetEase(Ease.InOutCirc);
        yield return new WaitForSeconds(1.5f);

        //<<--
        obj.transform.DOLocalMove(new Vector3(
            OriginalPosition.x,
            OriginalPosition.y, 0), 1.5f).SetEase(Ease.InOutCirc);
        yield return new WaitForSeconds(1.5f);

        StartCoroutine(ShakeCoroutine(obj));
    }

    IEnumerator DeathCoroutine(GameObject obj)
    {
        yield return new WaitForSeconds(3);
        Speech.Instance.Say("Ну...", false, 0.15f);
        yield return new WaitForSeconds(5);
        Speech.Instance.Say("Это не круто", true, 0.15f);
    }
}
