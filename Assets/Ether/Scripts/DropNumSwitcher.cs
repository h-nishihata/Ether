﻿using UnityEngine.UI;
using UnityEngine;

/// <summary>
/// (1) 親オブジェクトに付いている，粒の数を変更するスクリプト.
/// </summary>
public class DropNumSwitcher : MonoBehaviour
{
    public Transform ether;
    public Transform[] drops;
    public Transform topDrop;
    public Transform bottomDrop;

    public Slider slider;
    public Text value;
    public static int maxDrops = 11;
    public static int numDrops;
    private int prevNumDrops;

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
        value.text = (numDrops + 2).ToString();
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
    }
}
