using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    public Transform fishPrefab;
    public Transform player;

    List<Transform> fishes = new List<Transform>();
    const int MAX_FISHES = 100;
    const float MAX_RADIUS= 10f;
    const float MAX_DEPTH = -5f;

    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            RemoveFishOutOfRange();
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
        Transform f = Instantiate(fishPrefab, transform);
        float angle = Random.Range(0f, 360f) * Mathf.PI / 180.0f;
        float radius = Random.Range(0f, MAX_RADIUS);
        float depth = Random.Range(0f, MAX_DEPTH);
        Vector3 pos = new Vector3(radius * Mathf.Cos(angle), depth, radius * Mathf.Sin(angle));
        f.position = Flatten(player.position) + pos;
        fishes.Add(f);
    }

    void RemoveFishOutOfRange()
    {
        int i = 0;
        while (i < fishes.Count)
        {
            Transform fish = fishes[i];
            if (!IsFishInSpawnArea(fish))
            {
                fishes.RemoveAt(i);
                Destroy(fish.gameObject);
            }
            else
                i++;
        }
    }

    bool IsFishInSpawnArea(Transform f)
    {
        float distance = Vector3.Distance(Flatten(player.position), Flatten(f.position));
        return distance < MAX_RADIUS;
    }

    Vector3 Flatten(Vector3 p) => new Vector3(p.x, 0f, p.z);
}
