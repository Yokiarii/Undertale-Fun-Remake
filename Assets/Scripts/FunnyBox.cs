using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FunnyBox : MonoBehaviour
{
    #region init

    private static FunnyBox _instance;
    public static FunnyBox Instance => _instance;
    public GameObject[] AllColliders = new GameObject[] { };
    Vector2 DefaultSize = new Vector2(1550,377);
    public Dictionary<string, FightBox> Boxes { get; private set; } = new();
    public GameObject Answer;
    public bool Fighting {get;private set;} = false;

    #endregion
    void Awake()
    {
        _instance = this;
        InitBoxes();
    }
    #region Init Boxes
    void InitBoxes()
    {
        AddBox(522,377); // Чуть больше квадрата
    }
    #endregion
    void AddBox(float width, float height)
    {
        Boxes.Add(AllColliders[AllColliders.Length-1].name,new FightBox(width,height,AllColliders[AllColliders.Length-1]));
    }
    public void ResizeBox(float width, float height)
    {
        gameObject.GetComponent<RectTransform>().DOSizeDelta(new Vector2(width,height),0.5f);
    }
    public void ReturnBoxSize()
    {
        gameObject.GetComponent<RectTransform>().DOSizeDelta(DefaultSize,0.5f);
    }
    public void ResizeBoxByPreset(string nameOfBox)
    {
        foreach (var item in Boxes)
        {
            if(item.Key == nameOfBox)
            {
                Debug.Log(item.Value.Size);
                gameObject.GetComponent<RectTransform>().DOSizeDelta(item.Value.Size,0.5f);
            }
        }
    }
    public void TurnOnFightColliderByPreset(string nameOfBox)
    {
        foreach (var item in Boxes)
        {
            if(item.Key == nameOfBox)
            {
                item.Value.Collider.SetActive(true);
            }
        }
    }
    public void TurnOffAllColliders()
    {
        foreach (var item in Boxes)
        {
            item.Value.Collider.SetActive(false);
        }
    }

}
#region FightBox
public class FightBox : MonoBehaviour
{
    public Vector2 Size { get; private set; }
    public GameObject Collider { get; private set; }
    public FightBox(float width, float height, GameObject collider)
    {
        Size = new Vector2(width,height);
        Collider = collider;
    }
}
#endregion