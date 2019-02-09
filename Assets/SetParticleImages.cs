using System;
using UnityEngine;
using UnityEngine.UI;

public class SetParticleImages : MonoBehaviour
{
    public GameObject[] particles;
    private CSVReader csvReader;
    public int pageID;
    private int numBoxes;
    private int initLine;


    private void OnEnable()
    {
        numBoxes = Data.Instance.numBoxes;
        initLine = Data.Instance.csvInitLine;
        csvReader = transform.parent.GetComponent<CSVReader>();
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
            var numImages = Int32.Parse(csvReader.csvData[initLine + pageID][i]);
            images.sprite = csvReader.sourceImages[numImages - 2]; //2〜7段目が可変パーティクルになるよう設定.
        }
    }
}
