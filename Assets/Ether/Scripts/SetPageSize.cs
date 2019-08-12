using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPageSize : MonoBehaviour
{
    public RectTransform sculpture;

    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.height);
        var value = Screen.width / SetScreenResolution.referenceWidth;
        sculpture.localScale = new Vector3(value, value, value);
    }
}
