using System;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class SetParticleModels : MonoBehaviour
{
    private CSVReader3D csvReader;
    public int pageID;

    public Transform[] particles;
    private int numMaxBoxes = 13;
    //private int numBoxes;
    private int initLine;
    public MeshFilter[] meshes;
    public Color fillColor;

    public Text number;
    private StringBuilder lotNumber = new StringBuilder();

    public void WarmUp()
    {
        csvReader = GameObject.FindWithTag("List").GetComponent<CSVReader3D>();
        meshes = new MeshFilter[particles.Length];
        for (int i = 0; i < particles.Length; i++)
        {
            meshes[i] = particles[i].GetComponent<MeshFilter>();
        }
    }

    public void Trigger()
    {
        initLine = csvReader.csvInitLine;
        lotNumber.Clear();
        SetMeshes();
    }

    private void SetMeshes()
    {
        for (int i = 0; i < numMaxBoxes; i++)
        {
            meshes[i].mesh.Clear();

            var numImages = csvReader.csvData[initLine + pageID][i];
            if (numImages != "0")
            {
                lotNumber.Append(numImages);
                var imageID = Int32.Parse(numImages);
                meshes[i].sharedMesh = csvReader.sourceMeshes[imageID - 1].sharedMesh;
            }
        }
        number.text = lotNumber.ToString();
    }

}
