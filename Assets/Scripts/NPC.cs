using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public string chapter;

    public void Play()
    {
        StoryManager.singleton.PlayChapter(chapter);
    }
}
