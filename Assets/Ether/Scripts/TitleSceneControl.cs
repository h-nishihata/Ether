using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneControl : MonoBehaviour {
   
	public Texture	TitleTexture = null;
	public SimpleSpriteGUI	title;


	void Start ()
    {
		this.title = new SimpleSpriteGUI();
		this.title.create();
		this.title.setTexture(this.TitleTexture);
	}

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("Select");
        }
    }

	void OnGUI()
	{
		this.title.draw();
	}
}
