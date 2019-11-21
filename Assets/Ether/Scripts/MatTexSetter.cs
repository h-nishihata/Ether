using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Scene内の任意の場所(MainCamera)にアタッチされた，マテリアル管理のためのスクリプト.
/// </summary>
public class MatTexSetter : MonoBehaviour {

    public Slider slider;
    public Material textureMat;
    public Texture[] textures;
    public Texture[] normalMaps;
    public Text matTypeInfo;


    void Start () {
        this.SetTexture(0);
    }

    public void SetTexture(int matType)
    {
        if (matType < 0)
            matType = (int)slider.value;    

        textureMat.SetTexture("_MainTex", textures[matType]);
        textureMat.SetTexture("_BumpMap", normalMaps[matType]);
        matTypeInfo.text = "Material:" + "\n" + textures[matType].name;
    }
}