using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fishbar : MonoBehaviour
{
    public Transform min, max;
    public LineRenderer hitLine;
    public Transform cursor;
    float cursorT = 0.0f, hitLineMinT = 0.25f, hitLineMaxT = 0.75f;
    bool cursorDir = false;
    float cursorSpeed = 1.0f;
    public int reverseDirectionCounter = 3;
    const float hitCooldownMax = 1.0f;
    float hitCooldown = 0.0f;

    public System.Action OnHit;
    public System.Action OnMiss;

    private void Start()
    {
        SetTargetSize(0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        hitLine.SetPosition(0, at(hitLineMinT));
        hitLine.SetPosition(1, at(hitLineMaxT));
        cursor.position = at(cursorT);

        if (hitCooldown > 0.0f)
        {
            hitCooldown -= Time.deltaTime;
            return;
        }

        cursorT = cursorT + (cursorDir ? Time.deltaTime : -Time.deltaTime);
        if (cursorT < 0.0f || cursorT > 1.0f)
        {
            cursorDir = !cursorDir;
            reverseDirectionCounter--;
            if (reverseDirectionCounter <= 0)
            {
                OnMiss.Invoke();
            }
        }
        cursorT = Mathf.Clamp(cursorT, 0.0f, 1.0f);
    }

    public void HitSpace()
    {
        if (cursorT <= hitLineMaxT && cursorT >= hitLineMinT)
        {
            OnHit.Invoke();
            cursorT = 0.0f;
            cursorDir = true;
            reverseDirectionCounter = 3;
            hitCooldown = hitCooldownMax;
        }
        else
        {
            OnMiss.Invoke();
        }
    }

    public void SetTargetSize(float size)
    {
        hitLineMinT = (1.0f - size) / 2.0f;
        hitLineMaxT = 1.0f - (1.0f - size) / 2.0f;
        cursorT = 0f;
        cursorDir = true;
        reverseDirectionCounter = 3;
    }

    Vector3 at(float t)
    {
        return (1 - t) * min.position + t * max.position;
    }
}
