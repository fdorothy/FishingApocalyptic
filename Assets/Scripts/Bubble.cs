using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    public enum FishType
    {
        NORMAL,
        KOI
    }

    public FishType fishType;

    void OnCatch()
    {
        if (fishType == FishType.KOI)
        {
            StoryManager.singleton.PlayChapter("FindFish");
            FindObjectOfType<Player>().SetPlayerState(Player.PlayerState.DIALOGUE);
            Debug.Log("playing chapter findfish");
        }
        Destroy(gameObject);
    }
}
