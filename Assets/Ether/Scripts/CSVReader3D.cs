﻿using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CSVReader3D : MonoBehaviour
{
    private TextAsset csvFile; // CSVファイル.
    public string fileName = "patternData";
    public List<string[]> csvData = new List<string[]>(); // CSVの中身を入れるリスト.

    private int numPages;
    public int csvInitLine;

    private RectTransform list;
    private TurnPage pageSwitcher;

    public GameObject template;
    private GameObject[] pages;
    private int numMaxPages = 100;
    private SetParticleModels[] modelSetter;
    public MeshFilter[] sourceMeshes;

    public Slider slider;

    void Awake()
    {
        csvFile = Resources.Load(fileName) as TextAsset; // Resouces下のCSV読み込み.
        StringReader reader = new StringReader(csvFile.text);

        // ","で分割しつつ一行ずつ読み込み，リストに追加していく.
        while (reader.Peek() > -1) // reader.Peekが0になるまで繰り返す.
        {
            string line = reader.ReadLine(); // 一行ずつ読み込み.
            csvData.Add(line.Split(','));   // ","区切りでリストに追加.
        }

        // csvDatas[行][列]を指定して値を自由に取り出せる.
        //Debug.Log(csvData[0][1]);

        list = transform.GetComponent<RectTransform>();
        pageSwitcher = transform.GetComponent<TurnPage>();


        pages = new GameObject[numMaxPages];
        for (int i = 0; i < numMaxPages; i++)
        {
            pages[i] = Instantiate(template);
            var p = i < 10 ? "0" + i.ToString() : i.ToString();
            pages[i].name = "page" + p;
            pages[i].transform.SetParent(this.transform);
        }
    }

    void Start()
    {
        modelSetter = new SetParticleModels[pages.Length];
        for (int i = 0; i < pages.Length; i++)
        {
            modelSetter[i] = pages[i].GetComponentInChildren<SetParticleModels>();
            modelSetter[i].WarmUp();
        }

        var scene = SceneManager.GetActiveScene().name;
        numPages = scene == "2_Archives" ? 58 : 2;

        csvInitLine = 1;
        SetPages(numPages);
    }

    public void OnValueChanged()
    {
        var dropDownValue = (int)slider.value;
        switch (dropDownValue)
        {
            case 0: // 4 particles
                numPages = 2;
                csvInitLine = 1;
                break;
            case 1: // 5 particles
                numPages = 6;
                csvInitLine = 4;
                break;
            case 2: // 6 particles
                numPages = 24;
                csvInitLine = 11;
                break;
        }

        // 1ページ目に戻す.
        list.anchoredPosition = Vector3.zero;
        pageSwitcher.currentPage = 1;
        SetPages(numPages);
    }

    void SetPages(int activePages)
    {
        pageSwitcher.pageCount = activePages; // ページの端の位置を伝える.

        for (int i = 0; i < pages.Length; i++)
        {
            pages[i].SetActive(false);
        }
        for (int i = 0; i < activePages; i++)
        {
            modelSetter[i].pageID = i;
            modelSetter[i].Trigger();
            pages[i].SetActive(true);
        }
    }
}
