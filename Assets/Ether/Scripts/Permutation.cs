using UnityEngine;
using System.Collections.Generic;
using System.Text;

public class Permutation : MonoBehaviour
{
    private List<string> list;
    private List<string> tempResult;
    private int numParicles;
    int lastNumber0;
    int lastNumber1;

    private Camera mainCamera;
    private CSVWriter csvWriter;

    private List<string> result;


    public void OnValueChanged(int dromDownValue)
    {
        numParicles = dromDownValue;
    }

    public void ManualStart()
    {
        list = new List<string>() { "2", "3", "4", "5", "6", "7"};
        tempResult = new List<string>();
        result = new List<string>();

        mainCamera = Camera.main;
        csvWriter = mainCamera.GetComponent<CSVWriter>();

        GeneratePatterns();
    }

    public void GeneratePatterns()
    {
        int count = list.Count;

        for (int i = 0; i < count; i++)
        {
            tempResult.Add(list[i]);
            lastNumber0 = i;

            for (int j = 0; j < count; j++)
            {
                if (j == lastNumber0) continue;
                tempResult.Add(list[j]);
                lastNumber1 = j;

                for (int k = 0; k < count; k++)
                {
                    if (k == lastNumber1) continue;
                    tempResult.Add(list[k]);

                    Save(tempResult);
                    tempResult.RemoveAt(tempResult.Count - 1);
                }

                tempResult.RemoveAt(tempResult.Count - 1);
            }
            tempResult.Clear();
        }
    }

    public void Save(List<string> tempResult)
    {
        //result.Add("\n");
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