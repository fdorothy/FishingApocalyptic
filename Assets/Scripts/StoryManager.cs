using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class StoryManager : MonoBehaviour
{
    public TextAsset inkJson;
    public static StoryManager singleton;
    public Story story;

    private void Awake()
    {
        story = new Story(inkJson.text);
        singleton = this;
    }

    public void PlayChapter(string chapter)
    {
        story.ChoosePathString(chapter);
        Continue();
    }

    public void Continue()
    {
        if (story.canContinue)
        {
            string text = story.Continue();
            Messages.singleton.SetMessage(text);
        }
    }

    public void Update()
    {
    }
}
