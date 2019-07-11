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

    public Transform[] particles; // 粒のモデルのテンプレート.メッシュのみを変更して使用する.
    private MeshFilter[] meshFilters;
    [Range(1, 4)]
    public int scaleMultiplier = 4;
    private int numActiveBoxes;
    public Vector3[] offsetPositions;

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
        lotNumber.Clear(); // 番号をクリア.
        SetMeshes();
    }

    private void SetMeshes()
    {
        for (int i = 0; i < particles.Length; i++)
        {
            particles[i].transform.parent.gameObject.SetActive(true);
            meshFilters[i].mesh.Clear(); // すべての粒のメッシュをクリア.

            var modelID = csvReader.csvData[csvInitLine + pageID][i];
            if (modelID != "0")
            {
                numActiveBoxes++;
            }

            if (i > numActiveBoxes - 1)
            {
                particles[i].transform.parent.gameObject.SetActive(false); // 彫刻の中心を回転の中心を揃えるため，使わないBoxは非アクティブにする.
                continue;
            }

            lotNumber.Append(modelID); //  「1」と「8」のあいだの番号を生成.
            var dropID = Int32.Parse(modelID);
            meshFilters[i].mesh = csvReader.sourceMeshes[dropID - 1];
            //offsetPositions[dropID - 1].y *= scaleMultiplier;
            particles[i].transform.localPosition = offsetPositions[dropID - 1];
        }

        number.text = lotNumber.ToString();
        numActiveBoxes = 0;
    }
}
