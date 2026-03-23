using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fight : MonoBehaviour
{
    private static Fight _instance;
    public static Fight Instance => _instance;

    public bool IsActive = false;
    float TimeForFight = 6;
    public List<GameObject> ActiveAttacks = new();

    void Awake()
    {
        _instance = this;
    }

    public void Init()
    {
        SceneManager.Instance.ChangeScene(Scenes.Fight);
        Enemy.Instance.DamageInfo.SetActive(false);

        FunnyBox.Instance.ResizeBoxByPreset("FightCollider3:4");
        FunnyBox.Instance.TurnOnFightColliderByPreset("FightCollider3:4");
        Player.Instance.PlayerGameObject.SetActive(true);
        Player.Instance.ReturnPlayerPosition();
        
        StartCoroutine(Delay());
    }

    public void SpawnAttack()
    {
        var attack = Enemy.CurrentEnemy.GetAttack();
        ActiveAttacks.Add(Instantiate(Enemy.CurrentEnemy.GetAttackPrefab(attack.Name),FunnyBox.Instance.gameObject.transform));
        TimeForFight = attack.TimeForAttack;
    }

    public IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.5f);
        SpawnAttack();
        if(TimeForFight == -1)
            yield break;
        yield return new WaitForSeconds(TimeForFight);

        StartCoroutine(QuitFight());
    }

    public void QuitFightExternal()
    {
        StartCoroutine(QuitFight());
    }

    public IEnumerator QuitFight()
    {
        foreach (var item in ActiveAttacks)
        {
            Destroy(item);
        }
        FunnyBox.Instance.ReturnBoxSize();
        FunnyBox.Instance.TurnOffAllColliders();
        Player.Instance.PlayerGameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        Answer.Instance.SwitchActive(true);
        SceneManager.Instance.ChangeScene(Scenes.Menu);
        FunnyButtons.Instance.IsActive = true;
        FunnyButtons.Instance.UpdateButtonAndHeart();
        FunnyButtons.Instance.CanCancel = true;
        Answer.Instance.Type(Enemy.CurrentEnemy.StateRelation[Enemy.CurrentEnemy.CurrentRelation].BaseAnswer);
    }


}
