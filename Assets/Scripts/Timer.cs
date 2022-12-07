using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    TMPro.TMP_Text text;
    public bool paused = false;

    public System.Action OnTimer;
    Player player;

    public void Start()
    {
        player = FindObjectOfType<Player>();
        text = GetComponent<TMPro.TMP_Text>();
    }

    public void Update()
    {
        if (player.gas < 0f)
        {
            player.gas = 0f;
            OnTimer.Invoke();
        }
        text.text = System.String.Format("{0}", (int)player.gas);
    }
}
