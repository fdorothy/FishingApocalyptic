using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    Transform target;
    float fastSwimSpeed = 1.0f;
    float slowSwimSpeed = 0.5f;
    Lure lure;
    bool biting = false;

    const float MAX_DEPTH = 5f;

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
            float theta = 10f;
            transform.rotation *= Quaternion.Euler(Random.Range(-theta, theta), Random.Range(-theta, theta), Random.Range(-theta, theta));
            yield return new WaitForSeconds(1.0f);

            // check if we are close to the target lure
            lure = FindObjectOfType<Lure>();
            if (lure != null)
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
                if (lure && !biting && !lure.bitten)
                {
                    lure.Bite(this);
                    biting = true;
                }
            }
        } else
        {
            // swim around randomly
            transform.position += slowSwimSpeed * transform.forward * Time.deltaTime;
        }

        // clamp the position to just below the waterline and above our max depth
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -MAX_DEPTH, 0.0f), transform.position.z);
    }
}
