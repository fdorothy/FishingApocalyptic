using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleSpawner : MonoBehaviour
{
    public Transform bubblePrefab;
    public float maxDistance = 5f;
    public int numBubbles = 10;
    List<Transform> bubbles = new List<Transform>();
    Player player;

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        int i = 0;
        while (i < bubbles.Count)
        {
            Transform b = bubbles[i];
            if (!InRange(b.position))
            {
                Destroy(b.gameObject);
                bubbles.RemoveAt(i);
            }
            else
            {
                i++;
            }
        }
        while (bubbles.Count < numBubbles)
        {
            SpawnBubble();
        }
    }

    bool InRange(Vector3 pt)
    {
        return Vector3.Distance(player.transform.position, pt) < maxDistance;
    }

    void SpawnBubble()
    {
        float r = Random.Range(0f, maxDistance * 0.9f);
        float theta = Random.Range(0f, 360f) * Mathf.PI / 180f;
        Transform b = Instantiate(bubblePrefab, transform);
        b.position = player.transform.position + new Vector3(r * Mathf.Cos(theta), 0f, r * Mathf.Sin(theta));
        bubbles.Add(b);
    }
}
