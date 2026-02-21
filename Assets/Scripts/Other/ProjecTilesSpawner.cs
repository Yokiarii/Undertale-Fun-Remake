using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class ProjecTilesSpawner : MonoBehaviour
{
    public GameObject prefabBall;
    void FixedUpdate()
    {
        if (Keyboard.current.oKey.isPressed)
        {
            StartCoroutine(SpawnManyProjectTiles(10));
        }
    }
    IEnumerator SpawnManyProjectTiles(int g = 1)
    {
        for (int i = 0; i < g; i++)
        {
            SpawnProjectTile();
            yield return new WaitForSeconds(0.4f);
        }
    }
    void SpawnProjectTile()
    {
        var temp = GameObject.Instantiate(prefabBall,gameObject.transform);
        temp.transform.localPosition = new Vector3(Random.Range(-191.88f,217.3f),739,0);
    }
}
