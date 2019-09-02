using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Scene内の任意の場所(MainCamera)にアタッチされた，マテリアル管理のためのスクリプト.
/// </summary>
public class MatTexSetter : MonoBehaviour {

    public Material textureMat;
    public Texture[] textures;
    public Texture[] normalMaps;

    public Text numDropsInfo;
    public Text matTypeInfo;
    private int lastMat;
    public string lastMatName;

    public Color lightBGColor = new Color(0.93f, 0.93f, 0.88f, 1f);
    private float colTransToLight;
    private float colTransToDark;
    public Transform[] UIobjects;

    public static bool genButtonPressed;
    public static bool genConfirmed;
    public Transform confirmButton;

    public Text genMessage;
    private CSVReader csvReader;


    void Start () {
        this.SetTexture(0);
        csvReader = GameObject.FindWithTag("List").GetComponent<CSVReader>();
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
        if (Camera.main.backgroundColor == lightBGColor)
            return;

        for (int i = 0; i < UIobjects.Length; i++)
        {
            UIobjects[i].transform.gameObject.SetActive(false); // UIパネルを非表示.
        }

        if (matType != null)
        {
            switch (matType)
            {
                case "gold":
                    this.SetTexture(0, true);
                    break;
                case "platinum":
                    this.SetTexture(1, true);
                    break;
                case "micro_beads":
                    this.SetTexture(2, true);
                    break;
                case "alumina":
                    this.SetTexture(3, true);
                    break;
                case "particle":
                    this.SetTexture(4, true);
                    break;
                default:
                    this.SetTexture(5, true);
                    break;
            }
        }

        numDropsInfo.color = matTypeInfo.color = Color.black;
        colTransToDark = 0f;
        if (colTransToLight < 1f)
            colTransToLight += Time.deltaTime;
        Camera.main.backgroundColor = Color.Lerp(Color.black, lightBGColor, colTransToLight);
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
        colTransToLight = 0f;
        if (colTransToDark < 1f)
            colTransToDark += Time.deltaTime;
        Camera.main.backgroundColor = Color.Lerp(lightBGColor, Color.black, colTransToDark);
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
        Camera.main.backgroundColor = isGenerateMode ? lightBGColor : Color.black;

        genMessage.text = "Generate this pattern ?";
        genMessage.color = isGenerateMode ? Color.black : new Color(0, 0, 0, 0);
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

    public void Reset()
    {
        csvReader.OnValueChanged(0);
        MatTexSetter.genButtonPressed = MatTexSetter.genConfirmed = false;
        genMessage.color = new Color(0, 0, 0, 0);
        for (int i = 0; i < UIobjects.Length; i++)
        {
            UIobjects[i].transform.gameObject.SetActive(true); // UIパネルを非表示.
        }
        this.SetTexture(0);
    }
}