using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public enum PlayerState
    {
        READY,
        CAST,
        BITE,
        DOCKED
    }

    public float speed = 1.0f;
    public float turningSpeed = 90f;
    public Transform cameraOrigin;
    public Bobber bobberPrefab;
    public Fishbar fishbar;
    public float maxCastStrength = 5f;
    public float minCastStrength = 1f;
    public List<Fish.FishStats> fish = new List<Fish.FishStats>();


    Rigidbody rb;
    float castStrength;
    Bobber bobber;
    Dock dock;

    PlayerState playerState = PlayerState.READY;

    // Start is called before the first frame update
    void Start()
    {
        castStrength = minCastStrength;
        rb = GetComponent<Rigidbody>();
        fishbar.OnHit += () =>
        {
            bobber.lure.fish.health -= 1;
            if (bobber.lure.fish.health == 0)
            {
                fish.Add(bobber.lure.fish.fishStats);
                Destroy(bobber.lure.fish.gameObject);
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
        switch (playerState)
        {
            case PlayerState.READY:
                UpdateReady();
                break;
            case PlayerState.CAST:
                UpdateCast();
                break;
            case PlayerState.BITE:
                UpdateBite();
                break;
        }

        if (bobber && bobber.lure && bobber.lure.bitten)
        {
            if (!fishbar.gameObject.activeSelf)
            {
                fishbar.SetTargetSize(bobber.lure.fish.fishStats.targetSize);
                fishbar.gameObject.SetActive(true);
            }
        } else
        {
            if (fishbar.gameObject.activeSelf)
            {
                fishbar.gameObject.SetActive(false);
            }
        }
    }

    void UpdateReady()
    {
        if (Keyboard.current.spaceKey.isPressed)
        {
            castStrength += Time.deltaTime;
        }
        if (Keyboard.current.spaceKey.wasReleasedThisFrame)
        {
            Cast();
            castStrength = minCastStrength;
        }
        if (castStrength > maxCastStrength)
        {
            castStrength = maxCastStrength;
        }
    }

    void UpdateCast()
    {
        if (Keyboard.current.spaceKey.isPressed)
        {
            if (bobber)
            {
                Vector3 dir = (bobber.transform.position - transform.position).normalized;
                bobber.transform.position -= dir * Time.deltaTime;
            }
        }
        else
        {
            if (bobber)
                if (Vector3.Distance(transform.position, bobber.transform.position) < 0.1f)
                    PullLineIn();
        }
    }

    void UpdateBite()
    {
        if (UnityEngine.InputSystem.Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            fishbar.HitSpace();
        }
    }

    void PullLineIn()
    {
        castStrength = minCastStrength;
        Destroy(bobber.gameObject);
        Invoke("ReadyToCast", 0.5f);
    }

    void ReleaseFish()
    {
        castStrength = minCastStrength;
        bobber.ReleaseFish();
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

    public void Bite(Fish fish)
    {
        playerState = PlayerState.BITE;
        bobber.Bite(fish);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Dock")
        {
            Debug.Log("at the dock");
            playerState = PlayerState.DOCKED;
            castStrength = minCastStrength;
            dock = other.GetComponent<Dock>();
            Messages.singleton.SetMessage("<spacebar>");
            if (bobber)
                Destroy(bobber.gameObject);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Dock")
        {
            Debug.Log("leaving dock");
            playerState = PlayerState.READY;
            Messages.singleton.HideMessage();
        }
    }

    Vector3 Flatten(Vector3 p) => new Vector3(p.x, 0f, p.z);
}
