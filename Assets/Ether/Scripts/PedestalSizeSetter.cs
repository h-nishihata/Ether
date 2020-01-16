using System;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 各土台に付いている，サイズ変更用スクリプト.
/// </summary>
public class PedestalSizeSetter : MonoBehaviour
{
    public Slider widthSlider, heightSlider;
    private int width, height;
    public bool ignoreHeight; // Quad型の土台は高さを考慮しなくて良い.

    public BodyController bodyController;
    private float minEtherSize = 116f; // 粒サイズの最小を50mmとした場合の，3段のEtherの高さ116mmを基準とする.
    private float defaultScaleXZ = 2.32f; // その場合の台座幅のScale.
    public Transform bottomDrop;

    public StringBuilder infoText = new StringBuilder();
    public Text pedestalSizeInfo;


    public void Rescale()
    {
        // 台座の幅と奥行. 50mm刻みで変わる.
        width = (int)widthSlider.value;
        var wSurplus = width % 50;
        if (wSurplus > 0)
            width = width - wSurplus;

        // 設定したい台座幅と，現在の設定で想定されている彫刻の高さからオブジェクトのScaleを算出する.
        //var etherHeight = Mathf.Max(minEtherSize, bodyController.etherHeight);
        var unitHeight = bodyController.unitHeight;
        // 基準となる3段のEtherの高さ : 台座の幅をそれに揃えたときのScale = 希望の台座サイズ : 求めたいScale.
        //var widthScale = (width * defaultScaleXZ) / etherHeight;
        var widthScale = (float)width / unitHeight;


        // 台座の高さ.
        height = (int)heightSlider.value;
        var hSurplus = height % 50;
        if (hSurplus > 0)
        height = height - hSurplus;

        // 台座の高さのScale設定.
        //var heightScale = ignoreHeight ? 1f : (height * defaultScaleXZ) / etherHeight;
        var heightScale = ignoreHeight ? 1f : (float)height / unitHeight;
        var offset = ignoreHeight ? -0.3f : (heightScale - 0.5f) + 0.18f;

        gameObject.transform.localScale = new Vector3(widthScale, heightScale, widthScale);
        gameObject.transform.localPosition = new Vector3(0f, bottomDrop.localPosition.y - offset, 0f);

        this.UpdateInfo();
    }

    public void UpdateInfo()
    {
        infoText.Append("Pedestal Size: " + "\n");
        if (BodyController.activePedestalID > 0) // いずれかの台座が表示されていれば.
            infoText.Append("W " + width.ToString() + " mm" + "\n");
        if (BodyController.activePedestalID == 1) // Cylinder型の場合は高さも表示する.
            infoText.Append("H " + height.ToString() + " mm");

        pedestalSizeInfo.text = infoText.ToString();
        infoText.Clear();
    }
}
