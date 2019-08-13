using System;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 各ページの子オブジェクトに付いている，必要分のBoxを有効にするスクリプト. 
/// </summary>
public class SetDropModels : MonoBehaviour
{
    private CSVReader csvReader; // シーン内にある，モデル設定のスクリプト.
    private int csvInitLine;
    public int pageID; // 自分のページ番号.

    public Transform[] boxes; // 各ページの，粒を入れるBox.
    private int numActiveBoxes; // 有効化されているBoxの数.
    private SwitchActiveDrop[] switchActiveDrops; // 各Boxに付いている，どの粒を有効化するかを決めるスクリプト.

    public Vector3[] offsetPositions; // 3Dモデルをインポートした時点で付いていたオフセットを相殺するための値.

    public Text info;
    private StringBuilder lotNumber = new StringBuilder();


    public void ManualStart()
    {
        csvReader = GameObject.FindWithTag("List").GetComponent<CSVReader>();
        switchActiveDrops = new SwitchActiveDrop[boxes.Length];
        for (int i = 0; i < boxes.Length; i++)
        {
            switchActiveDrops[i] = boxes[i].GetComponent<SwitchActiveDrop>();
        }
    }

    public void SetDrops(int initLine)
    {
        csvInitLine = initLine + 1;
        lotNumber.Clear(); // 番号をクリア.


        for (int i = 0; i < boxes.Length; i++)
        {
            boxes[i].transform.gameObject.SetActive(true);

            var modelID = csvReader.csvData[csvInitLine + pageID][i];
            if (modelID != "0")
                numActiveBoxes++; // 段数をカウント.

            if (i > numActiveBoxes - 1)
            {
                boxes[i].transform.gameObject.SetActive(false); // 使わないBoxは非アクティブにする.
                continue;
            }

            lotNumber.Append(modelID); //  「1」と「8」のあいだの番号を生成.

            var dropID = Int32.Parse(modelID);
            switchActiveDrops[i].Trigger(dropID - 1);
        }

        SetInfo(lotNumber.ToString());
        numActiveBoxes = 0;
    }

    void SetInfo(string lotNumber)
    {
        info.text = "Number of drops: " + "\n" +
                    "Pattern: " + lotNumber + "\n" +
                    "Material: " + "\n";
        info.fontSize = (int)(Screen.width * 0.02f);
    }
}
