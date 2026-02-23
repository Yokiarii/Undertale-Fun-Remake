using System.Collections;
using UnityEngine;

public class Fight : MonoBehaviour
{
    private static Fight _instance;
    public static Fight Instance => _instance;

    public bool IsActive = false;
    int TimeForFight = 6;

    void Awake()
    {
        _instance = this;
    }

    public void Init()
    {
        Enemy.Instance.DamageInfo.SetActive(false);

        FunnyBox.Instance.ResizeBoxByPreset("FightCollider3:4");
        FunnyBox.Instance.TurnOnFightColliderByPreset("FightCollider3:4");
        
        StartCoroutine(Delay());
    }

    public IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(ProjecTilesSpawner.Instance.SpawnManyProjectTiles(10));
        yield return new WaitForSeconds(TimeForFight);

        StartCoroutine(QuitFight());
    }

    public IEnumerator QuitFight()
    {
        FunnyBox.Instance.ReturnBoxSize();
        FunnyBox.Instance.TurnOffAllColliders();
        Player.Instance.PlayerGameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        Answer.Instance.SwitchActive(true);
        SceneManager.Instance.ChangeScene(Scenes.Menu);
        FunnyButtons.Instance.IsActive = true;
        FunnyButtons.Instance.UpdateButtonAndHeart();
    }


}
