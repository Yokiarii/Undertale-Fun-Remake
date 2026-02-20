using UnityEngine;

public class Main : MonoBehaviour
{
    public static SceneManager SM;

    private static Main _instance;
    public static Main Instance => _instance;
    void Awake()
    {
        _instance = this;
    }
}
