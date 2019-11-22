using UnityEngine.UI;
using UnityEngine;

/// <summary>
/// 親オブジェクトに付いている，粒の数を変更するスクリプト.
/// </summary>
public class DropNumSwitcher : MonoBehaviour
{
    public Transform ether;
    public Transform[] drops;
    public Transform topDrop;
    public Transform bottomDrop;
    public Transform[] pedestals;

    public Slider numDropsSlider;
    public Text value;
    public static int maxDrops = 11;
    public static int numDrops;
    private int prevNumDrops;

    private Vector3[] defaultPositions; // 粒数が最大になったときの，それぞれの粒の位置.
    private float dropHeight = 0.5f;
    private RandomNumGenerator generator;

    public BodyController bodyController;
    public Button[] pedestalButtons;
    private Rotation rotation;


    private void Start()
    {
        generator = this.gameObject.GetComponent<RandomNumGenerator>();
        rotation = this.gameObject.GetComponent<Rotation>();

        defaultPositions = new Vector3[drops.Length];
        for (int i = 0; i < drops.Length; i++)
        {
            defaultPositions[i] = drops[i].transform.localPosition;
        }
    }

    void Update()
    {
        numDrops = (int)numDropsSlider.value;

        if (prevNumDrops != numDrops)
            this.ChangeNumDrops();
    }

    void ChangeNumDrops()
    {
        this.SwitchActiveDrops();
        value.text = "Num drops: " + "\n" + (numDrops + 2).ToString();
        prevNumDrops = numDrops;
        generator.Generate();
    }

    void SwitchActiveDrops()
    {
        // リセット.
        for (int i = 0; i < drops.Length; i++)
        {
            drops[i].localPosition = defaultPositions[i];
            drops[i].gameObject.SetActive(false);
        }

        // 粒数が減っても中心が保たれるように粒を配置する.
        for (int i = 0; i < numDrops; i++)
        {
            drops[i].gameObject.SetActive(true);
            drops[i].localPosition = new Vector3(0f, defaultPositions[i].y + (dropHeight * (maxDrops - numDrops) * 0.5f), 0f);
        }
        topDrop.localPosition = new Vector3(0f, drops[numDrops - 1].localPosition.y + dropHeight, 0f);
        bottomDrop.localPosition = new Vector3(0f, drops[0].localPosition.y - dropHeight, 0f);

        // 土台の位置調整.
        for (int i = 1; i < pedestals.Length; i++)
            pedestals[i].GetComponent<PedestalSizeSetter>().Rescale();

        // 人の位置調整.
        bodyController.AdjustGroundLevel();
    }

    /// <summary>
    /// 土台の有無を切り替える.
    /// </summary>
    public void SetPedestal(int id)
    {
        // リセット.
        for (int i = 0; i < pedestalButtons.Length; i++)
        {
            pedestalButtons[i].interactable = true;
            if (i > 0)
                pedestals[i].gameObject.SetActive(false);
        }

        // 選択した土台を有効にする.
        pedestalButtons[id].interactable = false;
        if (id > 0)
        {
            rotation.resetButton.interactable = true;
            pedestals[id].gameObject.SetActive(true);
            pedestals[id].GetComponent<PedestalSizeSetter>().Rescale();
        }

        // 人の位置調整.
        bodyController.activePedestalID = id;
        bodyController.AdjustGroundLevel();
    }
}
