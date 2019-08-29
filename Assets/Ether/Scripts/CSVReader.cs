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
    private TextAsset csvFile; // CSVファイル.
    private TextAsset archiveFile;
    public string fileName = "patternData";
    public int[] csvInitLines = new int[12]; // (今のところ)全ての粒数のパターンが一つのCSVファイルの中に収まっている.
                                              // この行番号は，リストの中でそれぞれの粒数のパターンが始まっている区切りを表す.
    private int numInitLines;
    public List<string[]> csvData = new List<string[]>(); // CSVファイルの中身を入れるリスト.
    public List<string[]> archiveData = new List<string[]>();

    private StringBuilder stringBuilder = new StringBuilder();
    public string[] archivedPatterns;

    private RectTransform list; // 全ページを含む，Canvasの子オブジェクト.
    private TurnPage pageSwitcher;
    private int numMaxPages = 200; // 最大ページ数.
    private int numPages; // 用意するページ数.
    public GameObject pageTemplate;
    private GameObject[] pages;

    private SetDropModels[] setDropModels; //各ページの子オブジェクトについている，モデル設定のためのスクリプト.

    private float lastHandlePos;


    void Awake()
    {
        csvFile = Resources.Load(fileName) as TextAsset; // Resouces下のCSV読み込み.
        archiveFile = Resources.Load("archivedData") as TextAsset;
        StringReader reader = new StringReader(csvFile.text);
        StringReader reader01 = new StringReader(archiveFile.text);

        // ","で分割しつつ一行ずつ読み込み，リストに追加していく.
        while (reader.Peek() > -1) // reader.Peekが0になるまで繰り返す.
        {
            string line = reader.ReadLine(); // 一行ずつ読み込み.
            csvData.Add(line.Split(','));   // ","区切りでリストに追加.
        }
        while (reader01.Peek() > -1)
        {
            string line = reader01.ReadLine();
            archiveData.Add(line.Split(','));
        }
        // csvDatas[行][列]を指定して値を自由に取り出せる.
        //Debug.Log(csvData[0][1]);

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
            //archivedPatterns[i] = string.Join("", archiveData[i]);
            for (int j = 0; j < 13; j++)
            {
                var val = archiveData[i][j];
                stringBuilder.Append(val);
            }
            archivedPatterns[i] = stringBuilder.ToString();
            //Debug.Log(archivedPatterns[i]);
            stringBuilder.Clear();
        }
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

        SetPages(7, csvInitLines[0]);
    }

    /// <summary>
    /// スライダーから粒の数を変更する.
    /// </summary>
    public void OnValueChanged(float handlePos)
    {
        if (handlePos == lastHandlePos)
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
            setDropModels[i].pageID = i;
            setDropModels[i].SetDrops(initLine);
        }
    }
}
