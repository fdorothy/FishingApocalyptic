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

    public Lure lurePrefab;
    public Lure lure;

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
                        lure = Instantiate(lurePrefab);
                        lure.bobber = this;
                        lure.transform.position = transform.position - Vector3.up * 0.5f;
                        lure.transform.SetParent(transform);
                    }
                } break;
            case BobberStateEnum.WAIT:
                {
                } break;
        }
    }

    public void Bite(Fish fish)
    {
        // bob the bobber up and down to indicate a bite
        transform.DOMoveY(-0.1f, 0.1f).SetLoops(-1, LoopType.Yoyo);
        state = BobberStateEnum.BITE;
    }
}
