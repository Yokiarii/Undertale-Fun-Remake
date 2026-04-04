using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{

    private static PlayerAttack _instance;
    public static PlayerAttack Instance => _instance;

    public GameObject RangeImage;
    public GameObject Line;
    public GameObject LineStop;
    public GameObject Miss;

    public bool PlayerClick = false;
    public bool IsReady = false;
    public bool IsFollowing = true;
    public bool IsMiss = false;
    public Vector3 OriginalPosLine;

    void Awake()
    {
        _instance = this;
        OriginalPosLine = Line.transform.position;
    }
    void OnEnable()
    {
        IsMiss = false;
        Line.transform.position = OriginalPosLine;
        Line.SetActive(true);
        LineStop.SetActive(false);
        Miss.SetActive(false);
        PlayerClick = false;
        IsReady = false;
        IsFollowing = true;
        StartCoroutine(Delay());
        StartLine();
    }

    void FixedUpdate()
    {
        if (IsFollowing)
        {
            FollowLine();
        }
        if(LineStop.GetComponent<RectTransform>().localPosition.x > 738)
        {
            IsMiss = true;
        }
        if (IsMiss && IsFollowing)
        {
            StartCoroutine(DoMiss());
            return;
        }
        if(!IsReady)
            return;
        if(PlayerClick)
            return;
        if(Keyboard.current.enterKey.isPressed || Keyboard.current.zKey.isPressed)
        {
            StartCoroutine(StopLine());
        }
    }

    IEnumerator DoMiss()
    {
        IsFollowing = false;
        Miss.SetActive(true);
        Miss.GetComponent<RectTransform>().DOLocalJump(Miss.GetComponent<RectTransform>().localPosition, 65, 1, 0.56f);
        PlayerClick = true;

        Line.SetActive(false);
        LineStop.SetActive(false);

        yield return new WaitForSeconds(2f);

        RangeImage.transform.DOScaleX(0, 0.5f);
        Fight.Instance.Init();

        yield return new WaitForSeconds(0.5f);

        gameObject.SetActive(false);
        RangeImage.transform.DOScaleX(2.71f, 1);

        FunnyButtons.Instance.TurnOffButtons();
    }

    void StartLine()
    {
        Line.transform.DOLocalMoveX(740f,2.5f).SetEase(Ease.InOutQuad);
    }

    IEnumerator StopLine()
    {
        SoundManagerUi.Instance.PlaySound("slash");
        PlayerClick = true;

        IsFollowing = false;
        Line.SetActive(false);
        LineStop.SetActive(true);
        
        var deviation = LineStop.transform.position.x;
        Enemy.Instance.ChangeHp(-DamageCalculator.CalculateDamageInt(deviation,Player.Instance.Damage)); // Отнимает хп у врага

        yield return new WaitForSeconds(3);

        LineStop.SetActive(false);
        RangeImage.transform.DOScaleX(0, 0.5f); //выключает панель с атакой 
        
        SceneManager.Instance.FightSceneObserver.EnterFight();

        yield return new WaitForSeconds(0.5f);

        gameObject.SetActive(false);
        RangeImage.transform.DOScaleX(2.71f, 1); //выключает оставшуюся панель с атакой 

        FunnyButtons.Instance.TurnOffButtons();

    }
    void FollowLine()
    {
        LineStop.transform.position = Line.transform.position;
    }
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.3f);
        IsReady = true;
    }
}
