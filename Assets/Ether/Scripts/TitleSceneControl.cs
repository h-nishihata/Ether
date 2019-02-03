using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneControl : MonoBehaviour {
   
	public Texture	TitleTexture = null;
	public SimpleSpriteGUI	title;
    private GUIStyle guiStyle;
    private GUIStyleState styleState;
    public string version = "Version_0.2.0";

	void Start ()
    {
		this.title = new SimpleSpriteGUI();
		this.title.create();
		this.title.setTexture(this.TitleTexture);

        guiStyle = new GUIStyle();
        guiStyle.fontSize = 48;
        styleState = new GUIStyleState();
        styleState.textColor = Color.black;
        guiStyle.normal = styleState;
	}

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("1_Select");
        }
    }

	void OnGUI()
	{
		this.title.draw();
        GUI.Label(new Rect(800, 2500, 200, 200), version, guiStyle);
	}
}
