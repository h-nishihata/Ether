using UnityEngine.UI;
using UnityEngine;

public class DropNumSwitcher : MonoBehaviour
{
    public Transform ether;
    public Transform[] drops;
    public Slider slider;
    public static int numDrops;
    private int prevNumDrops;
    private float unit = 0.5f;
    private Vector3[] initPositions;


    private void Start()
    {
        initPositions = new Vector3[drops.Length];
        for (int i = 0; i < drops.Length; i++)
        {
            initPositions[i] = drops[i].transform.localPosition;
        }
    }

    void Update()
    {
        numDrops = (int)slider.value;
        if (prevNumDrops == numDrops)
            return;

        this.SwitchActiveDrops();
        prevNumDrops = numDrops;
    }

    void SwitchActiveDrops()
    {
        for (int i = 0; i < drops.Length; i++)
        {
            drops[i].localPosition = Vector3.zero;
            drops[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < numDrops; i++)
        {
            drops[i].gameObject.SetActive(true);
            drops[i].localPosition = new Vector3(0f, initPositions[i].y + (unit * (11 - numDrops) * 0.5f), 0f);
        }

    }
}
