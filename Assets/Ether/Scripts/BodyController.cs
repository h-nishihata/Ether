using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UIのBody Toggleに付いている，人型の有無を切り替えるスクリプト. 
/// 画面上でのEtherの大きさは変えず，想定された彫刻の高さに準じて人の側のスケールを変更している.
/// </summary>
public class BodyController : MonoBehaviour
{
    public Transform body;
    private Toggle toggle;

    public Slider etherSizeSlider; // 粒の高さを変更するスライダー.
    private float maxDropHeight = 1000f;
    public int unitHeight; // 一粒の高さ.
    private int zPos;
    public Text etherHeightInfo;

    public Transform[] grounds;
    public static int activePedestalID;
    public PedestalSizeSetter[] pedestalSizes;

    public DropNumSwitcher dropNumSwitcher;


    private void Start()
    {
        toggle = this.gameObject.GetComponent<Toggle>();
        this.Rescale();
    }

    /// <summary>
    /// 人型の有無を切り替える.
    /// </summary>
    public void EnableBody()
    {
        body.gameObject.SetActive(toggle.isOn);
        toggle.targetGraphic.color = toggle.isOn ? Color.cyan : Color.white;
    }

    /// <summary>
    /// 接地面の位置を調整する.
    /// </summary>
    public void AdjustGroundLevel()
    {
        var yPos = grounds[activePedestalID].transform.position.y;
        body.transform.localPosition = new Vector3(-3f, yPos, zPos);
    }

    public void Rescale()
    {
        // 一粒の高さが50mm刻みで変わる.
        unitHeight = (int)etherSizeSlider.value;
        var surplus = unitHeight % 50;
        if (surplus > 0)
            unitHeight = unitHeight - surplus;

        this.UpdateInfo();

        // 【粒の高さに合わせて】人型の大きさを設定する.
        // N.B. 段数が変わるだけなら，ただ積み上がっていくだけで粒の高さは変わらないので，人の大きさは変える必要はない.
        var scale = maxDropHeight / unitHeight;
        zPos = (int)scale; // scaleが大きくなると土台にめり込んでしまうので，奥に移動する.
        body.transform.localScale = new Vector3(scale, scale, scale);

        // 彫刻のサイズに合わせて土台の大きさを変更.
        for (int i = 0; i < pedestalSizes.Length; i++)
            pedestalSizes[i].Rescale();

        // 土台に合わせて地面の位置を変更.
        this.AdjustGroundLevel();
    }

    public void UpdateInfo()
    {
        // 彫刻全体の高さを割り出す.
        var etherHeight = (unitHeight * dropNumSwitcher.numDrops) + (unitHeight * 0.66f * 2); // 上下二つの粒は他の粒より低い.
        etherHeightInfo.text = "Ether Height: " + "\n" + etherHeight.ToString() + " mm";
    }
}