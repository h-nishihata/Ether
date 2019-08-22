using System.Linq;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// スライダーをドラッグしたとき，目盛上で最寄りの数値に合わせるためのスクリプト.
/// </summary>
public class SliderAssist : MonoBehaviour
{
    public Slider slider;
    private float lastSliderValue;
    private RectTransform handlePosition;
    public  RectTransform fill;
    private Vector2[] fixedPos = new Vector2[11];
    private Vector2 currentPos;

    public CSVReader csvReader;
    public Text info;
    public AudioManager audioManager;


    void Start()
    {
        for (int i = 0; i < fixedPos.Length; i++)
        {
            fixedPos[i] = new Vector2(i * 0.1f, 1f);
        }
        handlePosition = this.GetComponent<RectTransform>();
        lastSliderValue = slider.value;
        SetInfo(0f);
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            currentPos = handlePosition.anchorMin;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (slider.value == lastSliderValue)
                return;

            lastSliderValue = slider.value;
            ToNearest();;
        }
    }

    void ToNearest()
    {
        var nearest = fixedPos.OrderBy(x => Mathf.Abs(x.x - currentPos.x)).First();
        csvReader.OnValueChanged(nearest.x); // 粒数を変更することを伝える.
        // ハンドル位置を変更する.
        handlePosition.anchorMax = fill.anchorMax = nearest;
        handlePosition.anchorMin = nearest * Vector2.right; // yの値だけ0にして使用する.

        SetInfo(nearest.x);
        audioManager.Play(0);
    }

    void SetInfo(float nearestValue)
    {
        var numDrops = (int)(nearestValue * 10 + 3);
        info.text = "num drops: " + numDrops.ToString();
    }
}
