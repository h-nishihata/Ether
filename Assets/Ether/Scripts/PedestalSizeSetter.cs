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
    public Transform bottomDrop;

    public StringBuilder infoText = new StringBuilder();
    public Text pedestalSizeInfo;


    public void Rescale()
    {
        // 現在設定されている一粒の高さ.
        var unitHeight = bodyController.unitHeight;

        // 設定したい台座の幅と奥行. 50mm刻みで変わる.
        width = (int)widthSlider.value;
        var wSurplus = width % 50;
        if (wSurplus > 0)
            width = width - wSurplus;

        // 台座オブジェクトのScaleを算出する.
        // Quad・Cylinderともに，スケールが1のときにSceneビュー上でEther一粒の高さと同じ幅・奥行になる.
        // よって，現在の一粒の高さ : 1 = 希望の台座幅 : 求めたいScale.
        var widthScale = (float)width / unitHeight;

        // 設定したい台座の高さ.
        height = (int)heightSlider.value;
        var hSurplus = height % 50;
        if (hSurplus > 0)
        height = height - hSurplus;

        // 同様に，台座の高さのScaleも設定する.
        // Cylinderはスケール1のときの高さがグリッド２個分なので注意.
        var heightScale = ignoreHeight ? 1f : (float)height / (unitHeight * 2f);
        gameObject.transform.localScale = new Vector3(widthScale, heightScale, widthScale);

        // 位置調整.
        var offset = ignoreHeight ? -0.3f : (heightScale - 0.5f) + 0.18f; // TO DO: マジックナンバーどうにかしたい.
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
