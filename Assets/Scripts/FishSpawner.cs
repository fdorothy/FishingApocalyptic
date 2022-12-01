using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    public Fish fishPrefab;

    List<Fish> fishes = new List<Fish>();
    GameObject fishParent;
    const int MAX_FISHES = 100;
    BoxCollider boxCollider;

    private void Start()
    {
        fishParent = GameObject.Find("Fishes");
        boxCollider = GetComponent<BoxCollider>();
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            while (fishes.Count < MAX_FISHES)
            {
                SpawnFish();
            }
            yield return new WaitForSeconds(5f);
        }
    }

    void SpawnFish()
    {
        // create fish somewhere in the circle around the player
        Fish f = Instantiate<Fish>(fishPrefab, fishParent.transform);
        float x = Random.Range(boxCollider.bounds.min.x, boxCollider.bounds.max.x);
        float y = Random.Range(boxCollider.bounds.min.y, boxCollider.bounds.max.y);
        float z = Random.Range(boxCollider.bounds.min.z, boxCollider.bounds.max.z);
        f.transform.position = new Vector3(x, y, z);
        f.bounds = boxCollider.bounds;
        fishes.Add(f);
    }
}
