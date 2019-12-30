﻿using UnityEngine;
using UnityEngine.UI;

public class BodyController : MonoBehaviour
{
    public Transform body;
    private Toggle toggle;

    public Button resetButton;
    public Transform[] pedestals;
    public int activePedestalID;

    public Slider bodySizeSlider;
    public int bodyHeight = 1800;
    private float defaultScale = 1.3f;


    private void Start()
    {
        toggle = this.gameObject.GetComponent<Toggle>();
    }

    public void EnableBody()
    {
        body.gameObject.SetActive(toggle.isOn);
        resetButton.interactable = true;
    }

    public void AdjustGroundLevel()
    {
        var yPos = pedestals[activePedestalID].transform.position.y;
        //body.transform.localPosition = new Vector3(-2f, yPos, 1.8f);
    }

    public void Rescale()
    {
        // 一粒の高さが50mm刻みで変わる.
        var unitHeight = (int)bodySizeSlider.value;
        var surplus = unitHeight % 50;
        if (surplus > 0)
            unitHeight = unitHeight - surplus;

        // 彫刻全体の高さを割り出す.
        var totalHeight = (unitHeight * DropNumSwitcher.numDrops) + (unitHeight * 0.66f * 2); // 上下二つの粒は他の粒より低い.
        var multiplyRate = bodyHeight / totalHeight;
        var scale = defaultScale * multiplyRate;
        body.transform.localScale = new Vector3(scale, scale, scale);
    }
}