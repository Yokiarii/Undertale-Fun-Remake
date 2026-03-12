
using System.Collections;
using System.IO;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using Newtonsoft.Json;
using System.Collections.Generic;
using System;

public class Main : MonoBehaviour
{
    private static Main _instance;
    public static Main Instance => _instance;

    public static string Version = "Build v0.1.0-demo PC";
    public static string Tag_Version = "demo_7";

    public GameObject AllSpace;
    public GameObject MainCanvas;
    public GameObject GameOver;
    public GameObject FightScene;

    private ChromaticAberration CA;
    [SerializeField] private Volume postProcessVolume;

    void Awake()
    {
        _instance = this;
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;


    }

    void Start()
    {
        Debug.Log(Data.Instance.EnemyData.Get("Sharoku").StateRelation["Normal"].Moveset.Get("attack number onee").Damage);
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
