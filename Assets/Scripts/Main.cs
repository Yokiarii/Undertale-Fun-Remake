using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Main : MonoBehaviour
{
    private static Main _instance;
    public static Main Instance => _instance;
    bool Tempbool = false;
    public GameObject AllSpace;

    void Awake()
    {
        _instance = this;
        Application.targetFrameRate = 60;
    }
    void Start()
    {
        SceneManager.Instance.DoFightPanel();
    }

    void FixedUpdate()
    {
        if(Keyboard.current.hKey.isPressed)
            StartCoroutine(Temp());
    }

    IEnumerator Temp()
    {
        if(Tempbool)
            yield break;
        Tempbool = true;
        Player.Instance.ChangeHP(-2);
        yield return new WaitForSeconds(2);
        Tempbool = false;
    }
}
