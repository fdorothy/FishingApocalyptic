using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public enum PlayerState
    {
        READY,
        CAST
    }

    public float speed = 1.0f;
    public float turningSpeed = 90f;
    public Transform cameraOrigin;
    public Bobber bobberPrefab;
    public Fishbar fishbar;
    Rigidbody rb;
    float castStrength = MIN_CAST_STRENGTH;
    const float MAX_CAST_STRENGTH = 5f;
    const float MIN_CAST_STRENGTH = 1f;
    Bobber bobber;
    public List<Fish.FishStats> fish = new List<Fish.FishStats>();

    PlayerState playerState = PlayerState.READY;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        fishbar.OnHit += () =>
        {
            bobber.lure.fish.health -= 1;
            if (bobber.lure.fish.health == 0)
            {
                fish.Add(bobber.lure.fish.fishStats);
                PullLineIn();
            }
        };
        fishbar.OnMiss += () =>
        {
            ReleaseFish();
        };
    }

    private void Update()
    {
        if (playerState == PlayerState.READY)
        {
            if (Keyboard.current.spaceKey.isPressed)
            {
                castStrength += Time.deltaTime;
            }
            if (Keyboard.current.spaceKey.wasReleasedThisFrame)
            {
                Cast();
                castStrength = MIN_CAST_STRENGTH;
            }
            if (castStrength > MAX_CAST_STRENGTH)
            {
                castStrength = MAX_CAST_STRENGTH;
            }
        } else if (playerState == PlayerState.CAST)
        {
            if (Keyboard.current.spaceKey.isPressed)
            {
                if (bobber)
                {
                    Vector3 dir = (bobber.transform.position - transform.position).normalized;
                    bobber.transform.position -= dir * Time.deltaTime;
                }
            } else
            {
                if (bobber)
                    if (Vector3.Distance(transform.position, bobber.transform.position) < 0.1f)
                        PullLineIn();
            }
        }

        if (bobber && bobber.lure && bobber.lure.bitten)
        {
            fishbar.gameObject.SetActive(true);
        } else
        {
            fishbar.gameObject.SetActive(false);
        }
    }

    void PullLineIn()
    {
        castStrength = MIN_CAST_STRENGTH;
        Destroy(bobber.gameObject);
        Invoke("ReadyToCast", 0.5f);
    }

    void ReleaseFish()
    {
        castStrength = MIN_CAST_STRENGTH;
        Destroy(bobber.gameObject);
        Invoke("ReadyToCast", 0.5f);
    }

    void ReadyToCast()
    {
        playerState = PlayerState.READY;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float s = 0.0f;
        float turn = 0.0f;
        if (Keyboard.current.wKey.isPressed)
        {
            s += speed;
        }
        if (Keyboard.current.sKey.isPressed)
        {
            s -= speed;
        }
        if (Keyboard.current.aKey.isPressed)
        {
            turn -= turningSpeed;
        }
        if (Keyboard.current.dKey.isPressed)
        {
            turn += turningSpeed;
        }

        rb.MovePosition(transform.position + transform.forward * s * Time.deltaTime);
        cameraOrigin.rotation = cameraOrigin.rotation * Quaternion.Euler(0f, turn * Time.deltaTime, 0f);
        rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, cameraOrigin.rotation, Time.deltaTime * s * turningSpeed));
    }

    public void Cast()
    {
        if (bobber)
            Destroy(bobber.gameObject);
        Vector3 dir = Flatten(cameraOrigin.forward).normalized + Vector3.up * Mathf.Tan(Mathf.PI / 6);
        bobber = Instantiate<Bobber>(bobberPrefab);
        bobber.transform.position = transform.position + Vector3.up * 0.25f;
        Rigidbody rb = bobber.GetComponent<Rigidbody>();
        rb.velocity = dir * castStrength * 2f;
        playerState = PlayerState.CAST;
    }
    Vector3 Flatten(Vector3 p) => new Vector3(p.x, 0f, p.z);
}
