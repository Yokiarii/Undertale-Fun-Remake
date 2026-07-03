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

    void ChangeState()
    {
        StopAllCoroutines();
        switch (State)
        {
            case "Idle":
                foreach (var item in Parts)
                {
                    if(item.name == "Leg_Left" || item.name == "Leg_Right")
                        continue;
                    ShakeAnimation(item);
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
}
