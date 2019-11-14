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
    public List<string> numList = new List<string> {"1", "2", "3", "4", "5", "6"}; // 上下のパーツは同じもので，番号を0とする.間のパーツが6種類ある.
    private string num;
    private string prevNum;
    public ModelSetter[] modelSetters;

    public Text patternInfo;
    private StringBuilder pattern = new StringBuilder(); // 生成されたパターンの文字列.


    public void Generate()
    {
        pattern.Append("Pattern: 0");

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

            modelSetters[i].SetModel(Int32.Parse(num)-1);
            pattern.Append(num);
        }

        pattern.Append("0");
        patternInfo.text = pattern.ToString();
        this.Reset();
    }

    private void Reset()
    {
        numList.Clear();
        for (int i = 1; i < 7; i++)
        {
            numList.Add(i.ToString());
        }
        pattern.Clear();
    }
}
