using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform prefab;
    public Transform around;
    public List<Transform> spawns = new List<Transform>();
    public float maxRange = 10f;
    public int maxSpawn = 10;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < maxSpawn; i++)
            RandomSpawn();
    }

    // Update is called once per frame
    void Update()
    {
        int i = 0;
        while (i < spawns.Count)
        {
            Transform b = spawns[i];
            if (!InRange(b.position))
            {
                Destroy(b.gameObject);
                spawns.RemoveAt(i);
            }
            else
            {
                i++;
            }
        }
        while (spawns.Count < maxSpawn)
        {
            RandomSpawn(true);
        }
    }

    bool InRange(Vector3 pt)
    {
        return Vector3.Distance(around.transform.position, pt) < maxRange;
    }

    Transform RandomSpawn(bool useMaxRange = false)
    {
        Transform t = Spawn();
        float theta = Random.Range(0f, 360f) * Mathf.PI / 180f;
        float r = Random.Range(useMaxRange ? 0f : maxRange * 0.9f, maxRange);
        t.position = new Vector3(Mathf.Cos(theta) * r, 0f, Mathf.Sin(theta) * r) + around.position;
        spawns.Add(t);
        return t;
    }

    Transform Spawn() => Instantiate(prefab, transform);
}
