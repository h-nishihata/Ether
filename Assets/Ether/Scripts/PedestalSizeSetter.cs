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
        var surplus = width % 50;
        if (surplus > 0)
            width = width - surplus;

        var etherHeight = Mathf.Max(minEtherSize, bodyController.totalHeight);
        Debug.Log("etherHeight :" + etherHeight);
        // 設定したい台座幅からオブジェクトのScaleを算出する.
        // 166mm : 2.32 = 希望の台座サイズ : X
        var widthScale = (width * defaultScaleXZ) / etherHeight;

        //var hightValue = (int)heightSlider.value;
        //surplus = hightValue % 50;
        //if (surplus > 0)
        //hightValue = hightValue - surplus;

        var height = ignoreHeight ? 1f : heightSlider.value;
        var offset = ignoreHeight ? 0f : height - 0.5f;

        gameObject.transform.localScale = new Vector3(widthScale, height, widthScale);
        gameObject.transform.localPosition = new Vector3(0f, bottomDrop.localPosition.y - 0.18f - offset, 0f);
    }
}
