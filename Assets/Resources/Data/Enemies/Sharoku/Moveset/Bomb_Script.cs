using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Bomb_Script : MonoBehaviour
{
    public GameObject Bomb;
    public GameObject Particle;
    public bool IsCatch = false;
    void Start()
    {
        Debug.Log("Bomb attack is started");
        StartCoroutine(Delay());
    }
    void Update()
    {
        if (Bomb.transform.localPosition.y < 139 && !IsCatch)
        {
            Bomb.GetComponent<Collider2D>().isTrigger = false;
        }
    }
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(1);
        Bomb.transform.DOLocalMoveY(Bomb.transform.localPosition.y + 20, 0.50f).SetEase(Ease.OutCirc);
        yield return new WaitForSeconds(0.50f);
        Vector2 direction = (Player.Instance.PlayerGameObject.transform.localPosition - Bomb.transform.localPosition).normalized;
        Bomb.GetComponent<Rigidbody2D>().AddForce(direction * 4f, ForceMode2D.Impulse);
        StartCoroutine(Catch());
        StartCoroutine(Timer());
    }
    IEnumerator Catch()
    {
        var switcher = true;
        while (switcher)
        {

            Collider2D[] hits = Physics2D.OverlapCircleAll(Bomb.transform.position, 0.32f);

            foreach (var hit in hits)
            {
                if (hit.CompareTag("Player"))
                {
                    Bomb.transform.SetParent(Player.Instance.PlayerGameObject.transform);
                    Bomb.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
                    Bomb.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;

                    switcher = false;
                    IsCatch = true;
                    Bomb.GetComponent<Collider2D>().isTrigger = true;
                    yield break;
                }
            }
            yield return new WaitForFixedUpdate();
        }
    }
    IEnumerator Timer()
    {
        int time = 0;
        bool state = true;
        while (state)
        {
            if (IsCatch)
            {
                yield return new WaitForSeconds(2);
                for (int i = 0; i < 12; i++)
                {
                    var temp = Instantiate(Particle, transform);
                    temp.transform.position = Bomb.transform.position;
                    temp.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-100, 100), Random.Range(-100, 100)));
                }

                Bomb.SetActive(false);
                Main.Instance.AllSpace.transform.DOShakePosition(0.8f, 8, 15, 50);
                yield return new WaitForSeconds(3);
                Fight.Instance.QuitFightExternal();
                yield break;
            }
            yield return new WaitForSeconds(1);
            time++;
            if (time == 3)
            {
                for (int i = 0; i < 12; i++)
                {
                    var temp = Instantiate(Particle, transform);
                    temp.transform.position = Bomb.transform.position;
                    temp.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-100, 100), Random.Range(-100, 100)));
                }

                var temp2 = Instantiate(Particle, transform);
                temp2.transform.position = Bomb.transform.position;
                Vector2 direction = (Player.Instance.PlayerGameObject.transform.localPosition - temp2.transform.localPosition).normalized;
                temp2.GetComponent<Rigidbody2D>().AddForce(direction * 2f, ForceMode2D.Impulse);
                yield return new WaitForEndOfFrame();

                Bomb.SetActive(false);
                Main.Instance.AllSpace.transform.DOShakePosition(0.8f, 8, 15, 50);
                yield return new WaitForSeconds(3);
                state = false;
            }
        }
        Fight.Instance.QuitFightExternal();
    }
    void OnDestroy()
    {
        Destroy(Bomb);
    }
}
