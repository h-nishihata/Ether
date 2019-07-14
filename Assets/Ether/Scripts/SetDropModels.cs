using System;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 各ページの子オブジェクトについている，モデル設定のためのスクリプト.
/// </summary>
public class SetDropModels : MonoBehaviour
{
    private CSVReader csvReader; // シーン内にある，モデル設定のスクリプト.
    private int csvInitLine;
    public int pageID; // ページ番号.

    public Transform[] boxes; // 各ページの，粒を入れる枠.
    private int numActiveBoxes; // 有効化されているBoxの数.
    private SwitchActiveDrop[] switchActiveDrops; // 各Boxに付いている，どの粒を有効化するかを決めるスクリプト.

    public VerticalLayoutGroup verticalLayoutGroup;
    public Vector3[] offsetPositions; // 枠の上下のオフセット値.
    private int padding;

    public Text number;
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

    public void Trigger()
    {
        csvInitLine = csvReader.csvInitLine;
        lotNumber.Clear(); // 番号をクリア.
        SetDrops();
    }

    private void SetDrops()
    {
        for (int i = 0; i < boxes.Length; i++)
        {
            boxes[i].transform.gameObject.SetActive(true);

            var modelID = csvReader.csvData[csvInitLine + pageID][i];
            if (modelID != "0")
                numActiveBoxes++; // 段数をカウント.

            if (i > numActiveBoxes - 1)
            {
                boxes[i].transform.gameObject.SetActive(false); // 彫刻の中心を回転の中心を揃えるため，使わないBoxは非アクティブにする.
                continue;
            }

            lotNumber.Append(modelID); //  「1」と「8」のあいだの番号を生成.

            var dropID = Int32.Parse(modelID);
            switchActiveDrops[i].Trigger(dropID - 1);
        }

        number.text = lotNumber.ToString();
        AdjustOffsets(numActiveBoxes);
        numActiveBoxes = 0;
    }

    private void AdjustOffsets(int numDrops)
    {
        switch (numDrops)
        {
            case 4:
                padding = 550;
                break;
            case 5:
                padding = 350;
                break;
            case 6:
                padding = 200;
                break;
        }
        verticalLayoutGroup.padding.top = verticalLayoutGroup.padding.bottom = padding;
    }
}
