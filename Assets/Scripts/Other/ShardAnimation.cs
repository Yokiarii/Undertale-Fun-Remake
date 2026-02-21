using UnityEngine;

public class ShardAnimation : MonoBehaviour
{
    void Start()
    {
        float tempY = 0;
        if (Random.Range(0,100) > 80)
        {
            tempY = -50;
        }
        var vector = new Vector2(Random.Range(-30,30),Random.Range(tempY,120));
        gameObject.GetComponent<Rigidbody2D>().AddForce(vector);
    }
}
