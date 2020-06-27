using UnityEngine;

/// <summary>
/// 粒が入る枠一つ一つに付いている，粒モデル設定のスクリプト.
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
        filter.mesh = meshes[modelID - 2]; // 可変の2~7番の粒を配列の0番から順に格納しているため，2を引いて数字を合わせている.
    }
}
