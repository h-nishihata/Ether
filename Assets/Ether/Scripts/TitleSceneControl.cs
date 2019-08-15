using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneControl : MonoBehaviour {

    private int screenWidth;
    private int screenHeight;
    private GUIStyle guiStyle;
    public string version = "Ver_1.0.0";

    void Awake()
    {
        screenWidth = Screen.width;
        screenHeight = Screen.height;

        if (Application.platform == RuntimePlatform.WindowsPlayer ||
            Application.platform == RuntimePlatform.OSXPlayer ||
            Application.platform == RuntimePlatform.LinuxPlayer)
        {
            Screen.SetResolution(screenWidth, screenHeight, false);
        }
    }

    void Start ()
    {
        guiStyle = new GUIStyle();
        guiStyle.fontSize = (int)(screenWidth * 0.03f);
        guiStyle.normal.textColor = Color.cyan;
	}

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            SceneManager.LoadScene("Main");
    }

	void OnGUI()
	{
        GUI.Label(new Rect(100, Screen.height - 100, 100, 100), version, guiStyle);
	}
}
