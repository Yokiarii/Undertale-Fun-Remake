using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Main : MonoBehaviour
{
    private static Main _instance;
    public static Main Instance => _instance;
    public GameObject AllSpace;

    void Awake()
    {
        _instance = this;
        Application.targetFrameRate = 60;
    }

}
