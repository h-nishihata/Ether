using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// (0) 親オブジェクトに付いている，パターンを生成するスクリプト.
/// </summary>
public class RandomNumGenerator : MonoBehaviour
{
    List<string> numList = new List<string> {"1", "2", "3", "4", "5", "6"};
    private string num;
    public ModelSetter[] modelSetters;


    public void Generate()
    {
        for (int i = 0; i < DropNumSwitcher.numDrops; i++)
        {
            num = numList[UnityEngine.Random.Range(0, numList.Count)];
            //numList.Remove(num);

            modelSetters[i].SetModel(Int32.Parse(num)-1);
        }
        // リセット.
        for (int i = 1; i < 7; i++)
        {
            numList.Add(i.ToString());
        }
    }


    /*
     上下も入れて8粒までなら，形が被らないようにできる.それ以上の段数になったときには，同じ数字が続かないようにさえしていれば，同じ粒を繰り返し使えるようにする.
        
    do
    {
        num = (Random.Range(1, 7)).ToString();
    } while (num == prevNum);

    pattern[i] = num;
    prevNum = pattern[i];
    */
}
