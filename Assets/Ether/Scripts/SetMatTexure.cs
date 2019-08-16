using UnityEngine;

public class SetMatTexure : MonoBehaviour {

    public Material textureMat;
    public Texture[] textures;
    public Texture[] normalMaps;

    public Color targetColor = new Color(0.93f, 0.93f, 0.88f, 1f);
    private float colTransToWhite;
    private float colTransToBlack;


    void Start () {
        this.SetTexture(0);
    }

    public void SetTexture(int matType)
    {
        textureMat.SetTexture("_MainTex", textures[matType]);
        textureMat.SetTexture("_BumpMap", normalMaps[matType]);
    }

    public void ChangeBGToWhite()
    {
        colTransToBlack = 0f;
        if (colTransToWhite < 1f)
            colTransToWhite += Time.deltaTime;
        Camera.main.backgroundColor = Color.Lerp(Color.black, targetColor, colTransToWhite);
    }

    public void ChangeBGToBlack()
    {
        colTransToWhite = 0f;
        if (colTransToBlack < 1f)
            colTransToBlack += Time.deltaTime;
        Camera.main.backgroundColor = Color.Lerp(targetColor, Color.black, colTransToBlack);
    }
}