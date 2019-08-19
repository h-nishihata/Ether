using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 別シーンで順列を生成するスクリプト.
/// </summary>
public class Permutation : MonoBehaviour
{
    private List<string> dropList;
    private List<string> tempResult;
    private List<string> result;

    //public int numParicles;
    private int iterCount;
    private string lastNum;

    private CSVWriter csvWriter;


    public void Start()
    {
        dropList = new List<string>() { "2", "3", "4", "5", "6", "7"};
        tempResult = new List<string>();
        result = new List<string>();

        csvWriter = Camera.main.GetComponent<CSVWriter>();

        GeneratePatterns();
    }

    public void GeneratePatterns()
    {
        for (int i = 0; i < dropList.Count; i++)
        {
            //if ((iterCount < 1) && (Random.Range(0f, 10f) > 3f))
            //continue;
            tempResult.Add(dropList[i]);

            //if (iterCount < 2)
            //{
                for (int j = 0; j < dropList.Count; j++)
                {
                    tempResult.Add(dropList[j]);


                    //for (int k = 0; k < dropList.Count; k++)
                    //{
                        //tempResult.Add(dropList[k]);
                        if (iterCount < 0)
                        {
                            iterCount++;
                            GeneratePatterns();
                        }
                        Save(tempResult);
                        tempResult.RemoveAt(tempResult.Count - 1);
                    //}
                    //Save(tempResult);
                    //tempResult.RemoveAt(tempResult.Count - 1);
                }
            //}
            //else
            //{
            //    Save(tempResult);
            //}

            tempResult.RemoveAt(tempResult.Count - 1);

            if ((i == dropList.Count - 1) && (iterCount >0))
                iterCount--;
        }
    }

    public void Save(List<string> tempResult)
    {
        if (tempResult.Count < 2)
            return;

        //if (Random.Range(0f, 10f) > 1f) // 膨大な数のパターンが生成されるので，CSVファイルへ書き込む前の段階でランダムに選択する.
            //return;

        result.Add("1");
        for (int i = 0; i < tempResult.Count; i++)
        {
            if (lastNum == tempResult[i]) // 同じ粒が続いているものは使わないので，飛ばす.
                goto CLEAR;
            result.Add(tempResult[i]);
            lastNum = tempResult[i];
        }
        result.Add("8");

        //if (result.Count == tempResult.Count + 2)
        //{
            var stringData = string.Join(",", result.ToArray());
            //Debug.Log(stringData);
            csvWriter.Save(stringData, "patternData");

            // ex.
            //stringArray = new string[] { "\n", "1,2,3", "4,5,6", "7,8,9" };
            //csvWriter.Save(stringArray, "patternData");
        //}

    CLEAR: ;
        result.Clear();
        lastNum = "";
    }
}