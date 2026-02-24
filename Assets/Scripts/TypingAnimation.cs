using System.Collections;
using TMPro;
using UnityEngine;

public class TypingAnimation : MonoBehaviour
{
    TextMeshProUGUI text;
    string TargetText;
    void OnEnable()
    {
        text = GetComponent<TextMeshProUGUI>();
        TargetText = text.text;
        text.text = "";
        StartCoroutine(Animation());
    }
    IEnumerator Animation()
    {
        for (int i = 0; i < TargetText.Length; i++)
        {
            text.text += TargetText[i];
            yield return new WaitForSeconds(0.035f);
        }
    }
}
