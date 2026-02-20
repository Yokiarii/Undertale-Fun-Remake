using UnityEngine;

public class EnemyCollider : MonoBehaviour
{
    public int Damage = 1;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player.Instance.ChangeHP(-Damage);
        }
    }
}
