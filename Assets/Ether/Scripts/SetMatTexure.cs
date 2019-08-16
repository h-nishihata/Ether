using UnityEngine;

public class SetMatTexure : MonoBehaviour {

    public Material textureMat;
    public Texture[] textures;
    public Texture[] normalMaps;

    public Color targetColor = new Color(0.93f, 0.93f, 0.88f, 1f);
    private float colTransToWhite;
    private float colTransToBlack;

    public Transform UIPanel;

    private int lastMat;


    void Start () {
        this.SetTexture(0);
    }

    public virtual void SetTexture(int matType)
    {
        textureMat.SetTexture("_MainTex", textures[matType]);
        textureMat.SetTexture("_BumpMap", normalMaps[matType]);
        lastMat = matType;
    }

    /// <summary>
    /// SetTexture()のオーバーロード関数. 
    /// lastMatにユーザが選択中のマテリアルを退避させておきたいが，素材が決まっている制作済パターンのときにも同じメソッドを使うと，上書きされてしまうため.
    /// </summary>
    public void SetTexture(int matType, bool isSetFromCSV)
    {
        textureMat.SetTexture("_MainTex", textures[matType]);
        textureMat.SetTexture("_BumpMap", normalMaps[matType]);
    }

    public void ChangeBGToWhite(string matType)
    {
        UIPanel.transform.gameObject.SetActive(false); // UIパネルを非表示.

        if (matType != null)
        {
            switch (matType)
            {
                case "Alumina":
                    this.SetTexture(3, true);
                    break;
                case "Particle":
                    this.SetTexture(4, true);
                    break;
            }

        }

        colTransToBlack = 0f;
        if (colTransToWhite < 1f)
            colTransToWhite += Time.deltaTime;
        Camera.main.backgroundColor = Color.Lerp(Color.black, targetColor, colTransToWhite);
    }

    public void ChangeBGToBlack()
    {
        this.SetTexture(lastMat);
        if (Camera.main.backgroundColor == Color.black)
            return;

        UIPanel.transform.gameObject.SetActive(true); // UIパネルを表示.

        colTransToWhite = 0f;
        if (colTransToBlack < 1f)
            colTransToBlack += Time.deltaTime;
        Camera.main.backgroundColor = Color.Lerp(targetColor, Color.black, colTransToBlack);
    }
}