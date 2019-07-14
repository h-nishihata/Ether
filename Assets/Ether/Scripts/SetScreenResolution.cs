using UnityEngine;
using System.Collections;
public class SetScreenResolution : MonoBehaviour
{
    public int ScreenWidth = 416;
    public int ScreenHeight = 900;

    void Awake()
    {
        if (Application.platform == RuntimePlatform.WindowsPlayer ||
        Application.platform == RuntimePlatform.OSXPlayer ||
        Application.platform == RuntimePlatform.LinuxPlayer)
        {
            Screen.SetResolution(ScreenWidth, ScreenHeight, false);
        }
    }
}