using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class Permutation : MonoBehaviour
{
    private List<string> result;
    private List<string> AB;
    private List<string> ABC;
    private List<string> ABCD;
    private List<string> ABCDE;
    private List<string> ABCDEF;
    private int numParticles = 3;

    private void Start()
    {
        result = new List<string>();
        AB = new List<string>() { "2", "3"};
        ABC = new List<string>() { "2", "3", "4"};
        ABCD = new List<string>() { "2", "3", "4", "5" };
        ABCDE = new List<string>() { "2", "3", "4", "5", "6" };
        ABCDEF = new List<string>() { "2", "3", "4", "5", "6", "7" };
    }

    public void OnValueChanged(int dropDownValue)
    {
        numParticles = dropDownValue;
    }

    public void GeneratePatterns()
    {
        switch (numParticles)
        {
            case 0:
                permutation(AB, result);
                break;
            case 1:
                permutation(ABC, result);
                break;
            case 2:
                permutation(ABCD, result);
                break;
            case 3:
                permutation(ABCDE, result);
                break;
            case 4:
                permutation(ABCDEF, result);
                break;
        }
    }

    private void permutation(List<string> list, List<string> result)
    {
        int count = list.Count;

        for (int i = 0; i < count; i++)
        {
            result.Add(list[i]);
            list.RemoveAt(i);

            if (count >= 2)
            {
                permutation(list, result);
                list.Insert(i, result[result.Count - 1]);
                result.RemoveAt(result.Count - 1);
            }

            if (count == 1)
            {
                string[] tmp = result.ToArray();
                Debug.Log(string.Join(",", tmp));
                list.Insert(i, result[result.Count - 1]);
                result.RemoveAt(result.Count - 1);
            }
        }
    }
}