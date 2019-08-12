using UnityEngine;

/// <summary>
/// アプリ起動時にダイアログは出さず，指定した解像度で実行するようPlayer Settingsから設定できるはずだが，
/// おそらくProjectSettings.assetに前回起動時の情報が残っているせいで反映されない.
/// 仕方ないので，Game画面で設定した解像度に基づいて，強制的に解像度を変更する.
/// </summary>
public class SetScreenResolution : MonoBehaviour
{
    private int screenWidth;
    private int screenHeight;
    public static int defaultWidth = 1242;

    public TurnPage pageSwitcher;

    void Awake()
    {
        screenWidth = Screen.width;
        screenHeight = Screen.height;

        if (Application.platform == RuntimePlatform.WindowsPlayer ||
            Application.platform == RuntimePlatform.OSXPlayer ||
            Application.platform == RuntimePlatform.LinuxPlayer)
        {
            Screen.SetResolution(screenWidth, screenHeight, false); // フルスクリーン表示を無効に設定.
        }

        Camera.main.orthographicSize = screenWidth;
        pageSwitcher.PageWidth = screenWidth;
    }
}