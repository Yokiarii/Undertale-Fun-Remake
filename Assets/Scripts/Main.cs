
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    private static Main _instance;
    public static Main Instance => _instance;

    public static string Version = "Build v0.8.0-demo PC";
    public static string Tag_Version = "demo_8";

    public GameObject AllSpace;
    public GameObject MainCanvas;
    public GameObject GameOver;
    public GameObject FightScene;

    public GameObject TempHeartForAnimation;

    private ChromaticAberration CA;
    [SerializeField] private Volume postProcessVolume;

    void Awake()
    {
        _instance = this;
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
        
        AllSpace.GetComponent<CanvasGroup>().alpha = 0;
    }

    void Start()
    {
        StartCoroutine(TempAnimation());
    }

    IEnumerator TempAnimation()
    {
        TempHeartForAnimation.SetActive(true);
        yield return new WaitForSeconds(1f);
        SoundManagerUi.Instance.PlaySound("enter_fight_sound");

        yield return new WaitForSeconds(0.5f);

        var temp = TempHeartForAnimation.GetComponent<Image>();
        temp.DOFade(0,0.06f);
        yield return new WaitForSeconds(0.06f);
        temp.DOFade(1,0.06f);
        yield return new WaitForSeconds(0.06f);
        temp.DOFade(0,0.06f);
        yield return new WaitForSeconds(0.06f);
        temp.DOFade(1,0.06f);
        yield return new WaitForSeconds(0.06f);
        temp.DOFade(0,0.06f);
        yield return new WaitForSeconds(0.06f);
        temp.DOFade(1,0.06f);

        TempHeartForAnimation.transform.DOLocalMove(new Vector3(-607.9f,-481.5f,0),0.7f);
        yield return new WaitForSeconds(0.7f);
        AllSpace.GetComponent<CanvasGroup>().DOFade(1,0.7f);
        yield return new WaitForSeconds(0.7f);
        TempHeartForAnimation.SetActive(false);
        Answer.Instance.Type(Enemy.CurrentEnemy.StateRelation[Enemy.CurrentEnemy.CurrentRelation].BaseAnswer);
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
