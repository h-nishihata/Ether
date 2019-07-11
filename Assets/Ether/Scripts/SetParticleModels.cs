using System;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 各ページの子オブジェクトについている，モデル設定のためのスクリプト.
/// </summary>
public class SetParticleModels : MonoBehaviour
{
    private CSVReader3D csvReader;// シーン内にある，モデル設定のスクリプト.
    private int csvInitLine;
    public int pageID;

    public Transform[] particles;
    public MeshFilter[] meshFilters;
    //private int numMaxBoxes = 13;
    private int numBoxes;
    //private int bottomBoxOffset;
    public Vector3[] offsetValues;

    public Text number;
    private StringBuilder lotNumber = new StringBuilder();

    public void ManualStart()
    {
        csvReader = GameObject.FindWithTag("List").GetComponent<CSVReader3D>();
        meshFilters = new MeshFilter[particles.Length];
        for (int i = 0; i < particles.Length; i++)
        {
            meshFilters[i] = particles[i].GetComponent<MeshFilter>();
        }
    }

    public void Trigger()
    {
        csvInitLine = csvReader.csvInitLine;
        lotNumber.Clear();
        SetMeshes();
    }

    private void SetMeshes()
    {
        for (int i = 0; i < particles.Length; i++)
        {
            meshFilters[i].mesh.Clear(); // すべての粒のメッシュをクリア.
            var modelID = csvReader.csvData[csvInitLine + pageID][i];
            if (modelID != "0")
            {
                numBoxes++;
            }
            //}
            //bottomBoxOffset = (particles.Length - numBoxes) / 2;
            //for (int i = 0; i < numBoxes; i++)
            //{
            //var modelNumber = csvReader.csvData[csvInitLine + pageID][i];
            if (i > numBoxes - 1)
                continue;

            lotNumber.Append(modelID); //  「1」と「8」のあいだの番号を生成.
            var dropID = Int32.Parse(modelID);
            meshFilters[i].sharedMesh = csvReader.sourceMeshes[dropID - 1].sharedMesh;
            particles[i].transform.localPosition = offsetValues[dropID - 1];
        }
        number.text = lotNumber.ToString();
        numBoxes = 0;
    }
}
