using System;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class SetParticleImages : MonoBehaviour
{
    private CSVReader csvReader;
    public int pageID;

    public GameObject[] particles;
    public int numMaxBoxes = 6;
    private int numBoxes;
    private int initLine;
    public Color fillColor;

    public GameObject number;
    private StringBuilder lotNumber = new StringBuilder();


    public void Trigger()
    {
        if (csvReader == null)
        {
            csvReader = transform.parent.GetComponent<CSVReader>();
        }       
        numBoxes = csvReader.numBoxes;
        initLine = csvReader.csvInitLine;

        lotNumber.Clear();
        SetImages();
    }

    private void SetImages()
    {
        for (int i = 0; i < numMaxBoxes; i++)
        {
            var images = particles[i].GetComponent<Image>();
            if (i < numBoxes)
            {
                images.color = Color.white;
                var numImages = csvReader.csvData[initLine + pageID][i];
                lotNumber.Append(numImages);
                var imageID = Int32.Parse(numImages);
                images.sprite = csvReader.sourceImages[imageID - 1];
            }
            else if(i >= numBoxes)
            {
                images.color = fillColor;
            }
        }

        number.GetComponent<Text>().text = lotNumber.ToString();
    }
}
