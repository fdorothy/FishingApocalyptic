using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeMenu : MonoBehaviour
{
    Player player;
    Button gasButton;
    Button lureButton;
    Button speedButton;
    TMP_Text gasText;
    TMP_Text lureText;
    TMP_Text speedText;
    TMP_Text pointText;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();

        Transform t;
        t = transform.Find("UpgradeGas");
        gasButton = t.GetComponent<Button>();
        gasText = t.GetChild(0).GetComponent<TMP_Text>();

        t = transform.Find("UpgradeLure");
        lureButton = t.GetComponent<Button>();
        lureText = t.GetChild(0).GetComponent<TMP_Text>();

        t = transform.Find("UpgradeSpeed");
        speedButton = t.GetComponent<Button>();
        speedText = t.GetChild(0).GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        gasText.text = string.Format("Gas ({0})", player.playerStatistics.gas);
        gasButton.enabled = HasEnoughPoints(player.playerStatistics.gas);
        lureText.text = string.Format("Lure ({0})", player.playerStatistics.lure);
        lureButton.enabled = HasEnoughPoints(player.playerStatistics.lure);
        speedText.text = string.Format("Speed ({0})", player.playerStatistics.speed);
        speedButton.enabled = HasEnoughPoints(player.playerStatistics.speed);
    }

    public void OnUpgradeGas()
    {
        Player.PlayerStatistics s = player.playerStatistics;
        if (HasEnoughPoints(s.gas))
        {
            s.points -= s.gas;
            s.gas++;
            Debug.Log("upgraded gas" + s.gas);
        }
    }

    public void OnUpgradeLure()
    {
        Player.PlayerStatistics s = player.playerStatistics;
        if (HasEnoughPoints(s.lure))
        {
            s.points -= s.lure;
            s.lure++;
        }

    }

    public void OnUpgradeSpeed()
    {
        Player.PlayerStatistics s = player.playerStatistics;
        if (HasEnoughPoints(s.speed))
        {
            s.points -= s.speed;
            s.speed++;
        }
    }

    public void OnClose()
    {
        gameObject.SetActive(false);
    }

    public void OnOpen()
    {
        gameObject.SetActive(true);
    }

    bool HasEnoughPoints(int stat)
    {
        return player.playerStatistics.points >= stat;
    }
}
