using UnityEngine;
using System.Collections.Generic;

public class Permutation : MonoBehaviour
{
    private List<string> list;
    private List<string> result;

    private Camera mainCamera;
    private CSVWriter csvWriter;
    int lastNumber0;
    int lastNumber1;


    private void Start()
    {
        list = new List<string>() { "2", "3", "4", "5", "6", "7"};
        result = new List<string>();

        mainCamera = Camera.main;
        csvWriter = mainCamera.GetComponent<CSVWriter>();

        permutation(list, result);
    }

    private void permutation(List<string> list, List<string> result)
    {
        int count = list.Count;

        for (int i = 0; i < count; i++)
        {
            result.Add(list[i]);
            lastNumber0 = i;

            for (int j = 0; j < count; j++)
            {
                if (j == lastNumber0) continue;
                result.Add(list[j]);
                lastNumber1 = j;

                for (int k = 0; k < count; k++)
                {
                    if (k == lastNumber1) continue;
                    result.Add(list[k]);

                    string[] tmp = result.ToArray();
                    Debug.Log(string.Join(",", tmp));

                    result.RemoveAt(result.Count - 1);
                }

                result.RemoveAt(result.Count - 1);
            }
            result.Clear();
        }
    }

    public void Save(int numParticles)
    {
        var testData = new string[] { "\n", "1,2,3", "4,5,6", "7,8,9" };
        csvWriter.Save(testData, "patternData");
    }
}