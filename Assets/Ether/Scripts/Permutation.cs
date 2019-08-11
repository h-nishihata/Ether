using UnityEngine;
using System.Collections.Generic;
using System;

public class Permutation : MonoBehaviour
{
    private List<string> list;
    private List<string> tempResult;
    private List<string> result;

    private int numParicles;
    private string lastNumber;

    private Camera mainCamera;
    private CSVWriter csvWriter;

    int iterCount;


    public void OnValueChanged(int dromDownValue)
    {
        numParicles = dromDownValue;
    }

    public void Start()
    {
        list = new List<string>() { "2", "3"/*, "4", "5", "6", "7"*/};
        tempResult = new List<string>();
        result = new List<string>();

        mainCamera = Camera.main;
        csvWriter = mainCamera.GetComponent<CSVWriter>();

        GeneratePatterns();
    }

    public void GeneratePatterns()
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (tempResult.Count > 0)
            {
                //if (list[i] == tempResult[tempResult.Count - 1])
                    //continue;
            }
            tempResult.Add(list[i]);

            for (int j = 0; j < list.Count; j++)
            {
                //if (list[j] == tempResult[tempResult.Count - 1])
                    //continue;
                tempResult.Add(list[j]);

                if (iterCount < 1)
                {
                    iterCount = 1;
                    //Debug.Log("<color=red>iterCount: </color>" + iterCount);
                    GeneratePatterns();
                }
                Save(tempResult);
                tempResult.RemoveAt(tempResult.Count - 1);
            }
            //Debug.Log("<color=red>iterCount: </color>" + iterCount);
            tempResult.RemoveAt(tempResult.Count - 1);

            if (i == list.Count - 1)
            {
                //if (iterCount < 2)
                //    iterCount++;
                //else
                    iterCount = 0;
                //Debug.Log("<color=red>reset: </color>" + iterCount);
            }
        }
    }

    public void Save(List<string> tempResult)
    {
        if (tempResult.Count < 4)
            return;

        result.Add("1");
        for (int i = 0; i < tempResult.Count; i++)
        {
            result.Add(tempResult[i]);
        }
        result.Add("8");

        var temp = result.ToArray();
        Debug.Log(string.Join(",", temp));
        result.Clear();

        //stringArray = new string[] { "\n", "1,2,3", "4,5,6", "7,8,9" };
        //csvWriter.Save(stringArray, "patternData");
    }
}