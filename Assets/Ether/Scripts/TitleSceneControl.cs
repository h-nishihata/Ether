using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneControl : MonoBehaviour {

    private int screenWidth;
    private int screenHeight;
    private GUIStyle guiStyle;
    public string version = "Ver_1.0.0";
    //public Transform ether;
    //private float rotateVal;

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
        guiStyle.fontSize = (int)(screenWidth * 0.02f);
        guiStyle.normal.textColor = Color.white;
	}

    void Update()
    {
        //rotateVal += Time.deltaTime * 20f;
        //ether.transform.rotation = Quaternion.AngleAxis(rotateVal, Vector3.right);

        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("Main");
        }
    }

	void OnGUI()
	{
        GUI.Label(new Rect(Screen.width - 200, Screen.height - 100, 200, 100), version, guiStyle);
	}
}
