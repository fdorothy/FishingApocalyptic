using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    TMPro.TMP_Text text;
    float timer = 30f;
    public bool paused = false;

    public System.Action OnTimer;

    public void Start()
    {
        text = GetComponent<TMPro.TMP_Text>();
    }

    public void Update()
    {
        if (!paused)
            timer -= Time.deltaTime;
        if (timer < 0f)
        {
            timer = 0f;
            paused = true;
            OnTimer.Invoke();
        }
        text.text = System.String.Format("{0}", (int)timer);
    }
    public void SetTimer(float value)
    {
        timer = value;
    }
}
