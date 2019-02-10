using System;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class SetParticleImages : MonoBehaviour
{
    private CSVReader csvReader;
    public int pageID;

    public GameObject[] particles;
    private int numBoxes;
    private int initLine;

    public GameObject number;
    private StringBuilder lotNumber = new StringBuilder();


    private void OnEnable()
    {
        numBoxes = Data.Instance.numBoxes;
        initLine = Data.Instance.csvInitLine;
        csvReader = transform.parent.GetComponent<CSVReader>();
        lotNumber.Append("1");
        PrepareBoxes();
        SetImages();
    }

    private void PrepareBoxes()
    {
        for (int i = 0; i < particles.Length; i++)
        {
            particles[i].SetActive(false);
        }
        for (int i = 0; i < numBoxes; i++)
        {
            particles[i].SetActive(true);
        }
    }

    private void SetImages()
    {
        for (int i = 0; i < numBoxes; i++)
        {
            var images = particles[i].GetComponent<Image>();
            var numImages = csvReader.csvData[initLine + pageID][i];
            lotNumber.Append(numImages);
            var imageID = Int32.Parse(numImages);
            images.sprite = csvReader.sourceImages[imageID - 2]; //2〜7段目が可変パーティクルになるよう設定.
        }

        SetLotNumber();
    }

    private void SetLotNumber()
    {
        lotNumber.Append("8");
        number.GetComponent<Text>().text = lotNumber.ToString();
    }
}
