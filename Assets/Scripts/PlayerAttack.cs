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

    public bool PlayerClick = false;
    public bool IsReady = false;
    public bool IsFollowing = true;
    public Vector3 OriginalPosLine;

    void Awake()
    {
        _instance = this;
        OriginalPosLine = Line.transform.position;
    }
    void OnEnable()
    {
        Line.transform.position = OriginalPosLine;
        Line.SetActive(true);
        LineStop.SetActive(false);
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
        if(!IsReady)
            return;
        if(PlayerClick)
            return;
        if(Keyboard.current.enterKey.isPressed || Keyboard.current.zKey.isPressed)
        {
            StopLine();
        }
    }

    void StartLine()
    {
        Line.transform.DOLocalMoveX(740f,2.5f).SetEase(Ease.InOutQuad);
    }

    void StopLine()
    {
        SoundManagerUi.Instance.PlaySound("slash");
        PlayerClick = true;

        IsFollowing = false;
        Line.SetActive(false);
        LineStop.SetActive(true);
        
        var deviation = LineStop.transform.position.x;
        Enemy.Instance.ChangeHp(-DamageCalculator.CalculateDamageInt(deviation));
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
