using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToSelectScene : MonoBehaviour
{
    public void OnButtonPressed()
    {
        SceneManager.LoadScene("1_Select");
    }
}
