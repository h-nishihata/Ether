using UnityEngine;
using System.Linq;

/// <summary>
/// スライダーをドラッグしたとき，目盛上で最寄りの数値に合わせるためのスクリプト.
/// </summary>
public class SliderAssist : MonoBehaviour
{
    private RectTransform handlePosition;
    public  RectTransform fill;
    public Vector2[] fixedPos;
    private Vector2 currentPos;


    void Start()
    {
        handlePosition = this.GetComponent<RectTransform>();
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            currentPos = handlePosition.anchorMin;
        }
        else
        {
            ToNearest();
        }

    }

    void ToNearest()
    {
        var nearest = fixedPos.OrderBy(x => Mathf.Abs(x.x - currentPos.x)).First();
        handlePosition.anchorMax = fill.anchorMax = nearest;
        handlePosition.anchorMin = nearest * Vector2.right; // yの値だけ0にして使用する.
    }
}
