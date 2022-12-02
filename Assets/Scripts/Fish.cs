using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Fish : MonoBehaviour
{

    [System.Serializable]
    public class FishStats
    {
        public Sprite sprite;
        public string name;
        public int cost;
        public float targetSize = 0.5f;
    }

    public float fastSwimSpeed = 0.5f;
    public float slowSwimSpeed = 0.1f;
    public int health = 1, maxHealth = 1;
    public float cursorSpeed = 1.0f;
    public Bounds bounds;
    public bool biting = false;

    Vector3 target;
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
            if (!lure)
            {
                target = new Vector3(
                    Random.Range(bounds.min.x, bounds.max.x),
                    Random.Range(bounds.min.y, bounds.max.y),
                    Random.Range(bounds.min.z, bounds.max.z)
                );
            }
            yield return new WaitForSeconds(1.0f);

            // check if we are close to the target lure
            lure = FindObjectOfType<Lure>();
            if (lure != null && !lure.bitten)
            {
                if (Vector3.Distance(transform.position, lure.transform.position) < 1.0f)
                    target = lure.transform.position;
                else
                    lure = null;
            }
            if (lure != null && lure.bitten)
                lure = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (biting)
            return;
        if (Vector3.Distance(target, transform.position) > 0.1f)
        {
            transform.position += transform.forward * slowSwimSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(target - transform.position, Vector3.up), Time.deltaTime);
        }
        else
        {
            // bite the lure!
            if (lure && !lure.bitten)
            {
                biting = true;
                FindObjectOfType<Player>().Bite(this);
            }
        }
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, bounds.min.x, bounds.max.x),
            Mathf.Clamp(transform.position.y, bounds.min.y, bounds.max.y),
            Mathf.Clamp(transform.position.z, bounds.min.z, bounds.max.z)
        );
    }

    public void ReleaseFish()
    {
        biting = false;
    }
}
