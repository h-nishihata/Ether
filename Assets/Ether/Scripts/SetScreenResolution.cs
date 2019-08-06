using UnityEngine;
using UnityEngine.UI;

public class SetScreenResolution : MonoBehaviour
{
    public int screenWidth = 397;
    public int screenHeight = 860;

    public TurnPage pageSwitcher;


    /// <summary>
    /// ビルドでの実行時に強制的に解像度を変更する.
    /// </summary>
    void Awake()
    {
        screenWidth = Screen.width;

        if (Application.platform == RuntimePlatform.WindowsPlayer ||
            Application.platform == RuntimePlatform.OSXPlayer ||
            Application.platform == RuntimePlatform.LinuxPlayer)
        {
            Screen.SetResolution(screenWidth, screenHeight, false);
        }

        Camera.main.orthographicSize = screenWidth;
        pageSwitcher.PageWidth = screenWidth;
    }
}