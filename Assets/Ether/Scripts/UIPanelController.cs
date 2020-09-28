using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Main Cameraに付いている，UIパネルの表示/非表示を切り替えるためのスクリプト.
/// </summary>
public class UIPanelController : MonoBehaviour
{
    public Transform UIPanel;
    private Camera cam;
    public bool isSwitching;


    private void Start()
    {
        cam = Camera.main;
    }

    public void ShowUIPanel(bool status)
    {
        UIPanel.gameObject.SetActive(status);
        this.isSwitching = true;
    }

    private void Update()
    {
        if ((!UIPanel.gameObject.activeSelf) && Input.GetMouseButton(0))
            this.ShowUIPanel(true);
        if(isSwitching && !Rotation.isEyeMode)
            this.TranslateCamPos();

        if (RandomNumGenerator.isArchiveMode)
            cam.backgroundColor = Color.gray;
        else
            cam.backgroundColor = Color.black;
    }

    /// <summary>
    /// UIパネルを非表示にしたとき，モデルを画面の中央に移動させる.
    /// </summary>
    void TranslateCamPos()
    {
        if (!UIPanel.gameObject.activeSelf)
        {
            if (cam.transform.localPosition.x < 0f)
                cam.transform.Translate(Vector3.right * Time.deltaTime * 2f);
            else
                isSwitching = false;
        }
        else if (UIPanel.gameObject.activeSelf)
        {
            if (cam.transform.localPosition.x > -2f)
                cam.transform.Translate(-Vector3.right * Time.deltaTime * 2f);
            else
                isSwitching = false;
        }
    }
}
