﻿using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SpinLogic : MonoBehaviour
{
    public TurnPage pageSwitcher;
    private int pageID;
    public SetDropModels modelSetter;
    private SetMatTexure materialSetter;
    string lotNumber;

    private bool userHasTouched;

    private float lastX, lastY;
    private float diffX, diffY = 0.5f;
    private int directionX, directionY = 1;
    private float decayLevel = 0.03f;

    private Text patternInfo;
    private CSVWriter csvWriter;

    private void Start()
    {
        pageSwitcher = GameObject.FindWithTag("List").GetComponent<TurnPage>();
        pageID = modelSetter.pageID;
        materialSetter = Camera.main.GetComponent<SetMatTexure>();
        patternInfo = modelSetter.info;
        csvWriter = GameObject.FindWithTag("CSVWriter").GetComponent<CSVWriter>();
    }

    void Update ()
    {
        if (pageSwitcher.currentPage - 1 != pageID) // 現在表示中でなければ...
        {
            transform.rotation = Quaternion.identity; // 回転をリセット.
        }
        else if (pageSwitcher.currentPage - 1 == pageID) // 自分のページが表示されたら...
        {
            // すでに制作されたことがある場合，自分のページが表示されたら(MainCameraに映ったら)カメラの背景色を徐々に変える. 
            if (modelSetter.isExistentInArchive)
            {
                var matType = modelSetter.fixedMat;
                materialSetter.ChangeBGToWhite(matType); // 背景色を白に変える.
            }
            else if (!modelSetter.isExistentInArchive)
            {
                if (SetMatTexure.genButtonPressed)
                {
                    patternInfo.color = Color.black;
                }
                else
                {
                    patternInfo.color = Color.white;
                    materialSetter.ChangeBGToBlack(); // 通常は黒を使用する.
                }
            }

            if(SetMatTexure.genConfirmed)
                this.GenerateNewPattern();

            // タッチで3Dモデルを回転.
            if (Input.GetMouseButton(0))
            {
                diffX = Mathf.Abs(lastX - Input.GetAxis("Mouse X"));
                diffY = Mathf.Abs(lastY - Input.GetAxis("Mouse Y"));
                // X軸方向.
                if (lastX < Input.GetAxis("Mouse X"))
                {
                    directionX = -1;
                    transform.Rotate(Vector3.up, -diffX * 0.8f);
                }
                else if (lastX > Input.GetAxis("Mouse X"))
                {
                    directionX = 1;
                    transform.Rotate(Vector3.up, diffX * 0.8f);
                }
                // Y軸方向.
                if (lastY < Input.GetAxis("Mouse Y"))
                {
                    directionY = -1;
                    transform.Rotate(Vector3.right, -diffY * 3f);
                }
                else if (lastY > Input.GetAxis("Mouse Y"))
                {
                    directionY = 1;
                    transform.Rotate(Vector3.right, diffY * 3f);
                }

                lastX = -Input.GetAxis("Mouse X");
                lastY = -Input.GetAxis("Mouse Y");

                userHasTouched = true;
            }
            else
            {
                if (!userHasTouched)
                    return;
                // マウスボタンを離した後も惰性で回転を続け，徐々に減衰する.
                if (diffX < 0f)
                    diffX += decayLevel;
                else if (diffX > 0f)
                    diffX -= decayLevel;

                if (diffY > 0f)
                    diffY -= decayLevel;
                else if (diffY < 0f)
                    diffY += decayLevel;

                transform.Rotate(Vector3.up, diffX * directionX);
                transform.Rotate(Vector3.right, diffY * directionY);
            }
        }
    }

    /// <summary>
    /// https://stackoverflow.com/questions/14370757/editing-saving-a-row-in-a-csv-file
    /// </summary>
    void GenerateNewPattern()
    {
        lotNumber = modelSetter.lotNumber4CSV;
        //csvWriter.Save(test, "patternData");
        return;

        var path = Application.dataPath + "/Resources/patternData.csv";

        if (!File.Exists(path))
            return;

        using (StreamReader reader = new StreamReader(path))
        {
            String line;
            while ((line = reader.ReadLine()) != null)
            {
                if (line.Contains(lotNumber))
                {
                    String[] split = line.Split(',');
                    if (split[13].Contains(""))
                    {
                        split[13] = "Gold";
                        line = String.Join(",", split);
                        Debug.Log(line);
                    }
                }

            }
        }
    }
}