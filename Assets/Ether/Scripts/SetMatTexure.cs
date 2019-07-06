using UnityEngine;

public class SetMatTexure : MonoBehaviour {

    public Material textureMat;
    public Texture[] textures;


    // Use this for initialization
    void Start () {
        textureMat.SetTexture("_MainTex", textures[0]);
    }

    public void SetDefaultTexture()
    {
        textureMat.SetTexture("_MainTex", textures[0]);
    }

    public void SetTextureA()
    {
        textureMat.SetTexture("_MainTex", textures[1]);
    }

    public void SetTextureB()
    {
        textureMat.SetTexture("_MainTex", textures[2]);
    }
}