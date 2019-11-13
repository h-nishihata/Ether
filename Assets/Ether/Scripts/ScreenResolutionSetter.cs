using UnityEngine;

public class ScreenResolutionSetter : MonoBehaviour
{
    public int screenWidth = 2000;
    public int screenHeight = 2500;


    void Awake()
    {
        if (Application.platform == RuntimePlatform.WindowsPlayer ||
            Application.platform == RuntimePlatform.OSXPlayer ||
            Application.platform == RuntimePlatform.LinuxPlayer)
        {
            Screen.SetResolution(screenWidth, screenHeight, false); // フルスクリーン表示を無効に設定.
        }
    }
}