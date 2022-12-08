using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Points : MonoBehaviour
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
            text.text = s.points.ToString();
        }
    }
}
