using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Bobber : MonoBehaviour
{
    public enum BobberStateEnum
    {
        CAST,
        WAIT,
        BITE,
        REEL
    }

    BobberStateEnum state = BobberStateEnum.CAST;

    Rigidbody rb;
    Tween activeTween;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case BobberStateEnum.CAST:
                {
                    if (transform.position.y < 0.0f)
                    {
                        state = BobberStateEnum.WAIT;
                        rb.velocity = Vector3.zero;
                        transform.position = new Vector3(transform.position.x, 0.0f, transform.position.z);
                        rb.isKinematic = true;
                    }
                } break;
            case BobberStateEnum.WAIT:
                {
                } break;
        }
    }

    public bool Bitten => state == BobberStateEnum.BITE;

    public void Bite()
    {
        // bob the bobber up and down to indicate a bite
        StopBobbing();
        activeTween = transform.DOMoveY(-0.05f, 0.5f).SetLoops(-1, LoopType.Yoyo);
        state = BobberStateEnum.BITE;
    }

    public void ReleaseFish()
    {
        StopBobbing();
    }

    public void StopBobbing()
    {
        if (activeTween != null)
        {
            activeTween.Kill(false);
            transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
        }
    }
}
