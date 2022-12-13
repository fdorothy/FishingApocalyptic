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
        this.text.text = text;
        
        // dumb hack to force a resize event to fit to new text
        var elem = this.text.GetComponent<UnityEngine.UI.ContentSizeFitter>();
        elem.SetLayoutHorizontal();

        panel.gameObject.SetActive(true);
    }

    public void HideMessage()
    {
        panel.gameObject.SetActive(false);
    }
}
