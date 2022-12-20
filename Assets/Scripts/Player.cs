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
        DOCKED,
        NPC,
        DIALOGUE
    }

    public PlayerStatistics playerStatistics;
    public float speed = 1.0f;
    public float turningSpeed = 90f;
    public Transform cameraOrigin;
    public Bobber bobberPrefab;
    public Fishbar fishbar;
    public float maxCastStrength = 5f;
    public float minCastStrength = 1f;
    public float gas = 0f;
    public LineRenderer lr;
    UpgradeMenu upgradeMenu;
    NPC currentNPC;
    public ParticleSystem wakeSystem;

    public class PlayerStatistics
    {
        public int gas;
        public int lure;
        public int speed;
        public int points;
    }

    public float maxSpeed
    {
        get {
            switch (playerStatistics.speed)
            {
                case 1: return 0.5f;
                case 2: return 0.6f;
                case 3: return 0.7f;
                case 4: return 0.8f;
                case 5: return 0.9f;
                case 6: return 1.0f;
                default: return 0.5f;
            }
        }
    }

    public float fishProbability
    {
        get
        {
            switch (playerStatistics.lure)
            {
                case 1: return 0.0f;
                case 2: return 0.1f;
                case 3: return 0.2f;
                case 4: return 0.3f;
                case 5: return 0.4f;
                case 6: return 0.5f;
                default: return 0.0f;
            }
        }
    }

    public float maxGas
    {
        get
        {
            switch (playerStatistics.gas)
            {
                case 1: return 15f;
                case 2: return 20f;
                case 3: return 25f;
                case 4: return 30f;
                case 5: return 45f;
                case 6: return 50f;
                default: return 15f;
            }
        }
    }

    Rigidbody rb;
    float castStrength;
    Bobber bobber;
    Dock dock;
    Timer timer;
    Vector3 startPosition;
    Quaternion startRotation;
    Coroutine fishingRoutine;
    int fishPoints = 0;
    PlayerState playerState = PlayerState.READY;

    // Start is called before the first frame update
    void Start()
    {
        upgradeMenu = FindObjectOfType<UpgradeMenu>();
        upgradeMenu.OnClose();
        wakeSystem.Stop();
        playerStatistics = new PlayerStatistics()
        {
            gas=1,
            lure=1,
            speed=1,
            points=0
        };
        gas = maxGas;
        startPosition = transform.position;
        startRotation = transform.rotation;
        timer = FindObjectOfType<Timer>();
        timer.OnTimer += () =>
        {
            transform.position = startPosition;
            transform.rotation = startRotation;
            PullLineIn(false);
            Debug.Log("setting state to docked");
            playerState = PlayerState.DOCKED;
            gas = maxGas;
        };
        castStrength = minCastStrength;
        rb = GetComponent<Rigidbody>();
        fishbar.OnHit += () =>
        {
            Debug.Log("hit!");
            playerStatistics.points += fishPoints;
            PullLineIn();
        };
        fishbar.OnMiss += () =>
        {
            ReleaseFish();
        };
    }

    /**
     * Routine that handles when we get a bite
     */
    public IEnumerator FishingRoutine()
    {
        yield return new WaitForSeconds(1.0f);
        while (bobber)
        {
            if (playerState == PlayerState.CAST)
            {
                float p = Random.Range(0.0f, 1.0f);
                float fishProb = fishProbability;
                if (bobber && bobber.inBubbles)
                    fishProb += 0.5f;
                Debug.Log("fish prob = " + fishProb);
                if (p < fishProb && bobber)
                {
                    fishPoints = 1;
                    Bite();
                } else if (p < 0.8f)
                {
                    fishPoints = 0;
                    Bite();
                }
            }
            yield return new WaitForSeconds(1.0f);
        }
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
            case PlayerState.DOCKED:
                UpdateDocked();
                break;
            case PlayerState.NPC:
                UpdateNPC();
                break;
            case PlayerState.DIALOGUE:
                UpdateDialogue();
                break;
        }

        if (bobber && bobber.Bitten)
        {
            if (!fishbar.gameObject.activeSelf)
            {
                fishbar.SetTargetSize(0.5f);
                fishbar.gameObject.SetActive(true);
            }
        } else
        {
            if (fishbar.gameObject.activeSelf)
            {
                fishbar.gameObject.SetActive(false);
            }
        }
        UpdateLine();
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

    void UpdateDocked()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
            upgradeMenu.gameObject.SetActive(!upgradeMenu.gameObject.activeSelf);
        gas = maxGas;
    }

    void UpdateNPC()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            currentNPC.Play();
            PullLineIn(false);
            playerState = PlayerState.DIALOGUE;
        }
    }
    void UpdateDialogue()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            if (!StoryManager.singleton.story.canContinue)
            {
                Invoke("ReadyToCast", 0.5f);
                Messages.singleton.HideMessage();
            } else
            {
                StoryManager.singleton.Continue();
            }
        }
    }

    void UpdateLine()
    {
        if (bobber)
        {
            lr.gameObject.SetActive(true);
            lr.SetPosition(0, transform.position + Vector3.up * 0.1f);
            lr.SetPosition(1, bobber.transform.position + Vector3.up * 0.05f);
        }
        else
        {
            lr.gameObject.SetActive(false);
        }
    }

    void PullLineIn(bool readyToCast = true)
    {
        castStrength = minCastStrength;
        if (bobber)
            Destroy(bobber.gameObject);
        fishbar.gameObject.SetActive(false);
        if (readyToCast)
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
        if (playerState != PlayerState.DOCKED)
            playerState = PlayerState.READY;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float s = 0.0f;
        float turn = 0.0f;
        if (Keyboard.current.wKey.isPressed)
        {
            s += maxSpeed;
        }
        if (Keyboard.current.sKey.isPressed)
        {
            s -= maxSpeed;
        }
        if (Keyboard.current.aKey.isPressed)
        {
            turn -= turningSpeed;
        }
        if (Keyboard.current.dKey.isPressed)
        {
            turn += turningSpeed;
        }

        if (Mathf.Abs(speed) > 0.1f)
        {
            if (!wakeSystem.isPlaying)
                wakeSystem.Play();
        }
        else
            wakeSystem.Stop();

        speed = Mathf.Lerp(speed, s, Time.deltaTime);
        rb.velocity = Vector3.zero;
        rb.MovePosition(transform.position + transform.forward * speed * Time.deltaTime);
        if (s != 0.0f)
            gas -= Time.deltaTime;
        cameraOrigin.rotation = cameraOrigin.rotation * Quaternion.Euler(0f, turn * Time.deltaTime, 0f);
        rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, cameraOrigin.rotation, Time.deltaTime * Mathf.Abs(s) * turningSpeed));
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
        StartFishing();
    }

    public void Bite()
    {
        playerState = PlayerState.BITE;
        bobber.Bite();
        StopFishing();
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
            timer.paused = true;
            if (bobber)
                Destroy(bobber.gameObject);
            StopFishing();
        }
        if (other.gameObject.tag == "NPC")
        {
            Messages.singleton.SetMessage("<spacebar>");
            currentNPC = other.gameObject.GetComponent<NPC>();
            playerState = PlayerState.NPC;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Dock")
        {
            Debug.Log("leaving dock");
            playerState = PlayerState.READY;
            Messages.singleton.HideMessage();
            timer.paused = false;
            upgradeMenu.gameObject.SetActive(false);
        }
        if (other.gameObject.tag == "NPC")
        {
            playerState = PlayerState.READY;
            Messages.singleton.HideMessage();
        }
    }

    void StopFishing()
    {
        if (fishingRoutine != null)
            StopCoroutine(fishingRoutine);
        fishingRoutine = null;
    }

    void StartFishing()
    {
        StopFishing();
        fishingRoutine = StartCoroutine(FishingRoutine());
    }

    Vector3 Flatten(Vector3 p) => new Vector3(p.x, 0f, p.z);
}
