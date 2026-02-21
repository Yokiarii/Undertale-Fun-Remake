
using UnityEngine;

public class Main : MonoBehaviour
{
    private static Main _instance;
    public static Main Instance => _instance;
    public GameObject AllSpace;
    public GameObject MainCanvas;
    public GameObject GameOver;
    public GameObject FightScene;

    void Awake()
    {
        _instance = this;
        Application.targetFrameRate = 60;
    }

}
