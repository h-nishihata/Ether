using UnityEngine;
using UnityEngine.UI;

public class SetMatTexure : MonoBehaviour {

    public Material textureMat;
    public Texture[] textures;
    public Texture[] normalMaps;

    public Text numDropsInfo;
    public Text matTypeInfo;
    private int lastMat;
    public string lastMatName;

    public Color targetColor = new Color(0.93f, 0.93f, 0.88f, 1f);
    private float colTransToWhite;
    private float colTransToBlack;
    public Transform[] UIobjects;

    public static bool genButtonPressed;
    public static bool genConfirmed;
    public Transform confirmButton;


    void Start () {
        this.SetTexture(0);
    }

    public virtual void SetTexture(int matType)
    {
        textureMat.SetTexture("_MainTex", textures[matType]);
        textureMat.SetTexture("_BumpMap", normalMaps[matType]);
        SetInfo(textures[matType].name);
        lastMat = matType;
        lastMatName = textures[lastMat].name;
    }

    /// <summary>
    /// SetTexture()のオーバーロード関数. 
    /// ユーザが選択中のマテリアルをlastMatに退避させておきたいが，素材が決まっている制作済パターンのときにも同じメソッドを使うと，上書きされてしまうため.
    /// </summary>
    public void SetTexture(int matType, bool isSetFromCSV)
    {
        textureMat.SetTexture("_MainTex", textures[matType]);
        textureMat.SetTexture("_BumpMap", normalMaps[matType]);
        SetInfo(textures[matType].name);
    }

    public void ChangeBGToWhite(string matType)
    {
        if (Camera.main.backgroundColor == targetColor)
            return;

        for (int i = 0; i < UIobjects.Length; i++)
        {
            UIobjects[i].transform.gameObject.SetActive(false); // UIパネルを非表示.
        }

        if (matType != null)
        {
            switch (matType)
            {
                case "Gold":
                    this.SetTexture(0, true);
                    break;
                case "platinum":
                    this.SetTexture(1, true);
                    break;
                case "MicroBeads":
                    this.SetTexture(2, true);
                    break;
                case "Alumina":
                    this.SetTexture(3, true);
                    break;
                case "Particle":
                    this.SetTexture(4, true);
                    break;
            }
        }

        numDropsInfo.color = matTypeInfo.color = Color.black;
        colTransToBlack = 0f;
        if (colTransToWhite < 1f)
            colTransToWhite += Time.deltaTime;
        Camera.main.backgroundColor = Color.Lerp(Color.black, targetColor, colTransToWhite);
    }

    public void ChangeBGToBlack()
    {
        if (Camera.main.backgroundColor == Color.black || genButtonPressed)
            return;

        for (int i = 0; i < UIobjects.Length; i++)
        {
            UIobjects[i].transform.gameObject.SetActive(true); // UIパネルを表示.
        }
        this.SetTexture(lastMat);

        numDropsInfo.color = matTypeInfo.color = Color.white;
        colTransToWhite = 0f;
        if (colTransToBlack < 1f)
            colTransToBlack += Time.deltaTime;
        Camera.main.backgroundColor = Color.Lerp(targetColor, Color.black, colTransToBlack);
    }

    public void ConfirmGenerate(bool isGenerateMode)
    {
        genButtonPressed = isGenerateMode;
        for (int i = 0; i < UIobjects.Length; i++)
        {
            UIobjects[i].transform.gameObject.SetActive(!isGenerateMode); // UIパネルを非表示.
        }
        numDropsInfo.color = matTypeInfo.color = isGenerateMode ? Color.black : Color.white;
        confirmButton.transform.gameObject.SetActive(isGenerateMode); // 確認用ボタンを表示.
        Camera.main.backgroundColor = isGenerateMode ? targetColor : Color.black;
    }

    public void Generate()
    {
        genConfirmed = true;
        confirmButton.transform.gameObject.SetActive(false);
        Camera.main.backgroundColor = Color.black;
    }

    void SetInfo(string matName)
    {
        matTypeInfo.text = "material type: " + matName;
    }
}