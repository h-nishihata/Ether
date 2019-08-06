using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Canvasについている，データ読込〜粒の設定までを行うスクリプト.
/// </summary>
public class CSVReader : MonoBehaviour
{
    private TextAsset csvFile; // CSVファイル.
    public string fileName = "patternData";
    public List<string[]> csvData = new List<string[]>(); // CSVの中身を入れるリスト.
    public int csvInitLine;

    private RectTransform list;// 全ページを含む，Canvasの子オブジェクト.
    private TurnPage pageSwitcher;

    private int numPages;
    public GameObject pageTemplate;
    private GameObject[] pages;
    private int numMaxPages = 100;

    private SetDropModels[] setDropModels; //各ページの子オブジェクトについている，モデル設定のためのスクリプト.

    public Slider slider;
    private int sliderValue;
    private int lastSliderValue;

    public Text numDrops;


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
    }

    void Start()
    {
        list = transform.GetComponent<RectTransform>();
        pageSwitcher = transform.GetComponent<TurnPage>();

        // 上限までページを生成し，オブジェクトに名前をつける.
        pages = new GameObject[numMaxPages];
        for (int i = 0; i < numMaxPages; i++)
        {
            pages[i] = Instantiate(pageTemplate);
            var p = i < 10 ? "0" + i.ToString() : i.ToString();
            pages[i].name = "page" + p;
            pages[i].transform.SetParent(this.transform);
        }

        // 粒のモデルを準備.
        setDropModels = new SetDropModels[pages.Length];
        for (int i = 0; i < pages.Length; i++)
        {
            setDropModels[i] = pages[i].GetComponentInChildren<SetDropModels>();
            setDropModels[i].ManualStart();
        }

        // 生成シーンとアーカイヴシーンで同じスクリプトを使用しているので，初期ページ数が異なる.
        var scene = SceneManager.GetActiveScene().name;
        numPages = scene == "2_Archives" ? 58 : 2;
        csvInitLine = 0;
        SetPages(numPages);
    }

    /// <summary>
    /// スライダーから粒の数を変更する.
    /// </summary>
    public void OnValueChanged()
    {
        sliderValue = (int)slider.value;
        if (sliderValue == lastSliderValue)
            return;
        lastSliderValue = sliderValue;

        switch (sliderValue)
        {
            case 0: // 4 particles
                numPages = 2;
                csvInitLine = 0;
                break;
            case 1: // 5 particles
                numPages = 6;
                csvInitLine = 2;
                break;
            case 2: // 6 particles
                numPages = 24;
                csvInitLine = 8;
                break;
        }

        // 1ページ目に戻す.
        list.anchoredPosition = Vector3.zero;
        pageSwitcher.currentPage = 1;
        SetPages(numPages);
    }

    /// <summary>
    /// 必要分のページを有効にする.
    /// </summary>
    /// <param name="activePages">アクティヴになっているページの数.</param>
    void SetPages(int activePages)
    {
        pageSwitcher.pageCount = activePages; // ページの端の位置を伝える.

        for (int i = 0; i < pages.Length; i++)
        {
            pages[i].SetActive(false); //一旦すべてのページをオフにする.
        }
        for (int i = 0; i < activePages; i++)
        {
            pages[i].SetActive(true);
            setDropModels[i].pageID = i;
            setDropModels[i].Trigger();
        }
    }
}
