using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Messages : MonoBehaviour
{
    public static Messages singleton;

    public TMPro.TMP_Text text;
    public Transform panel;

    public void Awake()
    {
        singleton = this;
        HideMessage();
    }

    public void SetMessage(string text)
    {
        panel.gameObject.SetActive(true);
        this.text.text = text;
    }

    public void HideMessage()
    {
        panel.gameObject.SetActive(false);
    }
}
