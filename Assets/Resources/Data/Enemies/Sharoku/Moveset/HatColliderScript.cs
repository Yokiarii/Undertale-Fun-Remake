using System.Collections;
using UnityEngine;

public class HatColliderScript : MonoBehaviour
{
    public int Damage = 3;
    public bool IsHeal = false;
    public bool IsHealing = false;
    public bool IsPlayerGrabbing = false;
    public bool PlayerFinallyCatch = false;
    private int _countOfGrabbing = 0;
    public int CountOfGrabbing
    {
        get { return _countOfGrabbing; }
        set
        {
            _countOfGrabbing = value;
            if (_countOfGrabbing == 1)
            {
                Speech.Instance.Say("Почти поймал...");
            }
            if (_countOfGrabbing == 2)
            {
                Speech.Instance.Say("Да уж...");
            }
            if (_countOfGrabbing == 3)
            {
                PlayerFinallyCatch = true;
            }
        }
    }
    private static HatColliderScript _instance;
    public static HatColliderScript Instance => _instance;

    void Start()
    {
        _instance = this;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (IsHeal)
            {
                StartCoroutine(HealDelay());
                StartCoroutine(GrabDelay());
            }
            else
            {
                Player.Instance.ChangeHP(-Damage);
            }
        }
    }
    IEnumerator GrabDelay()
    {
        if (IsPlayerGrabbing)
            yield break;

        IsPlayerGrabbing = true;
        CountOfGrabbing++;
        yield return new WaitForSeconds(5);
        IsPlayerGrabbing = false;
    }
    IEnumerator HealDelay()
    {
        if (IsHealing == true)
            yield break;

        IsHealing = true;
        Player.Instance.ChangeHP(+16);
        yield return new WaitForSeconds(5f);
        IsHealing = false;
    }
}
