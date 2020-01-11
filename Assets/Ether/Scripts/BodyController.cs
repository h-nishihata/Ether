using UnityEngine;
using UnityEngine.UI;

public class BodyController : MonoBehaviour
{
    public Transform body;
    private Toggle toggle;
    public Button resetButton;

    public Slider etherSizeSlider;
    public int unitHeight;
    private int bodyHeight = 1740;
    public float etherHeight;
    private int zPos;
    private float defaultScale = 1f; // 3段のEtherと高さを揃えたときのscale.一粒が750mmの計算になる.
    public Text etherHeightInfo;

    public Transform[] pedestals;
    public static int activePedestalID;
    public PedestalSizeSetter[] pedestalSizes;


    private void Start()
    {
        toggle = this.gameObject.GetComponent<Toggle>();
        this.Rescale();
    }

    public void EnableBody()
    {
        body.gameObject.SetActive(toggle.isOn);
        resetButton.interactable = true;
    }

    public void AdjustGroundLevel()
    {
        var yPos = pedestals[activePedestalID].transform.position.y;
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

        // 粒の高さに合わせて人型の大きさを設定する.
        // MEMO: 段数が変わるだけなら，積み上がっていくだけで粒の高さは変わらないので，人の大きさは変える必要はない.
        // だからetherHeight = 全体の高さを使ってスケールを求めているのがおかしい.
        //var multiplyRate = bodyHeight / etherHeight;
        //Debug.Log("bodyHeight: " + bodyHeight + ", etherHeight: " + etherHeight + ", multiplyRate: " + multiplyRate);
        //var scale = defaultScale * multiplyRate;
        var scale = 1000f / unitHeight;
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
        etherHeight = (unitHeight * DropNumSwitcher.numDrops) + (unitHeight * 0.66f * 2); // 上下二つの粒は他の粒より低い.
        //Debug.Log("etherHeight: " + etherHeight);
        etherHeightInfo.text = "Ether Height: " + "\n" + etherHeight.ToString() + " mm";
    }
}