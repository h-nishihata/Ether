using UnityEngine;
using UnityEngine.UI;

public class BodyController : MonoBehaviour
{
    public Transform body;
    private Toggle toggle;
    public Button resetButton;

    public Slider etherSizeSlider;
    public int unitHeight;
    public int bodyHeight = 1800;
    public float etherHeight;
    private int zPos;
    private float defaultScale = 1.3f;
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

        // 彫刻の高さに合わせて人型の大きさを設定する.
        var multiplyRate = bodyHeight / etherHeight;
        var scale = defaultScale * multiplyRate;
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
        etherHeightInfo.text = "Ether Height: " + "\n" + etherHeight.ToString() + " mm";
    }
}