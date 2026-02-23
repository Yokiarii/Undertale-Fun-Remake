using System.Collections;
using System.Threading;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{

    public GameObject RangeImage;
    public GameObject Line;
    public GameObject LineStop;

    public bool PlayerClick = false;
    public bool IsReady = false;
    public bool IsFollowing = true;
    
    void OnEnable()
    {
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
        IsFollowing = false;
        Line.SetActive(false);
        LineStop.SetActive(true);
        
        var deviation = LineStop.transform.position.x;
        Enemy.Instance.ChangeHp(DamageCalculator.CalculateDamageInt(deviation));
    }
    void FollowLine()
    {
        LineStop.transform.position = Line.transform.position;
    }
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.7f);
        IsReady = true;
    }
}
