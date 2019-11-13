using UnityEngine;
using UnityEngine.UI;

public class SliderValue : MonoBehaviour
{
    public Slider[] sliders;
    public Text[] values;


    void Update()
    {
        for (int i = 0; i < values.Length; i++)
        {
            values[i].text = sliders[i].value.ToString();
        }
    }
}
