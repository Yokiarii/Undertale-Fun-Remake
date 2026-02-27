using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class QuitApplication : MonoBehaviour
{
    public CanvasGroup text;
    public bool IsUpdating = false;

    void Update()
    {
        if(text.alpha == 1)
            Application.Quit();
        if (IsUpdating)
            return;

        if (Keyboard.current.escapeKey.isPressed)
        {
            StartCoroutine(Delay());
        } else
        {
            text.alpha -= 0.01f;
        }
    }

    IEnumerator Delay()
    {
        IsUpdating = true;
        text.alpha += 0.06f;
        yield return new WaitForSeconds(0.05f);
        IsUpdating = false;
    }
}
