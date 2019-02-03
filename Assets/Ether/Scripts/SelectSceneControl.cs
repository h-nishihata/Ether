using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectSceneControl : MonoBehaviour {
   

	void Start ()
    {
	}

    void Update()
    {

    }

	public void SwitchScene()
	{
        SceneManager.LoadScene("2_Swipe");
    }
}
