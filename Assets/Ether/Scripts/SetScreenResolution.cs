using UnityEngine;

public class SetScreenResolution : MonoBehaviour
{
    public int screenWidth = 416;
    public int screenHeight = 900;


    void Awake()
    {
        if (Application.platform == RuntimePlatform.WindowsPlayer ||
        Application.platform == RuntimePlatform.OSXPlayer ||
        Application.platform == RuntimePlatform.LinuxPlayer)
        {
            Screen.SetResolution(screenWidth, screenHeight, false);
        }
    }
}