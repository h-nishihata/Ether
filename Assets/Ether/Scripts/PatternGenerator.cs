using UnityEngine;
using UnityEngine.UI;


public class PatternGenerator : MonoBehaviour
{
    public Material[] mats;
    public Slider[] sliders;

    //public ColorPicker picker;
    public Button[] buttons;

    public Color lineColor = Color.black;
    public Color bgColor = Color.white;
    public bool isLineColor;

    void Start()
    {
        isLineColor = true;
    }

    void Update()
    {
        // パターン作成.
        mats[0].SetFloat("_Frequency", sliders[0].value);
        mats[0].SetFloat("_Fill", sliders[1].value);
        mats[0].SetColor("_LineColor", lineColor);
        mats[0].SetColor("_BGColor", bgColor);

        // マスク作成.
        for (int i = 1; i < mats.Length; i++)
        {
            mats[i].SetFloat("_Frequency", sliders[2].value);
            mats[i].SetFloat("_Fill", sliders[3].value);
            mats[i].SetColor("_LineColor", lineColor);
            mats[i].SetColor("_BGColor", bgColor);
        }
    }

    public void switchLineAndBG(bool lineButtonPressed)
    {
        buttons[0].interactable = !lineButtonPressed;
        buttons[1].interactable = lineButtonPressed;
        isLineColor = lineButtonPressed;
    }
}