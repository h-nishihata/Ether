using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColumnManager : MonoBehaviour
{
    public Slider mainSlider;
    public GameObject[] elements;
    public DropMe[] dropMes;

    private void Start()
    {
        dropMes = new DropMe[elements.Length];
        for (int i = 0; i < elements.Length; i++)
        {
            dropMes[i] = elements[i].GetComponentInChildren<DropMe>();
            elements[i].SetActive(false);
        }
        elements[0].SetActive(true);
    }

    public void ChangeNumElements()
    {
        var num = (int)mainSlider.value;
        for (int i = 0; i < elements.Length; i++)
        {
            if (i < num) elements[i].SetActive(true); else elements[i].SetActive(false);
        }
    }

    public void SavePatterns()
    {
        for (int i = 0; i < elements.Length; i++)
        {
            if(elements[i].activeSelf)
            {
                Debug.Log(i + ": " + dropMes[i].latestSpriteNum);
                // save values
            }
        }
    }
}
