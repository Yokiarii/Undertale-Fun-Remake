using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class AttackScene : MonoBehaviour
{
    private static AttackScene _instance;
    public static AttackScene Instance => _instance;

    public int CurrentCell = 0;

    [SerializeField] public GameObject[] Cells = new GameObject[4];
    [SerializeField] public TextMeshProUGUI[] Texts = new TextMeshProUGUI[4];
    [SerializeField] public GameObject[] Stars = new GameObject[4];

    void Awake()
    {
        _instance = this;
    }

    void FixedUpdate()
    {
        if (Keyboard.current.enterKey.isPressed || Keyboard.current.zKey.isPressed )
        {
            
        }
    }

    public void UpdateStars()
    {
        for (int i = 0; i < Stars.Length; i++)
        {
            if(i == 0)
                continue;
            Stars[i].SetActive(false);
        }
    }
}
