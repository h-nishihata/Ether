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

    public Slider slider;
    public Text value;
    public static int maxDrops = 11;
    public static int numDrops;
    private int prevNumDrops;
    public Button[] pedestalButtons;

    private Vector3[] defaultPositions; // 粒数が最大になったときの，それぞれの粒の位置.
    private float dropHeight = 0.5f;

    private RandomNumGenerator generator;


    private void Start()
    {
        generator = this.gameObject.GetComponent<RandomNumGenerator>();
        defaultPositions = new Vector3[drops.Length];
        for (int i = 0; i < drops.Length; i++)
        {
            defaultPositions[i] = drops[i].transform.localPosition;
        }
    }

    void Update()
    {
        numDrops = (int)slider.value;

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

        for (int i = 1; i < pedestals.Length; i++)
        {
            if (pedestals[i].gameObject.activeSelf)
                pedestals[i].localPosition = new Vector3(0f, bottomDrop.localPosition.y - 0.33f, 0f);
        }
    }

    public void SetPedestal(int id)
    {
        // リセット.
        for (int i = 0; i < pedestalButtons.Length; i++)
        {
            pedestalButtons[i].interactable = true;
            if (i > 0)
                pedestals[i].gameObject.SetActive(false);
        }

        pedestalButtons[id].interactable = false;
        if (id > 0)
        {
            pedestals[id].gameObject.SetActive(true);
            pedestals[id].localPosition = new Vector3(0f, bottomDrop.localPosition.y - 0.33f, 0f);
        }
    }
}
