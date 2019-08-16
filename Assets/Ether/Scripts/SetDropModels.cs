using System;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 各ページの子オブジェクトに付いている，必要分のBoxを有効にするスクリプト. 
/// </summary>
public class SetDropModels : MonoBehaviour
{
    private CSVReader csvReader; // シーン内にある，モデル設定のスクリプト.
    private int csvInitLine;
    public int pageID; // 自分のページ番号.

    public Transform[] boxes; // 各ページの，粒を入れるBox.
    private SwitchActiveDrop[] switchActiveDrops; // 各Boxに付いている，どの粒を有効化するかを決めるスクリプト.

    public Vector3[] offsetPositions; // 3Dモデルをインポートした時点で付いていたオフセットを相殺するための値.

    public Text info;
    private StringBuilder lotNumber = new StringBuilder();

    public bool isExistentInArchive;

    public void ManualStart()
    {
        csvReader = GameObject.FindWithTag("List").GetComponent<CSVReader>();
        switchActiveDrops = new SwitchActiveDrop[boxes.Length];
        for (int i = 0; i < boxes.Length; i++)
        {
            switchActiveDrops[i] = boxes[i].GetComponent<SwitchActiveDrop>();
        }
    }

    /// <summary>
    /// CSVファイル内の自分の行の数字の並びに従って，粒を設定する.
    /// </summary>
    /// <param name="initLine">粒数のグループの始まりの行番号.</param>
    public void SetDrops(int initLine)
    {
        csvInitLine = initLine + 1;
        lotNumber.Clear(); // 番号をクリア.


        for (int i = 0; i < boxes.Length; i++)
        {
            boxes[i].transform.gameObject.SetActive(false); // 一旦すべてのBoxをオフにする.

            var modelID = csvReader.csvData[csvInitLine + pageID][i];
            if (modelID != "0") // 1~8までのいずれかの粒を使うなら(使わない段にはリスト内で「0」を入れている)
                boxes[i].transform.gameObject.SetActive(true); // 使うBoxのみ有効化する.
            else
                continue;

            lotNumber.Append(modelID); // 「1」と「8」のあいだの番号を生成.
            switchActiveDrops[i].Trigger(Int32.Parse(modelID) - 1); // それぞれのBoxに，使用する粒のモデルを伝える.
        }

        isExistentInArchive = UnityEngine.Random.Range(0f, 10f) < 4.5f;
        //CheckExistence(csvInitLine + pageID);
        SetInfo(lotNumber.ToString());
    }

    void CheckExistence(int lineNum)
    {
        var matType = csvReader.csvData[csvInitLine + pageID][13];
        if(matType != "Z")
        {
            isExistentInArchive = true; // すでに制作されたことがある.
            // 背景色を変更(SpinLogic.csで，自分のページが表示中(MainCameraに映っている)かどうか毎フレーム確認しているので，表示されたらカメラの背景色を徐々に変える). 
            // 展示情報を表示(14列目以降の情報を順次読み込む). 文字色を変更.
            // UIパネルを非表示(アルファ値を下げる).
            // アルファベットに応じて，制作された素材を適用(SetMatTextureから行う. その際，選択中のマテリアルは退避させておく).
        }
    }

    void SetInfo(string lotNumber)
    {
        info.text = "Number of drops: " + "\n" +
                    "Pattern: " + lotNumber + "\n" +
                    "Material: " + "\n";
        info.fontSize = (int)(Screen.width * 0.02f);
    }
}
