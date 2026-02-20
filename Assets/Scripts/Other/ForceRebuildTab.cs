using UnityEngine;
using UnityEngine.UI;

public class ForceRebuildTab : MonoBehaviour
{
    void OnEnable()
    {
        Rebuild();
    }
    void Start()
    {
        Rebuild();
    }
    void Rebuild()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
    }
}
