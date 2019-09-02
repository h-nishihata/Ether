using UnityEngine;

public class SculptureScaleSetter : MonoBehaviour
{
    public RectTransform sculpture;
    float scale;

    void Start()
    {
        this.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.height);
        sculpture.sizeDelta = new Vector2(Screen.width, Screen.height);
        if (Screen.width != ScreenResolutionSetter.defaultWidth)
        {
            scale = (float)Screen.width / (float)ScreenResolutionSetter.defaultWidth;
            sculpture.localScale = new Vector3(scale, scale, scale);
        }
    }
}
