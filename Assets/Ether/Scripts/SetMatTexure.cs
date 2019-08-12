using UnityEngine;

public class SetMatTexure : MonoBehaviour {

    public Material textureMat;
    public Texture[] textures;
    public Texture[] normalMaps;


    void Start () {
        this.SetTexture(0);
    }

    public void SetTexture(int matType)
    {
        textureMat.SetTexture("_MainTex", textures[matType]);
        textureMat.SetTexture("_BumpMap", normalMaps[matType]);
    }
}