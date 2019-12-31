using UnityEngine;
using UnityEngine.UI;

public class BodyController : MonoBehaviour
{
    public Transform body;
    private Toggle toggle;
    public Button resetButton;

    public Slider etherSizeSlider;
    public int bodyHeight = 1800;
    public float etherHeight;
    private float defaultScale = 1.3f;

    public Transform[] pedestals;
    public int activePedestalID;
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
        body.transform.localPosition = new Vector3(-3f, yPos, 0f);
    }

    public void Rescale()
    {
        // 一粒の高さが50mm刻みで変わる.
        var unitHeight = (int)etherSizeSlider.value;
        var surplus = unitHeight % 50;
        if (surplus > 0)
            unitHeight = unitHeight - surplus;

        // 彫刻全体の高さを割り出す.
        etherHeight = (unitHeight * DropNumSwitcher.numDrops) + (unitHeight * 0.66f * 2); // 上下二つの粒は他の粒より低い.
        //Debug.Log("Sculpture height: " + totalHeight + " mm");

        // 彫刻の高さに合わせて人型の大きさを設定する.
        var multiplyRate = bodyHeight / etherHeight;
        var scale = defaultScale * multiplyRate;
        body.transform.localScale = new Vector3(scale, scale, scale);

        // 彫刻に合わせて土台の大きさを変更.
        for (int i = 0; i < pedestalSizes.Length; i++)
            pedestalSizes[i].Rescale();

        this.AdjustGroundLevel();
    }
}