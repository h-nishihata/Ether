using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CSVReader : MonoBehaviour
{
    private TextAsset csvFile; // CSVファイル.
    public List<string[]> csvData = new List<string[]>(); // CSVの中身を入れるリスト.
    public GameObject[] pages;
    private TurnPage pageSwitcher;
    public Sprite[] sourceImages;


    void Awake()
    {
        csvFile = Resources.Load("testCSV") as TextAsset; // Resouces下のCSV読み込み.
        StringReader reader = new StringReader(csvFile.text);

        // ","で分割しつつ一行ずつ読み込み，リストに追加していく.
        while (reader.Peek() > -1) // reader.Peekが0になるまで繰り返す.
        {
            string line = reader.ReadLine(); // 一行ずつ読み込み.
            csvData.Add(line.Split(','));   // ","区切りでリストに追加.
        }

        // csvDatas[行][列]を指定して値を自由に取り出せる.
        //Debug.Log(csvData[0][1]);

        pageSwitcher = transform.GetComponent<TurnPage>();
    }

    void Start()
    {
        var numPages = Data.Instance.numPages;
        //Debug.Log("pages: " + numPages);
        //Debug.Log("boxes: " + numBoxes);
        //Debug.Log(csvData[numLine][0]);

        SetPages(numPages);
        pageSwitcher.pageCount = numPages; //ページの端の位置を伝える.
    }

    void SetPages(int activePages)
    {
        for (int i = 0; i < activePages; i++)
        {
            pages[i].GetComponent<SetParticleImages>().pageID = i;
            pages[i].SetActive(true);
        }
    }
}
