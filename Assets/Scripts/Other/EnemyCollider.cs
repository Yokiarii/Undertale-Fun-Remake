using System.Collections;
using UnityEngine;

public class EnemyCollider : MonoBehaviour
{
    public int Damage = 1;
    void Start()
    {
        StartCoroutine(DeadTimer());
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player.Instance.ChangeHP(-Damage);
        }
    }
    IEnumerator DeadTimer()
    {
        yield return new WaitForSeconds(10);
        Destroy(gameObject);
    }
}
