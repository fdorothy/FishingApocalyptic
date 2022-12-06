using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statistics : MonoBehaviour
{
    Player player;
    TMPro.TMP_Text text;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        text = GetComponent<TMPro.TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player && text)
        {
            Player.PlayerStatistics s = player.playerStatistics;
            text.text = string.Format("Points: {0}\nGas: {1}\nLure: {2}\nSpeed: {3}", s.points, s.gas, s.lure, s.speed);
        }
    }
}
