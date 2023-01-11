using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public void Play()
    {
        StoryManager.singleton.PlayChapter(transform.name);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawIcon(transform.position, "olive.png", true);
    }
}
