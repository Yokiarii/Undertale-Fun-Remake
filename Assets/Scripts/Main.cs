using System.Collections;
using UnityEngine;

public class Main : MonoBehaviour
{
    private static Main _instance;
    public static Main Instance => _instance;
    void Awake()
    {
        _instance = this;
        Application.targetFrameRate = 60;
    }
    void Start()
    {
        SceneManager.Instance.DoFightPanel();
        StartCoroutine(Temp());
    }
    IEnumerator Temp()
    {
        yield return new WaitForSeconds(5);
        Player.Instance.ChangeHP(-5);
    }
}
