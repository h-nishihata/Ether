using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColumnManager : MonoBehaviour
{
    public Slider mainSlider;
    public GameObject[] elements;

    // Update is called once per frame
    public void ChangeNumElements()
    {
        var num = (int)mainSlider.value;
        //Debug.Log("value: " + num);
        for (int i = 0; i < elements.Length; i++)
        {
            if (i < num) elements[i].SetActive(true); else elements[i].SetActive(false);
        }
    }
}
