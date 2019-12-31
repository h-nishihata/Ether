using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 各土台に付いている，サイズ変更用スクリプト.
/// </summary>
public class PedestalSizeSetter : MonoBehaviour
{
    public Slider widthSlider;
    public Slider heightSlider;
    public bool ignoreHeight; // Quad型の土台は高さを考慮しなくて良い.

    public BodyController bodyController;
    private float minEtherSize = 116f; // 粒サイズの最小を50mmとした場合の，3段のEtherの高さ116mmを基準とする.
    private float defaultScaleXZ = 2.32f; // その場合の台座幅のScale.

    public Transform bottomDrop;


    public void Rescale()
    {
        // 台座の幅と奥行. 50mm刻みで変わる.
        var width = (int)widthSlider.value;
        var wSurplus = width % 50;
        if (wSurplus > 0)
            width = width - wSurplus;

        // 設定したい台座幅と，現在の設定で想定されている彫刻の高さからオブジェクトのScaleを算出する.
        // 画面上でのEtherの大きさは変わらないが，人の大きさを変えることによって，それに準じた高さだとみなしている.
        var etherHeight = Mathf.Max(minEtherSize, bodyController.etherHeight);
        // 基準となる3段のEtherの高さ : 台座の幅をそれに揃えたときのScale = 希望の台座サイズ : 求めたいScale.
        var widthScale = (width * defaultScaleXZ) / etherHeight;

        // 台座の高さ.
        var height = (int)heightSlider.value;
        var hSurplus = height % 50;
        if (hSurplus > 0)
        height = height - hSurplus;

        // 台座の高さのScale設定.
        var heightScale = ignoreHeight ? 1f : (height * defaultScaleXZ) / etherHeight;
        var offset = ignoreHeight ? -0.3f : (heightScale - 0.5f) + 0.18f;

        gameObject.transform.localScale = new Vector3(widthScale, heightScale, widthScale);
        gameObject.transform.localPosition = new Vector3(0f, bottomDrop.localPosition.y - offset, 0f);
    }
}
