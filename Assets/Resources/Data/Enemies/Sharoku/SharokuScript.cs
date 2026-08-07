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
    public GameObject Parts_Arms;
    public GameObject Parts_Legs;
    public GameObject Parts_Body;

    public List<GameObject> Parts = new List<GameObject>();

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

    public void ChangeState(string state = "Idle")
    {
        State = state;

        StopAllCoroutines();
        switch (State)
        {
            case "Idle":
                ShakeAnimation(Parts_Arms);
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
            OriginalPosition.x + Random.Range(-1,1),
            OriginalPosition.y + Random.Range(7,11),0),1.5f).SetEase(Ease.InOutCirc);
        yield return new WaitForSeconds(1.5f);

        //<<--
        obj.transform.DOLocalMove(new Vector3(
            OriginalPosition.x,
            OriginalPosition.y,0),1.5f).SetEase(Ease.InOutCirc);
        yield return new WaitForSeconds(1.5f);

        StartCoroutine(ShakeCoroutine(obj)); 
    }

    IEnumerator DeathCoroutine(GameObject obj)
    {

        yield return new WaitForSeconds(3);
        Speech.Instance.Say("Как это..", false, 0.15f);
        yield return new WaitForSeconds(5);
        Speech.Instance.Say("А зачем это..?", false, 0.15f);
        yield return new WaitForSeconds(5);
        Speech.Instance.Say("За что мне это..?", false, 0.09f);
    }
}
