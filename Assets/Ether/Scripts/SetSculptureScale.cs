using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSculptureScale : MonoBehaviour
{
    public RectTransform sculpture;


    void Start()
    {
        this.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.height);
        var scale = Screen.width / SetScreenResolution.minimumWidth;
        sculpture.sizeDelta = new Vector2(Screen.width, Screen.height);
        //sculpture.localScale = new Vector3(scale, scale, scale);
    }
}
