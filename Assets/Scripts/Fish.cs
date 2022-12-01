using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Fish : MonoBehaviour
{

    [System.Serializable]
    public struct FishStats
    {
        public Sprite sprite;
        public string name;
        public int cost;
    }

    public float fastSwimSpeed = 1.0f;
    public float slowSwimSpeed = 0.5f;
    public int health = 1, maxHealth = 1;
    public float cursorSpeed = 1.0f;
    public Bounds bounds;

    Transform target;
    Lure lure;

    public FishStats fishStats = new FishStats();

    // Start is called before the first frame update
    void Start()
    {
        float theta = 180f;
        transform.rotation *= Quaternion.Euler(Random.Range(-theta, theta), Random.Range(-theta, theta), Random.Range(-theta, theta));
        StartCoroutine(AIRoutine());
    }

    IEnumerator AIRoutine()
    {
        while (true)
        {
            // slightly change direction randomly
            float theta = 5f;
            transform.rotation *= Quaternion.Euler(Random.Range(-theta, theta), Random.Range(-theta, theta), Random.Range(-theta, theta));
            yield return new WaitForSeconds(1.0f);

            // check if we are close to the target lure
            lure = FindObjectOfType<Lure>();
            if (lure != null && !lure.bitten)
            {
                if (Vector3.Distance(transform.position, lure.transform.position) < 1.0f)
                    target = lure.transform;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            if (Vector3.Distance(target.position, transform.position) > 0.25f)
            {
                Vector3 dir = (target.position - transform.position).normalized;
                transform.position += dir * fastSwimSpeed * Time.deltaTime;
            } else
            {
                // bite the lure!
                if (lure && !lure.bitten)
                {
                    lure.Bite(this);
                }
            }
        } else
        {
            // swim around randomly
            transform.position += slowSwimSpeed * transform.forward * Time.deltaTime;
        }

        // clamp the position to our bounding box
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, bounds.min.x, bounds.max.x),
            Mathf.Clamp(transform.position.y, bounds.min.y, bounds.max.y),
            Mathf.Clamp(transform.position.z, bounds.min.z, bounds.max.z)
        );
    }
}
