using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 各土台に付いている，サイズ変更用スクリプト.
/// </summary>
public class PedestalSizeSetter : MonoBehaviour
{
    public Slider widthSlider;
    public Slider heightSlider;
    public Transform bottomDrop;
    public bool ignoreHeight;

    private float minEtherSize = 116f; // 粒サイズの最小を50mmとした場合の，3段のEtherの高さ116mmを基準とする.
    private float defaultScaleXZ = 2.32f; // その場合の台座幅のScale.
    public BodyController bodyController;


    public void Rescale()
    {
        // 台座サイズも50mm刻みで変わる.
        var width = (int)widthSlider.value;
        var wSurplus = width % 50;
        if (wSurplus > 0)
            width = width - wSurplus;

        var etherHeight = Mathf.Max(minEtherSize, bodyController.totalHeight);
        // 設定したい台座幅からオブジェクトのScaleを算出する.
        // 166mm : 2.32 = 希望の台座サイズ : X
        var widthScale = (width * defaultScaleXZ) / etherHeight;

        var height = (int)heightSlider.value;
        var hSurplus = height % 50;
        if (hSurplus > 0)
        height = height - hSurplus;

        var heightScale = ignoreHeight ? 1f : (height * defaultScaleXZ) / etherHeight;
        var offset = ignoreHeight ? 0f : heightScale - 0.5f;

        gameObject.transform.localScale = new Vector3(widthScale, heightScale, widthScale);
        gameObject.transform.localPosition = new Vector3(0f, bottomDrop.localPosition.y - 0.18f - offset, 0f);
    }
}
