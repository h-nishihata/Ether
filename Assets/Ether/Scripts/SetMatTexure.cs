using UnityEngine;

public class SetMatTexure : MonoBehaviour {

    public Material textureMat;
    public Texture[] textures;
    public Texture[] normalMaps;


    // Use this for initialization
    void Start () {
        textureMat.SetTexture("_MainTex", textures[0]);
        textureMat.SetTexture("_BumpMap", normalMaps[0]);
    }

    public void SetDefaultTexture()
    {
        textureMat.SetTexture("_MainTex", textures[0]);
        textureMat.SetTexture("_BumpMap", normalMaps[0]);
    }

    public void SetTextureA()
    {
        textureMat.SetTexture("_MainTex", textures[1]);
        textureMat.SetTexture("_BumpMap", null);
    }

    public void SetTextureB()
    {
        textureMat.SetTexture("_MainTex", textures[2]);
        textureMat.SetTexture("_BumpMap", null);
    }
}