using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] prefabs;
    public float delay = 2.0f;
    public bool active = true;
    public Vector2 delayRange = new Vector2(1, 2);

    void Start()
    {
        ResetDelay();
        // is a way that run a script independent of a normal loop
        StartCoroutine(EnemyGenerator());
    }

    // IEnumerator is a need for Courutine
    IEnumerator EnemyGenerator ()
    {
        // bekletme yeri
        yield return new WaitForSeconds(delay);

        if (active)
        {
            var newTransform = transform;

            GameObjectUtil.Instantiate(prefabs[Random.Range(0, prefabs.Length)], newTransform.position);
            ResetDelay();
        }

        StartCoroutine(EnemyGenerator());
    }

    void ResetDelay ()
    {
        delay = Random.Range(delayRange.x, delayRange.y);
    }

}
