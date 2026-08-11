using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb_Rush_Main : MonoBehaviour
{
    public GameObject bomb;
    public List<GameObject> obj = new();

    void Start()
    {
        StartCoroutine(Pipeline());
    }

    IEnumerator Pipeline()
    {
        Attack();
        yield return new WaitForSeconds(7);

        Attack();
        yield return new WaitForSeconds(8);


        Attack();
        yield return new WaitForSeconds(0.2f);
        Attack();


        yield return new WaitForSeconds(8);

        Attack();
        yield return new WaitForSeconds(0.2f);
        Attack();
        yield return new WaitForSeconds(0.2f);
        Attack();
        yield return new WaitForSeconds(0.2f);
        Attack();
        yield return new WaitForSeconds(0.2f);

        yield return new WaitForSeconds(9);

        for (int i = 0; i < 20; i++)
        {
            Attack(false);
            yield return new WaitForSeconds(0.2f);
        }

        yield return new WaitForSeconds(7);

        Speech.Instance.Say("...");

        yield return new WaitForSeconds(4);

        Speech.Instance.Say("Кажется эти бракованные...");

        yield return new WaitForSeconds(5);

        Fight.Instance.QuitFightExternal();
    }

    void Attack(bool isLethal = true)
    {
        var temp = Instantiate(bomb,FunnyBox.Instance.gameObject.transform);
        temp.GetComponent<Bomb_Rush_Script>().IsLethal = isLethal;
        obj.Add(temp);
    }

    void OnDestroy()
    {
        foreach (var item in obj)
        {
            Destroy(item);
        }
    }
}
