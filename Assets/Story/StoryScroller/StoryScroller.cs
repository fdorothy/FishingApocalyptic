using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class StoryScroller : MonoBehaviour
{
    public TMP_Text text_obj;//NOTICE TEXT MESH MOTHER FUCKING PRO GOD DAMN YOU MOTHER FUCKERS
    public AudioClip sfx_click;
    public float type_speed = 0.015f;
    public string filePathToRichTextDocument;

    AudioSource speaker;
    string[] lines;
    void Start()
    {
        speaker = this.gameObject.AddComponent<AudioSource>();
        string fileContent = File.ReadAllText(filePathToRichTextDocument);
        lines = fileContent.Split(new string[] { "\n", "\r\n" }, StringSplitOptions.None);
        StartCoroutine(ChapterOne());
    }

    //Typer types a string, one letter at a time to text_obj
    //waitAfter = optional; delay at very end.
    //callbackOnCompletion = optional; called after delay and at end.
    //Important note, uses bool isDone.
    //isDone is used by the routine that waits on Typer to finish
    bool isDone;
    IEnumerator Typer(string typeThis, float waitAfter = 0f, Func<bool> callbackOnCompletion = null)
    {
        isDone = false;

        while (typeThis != "")
        {
            text_obj.text += typeThis.Substring(0, 1);//consume the first character
            typeThis = typeThis.Substring(1);//then set TypeThis to everything After the 1st character
            if (sfx_click) speaker.PlayOneShot(sfx_click);
            yield return new WaitForSeconds(type_speed);
        }

        yield return new WaitForSeconds(waitAfter);
        if (callbackOnCompletion != null) callbackOnCompletion();
        isDone = true;
    }

    //A convenience method for calling Typer
    void TYPE(string toType, float waitAfter = 0f)
    {
        StartCoroutine(Typer(toType, waitAfter));//Coroutine will set isDone to true when is done
    }

    //types a variable number of newlines
    void NewLine(int howMany = 1, float waitAfter = 0f)
    {
        string s = "";
        for (int i = 0; i < howMany; i++)
        {
            s += "\n";
        }

        TYPE(s);
    }

    //clears the text screen
    void ClearScreen(float waitAfter = 0f)
    {
        text_obj.text = "";
    }

    //a string post-processor, to be used
    //after text_obj has been written to.
    //returns: the last X lines of a string, delimit is \n
    string GetXBottomLines(int x, string bigString)
    {
        string s = "";
        string[] allLines = bigString.Split("\n");
        Stack<String> stack = new Stack<string>(); ;
        for (int i = 0; i < x; i++)
        {
            if (i >= allLines.Length) break;

            string token = allLines[allLines.Length - i - 1];
            stack.Push(token);
        }

        foreach (string str in stack)
        {
            s += str + "\n";//add the newline BACK in, the split will have removed them
        }

        return s;
    }

    //-----------------------------------------------------------------------------------------------
    public IEnumerator PrintAllLinesRoutine()
    {
        ClearScreen();
        foreach (string line in lines)
        {
            TYPE(line, 0);
            yield return new WaitUntil(() => isDone == true);
            text_obj.text = GetXBottomLines(18, text_obj.text);
        }
    }


    public IEnumerator ChapterOne()
    {
        TYPE("Once...", 1f);
        yield return new WaitUntil(() => isDone == true);
        NewLine(2);
        yield return new WaitUntil(() => isDone == true);
        TYPE("Upon...", 1f);
        yield return new WaitUntil(() => isDone == true);
        NewLine(2);
        yield return new WaitUntil(() => isDone == true);
        TYPE("A Time....");
        yield return new WaitUntil(() => isDone == true);
        NewLine(2);
        yield return new WaitUntil(() => isDone == true);
    }
}
