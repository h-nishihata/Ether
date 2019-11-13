using System;
using System.Collections.Generic;
using UnityEngine;


public class RandomNumbers : MonoBehaviour
{
    string[] pattern = new string[11];
    //string prevNum;
    List<string> numList = new List<string> {"1", "2", "3", "4", "5", "6", "7"};
    //int numDrops;
    string num;
    public ModelSetter[] modelSetters;

    // Start is called before the first frame update
    public void Generate()
    {
        //numDrops = 3;
        for (int i = 0; i < DropNumSwitcher.numDrops; i++)
        {
            num = numList[UnityEngine.Random.Range(0, numList.Count)];
            pattern[i] = num;
            numList.Remove(num);
            /*
            do
            {
                num = (Random.Range(1, 7)).ToString();
            } while (num == prevNum);

            pattern[i] = num;
            prevNum = pattern[i];
            */
            //Debug.Log(pattern[i]);
            modelSetters[i].SetModel(Int32.Parse(num)-1);
        }
        // Reset
        for (int i = 1; i < 8; i++)
        {
            numList.Add(i.ToString());
        }
    }
}
