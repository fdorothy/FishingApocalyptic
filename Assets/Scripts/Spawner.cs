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
    Collider spawnArea;

    // Start is called before the first frame update
    void Start()
    {
        spawnArea = GameObject.Find("SpawnArea").GetComponent<Collider>();
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
            if (b == null)
            {
                spawns.RemoveAt(i);
            } else
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
        int maxIterations = 20;
        Vector3 pt;
        do
        {
            float x = Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x);
            float z = Random.Range(spawnArea.bounds.min.z, spawnArea.bounds.max.z);
            pt = new Vector3(x, 0f, z);
            maxIterations--;
        } while (Physics.OverlapSphere(pt, 0f).Length > 0 && maxIterations > 0);
        Transform t = Spawn();
        t.position = pt;
        spawns.Add(t);
        return t;
    }

    Transform Spawn() => Instantiate(prefab, transform);
}
