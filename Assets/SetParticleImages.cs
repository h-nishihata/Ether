using UnityEngine;
using UnityEngine.UI;

public class SetParticleImages : MonoBehaviour
{
    public Transform[] particles;
    private int numBoxes;
    //private int initLine;
    //public Sprite[] sourceImages;


    private void OnEnable()
    {
        numBoxes = Data.Instance.numBoxes;
        //initLine = Data.Instance.csvInitLine;
        PrepareBoxes();
        SetImages();
    }

    private void PrepareBoxes()
    {
        for (int i = 0; i < particles.Length; i++)
        {
            particles[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < numBoxes; i++)
        {
            particles[i].gameObject.SetActive(true);
        }
    }

    private void SetImages()
    {
        for (int i = 0; i < numBoxes; i++)
        {
            var images = particles[i].GetComponent<Image>().sprite;
            //var numImages = csvData[numLine][0];
            //images = sourceImages[];
        }
    }
}
