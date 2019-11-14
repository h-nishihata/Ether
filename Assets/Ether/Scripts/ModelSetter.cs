using UnityEngine;

/// <summary>
/// (2) 粒が入る枠一つ一つに付いている，粒モデル設定のスクリプト.
/// </summary>
public class ModelSetter : MonoBehaviour
{
    private MeshFilter filter;
    public Mesh[] meshes;


    void Start()
    {
        filter = this.gameObject.GetComponent<MeshFilter>();
    }

    public void SetModel(int modelID)
    {
        filter.mesh = meshes[modelID];
    }
}
