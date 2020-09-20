using System;
using System.Collections.Generic;
using UnityEngine;

using System.Text;
using UnityEngine.UI;

/// <summary>
/// 親オブジェクトに付いている，パターンを生成するスクリプト.
/// </summary>
public class RandomNumGenerator : MonoBehaviour
{
    private List<string> numList = new List<string> {"2", "3", "4", "5", "6", "7"}; // 上下を除いた6種類のパーツ.
    private string num;
    private string prevNum;

    private List<int> tempList = new List<int>(); // 重複していないか確認する間，生成した数字を逃しておくためのリスト.
    private CSVReader csvReader;
    public ModelSetter[] modelSetters;

    private StringBuilder pattern = new StringBuilder(); // 生成されたパターンの文字列.
    public Text patternInfo;

    public static bool isArchiveMode;
    private int archiveIterator;
    public Button[] buttons;

    private DropNumSwitcher dropNumSwitcher;


    private void Start()
    {
        csvReader = this.GetComponent<CSVReader>();
        dropNumSwitcher = this.GetComponent<DropNumSwitcher>();
    }
    public void EnableArchiveMode()
    {
        isArchiveMode = !isArchiveMode;
        if (isArchiveMode)
        {
            // 制作済パターンを最初から読み込む.
            archiveIterator = -1;
            dropNumSwitcher.numDrops = 1;

            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].interactable = i < 2;
            }
        }
        else
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].interactable = i >= 2;
            }
        }
        this.Generate();
    }

    public void Generate()
    {
        if (isArchiveMode)
        {
            this.ShowArchiveModels(true);
            return;
        }

        pattern.Append("1");

        for (int i = 0; i < dropNumSwitcher.numDrops; i++)
        {
            num = numList[UnityEngine.Random.Range(0, numList.Count)];
            if (dropNumSwitcher.numDrops < 7) // 上下も入れて8粒までなら，形が被らないようにできる.
            {
                numList.Remove(num);
            }
            else if (dropNumSwitcher.numDrops >= 7) // それ以上の段数になったときには，同じ数字が続かないようにさえしていれば，同じ粒を繰り返し使えるようにする.
            {
                do
                {
                    num = numList[UnityEngine.Random.Range(0, numList.Count)];
                } while (num == prevNum);
                prevNum = num;
            }

            tempList.Add(Int32.Parse(num));
            pattern.Append(num);
        }

        pattern.Append("8");

        var result = this.CheckExistence();
        if (result == true)
        {
            // 今までに制作されたことのあるパターンなら，数列の生成を最初からやり直す.
            this.Reset();
            this.Generate();
        }
        else
        {
            // 被っていなければ，モデルを設定する.
            patternInfo.text = "Pattern: " + "\n" + pattern;
            for (int i = 0; i < dropNumSwitcher.numDrops; i++)
                modelSetters[i].SetModel(tempList[i]);
            this.Reset();
        }
    }

    private bool CheckExistence()
    {
        var isExistent = false;
        for (int i = 0; i < csvReader.csvData.Count; i++)
        {
            if (pattern.ToString() == csvReader.csvData[i])
            {
                isExistent = true;
            }
        }
        return isExistent;
    }

    /// <summary>
    /// 今までに制作されたパターンのみを表示する.
    /// </summary>
    public void ShowArchiveModels(bool isAscending)
    {
        if (isAscending)
        {
            if (archiveIterator < csvReader.csvData.Count - 1)
                archiveIterator++;
            else
                archiveIterator = 0;
        }
        else
        {
            if (archiveIterator > 0)
                archiveIterator--;
            else
                archiveIterator = csvReader.csvData.Count - 1;
        }

        var curretLine = csvReader.csvData[archiveIterator];
        dropNumSwitcher.numDrops = curretLine.Length - 2; // 読み込んだパターンに応じて，可変の粒数を変更.

        for (int i = 0; i < curretLine.Length; i++)
        {
            if((i > 0) && (i < curretLine.Length - 1)) // 可変の粒の番号のみリストに加えて，ModelSetter.csに渡す.
                tempList.Add(Int32.Parse(curretLine[i].ToString()));
            pattern.Append(curretLine[i]);
        }

        patternInfo.text = "Pattern: " + "\n" + pattern;
        for (int i = 0; i < dropNumSwitcher.numDrops; i++)
            modelSetters[i].SetModel(tempList[i]);

        this.Reset();
    }

    private void Reset()
    {
        numList.Clear();
        for (int i = 2; i < 8; i++)
            numList.Add(i.ToString());

        pattern.Clear();
        tempList.Clear();
    }
}
