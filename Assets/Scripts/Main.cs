
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Main : MonoBehaviour
{
    private static Main _instance;
    public static Main Instance => _instance;
    public GameObject AllSpace;
    public GameObject MainCanvas;
    public GameObject GameOver;
    public GameObject FightScene;

    private ChromaticAberration CA;
    [SerializeField] private Volume postProcessVolume;

    void Awake()
    {
        _instance = this;
        Application.targetFrameRate = 60;
    }
    
    public IEnumerator ShakeCA()
    {
        if (postProcessVolume.profile.TryGet(out CA))
        {
            DOTween.To(() => CA.intensity.value,
                       x => CA.intensity.value = x,
                       0.30f,
                       0.16f);
                   
            yield return new WaitForSeconds(0.16f);
            DOTween.To(() => CA.intensity.value,
                       x => CA.intensity.value = x,
                       0,
                       0.16f);
        }
        else
        {
            Debug.LogError("ChromaticAberration not found in the Volume profile!");
        }
    }
}
