using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public int fishRequired = 0;
    StoryManager storyManager;
    Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = Player.singleton;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Messages.singleton.SetMessage("Fish Required: " + fishRequired);
            if (player.playerStatistics.points >= fishRequired)
            {
                gameObject.SetActive(false);
                player.playerStatistics.points -= fishRequired;
                Messages.singleton.HideMessage();
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Messages.singleton.HideMessage();
        }
    }
}
