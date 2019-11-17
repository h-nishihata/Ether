using System.Text;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// CSVデータの読込.
/// https://note.mu/macgyverthink/n/n83943f3bad60
/// </summary>
public class CSVReader : MonoBehaviour
{
    private TextAsset csvFile; // CSVファイル.
    public string fileNames = "archiveData";
    private StringReader reader;
    public List<string> csvData = new List<string>(); // CSVファイルの中身を入れるリスト.

    private StringBuilder stringBuilder = new StringBuilder();
    public string[] archivedPatterns; // 新たに生成されたパターンと重複していないか確認するための，今までに制作されたパターンの文字列データ.



    void Awake()
    {
        csvFile = Resources.Load(fileNames) as TextAsset; // Resouces下のCSV読み込み.
        reader = new StringReader(csvFile.text);

        while (reader.Peek() > -1) // reader.Peekが0になるまで繰り返す.
        {
            string line = reader.ReadLine(); // 一行ずつ読み込み.
            csvData.Add(line);   //，リストに追加.
        }
        // [行][列]を指定して値を自由に取り出せる.
        // Debug.Log(csvData[0][1]);
        
        /*
        archivedPatterns = new string[csvData.Count];
        for (int i = 0; i < csvData.Count; i++)
        {
            // archiveDataのそれぞれの行から，数字の並びだけを取り出して文字列にする.
            for (int j = 0; j < 13; j++)
            {
                var val = csvData[i][j];
                stringBuilder.Append(val);
            }
            archivedPatterns[i] = stringBuilder.ToString();
            stringBuilder.Clear();
        }
        */
    }
}