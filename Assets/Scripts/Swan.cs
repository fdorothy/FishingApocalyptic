using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swan : MonoBehaviour
{
    public enum SwanState
    {
        ROAMING,
        AVOIDING
    }

    public SwanState swanState = SwanState.ROAMING;
    public float minSpeed = 0.0f, maxSpeed = 1.0f;
    Quaternion targetRotation;
    float targetSpeed = 0.0f;
    float speed = 0.0f;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(SwanRoutine());
    }

    IEnumerator SwanRoutine()
    {
        while (true)
        {
            targetRotation = Quaternion.Euler(0f, Random.Range(0, 360.0f), 0.0f);
            targetSpeed = Random.Range(minSpeed, maxSpeed);
            yield return new WaitForSeconds(5.0f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (swanState)
        {
            case SwanState.ROAMING:
                {
                    speed = Mathf.Lerp(speed, targetSpeed, Time.deltaTime);
                    rb.MovePosition(transform.position + transform.forward * targetSpeed * Time.deltaTime);
                    rb.MoveRotation(Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime));
                } break;
        }
    }
}
