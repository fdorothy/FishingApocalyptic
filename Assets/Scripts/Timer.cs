using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Image gasImage;
    public bool paused = false;

    public System.Action OnTimer;
    Player player;

    public void Start()
    {
        player = FindObjectOfType<Player>();
    }

    public void Update()
    {
        if (player.gas < 0f)
        {
            player.gas = 0f;
            OnTimer.Invoke();
        }
        gasImage.fillAmount = (float)player.gas / player.maxGas;
        gasImage.fillMethod = Image.FillMethod.Horizontal;
    }
}
