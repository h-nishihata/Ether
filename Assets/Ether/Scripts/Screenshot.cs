using System.Collections;
using UnityEngine;

/// <summary>
/// スクリーンショットの撮影と保存，及びUIパネルの表示の切替を行うスクリプト.
/// </summary>
public class Screenshot : MonoBehaviour
{
    public Transform UIPanel;
    private bool UIActive = true;

    private bool shutterPressed;

    private bool flashStarted;
    private float flashDuration = 1.0f;
    public Renderer flashPlane;
    private Material flashMaterial;


    private void Start()
    {

        flashMaterial = flashPlane.material;
    }

    private void Update()
    {
        // UIパネル非表示時に，画面をタップすると再度表示する.
        if (!UIActive && Input.GetMouseButtonDown(0))
            switchUIPanel(true);

        if (shutterPressed)
        {
            StartCoroutine(TakeScreenshotAndSave());
            shutterPressed = false;
        }

        if (flashStarted)
        {
            if (flashDuration > 0.0f)
            {
                flashDuration -= 0.02f;
                flashMaterial.SetFloat("_Blend", flashDuration);
            }
            else
            {
                flashStarted = false;
                flashDuration = 1f;
                switchUIPanel(true);
            }
        }
    }

    /// <summary>
    /// UIパネルの表示/非表示の切替をする.
    /// </summary>
    public void switchUIPanel(bool isActive)
    {
        UIPanel.gameObject.SetActive(isActive);
        UIActive = isActive;
    }

    /// <summary>
    /// 「Screenshot」ボタンの押下を受け付ける.
    /// </summary>
    public void PressShutter()
    {
        if (shutterPressed)
            return;

        switchUIPanel(false);
        shutterPressed = true;
    }

    /// <summary>
    /// iOSデバイスのPhotosアプリにスクリーンショットを保存する.
    /// </summary>
    private IEnumerator TakeScreenshotAndSave()
    {
        yield return new WaitForEndOfFrame();

        Texture2D screenShot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        screenShot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        screenShot.Apply();

        // 現在時刻からファイル名を決定(iOSだとバグる).
        //var fileName = System.DateTime.Now.ToString("/yyMMdd_HHmmss") + ".png";

        //Debug.Log("Permission result: " + NativeGallery.SaveImageToGallery(screenShot, "Direction", "Image.png"));

        flashStarted = true;
        flashMaterial.SetFloat("_Blend", flashDuration);

        Destroy(screenShot);
    }
}
