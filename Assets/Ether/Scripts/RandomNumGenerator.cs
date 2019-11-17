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
    private List<string> numList = new List<string> {"2", "3", "4", "5", "6", "7"}; // 上下のパーツは同じもので，番号を0とする.間のパーツが6種類ある.
    private string num;
    private string prevNum;
    public ModelSetter[] modelSetters;

    public Text patternInfo;
    private StringBuilder pattern = new StringBuilder(); // 生成されたパターンの文字列.

    private CSVReader csvReader;


    private void Start()
    {
        csvReader = this.GetComponent<CSVReader>();
    }

    public void Generate()
    {
        pattern.Append("1");

        for (int i = 0; i < DropNumSwitcher.numDrops; i++)
        {
            num = numList[UnityEngine.Random.Range(0, numList.Count)];
            if (DropNumSwitcher.numDrops < 7) // 上下も入れて8粒までなら，形が被らないようにできる.
            {
                numList.Remove(num);
            }
            else if (DropNumSwitcher.numDrops >= 7) // それ以上の段数になったときには，同じ数字が続かないようにさえしていれば，同じ粒を繰り返し使えるようにする.
            {
                do
                {
                    num = numList[UnityEngine.Random.Range(0, numList.Count)];
                } while (num == prevNum);
                prevNum = num;
            }

            modelSetters[i].SetModel(Int32.Parse(num) - 2);
            pattern.Append(num);
        }

        pattern.Append("8");

        var result = this.CheckExistence();
        if (result)
            Debug.Log("<color='red'>The pattern already exists in the Archive: </color>" + pattern);
        else
            patternInfo.text = pattern.ToString();

        this.Reset();
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

    private void Reset()
    {
        numList.Clear();
        for (int i = 2; i < 8; i++)
        {
            numList.Add(i.ToString());
        }
        pattern.Clear();
    }
}
