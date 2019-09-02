using System;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 各ページの子オブジェクトに付いている，必要分のBoxを有効にするスクリプト. 
/// </summary>
public class DropModelSetter : MonoBehaviour
{
    private CSVReader csvReader; // シーン内にある，モデル設定のスクリプト.
    private int csvInitLine;
    public int pageID; // 自分のページ番号.

    public Transform[] boxes; // 各ページの，粒を入れるBox.
    private ActiveDropSwitcher[] switchActiveDrops; // 各Boxに付いている，どの粒を有効化するかを決めるスクリプト.
    public Vector3[] offsetPositions; // 3Dモデルをインポートした時点で付いていたオフセットを相殺するための値.

    public bool isExistentInArchive; // すでに制作されているかどうか.
    public string fixedMat; //　(制作済の場合)使用された素材.

    public Text patternInfo;
    private StringBuilder lotNumber = new StringBuilder(); // 生成されたパターンの文字列.
    private StringBuilder infoText = new StringBuilder(); // (制作済の場合)展示情報などを追加した最終の文字列.
    // CSVファイル書き込み用の，カンマで区切られたロット番号を作成する.
    string[] lotNumArray = new string[13];
    public string lotNumber4CSV;


    public void ManualStart()
    {
        csvReader = GameObject.FindWithTag("List").GetComponent<CSVReader>();
        switchActiveDrops = new ActiveDropSwitcher[boxes.Length];
        for (int i = 0; i < boxes.Length; i++)
        {
            switchActiveDrops[i] = boxes[i].GetComponent<ActiveDropSwitcher>();
        }
    }

    /// <summary>
    /// CSVファイル内の自分の行の数字の並びに従って，粒を設定する.
    /// </summary>
    /// <param name="initLine">粒数のグループの始まりの行番号.</param>
    public void SetDrops(int initLine)
    {
        csvInitLine = initLine + 1;
        // リセット.
        isExistentInArchive = false;
        lotNumber.Clear();
        infoText.Clear();

        for (int i = 0; i < boxes.Length; i++)
        {
            boxes[i].transform.gameObject.SetActive(false); // 一旦すべてのBoxをオフにする.

            var modelID = csvReader.csvData[csvInitLine + pageID][i];
            if (modelID != "") // 1~8までのいずれかの粒を使うなら(使わない段はリスト内で空白になっている)
                boxes[i].transform.gameObject.SetActive(true); // 使うBoxのみ有効化する.
            else
                continue;

            switchActiveDrops[i].Trigger(Int32.Parse(modelID) - 1); // それぞれのBoxに，使用する粒のモデルを伝える.
            lotNumArray[i] = modelID;
            lotNumber.Append(modelID); // 番号を生成.
        }

        CheckExistence(csvInitLine + pageID);
        SetInfo(lotNumber.ToString());
        lotNumber4CSV = string.Join(",", lotNumArray);
    }

    void CheckExistence(int lineNum)
    {
        for (int i = 0; i < csvReader.archivedPatterns.Length; i++)
        {
            if (csvReader.archivedPatterns[i] == lotNumber.ToString())
            {
                isExistentInArchive = true;
                fixedMat = csvReader.archiveData[i][13];
            }
        }
    }

    void SetInfo(string lotNumber)
    {
        infoText.Append("Pattern      : " + lotNumber + "\n");

        if (isExistentInArchive)
        {
            // 展示情報を表示(14列目以降の情報を順次読み込む). 
            var edition = csvReader.csvData[csvInitLine + pageID][14];
            var dimension = csvReader.csvData[csvInitLine + pageID][15];
            var year = csvReader.csvData[csvInitLine + pageID][16];
            var exhibition = csvReader.csvData[csvInitLine + pageID][17];
            infoText.Append("Edition      : " + edition + "\n");
            infoText.Append("Dimension: " + dimension + "\n");
            infoText.Append("Year           : " + year + "\n");
            infoText.Append("Exhibition : " + "\n" + exhibition + "\n");
        }

        patternInfo.fontSize = (int)(Screen.width * 0.03f);
        patternInfo.color = isExistentInArchive ? Color.black : Color.white;
        patternInfo.text = infoText.ToString();
    }
}