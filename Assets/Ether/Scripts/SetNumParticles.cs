using UnityEngine;
using UnityEngine.SceneManagement;

public class Data
{
    public readonly static Data Instance = new Data();

    public int numPages;
    public int numBoxes;
    public int csvInitLine;
}

public class SetNumParticles : MonoBehaviour
{
    private int numParticles;


    public void OnValueChanged(int dropDownValue)
    {
        numParticles = dropDownValue;
    }

    public void OnButtonPressed()
    {
        switch (numParticles)
        {
            case 0: // 4 particles
                Data.Instance.numPages = 2;
                Data.Instance.numBoxes = 2;
                Data.Instance.csvInitLine = 1;
                break;
            case 1: // 5 particles
                Data.Instance.numPages = 6;
                Data.Instance.numBoxes = 3;
                Data.Instance.csvInitLine = 4;
                break;
            case 2: // 6 particles
                Data.Instance.numPages = 24;
                Data.Instance.numBoxes = 4;
                Data.Instance.csvInitLine = 11;
                break;
        }

        SceneManager.LoadScene("2_Swipe");
    }
}
