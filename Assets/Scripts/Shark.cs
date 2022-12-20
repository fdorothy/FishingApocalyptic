using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shark : MonoBehaviour
{
    public enum SharkState
    {
        ROAMING,
        CHASING
    }

    public SharkState sharkState = SharkState.ROAMING;
    public float minSpeed = 0.0f, maxSpeed = 1.0f;
    public float maxChaseTime = 5.0f;
    Quaternion targetRotation;
    float targetSpeed = 0.0f;
    float speed = 0.0f;
    Rigidbody rb;
    Player player;
    public float maxRange;
    float chasingTime;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = FindObjectOfType<Player>();
        StartCoroutine(SharkRoutine());
    }

    IEnumerator SharkRoutine()
    {
        while (true)
        {
            switch (sharkState)
            {
                case SharkState.ROAMING:
                    {
                        targetRotation = Quaternion.Euler(0f, Random.Range(0, 360.0f), 0.0f);
                        targetSpeed = Random.Range(minSpeed, maxSpeed);
                        yield return new WaitForSeconds(5.0f);
                    }
                    break;
                case SharkState.CHASING:
                    {
                        yield return new WaitForSeconds(1.0f);
                    } break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (sharkState)
        {
            case SharkState.ROAMING:
                {
                    speed = Mathf.Lerp(speed, targetSpeed, Time.deltaTime);
                    rb.MovePosition(transform.position + transform.forward * targetSpeed * Time.deltaTime);
                    rb.MoveRotation(Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime));

                    // are we close to the player? if so chase them!
                    if (Vector3.Distance(transform.position, player.transform.position) < maxRange)
                    {
                        sharkState = SharkState.CHASING;
                        chasingTime = maxChaseTime;
                    }
                }
                break;
            case SharkState.CHASING:
                {
                    speed = Mathf.Lerp(speed, maxSpeed, Time.deltaTime);
                    targetRotation = Quaternion.LookRotation(player.transform.position - transform.position, Vector3.up);
                    rb.MovePosition(transform.position + transform.forward * targetSpeed * Time.deltaTime);
                    rb.MoveRotation(Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 10f));

                    chasingTime -= Time.deltaTime;
                    if (chasingTime < 0.0f)
                        sharkState = SharkState.ROAMING;

                    if (Vector3.Distance(transform.position, player.transform.position) > maxRange)
                    {
                        sharkState = SharkState.ROAMING;
                    }
                } break;
        }
    }
}
