using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class Permutation : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

        List<string> result = new List<string>();
        List<string> ABCD = new List<string>() { "2", "3", "4", "5" };
        permutation(ABCD, result);


    }

    void permutation(List<string> ABCD, List<string> result)
    {
        int count = ABCD.Count;

        for (int i = 0; i < count; i++)
        {
            result.Add(ABCD[i]);
            ABCD.RemoveAt(i);

            if (count >= 2)
            {
                permutation(ABCD, result);
                ABCD.Insert(i, result[result.Count - 1]);
                result.RemoveAt(result.Count - 1);
            }

            if (count == 1)
            {
                string[] tmp = result.ToArray();
                Debug.Log(string.Join(",", tmp));
                ABCD.Insert(i, result[result.Count - 1]);
                result.RemoveAt(result.Count - 1);
            }


        }

    }
}