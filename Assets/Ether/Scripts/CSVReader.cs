using System.Text;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// Canvasに付いている，データ読込〜粒の設定までを行うスクリプト.
/// https://note.mu/macgyverthink/n/n83943f3bad60
/// </summary>
public class CSVReader : MonoBehaviour
{
    private TextAsset[] csvFile = new TextAsset[2]; // CSVファイル.
    public string[] fileNames;
    private StringReader[] reader = new StringReader[2];
    public List<string[]> csvData = new List<string[]>(); // CSVファイルの中身を入れるリスト.
    public List<string[]> archiveData = new List<string[]>();
    public int[] csvInitLines = new int[12]; // (今のところ)全ての粒数のパターンが一つのCSVファイルの中に収まっている.この行番号は，リストの中でそれぞれの粒数のパターンが始まっている区切りを表す.
    private int numInitLines; // いくつめの区切り(===)かを示すカウント.

    private StringBuilder stringBuilder = new StringBuilder();
    public string[] archivedPatterns; // 新たに生成されたパターンと重複していないか確認するための，今までに制作されたパターンの文字列データ.

    private RectTransform list; // 全ページを含む，Canvasの子オブジェクト.
    private PageSwitcher pageSwitcher;
    private int numMaxPages = 200; // 最大ページ数.
    private int numPages; // 用意するページ数.
    public GameObject pageTemplate;
    private GameObject[] pages;

    private DropModelSetter[] modelSetter; //各ページの子オブジェクトについている，モデル設定のためのスクリプト.
    private float lastHandlePos;


    void Awake()
    {
        for (int i = 0; i < 2; i++)
        {
            csvFile[i] = Resources.Load(fileNames[i]) as TextAsset; // Resouces下のCSV読み込み.
            reader[i] = new StringReader(csvFile[i].text);

            while (reader[i].Peek() > -1) // reader.Peekが0になるまで繰り返す.
            {
                string line = reader[i].ReadLine(); // 一行ずつ読み込み.
                if(i == 0)
                    csvData.Add(line.Split(','));   // ","で分割し，リストに追加.
                else if(i == 1)
                    archiveData.Add(line.Split(','));
            }
            // [行][列]を指定して値を自由に取り出せる.
            // Debug.Log(csvData[0][1]);
        }

        for (int i = 0; i < csvData.Count; i++)
        {
            var value = csvData[i][0];
            if(value == "===") // CSVファイルの中で区切りの記号を見つけたら...
            {
                csvInitLines[numInitLines] = i; // 次の行から粒数が増える目印として登録する.
                numInitLines++;
            }
        }

        archivedPatterns = new string[archiveData.Count];
        for (int i = 0; i < archiveData.Count; i++)
        {
            // archiveDataのそれぞれの行から，数字の並びだけを取り出して文字列にする.
            for (int j = 0; j < 13; j++)
            {
                var val = archiveData[i][j];
                stringBuilder.Append(val);
            }
            archivedPatterns[i] = stringBuilder.ToString();
            stringBuilder.Clear();
        }
    }

    void Start()
    {
        list = transform.GetComponent<RectTransform>();
        pageSwitcher = transform.GetComponent<PageSwitcher>();

        // 上限までページを生成し，Hierarchy内のオブジェクトに名前をつける.
        pages = new GameObject[numMaxPages];
        for (int i = 0; i < numMaxPages; i++)
        {
            pages[i] = Instantiate(pageTemplate);
            var p = i < 10 ? "0" + i.ToString() : i.ToString();
            pages[i].name = "page" + p;
            pages[i].transform.SetParent(this.transform);
        }

        // 粒のモデルを準備.
        modelSetter = new DropModelSetter[pages.Length];
        for (int i = 0; i < pages.Length; i++)
        {
            modelSetter[i] = pages[i].GetComponentInChildren<DropModelSetter>();
            modelSetter[i].ManualStart();
        }

        SetPages(7, csvInitLines[0]);
    }

    /// <summary>
    /// スライダーから粒の数を変更する.
    /// </summary>
    public void OnValueChanged(float handlePos)
    {
        if ((!MatTexSetter.genConfirmed) && (handlePos == lastHandlePos))
            return;

        lastHandlePos = handlePos;
        numInitLines = (int)(handlePos * 10);

        // 1ページ目に戻す.
        list.anchoredPosition = Vector3.zero;
        pageSwitcher.currentPage = 1;
        // その粒数のパターンがいくつあるか(= 何ページ必要か)を数える.
        numPages = csvInitLines[numInitLines + 1] - (csvInitLines[numInitLines] + 1);
        SetPages(numPages, csvInitLines[numInitLines]);
    }

    /// <summary>
    /// 必要分のページを有効にする.
    /// </summary>
    /// <param name="activePages">準備するページの数.</param>
    /// <param name="initLine">データ読込を開始する行番号.</param>
    void SetPages(int activePages, int initLine)
    {
        pageSwitcher.pageCount = activePages; // ページの端の位置を伝える.

        for (int i = 0; i < pages.Length; i++)
        {
            pages[i].SetActive(false); // 一旦すべてのページをオフにする.
        }
        for (int i = 0; i < activePages; i++)
        {
            if (i >= pages.Length)
                return;

            pages[i].SetActive(true);
            modelSetter[i].pageID = i;
            modelSetter[i].SetDrops(initLine);
        }
    }
}
