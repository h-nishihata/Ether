using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// スライダーをドラッグしたとき，目盛上で最寄りの数値に合わせるためのスクリプト.
/// </summary>
public class SliderAssist : MonoBehaviour
{
    public Slider slider;
    private int lastSliderValue;

    public CSVReader csvReader;
    public Text numDropInfo;
    public AudioManager audioManager;


    void Start()
    {
        lastSliderValue = (int)slider.value;
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if ((int)slider.value == lastSliderValue)
                return;

            lastSliderValue = (int)slider.value;
            ToNearest();
        }
    }

    void ToNearest()
    {
        csvReader.OnValueChanged((int)slider.value); // 粒数を変更することを伝える.

        var numDrops = (int)slider.value;
        numDropInfo.text = "num drops: " + numDrops.ToString();
        audioManager.Play(0);
    }
}
